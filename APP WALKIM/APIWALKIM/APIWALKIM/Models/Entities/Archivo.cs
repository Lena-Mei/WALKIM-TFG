namespace APIWALKIM.Models.Entities
{
    public partial class Archivo
    {
        public int idArchivo {  get; set; }
        public string nombreArchivo { get; set; } = null!;
        public byte[] archivo { get; set; } = null!;
        public string extensionArchivo { get; set; } = null!;
        public DateTime fechaEntrada { get; set; }
        public int idServidor { get; set; }
    }
}
