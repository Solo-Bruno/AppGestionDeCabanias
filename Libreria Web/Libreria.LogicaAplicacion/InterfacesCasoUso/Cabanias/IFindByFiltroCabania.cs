using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Cabania
{
    public interface IFindByFiltroCabania
    {
        public IEnumerable<CabaniaDto> FindByFiltros(string? nombre, int? tipoId, int? cantidad, string? hab); 
    }
}
