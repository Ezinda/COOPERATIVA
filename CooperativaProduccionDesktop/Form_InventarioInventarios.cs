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
using CooperativaProduccion.Helpers.GridRecords;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Reflection;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_InventarioInventarios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_InventarioInventarios()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        #region Method Code

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar(); 
        }

        private void cbTabaco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkTabaco.Checked)
            {
                Guid Tabaco = Guid.Parse(cbProducto.SelectedValue.ToString());

                var clase = Context.Vw_Clase
                .Where(x => x.ID_PRODUCTO == Tabaco
                    && x.Vigente == true)
                .ToList();

                cbClase.DataSource = clase;
                cbClase.DisplayMember = "Nombre";
                cbClase.ValueMember = "Id";
            }
        }

        private void checkTabaco_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTabaco.Checked)
            {
                Guid Tabaco = Guid.Parse(cbProducto.SelectedValue.ToString());

                var clase = Context.Vw_Clase
                .Where(x => x.ID_PRODUCTO == Tabaco
                    && x.Vigente == true)
                .ToList();

                cbClase.DataSource = clase;
                cbClase.DisplayMember = "Nombre";
                cbClase.ValueMember = "Id";
            }
        }

        private void gridViewInventarioDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.LightSkyBlue;
            }
        }

        private void gridViewInventario_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView master = sender as GridView;
            GridView detail = master.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detail.RowStyle += gridViewInventarioDetalle_RowStyle;
            detail.DoubleClick += gridViewInventarioDetalle_DoubleClick;
        }

        private void gridViewInventarioDetalle_DoubleClick(object sender, EventArgs e)
        {
            GridView detailView = sender as GridView;
            int i = detailView.SourceRowHandle;
           
            string deposito = gridViewInventario.GetRowCellValue(i, "Deposito")
                .ToString();

            string producto = gridViewInventario.GetRowCellValue(i, "TipoTabaco")
                .ToString();

            GridView parcial = sender as GridView;
            string item = parcial
                   .GetRowCellValue(parcial.FocusedRowHandle, "Clase")
                   .ToString();

            var kardex = new Form_InventarioKardex(deposito, producto, item);
            kardex.Show();
        }

        private void gridViewInventario_DoubleClick(object sender, EventArgs e)
        {
            string deposito = gridViewInventario.GetRowCellValue(gridViewInventario.FocusedRowHandle, "Deposito")
                     .ToString();

            string producto = gridViewInventario.GetRowCellValue(gridViewInventario.FocusedRowHandle, "TipoTabaco")
                .ToString();

            string item = string.Empty;

            var kardex = new Form_InventarioKardex(deposito, producto, item);
            kardex.Show();
        }

        #endregion

        #region Method Dev

        private void CargarCombo()
        {
            var deposito = Context.Vw_Deposito
                .OrderBy(x => x.nombre)
                .ToList();

            cbDeposito.DataSource = deposito;
            cbDeposito.DisplayMember = "Nombre";
            cbDeposito.ValueMember = "Id";

            var producto = (from c in Context.Vw_TipoTabaco
                            select new
                            {
                                Id = c.id,
                                Descripcion = c.DESCRIPCION
                            })
                            .Union(from p in Context.Vw_Producto
                                   select new
                                   {
                                       Id = p.ID,
                                       Descripcion = p.DESCRIPCION
                                   })
                                   .OrderBy(x => x.Descripcion)
                                   .ToList();

            cbProducto.DataSource = producto;
            cbProducto.DisplayMember = "Descripcion";
            cbProducto.ValueMember = "Id";
        }

        private void Buscar()
        {
            List<GridInventario> lista = new List<GridInventario>();

            Expression<Func<Movimiento, bool>> pred = x => true;

            pred = pred.And(x => DbFunctions.TruncateTime(x.Fecha) <= dpHasta.Value.Date);

            Expression<Func<Vw_Pesada, bool>> pred2 = x => true;

            pred2 = checkTabaco.Checked ? pred2.And(x => x.DESCRIPCION == cbProducto.Text) : pred2;

            pred2 = checkClase.Checked ? pred2.And(x => x.Clase == cbClase.Text) : pred2;

            Expression<Func<Vw_Deposito, bool>> pred3 = x => true;

            pred3 = checkDeposito.Checked ? pred3.And(x => x.nombre == cbDeposito.Text) : pred3;
            
            Expression<Func<Vw_Producto, bool>> pred4 = x => true;

            pred4 = checkTabaco.Checked ? pred4.And(x => x.DESCRIPCION == cbProducto.Text) : pred4;

            var movimientos =
                (from m in Context.Movimiento.Where(pred)
                 join p in Context.Vw_Pesada.Where(pred2)
                     on m.TransaccionId equals p.PesadaDetalleId
                 join d in Context.Vw_Deposito.Where(pred3)
                     on m.DepositoId equals d.id
                 group new { m, p, d } by new
                 {
                     Deposito = d.nombre,
                     TipoTabaco = p.DESCRIPCION,
                     m.Unidad
                 } into g
                 select new
                 {
                     g.Key.Deposito,
                     g.Key.TipoTabaco,
                     g.Key.Unidad,
                     Ingreso = g.Sum(c => c.m.Ingreso),
                     Egreso = g.Sum(c => c.m.Egreso),
                     Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                 })
                 .Union(from m2 in Context.Movimiento.Where(pred)
                        join c in Context.Caja
                            on m2.TransaccionId equals c.Id
                        join pr in Context.Vw_Producto.Where(pred4)
                            on c.ProductoId equals pr.ID
                        join d2 in Context.Vw_Deposito.Where(pred3)
                            on m2.DepositoId equals d2.id
                        group new { m2, c, pr, d2 } by new
                        {
                            Deposito = d2.nombre,
                            TipoTabaco = pr.DESCRIPCION,
                            m2.Unidad
                        } into g
                        select new
                        {
                            g.Key.Deposito,
                            g.Key.TipoTabaco,
                            g.Key.Unidad,
                            Ingreso = g.Sum(c => c.m2.Ingreso),
                            Egreso = g.Sum(c => c.m2.Egreso),
                            Saldo = g.Sum(c => c.m2.Ingreso) - g.Sum(c => c.m2.Egreso)
                        })
                        .ToList();

            foreach (var movimiento in movimientos)
            {
                var movimientoDetalle =
                    (from m in Context.Movimiento.Where(pred)
                     join p in Context.Vw_Pesada.Where(x=> x.DESCRIPCION == movimiento.TipoTabaco)
                         on m.TransaccionId equals p.PesadaDetalleId
                     join d in Context.Vw_Deposito.Where(pred3)
                         on m.DepositoId equals d.id
                     group new { m, p, d } by new
                     {
                         Clase = p.Clase
                     } into g
                     select new
                     {
                         g.Key.Clase,
                         Ingreso = g.Sum(c => c.m.Ingreso),
                         Egreso = g.Sum(c => c.m.Egreso),
                         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                     })
                     .ToList();

                var rowsDetalle = movimientoDetalle.Select(x =>
                   new GridInventarioDetalle()
                   {
                       Clase = x.Clase,
                       Ingreso = x.Ingreso,
                       Egreso = x.Egreso,
                       Saldo = x.Saldo
                   })
                   .OrderBy(x => x.Clase)
                   .ToList();

                var rowInventario = new GridInventario();
                rowInventario.Deposito = movimiento.Deposito;
                rowInventario.TipoTabaco = movimiento.TipoTabaco;
                rowInventario.Unidad = movimiento.Unidad;
                rowInventario.Ingreso = movimiento.Ingreso;
                rowInventario.Egreso = movimiento.Egreso;
                rowInventario.Saldo = movimiento.Saldo;
                rowInventario.Detalle = rowsDetalle;
                lista.Add(rowInventario);
            }

            gridControlInventario.DataSource = new BindingList<GridInventario>(lista);
            gridViewInventario.Columns[0].Width = 120;
            gridViewInventario.Columns[1].Width = 150;
            gridViewInventario.Columns[2].Width = 100;
            gridViewInventario.Columns[3].Width = 120;
            gridViewInventario.Columns[4].Width = 120;
            gridViewInventario.Columns[5].Width = 120;

        }

        #endregion

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelInventario";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelInventario\" + Hora + " - ExcelInventario.xls";

            gridControlInventario.ExportToXls(fileName);
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