using Libreria.LogicaNegocio.ValidacionEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    public class Usuario:IValidable
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public void Validar()
        {
            ValidarMail();
            ValidarPass();
            
        }

        public void ValidarMail()
        {
            
            if (!Email.Contains("@"))
            {
                throw new InvalidOperationException("El email no tiene @");
            }else if (Email.IndexOf("@") == 0)
            {
                throw new InvalidOperationException("No se puede poner un @ al principio");
            }else if(Email.IndexOf("@") == Email.Length - 1)
            {
                throw new InvalidOperationException("No se puede poner un @ al final");
            }
           
            
        }
        public void ValidarPass()
        {
            if(Pass.Length < 6)
            {
                throw new InvalidOperationException("La contraseña debe tener mas de 6 caracteres");
            }else if (Pass.All(a => (a >= 65 && a <= 90)))
            {
                throw new InvalidOperationException("La contraseña debe tener al menos una mayuscula (Bruno)");
                
                
            }
            else if (!Pass.Any(char.IsUpper))
            {
                throw new InvalidOperationException("La contraseña debe tener Almenos una mayuscula (Fabricio)");

            }
            else if (!Pass.Any(char.IsLower))
            {
                throw new InvalidOperationException("La contraseña debe tener Almenos una minuscula (Fabricio)");
            }
            else if (!Pass.Any(char.IsDigit))
            {
                throw new InvalidOperationException("La contraseña debe tener almenos un numero (Fabricio)");
            }


        }

    }
}
