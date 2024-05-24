using APIWALKIM.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIWALKIM.Helpers.Interface
{
    public interface IhttpHandleResponse
    {
        public ActionResult HandleResponse(BaseResponseModel response);

    }
}
