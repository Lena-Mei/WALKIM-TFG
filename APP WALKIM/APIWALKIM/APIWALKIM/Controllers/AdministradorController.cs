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
    public class AdministradorController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly AdministradorBC adminBC = new AdministradorBC();

        [HttpGet]
        [Route("loginUsuario")]
        public ActionResult<BaseResponseModel> LoginUsuario(string correo, string contrasenya)
        {
            BaseResponseModel result = adminBC.LoginUsuario(correo, contrasenya);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPost]
        [Route("insertarAdmin")]
        public ActionResult<BaseResponseModel> InsertarAdmin(AdminRequest admin)
        {
            BaseResponseModel result = adminBC.InsertarAdmin(admin);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpDelete]
        [Route("eliminarAdmin")]
        public ActionResult<BaseResponseModel> EliminarAdmin(int idAdmin)
        {
            BaseResponseModel result = adminBC.EliminarAdmin(idAdmin);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actAdmin")]
        public ActionResult<BaseResponseModel> ActUsuario(AdminRequest admin)
        {
            BaseResponseModel result = adminBC.ActAdmin(admin);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAdmin")]
        public ActionResult<BaseResponseModel> GetUsuario(int idAdmin)
        {
            BaseResponseModel result = adminBC.GetAdmin(idAdmin);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllAdmin")]
        public ActionResult<BaseResponseModel> GetAllAdmin()
        {
            BaseResponseModel result = adminBC.GetAllAdmin();
            return _httpHandleResponse.HandleResponse(result);
        }
    }
}
