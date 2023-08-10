using Libreria.LogicaAplicacion.Dto.Auxiliar;
using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Mapeo
{
    public class MapeoMttoGroupBy
    {
        internal static MttoGrupBy mttoGrupBy(Mantenimiento mantenimiento)
        {
            return new MttoGrupBy() {

                Nombre = mantenimiento.NombreUs,
                Total = mantenimiento.Costo
            };

        }

        internal static IEnumerable<MttoGrupBy> listMttoGroup(IEnumerable<Mantenimiento> mantenimientos) {
            
            var ret = mantenimientos.Select(s => mttoGrupBy(s)).ToList();

            return ret;
        }

    }
}
