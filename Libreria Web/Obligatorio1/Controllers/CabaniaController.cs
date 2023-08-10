
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Obligatorio1.Controllers;
using System.Text.Json;
using System.Net.Http.Headers;
using Libreria.Web.Models;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using Libreria.Web.Models.auxiliares;

namespace Libreria.Web.Controllers
{
    public class CabaniaController : Controller
    {
        private IWebHostEnvironment _environment;

        private readonly ILogger<TipoCabaniaController> _logger;
        private static HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7296/api") };


        private Uri _urlCabania = new Uri($"{_httpClient.BaseAddress}/Cabania");

        private Uri _urlTipoCab = new Uri($"{_httpClient.BaseAddress}/TipoCabania");



        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };


        public CabaniaController(ILogger<TipoCabaniaController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _environment = environment;
        }


        //// GET: CabaniaController
        public ActionResult Index()
        {
            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                try
                {                   
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);
                    var response = _httpClient.GetAsync(_urlTipoCab).Result;

                    var jsonTipoCab = response.Content.ReadAsStringAsync().Result;

                    var listaTipos = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(jsonTipoCab, opciones);

                    ViewBag.Tipos = listaTipos;

                    var respuesta = _httpClient.GetAsync( _urlCabania).Result;

                    if (respuesta.IsSuccessStatusCode) { 
                        
                        var json = respuesta.Content.ReadAsStringAsync().Result;

                        var listaCabanias = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(json, opciones);

                        var cabConTipo = cabaniaConTipoModels(listaCabanias, listaTipos);
                        return View(cabConTipo);
                    }

                    return RedirectToAction("Index", "Home");
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

        public IActionResult Index(int? cantPer, string? hab, string? nomCab, int? tipoId)
        {


           FiltrosModel filtros = new FiltrosModel() { 
                    nombre = nomCab,
                    cantidad= cantPer,
                    tipoId= tipoId,
                    hab = hab,
           };


            string usuario = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);

            string filtrosSerealizados = System.Text.Json.JsonSerializer.Serialize(filtros);

            var body = new StringContent(filtrosSerealizados, Encoding.UTF8, "application/json");
            try
            {
                var respuesta = _httpClient.PostAsync($"{_urlCabania}/filtros", body).Result;

                ///////////////////////////////////////////////////
                var response = _httpClient.GetAsync(_urlTipoCab).Result;

                var jsonTipoCab = response.Content.ReadAsStringAsync().Result;

                var listaTipos = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(jsonTipoCab, opciones);

                ViewBag.Tipos = listaTipos;
                ////////////////////////////////////////////////////////////
                
                if (respuesta.IsSuccessStatusCode)
                {

                    var json = respuesta.Content.ReadAsStringAsync().Result;
                    var cabanias = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(json, opciones);

                    var cabConTipo = cabaniaConTipoModels(cabanias, listaTipos);
                   return View(cabConTipo);
                }

                ViewBag.msg = "No se econtro ninguna Cabania";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }

        }

