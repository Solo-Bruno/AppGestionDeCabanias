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
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }

    }
}
