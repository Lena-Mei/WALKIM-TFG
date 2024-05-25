using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;

using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoServController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly TipoServBC tipoServBC = new TipoServBC();

        [HttpPost]
        [Route("insertarTipoServ")]
        public ActionResult<BaseResponseModel> InsertarTipoServ(TipoServicioRequest servicio)
        {
            BaseResponseModel result = tipoServBC.InsertarTipoServ(servicio);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actTipoServ")]
        public ActionResult<BaseResponseModel> ActTipoServ(TipoServicioRequest servicio)
        {
            BaseResponseModel result = tipoServBC.ActTipoServ(servicio);
            return _httpHandleResponse.HandleResponse(result);
        }


        [HttpDelete]
        [Route("eliminarTipoServ")]
        public ActionResult<BaseResponseModel> EliminarTipoServ(int idTipoServ)
        {
            BaseResponseModel result = tipoServBC.EliminarTipoServ(idTipoServ);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllTipoServ")]
        public ActionResult<BaseResponseModel> GetAllTipoServ( )
        {
            BaseResponseModel result = tipoServBC.GetAllTipoServ();
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getTipoServ")]
        public ActionResult<BaseResponseModel> GetTipoServ(int idServ)
        {
            BaseResponseModel result = tipoServBC.GetTipoServ(idServ);
            return _httpHandleResponse.HandleResponse(result);
        }

    }

}
