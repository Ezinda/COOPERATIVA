using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class ProduccionRegistroDatosDeCalidadAnualLamina
    {
        public string Empresa { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Blend { get; set; }

        public long OrdenProduccion { get; set; }

        public long Corrida { get; set; }

        public long CajaReferente { get; set; }

        // +1
        
        public decimal Brab { get; set; }

        // +4

        public decimal Temp { get; set; }

        // +4
    }

    public class ProduccionRegistroDatosDeCalidadAnualLinea
    {
        public string Empresa { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Blend { get; set; }

        public long OrdenProduccion { get; set; }

        public long Corrida { get; set; }

        public long CajaReferente { get; set; }

        public int PesoMuestra { get; set; }

        public decimal Uno { get; set; }

        public decimal UnMedio { get; set; }

        public decimal TotalUnMedio { get; set; }

        public decimal UnCuarto { get; set; }

        public decimal UnOctavo { get; set; }

        public decimal Pan { get; set; }

        public decimal TotalUnCuarto { get; set; }

        public decimal PaloObjetable { get; set; }

        public decimal PaloTotal { get; set; }

        public int PesoMuestraGr { get; set; }

        public string UnoSinteticoGr { get; set; }

        public string UnoSinteticoCant { get; set; }

        public string UnoSinteticoPorc { get; set; }

        public string DosNaturalGr { get; set; }

        public string DosNaturalCant { get; set; }

        public string DosNaturalPorc { get; set; }

        public string TresAnimalGr { get; set; }

        public string TresAnimalCant { get; set; }

        public string TresAnimalPorc { get; set; }

        public string CuatroVegetalGr { get; set; }

        public string CuatroVegetalCant { get; set; }

        public string CuatroVegetalPorc { get; set; }

        public decimal Suma { get; set; }
    }
}
