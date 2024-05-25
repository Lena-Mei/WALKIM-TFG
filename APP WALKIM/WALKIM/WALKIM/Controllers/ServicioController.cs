using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class ServicioController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";


        private void DATOS(int idServicio)
        {
            var resenyas = Resenyas(idServicio);
            var tipos = ListadoTipos();
            var servidores = ListadoServidores();
            var usuarios = ListadoUsuario();
            var animales = ListadoAnimales();


            ViewBag.Resenyas = resenyas;
            ViewBag.Tipos = tipos;
            ViewBag.Servidores = servidores;
            ViewBag.Animales = animales;
            ViewBag.Usuarios = usuarios;
        }

        public IActionResult Index()
        {
            if (User.Identity!.IsAuthenticated && User.IsInRole("servidor"))
            {
                var tipos = ListadoTipos();
                ViewBag.Tipos = tipos;
                var servicios = ListadoServicios();
                return View(servicios);
            }
            else
            {
                return View("Error");
            }

        }

        public IActionResult Detalle(int idServicio)
        {
            DATOS(idServicio);
            Servicio servicio = GetServicio(idServicio);
            return View(servicio);
        }

        [HttpGet]
        public IActionResult CrearServicio()
        {
            var tipos = ListadoTipos();
            var animales = ListadoAnimales();

            ViewData["tipoServicio"] = new SelectList(tipos, "idTipoServicio", "nombre");
            ViewBag.Animales = animales;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearServicio(Servicio servicio, int[] animalesSeleccionados)
        {
            int idServidor = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());

            servicio.aceptaTipo = animalesSeleccionados.Select(id => new TipoAnimalServicio
            {
                idTipoAnimal = id,
                idServicio = servicio.idServicio // Esto debería establecerse correctamente después de guardar el servicio en la base de datos
            }).ToList();
            servicio.idServidor = idServidor;
            //if (ModelState.IsValid)
            //{
                if (AnyadirServicio(servicio))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Mensaje = "Error al añadir un unevo servicio";
                    return View(servicio);
                }
            //}
            //else
            //{
            //      ViewBag.Mensaje = "Error al añadir un unevo servicio";
            //        return View(servicio);
            //}
        }

        public IActionResult Contratos(int idServicio)
        {
            var usuarios = ListadoUsuario();
            var estados = ListadoEstados();
            ViewBag.Estados = estados;
            ViewBag.Usuarios = usuarios;

            var servicio = GetServicio(idServicio);
            return View(servicio);
        }


        [HttpGet]
        public IActionResult ActualizarServicio(int idServicio)
        {
            var tipos = ListadoTipos();
            var animales = ListadoAnimales();

            ViewData["tipoServicio"] = new SelectList(tipos, "idTipoServicio", "nombre");
            ViewBag.Animales = animales;
            var servicio = GetServicio(idServicio);
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarServicio(Servicio servicio, int[] animalesSeleccionados)
        {
            servicio.aceptaTipo = animalesSeleccionados.Select(id => new TipoAnimalServicio
            {
                idTipoAnimal = id,
                idServicio = servicio.idServicio // Esto debería establecerse correctamente después de guardar el servicio en la base de datos
            }).ToList();

            if (ActServicio(servicio))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("error");
            }
        }





        private bool ActServicio(Servicio servicio)
        {
            bool correcto = false;
            var servicioData = new { servicio = servicio };
            string url = generalUrl + "Servicio/actualizarServicio";
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




        private bool AnyadirServicio(Servicio servicio)
        {
            bool correcto = false;
            var servicioData = new { servicio = servicio };
            string url = generalUrl + "Servicio/insertarServicio";
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
        private List<Servidor> ListadoServidores()
        {
            List<Servidor> servidores = new List<Servidor>();
            try
            {
                string url = generalUrl + "Servidor/getAllServidor";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var servidorArray = json["listaServidor"].ToObject<JArray>();
                    servidores = servidorArray.ToObject<List<Servidor>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return servidores;
        }

        private List<TipoAnimal> ListadoAnimales()
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
                    var servidorArray = json["listaAnimal"].ToObject<JArray>();
                    animales = servidorArray.ToObject<List<TipoAnimal>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return animales;
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

        private List<Servicio> ListadoServicios()
        {
            var idServidor = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            List<Servicio> servicios = new List<Servicio>();
            try
            {
                string url = generalUrl + "Servicio/getAllServicio?idServidor=" + idServidor;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var serviciosArray = json["listaServicio"].ToObject<JArray>();
                    servicios = serviciosArray.ToObject<List<Servicio>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return servicios;
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


    }
}
