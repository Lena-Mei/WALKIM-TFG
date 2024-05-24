using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.AdminResponse
{
    public class ListaAdminResponse : BaseResponseModel
    {
        public List<Administrador> listaAdmin {  get; set; }
    }
}
