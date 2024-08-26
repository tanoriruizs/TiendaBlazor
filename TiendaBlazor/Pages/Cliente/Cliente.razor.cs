using TiendaBlazor.Models;

namespace TiendaBlazor.Pages.Cliente
{
    public partial class Cliente
    {
        private IEnumerable<Producto> productos;
        private IEnumerable<Producto> productosFiltrados;

        protected override async Task OnInitializedAsync()
        {
            productos = await ProductoService.GetAllProductosAsync();
            productosFiltrados = productos;
        }

        private void AgregarAlCarrito(Producto producto)
        {
        }

        private void FiltrarPorCategoria(string categoria)
        {
            productosFiltrados = productos
                .Where(p => !string.IsNullOrEmpty(p.Categoria) && p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void MostrarTodos()
        {
            productosFiltrados = productos;
        }

        private string GetImageSource(byte[] imageBytes, string imageType)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return string.Empty;
            }
            var base64String = Convert.ToBase64String(imageBytes);
            return $"data:{imageType};base64,{base64String}";
        }
    }
}
