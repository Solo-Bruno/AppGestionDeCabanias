using Libreria.LogicaAplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab
{
    public interface IUpdateTipoCabania
    {
        public TipoCabaniaDto FindById(int id);
        public void Update(TipoCabaniaDto tipoCabaniaDto);
    }
}
