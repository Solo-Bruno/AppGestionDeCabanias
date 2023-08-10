using Libreria.Web.Models;
using Libreria.Web.Models.auxiliares;
using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Controllers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Libreria.Web.Controllers
{
    public class MantenimientoController : Controller
    {

        private readonly ILogger<MantenimientoController> _logger;
        private static HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7296/api") };


        private Uri _urlMantenimiento = new Uri($"{_httpClient.BaseAddress}/Mantenimiento");

        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };
        public MantenimientoController(ILogger<MantenimientoController> logger)
        {
            _logger = logger;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

       

        public IActionResult Create(int id)
        {

            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                HttpContext.Session.SetInt32("NumeroHabitacion", id);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        public IActionResult Create(MantenimientoModel mant)
        {
            int? idCab = HttpContext.Session.GetInt32("NumeroHabitacion");

            int cabId = (int)idCab;
            if (mant == null)
            {
                ViewBag.msg = "No se recibio ningun mantenimiento";
                return View();
            }
            mant.CabaniaId = cabId;
            try
            {
                //Serealizar el obj
                var mantSerealizado = JsonSerializer.Serialize(mant);
                // crear el body 
                string usuario = HttpContext.Session.GetString("usuario");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);
                var body = new StringContent(mantSerealizado, Encoding.UTF8, "application/json");


                try
                {
                    var respuesta = _httpClient.PostAsync(_urlMantenimiento, body).Result;
                    if (respuesta.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index", "Cabania");
                    }
                    else
                    {

                        ViewBag.msg = "No se pudo crear el mantenimiento";
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

        [HttpGet]
        public IActionResult MantenimientosFecha(int id)
        {
            var valores = new ValoresBusquedaFechaModel() { 
                id = id,

            };

            var json = JsonSerializer.Serialize(valores);

            var body = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                string usuario = HttpContext.Session.GetString("usuario");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);
                var response = _httpClient.PostAsync($"{_urlMantenimiento}/All", body).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var listaMtto = JsonSerializer.Deserialize<IEnumerable<MantenimientoModel>>(content, opciones);

                    return View(listaMtto);
                }

                return RedirectToAction("Index", "Cabania");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cabania");
            }


        }

        [HttpPost]

        public IActionResult MantenimientosFecha(int id, DateTime Fecha1, DateTime Fecha2)
        {
            var valores = new ValoresBusquedaFechaModel()
            {
                id = id,
                fch1 = Fecha1,
                fch2 = Fecha2,

            };

            var json = JsonSerializer.Serialize(valores);

            var body = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                string usuario = HttpContext.Session.GetString("usuario");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);
                var response = _httpClient.PostAsync($"{_urlMantenimiento}/fechas", body).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var listaMtto = JsonSerializer.Deserialize<IEnumerable<MantenimientoModel>>(content, opciones);

                    return View(listaMtto);
                }

                return RedirectToAction("Index", "Cabania");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cabania");
            }


        }

        [HttpGet]
        public IActionResult MttoGroupBy() {

            string usuario = HttpContext.Session.GetString("usuario");

            if (usuario == null) { return RedirectToAction("Index", "Home"); }
            return View(); 
        
        }

        [HttpPost]


        public IActionResult MttoGroupBy(int top1, int top2)
        {
            MttoGroupByModel mttoGroupBy = new MttoGroupByModel() { 
            
                Top1 = top1,
                Top2 = top2,
            };
            string usuario = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);
            try
            {
                var json = JsonSerializer.Serialize(mttoGroupBy);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync($"{_urlMantenimiento}/GroupBy", body).Result;

                if (response.IsSuccessStatusCode) { 
                    var content = response.Content.ReadAsStringAsync().Result;
                    
                    var listaRet = JsonSerializer.Deserialize<IEnumerable<MttoGroupByView>>(content, opciones);

                    return View(listaRet);
                }

                ViewBag.msg = "No se pudo realizar";
                return View();
                
            }
            catch (Exception e)
            {
                ViewBag.msg =e.Message;
                return View();

            }
            

        }


        private string? GetRepuesta(Uri url)
        {
            var session = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var respuesta = _httpClient.GetAsync(url).Result;
            respuesta.EnsureSuccessStatusCode();

            var json = respuesta.Content.ReadAsStringAsync().Result;

            return json;

        }
    }
}
