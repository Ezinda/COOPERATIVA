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
using System.Data.Entity;
using CooperativaProduccion.Reports;
using System.Globalization;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.ReportModels;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Utils;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Printing;
using CooperativaProduccion.Helpers;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoPesada : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ProductorId;
        private Guid PesadaId;
        private Guid TipoTabacoId;
        private string totalfardo;
        private string totalkilo;
        private string importebruto;
        private string printerTicket;
        private bool continuar;
        private Form_RomaneoPesadaMostrador _pesadaMostrador;
        private Form_AdministracionBuscarProductor _formBuscarProductor;

        #region balanza - lectura 

        private string lectura;
        private static TimeSpan _horaInicio;
        private static List<RegPesada> _bufferentrada;
        private static RegPesada _registrotemporal;
        private static List<RegPesada> _buffersalida;
        private static double _intervalodecomprobacion = 1.5;
        private static decimal _minimonuevaentrada = 10;
        private static decimal _bajadaynuevaentrada_porcentaje = 10;
        private static System.Timers.Timer _timer;
        private string previous = String.Empty;

        private bool SerialPortPendingClose = false;
        private bool SerialPortClosed = false;
        private bool _DEBUG = false;

        #endregion

        public Form_RomaneoPesada(Guid? Id)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar(Id);
            CargarCombo();
            #region Balanza

            _bufferentrada = new List<RegPesada>();
            _buffersalida = new List<RegPesada>();
            _registrotemporal = null;

            m_serialPort1.BaudRate = 9600;
            m_serialPort1.Parity = Parity.None;
            m_serialPort1.StopBits = StopBits.One;
            m_serialPort1.DataBits = 8;
            m_serialPort1.Handshake = Handshake.None;
            m_serialPort1.RtsEnable = true;

            _horaInicio = DateTime.Now.TimeOfDay;
            _timer = new System.Timers.Timer(1000 * _intervalodecomprobacion);
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;

            if (ValidarDebug().Equals(false))
            {
                SerialPortPendingClose = false;
                SerialPortClosed = false;
                m_serialPort1.Open();
                m_serialPort1.DataReceived += new SerialDataReceivedEventHandler(m_serialPort1_DataReceived);
            }
            else
            {
                _DEBUG = true;
            }

            #endregion
        }

        #region Method Code

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();

                //decimal minimopeso = 0;
                decimal maximoValor = 0;
                TimeSpan hora = TimeSpan.Zero;

                foreach (var item in _bufferentrada)
                {
                    if (maximoValor < item.Valor)
                    {
                        maximoValor = item.Valor;
                        hora = item.Hora;
                    }
                }

                if (maximoValor >= _minimonuevaentrada)
                {
                    var registroactual = new RegPesada()
                    {
                        Hora = hora - _horaInicio,
                        Valor = maximoValor
                    };

                    if (_registrotemporal == null)
                    {
                        _registrotemporal = registroactual;
                    }
                    else
                    {
                        var bajadaynuevaentrada = (_bajadaynuevaentrada_porcentaje * _registrotemporal.Valor) / 100;

                        if (registroactual.Valor >= _registrotemporal.Valor)
                        {
                            _registrotemporal = registroactual;
                        }
                        else if ((registroactual.Valor - bajadaynuevaentrada) <= (_registrotemporal.Valor - bajadaynuevaentrada))
                        {
                            _buffersalida.Add(_registrotemporal);

                            System.Console.WriteLine(_registrotemporal.ToString());
                            if (!this.IsDisposed)
                            {
                                if (_registrotemporal.Valor.ToString() != string.Empty)
                                {
                                    txtKilos.Invoke((MethodInvoker)(() => txtKilos.Text = _registrotemporal.Valor.ToString()));
                                    SaveAndPrintKg(PesadaId);
                                }
                            }
                            _registrotemporal = null;
                        }
                    }
                }

                _bufferentrada.Clear();
            }
            finally
            {
                _timer.Start();
            }
        }

        private void btnPesadaMostrador_ItemClick(object sender, ItemClickEventArgs e)
        {
            _pesadaMostrador = new Form_RomaneoPesadaMostrador();
            _pesadaMostrador.nombre = txtNombre.Text;
            _pesadaMostrador.cuit = txtCuit.Text;
            _pesadaMostrador.CargarDatos();
            _pesadaMostrador.Show();
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
                cbTabaco.Focus();
            }

            if (e.KeyChar == 8)
            {
                txtNombre.Text = string.Empty;
                txtPreingreso.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                PasarMostrador(txtNombre.Text, txtCuit.Text);
            }
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnIniciarPesada_Click(object sender, EventArgs e)
        {       
            GrabarPesada(continuar);
            _timer.Start();
            txtClase.Focus();
        }

        private void btnCancelarPesada_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea cancelar la pesada?",
                "Confirmación", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            CancelarPesada();
        }
   
        private void checkBalanzaAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBalanzaAutomatica.Checked == false)
            {
                txtKilos.Enabled = true;
                txtKilos.Text = string.Empty;
                _timer.Stop();
            }
            else
            {
                txtKilos.Enabled = false;
                txtKilos.Text = string.Empty;
                _timer.Start();
            }
        }
  
        private void btnAgregarCaja_Click(object sender, EventArgs e)
        {
            if (txtKilos.Text != string.Empty && txtClase.Text != string.Empty)
            {
                SaveAndPrintKg(PesadaId);
                //TipoTabacoId = Guid.Parse(cbTabaco.SelectedValue.ToString());
                //GrabarPesadaDetalle(TipoTabacoId);
                //CargarGrilla();
                //CalcularTotales();
                //ActualizarPesada(PesadaId, false);
                //PasarFardoMostrador(false);
                //txtKilos.Invoke((MethodInvoker)(() => txtKilos.Text = "0"));
                //txtClase.Invoke((MethodInvoker)(() => txtClase.Text = string.Empty));
            }
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            ImprimirEtiqueta();
        }
       
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea eliminar la pesada seleccionada?",
               "Eliminar Datos", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            EliminarDatos();
            CargarGrilla();
            CalcularTotales();
            PasarFardoMostrador(true);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            totalfardo = txtTotalFardo.Text;
            totalkilo = txtTotalKilo.Text;
            importebruto = txtImporteBruto.Text;
            ActualizarPesada(PesadaId,true);
            var pesadavieja = PesadaId;
            PesadaId = Guid.NewGuid();
            continuar = false;
            Deshabilitar();
            ImpimirRomaneo(pesadavieja);
            PasarMostrador(string.Empty, string.Empty);
            PasarFardoMostrador(true);
            //m_serialPort1.Close();//Cerramos puerto
            //m_serialPort1.Dispose();//Liberamos recursos
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Closing Timer");

                _timer.Stop();
                _timer.Close();

                Log("End Closing Timer");

                if (_DEBUG == false)
                {
                    Log("Closing SerialPort");
                    SerialPortPendingClose = true;

                    while (SerialPortClosed == false)
                    {
                        ;
                    }

                    m_serialPort1.Close();//Cerramos puerto

                    Log("End Closing SerialPort");
                }
                
                if (_pesadaMostrador != null && !_pesadaMostrador.IsDisposed)
                {
                    Log("Closing Mostrador");

                    _pesadaMostrador.Close();

                    Log("End Closing Mostrador");
                }
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
            }

            this.Close();
        }

        private void Log(string v)
        {
            File.AppendAllText("Log.txt", v + Environment.NewLine);
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {

        }

        private void txtClase_TextChanged(object sender, EventArgs e)
        {
            //if (checkBalanzaAutomatica.Checked)
            //{
            //    var current = txtClase.Text;
            //
            //    if (current == previous)
            //    {
            //        return;
            //    }
            //
            //    LimpiarTxtClase();
            //    txtClase.SelectionStart = txtClase.Text.ToCharArray().Length;
            //    txtClase.SelectionLength = 0;
            //}
        }

        private void LimpiarTxtClase()
        {
            var current = txtClase.Text;

            if (previous != String.Empty)
            {
                var index = current.IndexOf(previous);

                if (index == 0)
                {
                    try
                    {
                        var newvalue = current.Substring(previous.Length);
                        previous = newvalue;
                        txtClase.Text = newvalue;
                        return;
                    }
                    catch
                    {
                    }
                }
            }
            previous = current;
        }

        private void m_serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SerialPortPendingClose)
            {
                SerialPortClosed = true;
            }
            else
            {
                m_serialPort1.ReadTimeout = 2000; //El timeout es esencial para parar la conexion pasado un tiempo. En este caso 2 segundos.
                try
                {
                    lectura = m_serialPort1.ReadLine();

                    byte[] bytesDeLectura = Encoding.ASCII.GetBytes(lectura);

                    var bytesNeto = bytesDeLectura.Skip(2).Take(9).ToArray();

                    var valorstr = Encoding.ASCII.GetString(bytesNeto);

                    decimal valor;

                    try
                    {
                        valor = Convert.ToDecimal(valorstr);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    var registro = new RegPesada()
                    {
                        Hora = DateTime.Now.TimeOfDay,
                        Valor = valor
                    };

                    _bufferentrada.Add(registro);

                    // txtKilos.Text = valor.ToString();
                    // txtLectura.Text = valor.ToString();

                    //Leemos una linea del puerto
                    // txtBalanza.Text = lectura;
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        #endregion

        #region Method Dev
        
        private void CargarCombo()
        {
            var tipotabaco = Context.Vw_TipoTabaco
                .Where(x => x.RUBRO_ID != null)
                .ToList();

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";
        }

        private void Iniciar(Guid? Id)
        {
            _pesadaMostrador = new Form_RomaneoPesadaMostrador();
            _pesadaMostrador.Show();
            Deshabilitar();
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            if (ValidarDebug().Equals(false))
            {
                string strFileConfig = @"Config.ini";
                IniParser parser = new IniParser(strFileConfig);
                printerTicket = parser.GetSetting("AppSettings", "PrinterTicketBalanza");
            }

            if (Id != null)
            {
                continuar = true;
                PesadaId = Id.Value;
                var pesada = Context.Pesada
                    .Where(x => x.Id == Id)
                    .FirstOrDefault();

                if (pesada != null)
                {
                    ProductorId = pesada.ProductorId.Value;
                    var productor = Context.Vw_Productor
                        .Where(x => x.ID == pesada.ProductorId)
                        .FirstOrDefault();

                    if (productor != null)
                    {
                        txtFet.Text = productor.nrofet;
                        txtCuit.Text = productor.CUIT;
                        txtNombre.Text = productor.NOMBRE;
                        txtProvincia.Text = productor.Provincia;
                        txtTotalFardo.Text = pesada.TotalFardo.Value.ToString();
                        txtTotalKilo.Text = pesada.TotalKg.Value.ToString();
                        txtImporteBruto.Text = pesada.ImporteBruto.Value.ToString();
                        txtPrecioPromedio.Text = pesada.PrecioPromedio.Value.ToString();

                        var result =
                            (from a in Context.Vw_Pesada
                                 .Where(x => x.PesadaId == pesada.Id)
                             select new RowPesada
                             {
                                 ID = a.PesadaDetalleId,
                                 NUMERO_FARDO = a.NumFardo.Value,
                                 CONTADOR_CAJA = a.ContadorFardo.Value,
                                 CLASE = a.Clase,
                                 KILOS = a.Kilos.Value,
                                 SUBTOTAL = a.Subtotal.Value
                             })
                            .OrderByDescending(x => x.CONTADOR_CAJA)
                            .ToList();
                        
                        if (result != null)
                        {
                            gridControlPesada.DataSource = result;
                        }
                    }
                }
                else
                {
                    gridControlPesada.DataSource = new BindingList<RowPesada>();
                }
                gridViewPesada.Columns["ID"].Visible = false;
                gridViewPesada.Columns[1].Caption = "Número Fardo";
                gridViewPesada.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[2].Caption = "Contador Caja";
                gridViewPesada.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[3].Caption = "Clase";
                gridViewPesada.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[4].Caption = "Kilos";
                gridViewPesada.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[5].Visible = false;
            }
        }
        
        private void CalcularTotales()
        {
            txtTotalFardo.Invoke((MethodInvoker)(() => txtTotalFardo.Text = CalcularTotalFardo(PesadaId).ToString()));
            txtTotalKilo.Invoke((MethodInvoker)(() => txtTotalKilo.Text = CalcularTotalKilos(PesadaId).ToString()));
            txtImporteBruto.Invoke((MethodInvoker)(() => txtImporteBruto.Text = CalcularTotalImporteBruto(PesadaId).ToString()));
            txtPrecioPromedio.Invoke((MethodInvoker)(() => txtPrecioPromedio.Text = CalcularPrecioPromedio(PesadaId).ToString()));
        }

        private void Buscar()
        {
            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                #region Preingreso - Deshabilitado
                //var result = Context.Vw_Preingreso
                //   .Where(x => x.Estado == true
                //       && x.FET.Contains(txtFet.Text))
                //   .OrderBy(x => x.Fecha)
                //   .ThenBy(x => x.Hora);

                //if (result.Any().Equals(true))
                //{
                //    if (result.Count() > 1)
                //    {
                //        _formBuscarPreingreso = new Form_RomaneoBuscarPreingreso();
                //        _formBuscarPreingreso.fet = txtFet.Text;
                //        _formBuscarPreingreso.target = DevConstantes.Pesada;
                //        _formBuscarPreingreso.BuscarFet();
                //        _formBuscarPreingreso.ShowDialog(this);
                //    }
                //    else if(result.Count() == 1)
                //    {
                //        var preingreso = result
                //            .Where(r => r.FET.Contains(txtFet.Text))
                //            .FirstOrDefault();

                //        if (preingreso != null)
                //        {
                //            ProductorId = preingreso.ProductorId.Value;
                //            txtFet.Text = preingreso.FET.ToString();
                //            txtNombre.Text = preingreso.Nombre;
                //            txtCuit.Text = preingreso.Cuit;
                //            txtProvincia.Text = preingreso.Provincia;
                //            txtPreingreso.Text = preingreso.NumeroPreingreso.ToString();
                //            GrabarPesada();
                //            PasarMostrador(txtNombre.Text, txtCuit.Text);
                //        }
                //    }
                //}
                //else
                //{
                    //BuscarProductor();
                //}
                #endregion
                BuscarProductor();
            }
        }
  
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ? 
                string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ? 
                string.Empty : empleado.Provincia.ToString();
        }
      
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
     
        private float CalcularTotalImporteBruto(Guid PesadaId)
        {
            float totalKilos = 0;
            var pesadas = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId);
            foreach (var pesada in pesadas)
            {
                totalKilos = totalKilos + Convert.ToSingle(pesada.Subtotal);
            }

            return totalKilos;
        }

        private int CalcularTotalFardo(Guid PesadaId)
        {
            int totalFardo = 0;
            var count = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId)
                .Count();
            if (count != 0)
            {
                totalFardo = count;
            }
            else
            {
                totalFardo = 0;
            }
            return totalFardo;
        }

        private float CalcularTotalKilos(Guid PesadaId)
        {
            float totalKilos = 0;
            var pesadas = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId).ToList();
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    totalKilos = totalKilos + Convert.ToSingle(pesada.Kilos.Value);
                }
            }
            else
            {
                totalKilos = 0;
            }

            return totalKilos;
        }

        private float CalcularPrecioPromedio(Guid PesadaId)
        {
            float totalpromedio = 0;
            float precioPromedio = 0;
            var pesadas = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId);
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    precioPromedio = precioPromedio + Convert.ToSingle(pesada.ClasePrecio.Value.ToString());
                    var count = Context.PesadaDetalle
                              .Where(x => x.PesadaId == PesadaId)
                              .Count();
                    totalpromedio = float.Parse(Math.Round(precioPromedio / count, 2).ToString());
                }
            }
            else
            {
                precioPromedio = 0;
            }           
            
            return totalpromedio;
        }

        private void GrabarPesada(bool continuar)
        {
            if (ValidarCamposPesada())
            {
                if (continuar.Equals(false))
                {
                    var resultado = MessageBox.Show("¿Desea iniciar una nueva pesada?",
                        "Crear Preingreso", MessageBoxButtons.OKCancel);
                    if (resultado != DialogResult.OK)
                    {
                        return;
                    }
                    GuardarDatosPesada();
                    CalcularTotales();
                }
                Habilitar();
                this.btnIniciarPesada.Enabled = false;
                this.txtClase.Focus();
            }
        }

        private void GrabarPesadaDetalle(Guid TipoTabacoId)
        {
            if (ValidarCamposPesadaDetalle())
            {
                GuardarDatosPesadaDetalle(TipoTabacoId);
            }
        }

        private long ContadorNumeroPesada()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.Pesada))
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

        private long ContadorNumeroRomaneo()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.Romaneo))
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

        private bool ValidarCamposPesada()
        {
            if (ProductorId == null)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidarCamposPesadaDetalle()
        {
            if (ProductorId == null && PesadaId == null && 
                TipoTabacoId == null && txtKilos.Text == string.Empty)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private int NumeroPuntoVentaRomaneo()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.PuntoVentaRomaneo))
                .FirstOrDefault();

            if (contador != null)
            {
                return int.Parse(contador.Valor.ToString());
            }
            else
            {
                return 1;
            }
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
                #region Preingreso - Deshabilitado
                //int numPreingreso = Int32.Parse(txtPreingreso.Text);
                //var preingreso = Context.Preingreso
                //    .Where(x => x.NumeroPreingreso == numPreingreso)
                //    .FirstOrDefault();
                //pesada.PreingresoId = preingreso.Id;
                #endregion
                pesada.ProductorId = ProductorId;
                pesada.FechaRomaneo = DateTime.Now.Date;
                pesada.PuntoVentaRomaneo = NumeroPuntoVentaRomaneo();
                pesada.NumRomaneo = ContadorNumeroRomaneo();
                pesada.IvaPorcentaje = decimal.Parse(DevConstantes.Iva,CultureInfo.InvariantCulture);
                pesada.RomaneoPendiente = true;
                pesada.TipoTabacoId = Guid.Parse(cbTabaco.SelectedValue.ToString());
                pesada.TotalFardo = Int32.Parse("0");
                pesada.TotalKg = float.Parse("0");
                pesada.ImporteBruto = decimal.Parse("0");
                pesada.PrecioPromedio = decimal.Parse("0");
                Context.Pesada.Add(pesada);
                Context.SaveChanges();

                #region Actualizar contador

                var contador = Context.Contador
                    .Where(x=>x.Nombre.Equals(DevConstantes.Pesada))
                    .FirstOrDefault();
                var count = Context.Contador.Find(contador.Id);
                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    Context.Entry(count).State = EntityState.Modified;
                    Context.SaveChanges();
                }
                contador = Context.Contador
                    .Where(x => x.Nombre.Equals(DevConstantes.Romaneo))
                    .FirstOrDefault();
                count = Context.Contador.Find(contador.Id);
                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    Context.Entry(count).State = EntityState.Modified;
                    Context.SaveChanges();
                }

                #endregion
            }
            catch
            {
                throw;
            }
        }

        private void GuardarDatosPesadaDetalle(Guid TipoTabacoId)
        {
            try
            {
                #region Pesada Detalle

                PesadaDetalle pesadaDetalle;
                pesadaDetalle = new PesadaDetalle();
                pesadaDetalle.Id = Guid.NewGuid();
                pesadaDetalle.PesadaId = PesadaId;
                pesadaDetalle.ContadorFardo = ContadorNumeroFardo(PesadaId);
                pesadaDetalle.NumFardo = NumeradorFardo();
                var clase = Context.Vw_Clase
                    .Where(x => x.Vigente == true 
                        && x.NOMBRE == txtClase.Text 
                        && x.ID_PRODUCTO == TipoTabacoId)
                    .FirstOrDefault();
                pesadaDetalle.ClaseId = clase.ID;
                pesadaDetalle.Kilos = float.Parse(Math.Round(decimal.Parse(txtKilos.Text), 0).ToString());
                pesadaDetalle.ClasePrecio = clase.PRECIOCOMPRA;
                Context.PesadaDetalle.Add(pesadaDetalle);
                Context.SaveChanges();

                #endregion

                #region Movimiento

                Movimiento movimiento;
                movimiento = new Movimiento();
                movimiento.Id = Guid.NewGuid();
                movimiento.Fecha = DateTime.Now.Date;
                movimiento.TransaccionId = pesadaDetalle.Id;
                movimiento.Unidad = DevConstantes.Kg;
                movimiento.Ingreso = pesadaDetalle.Kilos;
                Context.Movimiento.Add(movimiento);
                Context.SaveChanges();

                #endregion
            }
            catch
            {
                throw;
            }
        }

        private long ContadorNumeroFardo(Guid PesadaId)
        {
            long numFardo = 0;
            var pesadaDetalle = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId)
                .OrderByDescending(x => x.ContadorFardo)
                .FirstOrDefault();
            if (pesadaDetalle != null)
            {
                numFardo = pesadaDetalle.ContadorFardo.Value + 1;
            }
            else
            {
                numFardo = 1;
            }
            return numFardo;
        }

        private long NumeradorFardo()
        {
            long numFardo = 0;
            var pesadaDetalle = Context.PesadaDetalle
                .OrderByDescending(x => x.NumFardo)
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

        private void CargarGrilla()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            var result = (
                from a in Context.Vw_Pesada
                    .Where(x => x.PesadaId == PesadaId)
                select new RowPesada
                {
                    ID = a.PesadaDetalleId,
                    NUMERO_FARDO = a.NumFardo.Value,
                    CONTADOR_CAJA = a.ContadorFardo.Value,
                    CLASE = a.Clase,
                    KILOS = a.Kilos.Value,
                    SUBTOTAL = a.Subtotal.Value
                })
                .OrderByDescending(x => x.CONTADOR_CAJA)
                .ToList();

            if (result.Count > -1)
            {
                gridControlPesada.Invoke((MethodInvoker)(() => {
                    gridControlPesada.DataSource = result;

                    gridViewPesada.Columns["ID"].Visible = false;
                    gridViewPesada.Columns[1].Caption = "Número Fardo";
                    gridViewPesada.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[2].Caption = "Contador Caja";
                    gridViewPesada.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[3].Caption = "Clase";
                    gridViewPesada.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[4].Caption = "Kilos";
                    gridViewPesada.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewPesada.Columns[5].Visible = false;
                }));
            }
        }

        private void EliminarDatos()
        {
            Guid Id;
            if (gridViewPesada.SelectedRowsCount > 0)
            {
                Id = new Guid(gridViewPesada
                    .GetRowCellValue(gridViewPesada.FocusedRowHandle, "ID")
                    .ToString());
                var pesadaDetalle = Context.PesadaDetalle.Find(Id);

                if (pesadaDetalle == null)
                {
                    MessageBox.Show("No existe la pesada.",
                        "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Context.Entry(pesadaDetalle).State = EntityState.Deleted;
                    Context.SaveChanges();
                    CargarGrilla();
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun registro.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPesada(Guid PesadaId,bool finalizar)
        {
            var pesada = Context.Pesada.Find(PesadaId);
            if (pesada != null)
            {
                pesada.TotalFardo = Int32.Parse(txtTotalFardo.Text);
                pesada.TotalKg = float.Parse(txtTotalKilo.Text);
                pesada.ImporteBruto = decimal.Parse(txtImporteBruto.Text);
                pesada.PrecioPromedio = decimal.Parse(txtPrecioPromedio.Text);
                pesada.RomaneoPendiente = finalizar.Equals(true) ? false : true;
                Context.Entry(pesada).State = EntityState.Modified;
                Context.SaveChanges();

                #region preingreso - deshabilitado
                //var preingresoDetalle = Context.PreingresoDetalle
                //    .Where(x => x.PreingresoId == pesada.PreingresoId
                //        && x.ProductorId == ProductorId)
                //    .FirstOrDefault();
                //var preingreso = Context.PreingresoDetalle.Find(preingresoDetalle.Id);

                //if (preingreso != null)
                //{
                //    preingreso.Estado = false;

                //    Context.Entry(preingreso).State = EntityState.Modified;
                //    Context.SaveChanges();
                //}
                #endregion
            }
        }

        private void Deshabilitar()
        {
            ProductorId = Guid.Empty;
            txtFet.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPreingreso.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtProvincia.Text = string.Empty;
            txtClase.Text = string.Empty;
            txtKilos.Text = string.Empty;
            txtTotalKilo.Text = string.Empty;
            txtTotalFardo.Text = string.Empty;
            txtImporteBruto.Text = string.Empty;
            txtPrecioPromedio.Text = string.Empty;
            gridControlPesada.DataSource = new BindingList<RowPesada>();
            PasarMostrador(txtNombre.Text, txtCuit.Text);
            txtClase.Enabled = false;
            checkBalanzaAutomatica.Enabled = false;
            checkBalanzaAutomatica.Checked = true;
            txtKilos.Enabled = false;
            btnAgregarCaja.Enabled = false;
            btnReimprimir.Enabled = false;
            btnEliminar.Enabled = false;
            btnFinalizar.Enabled = false;
            btnIniciarPesada.Enabled = true;
            if (_timer != null)
            {
                _timer.Stop();
            }
        }
        
        private void PasarMostrador(string Productor, string Cuit)
        {
            _pesadaMostrador.nombre = Productor;
            _pesadaMostrador.cuit = Cuit;
            _pesadaMostrador.CargarDatos();
        }

        private void PasarFardoMostrador(bool limpiar)
        {
            if (limpiar.Equals(false))
            {
                _pesadaMostrador.numFardo = gridViewPesada.GetRowCellValue(0, "CONTADOR_CAJA").ToString();
                _pesadaMostrador.clase = gridViewPesada.GetRowCellValue(0, "CLASE").ToString();
                _pesadaMostrador.totalkg = txtTotalKilo.Text;
                _pesadaMostrador.porcentaje = CalcularPorcentaje();
                _pesadaMostrador.CargarFardo();
            }
            else
            {
                _pesadaMostrador.numFardo = string.Empty;
                _pesadaMostrador.clase = string.Empty;
                _pesadaMostrador.totalkg = string.Empty;
                _pesadaMostrador.porcentaje = string.Empty;
                _pesadaMostrador.CargarFardo();
               
            }
        }

        private string CalcularPorcentaje()
        {
            var clase = Context.Vw_Clase
                .Where(x => x.NOMBRE.Equals(DevConstantes.C1F))
                .FirstOrDefault();

            float promedio = Convert.ToSingle((decimal.Parse(txtPrecioPromedio.Text) * 100) / clase.PRECIOCOMPRA.Value);
            string porcentaje = promedio.ToString();
            return porcentaje;
        }

        private void ImprimirEtiqueta()
        {
            if (gridViewPesada.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewPesada.DataRowCount; i++)
                {
                    if (gridViewPesada.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewPesada
                            .GetRowCellValue(i, "ID")
                            .ToString());

                        var pesadaDetalle = Context.Vw_Pesada
                            .Where(x => x.PesadaDetalleId == Id)
                            .FirstOrDefault();

                        if (pesadaDetalle != null)
                        {
                            if (ValidarDebug().Equals(false))
                            {
                                if (ValidarImpresion().Equals(true))
                                {
                                    PrintTicket(pesadaDetalle.NumFardo.ToString(), pesadaDetalle.Clase,
                                        pesadaDetalle.Kilos.ToString());
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar registro.", 
                    "Atención", MessageBoxButtons.OK);
            }
        }

        private void ImpimirRomaneo(Guid PesadaId)
        {
            var pesada = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .FirstOrDefault();
            if (pesada.PesadaId != null)
            {
                var reporte = new RomaneoReport();
                reporte.Parameters["Productor"].Value = pesada.Productor;
                reporte.Parameters["Fet"].Value = pesada.Fet;
                reporte.Parameters["Localidad"].Value = pesada.Localidad;
                reporte.Parameters["Provincia"].Value = pesada.Provincia;
                reporte.Parameters["NumRomaneo"].Value = 
                    pesada.PuntoVentaRomaneo.ToString().PadLeft(4,'0') + " - " + pesada.NumRomaneo.ToString().PadLeft(8,'0');
                reporte.Parameters["Fecha"].Value = pesada.FechaRomaneo.Value
                    .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                reporte.Parameters["Reimpresion"].Value = string.Empty;

                #region Subreport Fardos

                List<RegistroFardo> datasourceFardo;
                datasourceFardo = GenerarReporteFardo(PesadaId);
                reporte.reportPesadaDetalle.ReportSource.DataSource = datasourceFardo;

                #endregion

                #region Subreport Clase

                List<RegistroPesada> datasourcePesada;
                datasourcePesada = GenerarReporteClase(PesadaId);
                reporte.reportDetalleClase.ReportSource.DataSource = datasourcePesada;

                #endregion

                #region Parametros Totales

                reporte.Parameters["totalfardo"].Value = totalfardo;
                reporte.Parameters["totalKilos"].Value = totalkilo;
                reporte.Parameters["ImporteBruto"].Value = importebruto;

                #endregion

                using (ReportPrintTool tool = new ReportPrintTool(reporte))
                {
                    reporte.ShowPreviewMarginLines = false;
                    tool.PreviewForm.Text = "Etiqueta";
                    if (ValidarDebug().Equals(false))
                    {
                        reporte.PrintingSystem.StartPrint += PrintingSystem_StartPrint;
                        reporte.Print();
                    }
                    else
                    {
                        tool.ShowPreviewDialog();
                    }
                }
            }
        }

        private void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Copies = 2;
        }

        public List<RegistroFardo> GenerarReporteFardo(Guid PesadaId)
        {
            List<RegistroFardo> datasource = new List<RegistroFardo>();

            var fardos = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .OrderBy(x=>x.NumFardo)
                .ToList();
            
            foreach (var fardo in fardos)
            {
                RegistroFardo registroFardos = new RegistroFardo();
                registroFardos.NumFardo = fardo.NumFardo.ToString();
                registroFardos.Clase = fardo.Clase;
                registroFardos.Kilos = fardo.Kilos.ToString();

                datasource.Add(registroFardos);
            }
            return datasource;
        }

        public List<RegistroPesada> GenerarReporteClase(Guid PesadaId)
        {
            List<RegistroPesada> datasource = new List<RegistroPesada>();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                conn.Open();

                string sql = @"SELECT * FROM dbo.Vw_ResumenPesadaPorClase where PesadaId='" + PesadaId + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    RegistroPesada registroFardos = new RegistroPesada();
                    registroFardos.Clase = row["Clase"].ToString();
                    registroFardos.Kilos = row["Kilos"].ToString();

                    datasource.Add(registroFardos);
                }

                return datasource;
            }
        }

        private void BuscarProductor()
        {
            var result =
                (from a in Context.Vw_Productor
                 select new
                 {
                     full = a.nrofet + a.NOMBRE + a.CUIT,
                     ID = a.ID,
                     FET = a.nrofet,
                     PRODUCTOR = a.NOMBRE,
                     CUIT = a.CUIT,
                     PROVINCIA = a.Provincia
                 });

            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var count = result
                    .Where(r => r.FET.Equals(txtFet.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtFet.Text;
                    _formBuscarProductor.target = DevConstantes.Pesada;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else if (count == 1)
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();

                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtNombre.Text = busqueda.PRODUCTOR.ToString();
                        txtCuit.Text = string.IsNullOrEmpty(busqueda.CUIT) ?
                            string.Empty : busqueda.CUIT;
                        txtProvincia.Text = string.IsNullOrEmpty(busqueda.PROVINCIA) ?
                            string.Empty : busqueda.PROVINCIA;
                        PasarMostrador(txtNombre.Text, txtCuit.Text);
                    }
                }
                else
                {
                    MessageBox.Show("N° de Fet no válido.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BuscarProductorPreingreso()
        {
            var result = (
                from a in Context.Vw_Preingreso
                select new
                {
                    ID = a.PreIngresoId,
                    FET = a.FET,
                    PRODUCTOR = a.Nombre,
                    CUIT = a.Cuit,
                    PROVINCIA = a.Provincia
                });

            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var count = result
                    .Where(r => r.FET.Equals(txtFet.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtFet.Text;
                    _formBuscarProductor.target = DevConstantes.Pesada;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else if (count == 1)
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();

                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID;
                        txtFet.Text = busqueda.FET.ToString();
                        txtNombre.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("N° de Fet no válido.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        void Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ?
                           string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ?
                string.Empty : empleado.Provincia;
            PasarMostrador(txtNombre.Text, txtCuit.Text);
        }

        private void Habilitar()
        {
            txtClase.Enabled = true;
            checkBalanzaAutomatica.Enabled = true;
            btnAgregarCaja.Enabled = true;
            gridControlPesada.Enabled = true;
            btnReimprimir.Enabled = true;
            btnEliminar.Enabled = true;
            btnFinalizar.Enabled = true;
        }

        private void CancelarPesada()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre == DevConstantes.PuntoVenta)
                .FirstOrDefault();

            var pesada = Context.Pesada
                .Where(x => x.NumPesada == contador.Valor
                    && x.ProductorId == ProductorId)
                .FirstOrDefault();

            if (pesada != null)
            {
                var romaneo = Context.Pesada.Find(pesada.Id);
                Context.Entry(romaneo).State = EntityState.Deleted;
                Context.SaveChanges();

                if (contador != null)
                {
                    var conta = Context.Contador.Find(contador.Id);
                    conta.Valor = conta.Valor - 1;
                    Context.Entry(conta).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }

            Deshabilitar();
        }

        private void SaveAndPrintKg(Guid Id)
        {
            if (txtKilos.Text != string.Empty && txtClase.Text != string.Empty && txtClase.Text != "0")
            {
                TipoTabacoId = Guid.Parse(cbTabaco.SelectedValue.ToString());
                GrabarPesadaDetalle(TipoTabacoId);
                CargarGrilla();

                var fardo = Context.PesadaDetalle
                    .Where(x => x.PesadaId == Id)
                    .OrderByDescending(x => x.NumFardo)
                    .FirstOrDefault();
                
                if (fardo != null)
                {
                    if (ValidarDebug().Equals(false))
                    {
                        if (ValidarImpresion().Equals(true))
                        {
                            PrintTicket(fardo.NumFardo.ToString(), txtClase.Text, txtKilos.Text);
                        }
                    }
                }

                CalcularTotales();
                ActualizarPesada(PesadaId,false);
                PasarFardoMostrador(false);
                txtKilos.Invoke((MethodInvoker)(() => txtKilos.Text = "0"));
                txtClase.Invoke((MethodInvoker)(() => txtClase.Text = string.Empty));
                txtClase.Invoke((MethodInvoker)(() => txtClase.Focus()));
            }
        }

        private void PrintTicket(string fardo, string clase, string lectura)
        {
            try
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
                s = s + "^XZ";

                if (printerTicket != null)
                {
                    PrintDialog pd = new PrintDialog();
                    pd.PrinterSettings = new PrinterSettings();
                    //pd.ShowDialog();
                    RawPrinterHelper.SendStringToPrinter(printerTicket, s);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en el módulo de impresión :", ex);
            }
        }

        private void CreateIfMissing(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }

        private bool ValidarDebug()
        {
           var debug = Context.Configuracion
             .Where(x => x.Nombre == DevConstantes.Debug)
             .FirstOrDefault();

            return debug.Valor;
        }

        private bool ValidarImpresion()
        {
            var balanza = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.ImpresiónBalanza)
              .FirstOrDefault();

            return balanza.Valor;
        }

        #endregion

        class RowPesada
        {
            public Guid ID { get; set; }
            public long NUMERO_FARDO { get; set; }
            public long CONTADOR_CAJA { get; set; }
            public string CLASE { get; set; }
            public double KILOS { get; set; }
            public decimal SUBTOTAL { get; set; }
        }

        private void txtClase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtKilos.Focus();
            }
        }

        private void txtKilos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtKilos.Text != string.Empty && txtClase.Text != string.Empty)
                {
                    SaveAndPrintKg(PesadaId);
                    //TipoTabacoId = Guid.Parse(cbTabaco.SelectedValue.ToString());
                    //GrabarPesadaDetalle(TipoTabacoId);
                    //CargarGrilla();
                    //CalcularTotales();
                    //ActualizarPesada(PesadaId, false);
                    //PasarFardoMostrador(false);
                    //txtKilos.Invoke((MethodInvoker)(() => txtKilos.Text = "0"));
                    //txtClase.Invoke((MethodInvoker)(() => txtClase.Text = string.Empty));
                }
            }
        }

        private void cbTabaco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnIniciarPesada.Focus();
            }
        }

        private void btnIniciarPesada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                GrabarPesada(continuar);
                _timer.Start();
                txtClase.Focus();
            }
        }
    }

    public class RegPesada
    {
        public TimeSpan Hora { get; set; }
        public decimal Valor { get; set; }

        public override string ToString()
        {
            return Hora.ToString("mm\\:ss") + " - " + Valor;
        }
    }
}