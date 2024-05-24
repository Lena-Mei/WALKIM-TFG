using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using APIWALKIM.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivoController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly ArchvioBC archivoBC = new ArchvioBC();

        [HttpPost]
        [Route("subirArchivo")]
        public ActionResult<BaseResponseModel> SubirArchivo(ArchivoRequest archivo)
        {
            BaseResponseModel result = archivoBC.SubirArchivo(archivo);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpDelete]
        [Route("eliminarArchivo")]
        public ActionResult<BaseResponseModel> EliminarArchivo(int idArchivo)
        {
            BaseResponseModel result = archivoBC.EliminarArchivo(idArchivo);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getArchivo")]
        public ActionResult<BaseResponseModel> GetArchivo(int idArchivo)
        {
            BaseResponseModel result = archivoBC.GetArchivo(idArchivo);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllArchivo")]
        public ActionResult<BaseResponseModel> GetAllArchivo(int? idServidor = null)
        {
            BaseResponseModel result = archivoBC.GetAllArchivo(idServidor);
            return _httpHandleResponse.HandleResponse(result);
        }
    }
}
