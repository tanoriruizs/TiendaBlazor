using Microsoft.AspNetCore.Components;
using TiendaBlazor.Models;

namespace TiendaBlazor.Pages.Cliente
{
    public partial class ProductoDetalle
    {

        [Parameter]
        public int ProductoId { get; set; }

        private Producto producto;

        protected override async Task OnInitializedAsync()
        {
            producto = await ProductoService.GetProductoByIdAsync(ProductoId);
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

        private void AgregarAlCarrito()
        {
        }
    }
}
