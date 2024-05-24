namespace WALKIM.Models
{
    public class Archivo
    {
        public int idArchivo { get; set; }
        public string? nombreArchivo { get; set; }
        public byte[]? archivo { get; set; }
        public string? extensionArchivo { get; set; }
        public DateTime fechaEntrada { get; set; }
        public int idServidor { get; set; }
    }
}
