using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.TipoAnimalResponse
{
    public class ListaTipoAnimalResponse : BaseResponseModel
    {
        public List<TipoAnimal> listaAnimal {  get; set; }
    }
}
