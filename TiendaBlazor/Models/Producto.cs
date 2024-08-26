﻿namespace BlazorWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }


        public byte[] Imagen { get; set; }
        public string TipoImagen { get; set; } 
    }
}
