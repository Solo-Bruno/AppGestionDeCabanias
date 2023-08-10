using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dto.Auxiliar
{
    public class FiltroCabaniasDto
    {
        /// <summary>
        ///  Tipo Cabania por el cual filtrar.
        /// </summary>
        public int? tipoId { get; set; }
        /// <summary>
        ///  Monto para filtrar.
        /// </summary>
        public int monto { get; set; }
      
    }
}
