using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DesktopEntities.Models;
using System.Linq.Expressions;
using Extensions;
using DevExpress.Utils;
using CooperativaProduccion.ReportModels;
using System.Data.SqlClient;
using System.Globalization;
using CooperativaProduccion.Reports;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.Helpers.GridRecords;
using System.IO;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoGestionRomaneo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;

        public Form_RomaneoGestionRomaneo(bool ReimpresionRomaneo, bool ResumenRomaneo,
            bool ResumenCompra, bool ResumenClaseMes, bool ResumenClaseTrimestre)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar(ReimpresionRomaneo, ResumenRomaneo, ResumenCompra,
                ResumenClaseMes, ResumenClaseTrimestre);
        }

        private void Iniciar(bool ReimpresionRomaneo, bool ResumenRomaneo,
            bool ResumenCompra, bool ResumenClaseMes, bool ResumenClaseTrimestre)
        {
            btnReimpresionRomaneo.Visibility = ReimpresionRomaneo.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenRomaneo.Visibility = ResumenRomaneo.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenCompra.Visibility = ResumenCompra.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenClasesMes.Visibility = ResumenClaseMes.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenClasesTrimestre.Visibility = ResumenClaseTrimestre.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        private void CargarCombo()
        {

            var tipotabaco = Context.Vw_TipoTabaco
                .Where(x => x.RUBRO_ID != null)
                .ToList();

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFet.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtProductor.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtProductor.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void txtProductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtProductor.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtFet.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarRomaneo_Click(object sender, EventArgs e)
        {
            BuscarRomaneo();
        }

        private void btnResumenRomaneo_Click(object sender, EventArgs e)
        {
            ResumenRomaneo();
        }

        private void BuscarProductor()
        {
            var result = (
                from a in Context.Vw_Productor
                select new
                {
                    full = a.nrofet + a.NOMBRE + a.CUIT,
                    ID = a.ID,
                    FET = a.nrofet,
                    PRODUCTOR = a.NOMBRE,
                    CUIT = a.CUIT,
                    PROVINCIA = a.Provincia
                });
            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var count = result
                    .Where(r => r.FET.Equals(txtFet.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtFet.Text;
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtProductor.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();

                    }
                    else
                    {
                        MessageBox.Show("N° de Fet no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtProductor.Text))
            {
                var count = result
                    .Where(r => r.PRODUCTOR.Contains(txtProductor.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtProductor.Text;
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtProductor.Text))
                      .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtProductor.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void BuscarRomaneo()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date &&
                x.FechaRomaneo <= dpHastaRomaneo.Value.Date);

            pred = !string.IsNullOrEmpty(txtFet.Text) ? pred.And(x => x.ProductorId == ProductorId) : pred;

            pred = !string.IsNullOrEmpty(cbTabaco.Text) ? pred.And(x => x.Tabaco == cbTabaco.Text) : pred;

            var result =
                (from a in Context.Vw_Romaneo
                     .Where(pred)
                 select new
                 {
                     ID = a.PesadaId,
                     FECHA = a.FechaRomaneo,
                     NUMROMANEO = a.NumRomaneo,
                     PRODUCTOR = a.NOMBRE,
                     CUIT = a.CUIT,
                     FET = a.nrofet,
                     PROVINCIA = a.Provincia,
                     KILOS = a.TotalKg,
                     BRUTOSINIVA = a.ImporteBruto,
                     TABACO = a.Tabaco
                 })
                .OrderByDescending(x => x.FECHA)
                .ThenBy(x => x.FET)
                .ToList();

            gridControlRomaneo.DataSource = result;
            gridViewRomaneo.Columns[0].Visible = false;
            gridViewRomaneo.Columns[1].Caption = "Fecha Romaneo";
            gridViewRomaneo.Columns[1].Width = 60;
            gridViewRomaneo.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].Caption = "Número Romaneo";
            gridViewRomaneo.Columns[2].Width = 60;
            gridViewRomaneo.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[3].Caption = "Productor";
            gridViewRomaneo.Columns[3].Width = 150;
            gridViewRomaneo.Columns[4].Caption = "CUIT";
            gridViewRomaneo.Columns[4].Width = 75;
            gridViewRomaneo.Columns[5].Caption = "FET";
            gridViewRomaneo.Columns[5].Width = 55;
            gridViewRomaneo.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[6].Caption = "Provincia";
            gridViewRomaneo.Columns[6].Width = 60;
            gridViewRomaneo.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].Caption = "Kilos";
            gridViewRomaneo.Columns[7].Width = 40;
            gridViewRomaneo.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].Caption = "Bruto sin IVA";
            gridViewRomaneo.Columns[8].Width = 60;
            gridViewRomaneo.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].Caption = "Tabaco";
            gridViewRomaneo.Columns[9].Width = 80;
            gridViewRomaneo.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewRomaneo.RowCount; i++)
            {
                gridViewRomaneo.SelectRow(i);
            }
        }

        private void ResumenRomaneo()
        {
            RomaneoExportToXLS();
        }

        private List<RegistroResumenRomaneoVirginia> GenerarReporteResumenRomaneoVirginia()
        {
            List<RegistroResumenRomaneoVirginia> datasource = new List<RegistroResumenRomaneoVirginia>();
            var resumenVirginia = Context.Vw_ResumenRomaneoVirginia
                .Where(x => x.fechaRomaneo.Value >= dpDesdeRomaneo.Value.Date
                    && x.fechaRomaneo.Value <= dpHastaRomaneo.Value.Date)
                .OrderBy(x=>x.NumRomaneo)
                .ToList();

            foreach (var resumen in resumenVirginia)
            {
                RegistroResumenRomaneoVirginia registro = new RegistroResumenRomaneoVirginia();
                registro.FechaRomaneo = resumen.fechaRomaneo.Value.ToShortDateString();
                registro.NumRomaneo = resumen.NumRomaneo.Value.ToString();
                registro.Productor = resumen.productor;
                registro.Cuit = resumen.cuit;
                registro.Fet = resumen.fet;
                registro.B1F = resumen.B1F.ToString();
                registro.B1L = resumen.B1L.ToString();
                registro.B2F = resumen.B2F.ToString();
                registro.B2KF = resumen.B2KF.ToString();
                registro.B2KL = resumen.B2KL.ToString();
                registro.B2L = resumen.B2L.ToString();
                registro.B3F = resumen.B3F.ToString();
                registro.B3KF = resumen.B3KF.ToString();
                registro.B3KL = resumen.B3KL.ToString();
                registro.B3L = resumen.B3L.ToString();
                registro.B4F = resumen.B4F.ToString();
                registro.B4L = resumen.B4L.ToString();
                registro.C1F = resumen.C1F.ToString();
                registro.C1L = resumen.C1L.ToString();
                registro.C2F = resumen.C2F.ToString();
                registro.C2K = resumen.C2K.ToString();
                registro.C2L = resumen.C2L.ToString();
                registro.C3F = resumen.C3F.ToString();
                registro.C3K = resumen.C3K.ToString();
                registro.C3L = resumen.C3L.ToString();
                registro.C4F = resumen.C4F.ToString();
                registro.C4L = resumen.C4L.ToString();
                registro.H1F = resumen.H1F.ToString();
                registro.H2F = resumen.H2F.ToString();
                registro.H3F = resumen.H3F.ToString();
                registro.N5B = resumen.N5B.ToString();
                registro.N5C = resumen.N5C.ToString();
                registro.N5K = resumen.N5K.ToString();
                registro.N5X = resumen.N5X.ToString();
                registro.NVB = resumen.NVB.ToString();
                registro.NVC = resumen.NVC.ToString();
                registro.NVX = resumen.NVX.ToString();
                registro.T1F = resumen.T1F.ToString();
                registro.T1L = resumen.T1L.ToString();
                registro.T2F = resumen.T2F.ToString();
                registro.T2KF = resumen.T2KF.ToString();
                registro.T2KL = resumen.T2KL.ToString();
                registro.T2L = resumen.T2L.ToString();
                registro.X1F = resumen.X1F.ToString();
                registro.X1L = resumen.X1L.ToString();
                registro.X2F = resumen.X2F.ToString();
                registro.X2K = resumen.X2K.ToString();
                registro.X2L = resumen.X2L.ToString();
                registro.X3F = resumen.X3F.ToString();
                registro.X3K = resumen.X3K.ToString();
                registro.X3L = resumen.X3L.ToString();
                registro.X4F = resumen.X4F.ToString();
                registro.X4L = resumen.X4L.ToString();
                registro.Totalkg = resumen.Totalkg.Value.ToString();
                registro.Importebruto = resumen.Importebruto.Value.ToString();
                datasource.Add(registro);
            }
            return datasource;
        } 

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        private void RomaneoExportToXLS()
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            if (cbTabaco.Text == DevConstantes.TabacoVirginia)
            {
                path = @"C:\SystemDocumentsCooperativa\ResumenRomaneoVirginia";

                CreateIfMissing(path);

                var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                  CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
                  .Replace("-", "").Replace(" ", "");

                string fileName = @"C:\SystemDocumentsCooperativa\ResumenRomaneoVirginia\" + Hora + " - ResumenRomaneoVirginia.xls";

                // Create a report instance.
                var reporte = new ResumenRomaneoVirginiaReport();

                reporte.Parameters["cabecera"].Value = "RESUMEN DE ROMANEOS - " + cbTabaco.Text
                    + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE TUCUMAN.-";

                List<RegistroResumenRomaneoVirginia> datasourceVirginia;
                datasourceVirginia = GenerarReporteResumenRomaneoVirginia();
                reporte.DataSource = datasourceVirginia;

                // Get its XLS export options.
                XlsExportOptions xlsOptions = reporte.ExportOptions.Xls;

                // Set XLS-specific export options.
                xlsOptions.ShowGridLines = true;
                xlsOptions.TextExportMode = TextExportMode.Value;

                // Export the report to XLS.
                reporte.ExportToXls(fileName);

                // Show the result.
                StartProcess(fileName);
            }
            else if (cbTabaco.Text == DevConstantes.TabacoBurley)
            {
                path = @"C:\SystemDocumentsCooperativa\ResumenRomaneoBurley";

                CreateIfMissing(path);

                var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                  CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
                  .Replace("-", "").Replace(" ", "");

                string fileName = @"C:\SystemDocumentsCooperativa\ResumenRomaneoBurley\" + Hora + " - ResumenRomaneoBurley.xls";

                // Create a report instance.
                var reporte = new ResumenRomaneoBurleyReport();

                reporte.Parameters["cabecera"].Value = "RESUMEN DE ROMANEOS - " + cbTabaco.Text
                    + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE TUCUMAN.-";

                List<RegistroResumenRomaneoBurley> datasourceBurley;
                datasourceBurley = GenerarReporteResumenRomaneoBurley();
                reporte.DataSource = datasourceBurley;

                // Get its XLS export options.
                XlsExportOptions xlsOptions = reporte.ExportOptions.Xls;

                // Set XLS-specific export options.
                xlsOptions.ShowGridLines = true;
                xlsOptions.TextExportMode = TextExportMode.Value;

                // Export the report to XLS.
                reporte.ExportToXls(fileName);

                // Show the result.
                StartProcess(fileName);
            }   
        }

        private List<RegistroResumenRomaneoBurley> GenerarReporteResumenRomaneoBurley()
        {
            List<RegistroResumenRomaneoBurley> datasource = new List<RegistroResumenRomaneoBurley>();
            var resumenBurley = Context.Vw_ResumenRomaneoBurley
                .Where(x => x.fechaRomaneo.Value >= dpDesdeRomaneo.Value.Date
                    && x.fechaRomaneo.Value <= dpHastaRomaneo.Value.Date)
                .OrderBy(x => x.NumRomaneo)
                .ToList();

            foreach (var resumen in resumenBurley)
            {
                RegistroResumenRomaneoBurley registro = new RegistroResumenRomaneoBurley();
                registro.FechaRomaneo = resumen.fechaRomaneo.Value.ToShortDateString();
                registro.NumRomaneo = resumen.NumRomaneo.Value.ToString();
                registro.Productor = resumen.productor;
                registro.Cuit = resumen.cuit;
                registro.Fet = resumen.fet;
                registro.B1F = resumen.B1F.ToString();
                registro.B1FR = resumen.B1FR.ToString();
                registro.B2F = resumen.B2F.ToString();
                registro.B2FR = resumen.B2FR.ToString();
                registro.B3F = resumen.B3F.ToString();
                registro.B3FR = resumen.B3FR.ToString();
                registro.B3K = resumen.B3K.ToString();
                registro.C1F = resumen.C1F.ToString();
                registro.C1L = resumen.C1L.ToString();
                registro.C2F = resumen.C2F.ToString();
                registro.C2L = resumen.C2L.ToString();
                registro.C3F = resumen.C3F.ToString();
                registro.C3K = resumen.C3K.ToString();
                registro.C3L = resumen.C3L.ToString();
                registro.NB = resumen.NB.ToString();
                registro.NG = resumen.NG.ToString();
                registro.NX = resumen.NX.ToString();
                registro.T1F = resumen.T1F.ToString();
                registro.T1FR = resumen.T1FR.ToString();
                registro.T2F = resumen.T2F.ToString();
                registro.T2FR = resumen.T2FR.ToString();
                registro.T3K = resumen.T3K.ToString();
                registro.X1F = resumen.X1F.ToString();
                registro.X1L = resumen.X1L.ToString();
                registro.X2F = resumen.X2F.ToString();
                registro.X2L = resumen.X2L.ToString();
                registro.X3K = resumen.X3K.ToString();
                registro.Totalkg = resumen.Totalkg.Value.ToString();
                registro.Importebruto = resumen.Importebruto.Value.ToString();

                datasource.Add(registro);
            }
            return datasource;
        }

        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch
            {
                throw;
            }
        }

        private void btnReimpresionRomaneo_Click(object sender, EventArgs e)
        {
            if (gridViewRomaneo.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewRomaneo.DataRowCount; i++)
                {
                    if (gridViewRomaneo.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewRomaneo.GetRowCellValue(i, "ID").ToString());
                        ImpimirRomaneo(Id);
                    }
                }
            }
        }

        private void ImpimirRomaneo(Guid PesadaId)
        {
            var pesada = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .FirstOrDefault();
            if (pesada.PesadaId != null)
            {
                var reporte = new RomaneoReport();
                reporte.Parameters["Productor"].Value = pesada.Productor;
                reporte.Parameters["Fet"].Value = pesada.Fet;
                reporte.Parameters["Localidad"].Value = pesada.Localidad;
                reporte.Parameters["Provincia"].Value = pesada.Provincia;
                reporte.Parameters["NumRomaneo"].Value = pesada.NumRomaneo;
                reporte.Parameters["Fecha"].Value = pesada.FechaRomaneo.Value
                    .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

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

                var total = Context.Pesada
                    .Where(x => x.Id == PesadaId)
                    .FirstOrDefault();
                if (total != null)
                {
                    reporte.Parameters["totalfardo"].Value = total.TotalFardo;
                    reporte.Parameters["totalKilos"].Value = total.TotalKg;
                    reporte.Parameters["ImporteBruto"].Value = total.ImporteBruto;
                }
                reporte.Parameters["Reimpresion"].Value = DevConstantes.Reimpresion;
                #endregion

                using (ReportPrintTool tool = new ReportPrintTool(reporte))
                {
                    reporte.ShowPreviewMarginLines = false;
                    tool.PreviewForm.Text = "Etiqueta";
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

        private void btnResumenCompra_Click(object sender, EventArgs e)
        {
            ResumenCompra();
        }

        private void ResumenCompra()
        {
            var reporte = new ResumenCompraReport();
            List<GridLiquidacionDetalle> datasourceDetalle;
            datasourceDetalle = GenerarReporteLiquidacionDetalle();
            reporte.DataSource = datasourceDetalle;
           
            reporte.Parameters["Tabaco"].Value = cbTabaco.Text;
            var año = dpDesdeRomaneo.Value.Date.Year;
            var mes = Convert.ToString(dpDesdeRomaneo.Value.Date.ToString("MMMM"));
            reporte.Parameters["Periodo"].Value = "CAMPAÑA " + año;
            reporte.Parameters["Mes"].Value = "MES DE " + mes.ToUpper();
            
          
            using (ReportPrintTool tool = new ReportPrintTool(reporte))
            {
                reporte.ShowPreviewMarginLines = false;
                tool.PreviewForm.Text = "Etiqueta";
                tool.ShowPreviewDialog();
            }

        }

        public List<GridLiquidacionDetalle> GenerarReporteLiquidacionDetalle()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            Expression<Func<Vw_ResumenRomaneoPorClase, bool>> pred = x => true;

            List<GridLiquidacionDetalle> datasource = new List<GridLiquidacionDetalle>();
            
            //pred = pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date &&
            //    x.FechaRomaneo <= dpHastaRomaneo.Value.Date);

            //pred = !string.IsNullOrEmpty(txtFet.Text) ? pred.And(x => x.ProductorId == ProductorId) : pred;


            var liquidacionDetalles = (
                from a in Context.Vw_ResumenRomaneoPorClase
                select new
                {
                    Clase = a.Clase,
                    Fardos = a.Fardos,
                    Kilos = a.Kilos,
                    PrecioClase = a.ClasePrecio,
                    Total = a.Total
                })
                .ToList();

            foreach (var liquidacionDetalle in liquidacionDetalles)
            {
                GridLiquidacionDetalle detalle = new GridLiquidacionDetalle();
                detalle.Clase = liquidacionDetalle.Clase;
                detalle.Fardos = liquidacionDetalle.Fardos;
                detalle.Kilos = liquidacionDetalle.Kilos;
                detalle.Total = liquidacionDetalle.Total;
                datasource.Add(detalle);
            }
            return datasource;
        }

        private void CreateIfMissing(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }

        private void btnReimpresionRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRomaneo.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewRomaneo.DataRowCount; i++)
                {
                    if (gridViewRomaneo.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewRomaneo.GetRowCellValue(i, "ID").ToString());
                        ImpimirRomaneo(Id);
                    }
                }
            }
        }

        private void btnResumenRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {
            ResumenRomaneo();
        }

        private void btnResumenCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            ResumenCompra();
        }

        private void btnResumenClasesMes_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnResumenClasesTrimestre_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnExportarRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}