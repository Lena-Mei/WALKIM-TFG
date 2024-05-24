using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class ServidorController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServidorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public IActionResult Index()
        {
            return View();
        }

        private int IDSERV()
        {
            int idServidor = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            return idServidor;
        }

     

        private void DATOS(int id)
        {
            var tipoAnimal = ListadoTipos();
            var servicios = GetServicios(id, null);
            ViewBag.Servicios = servicios;
            ViewBag.TipoAnimal = tipoAnimal;
        }

        public IActionResult Detalle(int id)
        {
            DATOS(id);
            var servidor = GetServidor(id);
            return View(servidor);
        }

        public IActionResult MisDatos()
        {
            if (User.Identity!.IsAuthenticated && User.IsInRole("servidor"))
            {
                
                var servidor = GetServidor(IDSERV());
                DATOS(IDSERV());
                return View(servidor);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ActImagen(IFormFile img)
        {
            int id = IDSERV();


            string strRutaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "IMG");
            string strExtension = Path.GetExtension(img.FileName);
            string strNombreFichero = id + strExtension;
            string strRutaFcihero = Path.Combine(strRutaImagenes, strNombreFichero);
            using (var fileStream = new FileStream(strRutaFcihero, FileMode.Create))
            {
                img.CopyTo(fileStream);
            }

            Imagen(strNombreFichero);
            return RedirectToAction("MisDatos");
        }



        [HttpGet]
        public ActionResult ActServidor()
        {
            var servidor = GetServidor(IDSERV());
            return View(servidor);
        }

        [HttpPost]
        public ActionResult ActServidor(Servidor servidor)
        {
            if (ActualizarServidor(servidor))
            {
                return RedirectToAction(nameof(MisDatos));
            }
            else
            {
                return View("Error");
            }
        }

        private bool ActualizarServidor(Servidor servidor)
        {
            bool correcto = false;
            var usuarioData = new { servidor = servidor };
            string url = generalUrl + "Servidor/actServidor";
            string jsonDatos = JsonConvert.SerializeObject(usuarioData);
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
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






















        private Servidor GetServidor(int id)
        {
            Servidor servidor = new Servidor();
            string url = generalUrl + "Servidor/getServidor?idServidor=" + id;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    servidor = json["servidor"].ToObject<Servidor>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                servidor = null;
            }
            return servidor;
        }

        private List<Servicio> GetServicios(int? idServidor, int? tipoServicio)
        {
            List<Servicio> listadoServicios = new List<Servicio>();
            try
            {
                string url = generalUrl + "Servicio/getAllServicio?idTipoServicio=" + tipoServicio + "&idServidor=" + idServidor;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var servicioArray = json["listaServicio"].ToObject<JArray>();
                    listadoServicios = servicioArray.ToObject<List<Servicio>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listadoServicios;
        }
        private List<TipoServicio> ListadoTipos()
        {
            List<TipoServicio> tipos = new List<TipoServicio>();
            try
            {
                string url = generalUrl + "TipoServ/getAllTipoServ";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var servidorArray = json["listaTipoServicio"].ToObject<JArray>();
                    tipos = servidorArray.ToObject<List<TipoServicio>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tipos;
        }

        private void Imagen(string img)
        {
            bool correcto = false;
            int id = IDSERV();
            try
            {
                string url = generalUrl + "Usuario/actFoto?idUsuario=" + id + "&imagen=" + img;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if (httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

    }
}

