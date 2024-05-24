namespace WALKIM.Models
{
    public class Mascota
    {
        public int idMascota { get; set; }
        public int edad { get; set; }
        public string nombre { get; set; } = null!;

        public string? imgMascota { get; set; }
        public int idTipoAnimal { get; set; }
        public int idUsuario { get; set; }
        public string? descripcion { get; set; }
    }
}
