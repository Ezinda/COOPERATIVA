using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using System.Globalization;
using CooperativaProduccion.Reports;
using DesktopEntities.Models;
using CooperativaProduccion.ReportModels;
using System.Data.SqlClient;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoPendiente : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid PesadaId;

        public Form_RomaneoPendiente(Guid Id)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            PesadaId = Id;
        }

        #region Method Code

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinuarPesada_Click(object sender, EventArgs e)
        {
            ContinuarPesada(PesadaId);
        }
        
        private void btnCerrarPesada_Click(object sender, EventArgs e)
        {
            this.Close();
            ImpimirRomaneo(PesadaId);
        }

        #endregion

        #region Method Dev

        private void ContinuarPesada(Guid Id)
        {
            this.Close();
            var pesada = new Form_RomaneoPesada(Id);
            pesada.Show();
        }
            
        private void ImpimirRomaneo(Guid PesadaId)
        {
            var pesada = Context.Pesada.Find(PesadaId);
            if (pesada != null)
            {
                pesada.RomaneoPendiente = false;
                Context.Entry(pesada).State = EntityState.Modified;
                Context.SaveChanges();

                var vw_pesada = Context.Vw_Pesada
                    .Where(x => x.PesadaId == PesadaId)
                    .FirstOrDefault();

                if (vw_pesada != null)
                {
                    var reporte = new RomaneoReport();
                    reporte.Parameters["Productor"].Value = vw_pesada.Productor;
                    reporte.Parameters["Fet"].Value = vw_pesada.Fet;
                    reporte.Parameters["Localidad"].Value = vw_pesada.Localidad;
                    reporte.Parameters["Provincia"].Value = vw_pesada.Provincia;
                    reporte.Parameters["NumRomaneo"].Value =
                        vw_pesada.PuntoVentaRomaneo.ToString().PadLeft(4, '0') + " - " + vw_pesada.NumRomaneo.ToString().PadLeft(8, '0');
                    reporte.Parameters["Fecha"].Value = vw_pesada.FechaRomaneo.Value
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    reporte.Parameters["Reimpresion"].Value = string.Empty;

                    #region Subreport Fardos

                    List<RegistroFardo> datasourceFardo;
                    datasourceFardo = GenerarReporteFardo(PesadaId);
                    reporte.reportPesadaDetalle.ReportSource.DataSource = datasourceFardo;

                    #endregion

                    #region Subreport Clase

                    List<RegistroPesada> datasourcePesada;
                    datasourcePesada = GenerarReporteClase(PesadaId);
                    reporte.reportDetalleClase.ReportSource.DataSource = datasourcePesada;

                    #endregion

                    #region Parametros Totales

                    var totales = Context.Pesada
                        .Where(x => x.Id == vw_pesada.PesadaId)
                        .FirstOrDefault();

                    if (totales != null)
                    {
                        reporte.Parameters["totalfardo"].Value = totales.TotalFardo;
                        reporte.Parameters["totalKilos"].Value = totales.TotalKg;
                        reporte.Parameters["ImporteBruto"].Value = totales.ImporteBruto;
                    }

                    #endregion

                    using (ReportPrintTool tool = new ReportPrintTool(reporte))
                    {
                        reporte.ShowPreviewMarginLines = false;
                        tool.PreviewForm.Text = "Romaneo";
                        if (ValidarDebug().Equals(true))
                        {
                            tool.ShowPreviewDialog();
                        }
                        else
                        {
                            reporte.Print();
                        }
                    }
                }
            }
        }

        public List<RegistroFardo> GenerarReporteFardo(Guid PesadaId)
        {
            List<RegistroFardo> datasource = new List<RegistroFardo>();

            var fardos = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .OrderBy(x => x.NumFardo)
                .ToList();

            foreach (var fardo in fardos)
            {
                RegistroFardo registroFardos = new RegistroFardo();
                registroFardos.NumFardo = fardo.NumFardo.ToString();
                registroFardos.Clase = fardo.Clase;
                registroFardos.Kilos = fardo.Kilos.ToString();

                datasource.Add(registroFardos);
            }
            return datasource;
        }

        public List<RegistroPesada> GenerarReporteClase(Guid PesadaId)
        {
            List<RegistroPesada> datasource = new List<RegistroPesada>();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                conn.Open();

                string sql = @"SELECT * FROM dbo.Vw_ResumenPesadaPorClase where PesadaId='" + PesadaId + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    RegistroPesada registroFardos = new RegistroPesada();
                    registroFardos.Clase = row["Clase"].ToString();
                    registroFardos.Kilos = row["Kilos"].ToString();

                    datasource.Add(registroFardos);
                }

                return datasource;
            }
        }

        private bool ValidarDebug()
        {
            var debug = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.Debug)
              .FirstOrDefault();

            return debug.Valor;
        }

        #endregion
    }
}