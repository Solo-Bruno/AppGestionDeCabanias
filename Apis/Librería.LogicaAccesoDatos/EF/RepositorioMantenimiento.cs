using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaAplicacion.Dto.Auxiliar;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioMantenimiento : IRepositorioMantenimiento
    {
        private LibreriaContext _db;

        public RepositorioMantenimiento(LibreriaContext DB)
        {
            _db = DB;
        }
        public void Add(Mantenimiento tipo)
        {
            throw new NotImplementedException();
        }

        public void AddMtto(Mantenimiento mantenimiento)
        {
            if (mantenimiento != null)
            {
                try
                {
                    mantenimiento.Validar();
                    _db.Mantenimientos.Add(mantenimiento);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e.InnerException;
                }

            }
            else
            {
                throw new ArgumentException("Mantenimiento no valido");
            }
        }

        public IEnumerable<Mantenimiento> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mantenimiento> FindByCabania(int id)
        {
            try
            {
                var list = _db.Mantenimientos.Where(m => m.CabaniaId == id).ToList();

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Mantenimiento> FindByFechas(DateTime fch1, DateTime fch2, int? id)
        {
            try
            {

                return _db.Mantenimientos
                     .Where(m => m.Fecha.Valor > fch1 && m.Fecha.Valor < fch2 && m.CabaniaId == id)
                     .OrderByDescending(m => m.Costo)
                     .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Mantenimiento FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Mantenimiento tipo)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mantenimiento tipo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mantenimiento> ManteniminetosEntrValores(int top1 , int top2)
        {

            var ret = _db.Mantenimientos
                        .Where(mant => mant.Cabania.CantMaxPers.CantMaxPers > top1 && mant.Cabania.CantMaxPers.CantMaxPers < top2)
                        .GroupBy(m => m.NombreUs).
                        Select(g => new { 
                        
                            NombreUs = g.Key,
                            Total = g.Sum(m => m.Costo)
                        
                        }).ToList();


            List<Mantenimiento> mttoRet = new List<Mantenimiento> ();
            foreach (var item in ret)
            {
               var nuevo = new Mantenimiento()
                {
                    NombreUs = item.NombreUs,
                    Costo = item.Total,

                };

                mttoRet.Add(nuevo);
            }

            return mttoRet;
                            
                        
                       

        }


    }
}
