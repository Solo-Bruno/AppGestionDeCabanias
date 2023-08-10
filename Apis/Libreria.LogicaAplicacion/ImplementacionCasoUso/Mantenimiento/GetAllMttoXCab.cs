using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.Mantenimiento
{
    public class GetAllMttoXCab : IGetAllMttoXCab
    {
        private IRepositorioMantenimiento _repMtto;

        public GetAllMttoXCab(IRepositorioMantenimiento RM)
        {
            _repMtto = RM;
        }
        public IEnumerable<MantenimientoDto> Ejecutar(int id)
        {
            try
            {
                var lista = _repMtto.FindByCabania(id);

                if (lista == null) { throw new MantenimientoException("No se encontro ninguna cabania"); }

                var ret = MapeoMantenimiento.listMantenimientoDto(lista);

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
