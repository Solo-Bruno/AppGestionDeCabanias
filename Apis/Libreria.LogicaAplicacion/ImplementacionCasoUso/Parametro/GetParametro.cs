using Libreria.LogicaAplicacion.InterfacesCasoUso.Parametro;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.Parametro
{
    public class GetParametro : IGetParametro
    {
        private IRepositorioParametros _RepoParametros;

        public GetParametro(IRepositorioParametros RP)
        {
            _RepoParametros = RP;
        }
        public int FindByNombre(string? nombre)
        {
            try
            {
                return _RepoParametros.FindByNombre(nombre);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se encontro el parametro: " + ex.Message);
            }
        }
    }
}
