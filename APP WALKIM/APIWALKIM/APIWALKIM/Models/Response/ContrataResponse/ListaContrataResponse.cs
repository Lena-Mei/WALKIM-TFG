using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.ContrataResponse
{
    public class ListaContrataResponse : BaseResponseModel
    {
        public List<Contrata> listaContrata { get; set; }
    }
}
