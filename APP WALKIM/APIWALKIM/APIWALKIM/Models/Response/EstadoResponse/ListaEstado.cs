using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.EstadoResponse
{
    public class ListaEstado : BaseResponseModel
    {
        public List<Estado> estadoLista {  get; set; }
    }
}
