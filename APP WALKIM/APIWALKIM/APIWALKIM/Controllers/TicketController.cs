using APIWALKIM.BC;
using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models;
using APIWALKIM.Models.Request;

using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController(IhttpHandleResponse httpHandleResponse)
    {
        private readonly IhttpHandleResponse _httpHandleResponse = httpHandleResponse;
        private readonly TicketBC ticketBC = new TicketBC();

        [HttpPost]
        [Route("insertTicket")]
        public ActionResult<BaseResponseModel> InsertTicket(TicketRequest ticket)
        {
            BaseResponseModel result = ticketBC.InsertTicket(ticket);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpDelete]
        [Route("eliminarTicket")]
        public ActionResult<BaseResponseModel> EliminarTicket(int ticket)
        {
            BaseResponseModel result = ticketBC.EliminarTicket(ticket);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getAllTicket")]
        public ActionResult<BaseResponseModel> GetAllTicket(int? idUsuario = null, int? idServidor = null, string? tipoCuenta = null)
        {
            BaseResponseModel result = ticketBC.GetAllTicket(idUsuario, idServidor, tipoCuenta);
            return _httpHandleResponse.HandleResponse(result);

        }

        [HttpGet]
        [Route("getTicket")]
        public ActionResult<BaseResponseModel> GetTicket(int idTicket)
        {
            BaseResponseModel result = ticketBC.GetTicket(idTicket);
            return _httpHandleResponse.HandleResponse(result);

        }
    }
}
