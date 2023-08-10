using System.ComponentModel.DataAnnotations;

namespace Libreria.Web.Models
{
    public class CabaniaModel
    {
        public int NumeroHabitacion { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool TieneJacuzzi { get; set; }
        public bool TieneReservas { get; set; }
        [Required]
        public int CantMaxPers { get; set; }

        public string? NombreFoto { get; set; }
        public int TipoId { get; set; }
       
    }
}
