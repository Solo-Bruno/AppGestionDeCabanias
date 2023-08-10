using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioTipoCabania:IRepositorio<TipoCabania>
    {
        public TipoCabania FindByName(string nombre);
        public void RemoveByName(string name);
    }
}
