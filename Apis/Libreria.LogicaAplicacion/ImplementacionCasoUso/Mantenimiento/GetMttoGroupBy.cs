using Libreria.LogicaAplicacion.Dto.Auxiliar;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.Mantenimiento
{
    public class GetMttoGroupBy : IGetMttoGroupBy
    {
        private IRepositorioMantenimiento _repoMtto;

        public GetMttoGroupBy(IRepositorioMantenimiento RM)
        {
            _repoMtto = RM;
        }
      

        IEnumerable<MttoGrupBy> IGetMttoGroupBy.GetMttoGroupBy(int top1, int top2)
        {
            try
            {
                var lista = _repoMtto.ManteniminetosEntrValores(top1, top2);

                var ret = MapeoMttoGroupBy.listMttoGroup(lista);

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
