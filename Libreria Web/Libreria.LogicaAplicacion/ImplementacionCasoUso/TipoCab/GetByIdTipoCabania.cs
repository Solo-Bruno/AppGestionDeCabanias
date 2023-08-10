using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab
{
    public class GetByIdTipoCabania: IGetByIdTipoCabania
    {
        private IRepositorioTipoCabania _RepoTc;


        public GetByIdTipoCabania(IRepositorioTipoCabania TC)
        {
            _RepoTc = TC;
 
        }


        public TipoCabaniaDto FindById(int Id)
        {
            try
            {
                var CabBuscada = _RepoTc.FindById(Id);
                var ret = MapeoTipoCabania.ToDto(CabBuscada);
                return ret;
            }
            catch (Exception e)
            {

                throw new TipoCabaniaException("No existe ese Tipo Cabania: " + e.Message);
            }
        }
    }
}
