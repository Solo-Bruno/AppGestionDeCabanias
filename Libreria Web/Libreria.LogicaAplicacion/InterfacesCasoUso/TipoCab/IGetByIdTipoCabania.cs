using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab
{
    public interface IGetByIdTipoCabania
    {

        public TipoCabaniaDto FindById(int Id);
   
    }
}
