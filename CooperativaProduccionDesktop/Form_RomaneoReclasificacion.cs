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
using System.Drawing.Printing;
using CooperativaProduccion.Helpers;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoReclasificacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_RomaneoReclasificacion()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar();
        }
        
        #region Method Code

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarClasificacion();
        }

        private void txtBuscador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                Buscar(txtFardo.Text);
                txtFardo.BackColor = Color.White;
                txtReclasificacion.Focus();
                txtReclasificacion.BackColor = Color.LightSkyBlue;
            }

            if (e.KeyChar == 8)
            {
                Limpiar();
            }
        }

        private void txtFardo_TextChanged(object sender, EventArgs e)
        {
            if (checkAutomaticoFardo.Checked && txtFardo.Text != string.Empty)
            {
                Buscar(txtFardo.Text);
                txtFardo.BackColor = Color.White;
                txtReclasificacion.Focus();
                txtReclasificacion.BackColor = Color.LightSkyBlue;
            }
        }

        private void checkAutomaticoFardo_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkAutomaticoFardo.Checked)
            {
                Limpiar();
            }
        }

        private void checkAutomaticaClase_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkAutomaticaClase.Checked)
            {
                txtReclasificacion.Text = string.Empty;
                txtReclasificacion.BackColor = Color.LightSkyBlue;
                txtReclasificacion.Focus();
            }
        }

        private void txtReclasificacion_TextChanged(object sender, EventArgs e)
        {
            if (checkAutomaticaClase.Checked
                && txtFardo.Text != string.Empty
                && txtReclasificacion.Text != string.Empty)
            {
                ActualizarClasificacion();
            }
        }

        #endregion

        #region Method Dev
        
        private void Iniciar()
        {
            var fardo = Context.PesadaDetalle
                .Where(x => x.ReclasificacionId == null)
                .OrderByDescending(x => x.NumFardo)
                .FirstOrDefault();

            if (fardo != null)
            {
                txtFardo.Text = fardo.NumFardo.ToString();
                var clase = Context.Vw_Clase
                    .Where(x => x.ID == fardo.ClaseId && x.Vigente == true)
                    .FirstOrDefault();

                if (clase != null)
                {
                    txtClase.Text = clase.NOMBRE;
                }
                txtReclasificacion.Focus();
                txtReclasificacion.BackColor = Color.LightSkyBlue;
            }
            else
            {
                txtFardo.Focus();
                txtFardo.BackColor = Color.LightSkyBlue;
                txtReclasificacion.BackColor = Color.White;
            }
        }

        private void Buscar(string numFardo)
        {
            long fardo = long.Parse(numFardo);
            var pesada = Context.Vw_Pesada
                    .Where(x => x.NumFardo == fardo )
                    .FirstOrDefault();

            if (pesada != null)
            {
                txtClase.Text = pesada.Clase;
            }
        }

        private void ActualizarClasificacion()
        {
            if (ValidarReclasificacion())
            {
                Reclasificar(txtFardo.Text);
            }
        }

        private bool ValidarReclasificacion()
        {
            if (txtFardo.Text == null && txtClase.Text == null 
                && txtReclasificacion.Text == null)
            {
                MessageBox.Show("No se ha seleccionado un fardo.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void Reclasificar(string numFardo)
        {
            long fardo = long.Parse(numFardo);
            var pesadadet = Context.PesadaDetalle
                    .Where(x => x.NumFardo == fardo )
                    .FirstOrDefault();

            if (pesadadet != null)
            {
                var pesadaDetalle = Context.PesadaDetalle.Find(pesadadet.Id);

                var clase = Context.Vw_Clase
                    .Where(x => x.NOMBRE.Equals(txtReclasificacion.Text))
                    .FirstOrDefault();

                if (clase != null)
                {
                    pesadaDetalle.ReclasificacionId = clase.ID;
                    pesadaDetalle.ReclasificacionPrecio = clase.PRECIOCOMPRA;
                    Context.Entry(pesadaDetalle).State = EntityState.Modified;
                    Context.SaveChanges();
                    PrintTicket(txtFardo.Text, txtReclasificacion.Text, pesadaDetalle.Kilos.ToString());
                    Limpiar();
                }
            }
        }

        private void Limpiar()
        {
            txtFardo.Text = string.Empty;
            txtFardo.BackColor = Color.LightSkyBlue;
            txtClase.Text = string.Empty;
            txtReclasificacion.Text = string.Empty;
            txtReclasificacion.BackColor = Color.White;
        }

        private void PrintTicket(string fardo, string clase, string lectura)
        {
            string s = "^XA";
            s = s + "^FX Top section with company logo, name and address.";
            s = s + "^LH25,50";
            s = s + "^PW900";
            s = s + "^CF0,40";
            s = s + "^FO120,50^FDCOOPERATIVA DE PRODUCTORES^FS";
            s = s + "^CF0,40";
            s = s + "^FO120,100^FDAGROPECUARIOS DEL TUCUMAN^FS";
            s = s + "^FO320,150^FDLTDA.^FS";
            s = s + "^CF0,30";
            s = s + "^FO130,250^FDRUTA 38 KM 699-LA INVERNADA^FS";
            s = s + "^FO310,290^FDDPTO. LA COCHA^FS";
            s = s + "^FX Third section with barcode.";
            s = s + "^CF0,35";
            s = s + "^FO90,400^FDFARDO " + fardo + "  CLASE " + clase + "  KILOS " + lectura + " - RE^FS";
            s = s + "^BY3,2,270";
            s = s + "^FO160,550^BC^FD" + fardo + "^FS";
            s = s + "^XZ";

            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            //pd.ShowDialog();
            RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s);
        }

        #endregion
    }
}