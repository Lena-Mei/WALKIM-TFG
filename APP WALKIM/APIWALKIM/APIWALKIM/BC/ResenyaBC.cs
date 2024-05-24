using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;

using APIWALKIM.Models.Response.ResenyaResponse;

namespace APIWALKIM.BC
{
    public class ResenyaBC
    {
        private readonly ResenyaDAC resenyaDAC = new ResenyaDAC();

        public BaseResponseModel InsertarResenya (ResenyaRequest resenya)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correcto = resenyaDAC.InsertarResenya(resenya.resenya);
            if (correcto)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            return result;
        }

        public BaseResponseModel GetAllResenyaServicio(int idServicio)
        {
            bool correcto;
            ListaResenyaResponse result = new ListaResenyaResponse();
            result.listaResenya = resenyaDAC.GetAllResenyaServicio(idServicio, out correcto);
            if (correcto)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            return result;
        }

        public BaseResponseModel GetResenya(int idResenya)
        {
            bool correcto;
            ResenyaResponse result = new ResenyaResponse();
            result.resenya = resenyaDAC.GetResenya(idResenya, out correcto);
            if (correcto)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            return result;
        }
    }
}
