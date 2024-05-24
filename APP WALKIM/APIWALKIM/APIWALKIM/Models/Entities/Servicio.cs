namespace APIWALKIM.Models.Entities
{
    public class Servicio
    {
        public int idServicio {  get; set; }
        public decimal precio { get; set; }
        public string nombre { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public decimal puntaje { get; set; }
        public int idTipoServicio { get; set; }
        public int idServidor { get; set; }

        public List<TipoAnimalServicio> aceptaTipo {  get; set; }
        public List<Contrata>? contratos { get; set; }

    }
}
