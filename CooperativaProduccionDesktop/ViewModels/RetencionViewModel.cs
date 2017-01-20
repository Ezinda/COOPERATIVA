using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class RetencionTypes
    {
        public const string RetencionIIBB = "IIBB";
        public const string RetencionEEAOC = "EEAOC";
        public const string RetencionSaludPublica = "SaludPublica";
        public const string RetencionGADM = "GADM";
        public const string RetencionGCIAS = "GCIAS";
        public const string RetencionRiego = "Riego";

        //public static string RetencionIVA = "IVA";
        //public static string RetencionMonotributo = "Monotributo";
    }

    public class RetencionViewModel
    {
        public string Nombre { get; set; }
    }

    public class RetencionAplicadaViewModel
    {
        public string Nombre { get; set; }

        public decimal Importe { get; set; }
    }

    public class RetencionDetalleViewModel
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }

    public class RetencionAplicadaDetalleViewModel
    {
        public string Nombre { get; set; }

        public decimal Porcentaje { get; set; }

        public decimal Base { get; set; }

        public bool UsaBase { get; set; }

        public decimal Importe { get; set; }
    }
}
