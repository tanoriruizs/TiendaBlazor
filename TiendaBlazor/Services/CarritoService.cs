using TiendaBlazor.Models;

namespace TiendaBlazor.Services
{
    public class CarritoService
    {
        private readonly List<Carrito> _carritoItems = new List<Carrito>();

        public Task<IEnumerable<Carrito>> GetCarritoItemsAsync(int usuarioId)
        {
            return Task.FromResult(_carritoItems.Where(c => c.UsuarioId == usuarioId).AsEnumerable());
        }

        public Task AgregarAlCarritoAsync(int usuarioId, Producto producto, int cantidad = 1)
        {
            var itemExistente = _carritoItems.FirstOrDefault(c => c.UsuarioId == usuarioId && c.ProductoId == producto.Id);

            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
            }
            else
            {
                _carritoItems.Add(new Carrito
                {
                    UsuarioId = usuarioId,
                    ProductoId = producto.Id,
                    Producto = producto,
                    Cantidad = cantidad
                });
            }

            return Task.CompletedTask;
        }

        public Task EliminarDelCarritoAsync(int usuarioId, int productoId)
        {
            var item = _carritoItems.FirstOrDefault(c => c.UsuarioId == usuarioId && c.ProductoId == productoId);
            if (item != null)
            {
                _carritoItems.Remove(item);
            }

            return Task.CompletedTask;
        }

        public Task VaciarCarritoAsync(int usuarioId)
        {
            _carritoItems.RemoveAll(c => c.UsuarioId == usuarioId);
            return Task.CompletedTask;
        }
    }
}
