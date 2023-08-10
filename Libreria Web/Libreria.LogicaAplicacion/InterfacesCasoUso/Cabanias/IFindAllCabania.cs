using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Cabania
{
    public interface IFindAllCabania
    {
        public IEnumerable<CabaniaDto> FindAll();
    }
}
