namespace WALKIM.Models
{
    public class Ticket
    {
        public int idTicket { get; set; }
        public string? descripcion { get; set; }
        public string? tituloProblema { get; set; }
        public DateTime fecha { get; set; }
        public int? idUsuario { get; set; }
        public int? idServidor { get; set; }
        public string? tipoCuenta { get; set; }
    }
}
