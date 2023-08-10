using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Auxiliar
{
    public class FiltrosDto
    {
        /// <summary>
        ///  Filtro del nombre.
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        ///  Filtro del Tipo Cabania.
        /// </summary>
        public int? tipoId { get; set; }
        /// <summary>
        ///  Filtro de la cantidad maxima de personas.
        /// </summary>
        public int? cantidad { get; set; }
        /// <summary>
        ///  Filtro si esta habilitada.
        /// </summary>
        public string? hab { get; set; }
    }
}
