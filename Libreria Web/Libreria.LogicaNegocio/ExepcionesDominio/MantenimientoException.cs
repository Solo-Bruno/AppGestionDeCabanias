using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.ExepcionesDominio
{
    public class MantenimientoException : Exception
    {
        public MantenimientoException() { }
        public MantenimientoException(string message) : base(message)
        {
        }

    }
}
