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
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using EntityFramework.Extensions;
using CooperativaProduccion.Reports;
using DevExpress.XtraPrinting;

namespace CooperativaProduccion
{
    public partial class Form_ProduccionGestionProduccion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_ProduccionGestionProduccion()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        private void CargarCombo()
        {
            var producto = 
                (from c in Context.Vw_Producto
                select new
                {
                    Id = c.ID,
                    Descripcion = c.DESCRIPCION
                })
                .OrderBy(x => x.Descripcion)
                .ToList();

            cbProducto.DataSource = producto;
            cbProducto.DisplayMember = "Descripcion";
            cbProducto.ValueMember = "Id";
        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkTabaco_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            Expression<Func<FardoEnProduccion, bool>> pred = x => true;

            pred = pred.And(x => x.Fecha >= dpDesde.Value.Date);

            pred = pred.And(x => x.Fecha <= dpHasta.Value.Date);

            Guid ProductoId = Guid.Parse(cbProducto.SelectedValue.ToString());

            pred = checkProducto.Checked ? pred.And(x => x.ProductoId == ProductoId) : pred;

            var movimientos =
                (from m in Context.FardoEnProduccion
                    .Where(pred)
                 join p in Context.Vw_Pesada
                     on m.PesadaDetalleId equals p.PesadaDetalleId
                 join d in Context.Vw_Producto
                     on m.ProductoId equals d.ID into pp
                     from pl in pp.DefaultIfEmpty()
                 select new
                 {
                     Id = m.Id,
                     PesadaDetalleId = m.PesadaDetalleId,
                     Fecha = m.Fecha,
                     Hora = m.Hora,
                     Fardo = p.NumFardo,
                     Kilos = p.Kilos,
                     Clase = p.Clase,
                     Tabaco = p.DESCRIPCION,
                     Blend = pl.DESCRIPCION
                 })
                 .OrderBy(x=>x.Fardo)
                 .ToList();

            List<ProduccionGrid> datasource = new List<ProduccionGrid>();
            
            foreach (var item in movimientos)
            {
                ProduccionGrid grid = new ProduccionGrid();
                grid.Id = item.Id;
                grid.PesadaDetalleId = item.PesadaDetalleId;
                grid.Fecha = item.Fecha;
                grid.Hora = item.Hora.ToString(@"hh\:mm", CultureInfo.CurrentCulture);
                grid.Fardo = item.Fardo.Value.ToString();
                grid.Kilos = item.Kilos.Value;
                grid.Clase = item.Clase;
                grid.Tabaco = item.Tabaco;
                grid.Blend = item.Blend;

                datasource.Add(grid);
            }

            gridControlFardo.DataSource = datasource;
            gridViewFardo.Columns[0].Visible = false;
            gridViewFardo.Columns[1].Visible = false;

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelProduccion";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelProduccion\"
            + Hora + " - ExcelProduccion.xls";

            // Create a report instance.
            var reporte = new ResumenProduccionPorHoraReport();

            List<FardoEnProduccionPorHora> datasourceProduccion;
            datasourceProduccion = GenerarReporteResumenProduccion();
            reporte.DataSource = datasourceProduccion;

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

        private List<FardoEnProduccionPorHora> GenerarReporteResumenProduccion()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var datasource = new List<FardoEnProduccionPorHora>();
            var resumenes= Context.Vw_FardoEnProduccionPorHora
                .Where(x => x.Fecha >= dpDesde.Value.Date
                    && x.Fecha <= dpHasta.Value.Date)
                .OrderBy(x => x.Fecha)
                .ThenBy(x=>x.Nombre)
                .ToList();

          

            foreach (var resumen in resumenes)
            {
                FardoEnProduccionPorHora registro = new FardoEnProduccionPorHora();
                registro.Fecha = resumen.Fecha.ToShortDateString();
                registro.Nombre = resumen.Nombre;
                registro.TipoTabaco = resumen.TipoTabaco;
                registro.cantidad = resumen.cantidad;
                registro.C06 = resumen.C06;
                registro.C07 = resumen.C07;
                registro.C08 = resumen.C08;
                registro.C09 = resumen.C09;
                registro.C10 = resumen.C10;
                registro.C11 = resumen.C11;
                registro.C12 = resumen.C12;
                registro.C13 = resumen.C13;
                registro.C14 = resumen.C14;
                registro.C15 = resumen.C15;
                registro.C16 = resumen.C16;
                registro.C17 = resumen.C17;
                registro.C18 = resumen.C18;
                registro.C19 = resumen.C19;
                registro.C20 = resumen.C20;
                registro.total = resumen.total;

                datasource.Add(registro);
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

    }

    class ProduccionGrid
    {
        public System.Guid Id { get; set; }
        public System.Guid PesadaDetalleId { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Fardo { get; set; }
        public double Kilos { get; set; }
        public string Clase { get; set; }
        public string Tabaco { get; set; }
        public string Blend { get; set; }

    }

    class FardoEnProduccionPorHora
    {
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string TipoTabaco { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<double> total { get; set; }
        public double C06 { get; set; }
        public double C07 { get; set; }
        public double C08 { get; set; }
        public double C09 { get; set; }
        public double C10 { get; set; }
        public double C11 { get; set; }
        public double C12 { get; set; }
        public double C13 { get; set; }
        public double C14 { get; set; }
        public double C15 { get; set; }
        public double C16 { get; set; }
        public double C17 { get; set; }
        public double C18 { get; set; }
        public double C19 { get; set; }
        public double C20 { get; set; }
    }
}