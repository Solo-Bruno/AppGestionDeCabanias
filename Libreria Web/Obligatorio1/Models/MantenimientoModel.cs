using System.ComponentModel.DataAnnotations;

namespace Libreria.Web.Models
{
    public class MantenimientoModel
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Costo { get; set; }
        [Required]
        public string NombreUs { get; set; }
        public int? CabaniaId { get; set; }
    }
}