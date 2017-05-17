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
                (from c in Context.Vw_TipoTabaco
                select new
                {
                    Id = c.id,
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

            Expression<Func<Vw_Pesada, bool>> pred2 = x => true;

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
                     Hora = m.Hora.Hours + ":" + m.Hora.Minutes,
                     Fardo = p.NumFardo,
                     Kilos = p.Kilos,
                     Clase = p.Clase,
                     Tabaco = p.DESCRIPCION,
                     Blend = pl.DESCRIPCION
                 })
                 .OrderBy(x=>x.Fardo)
                 .ToList();

            gridControlFardo.DataSource = movimientos;
            gridViewFardo.Columns[0].Visible = false;
            gridViewFardo.Columns[1].Visible = false;

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelProduccionTransferencia";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelProduccionTransferencia\"
            + Hora + " - ExcelProduccionTransferencia.xls";

            gridControlFardo.ExportToXls(fileName);
            StartProcess(fileName);
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
}