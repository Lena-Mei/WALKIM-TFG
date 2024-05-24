using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.TicketResponse
{
    public class ListaTicketResponse : BaseResponseModel
    {
        public List<Ticket> listaTicket {  get; set; }
    }
}
