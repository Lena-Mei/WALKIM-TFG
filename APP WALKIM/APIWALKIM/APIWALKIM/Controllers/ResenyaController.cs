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
    public class ResenyaController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly ResenyaBC resenyaBC = new ResenyaBC();

        [HttpPost]
        [Route("insertarResenya")]
        public ActionResult<BaseResponseModel> InsertarResenya(ResenyaRequest resenya)
        {
            BaseResponseModel result = resenyaBC.InsertarResenya(resenya);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getAllResenyaServicio")]
        public ActionResult<BaseResponseModel> GetAllResenyaServicio(int idServicio)
        {
            BaseResponseModel result = resenyaBC.GetAllResenyaServicio(idServicio);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getResenya")]
        public ActionResult<BaseResponseModel> GetResenya(int idResenya)
        {
            BaseResponseModel result = resenyaBC.GetResenya(idResenya);
            return _httpHandleResponse.HandleResponse(result);

        }
    }
}
