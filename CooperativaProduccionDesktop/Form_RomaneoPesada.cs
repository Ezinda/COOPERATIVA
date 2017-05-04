using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DesktopEntities.Models;
using CooperativaProduccion.Reports;
using System.Globalization;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.ReportModels;
using System.Data.SqlClient;
using DevExpress.Utils;
using System.IO.Ports;
using System.IO;
using System.Drawing.Printing;
using CooperativaProduccion.Helpers;
using System.Collections;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoPesada : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities _context { get; set; }
        private Guid _productorId;
        private Guid _pesadaId;
        private Guid TipoTabacoId;
        private string totalfardo;
        private string totalkilo;
        private string importebruto;
        private string printerTicket;
        private bool continuar;
        private Form_RomaneoPesadaMostrador _pesadaMostrador;
        private Form_AdministracionBuscarProductor _formBuscarProductor;

        #region balanza - lectura

        private static List<RegPesada> _bufferdecoincidencias = new List<RegPesada>();
        private static RegPesada _ultimoregistro;
        private static bool _printfrombuffer = false;
        private static bool _clearbuffer = false;
        private static bool _clearAndAddToBuffer = false;
        private const int _coincidenciasminimasparaimprimir = 7;
        private const int _coincidenciasmaximasparaimprimir = 30;
        private const int _limitecoincidenciaarriba = 0;
        private const int _limitecoincidenciaabajo = 0;
        private System.IO.Ports.SerialPort _serialport;

        private string lectura;
        private static TimeSpan _horaInicio;
        
        
        private static decimal _minimonuevaentrada = 10;
        private static decimal _bajadaynuevaentrada_porcentaje = 10;

        public System.Windows.Forms.TextBox _txtMostradorKilos;

        private bool _DEBUG = false;
        //private long _logcounter = 1;

        private List<ClaseRow> _coladeclases;

        #endregion

        public Form_RomaneoPesada(Guid? Id)
        {
            InitializeComponent();
            
            _context = new CooperativaProduccionEntities();
            _productorId = Guid.Empty;

            _coladeclases = new List<ClaseRow>();

            _serialport = new System.IO.Ports.SerialPort();
            _serialport.BaudRate = 9600;
            _serialport.Parity = Parity.None;
            _serialport.StopBits = StopBits.One;
            _serialport.DataBits = 8;
            _serialport.Handshake = Handshake.None;
            _serialport.RtsEnable = true;

            _horaInicio = DateTime.Now.TimeOfDay;

            if (IsDebug() == true)
            {
                _DEBUG = true;
            }
            else
            {
                _DEBUG = false;
            }

            CargarCombo();
            Iniciar(Id);

            if (checkBalanzaAutomatica.Checked)
            {
                ShowMostradorKilos();
            }
            else
            {
                HideMostradorKilos();
            }
        }

        private void _serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lectura = _serialport.ReadLine();

                byte[] bytesDeLectura = Encoding.ASCII.GetBytes(lectura);

                var bytesNeto = bytesDeLectura.Skip(2).Take(9).ToArray();

                var valorstr = Encoding.ASCII.GetString(bytesNeto);

                TimeSpan hora;
                decimal valor;
                decimal variacion;

                try
                {
                    valor = Convert.ToDecimal(valorstr);
                }
                catch
                {
                    return;
                }

                if (!this.IsDisposed && _txtMostradorKilos != null)
                {
                    textBox1.BeginInvoke((MethodInvoker)(() => textBox1.Text = valor.ToString()));
                    _txtMostradorKilos.BeginInvoke((MethodInvoker)(() => _txtMostradorKilos.Text = valor.ToString()));
                }

                hora = DateTime.Now.TimeOfDay;

                if (_ultimoregistro == null)
                {
                    variacion = -1000m;
                }
                else
                {
                    variacion = valor - _ultimoregistro.Valor;
                }

                var registro = new RegPesada()
                {
                    Hora = hora,
                    Valor = valor,
                    Variacion = variacion
                };

                _ultimoregistro = registro;

                if (valor == 0)
                {
                    if (_bufferdecoincidencias.Count >= _coincidenciasminimasparaimprimir &&
                        _bufferdecoincidencias.Count <= _coincidenciasmaximasparaimprimir)
                    {
                        _printfrombuffer = true;
                    }

                    variacion = -1000m;

                    _clearbuffer = true;
                }
                else if (variacion == -1000m)
                {
                    _clearAndAddToBuffer = true;
                }
                else if (_limitecoincidenciaabajo <= variacion && variacion <= _limitecoincidenciaarriba)
                {
                    _bufferdecoincidencias.Add(registro);

                    if (_bufferdecoincidencias.Count == _coincidenciasmaximasparaimprimir)
                    {
                        _printfrombuffer = true;
                    }
                }
                else
                {
                    if (_bufferdecoincidencias.Count >= _coincidenciasminimasparaimprimir &&
                        _bufferdecoincidencias.Count <= _coincidenciasmaximasparaimprimir)
                    {
                        _printfrombuffer = true;

                        variacion = -1000m;
                    }

                    _clearAndAddToBuffer = true;
                }

                if (_bufferdecoincidencias.Count != 0 && _clearAndAddToBuffer == false && valor != 0)
                {
                    try
                    {
                        LogReceived(_pesadaId,
                            valor + ";" +
                                variacion + ";" +
                                _bufferdecoincidencias.Count.ToString("000"));
                    }
                    catch
                    {
                    }
                }

                if (_printfrombuffer)
                {
                    decimal maximoValor = 0;
                    RegPesada maximoRegistro = null;

                    foreach (var item in _bufferdecoincidencias)
                    {
                        if (maximoValor < item.Valor)
                        {
                            maximoValor = item.Valor;
                            maximoRegistro = item;
                        }
                    }

                    try
                    {
                        LogReceived(_pesadaId,
                            "0000" + ";" +
                                maximoValor + ";" +
                                _bufferdecoincidencias.Count.ToString("000"));
                    }
                    catch
                    {
                    }

                    _ultimoregistro = null;
                    _bufferdecoincidencias.Clear();

                    txtKilos.BeginInvoke((MethodInvoker)(() => txtKilos.Text = maximoValor.ToString()));
                    SaveAndPrintKg(_pesadaId);

                    _bufferdecoincidencias.Clear();

                    _printfrombuffer = false;
                }

                if (_clearbuffer || _clearAndAddToBuffer)
                {
                    _bufferdecoincidencias.Clear();

                    if (_clearbuffer && valor == 0)
                    {
                        try
                        {
                            LogReceived(_pesadaId,
                                valor + ";" +
                                    variacion + ";" +
                                    _bufferdecoincidencias.Count.ToString("000"));
                        }
                        catch
                        {
                        }
                    }

                    _clearbuffer = false;

                    if (_clearAndAddToBuffer)
                    {
                        _bufferdecoincidencias.Add(registro);

                        try
                        {
                            LogReceived(_pesadaId,
                                valor + ";" +
                                    variacion + ";" +
                                    _bufferdecoincidencias.Count.ToString("000"));
                        }
                        catch
                        {
                        }

                        _ultimoregistro = registro;

                        _clearAndAddToBuffer = false;
                    }
                    else
                    {
                        _ultimoregistro = null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("DataReceived -> " + ex);
                return;
            }
        }

        #region Method Code

        private void btnPesadaMostrador_ItemClick(object sender, ItemClickEventArgs e)
        {
            _pesadaMostrador = new Form_RomaneoPesadaMostrador();

            _pesadaMostrador.nombre = txtNombre.Text;
            _pesadaMostrador.cuit = txtCuit.Text;
            _pesadaMostrador.CargarDatos();

            if (Screen.AllScreens.Count() > 1)
            {
                _pesadaMostrador.Location = Screen.AllScreens[1].WorkingArea.Location;
            }

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
                PasarMostrador(string.Empty, string.Empty);
            }
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnIniciarPesada_Click(object sender, EventArgs e)
        {
            var isbalanzaautomatica = checkBalanzaAutomatica.Checked;
            var productorId = _productorId;
            var tipotabacoId = cbTabaco.SelectedValue != null ? (Guid)cbTabaco.SelectedValue : Guid.Empty;

            SetAutoFocusClase();

            if (productorId == Guid.Empty)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (tipotabacoId == Guid.Empty)
            {
                MessageBox.Show("No se ha seleccionado un tipo de tabaco",
                    "Se requiere",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (ValidarCamposPesada())
            {
                if (continuar == false)
                {
                    var resultado = MessageBox.Show("¿Desea iniciar una nueva pesada?",
                        "Crear Preingreso",
                        MessageBoxButtons.OKCancel);

                    if (resultado == DialogResult.Cancel)
                    {
                        return;
                    }

                    GuardarDatosPesada();
                    CalcularTotales();
                }

                Habilitar();
                cbTabaco.Enabled = false;
                if (!_DEBUG && isbalanzaautomatica)
                {
                    CreateLogDirectory();
                    ClearLogsRomaneos();

                    _bufferdecoincidencias = new List<RegPesada>();

                    if (!_serialport.IsOpen)
                    {
                        var maximosintentos = 5;
                        var contador = 1;

                        while (!_serialport.IsOpen && contador <= maximosintentos)
                        {
                            if (contador != 1)
                            {
                                _serialport.DataReceived -= _serialport_DataReceived;
                                System.Threading.Thread.Sleep(100);
                            }

                            _serialport.DataReceived += _serialport_DataReceived;
                            _serialport.Open();

                            contador++;
                        }

                        if (!_serialport.IsOpen)
                        {
                            checkBalanzaAutomatica.Checked = false;

                            MessageBox.Show("No se ha podido conectar a la balanza.",
                                "Sin conexión",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }

                this.btnIniciarPesada.Enabled = false;
            }

            AutoFocusClase();
        }

        private bool _autofocusclase = false;

        private void SetAutoFocusClase()
        {
            _autofocusclase = true;
        }

        private void UnsetAutoFocusClase()
        {
            _autofocusclase = false;
        }

        private void AutoFocusClase()
        {
            if (_autofocusclase)
            {
                txtClase.Focus();
            }
        }

        private void btnCancelarPesada_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea cancelar la pesada?",
                "Confirmación",
                MessageBoxButtons.OKCancel);

            if (resultado == DialogResult.Cancel)
            {
                return;
            }

            if (!_DEBUG)
            {
                try
                {
                    if (_serialport.IsOpen)
                    {
                        _serialport.DataReceived -= _serialport_DataReceived;
                        _serialport.Close();
                    }
                }
                catch
                {
                }
            }

            CancelarPesada();

            UnsetAutoFocusClase();
        }
   
        private void checkBalanzaAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBalanzaAutomatica.Checked)
            {
                var iniciado = btnIniciarPesada.Enabled;

                if (!_DEBUG && iniciado)
                {
                    _bufferdecoincidencias = new List<RegPesada>();

                    if (!_serialport.IsOpen)
                    {
                        var maximosintentos = 5;
                        var contador = 1;

                        while (!_serialport.IsOpen && contador <= maximosintentos)
                        {
                            if (contador != 1)
                            {
                                _serialport.DataReceived -= _serialport_DataReceived;
                                System.Threading.Thread.Sleep(100);
                            }
                           
                            _serialport.DataReceived += _serialport_DataReceived;
                            _serialport.Open();

                            contador++;
                        }

                        if (!_serialport.IsOpen)
                        {
                            checkBalanzaAutomatica.Checked = false;

                            MessageBox.Show("No se ha podido conectar a la balanza.",
                                "Sin conexión",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }

                txtKilos.Enabled = false;
                txtKilos.Text = String.Empty;

                ShowMostradorKilos();
            }
            else
            {
                if (!_DEBUG)
                {
                    try
                    {
                        if (_serialport.IsOpen)
                        {
                            //_serialport.DataReceived -= _serialport_DataReceived;
                            //_serialport.Close();
                        }
                    }
                    catch
                    {
                    }
                }

                txtKilos.Enabled = true;
                txtKilos.Text = String.Empty;

                HideMostradorKilos();
            }

            txtClase.Focus();
        }

        private void ShowMostradorKilos()
        {
            if (_txtMostradorKilos == null)
            {
                this._txtMostradorKilos = new System.Windows.Forms.TextBox();

                ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
                this.groupControl3.SuspendLayout();

                this.groupControl3.Controls.Add(this._txtMostradorKilos);

                this._txtMostradorKilos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this._txtMostradorKilos.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this._txtMostradorKilos.Name = "txtMostradorKilos";

                this._txtMostradorKilos.Enabled = false;
                
                this._txtMostradorKilos.Location = new System.Drawing.Point(421, 6);
                this._txtMostradorKilos.Size = new System.Drawing.Size(92, 22);

                ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
                this.groupControl3.ResumeLayout(false);
                this.groupControl3.PerformLayout();

                // Get the controls index
                int zIndex = groupControl3.Controls.GetChildIndex(_txtMostradorKilos);
                // Bring it to the front
                _txtMostradorKilos.BringToFront();
                // Do something...
                // Then send it back again
                //parentControl.Controls.SetChildIndex(textBox, zIndex);
            }

            this._txtMostradorKilos.Visible = true;
        }

        private void HideMostradorKilos()
        {
            if (_txtMostradorKilos == null)
            {
                return;
            }

            this._txtMostradorKilos.Location = new System.Drawing.Point(421, 6);
            this._txtMostradorKilos.Size = new System.Drawing.Size(92, 22);

            this._txtMostradorKilos.Visible = false;
        }

        private void btnAgregarCaja_Click(object sender, EventArgs e)
        {
            if (txtKilos.Text != string.Empty && txtClase.Text != string.Empty)
            {
                SaveAndPrintKg(_pesadaId);
            }
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            ImprimirEtiqueta();

            AutoFocusClase();
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
            PasarFardoMostrador(true,string.Empty);

            AutoFocusClase();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            totalfardo = txtTotalFardo.Text;
            totalkilo = txtTotalKilo.Text;
            importebruto = txtImporteBruto.Text;
            ActualizarPesada(_pesadaId,true);
            var pesadavieja = _pesadaId;
            _pesadaId = Guid.NewGuid();
            continuar = false;
            Deshabilitar();
            ImpimirRomaneo(pesadavieja);
            PasarMostrador(string.Empty, string.Empty);
            PasarFardoMostrador(true,string.Empty);
            //m_serialPort1.Close();//Cerramos puerto
            //m_serialPort1.Dispose();//Liberamos recursos

            if (!_DEBUG)
            {
                try
                {
                    if (_serialport.IsOpen)
                    {
                        _serialport.DataReceived -= _serialport_DataReceived;
                        _serialport.Close();
                    }
                }
                catch
                {
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (!_DEBUG)
            {
                try
                {
                    _serialport.DataReceived -= _serialport_DataReceived;
                    _serialport.Close();
                }
                catch
                {
                }
            }

            if (_pesadaMostrador != null && !_pesadaMostrador.IsDisposed)
            {
                _pesadaMostrador.Close();
            }

            if (gridViewPesada.DataRowCount < 1)
            {
                CancelarPesada();
            }

            this.Close();
        }

        private void CreateLogDirectory()
        {
            var rootdir = DevConstantes.RootDocumentsDirectory;
            CreateIfMissing(rootdir);

            var logdir = Path.Combine(rootdir, DevConstantes.LogsDirectory);
            CreateIfMissing(logdir);
        }

        private void ClearLogsRomaneos()
        {
            var logdir = Path.Combine(DevConstantes.RootDocumentsDirectory, DevConstantes.LogsDirectory);
            var romaneoFiles = DevConstantes.LogsRomaneoFile + "*";
            var today = DateTime.Now.Date;
            string[] fileList = Directory.GetFiles(logdir, romaneoFiles);
            
            foreach (var file in fileList)
            {
                if (File.GetLastWriteTime(file).Date != today)
                {
                    File.Delete(file);
                }
            }
        }

        private static void CreateIfMissing(string path)
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

        //private void Log(string v)
        //{
        //    File.AppendAllText("Log.txt", v + Environment.NewLine);
        //}

        //private void Log(string v)
        //{
        //    File.AppendAllText("Log.txt", v + ";" + _logcounter.ToString("000000") + Environment.NewLine);
        //    _logcounter++;
        //}

        private static void LogReceived(Guid pesadaId, string line)
        {
            var filename = Path.Combine(DevConstantes.RootDocumentsDirectory,
                DevConstantes.LogsDirectory,
                DevConstantes.LogsRomaneoFile + "-" + pesadaId + ".txt");
            
            File.AppendAllText(filename,
                line + Environment.NewLine);
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Method Dev
        
        private void CargarCombo()
        {
            var tipotabaco = _context.Vw_TipoTabaco
                .Where(x => x.RUBRO_ID != null)
                .ToList();

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";
        }

        private void Iniciar(Guid? Id)
        {
            _pesadaMostrador = new Form_RomaneoPesadaMostrador();

            if (Screen.AllScreens.Count() > 1)
            {
                _pesadaMostrador.Location = Screen.AllScreens[1].WorkingArea.Location;
            }

            _pesadaMostrador.Show();
            
            Deshabilitar();
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            if (!_DEBUG)
            {
                string strFileConfig = @"Config.ini";
                IniParser parser = new IniParser(strFileConfig);
                printerTicket = parser.GetSetting("AppSettings", "PrinterTicketBalanza");
            }

            if (Id != null)
            {
                cbTabaco.Enabled = false;
                continuar = true;
                _pesadaId = Id.Value;
                var pesada = Context.Pesada
                    .Where(x => x.Id == Id)
                    .FirstOrDefault();

                if (pesada != null)
                {
                    _productorId = pesada.ProductorId.Value;
                    var productor = Context.Vw_Productor
                        .Where(x => x.ID == pesada.ProductorId)
                        .FirstOrDefault();

                    if (productor != null)
                    {
                        txtFet.Text = productor.nrofet;
                        txtCuit.Text = productor.CUIT;
                        txtNombre.Text = productor.NOMBRE;
                        txtProvincia.Text = productor.Provincia;
                        cbTabaco.SelectedValue = pesada.TipoTabacoId;
                        txtTotalFardo.Text = pesada.TotalFardo.Value.ToString();
                        txtTotalKilo.Text = pesada.TotalKg.Value.ToString();
                        txtImporteBruto.Text = pesada.ImporteBruto.Value.ToString();
                        txtPrecioPromedio.Text = pesada.PrecioPromedio.Value.ToString();

                        PasarMostrador(productor.NOMBRE, productor.CUIT);

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

                    var tabaco = _context.Vw_TipoTabaco
                        .Where(x => x.id == pesada.TipoTabacoId)
                        .FirstOrDefault();

                    PasarFardoMostrador(false,tabaco.DESCRIPCION);
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
            txtTotalFardo.Invoke((MethodInvoker)(() => txtTotalFardo.Text = CalcularTotalFardo(_pesadaId).ToString()));
            txtTotalKilo.Invoke((MethodInvoker)(() => txtTotalKilo.Text = CalcularTotalKilos(_pesadaId).ToString()));
            txtImporteBruto.Invoke((MethodInvoker)(() => txtImporteBruto.Text = CalcularTotalImporteBruto(_pesadaId).ToString()));
            txtPrecioPromedio.Invoke((MethodInvoker)(() => txtPrecioPromedio.Text = CalcularPrecioPromedio(_pesadaId).ToString()));
        }

        private void Buscar()
        {
            var result = (
              from a in _context.Vw_Productor
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
                else
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        _productorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET;
                        txtNombre.Text = busqueda.PRODUCTOR;
                        txtProvincia.Text = busqueda.PROVINCIA;
                        txtCuit.Text = busqueda.CUIT;
                        PasarMostrador(txtNombre.Text, txtCuit.Text);
                    }
                    else
                    {
                        MessageBox.Show("N° de Fet no válido.",
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
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.nombre = txtNombre.Text;
                    _formBuscarProductor.target = DevConstantes.Pesada;
                    _formBuscarProductor.BuscarNombre();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtNombre.Text))
                      .FirstOrDefault();

                    if (busqueda != null)
                    {
                        _productorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET;
                        txtNombre.Text = busqueda.PRODUCTOR;
                        txtProvincia.Text = busqueda.PROVINCIA;
                        txtCuit.Text = busqueda.CUIT;
                        PasarMostrador(txtNombre.Text, txtCuit.Text);
                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
  
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            _productorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = _context.Vw_Productor
                .Where(x => x.ID == _productorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ?
                           string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ?
                string.Empty : empleado.Provincia;
            PasarMostrador(txtNombre.Text, txtCuit.Text);
        }

        void Enviar(Guid Id, string fet, string nombre)
        {
            _productorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = _context.Vw_Productor
                .Where(x => x.ID == _productorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ?
                           string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ?
                string.Empty : empleado.Provincia;
            PasarMostrador(txtNombre.Text, txtCuit.Text);
        }
     
        private decimal CalcularTotalImporteBruto(Guid PesadaId)
        {
            decimal monto = 0;
            var pesadas = _context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId);

            foreach (var pesada in pesadas)
            {
                monto = monto + pesada.Subtotal.Value;
            }

            return monto;
        }

        private int CalcularTotalFardo(Guid PesadaId)
        {
            int totalFardo = 0;
            var count = _context.PesadaDetalle
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
            var pesadas = _context.Vw_Pesada
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
            var pesadas = _context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId);
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    precioPromedio = precioPromedio + Convert.ToSingle(pesada.ClasePrecio.Value.ToString());
                    var count = _context.PesadaDetalle
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

        private void GrabarPesadaDetalle(Guid pesadaId, Guid tipoTabacoId, string clase, double kilos)
        {
            try
            {
                var kilosround = Math.Round(kilos, 0);

                var detalleid = RegistrarDetallePesada(pesadaId, tipoTabacoId, clase, kilosround);

                var now = DateTime.Now.Date;

                if (!detalleid.Equals(Guid.Empty))
                {
                    RegistrarMovimiento(detalleid, kilosround, now);
                }
            }
            catch
            {
                throw;
            }
        }

        private Guid RegistrarMovimiento(Guid detalleid, double kilos, DateTime fecha)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = fecha;
            movimiento.TransaccionId = detalleid;
            movimiento.Unidad = DevConstantes.Kg;
            movimiento.Ingreso = kilos;
            movimiento.Egreso = 0;
            movimiento.Documento = DevConstantes.Romaneo;

            var deposito = _context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.MateriaPrima)
                .FirstOrDefault();

            if (deposito != null)
            {
                movimiento.DepositoId = deposito.id;
            }

            _context.Movimiento.Add(movimiento);
            _context.SaveChanges();

            return movimiento.Id;
        }

        private long ContadorNumeroPesada()
        {
            var contador = _context.Contador
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
            var contador = _context.Contador
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
            if (_productorId == null)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private int NumeroPuntoVentaRomaneo()
        {
            var contador = _context.Contador
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
                _pesadaId = pesada.Id;
                pesada.NumPesada = ContadorNumeroPesada();
                #region Preingreso - Deshabilitado
                //int numPreingreso = Int32.Parse(txtPreingreso.Text);
                //var preingreso = Context.Preingreso
                //    .Where(x => x.NumeroPreingreso == numPreingreso)
                //    .FirstOrDefault();
                //pesada.PreingresoId = preingreso.Id;
#endregion
                pesada.ProductorId = _productorId;
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
                _context.Pesada.Add(pesada);
                _context.SaveChanges();

                #region Actualizar contador

                var contador = _context.Contador
                    .Where(x=>x.Nombre.Equals(DevConstantes.Pesada))
                    .FirstOrDefault();
                var count = _context.Contador.Find(contador.Id);
                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    _context.Entry(count).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                contador = _context.Contador
                    .Where(x => x.Nombre.Equals(DevConstantes.Romaneo))
                    .FirstOrDefault();
                count = _context.Contador.Find(contador.Id);
                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    _context.Entry(count).State = EntityState.Modified;
                    _context.SaveChanges();
                }

#endregion
            }
            catch
            {
                throw;
            }
        }

        private Guid RegistrarDetallePesada(Guid pesadaId, Guid tipotabacoid, string clase, double kilos)
        {
            var vwclase = _context.Vw_Clase
                .Where(x => x.Vigente == true
                    && x.NOMBRE == clase
                    && x.ID_PRODUCTO == tipotabacoid)
                .Select(x => new
                {
                    x.ID,
                    x.PRECIOCOMPRA
                })
                .Single();

            long numFardo = NumeradorFardo();

            CooperativaProduccionEntities context = new CooperativaProduccionEntities();

            var existeFardo = context.PesadaDetalle
                .Where(x => x.PesadaId == pesadaId
                    && x.NumFardo == numFardo)
                .Any();

            if (existeFardo.Equals(false))
            {
                PesadaDetalle pesadaDetalle;

                pesadaDetalle = new PesadaDetalle();
                pesadaDetalle.Id = Guid.NewGuid();
                pesadaDetalle.PesadaId = _pesadaId;
                pesadaDetalle.ContadorFardo = ContadorNumeroFardo(pesadaId);
                pesadaDetalle.NumFardo = NumeradorFardo();
                pesadaDetalle.ClaseId = vwclase.ID;
                pesadaDetalle.Kilos = kilos;
                pesadaDetalle.ClasePrecio = vwclase.PRECIOCOMPRA;

                context.PesadaDetalle.Add(pesadaDetalle);
                context.SaveChanges();

                return pesadaDetalle.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        private long ContadorNumeroFardo(Guid PesadaId)
        {
            long numFardo = 0;
            var pesadaDetalle = _context.PesadaDetalle
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
            CooperativaProduccionEntities _context = new CooperativaProduccionEntities();

            long numFardo = 0;

            var pesadaDetalle = _context.PesadaDetalle
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
                    .Where(x => x.PesadaId == _pesadaId)
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

                var pesadaDetalle = _context.PesadaDetalle.Find(Id);

                if (pesadaDetalle == null)
                {
                    MessageBox.Show("No existe la pesada.",
                        "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var mov = _context.Movimiento
                        .Where(x => x.TransaccionId == pesadaDetalle.Id)
                        .FirstOrDefault();
                    
                    if (mov != null)
                    {
                        var movimiento = _context.Movimiento.Find(mov.Id);
                        _context.Entry(movimiento).State = EntityState.Deleted;
                        _context.SaveChanges();
                    }
                    
                    _context.Entry(pesadaDetalle).State = EntityState.Deleted;
                    _context.SaveChanges();
                    CargarGrilla();
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun registro.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPesada(Guid pesadaId, bool finalizar)
        {
            var pesada = _context.Pesada.Find(pesadaId);

            if (pesada != null)
            {
                pesada.TotalFardo = Int32.Parse(txtTotalFardo.Text);
                pesada.TotalKg = float.Parse(txtTotalKilo.Text);
                pesada.ImporteBruto = CalcularTotalImporteBruto(pesadaId); //decimal.Parse(txtImporteBruto.Text);
                pesada.PrecioPromedio = decimal.Parse(txtPrecioPromedio.Text);
                pesada.RomaneoPendiente = finalizar.Equals(true) ? false : true;
                _context.Entry(pesada).State = EntityState.Modified;
                _context.SaveChanges();

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
            _productorId = Guid.Empty;
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
        }
        
        private void PasarMostrador(string Productor, string Cuit)
        {
            _pesadaMostrador.nombre = Productor;
            _pesadaMostrador.cuit = Cuit;
            _pesadaMostrador.CargarDatos();
        }

        private void PasarFardoMostrador(bool limpiar,string Tabaco)
        {
            if (limpiar.Equals(false))
            {
                _pesadaMostrador.numFardo = gridViewPesada.GetRowCellValue(0, "CONTADOR_CAJA").ToString();
                _pesadaMostrador.clase = gridViewPesada.GetRowCellValue(0, "CLASE").ToString();
                _pesadaMostrador.totalkg = txtTotalKilo.Text;
                _pesadaMostrador.porcentaje = CalcularPorcentaje(Tabaco);
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

        private string CalcularPorcentaje(string Tabaco)
        {
            var clase = _context.Vw_Clase
                .Where(x => x.DESCRIPCION.Equals(Tabaco) 
                    && x.NOMBRE.Equals(DevConstantes.C1F))
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

                        var pesadaDetalle = _context.Vw_Pesada
                            .Where(x => x.PesadaDetalleId == Id)
                            .FirstOrDefault();

                        if (pesadaDetalle != null)
                        {
                            if (!_DEBUG)
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
            var pesada = _context.Vw_Pesada
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
                    if (!_DEBUG)
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

            var fardos = _context.Vw_Pesada
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
                (from a in _context.Vw_Productor
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
                        _productorId = busqueda.ID.Value;
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
                from a in _context.Vw_Preingreso
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
                        _productorId = busqueda.ID;
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
            var contadorRomaneo = _context.Contador
                .Where(x => x.Nombre == DevConstantes.Romaneo)
                .FirstOrDefault();
            
            var pesada = _context.Pesada
                .Where(x => x.NumPesada == contadorRomaneo.Valor
                    && x.ProductorId == _productorId)
                .FirstOrDefault();

            if (pesada != null)
            {
                var romaneo = _context.Pesada.Find(pesada.Id);
                _context.Entry(romaneo).State = EntityState.Deleted;
                _context.SaveChanges();

                if (contadorRomaneo != null)
                {
                    var contaRomaneo = _context.Contador.Find(contadorRomaneo.Id);
                    contaRomaneo.Valor = contaRomaneo.Valor - 1;
                    _context.Entry(contaRomaneo).State = EntityState.Modified;
                    _context.SaveChanges();

                    var contadorPesada = _context.Contador
                        .Where(x => x.Nombre == DevConstantes.Pesada)
                        .FirstOrDefault();

                    var contaPesada = _context.Contador.Find(contadorPesada.Id);
                    contaPesada.Valor = contaPesada.Valor - 1;
                    _context.Entry(contaPesada).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            Deshabilitar();
        }

        private void SaveAndPrintKg(Guid pesadaId)
        {
            var kilos = txtKilos.Text;
            string clase = String.Empty;

            if (checkBalanzaAutomatica.Checked)
            {
                clase = PopClaseColaDeClases();
            }
            else
            {
                clase = txtClase.Text.Trim();
            }

            if (kilos != String.Empty && kilos != "0" && clase != String.Empty && clase != "0")
            {
                var tipotabacoId = (Guid)cbTabaco.SelectedValue;
                var kilosnum = Convert.ToDouble(txtKilos.Text);

                TipoTabacoId = tipotabacoId;

                GrabarPesadaDetalle(pesadaId, tipotabacoId, clase, kilosnum);
                CargarGrilla();

                var fardo = _context.PesadaDetalle
                    .Where(x => x.PesadaId == pesadaId)
                    .OrderByDescending(x => x.NumFardo)
                    .FirstOrDefault();
                
                if (fardo != null)
                {
                    if (!_DEBUG)
                    {
                        if (ValidarImpresion().Equals(true))
                        {
                            PrintTicket(fardo.NumFardo.ToString(), clase, kilos);
                        }
                    }
                }

                CalcularTotales();
                ActualizarPesada(pesadaId, false);

                var tabaco = _context.Vw_TipoTabaco
                         .Where(x => x.id == TipoTabacoId)
                         .FirstOrDefault();

                PasarFardoMostrador(false,tabaco.DESCRIPCION);

                txtKilos.Invoke((MethodInvoker)(() => txtKilos.Text = "0"));
                txtClase.Invoke((MethodInvoker)(() => txtClase.Text = String.Empty));
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

        private bool IsDebug()
        {
           var debug = _context.Configuracion
             .Where(x => x.Nombre == DevConstantes.Debug)
             .FirstOrDefault();

            return debug.Valor;
        }

        private bool ValidarImpresion()
        {
            var balanza = _context.Configuracion
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
            if (e.KeyChar == (int)Keys.Enter)
            {
                if (checkBalanzaAutomatica.Checked)
                {
                    var clase = txtClase.Text.Trim();
                    var timestamp = DateTime.Now.Ticks;

                    PushClaseColaDeClases(clase, timestamp);

                    txtClase.Text = String.Empty;
                }
                else
                {
                    txtKilos.Focus();
                }
            }
        }

        private void PushClaseColaDeClases(string clase, long timestamp)
        {
            var registro = new ClaseRow()
            {
                Trick = timestamp,
                Clase = clase
            };

            _coladeclases.Add(registro);
            var coladeclases = _coladeclases.OrderBy(x => x.Trick).ToList();

            ActualizarGrillaDeClases(coladeclases);

            _coladeclases = coladeclases;
        }

        private string PopClaseColaDeClases()
        {
            var registro = _coladeclases.OrderBy(x => x.Trick).FirstOrDefault();

            if (registro == null)
            {
                return String.Empty;
            }

            var clase = registro.Clase;

            _coladeclases.Remove(registro);
            var coladeclases = _coladeclases.OrderBy(x => x.Trick).ToList();

            ActualizarGrillaDeClases(coladeclases);

            _coladeclases = coladeclases;

            return clase;
        }

        private void QuitarClaseColaDeClases(long trick)
        {
            var registro = _coladeclases.Where(x => x.Trick == trick).Single();

            var coladeclases = _coladeclases.ToList();

            coladeclases.Remove(registro);

            coladeclases = coladeclases.OrderBy(x => x.Trick).ToList();

            ActualizarGrillaDeClases(coladeclases);

            _coladeclases = coladeclases;
        }

        private void ActualizarGrillaDeClases(List<ClaseRow> coladeclases)
        {
            //gridControlClases.DataSource = coladeclases;
            gridControlClases.Invoke((MethodInvoker)(() => gridControlClases.DataSource = coladeclases));

            gridViewClases.Columns["Trick"].Visible = false;
            gridViewClases.Columns["Clase"].Caption = "En Cola";
            gridViewClases.Columns["Clase"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewClases.Columns["Clase"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void txtKilos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtKilos.Text != string.Empty && txtClase.Text != string.Empty)
                {
                    SaveAndPrintKg(_pesadaId);
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

        private void gridViewClases_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button.IsRight() && e.RowHandle >= 0)
            {
                var registro = gridViewClases.GetRow(e.RowHandle) as ClaseRow;

                QuitarClaseColaDeClases(registro.Trick);
            }

            AutoFocusClase();
        }

        private void gridViewClases_KeyUp(object sender, KeyEventArgs e)
        {
            var rowhandle = gridViewClases.FocusedRowHandle;

            if (e.KeyData == Keys.Delete && rowhandle >= 0)
            {
                var registro = gridViewClases.GetRow(rowhandle) as ClaseRow;

                QuitarClaseColaDeClases(registro.Trick);
            }

            AutoFocusClase();
        }

        private void btnBuscarProductorNombre_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
                cbTabaco.Focus();
            }

            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtPreingreso.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                PasarMostrador(string.Empty, string.Empty);
            }
        }
    }

    public class RegPesada
    {
        public TimeSpan Hora { get; set; }
        public decimal Valor { get; set; }
        public decimal Variacion { get; set; }

        public override string ToString()
        {
            return Hora.ToString("mm\\:ss") + " - " + Valor + " ~ " + Variacion;
        }
    }

    public class ClaseRow
    {
        public long Trick { get; set; }
        public String Clase { get; set; }
    }
}