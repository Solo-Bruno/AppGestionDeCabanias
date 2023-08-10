using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.ExepcionesDominio
{
    public class CabaniaException : Exception
    {
        public CabaniaException() { }
        public CabaniaException(string message) : base(message)
        {
        }
    }
}
