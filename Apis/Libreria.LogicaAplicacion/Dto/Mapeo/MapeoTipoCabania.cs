using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Entidades.ValueObject.TipoCabania;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Mapeo
{
    public class MapeoTipoCabania
    {
        internal static TipoCabaniaDto ToDto(TipoCabania tipoCabania) {

            return new TipoCabaniaDto() {
                Nombre = tipoCabania.Nombre.ValorNombre,
                Descripcion = tipoCabania.Descripcion,
                CostoHuesped = tipoCabania.CostoHuesped,
                Id = tipoCabania.Id,
            };

        }

        internal static TipoCabania FromDto(TipoCabaniaDto tipoCabaniaDto) {

            return new TipoCabania() { 
                
                Nombre = new NombreTipoCabania(tipoCabaniaDto.Nombre),
                Descripcion = tipoCabaniaDto.Descripcion,
                CostoHuesped = tipoCabaniaDto.CostoHuesped,
                Id = tipoCabaniaDto.Id,
            };
        
        }

        internal static IEnumerable<TipoCabaniaDto> ToListaTiposCabaniaDto(IEnumerable<TipoCabania> tiposCabanias) { 
            
            var lista = tiposCabanias.Select(tc => ToDto(tc)).ToList(); 
            return lista;
        }
    }
}
