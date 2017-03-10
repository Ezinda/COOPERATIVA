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

namespace CooperativaProduccion
{
    public partial class Form_InventarioFardos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private const int GridMinWidth = 200;
        private const int GridMinHeight = 400;

        public Form_InventarioFardos()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        #region Method Code

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<GridInventario> lista = new List<GridInventario>();

            Expression<Func<Movimiento, bool>> pred = x => true;

            pred = pred.And(x => x.Fecha <= dpHasta.Value.Date);

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
                .ToList();

            foreach (var movimiento in movimientos)
            {
                var movimientoDetalle =
                    (from m in Context.Movimiento.Where(pred)
                     join p in Context.Vw_Pesada.Where(pred2)
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

        #endregion

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

        private void gridViewInventarioDetalle_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {

        }

        private void gridViewInventarioDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                    e.Appearance.BackColor = Color.Salmon;
             
            }

            //GridView parentView = (sender as GridView).ParentView as GridView;
            //GridViewInfo vi = parentView.GetViewInfo() as GridViewInfo;
            //GridRowInfo ri = vi.RowsInfo.FindRow((sender as GridView).SourceRowHandle);
            //if (ri != null)
            //{
            //    if (ri.RowHandle >= 0)
            //    {
            //        if (ri.RowHandle % 2 == 0)
            //            e.Appearance.Assign(parentView.Appearance.GetAppearance("OddRow"));
            //        else
            //            e.Appearance.Assign(parentView.Appearance.GetAppearance("EvenRow"));
            //    }
            //}
        }

        private void gridViewInventario_RowStyle(object sender, RowStyleEventArgs e)
        {
           
        } 
    }
}