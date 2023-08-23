namespace AlumasAPI.ModelsDTOs
{
    public class BranchDTO
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
