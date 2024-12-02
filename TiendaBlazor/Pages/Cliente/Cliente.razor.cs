using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using TiendaBlazor.Data;
using TiendaBlazor.Models;

namespace TiendaBlazor.Pages.Cliente
{
    public partial class Cliente
    {
        private IEnumerable<Producto> productos;
        private IEnumerable<Producto> productosFiltrados;
        private string clienteNombre;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            clienteNombre = user.Identity.IsAuthenticated ? user.Identity.Name : "Cliente";

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
        private void NavegarADetalleProducto(int productoId)
        {
            NavigationManager.NavigateTo($"/producto/{productoId}");
        }
    }
}

