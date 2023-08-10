using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto
{
    public class CabaniaVerDto
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
        public List<MantenimientoDto> ListaMant { get; set; }

    }
}
