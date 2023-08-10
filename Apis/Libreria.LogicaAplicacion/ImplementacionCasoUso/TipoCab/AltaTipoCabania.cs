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
    public class AltaTipoCabania : IAltaTipoCabania
    {
        private IRepositorioTipoCabania _repoTC;
        private IRepositorioParametros _repoRP;

        public AltaTipoCabania(IRepositorioTipoCabania RTC, IRepositorioParametros RP)
        {
            _repoTC = RTC;
            _repoRP = RP;
        }
        public void Ejecutar(TipoCabaniaDto tipoCabaniaDto)
        {
            try
            {
                if (tipoCabaniaDto == null) { throw new TipoCabaniaException("El tipo no puede ser null"); }
                TipoCabania nuevoTC = MapeoTipoCabania.FromDto(tipoCabaniaDto);
                nuevoTC.topeMaximo = _repoRP.FindByNombre("topeMaximoTC");
                nuevoTC.topeMinimo = _repoRP.FindByNombre("topeMinimoTC");
                nuevoTC.Validar();
                _repoTC.Add(nuevoTC);
                tipoCabaniaDto.Id = nuevoTC.Id;
            }
            catch (Exception ex)
            {

                throw new ArgumentException("No se pudo validar:" + ex.Message);
            }
        }
    }
}
