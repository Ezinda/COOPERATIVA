using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    //public class ProduccionTemperaturaRecord
    //{
    //    public string Fecha { get; set; }
    //
    //    public string Hora { get; set; }
    //
    //    public string Turno { get; set; }
    //
    //    public long Caja { get; set; }
    //
    //    public decimal TempEmpaque { get; set; }
    //
    //    public string Ejecuto { get; set; }
    //
    //    public decimal TempAmbiente { get; set; }
    //
    //    public string Observaciones { get; set; }
    //}


    public class ProduccionTemperaturaRecord
    {
        public string _Fecha { get; set; }

        public string _Hora { get; set; }

        public long Corrida { get; set; }
    
        public string Minimo { get; set; }
    
        public string Meta { get; set; }
    
        public string Maximo { get; set; }
    
        public List<ProduccionTemperaturaDetalleRecord> Detalle { get; set; }
    }

    public class ProduccionTemperaturaDetalleRecord
    {
        public string Fecha { get; set; }

        public string Hora { get; set; }

        public string Turno { get; set; }

        public long Caja { get; set; }

        public decimal TempEmpaque { get; set; }

        public string Ejecuto { get; set; }

        public decimal TempAmbiente { get; set; }

        public string Observaciones { get; set; }
    }
}
