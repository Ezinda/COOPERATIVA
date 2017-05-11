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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_InventarioKardex : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public string depositokardex;
        public string productokardex;
        public string itemkardex;

        public Form_InventarioKardex(string deposito, string producto, string item)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar(deposito, producto, item);
        }

        private void Iniciar(string deposito, string producto, string item)
        {
            depositokardex = deposito;
            productokardex = producto;
            itemkardex = item;
            cbDeposito.Text = deposito;
            checkDeposito.Checked = true;
            cbProducto.Text = producto;
            checkTabaco.Checked = true;
            cbProducto.SelectedIndexChanged += cbProducto_SelectedIndexChanged;
            cbClase.Text = item;
            checkClase.Checked = true;
            if (!string.IsNullOrEmpty(item))
            {
                BuscarClase();
            }
            else
            {
                lblClase.Visible = false;
                cbClase.Visible = false;
                checkClase.Visible = false;
                BuscarProbucto();
            }
        }

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

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
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

        private void BuscarClase()
        {
            Expression<Func<Movimiento, bool>> pred = x => true;

            pred = checkDesde.Checked ? pred.And(x => DbFunctions.TruncateTime(x.Fecha) > dpDesde.Value.Date) : pred;

            pred = pred.And(x => DbFunctions.TruncateTime(x.Fecha) <= dpHasta.Value.Date);

            Expression<Func<Vw_Pesada, bool>> pred2 = x => true;

            pred2 = checkTabaco.Checked ? pred2.And(x => x.DESCRIPCION == cbProducto.Text) : pred2;

            pred2 = checkClase.Checked ? pred2.And(x => x.Clase == cbClase.Text) : pred2;

            Expression<Func<Vw_Deposito, bool>> pred3 = x => true;

            pred3 = checkDeposito.Checked ? pred3.And(x => x.nombre == cbDeposito.Text) : pred3;

            var movimientos =
                (from m in Context.Movimiento.Where(pred)
                 join p in Context.Vw_Pesada.Where(pred2)
                     on m.TransaccionId equals p.PesadaDetalleId
                 join d in Context.Vw_Deposito.Where(pred3)
                     on m.DepositoId equals d.id
                 group new { m, p, d } by new
                 {
                     Fecha = p.FechaRomaneo,
                     Deposito = d.nombre,
                     TipoTabaco = p.DESCRIPCION,
                     TipoDocumento = m.Documento,
                     NumeroDocumento = p.NumRomaneo,
                     m.Unidad
                 } into g
                 select new
                 {
                     g.Key.Fecha,
                     g.Key.Deposito,
                     g.Key.NumeroDocumento,
                     g.Key.TipoDocumento,
                     g.Key.TipoTabaco,
                     g.Key.Unidad,
                     Ingreso = g.Sum(c => c.m.Ingreso),
                     Egreso = g.Sum(c => c.m.Egreso),
                     Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                 })
                .OrderByDescending(x => x.Fecha)
                .ThenByDescending(x => x.NumeroDocumento)
                .ToList();

            if (!checkDesde.Checked)
            {
                gridControlInventario.DataSource = movimientos;
                gridViewInventario.Columns[0].Width = 120;
                gridViewInventario.Columns[1].Width = 150;
                gridViewInventario.Columns[2].Width = 100;
                gridViewInventario.Columns[3].Width = 120;
                gridViewInventario.Columns[4].Width = 120;
                gridViewInventario.Columns[5].Width = 120;
            }
            else if (checkDesde.Checked)
            {
                List<GridKardex> lista = new List<GridKardex>();

                foreach (var movimiento in movimientos)
                {
                    var rowInventario = new GridKardex();
                    rowInventario.Fecha = movimiento.Fecha.Value.ToShortDateString();
                    rowInventario.Deposito = movimiento.Deposito;
                    rowInventario.NumeroDocumento = movimiento.NumeroDocumento.Value.ToString();
                    rowInventario.TipoDocumento = movimiento.TipoDocumento;
                    rowInventario.TipoTabaco = movimiento.TipoTabaco;
                    rowInventario.Unidad = movimiento.Unidad;
                    rowInventario.Ingreso = movimiento.Ingreso.Value.ToString();
                    rowInventario.Egreso = movimiento.Egreso.Value.ToString();
                    rowInventario.Saldo = movimiento.Saldo.Value.ToString();
                    lista.Add(rowInventario);
                }

                Expression<Func<Movimiento, bool>> pred4 = x => true;

                pred4 = pred4.And(x => x.Fecha <= dpDesde.Value.Date);
               
                var desde = dpDesde.Value.ToShortDateString();
                
                var saldos =
                    (from m in Context.Movimiento.Where(pred4)
                     join p in Context.Vw_Pesada.Where(pred2)
                     on m.TransaccionId equals p.PesadaDetalleId
                     join d in Context.Vw_Deposito.Where(pred3)
                     on m.DepositoId equals d.id
                     group new { m, p, d } by new
                     {
                         Fecha = "Saldo al día " + desde,
                         Deposito = d.nombre,
                         TipoTabaco = p.DESCRIPCION,
                         TipoDocumento = DevConstantes.Romaneo,
                         m.Unidad
                     } into g
                     select new
                     {
                         g.Key.Fecha,
                         g.Key.Deposito,
                         g.Key.TipoDocumento,
                         g.Key.TipoTabaco,
                         g.Key.Unidad,
                         Ingreso = g.Sum(c => c.m.Ingreso),
                         Egreso = g.Sum(c => c.m.Egreso),
                         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                     })
                     .ToList();

                foreach (var saldo in saldos)
                {
                    var rowInventario = new GridKardex();
                    rowInventario.Fecha = saldo.Fecha;
                    rowInventario.Deposito = saldo.Deposito;
                    rowInventario.NumeroDocumento = string.Empty;
                    rowInventario.TipoDocumento = saldo.TipoDocumento;
                    rowInventario.TipoTabaco = saldo.TipoTabaco;
                    rowInventario.Unidad = saldo.Unidad;
                    rowInventario.Ingreso = string.Empty;
                    rowInventario.Egreso = string.Empty;
                    rowInventario.Saldo = saldo.Saldo.Value.ToString();
                    lista.Add(rowInventario);
                }

                lista.Reverse();

                gridControlInventario.DataSource = new BindingList<GridKardex>(lista);
                gridViewInventario.Columns[0].Width = 200;
                gridViewInventario.Columns[8].Visible = false;
                gridViewInventario.Columns["NumeroDocumento"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
        }

        private void BuscarProbucto()
        {
            Expression<Func<Vw_Movimiento, bool>> pred = x => true;

            pred = checkDesde.Checked ? pred.And(x => x.Fecha > dpDesde.Value.Date) : pred;

            pred = pred.And(x => x.Fecha <= dpHasta.Value.Date);

            Expression<Func<Vw_Producto, bool>> pred2 = x => true;

            pred2 = checkTabaco.Checked ? pred2.And(x => x.DESCRIPCION == cbProducto.Text) : pred2;

            Expression<Func<Vw_Deposito, bool>> pred3 = x => true;

            pred3 = checkDeposito.Checked ? pred3.And(x => x.nombre == cbDeposito.Text) : pred3;

            List<GridKardex> lista1 = new List<GridKardex>();

            var movimientos =
                (from m in Context.Vw_Movimiento.Where(pred)
                 .OrderBy(x=>x.NumeroCaja)
                 join p in Context.Vw_Producto.Where(pred2)
                    on m.ProductoId equals p.ID
                 join d in Context.Vw_Deposito.Where(pred3)
                    on m.DepositoId equals d.id
                 group new { m, p, d } by new
                 {
                     Fecha = m.FechaCaja,
                     Deposito = d.nombre,
                     TipoTabaco = p.DESCRIPCION,
                     TipoDocumento = m.Documento,
                     NumeroDocumento = m.NumeroDocumento,
                     m.Unidad
                 } into g
                 select new
                 {
                     g.Key.Fecha,
                     g.Key.Deposito,
                     g.Key.TipoDocumento,
                     g.Key.NumeroDocumento,
                     g.Key.TipoTabaco,
                     g.Key.Unidad,
                     Ingreso = g.Sum(c => c.m.Ingreso),
                     Egreso = g.Sum(c => c.m.Egreso),
                     Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                 })
                .OrderByDescending(x => x.Fecha)
                .ToList();

            foreach (var item in movimientos)
            {
                var rowInventario = new GridKardex();
                rowInventario.Fecha = item.Fecha.ToShortDateString();
                rowInventario.Deposito = item.Deposito;
                rowInventario.NumeroDocumento = string.Empty;
                rowInventario.TipoDocumento = item.TipoDocumento;
                rowInventario.TipoTabaco = item.TipoTabaco;
                rowInventario.Unidad = item.Unidad;
                rowInventario.Ingreso = string.Empty;
                rowInventario.Egreso = string.Empty;
                rowInventario.Saldo = item.Saldo.Value.ToString();
                lista1.Add(rowInventario);
            }

            lista1.Reverse();

            gridControlInventario.DataSource = new BindingList<GridKardex>(lista1);
            gridViewInventario.Columns[0].Width = 200;
            gridViewInventario.Columns[8].Visible = false;
            gridViewInventario.Columns["NumeroDocumento"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            if (!checkDesde.Checked)
            {
                gridControlInventario.DataSource = movimientos;
                gridViewInventario.Columns[0].Width = 120;
                gridViewInventario.Columns[1].Width = 150;
                gridViewInventario.Columns[2].Width = 300;
                gridViewInventario.Columns[3].Width = 120;
                gridViewInventario.Columns[4].Width = 120;
                gridViewInventario.Columns[5].Width = 120;
                gridViewInventario.Columns[8].Visible = false;
            }
            else if (checkDesde.Checked)
            {
                List<GridKardex> lista = new List<GridKardex>();

                foreach (var movimiento in movimientos)
                {
                    var rowInventario = new GridKardex();
                    rowInventario.Fecha = movimiento.Fecha.ToShortDateString();
                    rowInventario.Deposito = movimiento.Deposito;
                    rowInventario.NumeroDocumento = movimiento.NumeroDocumento;
                    rowInventario.TipoDocumento = movimiento.TipoDocumento;
                    rowInventario.TipoTabaco = movimiento.TipoTabaco;
                    rowInventario.Unidad = movimiento.Unidad;
                    rowInventario.Ingreso = movimiento.Ingreso.Value.ToString();
                    rowInventario.Egreso = movimiento.Egreso.Value.ToString();
                    rowInventario.Saldo = movimiento.Saldo.Value.ToString();
                    lista.Add(rowInventario);
                }

                Expression<Func<Movimiento, bool>> pred4 = x => true;

                pred4 = pred4.And(x => x.Fecha <= dpDesde.Value.Date);

                var desde = dpDesde.Value.ToShortDateString();

                var saldos =
                    (from m in Context.Vw_Movimiento.Where(pred)
                     join p in Context.Vw_Producto.Where(pred2)
                        on m.ProductoId equals p.ID
                     join d in Context.Vw_Deposito.Where(pred3)
                        on m.DepositoId equals d.id
                     group new { m, p, d } by new
                     {
                         Fecha = "Saldo al día " + desde,
                         Deposito = d.nombre,
                         TipoTabaco = p.DESCRIPCION,
                         TipoDocumento = m.Documento,
                         m.Unidad
                     } into g
                     select new
                     {
                         g.Key.Fecha,
                         g.Key.Deposito,
                         g.Key.TipoDocumento,
                         g.Key.TipoTabaco,
                         g.Key.Unidad,
                         Ingreso = g.Sum(c => c.m.Ingreso),
                         Egreso = g.Sum(c => c.m.Egreso),
                         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                     })
                     .ToList();

                foreach (var saldo in saldos)
                {
                    var rowInventario = new GridKardex();
                    rowInventario.Fecha = saldo.Fecha;
                    rowInventario.Deposito = saldo.Deposito;
                    rowInventario.NumeroDocumento = string.Empty;
                    rowInventario.TipoDocumento = saldo.TipoDocumento;
                    rowInventario.TipoTabaco = saldo.TipoTabaco;
                    rowInventario.Unidad = saldo.Unidad;
                    rowInventario.Ingreso = string.Empty;
                    rowInventario.Egreso = string.Empty;
                    rowInventario.Saldo = saldo.Saldo.Value.ToString();
                    lista.Add(rowInventario);
                }

                lista.Reverse();

                gridControlInventario.DataSource = new BindingList<GridKardex>(lista);
                gridViewInventario.Columns[0].Width = 200;
                gridViewInventario.Columns[8].Visible = false;
                gridViewInventario.Columns["NumeroDocumento"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
        }

        private void gridViewInventario_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.LightSkyBlue;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarClase();
            gridViewInventario.CustomUnboundColumnData += new CustomColumnDataEventHandler(gridViewInventario_CustomUnboundColumnData);
        }

        private void gridViewInventario_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "Total" & e.IsGetData)
            {
                decimal total = 0m;
                for (int i = 0; i <= e.ListSourceRowIndex; i++)
                {
                    total += Convert.ToDecimal(view.GetListSourceRowCellValue(i, "Saldo"));
                }
                e.Value = total;
            }
        }

        private void Form_InventarioKardex_Load(object sender, EventArgs e)
        {
            // Create an unbound column.
            GridColumn unbColumn = gridViewInventario.Columns.AddField("Total");
            unbColumn.VisibleIndex = gridViewInventario.Columns.Count;
            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // Disable editing.
            unbColumn.OptionsColumn.AllowEdit = false;
            // Specify format settings.
            unbColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //unbColumn.DisplayFormat.FormatString = "c";
            // Customize the appearance settings.
            unbColumn.AppearanceCell.BackColor = Color.LemonChiffon;
        }
    }
}