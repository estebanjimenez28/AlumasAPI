namespace AlumasAPI.ModelsDTOs
{
    public class ProductDTO
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public int Precio { get; set; }
        public bool Activo { get; set; }
        public int IdSucursal { get; set; }
        public int IdCliente { get; set; }
        public int IdProductoCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;
    }
}
