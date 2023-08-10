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
    public class RepositorioParametro : IRepositorioParametros
    {
        private LibreriaContext _db;

        public RepositorioParametro(LibreriaContext DB)
        {
            _db = DB;
        }
        public void Add(Parametro parametro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parametro> FindAll()
        {
            throw new NotImplementedException();
        }

        public Parametro FindById(int id)
        {
            throw new NotImplementedException();
        }

        public int FindByNombre(string? nombre)
        {
            try
            {
                var ret = _db.Parametros.FirstOrDefault(pa => pa.NombreParametro.Equals(nombre));

                if (ret == null) {
                    throw new Exception("No se encontro");
                }
                else
                {
                    return ret.ValorDelParametro;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Remove(Parametro tipo)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Parametro tipo)
        {
            throw new NotImplementedException();
        }
    }
}
