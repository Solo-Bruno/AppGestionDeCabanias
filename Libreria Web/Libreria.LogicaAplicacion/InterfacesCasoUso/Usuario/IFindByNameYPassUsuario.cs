using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Usuario
{
    public interface IFindByNameYPassUsuario
    {
        public UsuarioDto FindByName(string email,string pass);
    }
}
