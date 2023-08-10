using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T>where T : class
    {
        IEnumerable<T> FindAll();

        T FindById(int id);

        void Add(T tipo);

        void Remove(T tipo);

        void RemoveById(int id);
        void Update(T tipo);   
    }
}
