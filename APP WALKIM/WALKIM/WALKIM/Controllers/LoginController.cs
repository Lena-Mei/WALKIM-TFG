using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims; //Para guardar la información del usuario
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using WALKIM.Models;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace WALKIM.Controllers
{
    public class LoginController : Controller
    {
        //private readonly ILogger<IniciarSesion> _logger;
        private string generalUrl = "https://localhost:7006/api/";


        public ActionResult ElegirRol()
        {
            return View();
        }


        [HttpGet]
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            string error;
            if(InsertarUsuario(usuario, out error))
            {
                return RedirectToAction("IniciarSesion");
            }
            else
            {
                ViewData["errorMensaje"] = error;
                return View();
            }
        }

        [HttpGet]
        public ActionResult RegistrarServidor()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarServidor(Servidor servidor)
        {
            string error;
            if(InsertarServidor(servidor, out error))
            {
                return RedirectToAction(nameof(IniciarSesion));
            }
            else
            {
                ViewData["errorMensaje"] = error;
                return View(servidor);

            }
        }


        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(Usuario usuario)
        {
            string errorMensaje;
            int id = InicioSesion(usuario.correo, usuario.contrasenya, out errorMensaje);
            if (id > 0)
            {
                var rol = "";
                var nombre = "";
                var img = "";
                var estado = 1; 

                if (id % 2 == 0)
                {
                    rol = "usuario";
                    Usuario usuarioRegistrado = GetUsuario(id);
                    nombre = usuarioRegistrado.nombre;
                    img = usuarioRegistrado.imgPerfil;
                }
                else
                {
                    rol = "servidor";
                    Servidor servidorRegistrado = GetServidor(id);
                    nombre = servidorRegistrado.nombre;
                    img = servidorRegistrado.imgPerfil;
                    estado = servidorRegistrado.idEstado; 
                }

                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Role, rol),
            new Claim("Nombre", nombre),
            new Claim("ImagenPerfil", img)
        };

                if (rol == "servidor")
                {
                    claims.Add(new Claim("IdEstado", estado.ToString()));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["errorMensaje"] = errorMensaje;
                return View();
            }
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        private  int InicioSesion(string correo, string contrasenya, out string Error)
        {
            Error = "";
            int id = 0;
            var url = generalUrl + "Usuario/loginUsuario?correo=" + correo + "&contrasenya=" + contrasenya;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);

                     id = int.Parse(json["idUsuario"].ToString());
                   
                    return id;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404")) //NotFound
                {
                    Error = "Error en la API";

                }
                else if (ex.Message.Contains("400"))
                {

                    Error = "Usuario o contraseña no válidos.";
                }
            }
            return id;
        }

        private Usuario GetUsuario(int id)
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

        private bool InsertarUsuario(Usuario usuario, out string Error)
        {
            Error = "";
            bool correcto = false;
            var servicioData = new { usuario = usuario };
            string url = generalUrl + "Usuario/insertarUsuario";
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
                if (ex.Message.Contains("404")) //NotFound
                {
                    Error = "El correo introducido ya está registrado. Introduzca otro.";

                }
                else if (ex.Message.Contains("400"))
                {

                    Error = "Error en la API";
                }
            }
            return correcto;
        }


        private bool InsertarServidor(Servidor servidor, out string Error)
        {
            Error = "";
            bool correcto = false;
            var servicioData = new { servidor = servidor };
            string url = generalUrl + "Servidor/insertarServidor";
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
                if (ex.Message.Contains("404")) //NotFound
                {
                    Error = "El correo introducido ya está registrado. Introduzca otro.";

                }
                else if (ex.Message.Contains("400"))
                {

                    Error = "Error en la API";
                }
            }
            return correcto;
        }

    }
}
