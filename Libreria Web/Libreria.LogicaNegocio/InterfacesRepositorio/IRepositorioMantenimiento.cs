using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioMantenimiento: IRepositorio<Mantenimiento>
    {
        public IEnumerable<Mantenimiento> FindByCabania(Cabania cab);

        public IEnumerable<Mantenimiento> FindByFechas(DateTime fch1, DateTime fch2, int? id);
        public void AddMtto(Mantenimiento tipo, Cabania cab);
    }
}
