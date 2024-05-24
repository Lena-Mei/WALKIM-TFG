using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Response.ServidorResponse;
using APIWALKIM.Models.Request;

namespace APIWALKIM.BC
{
    public class ServidorBC
    {
        private readonly ServidorDAC servidorDAC = new ServidorDAC();

        
        public BaseResponseModel InsertarServidor (ServidorRequest servidor)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = servidorDAC.InsertarServidor(servidor.servidor);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if (resultado == -1)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El correo introducido ya está registrado";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Algún dato está vacío.";
            }
            return result;
        }

        public BaseResponseModel ActServidor(ServidorRequest servidor)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = servidorDAC.ActServidor(servidor.servidor);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if (resultado == -1)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El id introducido no es correcto";
            }
            return result;
        }

        public BaseResponseModel GetServidor(int idServidor)
        {
            ServidorResponse result = new ServidorResponse();
            if (idServidor > 0)
            {
                int resultado;
                result.servidor= servidorDAC.GetServidor(idServidor, out resultado);
                if (resultado == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;

                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NotFound;
                    result.message = "El ID introducido no existe";
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El ID introducido no es váñido";
            }
            return result;
        }

        public BaseResponseModel GetAllServidor(int? idEstado = null)
        {
            ListaServidorResponse servidores = new ListaServidorResponse();
            int resultado;
            servidores.listaServidor = servidorDAC.GetAllServidor(out resultado ,idEstado);
            if(resultado == 1)
            {
                if (servidores.listaServidor.Count() > 0)
                {
                    servidores.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    servidores.httpStatus = System.Net.HttpStatusCode.NotFound;
                    servidores.message = "No se ha registrado ningún usuario.";
                }
            }
            else
            {
                servidores.httpStatus = System.Net.HttpStatusCode.BadRequest;
                servidores.message = "El estado introducido no existe o no es válido.";
            }
           
            return servidores;
        }

        public BaseResponseModel ActEstadoServidor(int idEstado, int idServidor)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = servidorDAC.ActEstadoServidor(idEstado, idServidor);
            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if(resultado==-1)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idServidor o el idEstado no existen";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error";
            }
            return result;
        }
    }
}
