using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.UsuarioResponse
{
    public class ListaUsuarioResponse : BaseResponseModel
    {
        public List<Usuario> usuarioLista {  get; set; }
    }
}
