namespace AlumasAPI.ModelsDTOs
{
    public class DeliveryDTO
    {
        public int IdEntrega { get; set; }
        public string Direccion { get; set; } = null!;  
        public string Descripcion { get; set; } = null!;
        public int IdCliente { get; set; }
        public bool Activo { get; set; }
        public string NombreCliente { get; set; } = null!;
    }
}
