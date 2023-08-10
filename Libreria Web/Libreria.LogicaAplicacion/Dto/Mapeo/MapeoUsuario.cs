using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Mapeo
{
    public class MapeoUsuario
    {
        internal static UsuarioDto ToDto(Usuario usuario) {

            return new UsuarioDto() {
                
                Email = usuario.Email,
                Pass = usuario.Pass,
                Id = usuario.Id,
            
            };
        }

        internal static Usuario FromDto(UsuarioDto usuarioDto)
        {
            return new Usuario()
            {
                Email = usuarioDto.Email,
                Pass = usuarioDto.Pass,
                Id = usuarioDto.Id,
            };

        }

        internal static IEnumerable<UsuarioDto> TolistUsuarioDto(IEnumerable<Usuario> usuarios) {
        
            var ret = usuarios.Select(us => ToDto(us)).ToList();

            return ret;
        }
    }
}
