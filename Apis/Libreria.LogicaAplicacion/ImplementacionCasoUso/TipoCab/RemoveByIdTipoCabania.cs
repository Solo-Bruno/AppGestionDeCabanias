using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.TipoCab
{
    public class RemoveByIdTipoCabania : IRemoveByIdTipoCabania
    {
        private IRepositorioTipoCabania _RepoTc;

        public RemoveByIdTipoCabania(IRepositorioTipoCabania TC)
        {
            _RepoTc = TC;
        }
        public void RemoveById(int id)
        {
            try
            {
                _RepoTc.RemoveById(id);
            }
            catch (Exception e)
            {

                throw new TipoCabaniaException("No se pudo eliminar la cabania: " + e.Message);
            }
        }
    }
}
