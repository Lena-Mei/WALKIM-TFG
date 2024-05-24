using APIWALKIM.Helpers.Interface;
using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Helpers
{
    public class HandleHelper : Controller, IhttpHandleResponse
    {
        public ActionResult HandleResponse(BaseResponseModel response)
        {
            if (response.httpStatus == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);

            }
            if (response.httpStatus == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (response.httpStatus == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response.message);
            }
            if (response.httpStatus == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response.message);
            }
            if (response.httpStatus == System.Net.HttpStatusCode.Conflict)
            {
                return Conflict(response.message);
            }
            else
            {
                return Forbid();
            }
        }
    }
}
