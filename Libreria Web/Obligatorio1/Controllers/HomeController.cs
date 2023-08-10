
using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;
using Libreria.Web.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Obligatorio1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7296/api") };

        private Uri _url = new Uri($"{_httpClient.BaseAddress}/Usuario");
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(UsuarioModel usuario)
        {
            usuario.Token = "Sin Token";

            var usuarioSerializado = System.Text.Json.JsonSerializer.Serialize(usuario, opciones);
            var json = new StringContent(usuarioSerializado, Encoding.UTF8, "application/json");

            try
            {
                var response = _httpClient.PostAsync(_url + "/login", json).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonRespueste = response.Content.ReadAsStringAsync().Result;
                    var usuarioEncontrado = System.Text.Json.JsonSerializer.Deserialize<UsuarioModel>(jsonRespueste, opciones);

                    if (usuarioEncontrado == null || String.IsNullOrEmpty(usuarioEncontrado.Token))
                    {
                        return View();
                    }
                    else
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuarioEncontrado.Token);
                        HttpContext.Session.SetString("usuario", usuarioEncontrado.Token);
                        return RedirectToAction("Index");
                    }


                }
                else
                {
                    ViewBag.msg = "Hubo un error al intentar encontrar el usuario";
                    return View(usuario);
                }
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View(usuario);
            }
            
           
        }


        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]

        //public IActionResult Create(Usuario us)
        //{

        //    if (us != null)
        //    {
        //        try
        //        {
        //            RepUs.Add(us);
        //            ViewBag.msg = "Usuario creado";
        //            HttpContext.Session.SetInt32("UserIdLogin", us.Id);
        //            return RedirectToAction("Index");
        //        }
        //        catch (System.Exception e)
        //        {

        //            ViewBag.msg = e.Message;
        //        }
        //        return View();
        //    }
        //    else
        //    {

        //        ViewBag.msg = "El usuario es nulo";
        //        return View();

        //    }
        //}


        public IActionResult Logout()
        {
            if (_httpClient != null)
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            HttpContext.Session.Clear();
            return View(nameof(Login));

        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}