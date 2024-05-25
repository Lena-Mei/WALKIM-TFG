using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models.Response.TipoAnimalResponse;
using Microsoft.Data.SqlClient;

namespace APIWALKIM.BC
{
    public class TipoAnimalBC
    {
         private readonly TipoAnimalDAC tipoAnimalDAC = new TipoAnimalDAC();

        public BaseResponseModel InsertarTipoAnimal(TipoAnimalRequest tipoServ)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = tipoAnimalDAC.InsertarTipoAnimal(tipoServ.tipoAnimal);

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
                result.message = "El nombre del tipoAnimal ya esta registrado";
            }
            return result;



        }

        public BaseResponseModel ActTipoAnimal(TipoAnimalRequest tipoServ)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = tipoAnimalDAC.ActTipoAnimal(tipoServ.tipoAnimal);

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
                result.message = "El idTipoAnimal no es válido o no existe";
            }
            return result;



        }

        public BaseResponseModel EliminarTipoAnimal(int idTipoServ)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = tipoAnimalDAC.EliminarTipoAnimal(idTipoServ);

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
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El idTipoAnimal introducido no es válido o no existe";
            }
            return result;


        }

        public BaseResponseModel GetAllTipoAnimal()
        {
            bool correcto;
            ListaTipoAnimalResponse result = new ListaTipoAnimalResponse();
            result.listaAnimal = tipoAnimalDAC.GetAllAnimal(out correcto);
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

        public BaseResponseModel GetTipoAnimal(int idTipoAnimal)
        {
            int resultado;
            TipoAnimalResponse result = new TipoAnimalResponse();
            result.tipoAnimal = tipoAnimalDAC.GetTipoAnimal(idTipoAnimal, out resultado);
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
                result.message = "El idTipoAnimal introducido no existe o no es válido";
            }
            return result;
        }
      
    }
}
