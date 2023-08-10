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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento
{
    public class FindByFechas : IFindByFechas
    {

        private IRepositorioMantenimiento _repoMT;


        public FindByFechas(IRepositorioMantenimiento RM)
        {
            _repoMT = RM;

        }

        public IEnumerable<MantenimientoDto> ObtenerPorFechas(DateTime fch1, DateTime fch2, int? id)
        {
            try
            {
                var mant = _repoMT.FindByFechas(fch1, fch2, id);
                var ret = MapeoMantenimiento.listMantenimientoDto(mant);
                return ret;
            }
            catch (Exception e)
            {

                throw new TipoCabaniaException("No se encontro ninguno: " + e.Message);
            }



        }

       
    }
}
