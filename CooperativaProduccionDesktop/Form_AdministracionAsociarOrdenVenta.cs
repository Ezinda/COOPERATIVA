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
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionAsociarOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_AdministracionAsociarOrdenVenta()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        private void CargarCombo()
        {
            var producto = Context.Vw_Producto.ToList();
            cbProductoConsulta.DataSource = producto;
            cbProductoConsulta.DisplayMember = "DESCRIPCION";
            cbProductoConsulta.ValueMember = "ID";
        }

        private void btnBuscarCaja_Click(object sender, EventArgs e)
        {
            if (ValidarConsulta())
            {
                BuscarCajaConsulta();
            }
        }

        private bool ValidarConsulta()
        {
            if (cbProductoConsulta.Text == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un producto",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtCantidadCajaConsulta.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una cantidad de cajas válidas",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void BuscarCajaConsulta()
        {
            var ProductoId = Guid.Parse(cbProductoConsulta.SelectedValue.ToString());
            var cantidad = int.Parse(txtCantidadCajaConsulta.Text);

            var result =
                (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId
                         && x.CataId == null)
                 join p in Context.Vw_Producto
                 on c.ProductoId equals p.ID
                 select new
                 {
                     Id = c.Id,
                     NumLote = c.LoteCaja,
                     NumCaja = c.NumeroCaja,
                     Producto = p.DESCRIPCION,
                     Bruto = c.Bruto,
                     Tara = c.Tara,
                     Neto = c.Neto,
                     Cata = c.Cata.NumCata,
                     Fecha = c.Fecha
                 })
                 .Take(cantidad)
                 .OrderBy(x => x.NumCaja)
                 .ToList();

            gridControlCajaConsulta.DataSource = result;
            gridViewCajaConsulta.Columns[0].Visible = false;
            gridViewCajaConsulta.Columns[1].Caption = "N° Lote";
            gridViewCajaConsulta.Columns[1].Width = 110;
            gridViewCajaConsulta.Columns[2].Caption = "N° Caja";
            gridViewCajaConsulta.Columns[2].Width = 110;
            gridViewCajaConsulta.Columns[3].Caption = "Producto";
            gridViewCajaConsulta.Columns[3].Width = 100;
            gridViewCajaConsulta.Columns[4].Caption = "Bruto";
            gridViewCajaConsulta.Columns[4].Width = 100;
            gridViewCajaConsulta.Columns[5].Caption = "Tara";
            gridViewCajaConsulta.Columns[5].Width = 100;
            gridViewCajaConsulta.Columns[6].Caption = "Neto";
            gridViewCajaConsulta.Columns[6].Width = 100;
            gridViewCajaConsulta.Columns[7].Caption = "N° Cata";
            gridViewCajaConsulta.Columns[7].Width = 200;
            gridViewCajaConsulta.Columns[8].Visible = false;
        }

        private void btnAsociarOV_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea asociar una orden de venta a las cajas?",
                    "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            AsociarOV();
        }

        private void AsociarOV()
        {
            if (gridViewCajaConsulta.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewCajaConsulta.DataRowCount; i++)
                {
                    if (gridViewCajaConsulta.IsRowSelected(i))
                    {
                        Guid CajaId = new Guid(gridViewCajaConsulta
                            .GetRowCellValue(i, "Id")
                            .ToString());

                        var Caja = Context.Caja.Find(CajaId);

                        if (Caja != null)
                        {
                            Context.Entry(Caja).State = EntityState.Modified;
                            Context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}