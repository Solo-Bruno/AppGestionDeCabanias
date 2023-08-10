using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Parametro
{
    public interface IGetParametro
    {
        public int FindByNombre(string? nombre);
    }
}
