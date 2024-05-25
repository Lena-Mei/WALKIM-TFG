using AdminWALKIM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace AdminWALKIM.Controllers
{
    public class AdminController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";
        public async Task<IActionResult> Index()
        {
            var admins = await ListadoAdmin();
            return View(admins);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int idAdmin)
        {
            var admin = await GetAdmin(idAdmin);
            if(admin != null)
            {
                return View(admin);
            }
            else
            {
                return View("Error en la API");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Editar(Administrador objAdmin)
        {
            string error = "";
            if (ModelState.IsValid)
            {
                bool correcto = ActDatosAdmin(objAdmin, out error);
                if (correcto)
                {
                    ViewData["correcto"] = true;
                    return View(objAdmin);

                }
                else
                {
                    ViewData["mensaje"] = error;
                    return View(objAdmin);
                }
            }
            return View(objAdmin);
        }


       public async Task<ActionResult> EliminarAdmin(int idAdmin)
        {
            if (await EliminarAdministrador(idAdmin))
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View("Error");
            }
        }



        private async Task<bool> EliminarAdministrador(int idAdmin)
        {

            bool correcto = false;
            string url = generalUrl + "Administrador/eliminarAdmin?idAdmin=" + idAdmin;
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


        private async Task< List<Administrador>> ListadoAdmin()
        {
            List<Administrador> listaAdmin = new List<Administrador>();
            try
            {
                string url = generalUrl + "Administrador/getAllAdmin";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var pedidoArray = json["listaAdmin"].ToObject<JArray>();
                    listaAdmin = pedidoArray.ToObject<List<Administrador>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaAdmin;
        }

        private async Task< Administrador> GetAdmin(int? idAdmin)
        {
            Administrador admin = new Administrador();
            string url = generalUrl + "Administrador/getAdmin?idAdmin=" + idAdmin;
            try
            {


                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    admin = json["administrador"].ToObject<Administrador>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                admin = null;
            }
            return admin;
        }

        private bool ActDatosAdmin(Administrador objAdmin, out string error)
        {
            error = "";
            bool correcto = false;
            var adminData = new { administrador = objAdmin };

            string jsonDatos = JsonConvert.SerializeObject(adminData);
            string url = generalUrl + "Administrador/actAdmin";
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

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400")) //BadRequest
                {
                    error = "Algún dato está vacío o es nulo.";

                }
                else if (ex.Message.Contains("404")) //NotFound
                {

                    error = "Algún dato introducido ya existe o es inválido.";
                }
            }
            return correcto;
        }

    }
}
