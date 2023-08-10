using Libreria.LogicaNegocio.Entidades.ValueObject.Cabania;
using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Libreria.LogicaAplicacion.Dto
{
    public class CabaniaDto
    {
        /// <summary>
        /// Identificador de la cabania.
        /// </summary>
        public int NumeroHabitacion { get; set; }
        /// <summary>
        /// Nombre de la cabania.
        /// </summary>
        [Required]
        public string Nombre { get; set; }
        /// <summary>
        /// Descripcion de la cabania.
        /// </summary>
        [Required]
        public string Descripcion { get; set; }
        /// <summary>
        /// Informa si la cabania tiene incorporado un/os Jacizzi/s.
        /// </summary>
        public bool TieneJacuzzi { get; set; }
        /// <summary>
        /// Informa si la cabania tiene reservas.
        /// </summary>
        public bool TieneReservas { get; set; }
        /// <summary>
        /// Cantidad maxima de personas que soporta la cabania.
        /// </summary>

        public int CantMaxPers { get; set; }
        /// <summary>
        /// Nombre de la imagen de la Cabania.
        /// </summary>

        public string? NombreFoto { get; set; }
        /// <summary>
        /// Id que vincula la Cabania con Tipo Cabania .
        /// </summary>
        public int TipoId { get; set; }
        
        

    }
}
