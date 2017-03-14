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
using CooperativaProduccion.Reports;
using System.Globalization;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.ReportModels;
using System.Linq.Expressions;
using Extensions;
using DevExpress.XtraPrinting;
using System.IO;
using System.Diagnostics;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoFiltroResumenCompra : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public string Origen;
        public Form_RomaneoFiltroResumenCompra(string path)
        {
            InitializeComponent();
            Iniciar(path);
        }

        private void Iniciar(string path)
        {
            Origen = path;
            dpDesdeRomaneo.Focus();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        private void CargarCombo()
        {
            var tipotabaco = Context.Vw_TipoTabaco
                .Where(x => x.RUBRO_ID != null)
                .ToList();

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";

            var provincia = Context.Vw_Provincia
                .ToList();

            cbProvincia.DataSource = provincia;
            cbProvincia.DisplayMember = "Nombre";
            cbProvincia.ValueMember = "Id";
            cbProvincia.Text = "Tucumán";
        }

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Origen.Equals(DevConstantes.ResumenRomaneo))
            {
                RomaneoExportToXLS();
            }
            else if (Origen.Equals(DevConstantes.ResumenCompra))
            {
                ResumenCompra();
            }
            else if (Origen.Equals(DevConstantes.ResumenClasesMes))
            {
                ResumenClasePorMes();
            }
            else if (Origen.Equals(DevConstantes.ResumenClasesTrimestre))
            {
                ResumenClasePorTrimestre();
            }
            this.Close();
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

        private List<RegistroResumenRomaneoVirginia> GenerarReporteResumenRomaneoVirginia()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var datasource = new List<RegistroResumenRomaneoVirginia>();
            var resumenVirginia = Context.Vw_ResumenRomaneoVirginia
                .Where(x => x.fechaRomaneo.Value >= dpDesdeRomaneo.Value.Date
                    && x.fechaRomaneo.Value <= dpHastaRomaneo.Value.Date)
                .OrderBy(x => x.NumRomaneo)
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

        private void ResumenCompra()
        {
            var reporte = new ResumenCompraReport();
            List<ResumenCompraPorMes> datasourceResumenCompraPorMes;
            datasourceResumenCompraPorMes = GenerarReporteResumenCompraPorMes();
            reporte.DataSource = datasourceResumenCompraPorMes;

            reporte.Parameters["cabecera"].Value = "RESUMEN DE COMPRA - " + cbTabaco.Text
               + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
               + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE TUCUMAN.- "
               + " Desde: " + dpDesdeRomaneo.Value.Date.ToShortDateString() 
               + " Hasta: " + dpDesdeRomaneo.Value.Date.ToShortDateString();

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

            pred = !string.IsNullOrEmpty(cbProvincia.Text) ? pred.And(x => x.provincia == cbProvincia.Text) : pred;

            var clases = Context.Vw_Clase
                    .Where(x => x.DESCRIPCION == cbTabaco.Text)
                                .ToList();

            var liquidacionDetalles =
                (from a in Context.Vw_ResumenCompraPorClase
                 .Where(pred)
                 group a by new
                 {
                     Clase = a.Clase,
                     Orden = a.orden
                 } into g
                 select new
                 {
                     Clase = g.Key.Clase,
                     Orden = g.Key.Orden,
                     Fardos = g.Sum(x => x.Fardos),
                     Kilos = g.Sum(x => x.Kilos),
                     Total = g.Sum(x => x.Importe)
                 })
                 .OrderBy(x => x.Orden)
                 .ToList();



            var liquidaciones = liquidacionDetalles
                .FullOuterJoin(clases, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.Orden)
                .ToList();

            foreach (var liquidacionDetalle in liquidaciones)
            {
                ResumenCompraPorMes detalle = new ResumenCompraPorMes();
                detalle.Clase = liquidacionDetalle.a == null ? liquidacionDetalle.b.NOMBRE : liquidacionDetalle.a.Clase;
                detalle.Fardos = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Fardos.Value.ToString();
                detalle.Kilos = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Kilos.Value.ToString();
                detalle.Importe = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Total.Value.ToString("F", culture);
                datasource.Add(detalle);
            }
            return datasource;
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

            pred = !string.IsNullOrEmpty(cbProvincia.Text) ? pred.And(x => x.provincia == cbProvincia.Text) : pred;

            var clases = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == cbTabaco.Text)
                .ToList();

            var resumen =
                (from r in context.Vw_ResumenClasePorFecha
                .Where(pred)
                 group r by new
                 {
                     Clase = r.Clase,
                     Orden = r.Orden,
                     PrecioPorKilo = r.PrecioPorKilo
                 } into g
                 select new
                 {
                     Clase = g.Key.Clase,
                     Kilos = g.Sum(x => x.Kilos),
                     PrecioPorKilo = g.Key.PrecioPorKilo,
                     Total = g.Sum(x => x.Importe)
                 })
                .ToList();

            var resumenes = resumen
                .FullOuterJoin(clases, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.Orden)
                .ToList();

            foreach (var item in resumenes)
            {
                ResumenClasePorMes detalle = result.Where(x => x.Clase == item.b.NOMBRE).SingleOrDefault();

                if (detalle == null)
                {
                    detalle = new ResumenClasePorMes();

                    detalle.Clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                    detalle.Kilos = item.a == null ? "0" : item.a.Kilos.Value.ToString();
                    detalle.PrecioPorKilo = item.a == null ? item.b.PRECIOCOMPRA.Value.ToString() : item.a.PrecioPorKilo.Value.ToString("F", culture);
                    detalle.Total = item.a == null ? "0" : item.a.Total.Value.ToString("F", culture);
                }
                else
                {
                    var kilos = item.a == null ? 0 : Convert.ToDecimal(detalle.Kilos) + Convert.ToDecimal(item.a.Kilos.Value);
                    var total = item.a == null ? 0 : Convert.ToDecimal(detalle.Total) + Convert.ToDecimal(item.a.Total.Value);

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
            var mes03 = desde.Month + 2;
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

            List<ResumenClasePorTrimestre> result = new List<ResumenClasePorTrimestre>();

            pred = pred.And(x =>
                x.Tabaco == tipotabaco &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            pred = !string.IsNullOrEmpty(cbProvincia.Text) ? pred.And(x => x.provincia == cbProvincia.Text) : pred;

            var clases = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == cbTabaco.Text)
                .ToList();

            var resumen = (from r in context.Vw_ResumenClasePorFecha
                .Where(pred)
                           group r by new
                           {
                               Mes = r.FechaRomaneo.Value.Month,
                               Clase = r.Clase,
                               Orden = r.Orden,
                               PrecioPorKilo = r.PrecioPorKilo
                           } into g
                           select new
                           {
                               Mes = g.Key.Mes,
                               Orden = g.Key.Orden,
                               Clase = g.Key.Clase,
                               Kilos = g.Sum(x => x.Kilos),
                               PrecioPorKilo = g.Key.PrecioPorKilo,
                               Total = g.Sum(x => x.Importe)
                           })
                .ToList();

            var resumenes = resumen
                .FullOuterJoin(clases, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.NOMBRE)
                .ThenBy(x => x.b.Orden)
                .ToList();


            foreach (var item in resumenes)
            {
                //if (item.a == null)
                //{
                //    resumenes
                //        .Where(x =>
                //            x.a != null &&
                //            x.a.Clase == item.b.NOMBRE &&
                //            x.a.Mes == mes01)
                //        
                //}

                var mesdeitem = item.a == null ? mes01 : item.a.Mes;
                var clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                ResumenClasePorTrimestre detalle = result.Where(x => x.Clase == clase).SingleOrDefault();

                if (detalle == null)
                {
                    var kilos01 = 0m;
                    var kilos02 = 0m;
                    var kilos03 = 0m;
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                    }
                    else if (mesdeitem == mes02)
                    {
                        kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                    }
                    else if (mesdeitem == mes03)
                    {
                        kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                    }

                    totalkilos = kilos01 + kilos02 + kilos03;

                    detalle = new ResumenClasePorTrimestre();

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Kilos01 = kilos01;
                    detalle.Kilos02 = kilos02;
                    detalle.Kilos03 = kilos03;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;

                    result.Add(detalle);
                }
                else
                {
                    var kilos01 = Convert.ToDecimal(detalle.Kilos01);
                    var kilos02 = Convert.ToDecimal(detalle.Kilos02);
                    var kilos03 = Convert.ToDecimal(detalle.Kilos03);
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                    }
                    else if (mesdeitem == mes02)
                    {
                        kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                    }
                    else if (mesdeitem == mes03)
                    {
                        kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                    }

                    totalkilos = kilos01 + kilos02 + kilos03;

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Kilos01 = kilos01;
                    detalle.Kilos02 = kilos02;
                    detalle.Kilos03 = kilos03;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;
                }
            }

            return result;
        }

        private void dpDesdeRomaneo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dpHastaRomaneo.Focus();
            }
        }

        private void dpHastaRomaneo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
               cbTabaco.Focus();
            }
        }

        private void cbTabaco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbProvincia.Focus();
            }
        }

        private void cbProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAceptar.Focus();
            }
        }

        private void btnAceptar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Origen.Equals(DevConstantes.ResumenRomaneo))
                {
                    RomaneoExportToXLS();
                }
                else if (Origen.Equals(DevConstantes.ResumenCompra))
                {
                    ResumenCompra();
                }
                else if (Origen.Equals(DevConstantes.ResumenClasesMes))
                {
                    ResumenClasePorMes();
                }
                else if (Origen.Equals(DevConstantes.ResumenClasesTrimestre))
                {
                    ResumenClasePorTrimestre();
                }
                this.Close();
            }
        }
    }
}