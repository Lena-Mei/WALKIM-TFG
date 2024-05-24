namespace APIWALKIM.Models.Entities
{
    public  class Administrador
    {
        public int idAdministrador { get; set; }
        public string nombre { get; set; } = null!;
        public string correo { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public string apellido1 { get; set; } = null!;
        public string? apellido2 { get; set; }
        public string contrasenya { get; set; } = null!;
    }
}
