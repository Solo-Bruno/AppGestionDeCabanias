using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Usuario;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.Usuario
{
    public class FindByNameYPassUsuario: IFindByNameYPassUsuario
    {
        private IRepositorioUsuario _RepoUs;

        public FindByNameYPassUsuario(IRepositorioUsuario RU)
        {
            _RepoUs = RU;
        }

        public UsuarioDto FindByName(string email, string pass)
        {
            try
            {
                var usuario = _RepoUs.FindByName(email, pass);
                var UsuarioDto = MapeoUsuario.ToDto(usuario);
                return UsuarioDto;
            }
            catch (Exception e)
            {
                throw new UsuarioException("No se encontro ese usuario: " + e.Message);
            }
        }
    }
}
