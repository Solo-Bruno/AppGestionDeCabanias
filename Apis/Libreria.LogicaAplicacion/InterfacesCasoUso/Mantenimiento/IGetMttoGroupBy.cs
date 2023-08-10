using Libreria.LogicaAplicacion.Dto.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento
{
    public interface IGetMttoGroupBy
    {
        public IEnumerable<MttoGrupBy> GetMttoGroupBy(int top1 , int top2);
    }
}
