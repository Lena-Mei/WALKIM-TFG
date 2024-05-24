using APIWALKIM.BC;
using APIWALKIM.DAC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using Microsoft.AspNetCore.Mvc;
namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly UsuarioBC usuarioBC = new UsuarioBC();

        [HttpGet]
        [Route("loginUsuario")]
        public ActionResult<BaseResponseModel> LoginUsuario(string correo, string contrasenya)
        {
            BaseResponseModel result = usuarioBC.LoginUsuario(correo, contrasenya);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPost]
        [Route("insertarUsuario")]
        public ActionResult<BaseResponseModel> InsertarUsuario(UsuarioRequest usuario )
        {
            BaseResponseModel result = usuarioBC.InsertarUsuario(usuario);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actUsuario")]
        public ActionResult<BaseResponseModel> ActUsuario(UsuarioRequest usuario)
        {
            BaseResponseModel result = usuarioBC.ActUsuario(usuario);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getUsuario")]
        public ActionResult<BaseResponseModel> GetUsuario(int idUsuario)
        {
            BaseResponseModel result = usuarioBC.GetUsuario(idUsuario);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllUsuarios")]
        public ActionResult<BaseResponseModel> GetAllUsuario()
        {
            BaseResponseModel result = usuarioBC.GetAllUsuario();
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actContrasenya")]
        public ActionResult<BaseResponseModel> ActContrasenya(int idUsuario, string contrasenya)
        {
            BaseResponseModel result = usuarioBC.ActContrasenya(idUsuario, contrasenya);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actFoto")]
        public ActionResult<BaseResponseModel> ActFoto(int idUsuario, string imagen)
        {
            BaseResponseModel result = usuarioBC.ActFoto(idUsuario, imagen);
            return _httpHandleResponse.HandleResponse(result);
        }


    }
}
