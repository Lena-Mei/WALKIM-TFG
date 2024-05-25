using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Request;

using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoAnimalController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly TipoAnimalBC tipoAnimalBC = new TipoAnimalBC();

        [HttpPost]
        [Route("insertarTipoAnimal")]
        public ActionResult<BaseResponseModel> InsertarTipoAnimal(TipoAnimalRequest animal)
        { 
            BaseResponseModel result = tipoAnimalBC.InsertarTipoAnimal(animal);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("actTipoAnimal")]
        public ActionResult<BaseResponseModel> ActTipoAnimal(TipoAnimalRequest animal)
        {
            BaseResponseModel result = tipoAnimalBC.ActTipoAnimal(animal);
            return _httpHandleResponse.HandleResponse(result);
        }


        [HttpDelete]
        [Route("eliminarTipoAnimal")]
        public ActionResult<BaseResponseModel> EliminarTipoAnimal(int idTipoServ)
        {
            BaseResponseModel result = tipoAnimalBC.EliminarTipoAnimal(idTipoServ);
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllTipoAnimal")]
        public ActionResult<BaseResponseModel> GetAllTipoAnimal()
        {
            BaseResponseModel result = tipoAnimalBC.GetAllTipoAnimal();
            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getTipoAnimal")]
        public ActionResult<BaseResponseModel> GetTipoAnimalv(int idServ)
        {
            BaseResponseModel result = tipoAnimalBC.GetTipoAnimal(idServ);
            return _httpHandleResponse.HandleResponse(result);
        }

    }
}
