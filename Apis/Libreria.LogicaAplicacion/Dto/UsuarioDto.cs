using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto
{
    public class UsuarioDto
    {
        /// <summary>
        ///  Id del usuario.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  Email del Usuario.
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        ///  Contrasenia del Usuario.
        /// </summary>
        [Required]
        public string Pass { get; set; }
        /// <summary>
        ///  Token del Usuario.
        /// </summary>
        public string Token { get; set; }

    }
}
