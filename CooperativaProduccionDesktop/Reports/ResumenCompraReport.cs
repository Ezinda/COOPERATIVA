using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CooperativaProduccion.Reports
{
    public partial class ResumenCompraReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ResumenCompraReport()
        {
            InitializeComponent();
        }

        private void xrLabel12_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Convert.ToSingle(xrLabel9.Summary.GetResult()) / Convert.ToSingle(xrLabel8.Summary.GetResult());
            e.Handled = true;
        }
    }
}
