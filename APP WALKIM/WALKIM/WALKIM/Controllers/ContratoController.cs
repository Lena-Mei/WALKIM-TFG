using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class ContratoController : Controller
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

        [HttpGet]
        public IActionResult CrearContrato(int idServicio)
        {

            ViewBag.IDSer = idServicio;
            
            var mascotasUsuario = MascotasUsuario(IDUSER());
            var servicio = GetServicio(idServicio);

            ViewBag.Servicio = servicio;
            ViewBag.Mascotas = mascotasUsuario;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearContrato(Contrata contrato, int[] mascotaSeleccionado)
        {
            contrato.idUsuario = IDUSER();
            contrato.listaMascotas = mascotaSeleccionado.Select(id => new MascotaContrato
            {
                idContrato = contrato.idContrato,
                idMascota = id
            }).ToList();

            if (AnyadirContrato(contrato))
            {
                return RedirectToAction("MisContratos", "Usuario");
            }
            else
            {
                return View("Error");
            }
        }


        public IActionResult DetalleContrato(int idContrato) 
        {
          

            var contrato = GetContrato(idContrato);
            var servicio = GetServicio(contrato.idServicio);
            ViewBag.Servicio = servicio;
            var usuarios = GetUsuario(contrato.idUsuario);
            ViewBag.Usuarios = usuarios;
            var animales = GetAllTipoAnimal();
            ViewBag.Animales = animales;

            var estados = ListadoEstados();
            ViewBag.Estados = estados;
            return View(contrato);
        }

        public IActionResult ActEstado (int idEstado, int idContrato)
        {
            if(ActualizarEstado(idEstado, idContrato))
            {
                return RedirectToAction("DetalleContrato", "Contrato", new {idContrato= idContrato});
            }
            else
            {
                return View("Error");
            }
        }



        private bool ActualizarEstado(int idEstado, int idContrato)
        {
            bool correcto = false;
            try
            {
                string url = generalUrl + "Contrata/actEstadoContrato?idEstado="+idEstado+"&idContrato="+idContrato;

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
            return correcto;
        }


        private List<Mascota> MascotasUsuario(int id)
        {

            List<Mascota> mascotas = new List<Mascota>();
            try
            {
                string url = generalUrl + "Mascota/getAllMascota?idUsuario=" + id;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var contratosArray = json["listaMascota"].ToObject<JArray>();
                    mascotas = contratosArray.ToObject<List<Mascota>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mascotas;
        }

        private Servicio GetServicio(int id)
        {
            Servicio servicio = new Servicio();
            string url = generalUrl + "Servicio/getServicio?idServicio=" + id;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    servicio = json["servicio"].ToObject<Servicio>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                servicio = null;
            }
            return servicio;
        }
        private Contrata GetContrato(int id)
        {
            Contrata contrato = new Contrata();
            string url = generalUrl + "Contrata/getContrato?idContrato=" + id;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    contrato = json["contrata"].ToObject<Contrata>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                contrato = null;
            }
            return contrato;
        }

        private Usuario GetUsuario(int id)
        {
            Usuario servicio = new Usuario();
            string url = generalUrl + "Usuario/getUsuario?idUsuario=" + id;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    servicio = json["usuario"].ToObject<Usuario>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                servicio = null;
            }
            return servicio;
        }

        private bool AnyadirContrato(Contrata contrata)
        {
            bool correcto = false;
            var servicioData = new { contrato = contrata };
            string url = generalUrl + "Contrata/crearContrato";
            string jsonDatos = JsonConvert.SerializeObject(servicioData);
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

        private List<Estado> ListadoEstados()
        {
            List<Estado> estados = new List<Estado>();
            try
            {
                string url = generalUrl + "Estado/getAllEstado";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var estadoArray = json["estadoLista"].ToObject<JArray>();
                    estados = estadoArray.ToObject<List<Estado>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return estados;
        }

        private List<TipoAnimal> GetAllTipoAnimal()
        {
            List<TipoAnimal> animales = new List<TipoAnimal>();
            try
            {
                string url = generalUrl + "TipoAnimal/getAllTipoAnimal";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var animalArray = json["listaAnimal"].ToObject<JArray>();
                    animales = animalArray.ToObject<List<TipoAnimal>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return animales;
        }
    }
}
