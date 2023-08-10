using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Web.Controllers
{
    public class MantenimientoController : Controller
    {
       
        private IRepositorioMantenimiento RepoMan;
        private IRepositorioCabania RepoCab ;
        private IRepositorioParametros RepoParametro;

        public MantenimientoController(IRepositorioMantenimiento RM, IRepositorioCabania RC, IRepositorioParametros RP)
        {
            RepoMan = RM;
            RepoCab = RC;
            RepoParametro = RP;
        }
        // GET: MantenimientoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MantenimientoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MantenimientoController/Create/4
        public ActionResult Create(int id)
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");
            if (user != null)
            {
                HttpContext.Session.SetInt32("NumeroHabitacion", id);
                ViewBag.cab = RepoCab.FindById(id);
                return View();
            }
            else { 
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: MantenimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mantenimiento mant)
        {
            int? idCab = HttpContext.Session.GetInt32("NumeroHabitacion");
            ViewBag.cab = RepoCab.FindById(idCab);
            try
            {
                if (RepoCab.CantidadDeMantenimientos(idCab) < 3)
                {
                    Cabania buscada = RepoCab.FindById(idCab);
                    if (buscada != null)
                    {
                        try
                        {
                            mant.topeMaximoMtto = RepoParametro.FindByNombre("topeMaximoMtto");
                            mant.topeMinimoMtto = RepoParametro.FindByNombre("topeMinimoMtto");
                            RepoMan.AddMtto(mant, buscada);
                            buscada.MisMantenimientos.Add(mant);
                            return RedirectToAction("Index", "Cabania");
                        }
                        catch (Exception e)
                        {
                            ViewBag.msg = e.Message;
                            return View();
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Cabania");
                    }
                }
                else {
                    ViewBag.msg = "Ya tiene 3 manteniemientos el dia de hoy";
                    return View();
                } 
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }

        // GET: MantenimientoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MantenimientoController/Edit/5
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

        // GET: MantenimientoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MantenimientoController/Delete/5
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

        public IActionResult MantenimientosFecha(int id)
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null)
            {
                try
                {
                    HttpContext.Session.SetInt32("idCab", id);
                    Cabania cab = RepoCab.FindById(id);
                    IEnumerable<Mantenimiento> mant = cab.MisMantenimientos;
                    if (mant == null)
                    {
                        ViewBag.msg = "No tiene mantenimientos";
                    }
                    return View(mant);
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
        [HttpPost]
        public IActionResult MantenimientosFecha(DateTime Fecha1, DateTime Fecha2)
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null)
            {
                try
                {
                    int? idCab = HttpContext.Session.GetInt32("idCab");
                    IEnumerable<Mantenimiento> mant = RepoMan.FindByFechas(Fecha1, Fecha2, idCab);
                    if (mant == null) {
                        ViewBag.msg = "No tiene mantenimientos";
                    }
                    return View(mant);
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
    }
}
