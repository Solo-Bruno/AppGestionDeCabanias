using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabania;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.CabaniasCar
{
    public class FindCabaniasFiltrado : IFindCabaniasFiltrado
    {
        private IRepositorioCabania _RepoCab;

        public FindCabaniasFiltrado(IRepositorioCabania repositorioCabania)
        {
            _RepoCab = repositorioCabania;
        }
       

        public IEnumerable<CabaniaDto> ObtenerCabaniasFiltro(int? tipo, int monto)
        {
            try
            {
                var lista = _RepoCab.ObtenerCabaniasFiltro(tipo, monto);

                if (lista == null) { throw new CabaniaException("No se encuentran ningun tipo"); }

                var ret = MapeoCabania.listCabaniaDto(lista);

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
