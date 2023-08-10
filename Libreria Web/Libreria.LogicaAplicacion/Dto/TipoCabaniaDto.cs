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
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int CostoHuesped { get; set; }
    }
}
