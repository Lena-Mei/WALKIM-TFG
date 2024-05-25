using System.ComponentModel.DataAnnotations;

namespace WALKIM.Models
{
    public class Contrata
    {
        public int idContrato { get; set; }
        public int idServicio { get; set; }
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Es necesario introducir la duración del servicio al que vas a contratar.")]
        public string tiempo { get; set; } = null!;
        [Required(ErrorMessage = "Es necesario escoger la fecha del servicio al que quieres que se realice.")]
        public DateTime fecha { get; set; }
        public int idEstado { get; set; }

        public List<MascotaContrato> listaMascotas { get; set; }
    }
}
