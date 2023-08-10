using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Auxiliar
{
    public class MttoGrupBy
    {
        /// <summary>
        ///  Nombre del Empleado que realizo el Mantenimineto.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        ///  Monto total de sus Manteniminetos realizados.
        /// </summary>
        public int Total { get; set; }
    }
}
