

using System.ComponentModel.DataAnnotations;

namespace Libreria.Web.Models
{
    public class TipoCabaniaModel
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int CostoHuesped { get; set; }
    }
}
