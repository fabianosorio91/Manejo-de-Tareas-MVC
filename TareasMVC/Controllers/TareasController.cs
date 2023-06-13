using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;
using TareasMVC.Migrations;
using TareasMVC.Models;
using TareasMVC.Servicios;

namespace TareasMVC.Controllers
{
    [Route("api/tareas")]
    public class TareasController : ControllerBase /*web appi*/
    {
        private readonly ApplicationDbContext context;
        private readonly IserviciosUsuarios servicioUsuarios;

        public TareasController(ApplicationDbContext context, IserviciosUsuarios servicioUsuarios)
        {
            this.context = context;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<List<TareaDTO>> Get()
        {
            var usuarioId = servicioUsuarios.ObtenerusuarioId();
            var tareas = await context.Tareas
                .Where(t => t.UsuarioCreacionId == usuarioId)
                .OrderBy(t => t.Orden)
                .Select(t => new TareaDTO 
                {
                    Id = t.Id,
                    Titulo =t.Titulo
                })
                .ToListAsync();

            return tareas;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
