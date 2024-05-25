using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WALKIM.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Azure.Core;
using Newtonsoft.Json;


namespace WALKIM.Controllers
{
    //hacer validaciones de que el usuario tipo "Usuario" solo puede meterse a este tipo de acciones ("tipoUsuario") ("usuario")

    
    public class UsuarioController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";

        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsuarioController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        private void DATOS()
        {
            var tipos = ListadoTipos();
            var servidores = ListadoServidores();
            var servicios = ListadoServicios();
            var estados = ListadoEstados();

            ViewBag.Tipos = tipos;
            ViewBag.Servidores = servidores;
            ViewBag.Servicios = servicios;
            ViewBag.Estados = estados;

        }

        private int IDUSER()
        {
            int idUsuario = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            return idUsuario;
        }

        public IActionResult Index()
        {
            List<Usuario> usuarios = ListadoUsuario();
            return View(usuarios);
        }

        public ActionResult DetalleUsuario(int id)
        {
            var animales = GetAllTipoAnimal();
            ViewBag.Animales = animales;
            var usuario = GetUsuario(id);
            return View(usuario);
        }

        public ActionResult MisContratos()
        {
            if(User.Identity!.IsAuthenticated && User.IsInRole("usuario"))
            {
                DATOS();
                var contratos = GetContratos(IDUSER());
                return View(contratos);
            }
            else
            {
                return View("error");
            }
        }

        public async Task< IActionResult> MisDatos() 
        {
            var animales = await GetAllTipoAnimal();
            ViewBag.Animales = animales;

           if(User.Identity!.IsAuthenticated && User.IsInRole("usuario"))
            {
               int idUsuario= IDUSER();
                var usuario = await GetUsuario(idUsuario);
                return View(usuario);
            }
            else
            {
                return View("Error");
            } 
        }

        [HttpPost]
        public ActionResult ActImagen(IFormFile img)
        {
            string id = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();


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
        public ActionResult ActUsuario()
        {
            var usuario = GetUsuario(IDUSER());
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActUsuario(Usuario usuario)
        {
            if (ActualizarUsuario(usuario))
            {
                return RedirectToAction(nameof(MisDatos));
            }
            else
            {
                return View("Error");
            }
        }



        private bool ActualizarUsuario(Usuario usuario)
        {
            bool correcto = false;
            var usuarioData = new { usuario = usuario };
            string url = generalUrl + "Usuario/actUsuario";
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

        private async Task<Usuario> GetUsuario(int id)
        {
            Usuario usuario = new Usuario();
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
                    usuario = json["usuario"].ToObject<Usuario>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                usuario = null;
            }
            return usuario;
        }

        private void Imagen (string img)
        {
            bool correcto=false;
            string id = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            try
            {
                string url = generalUrl + "Usuario/actFoto?idUsuario=" + id + "&imagen=" + img;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if(httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        private async Task< List<TipoAnimal> >GetAllTipoAnimal()
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

        private List<Contrata> GetContratos(int id)
        {
            List<Contrata> contratos = new List<Contrata>();
            try
            {
                string url = generalUrl + "Contrata/getAllContrato?idUsuario=" + id;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var contratosArray = json["listaContrata"].ToObject<JArray>();
                    contratos = contratosArray.ToObject<List<Contrata>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return contratos;
        }

        private List<Servicio> ListadoServicios()
        {
            List<Servicio> servicios = new List<Servicio>();
            try
            {
                string url = generalUrl + "Servicio/getAllServicio";
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
