using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Response.ContrataResponse;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using APIWALKIM.Models.Request;


namespace APIWALKIM.BC
{
    public class ContrataBC
    {
        private readonly ContrataDAC contrataDAC = new ContrataDAC();

        public BaseResponseModel CrearContrato(ContratarRequest contrata)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = contrataDAC.CrearContrato(contrata.contrato);
            if (resultado>0)
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
                result.message = "Algún dato introducido no es válido o no existe.";
            }
            return result;
        }

        public BaseResponseModel ActEstadoContrato(int idEstado, int idContrato)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = contrataDAC.ActEstadoContrato(idEstado, idContrato);
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
                result.message = "Algún dato introducido no es válido o no existe";
            }
            return result;
        }

        public BaseResponseModel GetContrato(int idContrato)
        {
            ContrataResponse result = new ContrataResponse();
            int resultado;
            result.contrata= contrataDAC.GetContrato(idContrato, out resultado);
            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if (resultado==0)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            else {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El id Introducido NO es válido o NO existe";
            }
            return result;
        }

        public BaseResponseModel GetAllContrato(int? idEstado = null, int? idServicio = null, int? idUsuario = null)
        {
            ListaContrataResponse result = new ListaContrataResponse();
            int resultado;
            result.listaContrata = contrataDAC.GetAllContrato(out resultado, idEstado, idServicio, idUsuario);
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
                result.message = "Alún id introducido no existe ";
            }
            return result;
        }
    }

}
