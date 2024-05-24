using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models.Response.ArchivoResponse;

namespace APIWALKIM.BC
{
    public class ArchvioBC
    {
        private readonly ArchivoDAC archivoDAC = new ArchivoDAC();

        public BaseResponseModel SubirArchivo (ArchivoRequest archivo)
        {
            BaseResponseModel result = new BaseResponseModel();
            int correcto = archivoDAC.SubirArchivo(archivo.archivo);
            if (correcto==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(correcto==-1)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El idServidor es inválido o no existe";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Error en la API";
            }
            return result;
        }

        public BaseResponseModel EliminarArchivo(int idArchivo)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = archivoDAC.EliminarArchivo(idArchivo);
            if (resultado==1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;

            }
            else if(resultado== 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Error en la API";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idArchivo introducido no existe o e sinválido";
            }
            return result;
        }

        public BaseResponseModel GetArchivo(int idArchivo)
        {
            int resultado;
            ArchivoResponse result = new ArchivoResponse();
            result.archivo = archivoDAC.GetArchivo(idArchivo, out resultado);
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
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idArchivo no es válido o no existe";
            }
            return result;
        }
        public BaseResponseModel GetAllArchivo(int? idServidor = null)
        {
            int resultado;
            ListaArchivoResponse result = new ListaArchivoResponse();
            result.listaArvicho = archivoDAC.GetAllArchivo(out resultado, idServidor);
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
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El idServidor no existe o es inválido";
            }
            return result;
        }
    }
}
