using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaBlazor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrito> Carritos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Correo = "admin@correo.com", Contraseña = "admin123", Rol = "Admin" },
                new Usuario { Id = 2, Correo = "cliente@correo.com", Contraseña = "cliente123", Rol = "Cliente" }
            );
        }

    }
}
