using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using TiendaBlazor.Models;
using TiendaBlazor.Data;

namespace TiendaBlazor.Pages.Admin
{
    public partial class AdminEditarProductos
    {
        [Parameter]
        public int productoId { get; set; }

        private Producto producto;
        private string fileName = "Cargar nueva imagen (opcional)";
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                producto = await ProductoService.GetProductoByIdAsync(productoId);
                if (producto == null)
                {
                    NavigationManager.NavigateTo("/admin");
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error al cargar el producto: {ex.Message}";
            }
        }

        private async Task EditarProducto()
        {
            if (!ValidarProducto())
            {
                errorMessage = "Por favor, complete todos los campos correctamente.";
                return;
            }

            try
            {
                await ProductoService.UpdateProductoAsync(producto);
                NavigationManager.NavigateTo("/admin");
            }
            catch (Exception ex)
            {
                errorMessage = $"Error al guardar el producto: {ex.Message}";
            }
        }

        private bool ValidarProducto()
        {
            if (string.IsNullOrEmpty(producto.Nombre) ||
                string.IsNullOrEmpty(producto.Descripcion) ||
                producto.Precio <= 0 ||
                producto.Stock < 0 ||
                string.IsNullOrEmpty(producto.Categoria))
            {
                return false;
            }

            return true;
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (!ValidarArchivo(file))
            {
                return;
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(memoryStream);
                    producto.Imagen = memoryStream.ToArray();
                    producto.TipoImagen = file.ContentType;
                }

                fileName = file.Name;
                errorMessage = null;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error al cargar la imagen: {ex.Message}";
            }
        }

        private bool ValidarArchivo(IBrowserFile file)
        {
            var maxFileSize = 1024 * 1024 * 5;
            var allowedFileTypes = new[] { "image/jpeg", "image/png", "image/gif" };

            if (file.Size > maxFileSize)
            {
                errorMessage = "El archivo es demasiado grande. El tamaño máximo permitido es 5MB.";
                return false;
            }

            if (!allowedFileTypes.Contains(file.ContentType))
            {
                errorMessage = "Formato de archivo no permitido. Solo se permiten imágenes JPEG, PNG y GIF.";
                return false;
            }

            return true;
        }
    }
}
