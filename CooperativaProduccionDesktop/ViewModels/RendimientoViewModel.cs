using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class RendimientoViewModel
    {
        public BlendDePeriodoViewModel Blend { get; set; }

        public string BlendDescripcion { get; set; }

        public DateTime Fecha { get; set; }

        public long OrdenDeProduccion { get; set; }

        public long Corrida { get; set; }

        //Turno(un valor puede ser "MAÑANA" pero esta columna esta vacia muchas veces)

        public Decimal Tara { get; set; }

        public long PrimeraCaja { get; set; }

        public long UltimaCaja { get; set; }

        public long NumeroCajas { get; set; }

        public long Kilos { get; set; }
        
        //Scrap(columna vacia)
    }
}
