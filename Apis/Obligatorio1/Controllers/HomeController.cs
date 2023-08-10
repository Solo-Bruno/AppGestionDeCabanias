using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;
using Libreria.LogicaNegocio.InterfacesRepositorio;

namespace Obligatorio1.Controllers
{
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;

        private IRepositorioUsuario RepUs;

        public HomeController(IRepositorioUsuario RU)
        {
            RepUs = RU;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("UserIdLogin");
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(string email, string pass)
        {

            try
            {
                Usuario nuevoUs = RepUs.FindByName(email, pass);

                if (nuevoUs != null)
                {

                    HttpContext.Session.SetInt32("UserIdLogin", nuevoUs.Id);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "No existe ese usuario";
                    return View();
                }
            }
            catch (Exception e)
            {

                ViewBag.msg = e.Message;
                return View();
            }


        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Usuario us)
        {

            if (us != null)
            {
                try
                {
                    RepUs.Add(us);
                    ViewBag.msg = "Usuario creado";
                    HttpContext.Session.SetInt32("UserIdLogin", us.Id);
                    return RedirectToAction("Index");
                }
                catch (System.Exception e)
                {

                    ViewBag.msg = e.Message;
                }
                return View();
            }
            else
            {

                ViewBag.msg = "El usuario es nulo";
                return View();

            }
        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}