using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Librería.LogicaAccesoDatos.EF;
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

        public void AddMtto(Mantenimiento mantenimiento, Cabania cab)
        {
            if (mantenimiento != null)
            {
                try
                {
                    mantenimiento.CabaniaId = cab.NumeroHabitacion;
                    mantenimiento.Fecha.Valor = DateTime.Now;
                    mantenimiento.Id = 0;
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

        public IEnumerable<Mantenimiento> FindByCabania(Cabania cab)
        {
            throw new NotImplementedException();
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
    }
}
