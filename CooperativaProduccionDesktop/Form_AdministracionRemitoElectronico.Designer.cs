namespace CooperativaProduccion
{
    partial class Form_AdministracionRemitoElectronico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionRemitoElectronico));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Remito = new DevExpress.XtraTab.XtraTabControl();
            this.TabIngresoRemito = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrdenVentaPendiente = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrdenVentaPendiente = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarOrdenVentaPendiente = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscarCliente = new DevExpress.XtraEditors.SimpleButton();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.TabConsultaRemito = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl8 = new DevExpress.XtraEditors.GroupControl();
            this.btnAsignarCata = new DevExpress.XtraEditors.SimpleButton();
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
            this.checkPeriodo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Remito)).BeginInit();
            this.Remito.SuspendLayout();
            this.TabIngresoRemito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVentaPendiente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaPendiente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.TabConsultaRemito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).BeginInit();
            this.groupControl8.SuspendLayout();
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
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(888, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // Remito
            // 
            this.Remito.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Remito.Location = new System.Drawing.Point(-3, 54);
            this.Remito.Name = "Remito";
            this.Remito.SelectedTabPage = this.TabIngresoRemito;
            this.Remito.Size = new System.Drawing.Size(896, 482);
            this.Remito.TabIndex = 76;
            this.Remito.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabIngresoRemito,
            this.TabConsultaRemito});
            // 
            // TabIngresoRemito
            // 
            this.TabIngresoRemito.Controls.Add(this.groupControl3);
            this.TabIngresoRemito.Controls.Add(this.groupControl1);
            this.TabIngresoRemito.Image = ((System.Drawing.Image)(resources.GetObject("TabIngresoRemito.Image")));
            this.TabIngresoRemito.Name = "TabIngresoRemito";
            this.TabIngresoRemito.Size = new System.Drawing.Size(890, 451);
            this.TabIngresoRemito.Text = "Ingreso de Remito";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.gridControlOrdenVentaPendiente);
            this.groupControl3.Location = new System.Drawing.Point(0, 54);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(891, 397);
            this.groupControl3.TabIndex = 76;
            this.groupControl3.Text = "Ordenes de Venta Pendientes";
            // 
            // gridControlOrdenVentaPendiente
            // 
            this.gridControlOrdenVentaPendiente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOrdenVentaPendiente.Location = new System.Drawing.Point(2, 21);
            this.gridControlOrdenVentaPendiente.MainView = this.gridViewOrdenVentaPendiente;
            this.gridControlOrdenVentaPendiente.MenuManager = this.ribbon;
            this.gridControlOrdenVentaPendiente.Name = "gridControlOrdenVentaPendiente";
            this.gridControlOrdenVentaPendiente.Size = new System.Drawing.Size(887, 374);
            this.gridControlOrdenVentaPendiente.TabIndex = 68;
            this.gridControlOrdenVentaPendiente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrdenVentaPendiente,
            this.gridView2});
            this.gridControlOrdenVentaPendiente.DoubleClick += new System.EventHandler(this.gridControlOrdenVentaPendiente_DoubleClick);
            // 
            // gridViewOrdenVentaPendiente
            // 
            this.gridViewOrdenVentaPendiente.GridControl = this.gridControlOrdenVentaPendiente;
            this.gridViewOrdenVentaPendiente.Name = "gridViewOrdenVentaPendiente";
            this.gridViewOrdenVentaPendiente.OptionsBehavior.Editable = false;
            this.gridViewOrdenVentaPendiente.OptionsSelection.MultiSelect = true;
            this.gridViewOrdenVentaPendiente.OptionsView.ShowGroupPanel = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlOrdenVentaPendiente;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.checkPeriodo);
            this.groupControl1.Controls.Add(this.btnBuscarOrdenVentaPendiente);
            this.groupControl1.Controls.Add(this.btnBuscarCliente);
            this.groupControl1.Controls.Add(this.txtCliente);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.dpHasta);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.dpDesde);
            this.groupControl1.Controls.Add(this.label15);
            this.groupControl1.Location = new System.Drawing.Point(1, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(888, 52);
            this.groupControl1.TabIndex = 72;
            this.groupControl1.Text = "Buscar ordenes de venta pendientes";
            // 
            // btnBuscarOrdenVentaPendiente
            // 
            this.btnBuscarOrdenVentaPendiente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarOrdenVentaPendiente.Image")));
            this.btnBuscarOrdenVentaPendiente.Location = new System.Drawing.Point(807, 26);
            this.btnBuscarOrdenVentaPendiente.Name = "btnBuscarOrdenVentaPendiente";
            this.btnBuscarOrdenVentaPendiente.Size = new System.Drawing.Size(76, 21);
            this.btnBuscarOrdenVentaPendiente.TabIndex = 82;
            this.btnBuscarOrdenVentaPendiente.Text = "Buscar";
            this.btnBuscarOrdenVentaPendiente.Click += new System.EventHandler(this.btnBuscarOrdenVentaPendiente_Click);
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.Image")));
            this.btnBuscarCliente.Location = new System.Drawing.Point(757, 26);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(25, 21);
            this.btnBuscarCliente.TabIndex = 81;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.Location = new System.Drawing.Point(448, 26);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(307, 21);
            this.txtCliente.TabIndex = 80;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(397, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 79;
            this.label2.Text = "Cliente";
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(272, 26);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(99, 21);
            this.dpHasta.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(191, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 78;
            this.label1.Text = "Fecha hasta";
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(86, 26);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.Size = new System.Drawing.Size(99, 21);
            this.dpDesde.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 16);
            this.label15.TabIndex = 76;
            this.label15.Text = "Fecha desde";
            // 
            // TabConsultaRemito
            // 
            this.TabConsultaRemito.Controls.Add(this.groupControl8);
            this.TabConsultaRemito.Controls.Add(this.groupControl7);
            this.TabConsultaRemito.Controls.Add(this.groupControl6);
            this.TabConsultaRemito.Controls.Add(this.groupControl4);
            this.TabConsultaRemito.Controls.Add(this.groupControl2);
            this.TabConsultaRemito.Image = ((System.Drawing.Image)(resources.GetObject("TabConsultaRemito.Image")));
            this.TabConsultaRemito.Name = "TabConsultaRemito";
            this.TabConsultaRemito.Size = new System.Drawing.Size(929, 451);
            this.TabConsultaRemito.Text = "Consulta de Remitos";
            // 
            // groupControl8
            // 
            this.groupControl8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl8.Controls.Add(this.btnAsignarCata);
            this.groupControl8.Location = new System.Drawing.Point(1, 419);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.ShowCaption = false;
            this.groupControl8.Size = new System.Drawing.Size(928, 31);
            this.groupControl8.TabIndex = 73;
            this.groupControl8.Text = "Buscar Cata";
            // 
            // btnAsignarCata
            // 
            this.btnAsignarCata.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignarCata.Image")));
            this.btnAsignarCata.Location = new System.Drawing.Point(5, 2);
            this.btnAsignarCata.Name = "btnAsignarCata";
            this.btnAsignarCata.Size = new System.Drawing.Size(100, 26);
            this.btnAsignarCata.TabIndex = 60;
            this.btnAsignarCata.Text = "Asignar Cata";
            // 
            // groupControl7
            // 
            this.groupControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl7.Controls.Add(this.btnPrevisualizar);
            this.groupControl7.Controls.Add(this.btnSubirAfip);
            this.groupControl7.Location = new System.Drawing.Point(1, 601);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1368, 33);
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
            this.groupControl6.Location = new System.Drawing.Point(-1, 55);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(930, 365);
            this.groupControl6.TabIndex = 75;
            this.groupControl6.Text = "Lista de Cajas";
            // 
            // gridControlCajaConsulta
            // 
            this.gridControlCajaConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCajaConsulta.Location = new System.Drawing.Point(2, 21);
            this.gridControlCajaConsulta.MainView = this.gridViewCajaConsulta;
            this.gridControlCajaConsulta.MenuManager = this.ribbon;
            this.gridControlCajaConsulta.Name = "gridControlCajaConsulta";
            this.gridControlCajaConsulta.Size = new System.Drawing.Size(926, 342);
            this.gridControlCajaConsulta.TabIndex = 68;
            this.gridControlCajaConsulta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCajaConsulta,
            this.gridViewLiquidacionDetalle});
            // 
            // gridViewCajaConsulta
            // 
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
            this.groupControl4.Location = new System.Drawing.Point(1283, 1);
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
            this.groupControl2.Size = new System.Drawing.Size(928, 53);
            this.groupControl2.TabIndex = 72;
            this.groupControl2.Text = "Buscar Cata";
            // 
            // txtCantidadCajaConsulta
            // 
            this.txtCantidadCajaConsulta.Location = new System.Drawing.Point(388, 26);
            this.txtCantidadCajaConsulta.Name = "txtCantidadCajaConsulta";
            this.txtCantidadCajaConsulta.Size = new System.Drawing.Size(53, 21);
            this.txtCantidadCajaConsulta.TabIndex = 62;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(274, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 16);
            this.label11.TabIndex = 61;
            this.label11.Text = "Cantidad de Cajas:";
            // 
            // btnBuscarCaja
            // 
            this.btnBuscarCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCaja.Image")));
            this.btnBuscarCaja.Location = new System.Drawing.Point(447, 23);
            this.btnBuscarCaja.Name = "btnBuscarCaja";
            this.btnBuscarCaja.Size = new System.Drawing.Size(76, 26);
            this.btnBuscarCaja.TabIndex = 60;
            this.btnBuscarCaja.Text = "Buscar";
            // 
            // cbProductoConsulta
            // 
            this.cbProductoConsulta.FormattingEnabled = true;
            this.cbProductoConsulta.Location = new System.Drawing.Point(69, 26);
            this.cbProductoConsulta.Name = "cbProductoConsulta";
            this.cbProductoConsulta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbProductoConsulta.Size = new System.Drawing.Size(199, 21);
            this.cbProductoConsulta.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Producto";
            // 
            // checkPeriodo
            // 
            this.checkPeriodo.AutoSize = true;
            this.checkPeriodo.Location = new System.Drawing.Point(377, 29);
            this.checkPeriodo.Name = "checkPeriodo";
            this.checkPeriodo.Size = new System.Drawing.Size(15, 14);
            this.checkPeriodo.TabIndex = 83;
            this.checkPeriodo.UseVisualStyleBackColor = true;
            // 
            // Form_AdministracionRemitoElectronico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 529);
            this.Controls.Add(this.Remito);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionRemitoElectronico";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración - Remito Electrónico";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Remito)).EndInit();
            this.Remito.ResumeLayout(false);
            this.TabIngresoRemito.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenVentaPendiente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenVentaPendiente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.TabConsultaRemito.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).EndInit();
            this.groupControl8.ResumeLayout(false);
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

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraTab.XtraTabControl Remito;
        private DevExpress.XtraTab.XtraTabPage TabIngresoRemito;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.DateTimePicker dpDesde;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraTab.XtraTabPage TabConsultaRemito;
        private DevExpress.XtraEditors.GroupControl groupControl8;
        private DevExpress.XtraEditors.SimpleButton btnAsignarCata;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCliente;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCliente;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraGrid.GridControl gridControlOrdenVentaPendiente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrdenVentaPendiente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnBuscarOrdenVentaPendiente;
        private System.Windows.Forms.CheckBox checkPeriodo;
    }
}