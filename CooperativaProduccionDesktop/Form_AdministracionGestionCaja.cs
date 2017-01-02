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
    public partial class Form_AdministracionGestionCaja : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private long LoteCaja;
        private Guid OrdenVentaId;
        private long NumOrden;
        private Guid ProductoId;

        public Form_AdministracionGestionCaja()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar();
        }

        #region Ingresar Caja

        #region Method Code

        private void btnGenerarLote_Click(object sender, EventArgs e)
        {
            if (Validar(false))
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
        }

        private void dpIngresoCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbProductoIngreso.Focus();
            }
        }

        private void cbProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBruto.Focus();
                var ordenVenta = cbProductoIngreso.SelectedItem as dynamic;
                OrdenVentaId = Guid.Parse(ordenVenta.OrdenVentaId.ToString());
                NumOrden = long.Parse(ordenVenta.NumOrden.ToString());
                Guid cbProducto = Guid.Parse(ordenVenta.ProductoId.ToString());
                var producto = Context.Vw_Producto
                    .Where(x => x.ID == cbProducto)
                    .FirstOrDefault();
                if (producto != null)
                {
                    lblProducto.Text = producto.DESCRIPCION;
                    ProductoId = producto.ID;
                    Habilitar();
                }
            }
        }
        
        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBruto.Focus();
            var ordenVenta = cbProductoIngreso.SelectedItem as dynamic;
            OrdenVentaId = Guid.Parse(ordenVenta.OrdenVentaId.ToString());
            NumOrden = long.Parse(ordenVenta.NumOrden.ToString());
            Guid cbProducto = Guid.Parse(ordenVenta.ProductoId.ToString());
            var producto = Context.Vw_Producto
                .Where(x => x.ID == cbProducto)
                .FirstOrDefault();
            if (producto != null)
            {
                lblProducto.Text = producto.DESCRIPCION;
                ProductoId = producto.ID;
                Habilitar();
            }
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

        private void txtCantidadCaja_KeyPress(object sender, KeyPressEventArgs e)
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
    
        #endregion

        #region Method Dev
       
        private void GenerarLoteCajas()
        {
            if (lblProducto.Text != string.Empty && txtBruto.Text != string.Empty
                && txtTara.Text != string.Empty && txtNeto.Text != string.Empty)
            {
                LoteCaja = ContadorNumeroLote();
                for (int i = 0; i < int.Parse(txtCantidadCajaIngreso.Text); i++)
                {
                    try
                    {
                        Caja caja;
                        caja = new Caja();
                        caja.Id = Guid.NewGuid();
                        caja.LoteCaja = LoteCaja;
                        caja.NumeroCaja = ContadorNumeroCaja();
                        caja.Fecha = dpIngresoCaja.Value.Date;
                        caja.Hora = DateTime.Now.TimeOfDay;
                        caja.ProductoId = ProductoId;
                        caja.Bruto = decimal.Parse(txtBruto.Text);
                        caja.Tara = decimal.Parse(txtTara.Text);
                        caja.Neto = decimal.Parse(txtNeto.Text);
                        caja.OrdenVentaId = OrdenVentaId;
                        if (checkCata.Checked)
                        {
                            var cata = Context.Cata
                                .Where(x => x.NumCaja == null);
                            if (cata != null)
                            {
                                caja.CataId = cata.FirstOrDefault().Id;
                                var NumCata = Context.Cata.Find(caja.CataId);
                                NumCata.NumCaja = caja.NumeroCaja;
                                NumCata.CajaId = caja.Id;
                                NumCata.OrdenVentaId = caja.OrdenVentaId;
                                var ordenVenta = Context.OrdenVenta
                                    .Where(x => x.Id == caja.OrdenVentaId)
                                    .FirstOrDefault();
                                if (ordenVenta != null)
                                {
                                    NumCata.NumOrden = ordenVenta.NumOrden;
                                }
                                Context.Entry(NumCata).State = EntityState.Modified;
                                Context.SaveChanges();
                            }
                        }
                      
                        Context.Caja.Add(caja);
                        Context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        private bool Validar(bool Consulta)
        {
            if (Consulta.Equals(false))
            {
                if (checkCata.Checked)
                {
                    var cata = Context.Cata
                        .Where(x => x.NumCaja == null)
                        .Count();
                    if (cata < int.Parse(txtCantidadCajaIngreso.Text))
                    {
                        MessageBox.Show("La cantidad de Nro. de CATA disponible es " +
                            "insuficiente para la cantidad de cajas ingresadas",
                            "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else
            {
                var cata = Context.Cata
                    .Where(x => x.NumCaja == null)
                    .Count();
                if (cata < int.Parse(txtCantidadCajaConsulta.Text))
                {
                    MessageBox.Show("La cantidad de Nro. de CATA disponible es " +
                        "insuficiente para la cantidad de cajas ingresadas",
                        "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void CargarCombo()
        {
            var ordenVenta = (
                from o in Context.OrdenVenta
                join p in Context.Vw_Producto
                on o.ProductoId equals p.ID
                join c in Context.Vw_Cliente
                on o.ClienteId equals c.ID
                select new
                {
                    OrdenVentaId = o.Id,
                    NumOperacion = o.NumOperacion,
                    NumOrden = o.NumOrden,
                    OperacionCliente = o.NumOperacion + " - " + c.RAZONSOCIAL,
                    Cliente = c.RAZONSOCIAL,
                    ProductoId = p.ID,
                    Producto = p.DESCRIPCION,
                    OrdenProducto = o.NumOrden + " - " + p.DESCRIPCION,
                    Fecha = o.Fecha
                })
                .OrderBy(x => x.NumOrden)
                .ToList();

            cbProductoIngreso.DataSource = ordenVenta;
            cbProductoIngreso.DisplayMember = "OrdenProducto";
            cbProductoIngreso.ValueMember = "OrdenVentaId";

            var producto = Context.Vw_Producto.ToList();
            cbProductoConsulta.DataSource = producto;
            cbProductoConsulta.DisplayMember = "DESCRIPCION";
            cbProductoConsulta.ValueMember = "ID";

        }

        private void Iniciar()
        {
            dpIngresoCaja.Focus();
            Deshabilitar();
            lblProducto.Text = string.Empty;
            txtCantidadCajaConsulta.Text = "0";
        }

        private void Deshabilitar()
        {
            txtBruto.Enabled = false;
            txtNeto.Enabled = false;
            txtTara.Enabled = false;
            txtCantidadCajaIngreso.Enabled = false;
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

        private void BuscarCaja(long Lote)
        {
            var result = 
                (from c in Context.Caja
                     .Where(x => x.LoteCaja == Lote)
                join p in Context.Vw_Producto
                on c.ProductoId equals p.ID into pr
                from cp in pr.DefaultIfEmpty()
                select new
                {
                    Id = c.Id,
                    NumCaja = c.NumeroCaja,
                    Producto = cp.DESCRIPCION,
                    Bruto = c.Bruto,
                    Tara = c.Tara,
                    Neto = c.Neto,
                    Cata = c.Cata.Cata1,
                    Fecha = c.Fecha
                })
                .OrderBy(x => x.NumCaja)
                .ToList();

            gridControlCaja.DataSource = result;
            gridViewCaja.Columns[0].Visible = false;
            gridViewCaja.Columns[1].Caption = "N° Caja";
            gridViewCaja.Columns[1].Width = 110;
            gridViewCaja.Columns[2].Caption = "Producto";
            gridViewCaja.Columns[2].Width = 100;
            gridViewCaja.Columns[3].Caption = "Bruto";
            gridViewCaja.Columns[3].Width = 100;
            gridViewCaja.Columns[4].Caption = "Tara";
            gridViewCaja.Columns[4].Width = 100;
            gridViewCaja.Columns[5].Caption = "Neto";
            gridViewCaja.Columns[5].Width = 100;
            gridViewCaja.Columns[6].Caption = "N° Cata";
            gridViewCaja.Columns[6].Width = 200;

        }

        private long ContadorNumeroCaja()
        {
            long numCaja = 0;
            var caja = Context.Caja
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

        private long ContadorNumeroLote()
        {
            long numLote = 0;
            var caja = Context.Caja
                .OrderByDescending(x => x.LoteCaja)
                .FirstOrDefault();
            if (caja != null)
            {
                numLote = caja.NumeroCaja + 1;
            }
            else
            {
                numLote = 1;
            }
            return numLote;
        }
    
        #endregion
        
        #endregion  

        #region Consultar Caja

        #region Method Code
        
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
                BuscarCajaConsulta();
            }
        }

        private void cbProductoConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCantidadCajaConsulta.Focus();
            }
        }

        private void cbProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidadCajaConsulta.Focus();
        }
        
        private void btnAsignarCata_Click(object sender, EventArgs e)
        {
            if (Validar(true))
            {
                var resultado = MessageBox.Show("¿Desea asignar número de cata a las cajas?",
                     "Atención", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                AsignarCata();
                BuscarCajaConsulta();
            }
        }

        #endregion

        #region Method Dev

        private void BuscarCajaConsulta()
        {
            var ProductoId = Guid.Parse(cbProductoConsulta.SelectedValue.ToString());
            var cantidad = int.Parse(txtCantidadCajaConsulta.Text);

            var result = 
                (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId 
                         && x.CataId == null) 
                 join ov in Context.OrdenVenta 
                 on c.OrdenVentaId equals ov.Id
                 join p in Context.Vw_Producto
                 on c.ProductoId equals p.ID into pr
                 from cp in pr.DefaultIfEmpty()
                 select new
                 {
                     Id = c.Id,
                     NumOrden = ov.NumOrden,
                     NumCaja = c.NumeroCaja,
                     Producto = cp.DESCRIPCION,
                     Bruto = c.Bruto,
                     Tara = c.Tara,
                     Neto = c.Neto,
                     Cata = c.Cata.Cata1,
                     Fecha = c.Fecha
                 })
                 .Take(cantidad)
                 .OrderBy(x => x.NumCaja)
                 .ToList();
            gridControlCajaConsulta.DataSource = result;
            gridViewCajaConsulta.Columns[0].Visible = false;
        }

        private void AsignarCata()
        {
            for (int i = 0; i < gridViewCajaConsulta.DataRowCount; i++)
            {
                Guid CajaId = new Guid(gridViewCajaConsulta
                    .GetRowCellValue(i, "Id")
                    .ToString());

                var Caja = Context.Caja.Find(CajaId);
                if (Caja != null)
                {
                    var Cata = Context.Cata
                        .Where(x => x.NumCaja == null);
                    if (Cata != null)
                    {
                        Caja.CataId = Cata.FirstOrDefault().Id;
                        Context.Entry(Caja).State = EntityState.Modified;
                        Context.SaveChanges();
                        var ActualizarCata = Context.Cata.Find(Caja.CataId);
                        ActualizarCata.NumCaja = Caja.NumeroCaja;
                        ActualizarCata.CajaId = Caja.Id;
                        var ordenVenta = Context.OrdenVenta
                            .Where(x => x.Id == Caja.OrdenVentaId)
                            .FirstOrDefault();
                        if (ordenVenta != null)
                        {
                            ActualizarCata.OrdenVentaId = Caja.OrdenVentaId;
                            ActualizarCata.NumOrden = ordenVenta.NumOrden;
                        }
                        Context.Entry(ActualizarCata).State = EntityState.Modified;
                        Context.SaveChanges();
                    }
                }
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

        #endregion
        
        #endregion
        
    }
}