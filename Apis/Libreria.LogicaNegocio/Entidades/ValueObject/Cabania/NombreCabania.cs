using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades.ValueObject.Cabania
{
    [Owned]
    public class NombreCabania: IEquatable<NombreCabania>
    {
        public string Valor { get; set; }
        public NombreCabania()
        {
            
        }
        public NombreCabania(string nombre)
        {
            ValidarNombre(nombre);
            Valor = nombre;
        }

        private void ValidarNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
            {
                throw new InvalidOperationException("El nombre no puede estar vacío");
            }
            else if (!nombre.All(nom => Char.IsLetter(nom) || nom == ' '))
            {
                //todo pregunta a la profe
                throw new InvalidOperationException("El nombre debe tener solo letras");

            }
            else if (nombre.LastIndexOf(" ") == nombre.Length - 1 || nombre.IndexOf(" ") == 0)
            {
                throw new InvalidOperationException("El nombre tiene espacios blancos al principio o al final");
            }
        }

        public bool Equals(NombreCabania? other)
        {
            if (other == null) {
                throw new ArgumentException("No se puede comparar contra null");           
            }
            return Valor.Equals(other.Valor);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as NombreCabania;
            if (other == null)
            {
                throw new ArgumentException("No se puede comparar contra null");
            }
            return Valor.Equals(other.Valor);
        }
    }
}
