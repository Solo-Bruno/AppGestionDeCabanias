using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Entidades.ValueObject.Cabania;
using Libreria.LogicaNegocio.ExepcionesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Mapeo
{
    public class MapeoCabania
    {
        internal static CabaniaDto ToDto(Cabania cabania) {

            return new CabaniaDto() {

                NumeroHabitacion = cabania.NumeroHabitacion,
                Nombre = cabania.Nombre.Valor,
                Descripcion = cabania.Descripcion,
                TieneJacuzzi = cabania.TieneJacuzzi,
                TieneReservas = cabania.TieneReservas,
                CantMaxPers = cabania.CantMaxPers.CantMaxPers,
                NombreFoto = cabania.NombreFoto,
                TipoId = cabania.TipoId,
                
            };
        }

        internal static Cabania FromDto(CabaniaDto cabaniaDto) {
            return new Cabania() { 
                NumeroHabitacion = cabaniaDto.NumeroHabitacion,
                Nombre = new NombreCabania(cabaniaDto.Nombre),
                Descripcion = cabaniaDto.Descripcion,
                TieneJacuzzi = cabaniaDto.TieneJacuzzi,
                TieneReservas = cabaniaDto.TieneReservas,
                CantMaxPers = new CantMaxPersonasCabania(cabaniaDto.CantMaxPers),
                NombreFoto = cabaniaDto.NombreFoto,
                TipoId = cabaniaDto.TipoId,
              
            };
            
        }

        internal static IEnumerable<CabaniaDto> listCabaniaDto(IEnumerable<Cabania> cabanias) { 
            
            var ret = cabanias.Select(c => ToDto(c)).ToList();
            return ret;
        }

        

    }
}
