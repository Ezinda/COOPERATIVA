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
using DevExpress.XtraEditors;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoPesada : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ProductorId;
        private Guid PesadaId;
       
        public Form_RomaneoPesada()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            cbOpcionCompra.SelectedIndex = 0;
            cbBoca.SelectedIndex = 0;
            CargarCombo();
      //      AddNewRow();
            checkBalanzaAutomatica.Checked = true;
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }
        }

        private void Buscar()
        {
            var result = Context.Vw_Preingreso
                    .Where(x => x.Estado == true)
                    .OrderBy(x => x.Fecha)
                    .ThenBy(x => x.Hora)
                    .ToList();

            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var preingreso = result
                    .Where(r => r.Fet.Contains(txtFet.Text))
                    .FirstOrDefault();
                if(preingreso.Fet!=null)
                {
                    ProductorId = preingreso.ProductorId;
                    txtFet.Text = preingreso.Fet.ToString();
                    txtNombre.Text = preingreso.Nombre;
                    txtCuit.Text = preingreso.Cuit;
                    txtProvincia.Text = preingreso.Provincia;
                    txtPreingreso.Text = preingreso.NumeroPreingreso.ToString();
                    GrabarPesada();
                }
            }
        }
  
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtPreingreso.Text = nombre;
            var empleado = Context.Productor
                .Where(x => x.Id == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = empleado.Cuit;
            txtProvincia.Text = empleado.Provincia;
        }

        private void CargarCombo()
        {
            var clase = Context.Clase.ToList();
            cbClase.DataSource = clase;
            cbClase.DisplayMember = "Nombre";
            cbClase.ValueMember = "Id";      
        }

        private void checkBalanzaAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBalanzaAutomatica.Checked == false)
            {
                txtKilos.Enabled = true;
            }
            else
            {
                txtKilos.Enabled = false;
            }
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void btnIniciarPesada_Click(object sender, EventArgs e)
        {
           txtKilos.Text = GetRandomNumber(1, 100).ToString("n2");
        }

        private void btnCancelarPesada_Click(object sender, EventArgs e)
        {
            txtKilos.Text = "";
        }

        private void btnAgregarCaja_Click(object sender, EventArgs e)
        {
            GrabarPesadaDetalle();
            CargarGrilla();
        }

        private void GrabarPesada()
        {
            if (ValidarCamposPesada())
            {
                var resultado = MessageBox.Show("¿Desea iniciar una nueva pesada?",
                    "Crear Preingreso", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GuardarDatosPesada();
            }
        }

        private void GrabarPesadaDetalle()
        {
            if (ValidarCamposPesadaDetalle())
            {
                GuardarDatosPesadaDetalle();
            }
        }

        private int ContadorNumeroPesada()
        {
            var count = Context.Pesada.Count();
            if (count != 0)
            {
                var codigo = Context.Pesada
                    .Max(x => x.NumPesada)
                    .ToString();
                return (Int16.Parse(codigo) + 1);
            }
            else
            {
                return 1;
            }
        }

        private bool ValidarCamposPesada()
        {
            if (ProductorId==null)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private bool ValidarCamposPesadaDetalle()
        {
            if (ProductorId == null && PesadaId == null && cbClase.Text == null && txtKilos.Text == string.Empty)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void GuardarDatosPesada()
        {
            try
            {
                Pesada pesada;
                pesada = new Pesada();
                pesada.Id = Guid.NewGuid();
                PesadaId = pesada.Id;
                pesada.NumPesada = ContadorNumeroPesada();
                int numPreingreso =  Int32.Parse(txtPreingreso.Text);
                var preingreso = Context.Preingreso
                    .Where(x => x.NumeroPreingreso == numPreingreso)
                    .FirstOrDefault();
                pesada.PreingresoId = preingreso.Id;
                pesada.ProductorId = ProductorId;
                
                Context.Pesada.Add(pesada);
                Context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void GuardarDatosPesadaDetalle()
        {
            try
            {
                PesadaDetalle pesadaDetalle;
                pesadaDetalle = new PesadaDetalle();
                pesadaDetalle.Id = Guid.NewGuid();
                pesadaDetalle.PesadaId = PesadaId;
                pesadaDetalle.NumFardo = CalcularNumeroFardo(PesadaId);
                pesadaDetalle.ClaseId = new Guid(cbClase.SelectedValue.ToString());
                pesadaDetalle.Kilos = float.Parse(txtKilos.Text);
              
                Context.PesadaDetalle.Add(pesadaDetalle);
                Context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private int CalcularNumeroFardo(Guid PesadaId)
        {
            int numFardo = 0;
            var pesadaDetalle = Context.PesadaDetalle
                .Where(x=>x.PesadaId == PesadaId)
                .OrderByDescending(x=>x.NumFardo)
                .FirstOrDefault();
            if (pesadaDetalle != null)
            {
                numFardo = pesadaDetalle.NumFardo.Value + 1;
            }
            else
            {
                numFardo = 1;
            }
            return numFardo;
        }

        //private void AddNewRow()
        //{
        //    DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
        //    DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
        //    DataGridViewColumn d3 = new DataGridViewTextBoxColumn();
        //    DataGridViewColumn d4 = new DataGridViewTextBoxColumn();
        //    DataGridViewColumn d5 = new DataGridViewTextBoxColumn();
        //    DataGridViewColumn d6 = new DataGridViewTextBoxColumn();

        //    //Add Header Texts to be displayed on the Columns
        //    d1.HeaderText = "Id";
        //    d2.HeaderText = "PreingresoDetalleId";
        //    d3.HeaderText = "ProductorId";
        //    d4.HeaderText = "Nro. Fardo";
        //    d5.HeaderText = "Clase";
        //    d6.HeaderText = "Kilos";
            
        //    d1.Visible = false;
        //    d2.Width = 120;
        //    d3.Width = 300;
        //    d4.Width = 120;
        //    d5.Width = 120;
        //    d6.Width = 120;
        //    //Add the Columns to the DataGridView
        //    dgvPesada.Columns.AddRange(d1, d2, d3, d4,d5,d6);
        //}

        private void CargarGrilla()
        {
            var result = (
                   from a in Context.Vw_Pesada
                   select new
                   {
                       CAJA = a.NumFardo,
                       CLASE = a.Nombre,
                       KILOS = a.Kilos
                   })
                   .ToList();

            if (result.Count > 0)
            {
                dgvPesada.DataSource = result;
            }
        }
    }
}