using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class ResenyaController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";

        private int IDUSER()
        {
            int idUsuario = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            return idUsuario;
        }

 


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalle(int idServicio)
        {
            var usuarios = ListadoUsuario();
            ViewBag.Usuarios = usuarios;
            var resenyas = Resenyas(idServicio);
            return View(resenyas);
        }

        [HttpGet]
        public IActionResult PublicarResenya(int idServicio)
        {
            ViewBag.Servicio = idServicio;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublicarResenya(Resenya resenya, int idServicio)
        {
            resenya.idServicio = idServicio;
            resenya.idUsuario = IDUSER();
            if (InsertarResenya(resenya))
            {
                return RedirectToAction("Detalle", "Servicio", new {idServicio=resenya.idServicio});
            }
            else
            {
                return View("Error");
            }
        }

        private List<Resenya> Resenyas(int idServicio)
        {
            List<Resenya> resenyas = new List<Resenya>();
            try
            {
                string url = generalUrl + "Resenya/getAllResenyaServicio?idServicio=" + idServicio;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var resenyaArray = json["listaResenya"].ToObject<JArray>();
                    resenyas = resenyaArray.ToObject<List<Resenya>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resenyas;
        }
        private List<Usuario> ListadoUsuario()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            try
            {
                string url = generalUrl + "Usuario/getAllUsuarios";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var usuarioArray = json["usuarioLista"].ToObject<JArray>();
                    listaUsuarios = usuarioArray.ToObject<List<Usuario>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaUsuarios;
        }


        private bool InsertarResenya(Resenya resenya)
        {
            bool correcto = false;
            var mascotaData = new { resenya = resenya };
            string url = generalUrl + "Resenya/insertarResenya";
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
