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
    public class GetCabaniaPorFiltros : IFindByFiltroCabania
    {
        private IRepositorioCabania _RepoCab;

        public GetCabaniaPorFiltros(IRepositorioCabania repositorioCabania)
        {
            _RepoCab = repositorioCabania;
        }
        public IEnumerable<CabaniaDto> FindByFiltros(string? nombre, int? tipoId, int? cantidad, string? hab)
        {
            try
            {
                var lista = _RepoCab.FindByFiltrado(nombre, tipoId, cantidad, hab);

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
