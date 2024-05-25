using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class ArchivoController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";

        private int IDUSER()
        {
            int idServidor = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            return idServidor;
        }

        public async Task<IActionResult> Index()
        {
            var archivos = await GetAllArchivo();
            ViewBag.Sitiene = archivos.Count > 0;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubirArchivos(List<IFormFile> archivos)
        {
            if (archivos == null || archivos.Count != 3)
            {
                ModelState.AddModelError("", "Debe subir exactamente 3 archivos.");
                return View("Index");
            }

            foreach (var formFile in archivos)
            {
                if (formFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        var archivo = new Archivo
                        {
                            nombreArchivo = formFile.FileName,
                            archivo = memoryStream.ToArray(),
                            extensionArchivo = Path.GetExtension(formFile.FileName),
                            fechaEntrada = DateTime.Now,
                            idServidor = IDUSER()
                        };

                        if (!await SubirArchivo(archivo))
                        {
                            ModelState.AddModelError("", "Hubo un problema al subir el archivo " + formFile.FileName);
                            return View("Index");
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private async Task<bool> SubirArchivo(Archivo archivo)
        {
            bool correcto = false;
            var archivoData = new { archivo = archivo };
            string url = generalUrl + "Archivo/subirArchivo";
            string jsonDatos = JsonConvert.SerializeObject(archivoData);
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                using (var streamWriter = new StreamWriter(await httpRequest.GetRequestStreamAsync()))
                {
                    await streamWriter.WriteAsync(jsonDatos);
                }

                var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if (httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string errorMensaje = await streamReader.ReadToEndAsync();
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

        private async Task<List<Archivo>> GetAllArchivo()
        {
            List<Archivo> archivos = new List<Archivo>();
            try
            {
                string url = generalUrl + "Archivo/getAllArchivo?idServidor=" + IDUSER();
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = await streamReader.ReadToEndAsync();
                    var json = JObject.Parse(resultado);
                    var archivoArray = json["listaArvicho"].ToObject<JArray>();
                    archivos = archivoArray.ToObject<List<Archivo>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return archivos;
        }
    }
}
