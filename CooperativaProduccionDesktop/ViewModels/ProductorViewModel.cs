using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class ProductorViewModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string CUIT { get; set; }

        public string FET { get; set; }

        public string Provincia { get; set; }
    }
}
