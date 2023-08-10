
using Libreria.Web.Models;
using Libreria.Web.Models.auxiliares;
using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Controllers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Libreria.Web.Controllers
{
    public class TipoCabaniaController : Controller
    {
        private readonly ILogger<TipoCabaniaController> _logger;
        private static HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7296/api") };

    
        private Uri _urlTipoCabania = new Uri($"{_httpClient.BaseAddress}/TipoCabania");


       
        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };


        public TipoCabaniaController(ILogger<TipoCabaniaController> logger)
        {
            _logger = logger;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public IActionResult Index()
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");
          
            if(true)
            {
                try
                {
                    var json = GetRepuesta(_urlTipoCabania);

                    if (json == null) {
                        ViewBag.msg = "A ocurrido un error";
                        return View();
                    
                    }

                    var listaTipoCabana = JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(json, opciones);
                    return View(listaTipoCabana);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }



        public IActionResult Create()
        {

            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Create(TipoCabaniaModel tipocab)
        {
            if (tipocab == null) {
                ViewBag.msg = "No se recivio ningun Tipo cabania";
                return View();
            }

            try
            {
                //Serealizar el obj
                var tipocabSerealizado = JsonSerializer.Serialize(tipocab);
                // crear el body 
                string usuario = HttpContext.Session.GetString("usuario");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);
                var body = new StringContent(tipocabSerealizado, Encoding.UTF8, "application/json");

                
                try
                {
                    var respuesta = _httpClient.PostAsync(_urlTipoCabania, body).Result;
                    if (respuesta.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                    else {

                        ViewBag.msg = "Error al crear el Tipo Cabania";
                        return View();

                    }

                }
                catch (Exception e)
                {

                    ViewBag.msg = e.Message;
                    return View();
                }

              

              

            }
            catch (Exception e)
            {

                ViewBag.msg = e.Message;
                return View();
            }



        }


        public IActionResult Remove(int id)
        {
            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {

                try
                {
                    string session = HttpContext.Session.GetString("usuario");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

                    var repuesta = _httpClient.DeleteAsync($"{_urlTipoCabania}/{id}").Result;

                    if (repuesta.IsSuccessStatusCode) {

                        return RedirectToAction("Index");
                    }

                    ViewBag.msg = "No se pudo eliminar";
                    return RedirectToAction("Index");
                    
                }
                catch (Exception e)
                {
                    ViewBag.msg = e.Message;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult View(int id) {

            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);

                    var response = _httpClient.GetAsync($"{_urlTipoCabania}/{id}").Result;
                    var json = response.Content.ReadAsStringAsync().Result;

                    var jsonDeserializado = JsonSerializer.Deserialize<TipoCabaniaModel>(json, opciones);



                    return View(jsonDeserializado);
                }
                catch (Exception e)
                {
                    ViewBag.msg = e.Message;
                    return View();
                    
                }

            }
            return RedirectToAction("Index", "Home");


        }

        public IActionResult BuscadorTipoCabania()
        {

            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                return View();
            }
            else { return RedirectToAction("Index", "Home"); }


        }
        [HttpPost]
        public IActionResult BuscadorTipoCabania(string nombreTipo)
        {

            string? user = HttpContext.Session.GetString("usuario");

            try
            {
                var info = new NombreTipoBuscador() { Valor = nombreTipo };

                var json = JsonSerializer.Serialize(info);

                var body = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);

                var respuesta = _httpClient.PostAsync($"{_urlTipoCabania}/Nombre", body).Result;

                if (respuesta.IsSuccessStatusCode) {
                        
                    var content = respuesta.Content.ReadAsStringAsync().Result;

                    var tipo = JsonSerializer.Deserialize<TipoCabaniaModel>(content, opciones);

                    return View(tipo);
                }

                ViewBag.msg = "No hay ningun tipo";
                return View();
            }
            catch (Exception e)
            {
               ViewBag.msg = e.Message;
               return View();
            }


        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);

                    var respuesta = _httpClient.GetAsync($"{_urlTipoCabania}/{id}").Result;

                    var json = respuesta.Content.ReadAsStringAsync().Result;

                    var tipoCab = JsonSerializer.Deserialize<TipoCabaniaModel>(json, opciones);

                   
                    if (tipoCab != null)
                    {
                        return View(tipoCab);
                    }
                    else
                    {
                        TempData["error"] = "No existe ese tipoCabNuevo";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                    return RedirectToAction("Index");

                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpPost]

        public IActionResult Update(TipoCabaniaModel tipoCabNuevo)
        {

            if (tipoCabNuevo != null)
            {
                try
                {
                    var jsonSerea = JsonSerializer.Serialize(tipoCabNuevo);

                    string usuario = HttpContext.Session.GetString("usuario");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);

                    var body = new StringContent(jsonSerea, Encoding.UTF8, "application/json");

                    var respuesta = _httpClient.PutAsync($"{_urlTipoCabania}/{tipoCabNuevo.Id}", body).Result;

                    if (respuesta.IsSuccessStatusCode) { 
                        return RedirectToAction("Index");
                    }

                    ViewBag.msg = "No se pudo actualizar";
                    return View(tipoCabNuevo);
                }
                catch (Exception e)
                {

                    ViewBag.msg = e.Message;
                    return View(tipoCabNuevo);
                }

            }
            else
            {
                ViewBag.msg = "No se pudo actualizar";
                return View();
            }

        }

        private string? GetRepuesta(Uri url) {
            var session = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var respuesta = _httpClient.GetAsync(url).Result;
            respuesta.EnsureSuccessStatusCode();

            var json = respuesta.Content.ReadAsStringAsync().Result;

            return json;
        
        }


    }
}
