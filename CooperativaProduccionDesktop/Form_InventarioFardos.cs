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
    public partial class Form_InventarioFardos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;

        public Form_InventarioFardos()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
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

            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var count = result
                    .Where(r => r.FET.Contains(txtFet.Text))
                    .Count();
                if (count > 1)
                {
                    var empleado = Context.Productor
                        .Where(x => x.Fet.Contains(txtFet.Text));
                    if (empleado.Count() > 1)
                    {
                        _formBuscarProductor = new Form_AdministracionBuscarProductor();
                        _formBuscarProductor.fet = txtFet.Text;
                        _formBuscarProductor.target = DevConstantes.Preingreso;
                        _formBuscarProductor.BuscarFet();
                        _formBuscarProductor.ShowDialog(this);
                    }
                }
                else if (count == 1)
                {
                    var empleado = Context.Productor
                        .Where(x => x.Fet.Contains(txtFet.Text));
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
                        MessageBox.Show("Productor no válido.",
                                              "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                var count = result
                    .Where(r => r.PRODUCTOR.Contains(txtNombre.Text))
                    .Count();
                if (count > 1)
                {
                    var empleado = Context.Productor
                        .Where(x => x.Nombre.Contains(txtNombre.Text));
                    if (empleado.Count() > 1)
                    {
                        _formBuscarProductor = new Form_AdministracionBuscarProductor();
                        _formBuscarProductor.nombre = txtNombre.Text;
                        _formBuscarProductor.target = DevConstantes.Preingreso;
                        _formBuscarProductor.BuscarNombre();
                        _formBuscarProductor.ShowDialog(this);
                    }
                }
                else if (count == 1)
                {
                    var empleado = Context.Productor
                        .Where(x => x.Nombre.Contains(txtNombre.Text));
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
                        MessageBox.Show("Productor no válido.",
                                              "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFet.Text))
                {
                    Buscar();
                }
                else
                {
                    txtNombre.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtCuit.Text = string.Empty;
            }
        }

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtNombre.Text))
                {
                    Buscar();
                }
                else
                {
                    txtFet.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtCuit.Text = string.Empty;
            }
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            Buscar();
        }

   }
}