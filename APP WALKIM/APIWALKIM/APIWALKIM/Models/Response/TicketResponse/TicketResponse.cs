using APIWALKIM.Models.Entities;

namespace APIWALKIM.Models.Response.TicketResponse
{
    public class TicketResponse : BaseResponseModel
    {
        public Ticket ticket { get; set; }
    }
}
