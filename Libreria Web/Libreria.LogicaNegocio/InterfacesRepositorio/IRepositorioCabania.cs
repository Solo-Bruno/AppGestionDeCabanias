using Libreria.LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioCabania: IRepositorio<Cabania>
    {
        public Cabania FindByName(string nombre);

        public void GuardarImg(Cabania cab, IFormFile img, string ruta);
        public int CantidadDeMantenimientos(int? id);
        public Cabania FindById(int? id);
        public int ExisteFoto(string nomb);
        public IEnumerable<Cabania> FindByFiltrado(string? nombre, int? tipoId, int? cantidad, string? hab);
    }
}
