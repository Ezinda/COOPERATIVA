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
                .OrderBy(x=>x.NUMROMANEO)
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

        private List<RegistroResumenRomaneoVirginia> GenerarReporteResumenRomaneoVirginia()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var datasource = new List<RegistroResumenRomaneoVirginia>();
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
                registro.Totalkg = resumen.Totalkg.Value.ToString("F", culture);
                registro.Importebruto = resumen.Importebruto.Value.ToString("F", culture);
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
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var datasource = new List<RegistroResumenRomaneoBurley>();
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
                registro.Totalkg = resumen.Totalkg.Value.ToString("F", culture);
                registro.Importebruto = resumen.Importebruto.Value.ToString("F", culture);

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

        private void ResumenClasePorMes()
        {
            var reporte = new ResumenDeClasePorMesReport();
            var desde = dpDesdeRomaneo.Value.Date;
            var hasta = dpHastaRomaneo.Value.Date;
            var mes = desde.ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var tipotabaco = cbTabaco.Text;

            if (desde.Month != hasta.Month || desde.Year != hasta.Year)
            {
                MessageBox.Show("El rango de fecha seleccionado debe tener el mismo mes y año.",
                    "Fecha fuera de rango",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (String.IsNullOrEmpty(tipotabaco))
            {
                MessageBox.Show("Se debe seleccionar un tipo de tabaco.",
                    "Tipo de tabaco no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var datasource = GenerarReporteResumenClasePorMes(desde, hasta, tipotabaco);

            reporte.DataSource = datasource;
            reporte.Parameters["Empresa"].Value = "Cooperativa de Productores Agropecuarios del Tucuman Ltda.";
            reporte.Parameters["TipoDeTabaco"].Value = tipotabaco;
            reporte.Parameters["Provincia"].Value = "Tucumán";
            reporte.Parameters["Mes"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToTitleCase(mes);
            reporte.Parameters["Desde"].Value = desde.ToShortDateString();
            reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

            using (ReportPrintTool tool = new ReportPrintTool(reporte))
            {
                reporte.ShowPreviewMarginLines = false;
                tool.PreviewForm.Text = "Etiqueta";
                tool.ShowPreviewDialog();
            }
        }

        public List<ResumenClasePorMes> GenerarReporteResumenClasePorMes(DateTime desde, DateTime hasta, string tipotabaco)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

            List<ResumenClasePorMes> result = new List<ResumenClasePorMes>();

            pred = pred.And(x =>
                x.Tabaco == tipotabaco &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            var resumen = context.Vw_ResumenClasePorFecha
                .Where(pred)
                .Select(x => new
                {
                    Clase = x.Clase,
                    Kilos = x.Kilos,
                    PrecioPorKilo = x.PrecioPorKilo,
                    Total = x.Importe
                })
                .OrderBy(x => x.Clase)
                .ToList();

            foreach (var item in resumen)
            {
                ResumenClasePorMes detalle = result.Where(x => x.Clase == item.Clase).SingleOrDefault();

                if (detalle == null)
                {
                    detalle = new ResumenClasePorMes();

                    detalle.Clase = item.Clase;
                    detalle.Kilos = item.Kilos.Value.ToString();
                    detalle.PrecioPorKilo = item.PrecioPorKilo.Value.ToString("F", culture);
                    detalle.Total = item.Total.Value.ToString("F", culture);
                }
                else
                {
                    var kilos = Convert.ToDecimal(detalle.Kilos) + Convert.ToDecimal(item.Kilos.Value);
                    var total = Convert.ToDecimal(detalle.Total) + Convert.ToDecimal(item.Total.Value);

                    detalle.Kilos = kilos.ToString("F0");
                    detalle.Total = total.ToString("F", culture);
                }

                result.Add(detalle);
            }

            return result;
        }

        private void ResumenClasePorTrimestre()
        {
            var reporte = new ResumenDeClasePorTrimestreReport();
            var desde = dpDesdeRomaneo.Value.Date;
            var hasta = dpHastaRomaneo.Value.Date;
            var tipotabaco = cbTabaco.Text;

            if (desde.Year != hasta.Year)
            {
                MessageBox.Show("El rango de fecha seleccionado debe tener el mismo año.",
                    "Fecha fuera de rango",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if ((hasta.Month - desde.Month) != 2)
            {
                MessageBox.Show("El rango de fecha seleccionado debe ser entre tres meses.",
                    "Fecha fuera de rango",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (String.IsNullOrEmpty(tipotabaco))
            {
                MessageBox.Show("Se debe seleccionar un tipo de tabaco.",
                    "Tipo de tabaco no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var anio = desde.Year.ToString();
            var mes01 = new DateTime(desde.Year, desde.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var mes02 = new DateTime(desde.Year, desde.Month + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var mes03 = new DateTime(desde.Year, desde.Month + 2, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var datasource = GenerarReporteResumenClasePorTrimestre(desde, hasta, tipotabaco);

            reporte.DataSource = datasource;
            reporte.Parameters["Campaña"].Value = anio + ".-";
            reporte.Parameters["TipoDeTabaco"].Value = tipotabaco;
            reporte.Parameters["Provincia"].Value = "TUCUMAN";
            reporte.Parameters["Mes01"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01);
            reporte.Parameters["Mes02"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02);
            reporte.Parameters["Mes03"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes03);
            reporte.Parameters["Desde"].Value = desde.ToShortDateString();
            reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

            using (ReportPrintTool tool = new ReportPrintTool(reporte))
            {
                reporte.ShowPreviewMarginLines = false;
                tool.PreviewForm.Text = "Etiqueta";
                tool.ShowPreviewDialog();
            }
        }

        public List<ResumenClasePorTrimestre> GenerarReporteResumenClasePorTrimestre(DateTime desde, DateTime hasta, string tipotabaco)
        {
            var mes01 = desde.Month;
            var mes02 = desde.Month + 1;
            var mes03 = desde.Month + 1;
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

            List<ResumenClasePorTrimestre> result = new List<ResumenClasePorTrimestre>();

            pred = pred.And(x =>
                x.Tabaco == tipotabaco &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            var resumen = context.Vw_ResumenClasePorFecha
                .Where(pred)
                .Select(x => new
                {
                    Fecha = x.FechaRomaneo,
                    Clase = x.Clase,
                    Kilos = x.Kilos,
                    PrecioPorKilo = x.PrecioPorKilo,
                    Total = x.Importe
                })
                .OrderBy(x => x.Clase)
                .ToList();

            foreach (var item in resumen)
            {
                var mesdeitem = item.Fecha.Value.Month;
                ResumenClasePorTrimestre detalle = result.Where(x => x.Clase == item.Clase).SingleOrDefault();

                if (detalle == null)
                {
                    var kilos01 = 0m;
                    var kilos02 = 0m;
                    var kilos03 = 0m;
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        kilos01 += Convert.ToDecimal(item.Kilos);
                    }
                    else if (mesdeitem == mes02)
                    {
                        kilos02 += Convert.ToDecimal(item.Kilos);
                    }
                    else if (mesdeitem == mes03)
                    {
                        kilos03 += Convert.ToDecimal(item.Kilos);
                    }

                    totalkilos = kilos01 + kilos02 + kilos03;

                    detalle = new ResumenClasePorTrimestre();

                    detalle.Clase = item.Clase;
                    detalle.Kilos01 = kilos01;
                    detalle.Kilos02 = kilos02;
                    detalle.Kilos03 = kilos03;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.PrecioPorKilo.Value;
                }
                else
                {
                    var kilos01 = Convert.ToDecimal(detalle.Kilos01);
                    var kilos02 = Convert.ToDecimal(detalle.Kilos02);
                    var kilos03 = Convert.ToDecimal(detalle.Kilos03);
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        kilos01 += Convert.ToDecimal(item.Kilos);
                    }
                    else if (mesdeitem == mes02)
                    {
                        kilos02 += Convert.ToDecimal(item.Kilos);
                    }
                    else if (mesdeitem == mes03)
                    {
                        kilos03 += Convert.ToDecimal(item.Kilos);
                    }

                    totalkilos = kilos01 + kilos02 + kilos03;

                    detalle = new ResumenClasePorTrimestre();

                    detalle.Clase = item.Clase;
                    detalle.Kilos01 = kilos01;
                    detalle.Kilos02 = kilos02;
                    detalle.Kilos03 = kilos03;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.PrecioPorKilo.Value;
                }

                result.Add(detalle);
            }

            return result;
        }

        private void ResumenCompra()
        {
            var reporte = new ResumenCompraReport();
            List<ResumenCompraPorMes> datasourceResumenCompraPorMes;
            datasourceResumenCompraPorMes = GenerarReporteResumenCompraPorMes();
            reporte.DataSource = datasourceResumenCompraPorMes;

            reporte.Parameters["cabecera"].Value = "RESUMEN DE COMPRA - " + cbTabaco.Text
               + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
               + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE TUCUMAN.-";
          
            using (ReportPrintTool tool = new ReportPrintTool(reporte))
            {
                reporte.ShowPreviewMarginLines = false;
                tool.PreviewForm.Text = "Etiqueta";
                tool.ShowPreviewDialog();
            }

        }

        public List<ResumenCompraPorMes> GenerarReporteResumenCompraPorMes()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var Context = new CooperativaProduccionEntities();

            Expression<Func<Vw_ResumenCompraPorClase, bool>> pred = x => true;

            List<ResumenCompraPorMes> datasource = new List<ResumenCompraPorMes>();
            
            pred = pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date &&
                x.FechaRomaneo <= dpHastaRomaneo.Value.Date);

            pred = !string.IsNullOrEmpty(cbTabaco.Text) ? pred.And(x => x.Tabaco == cbTabaco.Text) : pred;
            
            var liquidacionDetalles = (
                from a in Context.Vw_ResumenCompraPorClase
                    .Where(pred)
                select new
                {
                    Clase = a.Clase,
                    Fardos = a.Fardos,
                    Kilos = a.Kilos,
                    Total = a.Importe
                })
                .OrderBy(x=>x.Clase)
                .ToList();

            foreach (var liquidacionDetalle in liquidacionDetalles)
            {
                ResumenCompraPorMes detalle = new ResumenCompraPorMes();
                detalle.Clase = liquidacionDetalle.Clase;
                detalle.Fardos = liquidacionDetalle.Fardos.Value.ToString();
                detalle.Kilos = liquidacionDetalle.Kilos.Value.ToString();
                detalle.Importe = liquidacionDetalle.Total.Value.ToString("F", culture);
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
            RomaneoExportToXLS();
        }

        private void btnResumenCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            ResumenCompra();
        }

        private void btnResumenClasesMes_ItemClick(object sender, ItemClickEventArgs e)
        {
            ResumenClasePorMes();
        }

        private void btnResumenClasesTrimestre_ItemClick(object sender, ItemClickEventArgs e)
        {
            ResumenClasePorTrimestre();
        }

        private void btnExportarRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

       
    }
}