using System.ComponentModel.DataAnnotations;

namespace Libreria.LogicaAplicacion.Dto
{
    public class MantenimientoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Costo { get; set; }
        [Required]
        public string NombreUs { get; set; }
        public int CabaniaId { get; set; }
    }
}