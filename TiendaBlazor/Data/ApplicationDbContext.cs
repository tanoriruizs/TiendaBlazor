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
            // Configuración de precisión para Producto.Precio y Carrito.Total
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Carrito>()
                .Property(c => c.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Admin", Email = "admin@correo.com", Contraseña = "admin123", Rol = "Admin" },
                new Usuario { Id = 2, Nombre = "Cliente", Email = "cliente@correo.com", Contraseña = "cliente123", Rol = "Cliente" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Macbook", Descripcion = "Laptop para trabajo", Precio = Math.Round(10.99m, 2), Stock = 100, Categoria = "Electronicos", Imagen = File.ReadAllBytes("wwwroot/img/productos/mac.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 2, Nombre = "Playera Polo", Descripcion = "Playera polo elegante", Precio = Math.Round(20.99m, 2), Stock = 200, Categoria = "Ropa", Imagen = File.ReadAllBytes("wwwroot/img/productos/polo.jpg"), TipoImagen = "image/jpg" },
                new Producto { Id = 3, Nombre = "Converse", Descripcion = "Tenis comodos", Precio = Math.Round(30.99m, 2), Stock = 300, Categoria = "Calzado", Imagen = File.ReadAllBytes("wwwroot/img/productos/converse.png"), TipoImagen = "image/png" },
                new Producto { Id = 4, Nombre = "Refigerador", Descripcion = "Refigerador moderno", Precio = Math.Round(40.99m, 2), Stock = 400, Categoria = "Linea Blanca", Imagen = File.ReadAllBytes("wwwroot/img/productos/refigerador.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 5, Nombre = "Iphone", Descripcion = "Iphone moderno", Precio = Math.Round(50.99m, 2), Stock = 500, Categoria = "Electronicos", Imagen = File.ReadAllBytes("wwwroot/img/productos/iphone.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 6, Nombre = "Pantalon Levis", Descripcion = "Pantalon comodo", Precio = Math.Round(60.99m, 2), Stock = 600, Categoria = "Ropa", Imagen = File.ReadAllBytes("wwwroot/img/productos/pantalon.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 7, Nombre = "Nike", Descripcion = "Tenis comodos", Precio = Math.Round(70.99m, 2), Stock = 700, Categoria = "Calzado", Imagen = File.ReadAllBytes("wwwroot/img/productos/nike.jpg"), TipoImagen = "image/jpg" },
                new Producto { Id = 8, Nombre = "Lavadora", Descripcion = "Lavadora moderna", Precio = Math.Round(80.99m, 2), Stock = 800, Categoria = "Linea Blanca", Imagen = File.ReadAllBytes("wwwroot/img/productos/lavadora.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 9, Nombre = "Audifonos", Descripcion = "Audifonos modernos", Precio = Math.Round(90.99m, 2), Stock = 900, Categoria = "Electronicos", Imagen = File.ReadAllBytes("wwwroot/img/productos/audifonos.webp"), TipoImagen = "image/webp" },
                new Producto { Id = 10, Nombre = "Playera Adidas", Descripcion = "Playera comoda", Precio = Math.Round(100.99m, 2), Stock = 1000, Categoria = "Ropa", Imagen = File.ReadAllBytes("wwwroot/img/productos/playeraadidas.jpg"), TipoImagen = "image/jpg" }
            );
        }

        private static byte[] LeerImagen(string ruta)
        {
            return File.Exists(ruta) ? File.ReadAllBytes(ruta) : new byte[0];
        }
    }
}
