using Libreria.LogicaNegocio.Entidades.ValueObject.TipoCabania;
using Libreria.LogicaNegocio.ValidacionEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    [Table("TipoCabania")]
    //[Index(nameof(Nombre), IsUnique = true)]
    public class TipoCabania : IValidable
    {
        public int Id { get; set; }
        public NombreTipoCabania Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CostoHuesped { get; set; }
        public int topeMinimo { get; set; }
        public  int topeMaximo { get; set; }

        public void Validar()
        {
            //ValidarNombre();
            ValidarDescripcion();
            ValidarCostoHuesped();
        }

        //private void ValidarNombre()
        //{
           
        //    if (string.IsNullOrEmpty(Nombre.Trim()))
        //    {
        //        throw new InvalidOperationException("El nombre no puede estar vacío");
        //    }
        //    else if (!Nombre.All(nom => Char.IsLetter(nom) || nom == ' '))
        //    {
        //        throw new InvalidOperationException("El nombre debe tener solo letras ");
        //    }else if(Nombre.LastIndexOf(" ") == Nombre.Length -1 || Nombre.IndexOf(" ") == 0 )  
        //    {
        //        throw new InvalidOperationException("El nombre tiene espacios blancos al principio o al final");
        //    }
        //}
        private void ValidarDescripcion()
        {
            if(Descripcion.Length < topeMinimo || Descripcion.Length > topeMaximo)
            {
                throw new InvalidOperationException("La descripcion debe tener entre 10 a 200 caracteres");
            }
        }
        private void ValidarCostoHuesped() {

            if (CostoHuesped < 0)
            {
                throw new InvalidOperationException("El costo no puede ser menor a 0");
            }
        }
        
    }
}
