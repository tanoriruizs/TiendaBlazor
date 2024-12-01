using TiendaBlazor.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace TiendaBlazor.Pages.Admin
{
    public partial class AdminAgregarProductos
    {
        private Producto nuevoProducto = new Producto();
        private string fileName = "Carga tu imagen";
        private string errorMessage;
        private string precioInput;
        private string stockInput;

        private async Task AgregarProducto()
        {
            if (!ValidarProducto())
            {
                errorMessage = "Por favor, complete todos los campos correctamente.";
                return;
            }

            try
            {
                await ProductoService.AddProductoAsync(nuevoProducto);
                NavigationManager.NavigateTo("/admin");
            }
            catch (Exception ex)
            {
                errorMessage = $"Error al agregar el producto: {ex.Message}";
            }
        }

        private bool ValidarProducto()
        {
            if (string.IsNullOrEmpty(nuevoProducto.Nombre) ||
                string.IsNullOrEmpty(nuevoProducto.Descripcion) ||
                nuevoProducto.Precio <= 0 ||
                nuevoProducto.Stock < 0 ||
                string.IsNullOrEmpty(nuevoProducto.Categoria))
            {
                return false;
            }

            if (nuevoProducto.Imagen == null || nuevoProducto.Imagen.Length == 0)
            {
                errorMessage = "Debe cargar una imagen para el producto.";
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
                    nuevoProducto.Imagen = memoryStream.ToArray();
                    nuevoProducto.TipoImagen = file.ContentType;
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

        private void ValidarPrecio(ChangeEventArgs e)
        {
            precioInput = e.Value.ToString();
            if (decimal.TryParse(precioInput, out decimal precio))
            {
                nuevoProducto.Precio = precio;
            }
            else
            {
                precioInput = string.Empty; 
            }
        }

        private void ValidarStock(ChangeEventArgs e)
        {
            stockInput = e.Value.ToString();
            if (int.TryParse(stockInput, out int stock))
            {
                nuevoProducto.Stock = stock;
            }
            else
            {
                stockInput = string.Empty;
            }
        }
    }
}
