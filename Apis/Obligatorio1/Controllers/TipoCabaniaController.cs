using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;


namespace Libreria.Web.Controllers
{
    public class TipoCabaniaController : Controller
    {
        private IRepositorioTipoCabania RepoTC;
        private IRepositorioParametros RepoParametro;


        public TipoCabaniaController(IRepositorioTipoCabania tipoCab, IRepositorioParametros repPara)
        {
            RepoTC = tipoCab;
            RepoParametro = repPara;
        }
        public IActionResult Index()
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");
          
            if(user != null)
            {
                IEnumerable<TipoCabania> TiposCab = RepoTC.FindAll();
                if (TiposCab == null) {
                    ViewBag.msg = "No hay tipos de cabanias";
                }
                return View(TiposCab);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        


        public IActionResult Create() {

            int? user = HttpContext.Session.GetInt32("UserIdLogin");

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

        public IActionResult Create(TipoCabania cab) {

            try
            {
                cab.topeMinimo = RepoParametro.FindByNombre("topeMinimoTC");
                cab.topeMaximo = RepoParametro.FindByNombre("topeMaximoTC");
                cab.Validar();
                RepoTC.Add(cab);
                return RedirectToAction("Index");
                
            }
            catch (Exception e)
            {

                ViewBag.msg = e.Message;
                return View();
            }

           

        }


        public IActionResult Remove(int id)
        {
            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null)
            {
                
                    try
                    {
                        RepoTC.RemoveById(id);
                        return RedirectToAction("Index");
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


        
         public IActionResult BuscadorTipoCabania() {

            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null)
            {
                return View();
            }else { return RedirectToAction("Index"); }


        }
        [HttpPost]
        public IActionResult BuscadorTipoCabania(string nombreTipo)
        {

            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null)
            {

                try
                {
                    var cabaniaBuscada = RepoTC.FindByName(nombreTipo);
                    if (cabaniaBuscada == null)
                    {
                        ViewBag.msg = "No hay tipoCabNuevo con ese nombre";
                    }
                    return View(cabaniaBuscada);
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

        [HttpGet]
        public IActionResult Update(int id) {

            int? user = HttpContext.Session.GetInt32("UserIdLogin");

            if (user != null)
            {
                try
                {
                    var tipoCab = RepoTC.FindById(id);
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
                catch (Exception)
                {

                    throw;
                }
            }
            else {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpPost]

        public IActionResult Update(TipoCabania tipoCabNuevo) {
            
            if (tipoCabNuevo != null)
            {
                try
                {
                    tipoCabNuevo.topeMinimo = RepoParametro.FindByNombre("topeMinimoTC");
                    tipoCabNuevo.topeMaximo = RepoParametro.FindByNombre("topeMaximoTC");
                    RepoTC.Update(tipoCabNuevo);
                   
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                    ViewBag.msg = e.Message;
                    return View();
                }

            }else
            {
                ViewBag.msg = "No se pudo actualizar";
                return View();
            }
        
        }


    }
}
