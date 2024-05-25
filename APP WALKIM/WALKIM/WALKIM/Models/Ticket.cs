using System.ComponentModel.DataAnnotations;

namespace WALKIM.Models
{
    public class Ticket
    {
        public int idTicket { get; set; }
        [Required(ErrorMessage = "Se requiere una descripción del problema.")]
        public string? descripcion { get; set; }
        [Required(ErrorMessage = "Se requiere un título del problema.")]
        public string? tituloProblema { get; set; }
        public DateTime fecha { get; set; }
        public int? idUsuario { get; set; }
        public int? idServidor { get; set; }
        public string? tipoCuenta { get; set; }
    }
}
