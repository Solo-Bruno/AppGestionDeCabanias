using System.ComponentModel.DataAnnotations;

namespace Libreria.Web.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }
        public string Token { get; set; }
    }
}
