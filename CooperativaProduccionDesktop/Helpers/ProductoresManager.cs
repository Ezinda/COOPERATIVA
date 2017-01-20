using CooperativaProduccion.ViewModels;
using DesktopEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers
{
    public class ProductoresManager : IProductoresManager
    {
        public ProductorViewModel GetProductor(Guid productorid)
        {
            using (var context = new CooperativaProduccionEntities())
            {
                var productor = context.Vw_Productor
                    .Where(x => x.ID == productorid)
                    .Select(x => new ProductorViewModel()
                    {
                        Id = x.ID.Value,
                        Nombre = x.NOMBRE,
                        CUIT = x.CUIT,
                        FET = x.nrofet,
                        Provincia = x.Provincia
                    })
                    .Single();

                return productor;
            }
        }
    }

    public interface IProductoresManager
    {
        ProductorViewModel GetProductor(Guid productorid);
    }
}
