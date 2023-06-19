﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;
using TareasMVC.Models;
using TareasMVC.Servicios;

namespace TareasMVC.Controllers
{
    [Route("api/tareas")]
    public class TareasController : ControllerBase /*web appi*/
    {
        private readonly ApplicationDbContext context;
        private readonly IserviciosUsuarios servicioUsuarios;
        private readonly IMapper mapper;

        public TareasController(ApplicationDbContext context,
            IserviciosUsuarios servicioUsuarios,
            IMapper mapper)
        {
            this.context = context;
            this.servicioUsuarios = servicioUsuarios;
            this.mapper = mapper;
        }    

        [HttpGet]
        public async Task<ActionResult<List<TareaDTO>>> Get()
        {
            var usuarioId = servicioUsuarios.ObtenerusuarioId();
            var tareas = await context.Tareas
                .Where(t => t.UsuarioCreacionId == usuarioId)
                .OrderBy(t => t.Orden)
                .ProjectTo<TareaDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return tareas;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
        {
            var usuarioId = servicioUsuarios.ObtenerusuarioId();

            var existenTareas = await context.Tareas.AnyAsync(t => t.UsuarioCreacionId == usuarioId);

            var ordenMayor = 0;
            if (existenTareas)
            {
                ordenMayor = await context.Tareas.Where(t => t.UsuarioCreacionId == usuarioId)
                    .Select(t => t.Orden).MaxAsync();
            }

            var tarea = new Tarea
            {
                Titulo = titulo,
                UsuarioCreacionId = usuarioId,
                FechaCreacion = DateTime.UtcNow,
                Descripcion = titulo,
                Orden = ordenMayor + 1
            };
            context.Add(tarea);
            await context.SaveChangesAsync();

            return tarea;
        }

        [HttpPost("ordenar")]
        public async Task<IActionResult> Ordenar([FromBody] int[] ids)
        {
            var usuarioId = servicioUsuarios.ObtenerusuarioId();

            var tareas = await context.Tareas
                .Where(t => t.UsuarioCreacionId == usuarioId).ToListAsync();

            var tareasId = tareas.Select(t => t.Id);

            var idsTareasNoPertenecenAlUsuario = ids.Except(tareasId).ToList();

            if (idsTareasNoPertenecenAlUsuario.Any())
            {
                return Forbid();
            }

            var tareasDicionario = tareas.ToDictionary(x => x.Id);

            for (int i = 0; i < ids.Length; i++)
            {
                var id = ids[i];
                var tarea = tareasDicionario[id];
                tarea.Orden = i + 1;
            }

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
