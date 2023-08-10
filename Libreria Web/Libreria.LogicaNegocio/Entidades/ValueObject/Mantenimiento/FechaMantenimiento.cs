
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades.ValueObject.Mantenimiento
{
    [Owned]
    public class FechaMantenimiento:IEquatable<FechaMantenimiento>
    {
        public DateTime Valor { get; set; }

        public FechaMantenimiento()
        {
            
        }

        public FechaMantenimiento(DateTime date)
        {
            Valor = date;
        }

        public bool Equals(FechaMantenimiento? other)
        {
            if (other == null)
            {
                throw new ArgumentException("No se puede comparar contra null");
            }
            return Valor.Equals(other.Valor);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as FechaMantenimiento;
            if (other == null)
            {
                throw new ArgumentException("No se puede comparar contra null");
            }
            return Valor.Equals(other.Valor);
        }
    }
}
