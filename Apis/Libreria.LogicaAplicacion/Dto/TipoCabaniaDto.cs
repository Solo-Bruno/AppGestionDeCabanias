using Libreria.LogicaNegocio.Entidades.ValueObject.TipoCabania;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto
{
    public class TipoCabaniaDto
    {
        /// <summary>
        /// Id Tipo Cabania.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Tipo Cabania.
        /// </summary>
        [Required]
        public string Nombre { get; set; }
        /// <summary>
        /// Descripcion del Tipo Cabania.
        /// </summary>
        [Required]
        public string Descripcion { get; set; }
        /// <summary>
        /// Costo del Tipo Cabania.
        /// </summary>
        [Required]
        public int CostoHuesped { get; set; }
    }
}
