using Libreria.LogicaNegocio.Entidades.ValueObject.Mantenimiento;
using Libreria.LogicaNegocio.ValidacionEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    [Table("Mantenimiento")]
    public class Mantenimiento: IValidable
    {
        [Key]
        public int Id { get; set; }
        public FechaMantenimiento Fecha { get; set; }
        public string Descripcion { get; set;}
        public int Costo { get; set; }
        [Required]
        public string NombreUs { get; set; }

        [ForeignKey (nameof(Cabania))]
        public int CabaniaId { get; set; }
        public Cabania Cabania { get; set; }
        public int topeMinimoMtto { get; set; }
        public int topeMaximoMtto { get; set; }


        public void Validar()
        {
            ValidarDescripcion();
            ValidarCosto();
        }

        private void ValidarDescripcion()
        {
            if (Descripcion.Length < topeMinimoMtto || Descripcion.Length > topeMaximoMtto)
            {
                throw new InvalidOperationException("La descripcion debe tener entre 10 o 200 caracteres");

            }
        }

        private void ValidarCosto() {
            if (Costo < 0)
            {
                throw new InvalidOperationException("El costo debe ser positivo");
            }
        }
    }
}
