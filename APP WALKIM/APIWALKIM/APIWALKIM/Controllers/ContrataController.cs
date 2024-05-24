using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;
using APIWALKIM.Models.Request;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContrataController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly ContrataBC contrataBC = new ContrataBC();

        [HttpPost]
        [Route("crearContrato")]
        public ActionResult<BaseResponseModel> CrearContrato(ContratarRequest contrata)
        {
            BaseResponseModel result = contrataBC.CrearContrato(contrata);
            return _httpHandleResponse.HandleResponse(result);

        }


        [HttpPut]
        [Route("actEstadoContrato")]
        public ActionResult<BaseResponseModel> ActEstadoContrato(int idEstado, int idContrato)
        {
            BaseResponseModel result = contrataBC.ActEstadoContrato(idEstado, idContrato);
            return _httpHandleResponse.HandleResponse(result);

        }
        [HttpGet]
        [Route("getContrato")]
        public ActionResult<BaseResponseModel> GetContrato(int idContrato)
        {
            BaseResponseModel result = contrataBC.GetContrato(idContrato);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getAllContrato")]
        public ActionResult<BaseResponseModel> GetAllContrato(int? idEstado = null, int? idServicio = null, int? idUsuario = null)
        {
            BaseResponseModel result = contrataBC.GetAllContrato(idEstado, idServicio, idUsuario);
            return _httpHandleResponse.HandleResponse(result);

        }
    }
}
