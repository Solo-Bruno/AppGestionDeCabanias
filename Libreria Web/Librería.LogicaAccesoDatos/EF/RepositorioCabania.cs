using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioCabania : IRepositorioCabania
    {
        private LibreriaContext _db;

        public RepositorioCabania(LibreriaContext DB)
        {
            _db = DB;
        }

        public void Add(Cabania cab)
        {
            if (cab != null)
            {
                try
                {
                   TipoCabania bus = _db.TiposCabanias.Find(cab.TipoId);
                    if (bus != null)
                    {
                        cab.Tipo = bus;
                    }
                    else {

                        throw new Exception("Tipo cabania inexistente");
                    }
                    cab.Validar();
                    _db.Cabanias.Add(cab);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            else
            {
                throw new ArgumentException("La cabania no puede ser null");
            }
        }

        //to do En Irepo mtto
        public int CantidadDeMantenimientos(int? id)
        {
            try
            {
                int ret = 0;
                var cab = _db.Cabanias
                            .Include(Cabania => Cabania.MisMantenimientos)
                            .Include(Cabania => Cabania.Tipo)
                           .Where(Cabania => Cabania.NumeroHabitacion == id)
                           .FirstOrDefault();

                if (cab != null) {
                    foreach (var mant in cab.MisMantenimientos)
                    {
                        if (mant.Fecha.Valor.Day == DateTime.Today.Day)
                        {
                            ret++;
                        }
                    }

                    return ret;

                }else { 
                    throw new Exception("No se encontro la cabania"); 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int ExisteFoto(string nomb)
        {
            var ret = _db.Cabanias.Where(cab => cab.NombreFoto.Substring(0, nomb.Length).Equals(nomb));
            
            return ret.Count();
        }

        public IEnumerable<Cabania> FindAll()
        {
            return _db.Cabanias
                    .Include(Cabania => Cabania.Tipo)
                    .ToList();
        }



        public IEnumerable<Cabania> FindByFiltrado(string? nombre, int? tipoId, int? cantidad, string? hab)
        {
            try
            {
                if (hab == null && cantidad == null && tipoId == null && nombre == null) { 
                    
                    var ret = FindAll();

                    return ret;

                }
                else
                {
                    var busqueda = _db.Cabanias.AsQueryable();

                    if (nombre != null) busqueda = busqueda.Include(cab => cab.Tipo).Where(cab => cab.Nombre.Valor.IndexOf(nombre) != -1);
                    if (tipoId != null) busqueda = busqueda.Include(cab => cab.Tipo).Where(cab => cab.TipoId == tipoId);
                    if (cantidad != null) busqueda = busqueda.Include(cab => cab.Tipo).Where(cab => cab.CantMaxPers.CantMaxPers >= cantidad);
                    if (hab != null) busqueda = busqueda.Include(cab => cab.Tipo).Where(cab => cab.TieneReservas == false);


                    return busqueda;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cabania FindById(int id)
        {
            try
            {
                var ret = _db.Cabanias
                    .Include(cab => cab.MisMantenimientos)
                    .SingleOrDefault(cab => cab.NumeroHabitacion == id);
                           
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cabania FindById(int? id)
        {
            try
            {
                var ret = _db.Cabanias
                  .Include(cab => cab.MisMantenimientos)
                  .SingleOrDefault(cab => cab.NumeroHabitacion == id);
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cabania FindByName(string nombre)
        {
            try
            {
                return _db.Cabanias
                       .Include(cab => cab.Tipo)
                       .FirstOrDefault(cab => cab.Nombre.Equals(nombre));
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void GuardarImg(Cabania cab, IFormFile img, string ruta)
        {
            if (img == null || cab == null) throw new ArgumentException("Img null");
            // SUBIR LA IMAGEN
            //ruta física de wwwroot
            //todo imagen requerida

            string nombreImagen = img.FileName;
          
            //Sustituyo los " " por "_"

            nombreImagen = nombreImagen.Replace(" ", "_");

            // Busco si hay otra img que se llame asi

             
            int indice = nombreImagen.IndexOf(".");
            string nombreBuscar = nombreImagen.Substring(0 ,indice);
            int cantidadDeFotos = ExisteFoto(nombreBuscar);
            nombreImagen = nombreImagen.Insert(indice, "0" + cantidadDeFotos.ToString() );
            

           

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
                    cab.Validar();
                    _db.Entry(cab).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();

                }
                catch (Exception)
                {
                    RemoveById(cab.NumeroHabitacion);
                    throw;
                }

            
            
            
          
        }

        public void Remove(Cabania tipo)
        {
            try
            {
                _db.Cabanias.Remove(tipo);
                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveById(int id)
        {
            try
            {
                Cabania ret = FindById(id);
                if (ret != null)
                {
                    Remove(ret);
                }
                else {
                    throw new ArgumentException("No existe esa id");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Cabania tipo)
        {
            throw new NotImplementedException();
        }
    }
}
