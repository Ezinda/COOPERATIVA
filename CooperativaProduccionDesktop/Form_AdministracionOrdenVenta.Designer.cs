namespace CooperativaProduccion
{
    partial class Form_AdministracionOrdenVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionOrdenVenta));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.OrdenVenta = new DevExpress.XtraTab.XtraTabControl();
            this.TabNuevaOrdenVenta = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrdenVenta = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrdenVenta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarCliente = new DevExpress.XtraEditors.SimpleButton();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtNumOrdenVenta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGenerarOrdenVenta = new DevExpress.XtraEditors.SimpleButton();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumOperacion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TabConsultaOrdenVenta = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExportarResumen = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirAfip = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrdenVentaConsulta = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrdenVentaConsulta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarLiquidacion = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarPendientes = new DevExpress.XtraEditors.SimpleButton();
            this.checkPendienteEmitirRemito = new System.Windows.Forms.CheckBox();
            this.cbOperacionCliente = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdenVenta)).BeginInit();
            this.OrdenVenta.SuspendLayout();
            this.TabNuevaOrdenVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.TabConsultaOrdenVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
            this.groupControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVentaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaConsulta)).BeginInit();
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
            this.ribbon.ShowQatLocationSelector = false;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(983, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // OrdenVenta
            // 
            this.OrdenVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrdenVenta.Location = new System.Drawing.Point(2, 53);
            this.OrdenVenta.Name = "OrdenVenta";
            this.OrdenVenta.SelectedTabPage = this.TabNuevaOrdenVenta;
            this.OrdenVenta.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.OrdenVenta.Size = new System.Drawing.Size(982, 491);
            this.OrdenVenta.TabIndex = 75;
            this.OrdenVenta.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabNuevaOrdenVenta,
            this.TabConsultaOrdenVenta});
            this.OrdenVenta.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.OrdenVenta_SelectedPageChanged);
            // 
            // TabNuevaOrdenVenta
            // 
            this.TabNuevaOrdenVenta.Controls.Add(this.groupControl5);
            this.TabNuevaOrdenVenta.Controls.Add(this.groupControl3);
            this.TabNuevaOrdenVenta.Image = ((System.Drawing.Image)(resources.GetObject("TabNuevaOrdenVenta.Image")));
            this.TabNuevaOrdenVenta.Name = "TabNuevaOrdenVenta";
            this.TabNuevaOrdenVenta.Size = new System.Drawing.Size(976, 460);
            this.TabNuevaOrdenVenta.Text = "Nueva Orden de Venta";
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.gridControlOrdenVenta);
            this.groupControl5.Location = new System.Drawing.Point(0, 54);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(976, 407);
            this.groupControl5.TabIndex = 76;
            this.groupControl5.Text = "Detalle de Orden";
            // 
            // gridControlOrdenVenta
            // 
            this.gridControlOrdenVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOrdenVenta.Location = new System.Drawing.Point(2, 20);
            this.gridControlOrdenVenta.MainView = this.gridViewOrdenVenta;
            this.gridControlOrdenVenta.MenuManager = this.ribbon;
            this.gridControlOrdenVenta.Name = "gridControlOrdenVenta";
            this.gridControlOrdenVenta.Size = new System.Drawing.Size(972, 385);
            this.gridControlOrdenVenta.TabIndex = 68;
            this.gridControlOrdenVenta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrdenVenta,
            this.gridView2});
            // 
            // gridViewOrdenVenta
            // 
            this.gridViewOrdenVenta.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewOrdenVenta.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewOrdenVenta.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewOrdenVenta.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewOrdenVenta.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewOrdenVenta.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewOrdenVenta.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewOrdenVenta.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewOrdenVenta.GridControl = this.gridControlOrdenVenta;
            this.gridViewOrdenVenta.Name = "gridViewOrdenVenta";
            this.gridViewOrdenVenta.OptionsBehavior.Editable = false;
            this.gridViewOrdenVenta.OptionsSelection.MultiSelect = true;
            this.gridViewOrdenVenta.OptionsView.ShowGroupPanel = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlOrdenVenta;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnBuscarCliente);
            this.groupControl3.Controls.Add(this.txtCliente);
            this.groupControl3.Controls.Add(this.txtNumOrdenVenta);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Controls.Add(this.btnGenerarOrdenVenta);
            this.groupControl3.Controls.Add(this.cbProducto);
            this.groupControl3.Controls.Add(this.label2);
            this.groupControl3.Controls.Add(this.txtNumOperacion);
            this.groupControl3.Controls.Add(this.label11);
            this.groupControl3.Controls.Add(this.label1);
            this.groupControl3.Location = new System.Drawing.Point(1, 1);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(974, 52);
            this.groupControl3.TabIndex = 73;
            this.groupControl3.Text = "Orden de Venta";
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscarCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.Image")));
            this.btnBuscarCliente.Location = new System.Drawing.Point(622, 24);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(29, 22);
            this.btnBuscarCliente.TabIndex = 75;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCliente.Location = new System.Drawing.Point(362, 24);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(257, 21);
            this.txtCliente.TabIndex = 74;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // txtNumOrdenVenta
            // 
            this.txtNumOrdenVenta.Enabled = false;
            this.txtNumOrdenVenta.Location = new System.Drawing.Point(231, 24);
            this.txtNumOrdenVenta.Name = "txtNumOrdenVenta";
            this.txtNumOrdenVenta.Size = new System.Drawing.Size(80, 21);
            this.txtNumOrdenVenta.TabIndex = 73;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(169, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 72;
            this.label6.Text = "N° Orden";
            // 
            // btnGenerarOrdenVenta
            // 
            this.btnGenerarOrdenVenta.Location = new System.Drawing.Point(897, 24);
            this.btnGenerarOrdenVenta.Name = "btnGenerarOrdenVenta";
            this.btnGenerarOrdenVenta.Size = new System.Drawing.Size(75, 21);
            this.btnGenerarOrdenVenta.TabIndex = 71;
            this.btnGenerarOrdenVenta.Text = "Generar";
            this.btnGenerarOrdenVenta.Click += new System.EventHandler(this.btnGenerarOrdenVenta_Click);
            // 
            // cbProducto
            // 
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(720, 25);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(171, 21);
            this.cbProducto.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(657, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 65;
            this.label2.Text = "Producto";
            // 
            // txtNumOperacion
            // 
            this.txtNumOperacion.Enabled = false;
            this.txtNumOperacion.Location = new System.Drawing.Point(83, 24);
            this.txtNumOperacion.Name = "txtNumOperacion";
            this.txtNumOperacion.Size = new System.Drawing.Size(80, 21);
            this.txtNumOperacion.TabIndex = 64;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 16);
            this.label11.TabIndex = 63;
            this.label11.Text = "N° Operación";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(317, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 57;
            this.label1.Text = "Cliente";
            // 
            // TabConsultaOrdenVenta
            // 
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl1);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl7);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl6);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl4);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl2);
            this.TabConsultaOrdenVenta.Image = ((System.Drawing.Image)(resources.GetObject("TabConsultaOrdenVenta.Image")));
            this.TabConsultaOrdenVenta.Name = "TabConsultaOrdenVenta";
            this.TabConsultaOrdenVenta.Size = new System.Drawing.Size(976, 460);
            this.TabConsultaOrdenVenta.Text = "Consulta - Orden de Venta";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnExportarResumen);
            this.groupControl1.Location = new System.Drawing.Point(1, 426);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(974, 33);
            this.groupControl1.TabIndex = 78;
            this.groupControl1.Text = "Buscar Cata";
            // 
            // btnExportarResumen
            // 
            this.btnExportarResumen.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarResumen.Image")));
            this.btnExportarResumen.Location = new System.Drawing.Point(5, 5);
            this.btnExportarResumen.Name = "btnExportarResumen";
            this.btnExportarResumen.Size = new System.Drawing.Size(127, 22);
            this.btnExportarResumen.TabIndex = 61;
            this.btnExportarResumen.Text = "Exportar Resumen";
            this.btnExportarResumen.Click += new System.EventHandler(this.btnExportarResumen_Click);
            // 
            // groupControl7
            // 
            this.groupControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl7.Controls.Add(this.btnPrevisualizar);
            this.groupControl7.Controls.Add(this.btnSubirAfip);
            this.groupControl7.Location = new System.Drawing.Point(1, 606);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1557, 33);
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
            this.groupControl6.Controls.Add(this.gridControlOrdenVentaConsulta);
            this.groupControl6.Location = new System.Drawing.Point(-1, 55);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(978, 370);
            this.groupControl6.TabIndex = 75;
            this.groupControl6.Text = "Detalle de Orden";
            // 
            // gridControlOrdenVentaConsulta
            // 
            this.gridControlOrdenVentaConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOrdenVentaConsulta.Location = new System.Drawing.Point(2, 20);
            this.gridControlOrdenVentaConsulta.MainView = this.gridViewOrdenVentaConsulta;
            this.gridControlOrdenVentaConsulta.MenuManager = this.ribbon;
            this.gridControlOrdenVentaConsulta.Name = "gridControlOrdenVentaConsulta";
            this.gridControlOrdenVentaConsulta.Size = new System.Drawing.Size(974, 348);
            this.gridControlOrdenVentaConsulta.TabIndex = 68;
            this.gridControlOrdenVentaConsulta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrdenVentaConsulta,
            this.gridViewLiquidacionDetalle});
            this.gridControlOrdenVentaConsulta.DoubleClick += new System.EventHandler(this.gridControlOrdenVentaConsulta_DoubleClick);
            // 
            // gridViewOrdenVentaConsulta
            // 
            this.gridViewOrdenVentaConsulta.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewOrdenVentaConsulta.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewOrdenVentaConsulta.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewOrdenVentaConsulta.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewOrdenVentaConsulta.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewOrdenVentaConsulta.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewOrdenVentaConsulta.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewOrdenVentaConsulta.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewOrdenVentaConsulta.GridControl = this.gridControlOrdenVentaConsulta;
            this.gridViewOrdenVentaConsulta.Name = "gridViewOrdenVentaConsulta";
            this.gridViewOrdenVentaConsulta.OptionsBehavior.Editable = false;
            this.gridViewOrdenVentaConsulta.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewOrdenVentaConsulta.OptionsView.ShowGroupPanel = false;
            // 
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlOrdenVentaConsulta;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscarLiquidacion);
            this.groupControl4.Location = new System.Drawing.Point(1472, 1);
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
            this.groupControl2.Controls.Add(this.btnBuscarPendientes);
            this.groupControl2.Controls.Add(this.checkPendienteEmitirRemito);
            this.groupControl2.Controls.Add(this.cbOperacionCliente);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(1, 1);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(976, 53);
            this.groupControl2.TabIndex = 72;
            this.groupControl2.Text = "Buscar Cata";
            // 
            // btnBuscarPendientes
            // 
            this.btnBuscarPendientes.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarPendientes.Image")));
            this.btnBuscarPendientes.Location = new System.Drawing.Point(532, 27);
            this.btnBuscarPendientes.Name = "btnBuscarPendientes";
            this.btnBuscarPendientes.Size = new System.Drawing.Size(76, 20);
            this.btnBuscarPendientes.TabIndex = 60;
            this.btnBuscarPendientes.Text = "Buscar";
            this.btnBuscarPendientes.Click += new System.EventHandler(this.btnBuscarPendientes_Click);
            // 
            // checkPendienteEmitirRemito
            // 
            this.checkPendienteEmitirRemito.AutoSize = true;
            this.checkPendienteEmitirRemito.Location = new System.Drawing.Point(370, 29);
            this.checkPendienteEmitirRemito.Name = "checkPendienteEmitirRemito";
            this.checkPendienteEmitirRemito.Size = new System.Drawing.Size(156, 17);
            this.checkPendienteEmitirRemito.TabIndex = 59;
            this.checkPendienteEmitirRemito.Text = "Pendientes de emitir remito";
            this.checkPendienteEmitirRemito.UseVisualStyleBackColor = true;
            // 
            // cbOperacionCliente
            // 
            this.cbOperacionCliente.FormattingEnabled = true;
            this.cbOperacionCliente.Location = new System.Drawing.Point(60, 26);
            this.cbOperacionCliente.Name = "cbOperacionCliente";
            this.cbOperacionCliente.Size = new System.Drawing.Size(298, 21);
            this.cbOperacionCliente.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "N° Venta";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_AdministracionOrdenVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 542);
            this.Controls.Add(this.OrdenVenta);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionOrdenVenta";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración - Orden de Venta";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdenVenta)).EndInit();
            this.OrdenVenta.ResumeLayout(false);
            this.TabNuevaOrdenVenta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            this.TabConsultaOrdenVenta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVentaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaConsulta)).EndInit();
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
        private DevExpress.XtraTab.XtraTabControl OrdenVenta;
        private DevExpress.XtraTab.XtraTabPage TabConsultaOrdenVenta;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private DevExpress.XtraEditors.SimpleButton btnPrevisualizar;
        private DevExpress.XtraEditors.SimpleButton btnSubirAfip;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlOrdenVentaConsulta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrdenVentaConsulta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalle;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnBuscarLiquidacion;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnBuscarPendientes;
        private System.Windows.Forms.CheckBox checkPendienteEmitirRemito;
        private System.Windows.Forms.ComboBox cbOperacionCliente;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraTab.XtraTabPage TabNuevaOrdenVenta;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumOperacion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnGenerarOrdenVenta;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraGrid.GridControl gridControlOrdenVenta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrdenVenta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.TextBox txtNumOrdenVenta;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportarResumen;
        private System.Windows.Forms.TextBox txtCliente;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCliente;
    }
}