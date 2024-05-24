using APIWALKIM.BC;
using APIWALKIM.DAC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;
using APIWALKIM.Models.Request;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly ServicioBC servicioBC = new ServicioBC();

        [HttpPost]
        [Route("insertarServicio")]
        public ActionResult<BaseResponseModel> InsertarServicio(ServicioRequest servicio)
        {
            BaseResponseModel result = servicioBC.InsertarServicio(servicio);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPost]
        [Route("actualizarServicio")]
        public ActionResult<BaseResponseModel> ActualizarServicio(ServicioRequest servicio)
        {
            BaseResponseModel result = servicioBC.ActualizarServicio(servicio);
            return _httpHandleResponse.HandleResponse(result);
        }


        [HttpDelete]
        [Route("eliminarServicio")]
        public ActionResult<BaseResponseModel> EliminarServicio(int idServicio)
        {
            BaseResponseModel result = servicioBC.EliminarServicio(idServicio);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getServicio")]
        public ActionResult<BaseResponseModel> GetServicio(int idServicio)
        {
            BaseResponseModel result = servicioBC.GetServicio(idServicio);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllServicio")]
        public ActionResult<BaseResponseModel> GetAllServicio(int? idTipoServicio = null, int? idServidor = null)
        {
            BaseResponseModel result = servicioBC.GetAllServicio(idTipoServicio, idServidor);
            return _httpHandleResponse.HandleResponse(result);
        }
    }
}
