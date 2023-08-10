using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.CabaniasCar
{
    public class FindByIdCabania : IFindByIdCabania
    {
        private IRepositorioCabania _repoCab;

        public FindByIdCabania(IRepositorioCabania RC)
        {
            _repoCab = RC;
        }
        public CabaniaDto FindById(int id)
        {
            try
            {
                var cabania = _repoCab.FindById(id);

                if (cabania == null)
                {
                    throw new CabaniaException("No se encontro ninguna Cabania");
                }

                var ret = MapeoCabania.ToDto(cabania);

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
