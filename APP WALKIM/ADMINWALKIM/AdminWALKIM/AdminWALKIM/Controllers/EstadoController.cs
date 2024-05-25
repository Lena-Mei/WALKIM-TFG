using AdminWALKIM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminWALKIM.Controllers
{
    public class EstadoController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";

        public async Task< IActionResult> Index()
        {
            var estados = await ListadoEstados();
            return View(estados);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int idEstado)
        {
            var estado = await GetEstado(idEstado);
            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Estado estado)
        {
            string error = "";
            if (ModelState.IsValid)
            {
                bool correcto = ActEstado(estado, out error);
                if (correcto)
                {
                    ViewData["correcto"] = true;
                    return View(estado);

                }
                else
                {
                    ViewData["mensaje"] = error;
                    return View(estado);
                }
            }
            else
            {
                return View(estado);
            }
        }



        private async Task<List<Estado>> ListadoEstados()
        {
            List<Estado> estados = new List<Estado>();
            try
            {
                string url = generalUrl + "Estado/getAllEstado";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = await streamReader.ReadToEndAsync();
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

        private bool  ActEstado(Estado estado, out string error)
        {
            error = "";
            bool correcto = false;
            var estadoData = new { estado = estado };
            string url = generalUrl + "Estado/actEstado";
            string jsonDatos = JsonConvert.SerializeObject(estadoData);
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

                if (ex.Message.Contains("400")) //BadRequest
                {
                    error = "Algún dato está vacío o es nulo.";

                }
                else if (ex.Message.Contains("404")) //NotFound
                {

                    error = "Ya existe un estado con el nombre: " + estado.nombre;
                }
            }
            return correcto;
        }

        private async Task<Estado> GetEstado(int id)
        {
            Estado estado = new Estado();
            string url = generalUrl + "Estado/getEstado/" + id;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = await streamReader.ReadToEndAsync();
                    var json = JObject.Parse(resultado);
                    estado = json["estado"].ToObject<Estado>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                estado = null;
            }
            return estado;
        }



    }
}
