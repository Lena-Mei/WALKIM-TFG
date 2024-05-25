using Microsoft.AspNetCore.Mvc;

namespace AdminWALKIM.Controllers
{
    public class ServicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
