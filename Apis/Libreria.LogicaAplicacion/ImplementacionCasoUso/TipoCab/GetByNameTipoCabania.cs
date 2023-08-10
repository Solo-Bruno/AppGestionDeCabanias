using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.TipoCab
{
    public class GetByNameTipoCabania: IGetByNameTipoCabania
    {
        private IRepositorioTipoCabania _RepoTc;

        public GetByNameTipoCabania(IRepositorioTipoCabania TC)
        {
            _RepoTc = TC;
        }

        public TipoCabaniaDto FindByName(string nombre)
        {
            try
            {
                var CabBuscada = _RepoTc.FindByName(nombre);
                var ret = MapeoTipoCabania.ToDto(CabBuscada);
                return ret;
            }
            catch (Exception e)
            {

                throw new TipoCabaniaException("No se encontro la cabania: " + e.Message);
            }
        }
    }
}
