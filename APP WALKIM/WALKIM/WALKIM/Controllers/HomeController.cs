using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using WALKIM.Models;

namespace WALKIM.Controllers
{
    public class HomeController : Controller
    {
        private string generalUrl = "https://localhost:7006/api/";

        private readonly ILogger<HomeController> _logger;

        private void Datos()
        {
            var servidores = ListadoServidores();
            var tipos = ListadoTipos();

            ViewBag.Servidores = servidores;
            ViewBag.Tipos = tipos;
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Datos();
            List<Servicio> servicios = GetServicios(null, null);
            return View(servicios);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        private List<Servicio> GetServicios(int? idServidor, int? tipoServicio)
        {
            List<Servicio> listadoServicios = new List<Servicio>();
            try
            {
                string url = generalUrl + "Servicio/getAllServicio?idTipoServicio="+tipoServicio+"&idServidor="+idServidor;
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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
    }
}
