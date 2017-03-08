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

namespace CooperativaProduccion
{
    public partial class Form_InventarioFardos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_InventarioFardos()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        #region Method Code

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Expression<Func<Movimiento, bool>> pred = x => true;

            pred = pred.And(x => x.Fecha >= dpDesde.Value.Date 
                && x.Fecha <= dpHasta.Value.Date);

            var result = (
                from m in Context.Movimiento.Where(pred)
                join p in Context.Vw_Pesada 
                    on m.TransaccionId equals p.PesadaDetalleId
                join d in Context.Vw_Deposito 
                    on m.DepositoId equals d.id
                group new { m, p, d } by new
                {
                    m.Fecha,
                    Deposito = d.nombre,
                    TipoTabaco = p.DESCRIPCION,
                    m.Unidad
                } into g
                select new
                {
                    g.Key.Fecha,
                    g.Key.Deposito,
                    g.Key.TipoTabaco,
                    g.Key.Unidad,
                    Ingreso = g.Sum(c=>c.m.Ingreso),
                    Egreso = g.Sum(c=>c.m.Egreso),
                    Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                })
                .ToList();

            if (result.Count > 0)
            {
                gridControlFardos.DataSource = result;
            }
        }

        #endregion

        #region Method Dev

        private void CargarCombo()
        {
            var deposito = Context.Vw_Deposito
                .OrderBy(x=>x.nombre)
                .ToList();

            cbDeposito.DataSource = deposito;
            cbDeposito.DisplayMember = "Nombre";
            cbDeposito.ValueMember = "Id";
            
            var producto = (from c in Context.Vw_TipoTabaco
                            select new
                            {
                                Id = c.id ,
                                Descripcion = c.DESCRIPCION
                            })
                            .Union(from p in Context.Vw_Producto
                                   select new
                                   {
                                       Id = p.ID,
                                       Descripcion = p.DESCRIPCION
                                   })
                                   .OrderBy(x=>x.Descripcion)
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
    }
}