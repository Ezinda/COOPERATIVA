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
using System.Timers;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoReclasificacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private string printerTicket;
        private bool ExisteReclasificacion = false;
        private System.Timers.Timer aTimer;
        private string previous = String.Empty;
        private bool gestionreclasificacion = false;

        public Form_RomaneoReclasificacion(Guid? Id)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar(Id);
        }
        
        #region Method Code

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            try
            {
                aTimer.Stop();
                if (!this.IsDisposed)
                {
                    var existefardo = Context.PesadaDetalle
                        .Where(x => x.ReclasificacionId == null)
                        .OrderBy(x => x.NumFardo);


                    if (existefardo.Any().Equals(true))
                    {
                        var fardo = existefardo.FirstOrDefault();
                        txtFardo.Invoke((MethodInvoker)(() => txtFardo.Text = fardo.NumFardo.ToString()));

                        var clase = Context.Vw_Clase
                            .Where(x => x.ID == fardo.ClaseId
                                && x.Vigente == true)
                            .FirstOrDefault();

                        if (clase != null)
                        {
                            txtClase.Invoke((MethodInvoker)(() => txtClase.Text = clase.NOMBRE));

                        }
                        txtReclasificacion.Invoke((MethodInvoker)(() => txtReclasificacion.Focus()));
                        txtReclasificacion.Invoke((MethodInvoker)(() => txtReclasificacion.BackColor = Color.LightSkyBlue));
                    }

                }
            }
            finally
            {
                aTimer.Start();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarClasificacion();
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
                && txtReclasificacion.Text != string.Empty
                && ExisteReclasificacion == false)
            {
                //var current = txtReclasificacion.Text;

                //if (current == previous)
                //{
                //    return;
                //}

                //LimpiarTxtReclasificacion();
                //txtReclasificacion.SelectionStart = txtReclasificacion.Text.ToCharArray().Length;
                //txtReclasificacion.SelectionLength = 0;
                ActualizarClasificacion();
            }
        }
        
        private void LimpiarTxtReclasificacion()
        {
            var current = txtReclasificacion.Text;

            if (previous != String.Empty)
            {
                var index = current.IndexOf(previous);
                
                if (index == 0)
                {
                    try
                    {
                        var newvalue = current.Substring(previous.Length);
                        previous = newvalue;
                        txtReclasificacion.Text = newvalue;
                        return;
                    }
                    catch
                    {
                    }
                }
            }
            previous = current;
        }

        #endregion

        #region Method Dev
        
        private void Iniciar(Guid? Id)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
           
            if (ValidarDebug().Equals(false))
            {
                string strFileConfig = @"Config.ini";
                IniParser parser = new IniParser(strFileConfig);
                printerTicket = parser.GetSetting("AppSettings", "PrinterTicketReclasificacion");
            }

            if (Id == null)
            {
                var existefardo =
                    (from p in Context.Pesada.Where(x => x.RomaneoPendiente == true)
                     join pd in Context.PesadaDetalle
                     .Where(x => x.ReclasificacionId == null)
                     on p.Id equals pd.PesadaId
                     select new
                     {
                         NumFardo = pd.NumFardo,
                         ClaseId = pd.ClaseId
                     })
                     .OrderBy(x => x.NumFardo);

                if (existefardo.Any().Equals(true))
                {
                    var fardo = existefardo.FirstOrDefault(); 
                    txtFardo.Text = fardo.NumFardo.ToString();
                    var clase = Context.Vw_Clase
                        .Where(x => x.ID == fardo.ClaseId
                            && x.Vigente == true)
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
                    aTimer = new System.Timers.Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    aTimer.Interval = 2000;
                    aTimer.Enabled = true;
                }
            }
            else
            {
                gestionreclasificacion = true;

                var fardo = Context.PesadaDetalle
                    .Where(x => x.Id == Id)
                    .OrderBy(x => x.NumFardo)
                    .FirstOrDefault();

                if (fardo != null)
                {
                    txtFardo.Text = fardo.NumFardo.ToString();
                    var clase = Context.Vw_Clase
                        .Where(x => x.ID == fardo.ClaseId
                            && x.Vigente == true)
                        .FirstOrDefault();

                    if (clase != null)
                    {
                        txtClase.Text = clase.NOMBRE;
                    }
                    if (fardo.ReclasificacionId != null)
                    {
                        var reclasificacion = Context.Vw_Clase
                            .Where(x => x.ID == fardo.ReclasificacionId
                                && x.Vigente == true)
                            .FirstOrDefault();

                        if (reclasificacion != null)
                        {
                            ExisteReclasificacion = true;
                            txtReclasificacion.Text = reclasificacion.NOMBRE;
                        }
                    }

                    txtReclasificacion.Focus();
                    txtReclasificacion.BackColor = Color.LightSkyBlue;
                }
            }
        }

        private void ActualizarClasificacion()
        {
            if (ValidarReclasificacion())
            {
                if (!string.IsNullOrEmpty(txtFardo.Text))
                {
                    Reclasificar(gestionreclasificacion,txtFardo.Text);
                }
            }
        }

        private bool ValidarReclasificacion()
        {
            if (txtFardo.Text == string.Empty && txtClase.Text == null 
                && txtReclasificacion.Text == null)
            {
                MessageBox.Show("No se ha seleccionado un fardo.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void Reclasificar(bool gestionReclasificacion,string numFardo)
        {
            long salida = 0;
            if (long.TryParse(numFardo, out salida))
            {
                long fardo = long.Parse(numFardo);

                var pesadadet = Context.PesadaDetalle
                        .Where(x => x.NumFardo == fardo)
                        .FirstOrDefault();

                if (pesadadet != null)
                {
                    var clase = Context.Vw_Clase
                        .Where(x => x.NOMBRE.Equals(txtReclasificacion.Text))
                        .FirstOrDefault();

                    if (clase != null)
                    {
                        var pesadaDetalle = Context.PesadaDetalle.Find(pesadadet.Id);
                        pesadaDetalle.ReclasificacionId = clase.ID;
                        Context.Entry(pesadaDetalle).State = EntityState.Modified;
                        Context.SaveChanges();

                        if (ValidarDebug().Equals(false))
                        {
                            if ((gestionReclasificacion.Equals(false) && ValidarImpresion().Equals(true)) || 
                                gestionReclasificacion.Equals(true) && ValidarImpresionGestionReclasificacion().Equals(true))
                            {
                                string combinadoReclasificacion = txtClase.Text.Substring(0, 1) + " - " + txtReclasificacion.Text;
                                PrintTicket(txtFardo.Text, txtClase.Text, combinadoReclasificacion,
                                    pesadaDetalle.Kilos.ToString());
                            }
                        }
                        Limpiar();
                        Iniciar(null);

                        IEnlaceActualizar mienlace = this.Owner as Form_RomaneoGestionClasificacion;
                        if (mienlace != null)
                        {
                            mienlace.Enviar(true);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void Limpiar()
        {
            txtFardo.Text = string.Empty;
            txtClase.Text = string.Empty;
            txtReclasificacion.Text = string.Empty;
            txtReclasificacion.BackColor = Color.LightSkyBlue;
        }

        private void PrintTicket(string fardo, string clase,string reclasificacion, string lectura)
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
            s = s + "^FO90,400^FDFARDO " + fardo + "  CLASE " + clase + "  KILOS " + lectura + "^FS";
            s = s + "^BY3,2,270";
            s = s + "^FO160,550^BC^FD" + fardo + "^FS";
            s = s + "^FO90,900^FDFARDO " + fardo +" CLASE " + reclasificacion  +" KILOS "+ lectura +"^FS";
            s = s + "^XZ";

            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            //pd.ShowDialog();
            //RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s);
            RawPrinterHelper.SendStringToPrinter(printerTicket, s);
        }

        private bool ValidarImpresion()
        {
            var reclasificacion = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.ImpresiónReclasificacion)
              .FirstOrDefault();

            return reclasificacion.Valor;
        }

        private bool ValidarDebug()
        {
            var debug = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.Debug)
              .FirstOrDefault();

            return debug.Valor;
        }

        private bool ValidarImpresionGestionReclasificacion()
        {
            var gestionreclasificacion = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.ImpresiónGestionReclasificacion)
              .FirstOrDefault();

            return gestionreclasificacion.Valor;
        }

        #endregion

        private void txtReclasificacion_TextAlignChanged(object sender, EventArgs e)
        {

        }
    }
}