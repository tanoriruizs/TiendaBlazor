using TiendaBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace TiendaBlazor.Pages.Admin
{
    public partial class AdminProductos
    {
        private List<Producto> productos;
        private List<Producto> productosFiltrados;
        private string searchTerm = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            productos = (await ProductoService.GetAllProductosAsync()).ToList();
            productosFiltrados = productos;
        }

        private void EditarProducto(int id)
        {
            NavigationManager.NavigateTo($"/admin-editar/{id}");
        }

        private async Task EliminarProducto(int id)
        {
            bool confirmado = await JsRuntime.InvokeAsync<bool>("confirm", $"¿Estás seguro de que deseas eliminar el producto con Id {id}?");
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
                    .Where(p => p.Categoria.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        private void Buscar(ChangeEventArgs e)
        {
            searchTerm = e.Value?.ToString() ?? string.Empty;
            FiltrarProductos();
        }

        protected override void OnParametersSet()
        {
            FiltrarProductos();
        }
    }
}
