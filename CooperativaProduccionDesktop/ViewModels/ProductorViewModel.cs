using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class ProductorIVATASA
    {
        public const string ProductorIVATASAMonotributista = "";
        public const string ProductorIVATASAResponsableInscripto = "";

        public static string GetDescription(ProductorIVATASATypes situacion)
        {
            var result = String.Empty;

            switch (situacion)
            {
                case ProductorIVATASATypes.Monotributista:
                    result = "Monotributo";
                    break;
                case ProductorIVATASATypes.ResponsableInscripto:
                    result = "Responsable Inscripto";
                    break;
            }

            return result;
        }
    }

    public class ProductorViewModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string CUIT { get; set; }

        public string FET { get; set; }

        public string Provincia { get; set; }

        public ProductorIVATASATypes SituacionIVATASA { get; set; }

        public string SituacionIVATASADescripcion { get; set; }
    }

    public enum ProductorIVATASATypes
    {
        Monotributista,
        ResponsableInscripto
    }
}
