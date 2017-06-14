using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class BlendViewModel
    {
        public Guid Id { get; set; }

        public string Descripcion { get; set; }
    }

    public class BlendDePeriodoViewModel
    {
        public Guid Id { get; set; }

        public string Descripcion { get; set; }

        public int Periodo { get; set; }

        public int OrdenDeProduccion { get; set; }
    }
}
