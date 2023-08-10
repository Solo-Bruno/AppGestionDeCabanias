using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias
{
    public interface IFindByIdCabania
    {
        public CabaniaDto FindById(int id);
    }
}
