namespace TiendaBlazor.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public DateTime EstadoFecha { get; set; }
    }
}
