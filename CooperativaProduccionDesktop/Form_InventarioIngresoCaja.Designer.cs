namespace CooperativaProduccion
{
    partial class Form_InventarioIngresoCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InventarioIngresoCaja));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Cata = new DevExpress.XtraTab.XtraTabControl();
            this.TabIngresoCaja = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlCaja = new DevExpress.XtraGrid.GridControl();
            this.gridViewCaja = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtCantidadCajaIngreso = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGenerarLote = new DevExpress.XtraEditors.SimpleButton();
            this.txtNeto = new System.Windows.Forms.TextBox();
            this.txtTara = new System.Windows.Forms.TextBox();
            this.txtBruto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbProductoIngreso = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dpIngresoCaja = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.TabConsultaCaja = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirAfip = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlCajaConsulta = new DevExpress.XtraGrid.GridControl();
            this.gridViewCajaConsulta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarLiquidacion = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtCantidadCajaConsulta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBuscarCaja = new DevExpress.XtraEditors.SimpleButton();
            this.cbProductoConsulta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cata)).BeginInit();
            this.Cata.SuspendLayout();
            this.TabIngresoCaja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.TabConsultaCaja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
            this.groupControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCajaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCajaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowCategoryInCaption = false;
            this.ribbon.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowQatLocationSelector = false;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(986, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // Cata
            // 
            this.Cata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cata.Location = new System.Drawing.Point(2, 60);
            this.Cata.Name = "Cata";
            this.Cata.SelectedTabPage = this.TabIngresoCaja;
            this.Cata.Size = new System.Drawing.Size(984, 464);
            this.Cata.TabIndex = 76;
            this.Cata.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabIngresoCaja,
            this.TabConsultaCaja});
            // 
            // TabIngresoCaja
            // 
            this.TabIngresoCaja.Controls.Add(this.groupControl5);
            this.TabIngresoCaja.Controls.Add(this.groupControl1);
            this.TabIngresoCaja.Image = ((System.Drawing.Image)(resources.GetObject("TabIngresoCaja.Image")));
            this.TabIngresoCaja.Name = "TabIngresoCaja";
            this.TabIngresoCaja.Size = new System.Drawing.Size(978, 433);
            this.TabIngresoCaja.Text = "Ingreso de Cajas";
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.gridControlCaja);
            this.groupControl5.Location = new System.Drawing.Point(1, 51);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(978, 383);
            this.groupControl5.TabIndex = 76;
            this.groupControl5.Text = "Detalle de Catas";
            // 
            // gridControlCaja
            // 
            this.gridControlCaja.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCaja.Location = new System.Drawing.Point(2, 20);
            this.gridControlCaja.MainView = this.gridViewCaja;
            this.gridControlCaja.MenuManager = this.ribbon;
            this.gridControlCaja.Name = "gridControlCaja";
            this.gridControlCaja.Size = new System.Drawing.Size(974, 361);
            this.gridControlCaja.TabIndex = 68;
            this.gridControlCaja.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCaja,
            this.gridView2});
            // 
            // gridViewCaja
            // 
            this.gridViewCaja.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewCaja.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCaja.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewCaja.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewCaja.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewCaja.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCaja.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewCaja.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewCaja.GridControl = this.gridControlCaja;
            this.gridViewCaja.Name = "gridViewCaja";
            this.gridViewCaja.OptionsBehavior.Editable = false;
            this.gridViewCaja.OptionsSelection.MultiSelect = true;
            this.gridViewCaja.OptionsView.ShowGroupPanel = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlCaja;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.txtCantidadCajaIngreso);
            this.groupControl1.Controls.Add(this.label10);
            this.groupControl1.Controls.Add(this.btnGenerarLote);
            this.groupControl1.Controls.Add(this.txtNeto);
            this.groupControl1.Controls.Add(this.txtTara);
            this.groupControl1.Controls.Add(this.txtBruto);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.cbProductoIngreso);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.dpIngresoCaja);
            this.groupControl1.Controls.Add(this.label15);
            this.groupControl1.Location = new System.Drawing.Point(1, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(976, 49);
            this.groupControl1.TabIndex = 72;
            this.groupControl1.Text = "Ingreso de Cajas";
            // 
            // txtCantidadCajaIngreso
            // 
            this.txtCantidadCajaIngreso.Location = new System.Drawing.Point(766, 24);
            this.txtCantidadCajaIngreso.Name = "txtCantidadCajaIngreso";
            this.txtCantidadCajaIngreso.Size = new System.Drawing.Size(60, 21);
            this.txtCantidadCajaIngreso.TabIndex = 6;
            this.txtCantidadCajaIngreso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadCajaIngreso_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(664, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 88;
            this.label10.Text = "Cantidad de Cajas";
            // 
            // btnGenerarLote
            // 
            this.btnGenerarLote.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarLote.Image")));
            this.btnGenerarLote.Location = new System.Drawing.Point(832, 23);
            this.btnGenerarLote.Name = "btnGenerarLote";
            this.btnGenerarLote.Size = new System.Drawing.Size(91, 22);
            this.btnGenerarLote.TabIndex = 7;
            this.btnGenerarLote.Text = "Generar Lote";
            this.btnGenerarLote.Click += new System.EventHandler(this.btnGenerarLote_Click);
            // 
            // txtNeto
            // 
            this.txtNeto.Location = new System.Drawing.Point(597, 24);
            this.txtNeto.Name = "txtNeto";
            this.txtNeto.Size = new System.Drawing.Size(61, 21);
            this.txtNeto.TabIndex = 5;
            this.txtNeto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNeto_KeyPress);
            // 
            // txtTara
            // 
            this.txtTara.Location = new System.Drawing.Point(501, 24);
            this.txtTara.Name = "txtTara";
            this.txtTara.Size = new System.Drawing.Size(61, 21);
            this.txtTara.TabIndex = 4;
            this.txtTara.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTara_KeyPress);
            // 
            // txtBruto
            // 
            this.txtBruto.Location = new System.Drawing.Point(401, 24);
            this.txtBruto.Name = "txtBruto";
            this.txtBruto.Size = new System.Drawing.Size(62, 21);
            this.txtBruto.TabIndex = 3;
            this.txtBruto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBruto_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(568, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 16);
            this.label9.TabIndex = 83;
            this.label9.Text = "Neto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(469, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 16);
            this.label8.TabIndex = 82;
            this.label8.Text = "Tara";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(366, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 16);
            this.label7.TabIndex = 81;
            this.label7.Text = "Bruto";
            // 
            // cbProductoIngreso
            // 
            this.cbProductoIngreso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductoIngreso.FormattingEnabled = true;
            this.cbProductoIngreso.Location = new System.Drawing.Point(182, 24);
            this.cbProductoIngreso.Name = "cbProductoIngreso";
            this.cbProductoIngreso.Size = new System.Drawing.Size(178, 21);
            this.cbProductoIngreso.TabIndex = 2;
            this.cbProductoIngreso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbProductoIngreso_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(124, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 78;
            this.label5.Text = "Producto";
            // 
            // dpIngresoCaja
            // 
            this.dpIngresoCaja.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpIngresoCaja.Location = new System.Drawing.Point(43, 24);
            this.dpIngresoCaja.Name = "dpIngresoCaja";
            this.dpIngresoCaja.Size = new System.Drawing.Size(77, 21);
            this.dpIngresoCaja.TabIndex = 1;
            this.dpIngresoCaja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dpIngresoCaja_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 76;
            this.label15.Text = "Fecha";
            // 
            // TabConsultaCaja
            // 
            this.TabConsultaCaja.Controls.Add(this.groupControl7);
            this.TabConsultaCaja.Controls.Add(this.groupControl6);
            this.TabConsultaCaja.Controls.Add(this.groupControl4);
            this.TabConsultaCaja.Controls.Add(this.groupControl2);
            this.TabConsultaCaja.Image = ((System.Drawing.Image)(resources.GetObject("TabConsultaCaja.Image")));
            this.TabConsultaCaja.Name = "TabConsultaCaja";
            this.TabConsultaCaja.Size = new System.Drawing.Size(978, 433);
            this.TabConsultaCaja.Text = "Consulta de Cajas";
            // 
            // groupControl7
            // 
            this.groupControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl7.Controls.Add(this.btnPrevisualizar);
            this.groupControl7.Controls.Add(this.btnSubirAfip);
            this.groupControl7.Location = new System.Drawing.Point(1, 592);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1417, 33);
            this.groupControl7.TabIndex = 76;
            this.groupControl7.Text = "Buscar Romaneo";
            // 
            // btnPrevisualizar
            // 
            this.btnPrevisualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevisualizar.Image")));
            this.btnPrevisualizar.Location = new System.Drawing.Point(92, 5);
            this.btnPrevisualizar.Name = "btnPrevisualizar";
            this.btnPrevisualizar.Size = new System.Drawing.Size(90, 22);
            this.btnPrevisualizar.TabIndex = 41;
            this.btnPrevisualizar.Text = "Previsualizar";
            // 
            // btnSubirAfip
            // 
            this.btnSubirAfip.Image = ((System.Drawing.Image)(resources.GetObject("btnSubirAfip.Image")));
            this.btnSubirAfip.Location = new System.Drawing.Point(5, 5);
            this.btnSubirAfip.Name = "btnSubirAfip";
            this.btnSubirAfip.Size = new System.Drawing.Size(81, 22);
            this.btnSubirAfip.TabIndex = 40;
            this.btnSubirAfip.Text = "Subir Afip";
            // 
            // groupControl6
            // 
            this.groupControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl6.Controls.Add(this.gridControlCajaConsulta);
            this.groupControl6.Location = new System.Drawing.Point(-1, 52);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(978, 382);
            this.groupControl6.TabIndex = 75;
            this.groupControl6.Text = "Lista de Cajas";
            // 
            // gridControlCajaConsulta
            // 
            this.gridControlCajaConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCajaConsulta.Location = new System.Drawing.Point(2, 20);
            this.gridControlCajaConsulta.MainView = this.gridViewCajaConsulta;
            this.gridControlCajaConsulta.MenuManager = this.ribbon;
            this.gridControlCajaConsulta.Name = "gridControlCajaConsulta";
            this.gridControlCajaConsulta.Size = new System.Drawing.Size(974, 360);
            this.gridControlCajaConsulta.TabIndex = 68;
            this.gridControlCajaConsulta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCajaConsulta,
            this.gridViewLiquidacionDetalle});
            // 
            // gridViewCajaConsulta
            // 
            this.gridViewCajaConsulta.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewCajaConsulta.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCajaConsulta.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewCajaConsulta.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewCajaConsulta.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewCajaConsulta.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCajaConsulta.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewCajaConsulta.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewCajaConsulta.GridControl = this.gridControlCajaConsulta;
            this.gridViewCajaConsulta.Name = "gridViewCajaConsulta";
            this.gridViewCajaConsulta.OptionsBehavior.Editable = false;
            this.gridViewCajaConsulta.OptionsSelection.MultiSelect = true;
            this.gridViewCajaConsulta.OptionsView.ShowGroupPanel = false;
            // 
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlCajaConsulta;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscarLiquidacion);
            this.groupControl4.Location = new System.Drawing.Point(1332, 1);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(86, 82);
            this.groupControl4.TabIndex = 74;
            this.groupControl4.Text = "Opciones";
            // 
            // btnBuscarLiquidacion
            // 
            this.btnBuscarLiquidacion.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarLiquidacion.Image")));
            this.btnBuscarLiquidacion.Location = new System.Drawing.Point(3, 23);
            this.btnBuscarLiquidacion.Name = "btnBuscarLiquidacion";
            this.btnBuscarLiquidacion.Size = new System.Drawing.Size(81, 22);
            this.btnBuscarLiquidacion.TabIndex = 39;
            this.btnBuscarLiquidacion.Text = "Buscar";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.txtCantidadCajaConsulta);
            this.groupControl2.Controls.Add(this.label11);
            this.groupControl2.Controls.Add(this.btnBuscarCaja);
            this.groupControl2.Controls.Add(this.cbProductoConsulta);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(1, 1);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(976, 50);
            this.groupControl2.TabIndex = 72;
            this.groupControl2.Text = "Buscar Cata";
            // 
            // txtCantidadCajaConsulta
            // 
            this.txtCantidadCajaConsulta.Location = new System.Drawing.Point(360, 25);
            this.txtCantidadCajaConsulta.Name = "txtCantidadCajaConsulta";
            this.txtCantidadCajaConsulta.Size = new System.Drawing.Size(93, 21);
            this.txtCantidadCajaConsulta.TabIndex = 62;
            this.txtCantidadCajaConsulta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadCajaConsulta_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(253, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 16);
            this.label11.TabIndex = 61;
            this.label11.Text = "Cantidad de Cajas";
            // 
            // btnBuscarCaja
            // 
            this.btnBuscarCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCaja.Image")));
            this.btnBuscarCaja.Location = new System.Drawing.Point(459, 24);
            this.btnBuscarCaja.Name = "btnBuscarCaja";
            this.btnBuscarCaja.Size = new System.Drawing.Size(76, 21);
            this.btnBuscarCaja.TabIndex = 60;
            this.btnBuscarCaja.Text = "Buscar";
            this.btnBuscarCaja.Click += new System.EventHandler(this.btnBuscarCaja_Click);
            // 
            // cbProductoConsulta
            // 
            this.cbProductoConsulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductoConsulta.FormattingEnabled = true;
            this.cbProductoConsulta.Location = new System.Drawing.Point(69, 25);
            this.cbProductoConsulta.Name = "cbProductoConsulta";
            this.cbProductoConsulta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbProductoConsulta.Size = new System.Drawing.Size(177, 21);
            this.cbProductoConsulta.TabIndex = 58;
            this.cbProductoConsulta.SelectedIndexChanged += new System.EventHandler(this.cbProductoConsulta_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Producto";
            // 
            // Form_InventarioIngresoCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 536);
            this.Controls.Add(this.Cata);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_InventarioIngresoCaja";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario - Ingreso de Cajas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cata)).EndInit();
            this.Cata.ResumeLayout(false);
            this.TabIngresoCaja.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.TabConsultaCaja.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCajaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCajaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraTab.XtraTabControl Cata;
        private DevExpress.XtraTab.XtraTabPage TabIngresoCaja;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraGrid.GridControl gridControlCaja;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCaja;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox txtCantidadCajaIngreso;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.SimpleButton btnGenerarLote;
        private System.Windows.Forms.TextBox txtNeto;
        private System.Windows.Forms.TextBox txtTara;
        private System.Windows.Forms.TextBox txtBruto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbProductoIngreso;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dpIngresoCaja;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraTab.XtraTabPage TabConsultaCaja;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private DevExpress.XtraEditors.SimpleButton btnPrevisualizar;
        private DevExpress.XtraEditors.SimpleButton btnSubirAfip;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlCajaConsulta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCajaConsulta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalle;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnBuscarLiquidacion;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox txtCantidadCajaConsulta;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCaja;
        private System.Windows.Forms.ComboBox cbProductoConsulta;
        private System.Windows.Forms.Label label3;
    }
}