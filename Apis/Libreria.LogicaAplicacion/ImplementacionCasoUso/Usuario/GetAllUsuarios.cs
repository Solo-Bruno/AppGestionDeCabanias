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
    public class GetAllUsuarios : IGetAllUsuarios
    {
        private IRepositorioUsuario _ru;

        public GetAllUsuarios(IRepositorioUsuario repositorioUsuario)
        {
            _ru = repositorioUsuario;
        }
        public IEnumerable<UsuarioDto> TraerUsuarios()
        {
            try
            {
                var usuarios = _ru.FindAll();

                var ret = MapeoUsuario.TolistUsuarioDto(usuarios);

                return ret;
            }
            catch (Exception)
            {

                throw new UsuarioException("No se pudieron encontrar los usuarios");
            }
        }
    }
}
