using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabania;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.CabaniasCar
{
    public class FindAllCabania : IFindAllCabania
    {
        private IRepositorioCabania _repoCab;

        public FindAllCabania(IRepositorioCabania RCab)
        {
            _repoCab = RCab;
        }
        public IEnumerable<CabaniaDto> FindAll()
        {
            var lista = _repoCab.FindAll();

            if (lista == null) throw new TipoCabaniaException("No se encuentran ningun tipo");

            var ret = MapeoCabania.listCabaniaDto(lista);

            return ret;
        }
    }
}
