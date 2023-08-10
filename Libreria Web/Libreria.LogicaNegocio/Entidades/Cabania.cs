using Libreria.LogicaNegocio.Entidades.ValueObject.Cabania;
using Libreria.LogicaNegocio.ValidacionEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    [Table("Cabania")]
   // [Index(nameof(Nombre), IsUnique = true)]
 
    public class Cabania: IValidable
    {
        
        [Key] public int NumeroHabitacion { get; set; }
        public NombreCabania Nombre { get; set; }
        public string Descripcion { get; set;}
        public bool TieneJacuzzi { get; set; }
        public bool TieneReservas { get; set; }
        
        public CantMaxPersonasCabania CantMaxPers { get; set; }
        
        public string? NombreFoto { get; set; }

        [ForeignKey(nameof(Tipo))] public int TipoId { get; set; }
        public TipoCabania Tipo { get; set; }

        public List<Mantenimiento> MisMantenimientos { get; set; }

        public int topiMinimoCab { get; set; }
        public int topiMaximoCab { get; set; }

        public void Validar()
        {
           //ValidarNombre();
           ValidarDescripcion();
           ValidarNombreFoto();
        }
        //private void ValidarNombre()
        //{
        //    if (string.IsNullOrEmpty(Nombre.Trim()))
        //    {
        //        throw new InvalidOperationException("El nombre no puede estar vacío");
        //    }
        //    else if (!Nombre.All(nom => Char.IsLetter(nom) || nom == ' '))
        //    {
        //        //todo pregunta a la profe
        //        throw new InvalidOperationException("El nombre debe tener solo letras");

        //    }
        //    else if (Nombre.LastIndexOf(" ") == Nombre.Length - 1 || Nombre.IndexOf(" ") == 0)
        //    {
        //        throw new InvalidOperationException("El nombre tiene espacios blancos al principio o al final");
        //    }
        //}

        private void ValidarDescripcion()
        {
            if(Descripcion.Length < topiMinimoCab || Descripcion.Length > topiMaximoCab)
            {
                throw new InvalidOperationException("La descripcion debe tener entre 10 o 500 caracteres");

            }
        }

        private void ValidarNombreFoto() {
            if (NombreFoto != null) 
            {
                if (!(NombreFoto.Substring(NombreFoto.Length - 3) == "png" || (NombreFoto.Substring(NombreFoto.Length - 3) == "jpg")))
                {
                    throw new Exception("La extencion de la imagen tine que ser png o jpg ");
                }
            }
        }
    }
}
