﻿using System;
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

        private void cbProductoIngreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBruto.Focus();

            var producto = Context.Vw_Producto
                .Where(x => x.DESCRIPCION == cbProductoIngreso.Text)
                .FirstOrDefault();

            if (producto != null)
            {
                lblProducto.Text = producto.DESCRIPCION;
                ProductoId = producto.ID;
                Habilitar();
            }
        }

        private void cbProductoIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBruto.Focus();

                var producto = Context.Vw_Producto
                    .Where(x => x.DESCRIPCION == cbProductoIngreso.Text)
                    .FirstOrDefault();

                if (producto != null)
                {
                    lblProducto.Text = producto.DESCRIPCION;
                    ProductoId = producto.ID;
                    Habilitar();
                }
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
                        caja.Bruto = decimal.Parse(txtBruto.Text, CultureInfo.InvariantCulture);
                        caja.Tara = decimal.Parse(txtTara.Text, CultureInfo.InvariantCulture);
                        caja.Neto = decimal.Parse(txtNeto.Text, CultureInfo.InvariantCulture);

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
                                
                                Context.Entry(NumCata).State = EntityState.Modified;
                                Context.SaveChanges();
                            }
                        }

                        Context.Caja.Add(caja);
                        Context.SaveChanges();
                        RegistrarMovimiento(caja.Id, 1, caja.Fecha);
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
                 select new
                 {
                     Id = c.Id,
                     NumLote = c.LoteCaja,
                     NumCaja = c.NumeroCaja,
                     Producto = cp.DESCRIPCION,
                     Bruto = c.Bruto,
                     Tara = c.Tara,
                     Neto = c.Neto,
                     Cata = c.Cata.NumCata,
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
            gridViewCaja.Columns[4].Caption = "Bruto";
            gridViewCaja.Columns[4].Width = 100;
            gridViewCaja.Columns[5].Caption = "Tara";
            gridViewCaja.Columns[5].Width = 100;
            gridViewCaja.Columns[6].Caption = "Neto";
            gridViewCaja.Columns[6].Width = 100;
            gridViewCaja.Columns[7].Caption = "N° Cata";
            gridViewCaja.Columns[7].Width = 200;
            gridViewCaja.Columns[8].Visible = false;
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
                BuscarCajaConsulta();
            }
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
    }
}