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
using System.Data.Entity.SqlServer;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoFiltroResumen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public string Origen;

        public Form_RomaneoFiltroResumen(string path)
        {
            InitializeComponent();
            Iniciar(path);
        }

        #region Method Code
  
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

            if (e.KeyChar == 8)
            {
                cbTabaco.Text = string.Empty;
            }
        }

        private void cbProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAceptar.Focus();
            }

            if (e.KeyChar == 8)
            {
                cbProvincia.Text = string.Empty;
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
                    if (!string.IsNullOrEmpty(cbTabaco.Text))
                    {
                        ResumenClasePorTrimestre();
                    }
                    else
                    {
                        ResumenClasePorTrimestreTabacos();
                    }
                }
                else if (Origen.Equals(DevConstantes.ResumenClasesQuincena))
                {
                    if (!string.IsNullOrEmpty(cbTabaco.Text))
                    {
                        ResumenClasePorQuincena();
                    }
                    else
                    {
                        ResumenClasePorTrimestreTabacos();
                    }
                }
                else if (Origen.Equals(DevConstantes.Liquidacion))
                {
                    LiquidacionExportToXLS();
                }
                this.Close();
            }
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
                if (!string.IsNullOrEmpty(cbTabaco.Text))
                {
                    ResumenClasePorTrimestre();
                }
                else
                {
                    ResumenClasePorTrimestreTabacos();
                }
            }
            else if (Origen.Equals(DevConstantes.ResumenClasesQuincena))
            {
                if (!string.IsNullOrEmpty(cbTabaco.Text))
                {
                    ResumenClasePorQuincena();
                }
                else
                {
                    ResumenClasePorQuincenaTabacos();
                }
            }
            else if (Origen.Equals(DevConstantes.Liquidacion))
            {
                LiquidacionExportToXLS();
            }
            this.Close();
        }
        
        #endregion

        #region Method Dev

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

        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
            }
            catch
            {
                throw;
            }
        }

        #region Romaneo Exportacion

        private void RomaneoExportToXLS()
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            if (cbTabaco.Text == DevConstantes.TabacoVirginia)
            {
                #region Tabaco Virginia

                path = @"C:\SystemDocumentsCooperativa\ResumenRomaneoVirginia";

                CreateIfMissing(path);

                var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                  CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
                  .Replace("-", "").Replace(" ", "");

                string fileName = @"C:\SystemDocumentsCooperativa\ResumenRomaneoVirginia\" 
                    + Hora + " - ResumenRomaneoVirginia.xls";

                // Create a report instance.
                var reporte = new ResumenRomaneoVirginiaReport();

                reporte.Parameters["cabecera"].Value = "RESUMEN DE ROMANEOS - " + cbTabaco.Text
                    + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper()
                    + " - PROVINCIA DE " + cbProvincia.Text.ToUpper() + ".-";

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

                #endregion
            }
            else if (cbTabaco.Text == DevConstantes.TabacoBurley)
            {
                #region Tabaco Burley

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
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper()
                    + " - PROVINCIA DE " + cbProvincia.Text.ToUpper() + ".-";

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
                #endregion
            }
        }

        private List<RegistroResumenRomaneoBurley> GenerarReporteResumenRomaneoBurley()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var datasource = new List<RegistroResumenRomaneoBurley>();
            var resumenBurley = Context.Vw_ResumenRomaneoBurley
                .Where(x => x.fechaRomaneo.Value >= dpDesdeRomaneo.Value.Date
                    && x.fechaRomaneo.Value <= dpHastaRomaneo.Value.Date
                    && x.provincia == cbProvincia.Text)
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
                registro.T1F = resumen.T1F.ToString();
                registro.T1FR = resumen.T1FR.ToString();
                registro.B1F = resumen.B1F.ToString();
                registro.B1FR = resumen.B1FR.ToString();
                registro.C1L = resumen.C1L.ToString();
                registro.C1F = resumen.C1F.ToString();
                registro.X1L = resumen.X1L.ToString();
                registro.X1F = resumen.X1F.ToString();
                registro.T2F = resumen.T2F.ToString();
                registro.T2FR = resumen.T2FR.ToString();
                registro.B2F = resumen.B2F.ToString();
                registro.B2FR = resumen.B2FR.ToString();
                registro.C2L = resumen.C2L.ToString();
                registro.C2F = resumen.C2F.ToString();
                registro.X2L = resumen.X2L.ToString();
                registro.X2F = resumen.X2F.ToString();
                registro.T3K = resumen.T3K.ToString();
                registro.C3L = resumen.C3L.ToString();
                registro.C3F = resumen.C3F.ToString();
                registro.C3K = resumen.C3K.ToString();
                registro.X3K = resumen.X3K.ToString();
                registro.B3F = resumen.B3F.ToString();
                registro.B3FR = resumen.B3FR.ToString();
                registro.B3K = resumen.B3K.ToString();
                registro.NG = resumen.NG.ToString();
                registro.NX = resumen.NX.ToString();
                registro.NB = resumen.NB.ToString();
                registro.Totalkg = resumen.Totalkg.Value.ToString("F", culture);
                registro.Importebruto = resumen.Importebruto.Value.ToString("F", culture);

                datasource.Add(registro);
            }
            return datasource;
        }

        private List<RegistroResumenRomaneoVirginia> GenerarReporteResumenRomaneoVirginia()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var datasource = new List<RegistroResumenRomaneoVirginia>();
            var resumenVirginia = Context.Vw_ResumenRomaneoVirginia
                .Where(x => x.fechaRomaneo.Value >= dpDesdeRomaneo.Value.Date
                    && x.fechaRomaneo.Value <= dpHastaRomaneo.Value.Date
                    && x.provincia == cbProvincia.Text)
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
                registro.T1F = resumen.T1F.ToString();
                registro.T1L = resumen.T1L.ToString();
                registro.B1F = resumen.B1F.ToString();
                registro.B1L = resumen.B1L.ToString();
                registro.C1F = resumen.C1F.ToString();
                registro.C1L = resumen.C1L.ToString();
                registro.X1F = resumen.X1F.ToString();
                registro.X1L = resumen.X1L.ToString();
                registro.T2F = resumen.T2F.ToString();
                registro.T2L = resumen.T2L.ToString();
                registro.T2KL = resumen.T2KL.ToString();
                registro.T2KF = resumen.T2KF.ToString();
                registro.B2F = resumen.B2F.ToString();
                registro.B2L = resumen.B2L.ToString();
                registro.B2KL = resumen.B2KL.ToString();
                registro.B2KF = resumen.B2KF.ToString();
                registro.C2F = resumen.C2F.ToString();
                registro.C2L = resumen.C2L.ToString();
                registro.C2K = resumen.C2K.ToString();
                registro.X2F = resumen.X2F.ToString();
                registro.X2L = resumen.X2L.ToString();
                registro.X2K = resumen.X2K.ToString();
                registro.B3F = resumen.B3F.ToString();
                registro.B3L = resumen.B3L.ToString();
                registro.B3KL = resumen.B3KL.ToString();
                registro.B3KF = resumen.B3KF.ToString();
                registro.C3F = resumen.C3F.ToString();
                registro.C3L = resumen.C3L.ToString();
                registro.C3K = resumen.C3K.ToString();
                registro.X3F = resumen.X3F.ToString();
                registro.X3L = resumen.X3L.ToString();
                registro.X3K = resumen.X3K.ToString();
                registro.B4F = resumen.B4F.ToString();
                registro.B4L = resumen.B4L.ToString();
                registro.C4F = resumen.C4F.ToString();
                registro.C4L = resumen.C4L.ToString();
                registro.X4F = resumen.X4F.ToString();
                registro.X4L = resumen.X4L.ToString();
                registro.NVX = resumen.NVX.ToString();
                registro.NVC = resumen.NVC.ToString();
                registro.NVB = resumen.NVB.ToString();
                registro.N5K = resumen.N5K.ToString();
                registro.N5X = resumen.N5X.ToString();
                registro.N5C = resumen.N5C.ToString();
                registro.N5B = resumen.N5B.ToString();
                registro.H1F = resumen.H1F.ToString();
                registro.H2F = resumen.H2F.ToString();
                registro.H3F = resumen.H3F.ToString();
                registro.Totalkg = resumen.Totalkg.Value.ToString("F", culture);
                registro.Importebruto = resumen.Importebruto.Value.ToString("F", culture);
                datasource.Add(registro);
            }
            return datasource;
        }

        #endregion

        #region Resumen de Compra
       
        private void ResumenCompra()
        {
            List<ResumenCompra> datasourceResumenCompra;

            datasourceResumenCompra = GenerarReporteResumenCompra();

            if (!string.IsNullOrEmpty(cbTabaco.Text) && !string.IsNullOrEmpty(cbProvincia.Text))
            {
                var reporte = new ResumenCompraReport();

                reporte.DataSource = datasourceResumenCompra;

                reporte.Parameters["cabecera"].Value = "RESUMEN DE COMPRA - " + cbTabaco.Text
                   + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year
                   + " - MES DE " + MonthName(dpDesdeRomaneo.Value.Month).ToUpper()
                   + " - PROVINCIA DE "
                   + cbProvincia.Text.ToUpper() + ".- "
                   + " Desde: " + dpDesdeRomaneo.Value.Date.ToShortDateString()
                   + " Hasta: " + dpHastaRomaneo.Value.Date.ToShortDateString();

                Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
                wr.Text = "Resumen de compra";
                wr.documentViewerReports.DocumentSource = reporte;
                wr.Show();
            }
            else
            {
                var reporte = new ResumenCompraTabacosReport();

                reporte.DataSource = datasourceResumenCompra;

                reporte.Parameters["cabecera"].Value = "RESUMEN DE COMPRA - CAMPAÑA "
                    + dpDesdeRomaneo.Value.Year + ".- "
                    + " Desde: " + dpDesdeRomaneo.Value.Date.ToShortDateString()
                    + " Hasta: " + dpHastaRomaneo.Value.Date.ToShortDateString();

                Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
                wr.Text = "Resumen de compra";
                wr.documentViewerReports.DocumentSource = reporte;
                wr.Show();
            }
        }

        public List<ResumenCompra> GenerarReporteResumenCompra()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");

            var Context = new CooperativaProduccionEntities();

            List<ResumenCompra> datasource = new List<ResumenCompra>();

            Expression<Func<Vw_ResumenCompraPorClase, bool>> pred = x => true;

            pred = pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date
                              && x.FechaRomaneo <= dpHastaRomaneo.Value.Date);

            if (!string.IsNullOrEmpty(cbTabaco.Text) && !string.IsNullOrEmpty(cbProvincia.Text))
            {
                #region Resumen con Filtros Tabaco y Provincia

                pred = !string.IsNullOrEmpty(cbProvincia.Text) ?
                    pred.And(x => x.provincia == cbProvincia.Text) : pred;

                pred = !string.IsNullOrEmpty(cbTabaco.Text) ?
                    pred.And(x => x.Tabaco == cbTabaco.Text) : pred;

                var clases = Context.Vw_Clase
                        .Where(x => x.DESCRIPCION == cbTabaco.Text)
                        .ToList();

                var liquidacionDetalles =
                    (from a in Context.Vw_ResumenCompraPorClase
                     .Where(pred)
                     group a by new
                     {
                         Clase = a.Clase,
                         Orden = a.orden,
                     } into g
                     select new
                     {
                         Clase = g.Key.Clase,
                         Orden = g.Key.Orden,
                         Fardos = g.Sum(x => x.Fardos),
                         Kilos = g.Sum(x => x.Kilos),
                         Total = g.Sum(x => x.Importe.Value)
                     })
                     .OrderBy(x => x.Orden)
                     .ToList();

                var liquidaciones = liquidacionDetalles
                    .FullOuterJoin(clases, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                    .OrderBy(x => x.b.Orden)
                    .ToList();

                foreach (var liquidacionDetalle in liquidaciones)
                {
                    ResumenCompra detalle = new ResumenCompra();
                    detalle.Clase = liquidacionDetalle.a == null ? liquidacionDetalle.b.NOMBRE : liquidacionDetalle.a.Clase;
                    detalle.Fardos = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Fardos.Value.ToString();
                    detalle.Kilos = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Kilos.Value.ToString();
                    detalle.Importe = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Total.ToString("F", culture);
                    datasource.Add(detalle);
                }
                return datasource;

                #endregion
            }
            else if(!string.IsNullOrEmpty(cbTabaco.Text) && string.IsNullOrEmpty(cbProvincia.Text))
            {
                #region Resumen con Filtro Tabaco y sin Provincia

                pred = !string.IsNullOrEmpty(cbTabaco.Text) ?
                    pred.And(x => x.Tabaco == cbTabaco.Text) : pred;

                var clases = Context.Vw_Clase
                        .Where(x => x.DESCRIPCION == cbTabaco.Text)
                        .ToList();

                var detalles =
                    (from a in Context.Vw_ResumenCompraPorClase
                    .Where(pred)
                    group a by new
                    {
                        Clase = a.Clase,
                        Tabaco = a.Tabaco,
                        Orden = a.orden,
                        Provincia = a.provincia
                    } into g
                    select new
                    {
                        Clase = g.Key.Clase,
                        Tabaco = g.Key.Tabaco,
                        Orden = g.Key.Orden,
                        Provincia = g.Key.Provincia,
                        Fardos = g.Sum(x => x.Fardos),
                        Kilos = g.Sum(x => x.Kilos),
                        Total = g.Sum(x => x.Importe)
                    })
                    .OrderBy(x => x.Provincia)
                    .ThenBy(x => x.Orden)
                    .ToList();

                var liquidaciones = detalles
                    .FullOuterJoin(clases, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                    .OrderByDescending(x => x.a != null ? x.a.Provincia : string.Empty)
                    .ThenBy(x => x.b.Orden)
                    .ThenBy(x => x.b.DESCRIPCION)
                    .ToList();

                foreach (var item in liquidaciones)
                {
                    ResumenCompra detalle = new ResumenCompra();
                    detalle.Clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                    detalle.Tabaco = item.a == null ? item.b.DESCRIPCION : item.a.Tabaco;
                    detalle.Fardos = item.a == null ? "0" : item.a.Fardos.Value.ToString();
                    detalle.Kilos = item.a == null ? "0" : item.a.Kilos.Value.ToString();
                    detalle.Importe = item.a == null ? "0" : item.a.Total.Value.ToString("F", culture);
                    detalle.Provincia = item.a == null ? string.Empty : item.a.Provincia;
                    datasource.Add(detalle);
                }
                return datasource;

                #endregion
            }
            else
            {
                #region Resumen sin Filtros Tabaco y Provincia
                
                #region Tabaco Burley

                pred = pred.And(x=>x.Tabaco == DevConstantes.TabacoBurley);

                pred = !string.IsNullOrEmpty(cbProvincia.Text) ? 
                    pred.And(x => x.provincia == cbProvincia.Text) : pred;

                var clasesBurley = Context.Vw_Clase
                    .Where(x => x.DESCRIPCION == DevConstantes.TabacoBurley)
                    .ToList();

                var detallesBurley =
                    (from a in Context.Vw_ResumenCompraPorClase
                     .Where(pred)
                     group a by new
                     {
                         Clase = a.Clase,
                         Tabaco = a.Tabaco,
                         Orden = a.orden,
                         Provincia = a.provincia
                     } into g
                     select new
                     {
                         Clase = g.Key.Clase,
                         Tabaco = g.Key.Tabaco,
                         Orden = g.Key.Orden,
                         Provincia = g.Key.Provincia,
                         Fardos = g.Sum(x => x.Fardos),
                         Kilos = g.Sum(x => x.Kilos),
                         Total = g.Sum(x => x.Importe)
                     })
                     .OrderBy(x => x.Provincia)
                     .ThenBy(x => x.Orden)
                     .ToList();

                var detallesB = detallesBurley
                    .FullOuterJoin(clasesBurley, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                    .OrderByDescending(x => x.a != null ? x.a.Provincia : string.Empty)
                    .ThenBy(x => x.b.Orden)
                    .ThenBy(x => x.b.DESCRIPCION)
                    .ToList();
                              
                foreach (var item in detallesB)
                {
                    ResumenCompra detalle = new ResumenCompra();
                    detalle.Clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                    detalle.Tabaco = item.a == null ? item.b.DESCRIPCION : item.a.Tabaco;
                    detalle.Fardos = item.a == null ? "0" : item.a.Fardos.Value.ToString();
                    detalle.Kilos = item.a == null ? "0" : item.a.Kilos.Value.ToString();
                    detalle.Importe = item.a == null ? "0" : item.a.Total.Value.ToString("F", culture);
                    detalle.Provincia = item.a == null ? string.Empty : item.a.Provincia;
                    datasource.Add(detalle);
                }
                #endregion

                #region Tabaco Virginia

                Expression<Func<Vw_ResumenCompraPorClase, bool>> pred2 = x => true;

                pred2 = pred2.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date
                                  && x.FechaRomaneo <= dpHastaRomaneo.Value.Date);

                pred2 = pred2.And(x => x.Tabaco == DevConstantes.TabacoVirginia);

                pred2 = !string.IsNullOrEmpty(cbProvincia.Text) ? 
                    pred2.And(x => x.provincia == cbProvincia.Text) : pred2;

                var clasesVirginia = Context.Vw_Clase
                    .Where(x => x.DESCRIPCION == DevConstantes.TabacoVirginia)
                    .ToList();

                var detallesVirginia =
                    (from a in Context.Vw_ResumenCompraPorClase
                     .Where(pred2)
                     group a by new
                     {
                         Clase = a.Clase,
                         Tabaco = a.Tabaco,
                         Orden = a.orden,
                         Provincia = a.provincia
                     } into g
                     select new
                     {
                         Clase = g.Key.Clase,
                         Tabaco = g.Key.Tabaco,
                         Orden = g.Key.Orden,
                         Provincia = g.Key.Provincia,
                         Fardos = g.Sum(x => x.Fardos),
                         Kilos = g.Sum(x => x.Kilos),
                         Total = g.Sum(x => x.Importe)
                     })
                     .OrderBy(x => x.Provincia)
                     .ThenBy(x => x.Orden)
                     .ToList();

                var detallesV = detallesVirginia
                    .FullOuterJoin(clasesVirginia, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                    .OrderByDescending(x => x.a != null ? x.a.Provincia : string.Empty)
                    .ThenBy(x => x.b != null ? x.b.Orden : null)
                    .ThenBy(x=>x.b != null ? x.b.DESCRIPCION : null)
                    .ToList();

                foreach (var item in detallesV)
                {
                    ResumenCompra detalle = new ResumenCompra();
                    detalle.Clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                    detalle.Tabaco = item.a == null ? item.b.DESCRIPCION : item.a.Tabaco;
                    detalle.Fardos = item.a == null ? "0" : item.a.Fardos.Value.ToString();
                    detalle.Kilos = item.a == null ? "0" : item.a.Kilos.Value.ToString();
                    detalle.Importe = item.a == null ? "0" : item.a.Total.Value.ToString("F", culture);
                    detalle.Provincia = item.a == null ? string.Empty : item.a.Provincia;
                    datasource.Add(detalle);
                }
                #endregion

                return datasource;
                
                #endregion
            }
            //var clases = Context.Vw_Clase
            //        .Where(x => x.DESCRIPCION == cbTabaco.Text)
            //        .ToList();

            //var liquidacionDetalles =
            //    (from a in Context.Vw_ResumenCompraPorClase
            //     .Where(pred)
            //     group a by new
            //     {
            //         Clase = a.Clase,
            //         Orden = a.orden
            //     } into g
            //     select new
            //     {
            //         Clase = g.Key.Clase,
            //         Orden = g.Key.Orden,
            //         Fardos = g.Sum(x => x.Fardos),
            //         Kilos = g.Sum(x => x.Kilos),
            //         Total = g.Sum(x =>x.Importe.Value)
            //     })
            //     .OrderBy(x => x.Orden)
            //     .ToList();

            //var liquidaciones = liquidacionDetalles
            //    .FullOuterJoin(clases, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
            //    .OrderBy(x => x.b.Orden)
            //    .ToList();

            //foreach (var liquidacionDetalle in liquidaciones)
            //{
            //    ResumenCompra detalle = new ResumenCompra();
            //    detalle.Clase = liquidacionDetalle.a == null ? liquidacionDetalle.b.NOMBRE : liquidacionDetalle.a.Clase;
            //    detalle.Fardos = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Fardos.Value.ToString();
            //    detalle.Kilos = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Kilos.Value.ToString();
            //    detalle.Importe = liquidacionDetalle.a == null ? "0" : liquidacionDetalle.a.Total.ToString("F", culture);
            //    datasource.Add(detalle);
            //}
            //return datasource;
        }
        
        #endregion

        #region Resumen Clase por Mes
        
        private void ResumenClasePorMes()
        {
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

            if (!string.IsNullOrEmpty(cbTabaco.Text))
            {
                var reporte = new ResumenDeClasePorMesReport();
                var datasource = GenerarReporteResumenClasePorMes(desde, hasta, tipotabaco);

                reporte.DataSource = datasource;
                reporte.Parameters["Empresa"].Value = "Cooperativa de Productores Agropecuarios del Tucuman Ltda.";
                reporte.Parameters["TipoDeTabaco"].Value = tipotabaco;
                reporte.Parameters["Provincia"].Value = cbProvincia.Text;
                reporte.Parameters["Mes"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToTitleCase(mes);
                reporte.Parameters["Desde"].Value = desde.ToShortDateString();
                reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

                Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
                wr.Text = "Resumen por mes";
                wr.documentViewerReports.DocumentSource = reporte;
                wr.Show();
            }
            else
            {
                var reporte = new ResumenDeClasePorMesTabacosReport();
                var datasource = GenerarReporteResumenClasePorMes(desde, hasta, string.Empty);

                reporte.DataSource = datasource;
                reporte.Parameters["Empresa"].Value = "Cooperativa de Productores Agropecuarios del Tucuman Ltda.";
                reporte.Parameters["TipoDeTabaco"].Value = DevConstantes.TabacoBurley + " - " + DevConstantes.TabacoVirginia;
                reporte.Parameters["Provincia"].Value = cbProvincia.Text;
                reporte.Parameters["Mes"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToTitleCase(mes);
                reporte.Parameters["Desde"].Value = desde.ToShortDateString();
                reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

                Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
                wr.Text = "Resumen por mes";
                wr.documentViewerReports.DocumentSource = reporte;
                wr.Show();
            }
        }

        public List<ResumenClasePorMes> GenerarReporteResumenClasePorMes(DateTime desde, DateTime hasta, string tipotabaco)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
                
            if (!string.IsNullOrEmpty(tipotabaco))
            {
                Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;
            
                List<ResumenClasePorMes> result = new List<ResumenClasePorMes>();

                pred = pred.And(x =>
                    x.Tabaco == tipotabaco &&
                    x.FechaRomaneo >= desde &&
                    x.FechaRomaneo <= hasta);

                pred = !string.IsNullOrEmpty(cbProvincia.Text) ?
                    pred.And(x => x.provincia == cbProvincia.Text) : pred;

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
            else
            {
                var tabacoBurley = DevConstantes.TabacoBurley;
                var tabacoVirginia = DevConstantes.TabacoVirginia;
        
                List<ResumenClasePorMes> result = new List<ResumenClasePorMes>();

                #region Tabaco Burley

                Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

                pred = pred.And(x =>
                    x.Tabaco == tabacoBurley &&
                    x.FechaRomaneo >= desde &&
                    x.FechaRomaneo <= hasta);

                pred = !string.IsNullOrEmpty(cbProvincia.Text) ?
                    pred.And(x => x.provincia == cbProvincia.Text) : pred;

                var clasesBurley = Context.Vw_Clase
                    .Where(x => x.DESCRIPCION == tabacoBurley)
                    .ToList();

                var resumenBurley =
                    (from r in context.Vw_ResumenClasePorFecha
                    .Where(pred)
                     group r by new
                     {
                         Clase = r.Clase,
                         Tabaco = r.Tabaco,
                         Orden = r.Orden,
                         PrecioPorKilo = r.PrecioPorKilo
                     } into g
                     select new
                     {
                         Clase = g.Key.Clase,
                         Tabaco = g.Key.Tabaco,
                         Orden = g.Key.Orden,
                         Kilos = g.Sum(x => x.Kilos),
                         PrecioPorKilo = g.Key.PrecioPorKilo,
                         Total = g.Sum(x => x.Importe)
                     })
                    .ToList();

                var resumenesBurley = resumenBurley
                    .FullOuterJoin(clasesBurley, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                    .OrderBy(x => x.b.Orden)
                    .ToList();

                foreach (var item in resumenesBurley)
                {
                    ResumenClasePorMes detalle = result.Where(x => x.Clase == item.b.NOMBRE).SingleOrDefault();

                    if (detalle == null)
                    {
                        detalle = new ResumenClasePorMes();

                        detalle.Clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                        detalle.Tabaco = item.a == null ? item.b.DESCRIPCION : item.a.Tabaco;
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
                #endregion

                #region Tabaco Virginia
                List<ResumenClasePorMes> resultV = new List<ResumenClasePorMes>();

                Expression<Func<Vw_ResumenClasePorFecha, bool>> pred2 = x => true;

                pred2 = pred2.And(x =>
                    x.Tabaco == tabacoVirginia &&
                    x.FechaRomaneo >= desde &&
                    x.FechaRomaneo <= hasta);

                pred2 = !string.IsNullOrEmpty(cbProvincia.Text) ?
                    pred2.And(x => x.provincia == cbProvincia.Text) : pred2;

                var clasesVirginia = Context.Vw_Clase
                    .Where(x => x.DESCRIPCION == tabacoVirginia)
                    .ToList();

                var resumenVirginia =
                    (from r in context.Vw_ResumenClasePorFecha
                    .Where(pred2)
                     group r by new
                     {
                         Clase = r.Clase,
                         Tabaco = r.Tabaco,
                         Orden = r.Orden,
                         PrecioPorKilo = r.PrecioPorKilo
                     } into g
                     select new
                     {
                         Clase = g.Key.Clase,
                         Tabaco = g.Key.Tabaco,
                         Orden = g.Key.Orden,
                         Kilos = g.Sum(x => x.Kilos),
                         PrecioPorKilo = g.Key.PrecioPorKilo,
                         Total = g.Sum(x => x.Importe)
                     })
                    .ToList();

                var resumenesVirginia = resumenVirginia
                    .FullOuterJoin(clasesVirginia, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                    .OrderBy(x => x.b.Orden.Value)
                    .ToList();

                foreach (var item in resumenesVirginia)
                {
                    ResumenClasePorMes detalle = resultV.Where(x => x.Clase == item.b.NOMBRE).SingleOrDefault();

                    if (detalle == null)
                    {
                        detalle = new ResumenClasePorMes();

                        detalle.Clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                        detalle.Tabaco = item.a == null ? item.b.DESCRIPCION : item.a.Tabaco;
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
                #endregion

                return result;

            }
        }

        #endregion

        #region Resumen Clase por Trimestre

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
            reporte.Parameters["Provincia"].Value = cbProvincia.Text.ToUpper();
            reporte.Parameters["Mes01"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01);
            reporte.Parameters["Mes02"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02);
            reporte.Parameters["Mes03"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes03);
            reporte.Parameters["Desde"].Value = desde.ToShortDateString();
            reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

            Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
            wr.Text = "Resumen por trimestre";
            wr.documentViewerReports.DocumentSource = reporte;
            wr.Show();
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

        private void ResumenClasePorTrimestreTabacos()
        {
            var reporte = new ResumenDeClasePorTrimestreTabacosReport();
            var desde = dpDesdeRomaneo.Value.Date;
            var hasta = dpHastaRomaneo.Value.Date;

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

            var anio = desde.Year.ToString();
            var mes01 = new DateTime(desde.Year, desde.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var mes02 = new DateTime(desde.Year, desde.Month + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var mes03 = new DateTime(desde.Year, desde.Month + 2, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var datasource = GenerarReporteResumenClasePorTrimestreTabacos(desde, hasta);

            reporte.DataSource = datasource;
            reporte.Parameters["Campaña"].Value = anio + ".-";
            reporte.Parameters["TipoDeTabaco"].Value = DevConstantes.TabacoBurley + " - " + DevConstantes.TabacoVirginia;
            reporte.Parameters["Provincia"].Value = cbProvincia.Text.ToUpper();
            reporte.Parameters["Mes01"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01);
            reporte.Parameters["Mes02"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02);
            reporte.Parameters["Mes03"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes03);
            reporte.Parameters["Desde"].Value = desde.ToShortDateString();
            reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

            Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
            wr.Text = "Resumen por trimestre";
            wr.documentViewerReports.DocumentSource = reporte;
            wr.Show();
        }

        public List<ResumenClasePorTrimestre> GenerarReporteResumenClasePorTrimestreTabacos(DateTime desde, DateTime hasta)
        {
            var mes01 = desde.Month;
            var mes02 = desde.Month + 1;
            var mes03 = desde.Month + 2;
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
            var tabacoBurley = DevConstantes.TabacoBurley;
            var tabacoVirginia = DevConstantes.TabacoVirginia;
            List<ResumenClasePorTrimestre> result = new List<ResumenClasePorTrimestre>();

            #region Tabaco Burley

            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

            pred = pred.And(x =>
                x.Tabaco == tabacoBurley &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            pred = !string.IsNullOrEmpty(cbProvincia.Text) ?
                pred.And(x => x.provincia == cbProvincia.Text) : pred;

            var clasesBurley = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == tabacoBurley)
                .ToList();

            var resumenBurley = (from r in context.Vw_ResumenClasePorFecha
                .Where(pred)
                                 group r by new
                                 {
                                     Mes = r.FechaRomaneo.Value.Month,
                                     Clase = r.Clase,
                                     Tabaco = r.Tabaco,
                                     Orden = r.Orden,
                                     PrecioPorKilo = r.PrecioPorKilo
                                 } into g
                                 select new
                                 {
                                     Mes = g.Key.Mes,
                                     Orden = g.Key.Orden,
                                     Clase = g.Key.Clase,
                                     Tabaco = g.Key.Tabaco,
                                     Kilos = g.Sum(x => x.Kilos),
                                     PrecioPorKilo = g.Key.PrecioPorKilo,
                                     Total = g.Sum(x => x.Importe)
                                 })
                .ToList();

            var resumenesBurley = resumenBurley
                .FullOuterJoin(clasesBurley, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.Orden)
                .ToList();


            foreach (var item in resumenesBurley)
            {
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
                    detalle.Tabaco = item.b.DESCRIPCION;
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
                    detalle.Tabaco = item.b.DESCRIPCION;
                    detalle.Kilos01 = kilos01;
                    detalle.Kilos02 = kilos02;
                    detalle.Kilos03 = kilos03;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;
                }
            }
            #endregion

            #region Tabaco Virginia
            List<ResumenClasePorTrimestre> resultV = new List<ResumenClasePorTrimestre>();
            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred2 = x => true;

            pred2 = pred2.And(x =>
                x.Tabaco == tabacoVirginia &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            pred2 = !string.IsNullOrEmpty(cbProvincia.Text) ?
                pred2.And(x => x.provincia == cbProvincia.Text) : pred2;

            var clasesVirginia = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == tabacoVirginia)
                .ToList();

            var resumenVirginia = (from r in context.Vw_ResumenClasePorFecha
                .Where(pred2)
                                   group r by new
                                   {
                                       Mes = r.FechaRomaneo.Value.Month,
                                       Clase = r.Clase,
                                       Tabaco = r.Tabaco,
                                       Orden = r.Orden,
                                       PrecioPorKilo = r.PrecioPorKilo
                                   } into g
                                   select new
                                   {
                                       Mes = g.Key.Mes,
                                       Orden = g.Key.Orden,
                                       Clase = g.Key.Clase,
                                       Tabaco = g.Key.Tabaco,
                                       Kilos = g.Sum(x => x.Kilos),
                                       PrecioPorKilo = g.Key.PrecioPorKilo,
                                       Total = g.Sum(x => x.Importe)
                                   })
                .ToList();

            var resumenesVirginia = resumenVirginia
                .FullOuterJoin(clasesVirginia, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.Orden)
                .ToList();


            foreach (var item in resumenesVirginia)
            {
                var mesdeitem = item.a == null ? mes01 : item.a.Mes;
                var clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                ResumenClasePorTrimestre detalle = resultV.Where(x => x.Clase == clase).SingleOrDefault();

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
                    detalle.Tabaco = item.b.DESCRIPCION;
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
                    detalle.Tabaco = item.b.DESCRIPCION;
                    detalle.Kilos01 = kilos01;
                    detalle.Kilos02 = kilos02;
                    detalle.Kilos03 = kilos03;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;
                }
            }
            #endregion

            return result;
        }

        #endregion

        #region Resumen Clase por Quincena

        private void ResumenClasePorQuincena()
        {
            var reporte = new ResumenDeClasePorQuincenaReport();
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

            if ((hasta.Month - desde.Month) == 2)
            {
                MessageBox.Show("El rango de fecha seleccionado debe ser entre dos meses.",
                    "Fecha fuera de rango",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrEmpty(tipotabaco))
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
          
            var datasource = GenerarReporteResumenClasePorQuincena(desde, hasta, tipotabaco);

            reporte.DataSource = datasource;
            reporte.Parameters["Campaña"].Value = anio + ".-";
            reporte.Parameters["TipoDeTabaco"].Value = tipotabaco;
            reporte.Parameters["Provincia"].Value = cbProvincia.Text.ToUpper();
            reporte.Parameters["Quincena01"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01) + " 1ra Quincena";
            reporte.Parameters["Quincena02"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01) + " 2da Quincena";
            if (hasta.Month != desde.Month && hasta.Year == desde.Year)
            {
                reporte.Parameters["Quincena03"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02) + " 1ra Quincena";
                reporte.Parameters["Quincena04"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02) + " 2da Quincena";
            }
            else
            {
                reporte.Parameters["Quincena03"].Value = string.Empty;
                reporte.Parameters["Quincena04"].Value = string.Empty;
            }
            reporte.Parameters["Desde"].Value = desde.ToShortDateString();
            reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

            Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
            wr.Text = "Resumen por quincena";
            wr.documentViewerReports.DocumentSource = reporte;
            wr.Show();
        }

        public List<ResumenClasePorQuincena> GenerarReporteResumenClasePorQuincena(DateTime desde, DateTime hasta, string tipotabaco)
        {
            int primerdia = 1;
            int mediodia = 15;
            int ultimodiames1 = DateTime.DaysInMonth(desde.Year, desde.Month);
            int ultimodiames2 = DateTime.DaysInMonth(hasta.Year, hasta.Month);
            var mes01 = desde.Month;
            var mes02 = desde.Month + 1;

            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

            List<ResumenClasePorQuincena> result = new List<ResumenClasePorQuincena>();

            pred = pred.And(x =>
                x.Tabaco == tipotabaco &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            pred = !string.IsNullOrEmpty(cbProvincia.Text) ?
                pred.And(x => x.provincia == cbProvincia.Text) : pred;

            var clases = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == cbTabaco.Text)
                .ToList();

            var resumen =
                (from r in context.Vw_ResumenClasePorFecha
                 .Where(pred)
                 group r by new
                 {
                     Dia = r.FechaRomaneo.Value.Day,
                     Mes = r.FechaRomaneo.Value.Month,
                     Clase = r.Clase,
                     Orden = r.Orden,
                     PrecioPorKilo = r.PrecioPorKilo
                 } into g
                 select new
                 {
                     Dia = g.Key.Dia,
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
                var mesdeitem = item.a == null ? mes01 : item.a.Mes;
                var diadeitem = item.a == null ? mes01 : item.a.Dia;
                var clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                ResumenClasePorQuincena detalle = result.Where(x => x.Clase == clase).SingleOrDefault();

                if (detalle == null)
                {
                    var kilos01 = 0m;
                    var kilos02 = 0m;
                    var kilos03 = 0m;
                    var kilos04 = 0m;
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames1)
                        {
                            kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }
                    else if (mesdeitem == mes02)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames2)
                        {
                            kilos04 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }

                    totalkilos = kilos01 + kilos02 + kilos03 + kilos04;

                    detalle = new ResumenClasePorQuincena();

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Quincena01 = kilos01;
                    detalle.Quincena02 = kilos02;
                    detalle.Quincena03 = kilos03;
                    detalle.Quincena04 = kilos04;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;

                    result.Add(detalle);
                }
                else
                {
                    var kilos01 = Convert.ToDecimal(detalle.Quincena01);
                    var kilos02 = Convert.ToDecimal(detalle.Quincena02);
                    var kilos03 = Convert.ToDecimal(detalle.Quincena03);
                    var kilos04 = Convert.ToDecimal(detalle.Quincena04);
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames1)
                        {
                            kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }
                    else if (mesdeitem == mes02)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames2)
                        {
                            kilos04 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }

                    totalkilos = kilos01 + kilos02 + kilos03 + kilos04;

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Quincena01 = kilos01;
                    detalle.Quincena02 = kilos02;
                    detalle.Quincena03 = kilos03;
                    detalle.Quincena04 = kilos04;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;
                }
            }

            return result;
        }

        private void ResumenClasePorQuincenaTabacos()
        {
            var reporte = new ResumenDeClasePorQuincenaTabacosReport();
            var desde = dpDesdeRomaneo.Value.Date;
            var hasta = dpHastaRomaneo.Value.Date;

            if (desde.Year != hasta.Year)
            {
                MessageBox.Show("El rango de fecha seleccionado debe tener el mismo año.",
                    "Fecha fuera de rango",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if ((hasta.Month - desde.Month) == 2)
            {
                MessageBox.Show("El rango de fecha seleccionado debe ser entre tres meses.",
                    "Fecha fuera de rango",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var anio = desde.Year.ToString();
            var mes01 = new DateTime(desde.Year, desde.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            var mes02 = new DateTime(desde.Year, desde.Month + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));

            var datasource = GenerarReporteResumenClasePorQuincenaTabacos(desde, hasta);

            reporte.DataSource = datasource;
            reporte.Parameters["Campaña"].Value = anio + ".-";
            reporte.Parameters["TipoDeTabaco"].Value = DevConstantes.TabacoBurley + " - " + DevConstantes.TabacoVirginia;
            reporte.Parameters["Provincia"].Value = cbProvincia.Text.ToUpper();
            reporte.Parameters["Quincena01"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01) + " 1ra Quincena";
            reporte.Parameters["Quincena02"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes01) + " 2da Quincena";
            reporte.Parameters["Quincena03"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02) + " 1ra Quincena";
            reporte.Parameters["Quincena04"].Value = CultureInfo.CreateSpecificCulture("es").TextInfo.ToUpper(mes02) + " 2da Quincena";
            reporte.Parameters["Desde"].Value = desde.ToShortDateString();
            reporte.Parameters["Hasta"].Value = hasta.ToShortDateString();

            Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
            wr.Text = "Resumen por quincena";
            wr.documentViewerReports.DocumentSource = reporte;
            wr.Show();
        }

        public List<ResumenClasePorQuincena> GenerarReporteResumenClasePorQuincenaTabacos(DateTime desde, DateTime hasta)
        {
            int primerdia = 1;
            int mediodia = 15;
            int ultimodiames1 = DateTime.DaysInMonth(desde.Year, desde.Month);
            int ultimodiames2 = DateTime.DaysInMonth(hasta.Year, hasta.Month);
            var mes01 = desde.Month;
            var mes02 = desde.Month + 1;
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var context = new CooperativaProduccionEntities();
            var tabacoBurley = DevConstantes.TabacoBurley;
            var tabacoVirginia = DevConstantes.TabacoVirginia;
            List<ResumenClasePorQuincena> result = new List<ResumenClasePorQuincena>();

            #region Tabaco Burley

            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred = x => true;

            pred = pred.And(x =>
                x.Tabaco == tabacoBurley &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            pred = !string.IsNullOrEmpty(cbProvincia.Text) ?
                pred.And(x => x.provincia == cbProvincia.Text) : pred;

            var clasesBurley = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == tabacoBurley)
                .ToList();

            var resumenBurley = (from r in context.Vw_ResumenClasePorFecha
                .Where(pred)
                                 group r by new
                                 {
                                     Dia = r.FechaRomaneo.Value.Day,
                                     Mes = r.FechaRomaneo.Value.Month,
                                     Clase = r.Clase,
                                     Tabaco = r.Tabaco,
                                     Orden = r.Orden,
                                     PrecioPorKilo = r.PrecioPorKilo
                                 } into g
                                 select new
                                 {
                                     Dia = g.Key.Dia,
                                     Mes = g.Key.Mes,
                                     Orden = g.Key.Orden,
                                     Clase = g.Key.Clase,
                                     Tabaco = g.Key.Tabaco,
                                     Kilos = g.Sum(x => x.Kilos),
                                     PrecioPorKilo = g.Key.PrecioPorKilo,
                                     Total = g.Sum(x => x.Importe)
                                 })
                                 .ToList();

            var resumenesBurley = resumenBurley
                .FullOuterJoin(clasesBurley, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.Orden)
                .ThenBy(x => x.b.DESCRIPCION)
                .ToList();

            foreach (var item in resumenesBurley)
            {
                var mesdeitem = item.a == null ? mes01 : item.a.Mes;
                var diadeitem = item.a == null ? mes01 : item.a.Dia;
                var clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                ResumenClasePorQuincena detalle = result.Where(x => x.Clase == clase).SingleOrDefault();

                if (detalle == null)
                {
                    var kilos01 = 0m;
                    var kilos02 = 0m;
                    var kilos03 = 0m;
                    var kilos04 = 0m;
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames1)
                        {
                            kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }
                    else if (mesdeitem == mes02)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames2)
                        {
                            kilos04 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }

                    totalkilos = kilos01 + kilos02 + kilos03 + kilos04;

                    detalle = new ResumenClasePorQuincena();

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Tabaco = item.b.DESCRIPCION;
                    detalle.Quincena01 = kilos01;
                    detalle.Quincena02 = kilos02;
                    detalle.Quincena03 = kilos03;
                    detalle.Quincena04 = kilos04;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;

                    result.Add(detalle);
                }
                else
                {
                    var kilos01 = Convert.ToDecimal(detalle.Quincena01);
                    var kilos02 = Convert.ToDecimal(detalle.Quincena02);
                    var kilos03 = Convert.ToDecimal(detalle.Quincena03);
                    var kilos04 = Convert.ToDecimal(detalle.Quincena04);
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames1)
                        {
                            kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }
                    else if (mesdeitem == mes02)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames2)
                        {
                            kilos04 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }

                    totalkilos = kilos01 + kilos02 + kilos03 + kilos04;

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Tabaco = item.b.DESCRIPCION;
                    detalle.Quincena01 = kilos01;
                    detalle.Quincena02 = kilos02;
                    detalle.Quincena03 = kilos03;
                    detalle.Quincena04 = kilos04;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;
                }
            }
            #endregion

            #region Tabaco Virginia

            List<ResumenClasePorQuincena> resultV = new List<ResumenClasePorQuincena>();
            Expression<Func<Vw_ResumenClasePorFecha, bool>> pred2 = x => true;

            pred2 = pred2.And(x =>
                x.Tabaco == tabacoVirginia &&
                x.FechaRomaneo >= desde &&
                x.FechaRomaneo <= hasta);

            pred2 = !string.IsNullOrEmpty(cbProvincia.Text) ?
                pred2.And(x => x.provincia == cbProvincia.Text) : pred2;

            var clasesVirginia = Context.Vw_Clase
                .Where(x => x.DESCRIPCION == tabacoVirginia)
                .ToList();

            var resumenVirginia =
                (from r in context.Vw_ResumenClasePorFecha
                .Where(pred2)
                 group r by new
                 {
                     Dia = r.FechaRomaneo.Value.Day,
                     Mes = r.FechaRomaneo.Value.Month,
                     Clase = r.Clase,
                     Tabaco = r.Tabaco,
                     Orden = r.Orden,
                     PrecioPorKilo = r.PrecioPorKilo
                 } into g
                 select new
                 {
                     Dia = g.Key.Dia,
                     Mes = g.Key.Mes,
                     Orden = g.Key.Orden,
                     Clase = g.Key.Clase,
                     Tabaco = g.Key.Tabaco,
                     Kilos = g.Sum(x => x.Kilos),
                     PrecioPorKilo = g.Key.PrecioPorKilo,
                     Total = g.Sum(x => x.Importe)
                 })
                .ToList();

            var resumenesVirginia = resumenVirginia
                .FullOuterJoin(clasesVirginia, a => a.Clase, b => b.NOMBRE, (a, b, Clases) => new { a, b })
                .OrderBy(x => x.b.Orden)
                .ThenBy(x => x.b.DESCRIPCION)
                .ToList();


            foreach (var item in resumenesVirginia)
            {
                var mesdeitem = item.a == null ? mes01 : item.a.Mes;
                var diadeitem = item.a == null ? mes01 : item.a.Dia;
                var clase = item.a == null ? item.b.NOMBRE : item.a.Clase;
                ResumenClasePorQuincena detalle = resultV.Where(x => x.Clase == clase).SingleOrDefault();

                if (detalle == null)
                {
                    var kilos01 = 0m;
                    var kilos02 = 0m;
                    var kilos03 = 0m;
                    var kilos04 = 0m;
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames1)
                        {
                            kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }
                    else if (mesdeitem == mes02)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames2)
                        {
                            kilos04 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }

                    totalkilos = kilos01 + kilos02 + kilos03 + kilos04;

                    detalle = new ResumenClasePorQuincena();

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Tabaco = item.b.DESCRIPCION;
                    detalle.Quincena01 = kilos01;
                    detalle.Quincena02 = kilos02;
                    detalle.Quincena03 = kilos03;
                    detalle.Quincena04 = kilos04;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;

                    result.Add(detalle);
                }
                else
                {
                    var kilos01 = Convert.ToDecimal(detalle.Quincena01);
                    var kilos02 = Convert.ToDecimal(detalle.Quincena02);
                    var kilos03 = Convert.ToDecimal(detalle.Quincena03);
                    var kilos04 = Convert.ToDecimal(detalle.Quincena04);
                    var totalkilos = 0m;

                    if (mesdeitem == mes01)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos01 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames1)
                        {
                            kilos02 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }
                    else if (mesdeitem == mes02)
                    {
                        if (diadeitem >= primerdia && diadeitem <= mediodia)
                        {
                            kilos03 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                        else if (diadeitem > mediodia && diadeitem <= ultimodiames2)
                        {
                            kilos04 += item.a == null ? 0 : Convert.ToDecimal(item.a.Kilos);
                        }
                    }

                    totalkilos = kilos01 + kilos02 + kilos03 + kilos04;

                    detalle.Clase = item.b.NOMBRE;
                    detalle.Tabaco = item.b.DESCRIPCION;
                    detalle.Quincena01 = kilos01;
                    detalle.Quincena02 = kilos02;
                    detalle.Quincena03 = kilos03;
                    detalle.Quincena04 = kilos04;
                    detalle.TotalKilos = totalkilos;
                    detalle.PrecioPorKilo = item.b.PRECIOCOMPRA.Value;
                }
            }
            #endregion

            return result;
        }

        #endregion

        #region Liquidacion Exportacion

        private void LiquidacionExportToXLS()
        {
            string fileName = string.Empty;

            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion";

            CreateIfMissing(path);

            if (string.IsNullOrEmpty(cbTabaco.Text) || string.IsNullOrEmpty(cbProvincia.Text))
            {
                #region Liquidacion Ambos Tabacos

                path = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion";

                CreateIfMissing(path);

                path = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion\ResumenLiquidacionTabacos";

                CreateIfMissing(path);

                var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                  CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
                  .Replace("-", "").Replace(" ", "");

                fileName = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion\ResumenLiquidacionTabacos"
                    + Hora + " - ResumenLiquidacionTabacos.xls";

                // Create a report instance.
                var reporte = new ResumenLiquidacionTabacosReport();

                reporte.Parameters["cabecera"].Value = "RESUMEN DE LIQUIDACIONES - "
                    + DevConstantes.TabacoBurley + " - " + DevConstantes.TabacoVirginia
                    + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE "
                    + cbProvincia.Text.ToUpper() + ".-";

                List<RegistroResumenLiquidacion> datasourceLiquidacion;
                datasourceLiquidacion = GenerarReporteResumenLiquidacion();
                reporte.DataSource = datasourceLiquidacion;

                // Get its XLS export options.
                XlsExportOptions xlsOptions = reporte.ExportOptions.Xls;

                // Set XLS-specific export options.
                xlsOptions.ShowGridLines = true;
                xlsOptions.TextExportMode = TextExportMode.Value;

                // Export the report to XLS.
                reporte.ExportToXls(fileName);

                // Show the result.
                StartProcess(fileName);

                #endregion
            }
            else if (cbTabaco.Text == DevConstantes.TabacoVirginia && !string.IsNullOrEmpty(cbProvincia.Text))
            {
                #region Liquidacion Tabaco Virginia
                
                path = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion\ResumenLiquidacionVirginia";

                CreateIfMissing(path);

                var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                  CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
                  .Replace("-", "").Replace(" ", "");

                fileName = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion\ResumenLiquidacionVirginia\"
                    + Hora + " - ResumenLiquidacionVirginia.xls";
                var reporte = new ResumenLiquidacionReport();

                reporte.Parameters["cabecera"].Value = "RESUMEN DE LIQUIDACIONES - "
                    + cbTabaco.Text.ToUpper()
                    + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE "
                    + cbProvincia.Text.ToUpper() + ".-";

                List<RegistroResumenLiquidacion> datasourceLiquidacion;
                datasourceLiquidacion = GenerarReporteResumenLiquidacion();
                reporte.DataSource = datasourceLiquidacion;

                // Get its XLS export options.
                XlsxExportOptions xlsOptions = reporte.ExportOptions.Xlsx;

                // Set XLS-specific export options.
                xlsOptions.ShowGridLines = true;
                xlsOptions.TextExportMode = TextExportMode.Value;

                // Export the report to XLS.
                reporte.ExportToXls(fileName);

                // Show the result.
                StartProcess(fileName);

                #endregion
            }
            else if (cbTabaco.Text == DevConstantes.TabacoBurley && !string.IsNullOrEmpty(cbProvincia.Text))
            {
                #region Liquidacion Tabaco Burley

                path = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion\ResumenLiquidacionBurley";

                CreateIfMissing(path);

                var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                  CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
                  .Replace("-", "").Replace(" ", "");

                fileName = @"C:\SystemDocumentsCooperativa\ResumenLiquidacion\ResumenLiquidacionBurley\"
                    + Hora + " - ResumenLiquidacionBurley.xls";
                var reporte = new ResumenLiquidacionReport();

                reporte.Parameters["cabecera"].Value = "RESUMEN DE LIQUIDACIONES - "
                    + cbTabaco.Text.ToUpper()
                    + " - CAMPAÑA " + dpDesdeRomaneo.Value.Year + " - MES DE "
                    + MonthName(dpDesdeRomaneo.Value.Month).ToUpper() + " - PROVINCIA DE "
                    + cbProvincia.Text.ToUpper() + ".-";

                List<RegistroResumenLiquidacion> datasourceLiquidacion;
                datasourceLiquidacion = GenerarReporteResumenLiquidacion();
                reporte.DataSource = datasourceLiquidacion;

                // Get its XLS export options.
                XlsxExportOptions xlsOptions = reporte.ExportOptions.Xlsx;

                // Set XLS-specific export options.
                xlsOptions.ShowGridLines = true;
                xlsOptions.TextExportMode = TextExportMode.Value;

                // Export the report to XLS.
                reporte.ExportToXls(fileName);

                // Show the result.
                StartProcess(fileName);

                #endregion
            }
        }

        private List<RegistroResumenLiquidacion> GenerarReporteResumenLiquidacion()
        {
            if (!string.IsNullOrEmpty(cbTabaco.Text) && !string.IsNullOrEmpty(cbProvincia.Text))
            {
                #region Liquidacion con Filtro Tabaco y Provincia
               
                Expression<Func<Vw_Romaneo, bool>> pred = x => true;

                pred = pred.And(x => x.FechaInternaLiquidacion >= dpDesdeRomaneo.Value.Date
                    && x.FechaInternaLiquidacion <= dpHastaRomaneo.Value.Date);

                pred = !string.IsNullOrEmpty(cbTabaco.Text) ? 
                    pred.And(x => x.Tabaco == cbTabaco.Text) : pred;

                pred = !string.IsNullOrEmpty(cbProvincia.Text) ? 
                    pred.And(x => x.Provincia == cbProvincia.Text) : pred;

                pred = pred.And(x => x.NumInternoLiquidacion != null);

                List<RegistroResumenLiquidacion> datasource = new List<RegistroResumenLiquidacion>();

                var resumenLiquidacion = Context.Vw_Romaneo
                    .Where(pred)
                    .OrderBy(x => x.NumInternoLiquidacion)
                    .ToList();

                foreach (var resumen in resumenLiquidacion)
                {
                    RegistroResumenLiquidacion registro = new RegistroResumenLiquidacion();
                    registro.FechaInternaLiquidacion = resumen.FechaInternaLiquidacion.Value.ToShortDateString();
                    registro.nrofet = resumen.nrofet;
                    registro.NOMBRE = resumen.NOMBRE;
                    registro.CUIT = resumen.CUIT;

                    if (resumen.IVA == DevConstantes.MTS)
                    {
                        registro.IVA = DevConstantes.MonotributoSocial;
                    }
                    else if (resumen.IVA == DevConstantes.MT)
                    {
                        registro.IVA = DevConstantes.Monotributo;
                    }
                    else if (resumen.IVA == DevConstantes.RI)
                    {
                        registro.IVA = DevConstantes.ResponsableInscripto;
                    }
                    else if (resumen.IVA == DevConstantes.TP)
                    {
                        registro.IVA = DevConstantes.TrabajadorPromovido;
                    }

                    registro.TC = DevConstantes.LI;
                    registro.Letra = resumen.Letra;
                    registro.PuntoVentaLiquidacion = resumen.PuntoVentaLiquidacion.Value.ToString();
                    registro.NumInternoLiquidacion = resumen.NumInternoLiquidacion.Value.ToString();
                    registro.TotalKg = resumen.TotalKg.Value.ToString();
                    registro.ImporteNeto = resumen.ImporteNeto.Value.ToString();
                    registro.IvaCalculado = resumen.IvaCalculado.Value.ToString();
                    registro.ImporteBruto = resumen.ImporteBruto.Value.ToString();

                    datasource.Add(registro);
                }

                Expression<Func<Liquidacion, bool>> pred2 = x => true;

                pred2 = pred2.And(x => x.Fecha >= dpDesdeRomaneo.Value.Date
                    && x.Fecha <= dpHastaRomaneo.Value.Date);

                pred2 = !string.IsNullOrEmpty(cbTabaco.Text) ? 
                    pred2.And(x => x.Tabaco == cbTabaco.Text) : pred2;

                Expression<Func<Vw_Productor, bool>> pred3 = x => true;

                pred3 = !string.IsNullOrEmpty(cbProvincia.Text) ? 
                    pred3.And(x => x.Provincia == cbProvincia.Text) : pred3;

                    var ajustes =
                    (from l in Context.Liquidacion.Where(pred2)
                     join p in Context.Vw_Productor.Where(pred3)
                     on l.ProductorId equals p.ID
                     select new
                     {
                         LiquidacionId = l.Id,
                         Fecha = l.Fecha,
                         NumInternoLiquidacion = l.NumInternoLiquidacion,
                         Nombre = p.NOMBRE,
                         Cuit = p.CUIT,
                         Fet = p.nrofet,
                         Provincia = p.Provincia,
                         Letra = l.Letra,
                         Iva = p.IVA,
                         ImporteNeto = l.ImporteNeto,
                         IvaCalculado = l.IvaCalculado,
                         ImporteBruto = l.Total
                     })
                     .OrderBy(x => x.NumInternoLiquidacion)
                     .ToList();

                foreach (var item in ajustes)
                {
                    RegistroResumenLiquidacion registro = new RegistroResumenLiquidacion();
                    registro.FechaInternaLiquidacion = item.Fecha.ToShortDateString();
                    registro.nrofet = item.Fet;
                    registro.NOMBRE = item.Nombre;
                    registro.CUIT = item.Cuit;

                    if (item.Iva == DevConstantes.MTS)
                    {
                        registro.IVA = DevConstantes.MonotributoSocial;
                    }
                    else if (item.Iva == DevConstantes.MT)
                    {
                        registro.IVA = DevConstantes.Monotributo;
                    }
                    else if (item.Iva == DevConstantes.RI)
                    {
                        registro.IVA = DevConstantes.ResponsableInscripto;
                    }
                    else if (item.Iva == DevConstantes.TP)
                    {
                        registro.IVA = DevConstantes.TrabajadorPromovido;
                    }

                    registro.TC = DevConstantes.LI;
                    registro.Letra = item.Letra;
                    //registro.PuntoVentaLiquidacion = item.PuntoVentaLiquidacion.Value.ToString();
                    registro.NumInternoLiquidacion = item.NumInternoLiquidacion.ToString();
                    //registro.TotalKg = resumen.TotalKg.Value.ToString();
                    registro.ImporteNeto = item.ImporteNeto.ToString();
                    registro.IvaCalculado = item.IvaCalculado.ToString();
                    registro.ImporteBruto = item.ImporteBruto.ToString();

                    datasource.Add(registro);
                }
                return datasource;

                #endregion
            }
            else 
            {
                #region Liquidacion con sin Filtro Tabaco y Provincia

                Expression<Func<Vw_Romaneo, bool>> pred = x => true;

                pred = pred.And(x => x.FechaInternaLiquidacion >= dpDesdeRomaneo.Value.Date
                    && x.FechaInternaLiquidacion <= dpHastaRomaneo.Value.Date);

                pred = pred.And(x => x.NumInternoLiquidacion != null);

                pred = !string.IsNullOrEmpty(cbTabaco.Text) ? 
                    pred.And(x => x.Tabaco == cbTabaco.Text) : pred;
                
                pred = !string.IsNullOrEmpty(cbProvincia.Text) ? 
                    pred.And(x => x.Provincia == cbProvincia.Text) : pred;

                List<RegistroResumenLiquidacion> datasource = new List<RegistroResumenLiquidacion>();

                var resumenLiquidacion = Context.Vw_Romaneo
                    .Where(pred)
                    .OrderBy(x => x.Provincia)
                    .ThenBy(x => x.Tabaco)
                    .ToList();

                foreach (var resumen in resumenLiquidacion)
                {
                    RegistroResumenLiquidacion registro = new RegistroResumenLiquidacion();
                    registro.FechaInternaLiquidacion = resumen.FechaInternaLiquidacion.Value.ToShortDateString();
                    registro.nrofet = resumen.nrofet;
                    registro.NOMBRE = resumen.NOMBRE;
                    registro.CUIT = resumen.CUIT;

                    if (resumen.IVA == DevConstantes.MTS)
                    {
                        registro.IVA = DevConstantes.MonotributoSocial;
                    }
                    else if (resumen.IVA == DevConstantes.MT)
                    {
                        registro.IVA = DevConstantes.Monotributo;
                    }
                    else if (resumen.IVA == DevConstantes.RI)
                    {
                        registro.IVA = DevConstantes.ResponsableInscripto;
                    }
                    else if (resumen.IVA == DevConstantes.TP)
                    {
                        registro.IVA = DevConstantes.TrabajadorPromovido;
                    }
                    registro.Tabaco = resumen.Tabaco;
                    registro.Provincia = resumen.Provincia;
                    registro.TC = DevConstantes.LI;
                    registro.Letra = resumen.Letra;
                    registro.PuntoVentaLiquidacion = resumen.PuntoVentaLiquidacion.Value.ToString();
                    registro.NumInternoLiquidacion = resumen.NumInternoLiquidacion.Value.ToString();
                    registro.TotalKg = resumen.TotalKg.Value.ToString();
                    registro.ImporteNeto = resumen.ImporteNeto.Value.ToString();
                    registro.IvaCalculado = resumen.IvaCalculado.Value.ToString();
                    registro.ImporteBruto = resumen.ImporteBruto.Value.ToString();

                    datasource.Add(registro);
                }

                Expression<Func<Liquidacion, bool>> pred2 = x => true;

                pred2 = pred2.And(x => x.Fecha >= dpDesdeRomaneo.Value.Date
                    && x.Fecha <= dpHastaRomaneo.Value.Date);

                pred2 = !string.IsNullOrEmpty(cbTabaco.Text) ?
                    pred2.And(x => x.Tabaco == cbTabaco.Text) : pred2;

                Expression<Func<Vw_Productor, bool>> pred3 = x => true;

                pred3 = !string.IsNullOrEmpty(cbProvincia.Text) ?
                    pred3.And(x => x.Provincia == cbProvincia.Text) : pred3;

                var ajustes =
                (from l in Context.Liquidacion.Where(pred2)
                 join p in Context.Vw_Productor.Where(pred3)
                 on l.ProductorId equals p.ID
                 select new
                 {
                     LiquidacionId = l.Id,
                     Fecha = l.Fecha,
                     NumInternoLiquidacion = l.NumInternoLiquidacion,
                     Nombre = p.NOMBRE,
                     Cuit = p.CUIT,
                     Fet = p.nrofet,
                     Provincia = p.Provincia,
                     Letra = l.Letra,
                     Iva = p.IVA,
                     ImporteNeto = l.ImporteNeto,
                     IvaCalculado = l.IvaCalculado,
                     ImporteBruto = l.Total
                 })
                 .OrderBy(x => x.NumInternoLiquidacion)
                 .ToList();

                foreach (var item in ajustes)
                {
                    RegistroResumenLiquidacion registro = new RegistroResumenLiquidacion();
                    registro.FechaInternaLiquidacion = item.Fecha.ToShortDateString();
                    registro.nrofet = item.Fet;
                    registro.NOMBRE = item.Nombre;
                    registro.CUIT = item.Cuit;

                    if (item.Iva == DevConstantes.MTS)
                    {
                        registro.IVA = DevConstantes.MonotributoSocial;
                    }
                    else if (item.Iva == DevConstantes.MT)
                    {
                        registro.IVA = DevConstantes.Monotributo;
                    }
                    else if (item.Iva == DevConstantes.RI)
                    {
                        registro.IVA = DevConstantes.ResponsableInscripto;
                    }
                    else if (item.Iva == DevConstantes.TP)
                    {
                        registro.IVA = DevConstantes.TrabajadorPromovido;
                    }

                    registro.TC = DevConstantes.LI;
                    registro.Letra = item.Letra;
                    //registro.PuntoVentaLiquidacion = item.PuntoVentaLiquidacion.Value.ToString();
                    registro.NumInternoLiquidacion = item.NumInternoLiquidacion.ToString();
                    //registro.TotalKg = resumen.TotalKg.Value.ToString();
                    registro.ImporteNeto = item.ImporteNeto.ToString();
                    registro.IvaCalculado = item.IvaCalculado.ToString();
                    registro.ImporteBruto = item.ImporteBruto.ToString();

                    datasource.Add(registro);
                }

                return datasource;

                #endregion
            }
        }

        #endregion
        
        #endregion
    }
}