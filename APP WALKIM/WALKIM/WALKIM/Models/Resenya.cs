using System.ComponentModel.DataAnnotations;

namespace WALKIM.Models
{
    public class Resenya
    {
        public int idResenya { get; set; }
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "Es necesario introducir un comentario.")]

        public string? comentario { get; set; }
        public int puntaje { get; set; }
        public int idUsuario { get; set; }
        public int idServicio { get; set; }
    }
}
