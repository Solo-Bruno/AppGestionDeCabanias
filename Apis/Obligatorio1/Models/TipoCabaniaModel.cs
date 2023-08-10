using Libreria.LogicaNegocio.Entidades.ValueObject.TipoCabania;

namespace Libreria.Web.Models
{
    public class TipoCabaniaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CostoHuesped { get; set; }
    }
}
