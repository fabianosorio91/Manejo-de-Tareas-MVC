using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Entidades
{
    public class Tarea
    {
        public int Id { get; set; } //si se llama Id, se pone como primary key
        [StringLength(250)] //propiedad que se agrega cuando se hace la migracion
        [Required]//para que el campo no sea nulo
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public DateTime FechaCreacion { get; set; }
        //llave foranea
        public string UsuarioCreacionId { get; set; }
        public IdentityUser UsuarioCreacion { get; set; }
        public List<Paso> Pasos { get; set; } //relacion uno a muchos, una tareas, varios pasos. y un paso una unica tarea
    }
}

