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
using System.Globalization;

namespace CooperativaProduccion
{
    public partial class Form_InventarioIngresoCaja : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ProductoId;
        private long LoteCaja;

        public Form_InventarioIngresoCaja()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Habilitar();
        }

        private void CargarCombo()
        {
            var producto = Context.Vw_Producto.ToList();

            cbProductoIngreso.DataSource = producto;
            cbProductoIngreso.DisplayMember = "DESCRIPCION";
            cbProductoIngreso.ValueMember = "ID";

            cbProductoConsulta.DataSource = producto;
            cbProductoConsulta.DisplayMember = "DESCRIPCION";
            cbProductoConsulta.ValueMember = "ID";

        }

        private void dpIngresoCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbProductoIngreso.Focus();
            }
        }

        private void cbProductoIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBruto.Focus();
            }
        }

        private void Habilitar()
        {
            txtBruto.Enabled = true;
            txtBruto.Text = "0.00";
            txtNeto.Enabled = true;
            txtNeto.Text = "0.00";
            txtTara.Enabled = true;
            txtTara.Text = "0.00";
            txtCantidadCajaIngreso.Enabled = true;
            txtCantidadCajaIngreso.Text = "0";
        }

        private void txtBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtBruto.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtTara.Focus();
            }
        }

        private void txtTara_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtTara.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtNeto.Focus();
            }
        }

        private void txtNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtNeto.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtCantidadCajaIngreso.Focus();
            }
        }

        private void txtCantidadCajaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtCantidadCajaIngreso.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnGenerarLote.Focus();
            }
        }

        private void btnGenerarLote_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea generar el ingreso de cajas?",
                 "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            GenerarLoteCajas();
            BuscarCaja(LoteCaja);
        }

        private void GenerarLoteCajas()
        {
            if (txtBruto.Text != string.Empty && txtTara.Text != string.Empty
                && txtNeto.Text != string.Empty)
            {
                int cantidad = 0;
                bool cant = int.TryParse(txtCantidadCajaIngreso.Text, out cantidad);
                if (cant && cantidad > 0)
                {
                    var producto = Context.Vw_Producto
                                .Where(x => x.DESCRIPCION == cbProductoIngreso.Text)
                                .FirstOrDefault();
                    ProductoId = producto.ID;
                    try
                    {
                        LoteCaja = ContadorNumeroLote(dpIngresoCaja.Value.Year, ProductoId);
                        for (int i = 0; i < cantidad; i++)
                        {
                            Caja caja;
                            caja = new Caja();
                            caja.Id = Guid.NewGuid();
                            caja.NumeroCaja = ContadorNumeroCaja(dpIngresoCaja.Value.Year, ProductoId);
                            caja.LoteCaja = LoteCaja;
                            caja.Campaña = dpIngresoCaja.Value.Year;
                            caja.Fecha = dpIngresoCaja.Value.Date;
                            caja.Hora = DateTime.Now.TimeOfDay;
                            caja.ProductoId = producto.ID;
                            caja.Bruto = decimal.Parse(txtBruto.Text, CultureInfo.InvariantCulture);
                            caja.Tara = decimal.Parse(txtTara.Text, CultureInfo.InvariantCulture);
                            caja.Neto = decimal.Parse(txtNeto.Text, CultureInfo.InvariantCulture);
                            Context.Caja.Add(caja);
                            Context.SaveChanges();
                            RegistrarMovimiento(caja.Id, 1, caja.Fecha);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
        
        private void BuscarCaja(long Lote)
        {
            var result =
                (from c in Context.Caja
                     .Where(x => x.LoteCaja == Lote)
                 join p in Context.Vw_Producto
                 on c.ProductoId equals p.ID into pr
                 from cp in pr.DefaultIfEmpty()
                 join ca in Context.Cata 
                 on c.CataId equals ca.Id into cat
                 from joined in cat.DefaultIfEmpty()
                 select new
                 {
                     Id = c.Id,
                     NumLote = c.LoteCaja,
                     NumCaja = c.NumeroCaja,
                     Producto = cp.DESCRIPCION,
                     Tara = c.Tara,
                     Neto = c.Neto,
                     Bruto = c.Bruto,
                     Cata = joined.NumCata,
                     Fecha = c.Fecha
                 })
                .OrderBy(x => x.NumCaja)
                .ToList();

            gridControlCaja.DataSource = result;
            gridViewCaja.Columns[0].Visible = false;
            gridViewCaja.Columns[1].Caption = "N° Lote";
            gridViewCaja.Columns[1].Width = 110;
            gridViewCaja.Columns[2].Caption = "N° Caja";
            gridViewCaja.Columns[2].Width = 110;
            gridViewCaja.Columns[3].Caption = "Producto";
            gridViewCaja.Columns[3].Width = 100;
            gridViewCaja.Columns[4].Caption = "Tara";
            gridViewCaja.Columns[4].Width = 100;
            gridViewCaja.Columns[5].Caption = "Neto";
            gridViewCaja.Columns[5].Width = 100;
            gridViewCaja.Columns[6].Caption = "Bruto";
            gridViewCaja.Columns[6].Width = 100;
            gridViewCaja.Columns[7].Caption = "N° Cata";
            gridViewCaja.Columns[7].Width = 200;
            gridViewCaja.Columns[8].Visible = false;
        }

        private long ContadorNumeroCaja(int campaña, Guid ProductoId)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            long numCaja = 0;
            var caja = Context.Caja
                .Where(x => x.Campaña == campaña
                    && x.ProductoId == ProductoId)
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            if (caja != null)
            {
                numCaja = caja.NumeroCaja + 1;
            }
            else
            {
                numCaja = 1;
            }
            return numCaja;
        }

        private long ContadorNumeroLote(int campaña, Guid ProductoId)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            
            long numLote = 0;
            var caja = Context.Caja
                .Where(x => x.Campaña == campaña
                    && x.ProductoId == ProductoId)
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            if (caja != null)
            {
                numLote = caja.LoteCaja + 1;
            }
            else
            {
                numLote = 1;
            }
            return numLote;
        }

        private Guid RegistrarMovimiento(Guid Id, double kilos, DateTime fecha)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = fecha;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Caja;
            movimiento.Ingreso = kilos;
            movimiento.Egreso = 0;
            movimiento.Actual = true;
            movimiento.Anulado = false;

            var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.Deposito)
                .FirstOrDefault();

            if (deposito != null)
            {
                movimiento.DepositoId = deposito.id;
            }

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private void cbProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidadCajaConsulta.Focus();
        }

        private void txtCantidadCajaConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtCantidadCajaConsulta.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnBuscarCaja.Focus();
            }
        }

        private void btnBuscarCaja_Click(object sender, EventArgs e)
        {
            if (ValidarConsulta())
            {
                BuscarCajaConsulta(txtCantidadCajaConsulta.Text);
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
            return true;
        }

        private void BuscarCajaConsulta(string cajas)
        {
            var ProductoId = Guid.Parse(cbProductoConsulta.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(cajas))
            {
                var cantidad = int.Parse(txtCantidadCajaConsulta.Text);

                var result =
                    (from c in Context.Caja
                         .Where(x => x.ProductoId == ProductoId
                             && x.CataId == null)
                     join p in Context.Vw_Producto
                     on c.ProductoId equals p.ID
                     join ca in Context.Cata
                     on c.CataId equals ca.Id into cat
                     from joined in cat.DefaultIfEmpty()
                     select new
                     {
                         Id = c.Id,
                         NumLote = c.LoteCaja,
                         NumCaja = c.NumeroCaja,
                         Producto = p.DESCRIPCION,
                         Tara = c.Tara,
                         Neto = c.Neto,
                         Bruto = c.Bruto,
                         Cata = joined.NumCata,
                         Fecha = c.Fecha
                     })
                     .Take(cantidad)
                     .OrderBy(x => x.NumCaja)
                     .ToList();

                gridControlCajaConsulta.DataSource = result;
            }
            else
            {
                var result =
                    (from c in Context.Caja
                         .Where(x => x.ProductoId == ProductoId
                             && x.CataId == null)
                     join p in Context.Vw_Producto
                     on c.ProductoId equals p.ID
                     join ca in Context.Cata
                     on c.CataId equals ca.Id into cat
                     from joined in cat.DefaultIfEmpty()
                     select new
                     {
                         Id = c.Id,
                         NumLote = c.LoteCaja,
                         NumCaja = c.NumeroCaja,
                         Producto = p.DESCRIPCION,
                         Tara = c.Tara,
                         Neto = c.Neto,
                         Bruto = c.Bruto,
                         Cata = joined.NumCata,
                         Fecha = c.Fecha
                     })
                     .OrderBy(x => x.NumCaja)
                     .ToList();

                gridControlCajaConsulta.DataSource = result;
            }
            gridViewCajaConsulta.Columns[0].Visible = false;
            gridViewCajaConsulta.Columns[1].Caption = "N° Lote";
            gridViewCajaConsulta.Columns[1].Width = 110;
            gridViewCajaConsulta.Columns[2].Caption = "N° Caja";
            gridViewCajaConsulta.Columns[2].Width = 110;
            gridViewCajaConsulta.Columns[3].Caption = "Producto";
            gridViewCajaConsulta.Columns[3].Width = 100;
            gridViewCajaConsulta.Columns[4].Caption = "Tara";
            gridViewCajaConsulta.Columns[4].Width = 100;
            gridViewCajaConsulta.Columns[5].Caption = "Neto";
            gridViewCajaConsulta.Columns[5].Width = 100;
            gridViewCajaConsulta.Columns[6].Caption = "Bruto";
            gridViewCajaConsulta.Columns[6].Width = 100;
            gridViewCajaConsulta.Columns[7].Caption = "N° Cata";
            gridViewCajaConsulta.Columns[7].Width = 200;
            gridViewCajaConsulta.Columns[8].Visible = false;
        }
    }
}