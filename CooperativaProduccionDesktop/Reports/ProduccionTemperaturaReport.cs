using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CooperativaProduccion.Reports
{
    public partial class ProduccionTemperaturaReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ProduccionTemperaturaReport()
        {
            InitializeComponent();

            XRSubreport detailReport = this.Bands[BandKind.Detail].FindControl("Subreport", true) as XRSubreport;
            detailReport.ReportSource.DataSource = DataSource;
        }

    }
}
