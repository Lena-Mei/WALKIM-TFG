namespace APIWALKIM.Models.Entities
{
    public partial class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; } = null!;
        public string apellido1 { get; set; } = null!;
        public string? apellido2 { get; set; }
        public string correo { get; set; } = null!;
        public string? direccion {  get; set; }
        public string? ciudad {  get; set; }
        public string? provincia { get; set; }
        public string? codigoPostal { get; set; }
        public string? telefono { get; set; }
        public string contrasenya {  get; set; } = null!;

        public string? imgPerfil { get; set; }
        public string? descripcion { get; set; }

        public List<Mascota>? mascotas { get; set; }


    }
}
