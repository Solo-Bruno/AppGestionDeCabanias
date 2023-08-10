using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.ExepcionesDominio
{
    public class TipoCabaniaException : Exception
    {
        public TipoCabaniaException() { }
        public TipoCabaniaException(string message) : base(message)
        {
        }
    }
}
