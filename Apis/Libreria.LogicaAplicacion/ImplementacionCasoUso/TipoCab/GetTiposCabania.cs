using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.TipoCab
{
    public class GetTiposCabania : IGetTiposCabania
    {
        private IRepositorioTipoCabania _RepoTC;

        public GetTiposCabania(IRepositorioTipoCabania RTC)
        {
            _RepoTC = RTC;
        }
        public IEnumerable<TipoCabaniaDto> FindAll()
        {
            var lista = _RepoTC.FindAll();
            if (lista == null) throw new TipoCabaniaException("No se encuentran ningun tipo");

            var ret = MapeoTipoCabania.ToListaTiposCabaniaDto(lista);
            return ret;
        }
    }
}
