namespace AlumasAPI.ModelsDTOs
{
    public class ClientDTO
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string CorreoRespaldo { get; set; } = null!;
        public bool Activo { get; set; }
        public int IdSucursal { get; set; }

        public string NombreSucursal { get; set; } = null!;
    }
}
