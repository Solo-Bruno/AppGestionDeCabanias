namespace Libreria.Web.Models.auxiliares
{
    public class MapeoCabaniaConTipo
    {
        internal static CabaniaConTipoModel ToCabConTip(CabaniaModel cabanaia) {

            return new CabaniaConTipoModel()
            {
                TipoId = cabanaia.TipoId,
                NumeroHabitacion = cabanaia.NumeroHabitacion,
                Nombre = cabanaia.Nombre,
                Descripcion = cabanaia.Descripcion,
                TieneJacuzzi = cabanaia.TieneJacuzzi,
                TieneReservas = cabanaia.TieneReservas,
                CantMaxPers = cabanaia.CantMaxPers,
                NombreFoto = cabanaia.NombreFoto,
            };
        
        }
    }
}
