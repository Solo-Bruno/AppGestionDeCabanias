using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab
{
    public interface IGetTiposCabania
    {
        public IEnumerable<TipoCabaniaDto> FindAll();
    }
}
