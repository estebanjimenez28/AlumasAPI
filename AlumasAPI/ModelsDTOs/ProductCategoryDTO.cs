namespace AlumasAPI.ModelsDTOs
{
    public class ProductCategoryDTO
    {
        public int IdCategoriaProducto { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
