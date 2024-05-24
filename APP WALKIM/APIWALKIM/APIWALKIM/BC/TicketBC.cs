using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models.Response.TicketResponse;

namespace APIWALKIM.BC
{
    public class TicketBC
    {
        private readonly TicketDAC ticketDAC = new TicketDAC();

        public BaseResponseModel InsertTicket (TicketRequest ticket)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correcto = ticketDAC.InsertTicket(ticket.ticket);
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

        public BaseResponseModel EliminarTicket(int ticket)
        {
            BaseResponseModel result = new BaseResponseModel();
            bool correcto = ticketDAC.EliminarTicket(ticket);
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

        public BaseResponseModel GetAllTicket(int? idUsuario = null, int? idServidor = null, string? tipoCuenta = null)
        {
            bool correcto;
            ListaTicketResponse result = new ListaTicketResponse();
            result.listaTicket = ticketDAC.GetAllTicket(out correcto, idUsuario, idServidor, tipoCuenta);
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

        public BaseResponseModel GetTicket(int idTicket)
        {
            bool correcto;
            TicketResponse result = new TicketResponse();
            result.ticket = ticketDAC.GetTicket(idTicket, out correcto);
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
