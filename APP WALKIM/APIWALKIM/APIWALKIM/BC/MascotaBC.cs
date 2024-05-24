using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models.Response.MascotaResponse;

namespace APIWALKIM.BC
{
    public class MascotaBC
    {
        private readonly MascotaDAC mascotaDAC = new MascotaDAC();

        public BaseResponseModel InsertarMascota (MascotaRequest mascota)
        {
            BaseResponseModel result = new BaseResponseModel ();
            bool correcto = mascotaDAC.InsertarMascota(mascota.mascota);
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

        public BaseResponseModel ActMascota(MascotaRequest mascota)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correcto = mascotaDAC.ActMascota(mascota.mascota);
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

        public BaseResponseModel EliminarMascota(int idMascota)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correcto = mascotaDAC.EliminarMascota(idMascota);
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

        public BaseResponseModel GetAllMascota(int? idUsuario = null, int? idAnimal = null)
        {
            bool correcto;
            ListaMascotaResponse result = new ListaMascotaResponse();
            result.listaMascota = mascotaDAC.GetAllMascota(out correcto, idUsuario, idAnimal);
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

        public BaseResponseModel GetMascota(int idMascota)
        {
            bool correcto;
            MascotaResponse result = new MascotaResponse();
            result.mascota = mascotaDAC.GetMascota(idMascota,out correcto);
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

        public BaseResponseModel CambiarImagen (int idMascota, string img)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correcto = mascotaDAC.CambiarFoto(idMascota, img);
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
