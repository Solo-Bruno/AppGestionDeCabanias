using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento
{
    public interface IGetAllMttoXCab
    {
        public IEnumerable<MantenimientoDto> Ejecutar(int id);
    }
}
