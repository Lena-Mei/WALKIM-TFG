using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.ArchivoResponse
{
    public class ListaArchivoResponse : BaseResponseModel
    {
        public List<Archivo> listaArvicho { get; set; }
    }
}
