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
    public class UpdateTipoCabania: IUpdateTipoCabania
    {
        private IRepositorioTipoCabania _RepoTc;
        private IRepositorioParametros _RepoRP;

        public UpdateTipoCabania(IRepositorioTipoCabania TC, IRepositorioParametros RP)
        {
            _RepoTc = TC; 
            _RepoRP = RP;
        }

        public TipoCabaniaDto FindById(int id)
        {
            try
            {
                var CabBuscada = _RepoTc.FindById(id);
                var ret = MapeoTipoCabania.ToDto(CabBuscada);
                return ret;
            }
            catch (Exception e)
            {

                throw new TipoCabaniaException("No existe ese Tipo Cabania: " + e.Message);
            }
        }

        public void Update(TipoCabaniaDto tipoCabaniaDto)
        {
            try
            {
                var TipoCab = MapeoTipoCabania.FromDto(tipoCabaniaDto);
                TipoCab.topeMaximo = _RepoRP.FindByNombre("topeMaximoTC");
                TipoCab.topeMinimo = _RepoRP.FindByNombre("topeMinimoTC");
                TipoCab.Validar();
                _RepoTc.Update(TipoCab);

            }
            catch (Exception e)
            {

                throw new TipoCabaniaException("No se pudo actualizar la cabania: " + e.Message);
            }
        }
    }
}
