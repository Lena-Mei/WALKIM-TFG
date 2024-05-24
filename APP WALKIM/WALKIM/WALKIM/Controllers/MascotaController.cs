using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class MascotaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MascotaController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }
        private int IDUSER()
        {
            int idUsuario = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            return idUsuario;
        }

        private void Datos()
        {
            var animales = GetAllTipoAnimal();
            var usuarios = ListadoUsuario();

            ViewBag.Animales = animales;
            ViewBag.Usuarios = usuarios;

        }
        private string generalUrl = "https://localhost:7006/api/";

        public async Task<IActionResult> MascotaDetalle(int idMascota)
        {
            Datos();
            Mascota mascota = GetMascota(idMascota);
            return View(mascota);
        }

        [HttpPost]
        public ActionResult ActImagen(IFormFile img, int idMascota)
        {

            string strRutaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "IMG");
            //string strExtension = Path.GetExtension(img.FileName);
            string strNombreFichero = img.FileName;
            string strRutaFcihero = Path.Combine(strRutaImagenes, strNombreFichero);
            using (var fileStream = new FileStream(strRutaFcihero, FileMode.Create))
            {
                img.CopyTo(fileStream);
            }

            Imagen(strNombreFichero, idMascota);
            return RedirectToAction("MascotaDetalle", new {idMascota=idMascota});
        }


        [HttpGet]
        public ActionResult AnyadirMascota()
        {
            var animales = GetAllTipoAnimal();

            ViewData["tipoAnimal"] = new SelectList(animales, "idAnimal", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnyadirMascota(Mascota mascota)
        {
            mascota.idUsuario = IDUSER();
            if (InsertarMascota(mascota))
            {
                return RedirectToAction("MisDatos", "Usuario");
            }
            else
            {
                return View("Error"); 
            }
        }

        public ActionResult EliminarMascota(int idMascota)
        {
            if (EliminarMas(idMascota))
            {
                return RedirectToAction("MisDatos", "Usuario");
            }
            else
            {
                return View("Error");
            }
        }


        private bool EliminarMas(int idMascota)
        {
           
                bool correcto = false;
                string url = generalUrl + "Mascota/eliminarMascota?idMascota=" + idMascota;
                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "DELETE";
                    httpRequest.ContentType = "application/json";
                    httpRequest.Accept = "application/json";

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


        private Mascota GetMascota(int idMascota)
        {
            Mascota mascota = new Mascota();
            string url = generalUrl + "Mascota/getMascota?idMascota=" + idMascota;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    mascota = json["mascota"].ToObject<Mascota>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mascota;
        }

        [HttpGet]
        public ActionResult ActMascota(int idMascota)
        {
            var mascota = GetMascota(idMascota);
            var animales = GetAllTipoAnimal();

            ViewData["tipoAnimal"] = new SelectList(animales, "idAnimal", "nombre", mascota.idTipoAnimal);
            return View(mascota);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActMascota(Mascota mascota)
        {
            if (ActualizarMascota(mascota))
            {
                return RedirectToAction("MisDatos", "Usuario");
            }
            else
            {
                return View("Error");
            }
        }



















        private bool ActualizarMascota(Mascota mascota)
        {
            bool correcto = false;
            var mascotaData = new { mascota = mascota };
            string url = generalUrl + "Mascota/actMascota";
            string jsonDatos = JsonConvert.SerializeObject(mascotaData);
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

        private void Imagen(string img, int idMascota)
        {
            bool correcto = false;
            try
            {
                string url = generalUrl + "Mascota/cambiarFoto?idMascota=" + idMascota + "&img=" + img;
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

        private bool InsertarMascota(Mascota mascota)
        {
            bool correcto = false;
            var mascotaData = new { mascota = mascota };
            string url = generalUrl + "Mascota/insertarMascota";
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
