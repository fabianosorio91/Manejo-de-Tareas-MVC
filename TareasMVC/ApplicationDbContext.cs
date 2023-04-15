using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;

namespace TareasMVC
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //es como poner [require] en la clase tarea
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Tarea>().Property(t => t.Descripcion).HasMaxLength(250);
        }

        //para crear la tabla Tareas
        public DbSet<Tarea> Tareas { get; set; } //tabla que vamos a crear y el nombre de la tabla
        public DbSet<Paso> Pasos { get; set; }
        public DbSet<ArchivoAdjunto> ArchivoAdjuntos { get; set; }
    }
}
