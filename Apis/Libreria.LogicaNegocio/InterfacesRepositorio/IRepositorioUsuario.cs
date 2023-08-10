using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuario: IRepositorio<Usuario>
    {
        Usuario FindByName(string email, string pass);
    }
}
