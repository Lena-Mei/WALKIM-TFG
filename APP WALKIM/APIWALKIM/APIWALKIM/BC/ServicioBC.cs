using APIWALKIM.DAC;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using APIWALKIM.Models.Response.ServicioResponse;
using APIWALKIM.Models.Request;

namespace APIWALKIM.BC
{
    public class ServicioBC
    {
        private readonly ServicioDAC servicioDAC = new ServicioDAC();
        public BaseResponseModel InsertarServicio(ServicioRequest servicio)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = servicioDAC.InsertarServicio(servicio.servicio);

            if (resultado>0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado == -1)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idServidor o el idTipoServicio no son válidos";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Ya existe un Servicio con el nombre registrado";
            }
            return result;
        }

        public BaseResponseModel ActualizarServicio(ServicioRequest servicio)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = servicioDAC.ActualizarServicio(servicio.servicio);

            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado ==0)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            else if(resultado == -1)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El idServicio no existe o no es válido";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Algún dato introducido no es válido";
            }
            return result;



        }

        public BaseResponseModel EliminarServicio(int idServicio)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = servicioDAC.EliminarServicio(idServicio);

            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado==0)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idServicio introducido no existe o no es válido";
            }
            return result;
        }

        public BaseResponseModel GetServicio(int idServicio)
        {
            int resultado;
            ServicioResponse result = new ServicioResponse();
            result.servicio = servicioDAC.GetServicio(idServicio, out resultado);

            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado==0)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idServicio introducido no existe o no es válido";
            }
            return result;
        }

        public BaseResponseModel GetAllServicio(int? idTipoServicio = null, int? idServidor = null)
        {
            int resultado;
            ListaServicioResponse result = new ListaServicioResponse();
            result.listaServicio = servicioDAC.GetAllServicio( out resultado, idTipoServicio, idServidor);

            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado ==0)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Algún dato es inválido o no existe";
            }
            return result;
        }
    }
}
