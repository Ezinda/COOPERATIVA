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
    public partial class Form_RomaneoPreingreso : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {   
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;
       
        public Form_RomaneoPreingreso()
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
            txtBuscador.Text = string.Empty;
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
            if (ProductorId != Guid.Empty)
            {
                AgregarProductor(ProductorId, txtFet.Text, txtNombre.Text, txtCuit.Text);
            }
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
                    txtBuscador.Text = string.Empty;
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
                    valorCelda = string.Empty;
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
                from a in Context.Vw_Productor
                select new
                {
                    full = a.nrofet + a.NOMBRE + a.CUIT,
                    ID = a.ID,
                    FET = a.nrofet,
                    PRODUCTOR = a.NOMBRE,
                    CUIT = a.CUIT
                });

            if (!string.IsNullOrEmpty(txtBuscador.Text))
            {
                var count = Context.Vw_Productor
                    .Where(x => x.nrofet.Contains(txtBuscador.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtBuscador.Text;
                    _formBuscarProductor.target = DevConstantes.Preingreso;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtBuscador.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtNombre.Text = busqueda.PRODUCTOR.ToString();
                        txtCuit.Text = busqueda.CUIT.ToString();

                    }
                    else
                    {
                        count = Context.Vw_Productor
                            .Where(x => x.NOMBRE.Contains(txtBuscador.Text))
                            .Count();
                        if (count > 1)
                        {
                            _formBuscarProductor = new Form_AdministracionBuscarProductor();
                            _formBuscarProductor.nombre = txtBuscador.Text;
                            _formBuscarProductor.target = DevConstantes.Preingreso;
                            _formBuscarProductor.BuscarNombre();
                            _formBuscarProductor.ShowDialog(this);
                        }
                        else
                        {
                            var busquedaNombre = Context.Vw_Productor
                                .Where(x => x.NOMBRE.Contains(txtBuscador.Text))
                                .FirstOrDefault();
                            if (busqueda != null)
                            {
                                ProductorId = busquedaNombre.ID.Value;
                                txtFet.Text = busquedaNombre.nrofet.ToString();
                                txtNombre.Text = busquedaNombre.NOMBRE.ToString();
                                txtCuit.Text = busquedaNombre.CUIT.ToString();
                            }
                            else
                            {
                                count = Context.Vw_Productor
                                    .Where(x => x.CUIT.Contains(txtBuscador.Text))
                                    .Count();
                                if (count > 1)
                                {
                                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                                    _formBuscarProductor.cuit = txtBuscador.Text;
                                    _formBuscarProductor.target = DevConstantes.Preingreso;
                                    _formBuscarProductor.BuscarCuit();
                                    _formBuscarProductor.ShowDialog(this);
                                }
                                else
                                {
                                    var busquedaCuit = Context.Vw_Productor
                                        .Where(x => x.CUIT.Contains(txtBuscador.Text))
                                        .FirstOrDefault();
                                    if (busquedaCuit != null)
                                    {
                                        ProductorId = busquedaCuit.ID.Value;
                                        txtFet.Text = busquedaCuit.nrofet.ToString();
                                        txtNombre.Text = busquedaCuit.NOMBRE.ToString();
                                        txtCuit.Text = busquedaCuit.CUIT.ToString();
                                    }
                                }
                            }
                        }
                    }
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
            if (dgvProductores.RowCount <= 1)
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
                            preingresoDetalle.Estado = true;

                            Context.PreingresoDetalle.Add(preingresoDetalle);
                            Context.SaveChanges();
                        }
                    }
                }
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
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = empleado.CUIT;
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