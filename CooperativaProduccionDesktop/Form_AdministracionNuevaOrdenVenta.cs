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
        private Guid ClienteId;
        private Guid TransporteId;

        public Form_AdministracionNuevaOrdenVenta(Guid? Id)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar(Id);
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
                txtDomicilio.Focus();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea crear una nueva orden de venta?",
               "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }

            if (ValidarCampos())
            {
                GenerarOrdenVenta();
            }
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
                     DOMICILIO = a.DOMICILIO,
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
                        txtDomicilio.Text = busqueda.DOMICILIO;
                        txtCuitCliente.Text = busqueda.CUIT;
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
                                txtDomicilio.Text = busqueda.DOMICILIO;
                                txtCuitCliente.Text = busqueda.CUIT;
                            }
                        }
                    }
                }
            }
        }

        public void Enviar(Guid Id, string fet, string nombre)
        {
            var cliente = Context.Vw_Cliente
                .Where(x => x.ID == Id);

            if (cliente.Any().Equals(true))
            {
                ClienteId = Id;
                txtCuitCliente.Text = fet;
                txtCliente.Text = nombre;
                txtDomicilio.Text = cliente.FirstOrDefault().DOMICILIO;
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
                        txtCuitTransporte.Text = busqueda.CUIT;
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
                                txtCuitTransporte.Text = busquedaNombre.CUIT;
                            }
                        }
                    }
                }
            }
        }

        private long ContadorOrdenVenta()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.OrdenVenta))
                .FirstOrDefault();

            if (contador != null)
            {
                return (contador.Valor.Value + 1);
            }
            else
            {
                return 1;
            }       
        }

        private void ActualizarContadorOrdenVenta()
        {
            var contador = Context.Contador
               .Where(x => x.Nombre.Equals(DevConstantes.OrdenVenta))
               .FirstOrDefault();

            if (contador != null)
            {
                var count = Context.Contador.Find(contador.Id);

                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    Context.Entry(count).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
        }

        private long ContadorNumeroOperacion()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.NumeroOperacion))
                .FirstOrDefault();

            if (contador != null)
            {
                return (contador.Valor.Value + 1);
            }
            else
            {
                return 1;
            }
        }

        private void ActualizarContadorNumeroOperacion()
        {
            var contador = Context.Contador
               .Where(x => x.Nombre.Equals(DevConstantes.NumeroOperacion))
               .FirstOrDefault();

            if (contador != null)
            {
                var count = Context.Contador.Find(contador.Id);

                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    Context.Entry(count).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
        }

        private void Iniciar(Guid? Id)
        {
            if (Id == null)
            {
                txtOperacion.Text = ContadorNumeroOperacion().ToString();
                txtOrden.Text = ContadorOrdenVenta().ToString();
            }
            else
            {
                CargarDatos(Id.Value);
            }
        }

        private void GenerarOrdenVenta()
        {
            try
            {
                OrdenVenta ov;
                ov = new OrdenVenta();
                ov.Id = Guid.NewGuid();
                ov.NumOperacion = ContadorNumeroOperacion();
                ov.NumOrden = ContadorOrdenVenta();
                ov.Fecha = DateTime.Now.Date;
                ov.ClienteId = ClienteId;
                ov.Calle = txtDomicilio.Text;
                ov.Numero = txtNumero.Text;
                ov.Piso = txtPiso.Text;
                ov.Dpto = txtDpto.Text;
                ov.TransporteId = TransporteId;
                ov.Dominio = txtDominio.Text;
                ov.DominioAcoplado = txtDominioAcoplado.Text;
                ov.ApellidoChofer = txtApellido.Text;
                ov.NombreChofer = txtNombre.Text;
                ov.CuitChofer = txtCuitChofer.Text;
                ov.Pendiente = true;
                Context.OrdenVenta.Add(ov);
                Context.SaveChanges();
                ActualizarContadorOrdenVenta();
                ActualizarContadorNumeroOperacion();

                IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;

                if (mienlace != null)
                {
                    mienlace.Enviar(true);
                }

                this.Close();
            }
            catch
            {
                throw;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe ingresar un cliente.",
                               "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void CargarDatos(Guid Id)
        {
            var ov = Context.OrdenVenta
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            if (ov != null)
            {
                txtOperacion.Text = ov.NumOperacion.ToString();
                txtOrden.Text = ov.NumOrden.ToString();
                dpFechaOrden.Value = ov.Fecha;
                ClienteId = ov.ClienteId;

                var cliente = Context.Vw_Cliente
                    .Where(x => x.ID == ov.ClienteId)
                    .FirstOrDefault();

                if (cliente != null)
                {
                    txtCliente.Text = cliente.RAZONSOCIAL;
                    txtCuitCliente.Text = cliente.CUIT.Contains(DevConstantes.XX) ? 
                        cliente.CUITE : cliente.CUIT;
                    txtDomicilio.Text = cliente.DOMICILIO;
                }

                txtNumero.Text = ov.Numero;
                txtPiso.Text = ov.Piso;
                txtDpto.Text = ov.Dpto;

                var transporte = Context.Vw_Transporte
                    .Where(x => x.ALIAS_0_ID == ov.TransporteId)
                    .FirstOrDefault();
                if (transporte != null)
                {
                    txtRazonSocial.Text = transporte.ALIAS_1_NOMBRE;
                    txtCuitTransporte.Text = transporte.CUIT;
                }
                txtDominio.Text = ov.Dominio;
                txtDominioAcoplado.Text = ov.DominioAcoplado;
                txtApellido.Text = ov.ApellidoChofer;
                txtNombre.Text = ov.NombreChofer;
                txtCuitChofer.Text = ov.CuitChofer;
            }
            
        }
        #endregion

    }
}