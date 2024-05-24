using APIWALKIM.BC;
using APIWALKIM.DAC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotaController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly MascotaBC mascotaBC = new MascotaBC();

        [HttpPost]
        [Route("insertarMascota")]
        public ActionResult<BaseResponseModel> InsertarMascota(MascotaRequest mascota)
        {
            BaseResponseModel result = mascotaBC.InsertarMascota(mascota);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpPut]
        [Route("actMascota")]
        public ActionResult<BaseResponseModel> ActMascota(MascotaRequest mascota)
        {
            BaseResponseModel result = mascotaBC.ActMascota(mascota);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpDelete]
        [Route("eliminarMascota")]
        public ActionResult<BaseResponseModel> EliminarMascota(int idMascota)
        {
            BaseResponseModel result = mascotaBC.EliminarMascota(idMascota);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getAllMascota")]
        public ActionResult<BaseResponseModel> GetAllMascota(int? idUsuario = null, int? idAnimal = null)
        {
            BaseResponseModel result = mascotaBC.GetAllMascota(idUsuario, idAnimal);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getMascota")]
        public ActionResult<BaseResponseModel> GetMascota(int idMascota)
        {
            BaseResponseModel result = mascotaBC.GetMascota(idMascota);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("cambiarFoto")]
        public ActionResult<BaseResponseModel> CambiarFoto(int idMascota, string img)
        {
            BaseResponseModel result = mascotaBC.CambiarImagen(idMascota, img);
            return _httpHandleResponse.HandleResponse(result);

        }


    }
}