using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Mapeo;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.ImplementacionCasoUso.CabaniasCar
{
    public class AltaCabania : IAltaCabania
    {
        private IRepositorioCabania _repoCab;
        private IRepositorioParametros _repoPara;

        public AltaCabania(IRepositorioCabania repositorioCabania, IRepositorioParametros repositorioParametros)
        {
            _repoCab = repositorioCabania;
            _repoPara = repositorioParametros;
        }
        public void Ejecutar(CabaniaDto cabaniaDto)
        {
            try
            {
                if (cabaniaDto == null)
                {
                    throw new CabaniaException("No hay datos para poder crear esta Cabania");
                }

                var cabania = MapeoCabania.FromDto(cabaniaDto);
                cabania.topiMaximoCab = _repoPara.FindByNombre("topeMaximoCab");
                cabania.topiMinimoCab = _repoPara.FindByNombre("topeMinimoCab");

                _repoCab.Add(cabania);
 
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public void GuardarImagen(CabaniaDto cab, IFormFile img, string ruta)
        //{
        //    try
        //    {
        //        var cabania = _repoCab.FindById(cab.NumeroHabitacion);
        //        _repoCab.GuardarImg(cabania, img, ruta);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
