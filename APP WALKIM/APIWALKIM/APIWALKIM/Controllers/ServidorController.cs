using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using APIWALKIM.Models.Request;

using Microsoft.AspNetCore.Mvc;
using APIWALKIM.DAC;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ServidorController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly ServidorBC servidorBC = new ServidorBC();

        [HttpPost]
        [Route("insertarServidor")]
        public ActionResult<BaseResponseModel> InsertarServidor(ServidorRequest servidor)
        {
            BaseResponseModel result = servidorBC.InsertarServidor(servidor);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actServidor")]
        public ActionResult<BaseResponseModel> ActServidor(ServidorRequest servidor)
        {
            BaseResponseModel result = servidorBC.ActServidor(servidor);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getServidor")]
        public ActionResult<BaseResponseModel> GetServidor(int idServidor)
        {
            BaseResponseModel result = servidorBC.GetServidor(idServidor);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllServidor")]
        public ActionResult<BaseResponseModel> GetAllServidor(int? idEstado = null)
        {
            BaseResponseModel result = servidorBC.GetAllServidor(idEstado);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("actEstadoServidor")]
        public ActionResult<BaseResponseModel> actEstadoServidor(int idEstado, int idServidor)
        {
            BaseResponseModel result = servidorBC.ActEstadoServidor(idEstado, idServidor);
            return _httpHandleResponse.HandleResponse(result);
        }

    }
}
