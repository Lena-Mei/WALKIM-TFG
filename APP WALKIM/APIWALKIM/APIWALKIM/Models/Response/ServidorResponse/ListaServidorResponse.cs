using APIWALKIM.Models.Entities;
namespace APIWALKIM.Models.Response.ServidorResponse
{
    public class ListaServidorResponse :BaseResponseModel
    {
        public List<Servidor> listaServidor { get; set; }
    }
}
