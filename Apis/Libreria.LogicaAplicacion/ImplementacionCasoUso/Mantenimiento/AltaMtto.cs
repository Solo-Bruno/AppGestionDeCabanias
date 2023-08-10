using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.Mantenimiento
{
    public class AltaMtto : IAltaMtto
    {
        private IRepositorioMantenimiento _repoMT;
        private IRepositorioCabania _repoRC;
        private IRepositorioParametros _repoPA;

        public AltaMtto(IRepositorioMantenimiento RM, IRepositorioCabania RC, IRepositorioParametros RP)
        {
            _repoMT = RM;
            _repoRC = RC;
            _repoPA = RP;
        }

        public void Ejecutar(MantenimientoDto mantenimientoDto)
        {
            try
            {
                if (mantenimientoDto == null) { throw new TipoCabaniaException("El tipo no puede ser null"); }
                var mant = MapeoMantenimiento.FromDto(mantenimientoDto);

                var cantMant = _repoRC.CantidadDeMantenimientos(mant.CabaniaId);
                if (cantMant >= 3) { throw new TipoCabaniaException("Ya tiene mas de tres mantenimientos hoy"); }
                mant.Cabania = _repoRC.FindById(mant.CabaniaId);

                mant.topeMaximoMtto = _repoPA.FindByNombre("topeMaximoMtto");
                mant.topeMinimoMtto = _repoPA.FindByNombre("topeMinimoMtto");

                mant.Fecha.Valor = DateTime.Now;
                mant.Id = 0;
                mant.Validar();
                _repoMT.AddMtto(mant);
               
            }
            catch (Exception ex)
            {

                throw new ArgumentException("No se pudo hacer el alta:" + ex.Message);
            }
        }
    }
}
