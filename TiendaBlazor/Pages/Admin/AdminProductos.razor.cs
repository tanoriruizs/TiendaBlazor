using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TiendaBlazor.Models;
using TiendaBlazor.Services;


namespace TiendaBlazor.Pages.Admin
{
    public partial class AdminProductos
    {
        private List<Producto> productos = new();
        private List<Producto> productosFiltrados = new();
        private string searchTerm = string.Empty;
        private int registrosVisibles = 10; 
        private string? UserName;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                if (user.IsInRole("Admin"))
                {
                    UserName = user.Identity.Name;

                    productos = (await ProductoService.GetAllProductosAsync()).ToList();
                    productosFiltrados = productos;
                }
                else
                {
                    NavigationManager.NavigateTo("/NoAutorizado");
                }
            }
            else
            {
                NavigationManager.NavigateTo("/Login");
            }
        }

        private void EditarProducto(int id)
        {
            NavigationManager.NavigateTo($"/admin-editar/{id}");
        }

        private async Task EliminarProducto(int id)
        {
            bool confirmado = await JsRuntime.InvokeAsync<bool>("confirm", "¿Estás seguro de que deseas eliminar este producto?");
            if (confirmado)
            {
                await ProductoService.DeleteProductoAsync(id);
                productos = (await ProductoService.GetAllProductosAsync()).ToList();
                FiltrarProductos();
            }
        }

        private void FiltrarProductos()
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                productosFiltrados = productos;
            }
            else
            {
                productosFiltrados = productos
                    .Where(p => p.Nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        private void Buscar(ChangeEventArgs e)
        {
            searchTerm = e.Value?.ToString() ?? string.Empty;
            FiltrarProductos();
        }
    }
}
