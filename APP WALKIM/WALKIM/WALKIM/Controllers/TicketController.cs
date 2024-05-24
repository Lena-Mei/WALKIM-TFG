using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class TicketController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";
        private int IDUSER()
        {
            int idUsuario = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            return idUsuario;
        }

        private string ROLUSER()
        {
            string rol = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault().ToString();
            return rol;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CrearTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearTicket(Ticket ticket)
        {
            if (ROLUSER() == "usuario")
            {
                ticket.idUsuario = IDUSER();
                ticket.idServidor = null;
            }
            else
            {
                ticket.idServidor = IDUSER();
                ticket.idUsuario = null;
            }
           
            ticket.tipoCuenta= ROLUSER();
            if(InsertarTicket(ticket))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        private bool InsertarTicket(Ticket ticket)
        {
            bool correcto = false;
            var mascotaData = new { ticket = ticket };
            string url = generalUrl + "Ticket/insertTicket";
            string jsonDatos = JsonConvert.SerializeObject(mascotaData);
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {

                    streamWriter.Write(jsonDatos);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if (httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string errorMensaje = streamReader.ReadToEnd();
                        Console.WriteLine(errorMensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return correcto;
        }
    }
}
