using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.ExepcionesDominio
{
    public class ParametroException : Exception
    {
        public ParametroException() { }
        public ParametroException(string message) : base(message)
        {
        }
    }
}
