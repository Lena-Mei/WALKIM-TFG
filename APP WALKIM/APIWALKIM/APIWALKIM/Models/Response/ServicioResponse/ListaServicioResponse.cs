using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.ServicioResponse
{
    public class ListaServicioResponse : BaseResponseModel
    {
        public List<Servicio> listaServicio {  get; set; }
    }
}
