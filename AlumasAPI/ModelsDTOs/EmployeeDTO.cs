namespace AlumasAPI.ModelsDTOs
{
    public class EmployeeDTO
    {
        public int IdEmpelado { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string NumeroTelefonico{ get; set; } = null!;
        public bool Activo { get; set; }
        public int IdSucursal{ get; set; }

        public string NombreSucursal { get; set; } = null!;
    }
}
