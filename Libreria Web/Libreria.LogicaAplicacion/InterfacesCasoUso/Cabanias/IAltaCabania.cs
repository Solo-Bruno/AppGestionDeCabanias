using Libreria.LogicaAplicacion.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias
{
    public interface IAltaCabania
    {
        public void Ejecutar(CabaniaDto cabaniaDto);
        public void GuardarImagen(CabaniaDto cab, IFormFile img, string ruta);
    }
}
