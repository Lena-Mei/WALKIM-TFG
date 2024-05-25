using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;

using Microsoft.AspNetCore.Mvc;


namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController (IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly EstadoBC  estadoBC = new EstadoBC();

        [HttpPost]
        [Route("insertarEstado")]
        public ActionResult<BaseResponseModel> InsertarEstado (EstadoRequest estado)
        {
            BaseResponseModel result = estadoBC.InsertarEstado(estado);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actEstado")]
        public ActionResult<BaseResponseModel> ActEstado(EstadoRequest estado)
        {
            BaseResponseModel result = estadoBC.ActEstado(estado);
            return _httpHandleResponse.HandleResponse(result);
        }


        [HttpDelete]
        [Route("eliminarEstado")]
        public ActionResult<BaseResponseModel> EliminarEstado(int idEstado)
        {
            BaseResponseModel result = estadoBC.EliminarEstado(idEstado);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getEstado/{idEstado}")]
        public ActionResult<BaseResponseModel> GetEstado(int idEstado)
        {
            BaseResponseModel result = estadoBC.GetEstado(idEstado);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllEstado")]
        public ActionResult<BaseResponseModel> GetAllEstado()
        {
            BaseResponseModel result = estadoBC.GetAllEstado();
            return _httpHandleResponse.HandleResponse(result);
        }
    }
}
