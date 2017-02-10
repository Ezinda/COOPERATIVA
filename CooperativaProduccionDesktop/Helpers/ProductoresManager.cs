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
                    .Select(x => new
                    {
                        ID = x.ID.Value,
                        x.NOMBRE,
                        x.CUIT,
                        x.nrofet,
                        x.Provincia,
                        x.IVA
                    })
                    .Single();

                ProductorIVATASATypes situacionIVATASA;

                switch (productor.IVA)
	            {
	            	case "MT":
                        situacionIVATASA = ProductorIVATASATypes.Monotributista;
                        break;
                    default:
                        situacionIVATASA = ProductorIVATASATypes.ResponsableInscripto;
                        break;
	            }

                var situacionIVATASADescripcion = ProductorIVATASA.GetDescription(situacionIVATASA);

                var productorvm = new ProductorViewModel()
                {
                    Id = productor.ID,
                    Nombre = productor.NOMBRE,
                    CUIT = productor.CUIT,
                    FET = productor.nrofet,
                    Provincia = productor.Provincia,
                    SituacionIVATASA = situacionIVATASA,
                    SituacionIVATASADescripcion = situacionIVATASADescripcion
                };

                return productorvm;
            }
        }
    }

    public interface IProductoresManager
    {
        ProductorViewModel GetProductor(Guid productorid);
    }
}
