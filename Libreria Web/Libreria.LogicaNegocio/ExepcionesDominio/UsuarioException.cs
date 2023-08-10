using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.ExepcionesDominio
{
    public class UsuarioException : Exception
    {
        public UsuarioException() { }
        public UsuarioException(string message) : base(message)
        {
        }
    }
}
