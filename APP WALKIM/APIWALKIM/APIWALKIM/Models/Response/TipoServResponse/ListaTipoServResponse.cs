using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.TipoServResponse
{
    public class ListaTipoServResponse : BaseResponseModel
    {
        public List<TipoServicio> listaTipoServicio {  get; set; }
    }
}
