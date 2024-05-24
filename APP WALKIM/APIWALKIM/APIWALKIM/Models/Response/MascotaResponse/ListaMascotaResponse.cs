using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.MascotaResponse
{
    public class ListaMascotaResponse : BaseResponseModel
    {
        public List<Mascota> listaMascota { get; set; }
    }
}
