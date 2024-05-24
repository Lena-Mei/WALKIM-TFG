using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.ResenyaResponse
{
    public class ListaResenyaResponse : BaseResponseModel
    {
        public List<Resenya> listaResenya { get; set; }
    }
}
