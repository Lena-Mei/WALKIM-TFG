using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Response.TipoServResponse;

namespace APIWALKIM.BC
{
    public class TipoServBC
    {
        private readonly TipoServDAC tipoServDAC = new TipoServDAC();

        public BaseResponseModel InsertarTipoServ (TipoServicio tipoServ)
        {
            BaseResponseModel result = new BaseResponseModel ();
            int resultado = tipoServDAC.InsertarTipoServ(tipoServ);

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
                result.message = "Ya existe TipoServicio con el nombre '"+ tipoServ.nombre+"' registrado";
            }
            return result;



        }

        public BaseResponseModel ActTipoServ(TipoServicio tipoServ)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = tipoServDAC.ActTipoServ(tipoServ);

            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado ==-1)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El idTipoServicio no existe o es inválido";
            }
            else if(resultado == -2)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Ya existe un TipoServicio con el nombre registrado";
            }
            return result;



        }

        public BaseResponseModel EliminarTipoServ (int idTipoServ)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correct = tipoServDAC.EliminarTipoServ(idTipoServ);

            if (correct)
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

        public BaseResponseModel GetAllTipoServ()
        {
            bool correcto;
            ListaTipoServResponse result = new ListaTipoServResponse();
            result.listaTipoServicio = tipoServDAC.GetAllServicio(out correcto);
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

        public BaseResponseModel GetTipoServ(int idServ)
        {
            int resultado;
            TipoServResponse result = new TipoServResponse();
            result.TipoServicio = tipoServDAC.GetTipoServ(idServ,out resultado);
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
                result.message = "El idTipoServicio no existe o no es válido";
            }
            return result;
        }
    } 
}
