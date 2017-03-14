﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridKardex
    {
        public string Fecha { get; set; }
        public string Deposito { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoTabaco { get; set; }
        public string Unidad { get; set; }
        public Nullable<double> Ingreso { get; set; }
        public Nullable<double> Egreso { get; set; }
        public Nullable<double> Saldo { get; set; }

    }
}
