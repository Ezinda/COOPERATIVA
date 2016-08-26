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
    }
}