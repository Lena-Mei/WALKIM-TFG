namespace APIWALKIM.Models.Entities
{
    public class Resenya
    {
        public int idResenya {  get; set; }
        public DateTime fecha { get; set; }
        public string? comentario { get; set; }
        public int puntaje { get; set; }
        public int idUsuario { get; set; }
        public int idServicio { get; set; }
    }
}
