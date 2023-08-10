using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades.ValueObject.Cabania
{
    [Owned]
    public class CantMaxPersonasCabania: IEquatable<CantMaxPersonasCabania>
    {
        public int CantMaxPers { get; set; }

        public CantMaxPersonasCabania()
        {
            
        }
        public CantMaxPersonasCabania(int cant)
        {
            ValidarCantidad(cant);
            CantMaxPers = cant;
        }

        private void ValidarCantidad(int cantidad) { 
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad tiene que ser mayor a 0");
            }
        }

        public bool Equals(CantMaxPersonasCabania? other)
        {
            if (other == null) {
                throw new ArgumentException("No se puede comparar contra null");
            }   
            return CantMaxPers.Equals(other.CantMaxPers);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as CantMaxPersonasCabania;
            if (other == null)
            {
                throw new ArgumentException("No se puede comparar contra null");
            }
            return CantMaxPers.Equals(other.CantMaxPers);
        }
    }
}
