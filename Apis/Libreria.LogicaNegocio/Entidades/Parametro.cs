using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    public class Parametro
    {
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
       [Key]
       public string NombreParametro { get; set; }
       public int ValorDelParametro { get; set; }
      
    }
}
