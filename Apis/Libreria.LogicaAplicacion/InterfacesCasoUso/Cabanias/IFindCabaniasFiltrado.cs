using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias
{
    public interface IFindCabaniasFiltrado
    {
        public IEnumerable<CabaniaDto> ObtenerCabaniasFiltro(int? tipo, int monto);
    }
}
