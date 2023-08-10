using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Auxiliar
{
    public class ValoresBusquedaFechaDto
    {
        /// <summary>
        ///  Filtro rango fecha 1.
        /// </summary>
        public DateTime fch1 { get; set; }
        /// <summary>
        ///  Filtro rango fecha 2.
        /// </summary>
        public DateTime fch2 { get; set; }
        /// <summary>
        ///  Id de la Cabania.
        /// </summary>
        public int? id { get; set; }
    }
}
