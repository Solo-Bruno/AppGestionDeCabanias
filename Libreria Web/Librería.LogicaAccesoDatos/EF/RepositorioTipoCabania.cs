using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioTipoCabania : IRepositorioTipoCabania
    {
        private LibreriaContext _db;

        public RepositorioTipoCabania(LibreriaContext DB)
        {
            _db = DB;
        }
        public void Add(TipoCabania tipo)
        {
            if (tipo != null)
            {
                try
                {
                    tipo.Validar();
                    _db.TiposCabanias.Add(tipo);
                    _db.SaveChanges();
                    
                }
                catch (Exception e)
                {

                    throw ;
                }
            }
            else
            {
                throw new ArgumentException("No se encontro el tipo");
            }
        }

        public IEnumerable<TipoCabania> FindAll()
        {
            return _db.TiposCabanias.ToList();
        }

        public TipoCabania FindById(int id)
        {
            TipoCabania aux = _db.TiposCabanias.Find(id);
            return aux;
        }

        public TipoCabania FindByName(string nombre)
        {

            try
            {
                TipoCabania ret = _db.TiposCabanias.FirstOrDefault(tipo => tipo.Nombre.ValorNombre.IndexOf(nombre) != -1);
                                       
                return ret;
            }
            catch
            {
                throw new ArgumentException("No existe el nombre ingresado");
            }

        }

        public void Remove(TipoCabania tipo)
        {
            bool TieneCabania = _db.Cabanias.FirstOrDefault(Cabania => Cabania.Tipo.Equals(tipo)) != null;
            try
            {
                if (tipo != null)
                {
                    if (!TieneCabania)
                    {
                        _db.TiposCabanias.Remove(tipo);
                        _db.SaveChanges();
                    }
                    else {
                        throw new ArgumentException("Este tipo ya tiene un o mas Cabanias");
                    }
                }
                else
                {
                    throw new ArgumentException("El tipo es nulo");
                }
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
                TipoCabania aux = FindById(id);
                Remove(aux);
                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw ;
            }
        }

        public void RemoveByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var tipo = FindByName(name);
                    if (tipo != null)
                    {
                        Remove(tipo);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else {
                throw new ArgumentException("El nombre no pude estar vacio");
            }
        }

        public void Update(TipoCabania tipo)
        {
            if (tipo != null)
            {
                try
                {
                    tipo.Validar();
                    TipoCabania aux = FindById(tipo.Id);
                    aux.Descripcion = tipo.Descripcion;
                    aux.CostoHuesped = tipo.CostoHuesped;
                    _db.Entry(aux).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
