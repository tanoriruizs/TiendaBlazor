using TiendaBlazor.Models;
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
                new Usuario { Id = 1, Nombre = "Admin", Correo = "admin@correo.com", Contraseña = "admin123", Rol = "Admin" },
                new Usuario { Id = 2, Nombre = "Cliente", Correo = "cliente@correo.com", Contraseña = "cliente123", Rol = "Cliente" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Macbook", Descripcion = "Laptop para trabajo", Precio = 10.99m, Stock = 100, Categoria = "Electronicos", Imagen = File.ReadAllBytes("wwwroot/img/productos/mac.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 2, Nombre = "Playera Polo", Descripcion = "Playera polo elegante", Precio = 20.99m, Stock = 200, Categoria = "Ropa", Imagen = File.ReadAllBytes("wwwroot/img/productos/polo.jpg"), TipoImagen = "image/jpg" },
                new Producto { Id = 3, Nombre = "Converse", Descripcion = "Tenis comodos", Precio = 30.99m, Stock = 300, Categoria = "Calzado", Imagen = File.ReadAllBytes("wwwroot/img/productos/converse.png"), TipoImagen = "image/png" },
                new Producto { Id = 4, Nombre = "Refigerador", Descripcion = "Refigerador moderno", Precio = 40.99m, Stock = 400, Categoria = "Linea Blanca", Imagen = File.ReadAllBytes("wwwroot/img/productos/refigerador.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 5, Nombre = "Iphone", Descripcion = "Iphone moderno", Precio = 50.99m, Stock = 500, Categoria = "Electronicos", Imagen = File.ReadAllBytes("wwwroot/img/productos/iphone.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 6, Nombre = "Pantalon Levis", Descripcion = "Pantalon comodo", Precio = 60.99m, Stock = 600, Categoria = "Ropa", Imagen = File.ReadAllBytes("wwwroot/img/productos/pantalon.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 7, Nombre = "Nike", Descripcion = "Tenis comodos", Precio = 70.99m, Stock = 700, Categoria = "Calzado", Imagen = File.ReadAllBytes("wwwroot/img/productos/nike.jpg"), TipoImagen = "image/jpg" },
                new Producto { Id = 8, Nombre = "Lavadora", Descripcion = "Lavadora moderna", Precio = 80.99m, Stock = 800, Categoria = "Linea Blanca", Imagen = File.ReadAllBytes("wwwroot/img/productos/lavadora.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 9, Nombre = "Audifonos", Descripcion = "Audifonos modernos", Precio = 90.99m, Stock = 900, Categoria = "Electronicos", Imagen = File.ReadAllBytes("wwwroot/img/productos/audifonos.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 10, Nombre = "Playera Adidas", Descripcion = "PLayera comoda", Precio = 100.99m, Stock = 1000, Categoria = "Ropa", Imagen = File.ReadAllBytes("wwwroot/img/productos/playeraadidas.jpg"), TipoImagen = "image/jpg" }
            );
        }
    }
}