        //// GET: CabaniaController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: CabaniaController/Create
        public IActionResult Create()
        {
            string? user = HttpContext.Session.GetString("usuario");

            if (user != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);
                var response = _httpClient.GetAsync(_urlTipoCab).Result;

                var json = response.Content.ReadAsStringAsync().Result;

                var listaTipos = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(json, opciones);

                if (listaTipos != null)
                {
                    ViewBag.listaTipos = listaTipos;
                }

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // POST: CabaniaController/Create
        [HttpPost]
        public IActionResult Create(CabaniaModel cab, IFormFile imagen)
        {
            string usuario = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario);

            // Lista desplegable de tipos cabania
            var response = _httpClient.GetAsync(_urlTipoCab).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            var listaTipos = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(json, opciones);

            if (listaTipos != null)
            {
                ViewBag.listaTipos = listaTipos;
            }

            string ruta = _environment.WebRootPath;

           



            if (cab != null &&  imagen != null) {

                try
                {


                    GuardarImg(cab, imagen, ruta);

                    var jsonCab = System.Text.Json.JsonSerializer.Serialize(cab);

                    var body = new StringContent(jsonCab, Encoding.UTF8, "application/json"); 

                    var respuesta = _httpClient.PostAsync(_urlCabania, body).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }

                    ViewBag.msg = "Ocurrio un error";
                    return View();
                }
                catch (Exception e)
                {
                    ViewBag.msg = e.Message;
                    return View();
                }


            }
            ViewBag.msg = "Falta la Imagen";
            return View();


        }



        //Metodo Auxiliar
        private IEnumerable<CabaniaConTipoModel> cabaniaConTipoModels(IEnumerable<CabaniaModel> cabanias, IEnumerable<TipoCabaniaModel> tipos) {
        
            var ret = new List<CabaniaConTipoModel>();

            foreach (var item in cabanias)
            {
                var cabConTipo = MapeoCabaniaConTipo.ToCabConTip(item);
                cabConTipo.TipoCabania = tipos.FirstOrDefault(tip => tip.Id == item.TipoId);
                ret.Add(cabConTipo);
            }

            return ret;
        }

        private void GuardarImg(CabaniaModel cab, IFormFile img, string ruta)
        {
            if (img == null || cab == null) throw new ArgumentException("Img null");
            // SUBIR LA IMAGEN
            //ruta física de wwwroot
            //todo imagen requerida

            string nombreImagen = img.FileName;

            //Sustituyo los " " por "_"

            nombreImagen = nombreImagen.Replace(" ", "_");

            // Busco si hay otra img que se llame asi
            var respuesta = _httpClient.GetAsync(_urlCabania).Result;
            var json = respuesta.Content.ReadAsStringAsync().Result;
            var listaCabanias = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(json, opciones);


            int indice = nombreImagen.IndexOf(".");
            string nombreBuscar = nombreImagen.Substring(0, indice);
            var listcant = listaCabanias.Where(c => c.NombreFoto.Substring(0, (c.NombreFoto.Length - 4)).Equals(nombreBuscar)).ToList();
            int cantidadDeFotos = listcant.Count();
            nombreImagen = nombreImagen.Insert(indice, "0" + cantidadDeFotos.ToString());




            //ruta donde se guardan las fotos de las personas
            string rutaFisicaFoto = Path.Combine
            (ruta, "imagen", "fotos", nombreImagen);
            //FileStream permite manejar archivos
            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar
                using (FileStream f = new FileStream(rutaFisicaFoto, FileMode.Create))
                {
                    //Para archivos grandes o varios archivos usar la versión
                    //asincrónica de CopyTo. Sería: await imagen.CopyToAsync (f);
                    img.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO
                cab.NombreFoto = nombreImagen;
                

            }
            catch (Exception)
            {
                throw;
            }


        }

        public IActionResult FiltradoCabanias()
        {
            string? user = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);
            var response = _httpClient.GetAsync(_urlTipoCab).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            var listaTipos = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(json, opciones);

            if (listaTipos != null)
            {
                ViewBag.listaTipos = listaTipos;
            }

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

        public IActionResult FiltradoCabanias(int? tipoId, int monto)
        {

            string? user = HttpContext.Session.GetString("usuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user);
            var response = _httpClient.GetAsync(_urlTipoCab).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            var listaTipos = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TipoCabaniaModel>>(json, opciones);

            if (listaTipos != null)
            {
                ViewBag.listaTipos = listaTipos;
            }

            FiltradoCabania filtros = new FiltradoCabania()
            {

                tipoId = tipoId,
                monto = monto

            };


           

            string filtrosSerealizados = System.Text.Json.JsonSerializer.Serialize(filtros);

            var body = new StringContent(filtrosSerealizados, Encoding.UTF8, "application/json");
            try
            {
                var respuesta = _httpClient.PostAsync($"{_urlCabania}/filtradoCabania", body).Result;



                if (respuesta.IsSuccessStatusCode)
                {
                    var listaDes = respuesta.Content.ReadAsStringAsync().Result;
                    var lista = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(listaDes, opciones);
                    return View(lista);
                }

                ViewBag.msg = "No se econtro ninguna Cabania";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }

        }
    }
}
