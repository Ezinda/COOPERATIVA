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
            AddNewRow();
        }

        #region Method Code

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

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            Buscar();
            txtTransporte.Focus();
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

        private void btnAgregarProductor_Click(object sender, EventArgs e)
        {
            AgregarProductor(ProductorId, txtFet.Text, txtNombre.Text, txtCuit.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarEncabezado();
        }

        private void mmObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnGrabarPreingreso.Focus();
            }
        }

        private void txtBuscador_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtTransporte.Focus();
                }
            }
        }

        private void btnAgregarProductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AgregarProductor(ProductorId, txtFet.Text, txtNombre.Text, txtCuit.Text);
            }
        }

        private void btnEliminarProductor_Click(object sender, EventArgs e)
        {
            string valorCelda;
            foreach (DataGridViewRow item in this.dgvProductores.SelectedRows)
            {
                if (dgvProductores.Rows[item.Index].Cells[item.Index].Value == null)
                {
                    valorCelda = "";
                }
                else
                {
                    valorCelda = dgvProductores.Rows[item.Index].Cells[item.Index].Value.ToString();
                }
                if (!string.IsNullOrEmpty(valorCelda))
                {
                    dgvProductores.Rows.RemoveAt(item.Index);
                }
            }
            LimpiarEncabezado();
        }
      
        #endregion

        #region Method Dev
     
        private void AddNewRow()
        {
            DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d3 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d4 = new DataGridViewTextBoxColumn();

            //Add Header Texts to be displayed on the Columns
            d1.HeaderText = "Id";
            d2.HeaderText = "FET";
            d3.HeaderText = "PRODUCTOR";
            d4.HeaderText = "CUIT";

            d1.Visible = false;
            d2.Width = 120;
            d3.Width = 300;
            d4.Width = 120;

            //Add the Columns to the DataGridView
            dgvProductores.Columns.AddRange(d1, d2, d3, d4);
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
             
        private void GrabarPreingreso()
        {
            if (ValidarCampos())
            {
                var resultado = MessageBox.Show("¿Desea crear un nuevo preingreso?",
                    "Crear Preingreso", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GuardarDatos();
                Limpiar();
            }
        }

        private bool ValidarCampos()
        {
            if (dgvProductores.RowCount < 0)
            {
                MessageBox.Show("No se ha agregado productores al preingreso",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private int ContadorNumeroPreingreso()
        {
            var count = Context.Preingreso.Count();
            if (count != 0)
            {
                var codigo = Context.Preingreso
                    .Max(x => x.NumeroPreingreso)
                    .ToString();
                return (Int16.Parse(codigo) + 1);
            }
            else
            {
                return 1;
            }
        }

        private void GuardarDatos()
        {
            try
            {
                Preingreso preingreso;
                preingreso = new Preingreso();
                preingreso.Id = Guid.NewGuid();
                preingreso.NumeroPreingreso = ContadorNumeroPreingreso();
                preingreso.Transporte = txtTransporte.Text;
                preingreso.Chofer = txtChofer.Text;
                preingreso.Patente = txtPatente.Text;
                preingreso.NumRemito = txtRemito.Text;
                preingreso.Observaciones = mmObservacion.Text;
                
                Context.Preingreso.Add(preingreso);
                Context.SaveChanges();

                if (dgvProductores.RowCount > 0)
                {
                    for (int i = 0; i <= dgvProductores.RowCount - 1; i++)
                    {
                        if (dgvProductores.Rows[i].Cells[0].Value != null)
                        {
                            PreingresoDetalle preingresoDetalle;
                            preingresoDetalle = new PreingresoDetalle();
                            preingresoDetalle.Id = Guid.NewGuid();
                            preingresoDetalle.PreingresoId = preingreso.Id;
                            preingresoDetalle.ProductorId = new Guid(dgvProductores.Rows[i].Cells[0].Value.ToString());
                            preingresoDetalle.Fecha = DateTime.Now.Date;
                            preingresoDetalle.Hora = DateTime.Now.TimeOfDay;

                            Context.PreingresoDetalle.Add(preingresoDetalle);
                            Context.SaveChanges();
                        }
                    }
                }
                MessageBox.Show("Preingreso creado.",
                    "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                throw;
            }
        }

        private void AgregarProductor(Guid Id,string Fet,string Productor,string Cuit)
        {
           bool Existe = ComprobarExistencia(Id);
           if (Existe.Equals(false))
           {
               this.dgvProductores.Rows.Add(Id, Fet, Productor, Cuit);
               LimpiarEncabezado();
           }
        }
       
        private bool ComprobarExistencia(Guid Id)
        {
            if (dgvProductores.RowCount > 0)
            {
                for (int i = 0; i <= dgvProductores.RowCount-1; i++)
                {
                    if (dgvProductores.Rows[i].Cells[0].Value != null)
                    {
                        var asd = dgvProductores.Rows[i].Cells[0].Value.ToString();
                        if (new Guid(dgvProductores.Rows[i].Cells[0].Value.ToString()) == Id)
                        {
                            var resultado = MessageBox.Show("Este productor ya ha sido agregado.",
                                "Atención", MessageBoxButtons.OK);
                            if (resultado == DialogResult.OK)
                            {
                                txtBuscador.Text = string.Empty;
                                txtCuit.Text = string.Empty;
                                txtNombre.Text = string.Empty;
                                txtFet.Text = string.Empty;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
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
            
        private void LimpiarEncabezado()
        {
            txtBuscador.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtFet.Text = string.Empty;
            txtBuscador.Focus();
        }
        
        private void Limpiar()
        {
            txtBuscador.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtFet.Text = string.Empty;
            txtBuscador.Focus();
            txtPatente.Text = string.Empty;
            txtChofer.Text = string.Empty;
            txtRemito.Text = string.Empty;
            txtTransporte.Text = string.Empty;
            mmObservacion.Text = string.Empty;
            if (dgvProductores.RowCount > 0)
            {
                dgvProductores.Rows.Clear();
            }
        }
        #endregion
    }
}