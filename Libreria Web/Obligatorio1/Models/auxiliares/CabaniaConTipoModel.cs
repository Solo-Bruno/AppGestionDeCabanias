using System.ComponentModel.DataAnnotations;

namespace Libreria.Web.Models.auxiliares
{
    public class CabaniaConTipoModel
    {
        public int NumeroHabitacion { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool TieneJacuzzi { get; set; }
        public bool TieneReservas { get; set; }

        public int CantMaxPers { get; set; }

        public string? NombreFoto { get; set; }
        public int TipoId { get; set; }
        public TipoCabaniaModel TipoCabania { get; set; }
    }
}
