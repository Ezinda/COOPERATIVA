namespace CooperativaProduccion
{
    partial class Form_RomaneoPesada
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_RomaneoPesada));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtProvincia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscarProductor = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancelarPesada = new DevExpress.XtraEditors.SimpleButton();
            this.btnIniciarPesada = new DevExpress.XtraEditors.SimpleButton();
            this.cbOpcionCompra = new System.Windows.Forms.ComboBox();
            this.cbBoca = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnReimprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.dgvPesada = new System.Windows.Forms.DataGridView();
            this.txtKilos = new System.Windows.Forms.TextBox();
            this.checkBalanzaAutomatica = new System.Windows.Forms.CheckBox();
            this.btnAgregarCaja = new DevExpress.XtraEditors.SimpleButton();
            this.cbClase = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.txtPrecioPromedio = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtImporteBruto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalKilo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotalFardo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barButtonItem1});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowCategoryInCaption = false;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(711, 144);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Visualizar Pesadas";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Principal";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Opciones";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtProvincia);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.txtCuit);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.txtNombre);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.txtFet);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.btnBuscarProductor);
            this.groupControl2.Location = new System.Drawing.Point(4, 199);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(705, 90);
            this.groupControl2.TabIndex = 21;
            this.groupControl2.Text = "Buscar Productor";
            // 
            // txtProvincia
            // 
            this.txtProvincia.Enabled = false;
            this.txtProvincia.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProvincia.Location = new System.Drawing.Point(550, 57);
            this.txtProvincia.Name = "txtProvincia";
            this.txtProvincia.Size = new System.Drawing.Size(151, 26);
            this.txtProvincia.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(479, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 62;
            this.label1.Text = "Provincia:";
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuit.Location = new System.Drawing.Point(322, 57);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(151, 26);
            this.txtCuit.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(265, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 59;
            this.label4.Text = "C.U.I.T:";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(322, 25);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(379, 26);
            this.txtNombre.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(241, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "Productor:";
            // 
            // txtFet
            // 
            this.txtFet.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFet.Location = new System.Drawing.Point(63, 24);
            this.txtFet.Name = "txtFet";
            this.txtFet.Size = new System.Drawing.Size(145, 26);
            this.txtFet.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 55;
            this.label3.Text = "N° FET:";
            // 
            // btnBuscarProductor
            // 
            this.btnBuscarProductor.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProductor.Image")));
            this.btnBuscarProductor.Location = new System.Drawing.Point(210, 24);
            this.btnBuscarProductor.Name = "btnBuscarProductor";
            this.btnBuscarProductor.Size = new System.Drawing.Size(25, 26);
            this.btnBuscarProductor.TabIndex = 39;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCancelarPesada);
            this.groupControl1.Controls.Add(this.btnIniciarPesada);
            this.groupControl1.Controls.Add(this.cbOpcionCompra);
            this.groupControl1.Controls.Add(this.cbBoca);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Location = new System.Drawing.Point(4, 292);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(705, 89);
            this.groupControl1.TabIndex = 22;
            this.groupControl1.Text = "Parámetros";
            // 
            // btnCancelarPesada
            // 
            this.btnCancelarPesada.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarPesada.Image")));
            this.btnCancelarPesada.Location = new System.Drawing.Point(144, 58);
            this.btnCancelarPesada.Name = "btnCancelarPesada";
            this.btnCancelarPesada.Size = new System.Drawing.Size(126, 26);
            this.btnCancelarPesada.TabIndex = 61;
            this.btnCancelarPesada.Text = "Cancelar";
            // 
            // btnIniciarPesada
            // 
            this.btnIniciarPesada.Image = ((System.Drawing.Image)(resources.GetObject("btnIniciarPesada.Image")));
            this.btnIniciarPesada.Location = new System.Drawing.Point(12, 58);
            this.btnIniciarPesada.Name = "btnIniciarPesada";
            this.btnIniciarPesada.Size = new System.Drawing.Size(126, 26);
            this.btnIniciarPesada.TabIndex = 60;
            this.btnIniciarPesada.Text = "Iniciar";
            // 
            // cbOpcionCompra
            // 
            this.cbOpcionCompra.FormattingEnabled = true;
            this.cbOpcionCompra.Items.AddRange(new object[] {
            "Cooperativa"});
            this.cbOpcionCompra.Location = new System.Drawing.Point(141, 27);
            this.cbOpcionCompra.Name = "cbOpcionCompra";
            this.cbOpcionCompra.Size = new System.Drawing.Size(197, 21);
            this.cbOpcionCompra.TabIndex = 59;
            // 
            // cbBoca
            // 
            this.cbBoca.FormattingEnabled = true;
            this.cbBoca.Items.AddRange(new object[] {
            "Puerta 1"});
            this.cbBoca.Location = new System.Drawing.Point(448, 27);
            this.cbBoca.Name = "cbBoca";
            this.cbBoca.Size = new System.Drawing.Size(197, 21);
            this.cbBoca.TabIndex = 58;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(398, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 20);
            this.label7.TabIndex = 57;
            this.label7.Text = "Boca:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 20);
            this.label8.TabIndex = 55;
            this.label8.Text = "Opción de Compra:";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btnReimprimir);
            this.groupControl3.Controls.Add(this.btnEliminar);
            this.groupControl3.Controls.Add(this.dgvPesada);
            this.groupControl3.Controls.Add(this.txtKilos);
            this.groupControl3.Controls.Add(this.checkBalanzaAutomatica);
            this.groupControl3.Controls.Add(this.btnAgregarCaja);
            this.groupControl3.Controls.Add(this.cbClase);
            this.groupControl3.Controls.Add(this.label5);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Location = new System.Drawing.Point(4, 387);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(705, 280);
            this.groupControl3.TabIndex = 23;
            this.groupControl3.Text = "Parámetros";
            // 
            // btnReimprimir
            // 
            this.btnReimprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnReimprimir.Image")));
            this.btnReimprimir.Location = new System.Drawing.Point(443, 248);
            this.btnReimprimir.Name = "btnReimprimir";
            this.btnReimprimir.Size = new System.Drawing.Size(126, 26);
            this.btnReimprimir.TabIndex = 66;
            this.btnReimprimir.Text = "Reimprimir";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.Location = new System.Drawing.Point(575, 248);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(126, 26);
            this.btnEliminar.TabIndex = 65;
            this.btnEliminar.Text = "Eliminar";
            // 
            // dgvPesada
            // 
            this.dgvPesada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesada.Location = new System.Drawing.Point(5, 54);
            this.dgvPesada.Name = "dgvPesada";
            this.dgvPesada.Size = new System.Drawing.Size(695, 188);
            this.dgvPesada.TabIndex = 64;
            // 
            // txtKilos
            // 
            this.txtKilos.Enabled = false;
            this.txtKilos.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKilos.Location = new System.Drawing.Point(448, 24);
            this.txtKilos.Name = "txtKilos";
            this.txtKilos.Size = new System.Drawing.Size(121, 26);
            this.txtKilos.TabIndex = 63;
            // 
            // checkBalanzaAutomatica
            // 
            this.checkBalanzaAutomatica.AutoSize = true;
            this.checkBalanzaAutomatica.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBalanzaAutomatica.Location = new System.Drawing.Point(221, 26);
            this.checkBalanzaAutomatica.Name = "checkBalanzaAutomatica";
            this.checkBalanzaAutomatica.Size = new System.Drawing.Size(150, 24);
            this.checkBalanzaAutomatica.TabIndex = 62;
            this.checkBalanzaAutomatica.Text = "Balanza Automática";
            this.checkBalanzaAutomatica.UseVisualStyleBackColor = true;
            // 
            // btnAgregarCaja
            // 
            this.btnAgregarCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarCaja.Image")));
            this.btnAgregarCaja.Location = new System.Drawing.Point(575, 24);
            this.btnAgregarCaja.Name = "btnAgregarCaja";
            this.btnAgregarCaja.Size = new System.Drawing.Size(126, 26);
            this.btnAgregarCaja.TabIndex = 60;
            this.btnAgregarCaja.Text = "Agregar";
            // 
            // cbClase
            // 
            this.cbClase.FormattingEnabled = true;
            this.cbClase.Items.AddRange(new object[] {
            "Cooperativa"});
            this.cbClase.Location = new System.Drawing.Point(61, 27);
            this.cbClase.Name = "cbClase";
            this.cbClase.Size = new System.Drawing.Size(133, 21);
            this.cbClase.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(398, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 57;
            this.label5.Text = "Kilos:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 20);
            this.label6.TabIndex = 55;
            this.label6.Text = "Clase:";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.txtPrecioPromedio);
            this.groupControl4.Controls.Add(this.label12);
            this.groupControl4.Controls.Add(this.txtImporteBruto);
            this.groupControl4.Controls.Add(this.label11);
            this.groupControl4.Controls.Add(this.txtTotalKilo);
            this.groupControl4.Controls.Add(this.label10);
            this.groupControl4.Controls.Add(this.txtTotalFardo);
            this.groupControl4.Controls.Add(this.label9);
            this.groupControl4.Location = new System.Drawing.Point(5, 671);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(704, 89);
            this.groupControl4.TabIndex = 24;
            this.groupControl4.Text = "Totales";
            // 
            // txtPrecioPromedio
            // 
            this.txtPrecioPromedio.Enabled = false;
            this.txtPrecioPromedio.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioPromedio.Location = new System.Drawing.Point(126, 57);
            this.txtPrecioPromedio.Name = "txtPrecioPromedio";
            this.txtPrecioPromedio.Size = new System.Drawing.Size(108, 26);
            this.txtPrecioPromedio.TabIndex = 71;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 20);
            this.label12.TabIndex = 70;
            this.label12.Text = "Precio Promedio:";
            // 
            // txtImporteBruto
            // 
            this.txtImporteBruto.Enabled = false;
            this.txtImporteBruto.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporteBruto.Location = new System.Drawing.Point(556, 25);
            this.txtImporteBruto.Name = "txtImporteBruto";
            this.txtImporteBruto.Size = new System.Drawing.Size(117, 26);
            this.txtImporteBruto.TabIndex = 69;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(451, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 20);
            this.label11.TabIndex = 68;
            this.label11.Text = "Importe Bruto:";
            // 
            // txtTotalKilo
            // 
            this.txtTotalKilo.Enabled = false;
            this.txtTotalKilo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalKilo.Location = new System.Drawing.Point(321, 25);
            this.txtTotalKilo.Name = "txtTotalKilo";
            this.txtTotalKilo.Size = new System.Drawing.Size(109, 26);
            this.txtTotalKilo.TabIndex = 67;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(235, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 66;
            this.label10.Text = "Total Kilos:";
            // 
            // txtTotalFardo
            // 
            this.txtTotalFardo.Enabled = false;
            this.txtTotalFardo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalFardo.Location = new System.Drawing.Point(126, 25);
            this.txtTotalFardo.Name = "txtTotalFardo";
            this.txtTotalFardo.Size = new System.Drawing.Size(108, 26);
            this.txtTotalFardo.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 20);
            this.label9.TabIndex = 64;
            this.label9.Text = "Total Fardos:";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Image = ((System.Drawing.Image)(resources.GetObject("btnFinalizar.Image")));
            this.btnFinalizar.Location = new System.Drawing.Point(447, 766);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(126, 26);
            this.btnFinalizar.TabIndex = 68;
            this.btnFinalizar.Text = "Finalizar";
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(579, 766);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(126, 26);
            this.btnSalir.TabIndex = 67;
            this.btnSalir.Text = "Salir";
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.textBox3);
            this.groupControl5.Controls.Add(this.label15);
            this.groupControl5.Controls.Add(this.simpleButton1);
            this.groupControl5.Location = new System.Drawing.Point(4, 141);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(705, 56);
            this.groupControl5.TabIndex = 70;
            this.groupControl5.Text = "Buscar Productor";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(155, 24);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(159, 26);
            this.textBox3.TabIndex = 58;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(144, 20);
            this.label15.TabIndex = 57;
            this.label15.Text = "Numero de Romaneo:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(320, 24);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(161, 26);
            this.simpleButton1.TabIndex = 39;
            this.simpleButton1.Text = "Recuperar última pesada";
            // 
            // Form_RomaneoPesada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 795);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Name = "Form_RomaneoPesada";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Romaneo - Pesada";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        public System.Windows.Forms.TextBox txtProvincia;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFet;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnBuscarProductor;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ComboBox cbOpcionCompra;
        private System.Windows.Forms.ComboBox cbBoca;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnCancelarPesada;
        private DevExpress.XtraEditors.SimpleButton btnIniciarPesada;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnReimprimir;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private System.Windows.Forms.DataGridView dgvPesada;
        public System.Windows.Forms.TextBox txtKilos;
        private System.Windows.Forms.CheckBox checkBalanzaAutomatica;
        private DevExpress.XtraEditors.SimpleButton btnAgregarCaja;
        private System.Windows.Forms.ComboBox cbClase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        public System.Windows.Forms.TextBox txtPrecioPromedio;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtImporteBruto;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtTotalKilo;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtTotalFardo;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}