namespace APIWALKIM.Models.Entities
{
    public class Contrata
    {
        public int idContrato { get; set; }
        public int idServicio { get; set; }
        public int idUsuario { get; set; }
        public string tiempo { get; set; } = null!;
        public DateTime fecha { get; set; }
        public int idEstado { get; set; }

        public List<MascotaContrato> listaMascotas { get; set; }

    }
}
