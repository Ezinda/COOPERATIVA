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

namespace CooperativaProduccion
{
    public partial class Form_ProduccionPreingreso : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {   
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;
        public Form_ProduccionPreingreso()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtBuscador.Text))
                {
                    Buscar();
                    btnAgregarProductor.Focus();
                }
                else
                {
                    txtBuscador.Focus();
                }
            }
        }

        private void Buscar()
        {
            var result = (
                from a in Context.Productor
                select new
                {
                    full = a.Fet + a.Nombre + a.Cuit,
                    ID = a.Id,
                    FET = a.Fet,
                    PRODUCTOR = a.Nombre,
                    CUIT = a.Cuit
                });

            if (!string.IsNullOrEmpty(txtBuscador.Text))
            {
                var count = result
                    .Where(r => r.full.Contains(txtBuscador.Text))
                    .Count();
                if (count > 1)
                {
                    var empleado = Context.Productor
                        .Where(x => x.Fet.Contains(txtBuscador.Text));
                    if (empleado.Count() > 1)
                    {
                        _formBuscarProductor = new Form_AdministracionBuscarProductor();
                        _formBuscarProductor.fet = txtBuscador.Text;
                        _formBuscarProductor.target = DevConstantes.Preingreso;
                        _formBuscarProductor.BuscarFet();
                        _formBuscarProductor.ShowDialog(this);
                    }
                    else
                    {
                        empleado = Context.Productor
                            .Where(x => x.Nombre.Contains(txtBuscador.Text));
                        if (empleado.Count() > 1)
                        {
                            _formBuscarProductor = new Form_AdministracionBuscarProductor();
                            _formBuscarProductor.nombre = txtBuscador.Text;
                            _formBuscarProductor.target = DevConstantes.Preingreso;
                            _formBuscarProductor.BuscarNombre();
                            _formBuscarProductor.ShowDialog(this);
                        }
                        else
                        {
                            empleado = Context.Productor
                            .Where(x => x.Nombre.Contains(txtBuscador.Text));
                            if (empleado.Count() > 1)
                            {
                                _formBuscarProductor = new Form_AdministracionBuscarProductor();
                                _formBuscarProductor.nombre = txtBuscador.Text;
                                _formBuscarProductor.target = DevConstantes.Preingreso;
                                _formBuscarProductor.BuscarFet();
                                _formBuscarProductor.ShowDialog(this);
                            }
                        }
                    }
                }
                else if (count == 1)
                {
                    var empleado = Context.Productor
                        .Where(x => x.Fet.Contains(txtBuscador.Text));
                    if (empleado.Count() == 1)
                    {
                        var busqueda = empleado.FirstOrDefault();
                        if (busqueda != null)
                        {
                            ProductorId = busqueda.Id;
                            txtFet.Text = busqueda.Fet.ToString();
                            txtNombre.Text = busqueda.Nombre.ToString();
                            txtCuit.Text = busqueda.Cuit.ToString();
                        }
                        else
                        {
                            MessageBox.Show("N° FET no válido.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        empleado = Context.Productor
                            .Where(x => x.Nombre.Contains(txtBuscador.Text));
                        if (empleado.Count() == 1)
                        {
                            var busqueda = empleado.FirstOrDefault();
                            if (busqueda != null)
                            {
                                ProductorId = busqueda.Id;
                                txtFet.Text = busqueda.Fet.ToString();
                                txtNombre.Text = busqueda.Nombre.ToString();
                                txtCuit.Text = busqueda.Cuit.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Nombre no válido.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            empleado = Context.Productor
                            .Where(x => x.Cuit.Contains(txtBuscador.Text));
                            if (empleado.Count() == 1)
                            {
                                var busqueda = empleado.FirstOrDefault();
                                if (busqueda != null)
                                {
                                    ProductorId = busqueda.Id;
                                    txtFet.Text = busqueda.Fet.ToString();
                                    txtNombre.Text = busqueda.Nombre.ToString();
                                    txtCuit.Text = busqueda.Cuit.ToString();
                                }
                                else
                                {
                                    MessageBox.Show("C.U.I.T no válido.",
                                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Productor no válido.",
                                          "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            Buscar();
            txtTransporte.Focus();
        }

        private void txtProductor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            Buscar();
            txtTransporte.Focus();
        }

        private void txtTransporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtChofer.Focus();
            }
        }

        private void txtChofer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPatente.Focus();
            }
        }

        private void txtPatente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRemito.Focus();
            }
        }

        private void txtNumRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                mmObservacion.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabarPreingreso_Click(object sender, EventArgs e)
        {
            GrabarPreingreso();
        }

        private void GrabarPreingreso()
        {
            throw new NotImplementedException();
        }

        private void btnAgregarProductor_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void mmObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = Context.Productor
                .Where(x => x.Id == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = empleado.Cuit;
        }

        private void txtBuscador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtBuscador.Text))
                {
                    Buscar();
                }
                else
                {
                    txtTransporte.Focus();
                }
            }
        }
    }
}