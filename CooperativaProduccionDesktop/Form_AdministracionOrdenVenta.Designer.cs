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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionOrdenVenta));
            this.gridViewOrdenVentaDetalleConsulta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlOrdenVentaConsulta = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrdenVentaConsulta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.OrdenVenta = new DevExpress.XtraTab.XtraTabControl();
            this.TabNuevaOrdenVenta = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrdenVenta = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrdenVenta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtNumOrdenVenta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNuevaOrdenVenta = new DevExpress.XtraEditors.SimpleButton();
            this.txtNumOperacion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TabConsultaOrdenVenta = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExportarResumen = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirAfip = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarLiquidacion = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarCliente = new DevExpress.XtraEditors.SimpleButton();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dpHastaOrden = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dpDesdeOrden = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscarPendientes = new DevExpress.XtraEditors.SimpleButton();
            this.checkPendienteEmitirRemito = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaDetalleConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVentaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridViewOrdenVentaDetalleConsulta
            // 
            this.gridViewOrdenVentaDetalleConsulta.GridControl = this.gridControlOrdenVentaConsulta;
            this.gridViewOrdenVentaDetalleConsulta.Name = "gridViewOrdenVentaDetalleConsulta";
            this.gridViewOrdenVentaDetalleConsulta.OptionsView.ColumnAutoWidth = false;
            this.gridViewOrdenVentaDetalleConsulta.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlOrdenVentaConsulta
            // 
            this.gridControlOrdenVentaConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gridViewOrdenVentaDetalleConsulta;
            gridLevelNode1.RelationName = "Level1";
            this.gridControlOrdenVentaConsulta.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControlOrdenVentaConsulta.Location = new System.Drawing.Point(2, 20);
            this.gridControlOrdenVentaConsulta.MainView = this.gridViewOrdenVentaConsulta;
            this.gridControlOrdenVentaConsulta.MenuManager = this.ribbon;
            this.gridControlOrdenVentaConsulta.Name = "gridControlOrdenVentaConsulta";
            this.gridControlOrdenVentaConsulta.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControlOrdenVentaConsulta.Size = new System.Drawing.Size(909, 318);
            this.gridControlOrdenVentaConsulta.TabIndex = 68;
            this.gridControlOrdenVentaConsulta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrdenVentaConsulta,
            this.gridViewLiquidacionDetalle,
            this.gridViewOrdenVentaDetalleConsulta});
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
            this.gridViewOrdenVentaConsulta.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewOrdenVentaConsulta.OptionsView.ColumnAutoWidth = false;
            this.gridViewOrdenVentaConsulta.OptionsView.ShowGroupPanel = false;
            this.gridViewOrdenVentaConsulta.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridViewOrdenVentaConsulta_MasterRowExpanded);
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
            this.ribbon.Size = new System.Drawing.Size(920, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlOrdenVentaConsulta;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
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
            this.OrdenVenta.Size = new System.Drawing.Size(919, 461);
            this.OrdenVenta.TabIndex = 75;
            this.OrdenVenta.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabNuevaOrdenVenta,
            this.TabConsultaOrdenVenta});
            // 
            // TabNuevaOrdenVenta
            // 
            this.TabNuevaOrdenVenta.Controls.Add(this.groupControl5);
            this.TabNuevaOrdenVenta.Controls.Add(this.groupControl3);
            this.TabNuevaOrdenVenta.Image = ((System.Drawing.Image)(resources.GetObject("TabNuevaOrdenVenta.Image")));
            this.TabNuevaOrdenVenta.Name = "TabNuevaOrdenVenta";
            this.TabNuevaOrdenVenta.Size = new System.Drawing.Size(913, 430);
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
            this.groupControl5.Size = new System.Drawing.Size(912, 375);
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
            this.gridControlOrdenVenta.Size = new System.Drawing.Size(908, 353);
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
            this.groupControl3.Controls.Add(this.txtNumOrdenVenta);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Controls.Add(this.btnNuevaOrdenVenta);
            this.groupControl3.Controls.Add(this.txtNumOperacion);
            this.groupControl3.Controls.Add(this.label11);
            this.groupControl3.Location = new System.Drawing.Point(1, 1);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(911, 52);
            this.groupControl3.TabIndex = 73;
            this.groupControl3.Text = "Orden de Venta";
            // 
            // txtNumOrdenVenta
            // 
            this.txtNumOrdenVenta.Enabled = false;
            this.txtNumOrdenVenta.Location = new System.Drawing.Point(231, 24);
            this.txtNumOrdenVenta.Name = "txtNumOrdenVenta";
            this.txtNumOrdenVenta.ReadOnly = true;
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
            // btnNuevaOrdenVenta
            // 
            this.btnNuevaOrdenVenta.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaOrdenVenta.Image")));
            this.btnNuevaOrdenVenta.Location = new System.Drawing.Point(317, 24);
            this.btnNuevaOrdenVenta.Name = "btnNuevaOrdenVenta";
            this.btnNuevaOrdenVenta.Size = new System.Drawing.Size(99, 21);
            this.btnNuevaOrdenVenta.TabIndex = 71;
            this.btnNuevaOrdenVenta.Text = "Nueva Orden";
            this.btnNuevaOrdenVenta.Click += new System.EventHandler(this.btnNuevaOrdenVenta_Click);
            // 
            // txtNumOperacion
            // 
            this.txtNumOperacion.Enabled = false;
            this.txtNumOperacion.Location = new System.Drawing.Point(83, 24);
            this.txtNumOperacion.Name = "txtNumOperacion";
            this.txtNumOperacion.ReadOnly = true;
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
            // TabConsultaOrdenVenta
            // 
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl1);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl7);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl6);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl4);
            this.TabConsultaOrdenVenta.Controls.Add(this.groupControl2);
            this.TabConsultaOrdenVenta.Image = ((System.Drawing.Image)(resources.GetObject("TabConsultaOrdenVenta.Image")));
            this.TabConsultaOrdenVenta.Name = "TabConsultaOrdenVenta";
            this.TabConsultaOrdenVenta.Size = new System.Drawing.Size(913, 430);
            this.TabConsultaOrdenVenta.Text = "Consulta - Orden de Venta";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnExportarResumen);
            this.groupControl1.Location = new System.Drawing.Point(1, 396);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(911, 33);
            this.groupControl1.TabIndex = 78;
            this.groupControl1.Text = "Buscar Cata";
            // 
            // btnExportarResumen
            // 
            this.btnExportarResumen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarResumen.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarResumen.Image")));
            this.btnExportarResumen.Location = new System.Drawing.Point(777, 5);
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
            this.groupControl7.Location = new System.Drawing.Point(1, 591);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1494, 33);
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
            this.groupControl6.Size = new System.Drawing.Size(913, 340);
            this.groupControl6.TabIndex = 75;
            this.groupControl6.Text = "Detalle de Orden";
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscarLiquidacion);
            this.groupControl4.Location = new System.Drawing.Point(1409, 1);
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
            this.groupControl2.Controls.Add(this.btnBuscarCliente);
            this.groupControl2.Controls.Add(this.txtCliente);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.dpHastaOrden);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.dpDesdeOrden);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.btnBuscarPendientes);
            this.groupControl2.Controls.Add(this.checkPendienteEmitirRemito);
            this.groupControl2.Location = new System.Drawing.Point(1, 1);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(911, 53);
            this.groupControl2.TabIndex = 72;
            this.groupControl2.Text = "Buscar Cata";
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBuscarCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.Image")));
            this.btnBuscarCliente.Location = new System.Drawing.Point(630, 26);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(26, 21);
            this.btnBuscarCliente.TabIndex = 106;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCliente.Location = new System.Drawing.Point(311, 26);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(319, 21);
            this.txtCliente.TabIndex = 104;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 105;
            this.label1.Text = "Cliente";
            // 
            // dpHastaOrden
            // 
            this.dpHastaOrden.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHastaOrden.Location = new System.Drawing.Point(168, 24);
            this.dpHastaOrden.Name = "dpHastaOrden";
            this.dpHastaOrden.Size = new System.Drawing.Size(79, 21);
            this.dpHastaOrden.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 63;
            this.label2.Text = "Hasta";
            // 
            // dpDesdeOrden
            // 
            this.dpDesdeOrden.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesdeOrden.Location = new System.Drawing.Point(46, 25);
            this.dpDesdeOrden.Name = "dpDesdeOrden";
            this.dpDesdeOrden.Size = new System.Drawing.Size(79, 21);
            this.dpDesdeOrden.TabIndex = 62;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "Desde";
            // 
            // btnBuscarPendientes
            // 
            this.btnBuscarPendientes.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarPendientes.Image")));
            this.btnBuscarPendientes.Location = new System.Drawing.Point(829, 27);
            this.btnBuscarPendientes.Name = "btnBuscarPendientes";
            this.btnBuscarPendientes.Size = new System.Drawing.Size(76, 20);
            this.btnBuscarPendientes.TabIndex = 60;
            this.btnBuscarPendientes.Text = "Buscar";
            this.btnBuscarPendientes.Click += new System.EventHandler(this.btnBuscarPendientes_Click);
            // 
            // checkPendienteEmitirRemito
            // 
            this.checkPendienteEmitirRemito.AutoSize = true;
            this.checkPendienteEmitirRemito.Location = new System.Drawing.Point(667, 29);
            this.checkPendienteEmitirRemito.Name = "checkPendienteEmitirRemito";
            this.checkPendienteEmitirRemito.Size = new System.Drawing.Size(156, 17);
            this.checkPendienteEmitirRemito.TabIndex = 59;
            this.checkPendienteEmitirRemito.Text = "Pendientes de emitir remito";
            this.checkPendienteEmitirRemito.UseVisualStyleBackColor = true;
            // 
            // Form_AdministracionOrdenVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 542);
            this.Controls.Add(this.OrdenVenta);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionOrdenVenta";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración - Orden de Venta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_AdministracionOrdenVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaDetalleConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVentaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).EndInit();
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
        private DevExpress.XtraTab.XtraTabPage TabNuevaOrdenVenta;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TextBox txtNumOperacion;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SimpleButton btnNuevaOrdenVenta;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraGrid.GridControl gridControlOrdenVenta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrdenVenta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.TextBox txtNumOrdenVenta;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportarResumen;
        private System.Windows.Forms.DateTimePicker dpHastaOrden;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpDesdeOrden;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrdenVentaDetalleConsulta;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}