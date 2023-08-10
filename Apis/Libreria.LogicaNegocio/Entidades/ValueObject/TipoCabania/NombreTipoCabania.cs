using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades.ValueObject.TipoCabania
{
    [Owned]
    public class NombreTipoCabania: IEquatable<NombreTipoCabania>
    {

        public string ValorNombre { get; set; }
        protected NombreTipoCabania()
        {
            
        }

        public NombreTipoCabania(string nom)
        {
            ValidarNombre(nom);
            ValorNombre = nom;
        }

        private void ValidarNombre(string nombre)
        {

            if (string.IsNullOrEmpty(nombre.Trim()))
            {
                throw new InvalidOperationException("El nombre no puede estar vacío");
            }
            else if (!nombre.All(nom => Char.IsLetter(nom) || nom == ' '))
            {
                throw new InvalidOperationException("El nombre debe tener solo letras ");
            }
            else if (nombre.LastIndexOf(" ") == nombre.Length - 1 || nombre.IndexOf(" ") == 0)
            {
                throw new InvalidOperationException("El nombre tiene espacios blancos al principio o al final");
            }
        }

        public bool Equals(NombreTipoCabania? other)
        {
            if (other == null) {
                throw new ArgumentException("No se puede comparar contra null");
            }
            return ValorNombre.Equals(other.ValorNombre);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as NombreTipoCabania;
            if (other == null)
            {
                throw new ArgumentException("No se puede comparar contra null");
            }
            return ValorNombre.Equals(other.ValorNombre);
        }
    }
}
