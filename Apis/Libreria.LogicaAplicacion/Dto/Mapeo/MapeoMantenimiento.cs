using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Entidades.ValueObject.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Mapeo
{
    public class MapeoMantenimiento
    {
        internal static MantenimientoDto ToDto(Mantenimiento mantenimiento) {

            return new MantenimientoDto() { 
                Id = mantenimiento.Id,
                NombreUs = mantenimiento.NombreUs,
                Fecha = mantenimiento.Fecha.Valor,
                Costo = mantenimiento.Costo,
                Descripcion = mantenimiento.Descripcion,
                CabaniaId = mantenimiento.CabaniaId,
            };
        }

        internal static Mantenimiento FromDto(MantenimientoDto mantenimientoDto) {

            return new Mantenimiento()
            {
                Fecha = new FechaMantenimiento(mantenimientoDto.Fecha),
                NombreUs = mantenimientoDto.NombreUs,
                Costo = mantenimientoDto.Costo,
                Descripcion = mantenimientoDto.Descripcion,
                CabaniaId = mantenimientoDto.CabaniaId,
            };
        }

        internal static IEnumerable<MantenimientoDto> listMantenimientoDto(IEnumerable<Mantenimiento> mantenimientos) {
        
            var ret = mantenimientos.Select(man => ToDto(man)).ToList(); 
            return ret;
        }

    }
}
