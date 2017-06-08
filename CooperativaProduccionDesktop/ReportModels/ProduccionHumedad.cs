using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class ProduccionHumedadRecord
    {
        public string _Fecha { get; set; }

        public string _Hora { get; set; }

        public long Corrida { get; set; }

        public List<ProduccionHumedadDetalleRecord> Detalle { get; set; }
    }

    public class ProduccionHumedadDetalleRecord
    {
        public String Fecha { get; set; }

        public String HoraMuestra { get; set; }

        public String TemperaturaEmpaque { get; set; }

        public String NumeroCaja { get; set; }

        public String NumeroCapsulaBrab { get; set; }

        public String HoraEntrada { get; set; }

        public String HoraSalida { get; set; }

        public String HumBrab { get; set; }

        public String NumeroCapsulaHearson { get; set; }

        public String HumHearson { get; set; }
    }
}
