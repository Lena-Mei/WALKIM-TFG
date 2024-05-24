using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Response.EstadoResponse;


namespace APIWALKIM.BC
{
    public class EstadoBC
    {
        private readonly EstadoDAC estadoDAC = new EstadoDAC();

        public BaseResponseModel InsertarEstado (Estado estado)
        {
            BaseResponseModel result = new BaseResponseModel ();
            int resultado = estadoDAC.InsertarEstado (estado);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Falta algún dato o ya está registrado.";
            }
            return result;
        }

        public BaseResponseModel ActEstado(Estado estado)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = estadoDAC.ActEstado(estado);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if(resultado == -1)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Ya existe un Estado "+ estado.nombre;
            }
            else if(resultado == -2)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Algún dato introducido es nulo o el ID introducido no es correcto.";
            }
            return result;
        }
        public BaseResponseModel EliminarEstado(int idEstado)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = estadoDAC.EliminarEstado(idEstado);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else 
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El Id introducido no existe en la bbdd";
            }

            return result;
        }

        public BaseResponseModel GetEstado (int idEstado)
        {
            EstadoResponse result = new EstadoResponse();
            if(idEstado > 0)
            {
               
                int resultado;
                result.estado = estadoDAC.GetEstado(idEstado, out resultado);
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
                result.message = "ID no valido";
            }
            
            return result;
        }

        public BaseResponseModel GetAllEstado()
        {
            ListaEstado result = new ListaEstado();

            result.estadoLista = estadoDAC.GetAllEstados();

            if(result.estadoLista.Count()> 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "No se ha registrado ningún estado.";
            }

            return result;
        }
    }
}
