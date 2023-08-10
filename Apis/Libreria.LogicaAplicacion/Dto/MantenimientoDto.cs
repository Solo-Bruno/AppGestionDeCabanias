using System.ComponentModel.DataAnnotations;

namespace Libreria.LogicaAplicacion.Dto
{
    public class MantenimientoDto
    {
        /// <summary>
        /// Id Mantenimiento.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fecha del Mantenimiento.
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Descripcion del Mantenimiento.
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        ///Costo del Mantenimiento.
        /// </summary>
        public int Costo { get; set; }
        /// <summary>
        ///Nombre de la persona que realiza el Mantenimiento.
        /// </summary>
        [Required]
        public string NombreUs { get; set; }
        /// <summary>
        ///id de la cabania a la que le pertenese el Mantenimineto.
        /// </summary>
        public int CabaniaId { get; set; }
    }
}