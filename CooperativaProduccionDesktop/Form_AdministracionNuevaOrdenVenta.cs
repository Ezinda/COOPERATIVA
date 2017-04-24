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
    public partial class Form_AdministracionNuevaOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm,IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        public Form_AdministracionBuscarCliente _formBuscarCliente = null;
        public Form_AdministracionBuscarTransporte _formBuscarTransporte = null;
        private Guid OrdenVentaId;
        private Guid ClienteId;
        private Guid TransporteId;

        public Form_AdministracionNuevaOrdenVenta()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();                
        }

        #region Method Code

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            var resultado = MessageBox.Show("¿Desea eliminar esta orden de venta?",
                 "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar esta orden de venta?",
                   "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dpFechaOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCliente.Focus();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuscarCliente();
                txtCalle.Focus();
            }
        }

        private void txtCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNumero.Focus();
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPiso.Focus();
            }
        }

        private void txtPiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtDpto.Focus();
            }
        }

        private void txtDpto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRazonSocial.Focus();
            }
        }

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuscarTransporte();
                txtDominio.Focus();
            }
        }

        private void txtDominio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtDominioAcoplado.Focus();
            }
        }

        private void txtDominioAcoplado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtApellido.Focus();
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCuitChofer.Focus();
            }
        }

        private void txtCuitChofer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnGuardar.Focus();
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }

        private void btnBuscarTransporte_Click(object sender, EventArgs e)
        {
            BuscarTransporte();
        }

        #endregion

        #region Method Dev

        private void BuscarCliente()
        {
            var result =
                (from a in Context.Vw_Cliente
                 select new
                 {
                     full = a.CUIT + a.RAZONSOCIAL + a.CUITE,
                     ID = a.ID,
                     CUIT = a.CUIT.Contains(DevConstantes.XX) ? a.CUITE : a.CUIT,
                     CLIENTE = a.RAZONSOCIAL,
                     PROVINCIA = a.Provincia
                 });

            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                var count = result
                    .Where(x => x.CUIT.Contains(txtCliente.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarCliente = new Form_AdministracionBuscarCliente();
                    _formBuscarCliente.cuit = txtCliente.Text;
                    _formBuscarCliente.target = DevConstantes.OrdenVenta;
                    _formBuscarCliente.BuscarCuit();
                    _formBuscarCliente.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.CUIT.Equals(txtCliente.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ClienteId = busqueda.ID;
                        txtCliente.Text = busqueda.CLIENTE;
                    }
                    else
                    {
                        count = result
                            .Where(x => x.CLIENTE.Contains(txtCliente.Text))
                            .Count();
                        if (count > 1)
                        {
                            _formBuscarCliente = new Form_AdministracionBuscarCliente();
                            _formBuscarCliente.nombre = txtCliente.Text;
                            _formBuscarCliente.target = DevConstantes.OrdenVenta;
                            _formBuscarCliente.BuscarNombre();
                            _formBuscarCliente.ShowDialog(this);
                        }
                        else
                        {
                            var busquedaNombre = result
                                .Where(x => x.CLIENTE.Contains(txtCliente.Text))
                                .FirstOrDefault();

                            if (busquedaNombre != null)
                            {
                                ClienteId = busquedaNombre.ID;
                                txtCliente.Text = busquedaNombre.CLIENTE;
                            }
                        }
                    }
                }
            }
        }

        public void Enviar(Guid Id, string fet, string nombre)
        {
            var cliente = Context.Vw_Cliente
                .Where(x => x.ID == Id)
                .Any();

            if (cliente.Equals(true))
            {
                ClienteId = Id;
                txtCuitCliente.Text = fet;
                txtCliente.Text = nombre;
            }
            else
            {
                var transporte = Context.Vw_Transporte
                    .Where(x => x.ALIAS_0_ID == Id)
                    .Any();

                if (transporte.Equals(true))
                {
                    TransporteId = Id;
                    txtCuitTransporte.Text = fet;
                    txtRazonSocial.Text = nombre;
                }
            }
        }

        private void BuscarTransporte()
        {
            var result =
               (from a in Context.Vw_Transporte
                select new
                {
                    full = a.CUIT + a.ALIAS_1_NOMBRE,
                    ID = a.ALIAS_0_ID,
                    CUIT = a.CUIT,
                    TRANSPORTE = a.ALIAS_1_NOMBRE
                });

            if (!string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                var count = result
                    .Where(x => x.CUIT.Contains(txtRazonSocial.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarTransporte = new Form_AdministracionBuscarTransporte();
                    _formBuscarTransporte.cuit = txtRazonSocial.Text;
                    _formBuscarTransporte.target = DevConstantes.OrdenVenta;
                    _formBuscarTransporte.BuscarCuit();
                    _formBuscarTransporte.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.CUIT.Equals(txtRazonSocial.Text))
                        .FirstOrDefault();

                    if (busqueda != null)
                    {
                        TransporteId = busqueda.ID;
                        txtRazonSocial.Text = busqueda.TRANSPORTE;
                    }
                    else
                    {
                        count = result
                            .Where(x => x.TRANSPORTE.Contains(txtRazonSocial.Text))
                            .Count();

                        if (count > 1)
                        {
                            _formBuscarTransporte = new Form_AdministracionBuscarTransporte();
                            _formBuscarTransporte.nombre = txtRazonSocial.Text;
                            _formBuscarTransporte.target = DevConstantes.OrdenVenta;
                            _formBuscarTransporte.BuscarNombre();
                            _formBuscarTransporte.ShowDialog(this);
                        }
                        else
                        {
                            var busquedaNombre = result
                                .Where(x => x.TRANSPORTE.Contains(txtRazonSocial.Text))
                                .FirstOrDefault();

                            if (busquedaNombre != null)
                            {
                                TransporteId = busquedaNombre.ID;
                                txtRazonSocial.Text = busquedaNombre.TRANSPORTE;
                            }
                        }
                    }
                }
            }
        }

        #endregion

  
    }
}