using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Obligatorio1.Controllers;
using Libreria.LogicaNegocio.Entidades;

namespace Libreria.Web.Controllers
{
    public class CabaniaController : Controller
    {

        private IRepositorioCabania RepoCab;
        private IRepositorioTipoCabania RepoTipoCab;
        private IRepositorioParametros RepoParametro;
        private IWebHostEnvironment _environment;

        public CabaniaController(IRepositorioCabania RC, IRepositorioTipoCabania RTC, IRepositorioParametros RP, IWebHostEnvironment environment)
        {
            RepoCab = RC;
            RepoTipoCab = RTC;
            RepoParametro = RP;
            this._environment = environment;
        }

        //private readonly ILogger<CabaniaController> _logger;
        
        //public CabaniaController(ILogger<CabaniaController> logger, IWebHostEnvironment environment)
        //{
        //    _logger = logger;
        //    this._environment = environment;
        //}

    

    // GET: CabaniaController
    public ActionResult Index()
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");
            if (user != null)
            {
                try
                {
                    ViewBag.Tipos = RepoTipoCab.FindAll();
                    var list = RepoCab.FindAll();
                    return View(list);
                }
                catch (Exception e)
                {

                   ViewBag.msg = e.Message;
                   return View();
                }

            }
            else {

                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]

        public IActionResult Index(int? cantPer, string? hab, string? nomCab, int? tipoId ) {

            
            IEnumerable<Cabania> ret = null;
         

            try
            {
                ret = RepoCab.FindByFiltrado(nomCab, tipoId, cantPer, hab);
                
                if (ret == null) {
                    ViewBag.msg = "No se econtro ninguna Cabania";
                }
               
                ViewBag.Tipos = RepoTipoCab.FindAll();
                return View(ret);
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        // GET: CabaniaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CabaniaController/Create
        public ActionResult Create()
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null) {
                IEnumerable<TipoCabania> listaTipos = RepoTipoCab.FindAll();

                if (listaTipos != null)
                {
                    ViewBag.listaTipos = listaTipos;
                }

                return View();

            }else {
                return RedirectToAction("Index", "Home"); 
            }
         
        }

        // POST: CabaniaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cabania cab, IFormFile imagen)
        {
            IEnumerable<TipoCabania> listaTipos = RepoTipoCab.FindAll();

            if (listaTipos != null)
            {
                ViewBag.listaTipos = listaTipos;
            }
            if (cab != null && imagen != null)
            {
                try
                {
                    cab.topiMaximoCab = RepoParametro.FindByNombre("topeMaximoCab");
                    cab.topiMinimoCab = RepoParametro.FindByNombre("topeMinimoCab");
                    RepoCab.Add(cab);
                    Cabania bus = RepoCab.FindById(cab.NumeroHabitacion);
                    string rutaFisicaWwwRoot = _environment.WebRootPath;
                    RepoCab.GuardarImg(bus, imagen, rutaFisicaWwwRoot);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception e)
                {
                    ViewBag.msg = e.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "Falta la imagen";
                return View();
            }
        }

        // GET: CabaniaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CabaniaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CabaniaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CabaniaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
