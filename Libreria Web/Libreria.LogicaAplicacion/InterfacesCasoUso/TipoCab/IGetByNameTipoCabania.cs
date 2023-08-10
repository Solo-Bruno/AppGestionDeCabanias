using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab
{
    public interface IGetByNameTipoCabania
    {
        public TipoCabaniaDto FindByName(string nombre);
    }
}
