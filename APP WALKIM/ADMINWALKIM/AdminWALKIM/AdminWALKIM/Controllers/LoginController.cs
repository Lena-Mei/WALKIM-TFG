using AdminWALKIM.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace AdminWALKIM.Controllers
{
    public class LoginController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(Administrador admin)
        {
            string errorMensaje;
            int id = InicioSesion(admin.correo, admin.contrasenya, out errorMensaje);
            if (id > 0)
            {

                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),

        };

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
            return RedirectToAction("IniciarSesion", "Login");
        }

        private int InicioSesion(string correo, string contrasenya, out string Error)
        {
            Error = "";
            int id = 0;
            var url = generalUrl + "Administrador/loginUsuario?correo=" + correo + "&contrasenya=" + contrasenya;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);

                    id = int.Parse(json["idAdmin"].ToString());

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

    }
}
