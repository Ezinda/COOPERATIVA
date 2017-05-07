namespace CooperativaProduccion
{
    partial class Form_AdministracionGestionCata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionGestionCata));
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlCata = new DevExpress.XtraGrid.GridControl();
            this.gridViewCata = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Cata = new DevExpress.XtraTab.XtraTabControl();
            this.TabImportacionCata = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancelarImportacion = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportarCata = new DevExpress.XtraEditors.SimpleButton();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.progressCata = new System.Windows.Forms.ProgressBar();
            this.TabConsultaCata = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnExportarVinculacion = new DevExpress.XtraEditors.SimpleButton();
            this.txtDisponibles = new System.Windows.Forms.TextBox();
            this.txtUtilizados = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirAfip = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarLiquidacion = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarCata = new DevExpress.XtraEditors.SimpleButton();
            this.checkTodos = new System.Windows.Forms.CheckBox();
            this.cbLote = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.opd = new System.Windows.Forms.OpenFileDialog();
            this.btnExportarExcel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cata)).BeginInit();
            this.Cata.SuspendLayout();
            this.TabImportacionCata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.TabConsultaCata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
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
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlCata;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlCata
            // 
            this.gridControlCata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCata.Location = new System.Drawing.Point(2, 20);
            this.gridControlCata.MainView = this.gridViewCata;
            this.gridControlCata.MenuManager = this.ribbon;
            this.gridControlCata.Name = "gridControlCata";
            this.gridControlCata.Size = new System.Drawing.Size(740, 359);
            this.gridControlCata.TabIndex = 68;
            this.gridControlCata.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCata,
            this.gridViewLiquidacionDetalle});
            // 
            // gridViewCata
            // 
            this.gridViewCata.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewCata.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCata.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewCata.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewCata.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewCata.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCata.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewCata.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewCata.GridControl = this.gridControlCata;
            this.gridViewCata.Name = "gridViewCata";
            this.gridViewCata.OptionsBehavior.Editable = false;
            this.gridViewCata.OptionsSelection.MultiSelect = true;
            this.gridViewCata.OptionsView.ShowGroupPanel = false;
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
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(754, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // Cata
            // 
            this.Cata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cata.Location = new System.Drawing.Point(4, 55);
            this.Cata.Name = "Cata";
            this.Cata.SelectedTabPage = this.TabImportacionCata;
            this.Cata.Size = new System.Drawing.Size(750, 503);
            this.Cata.TabIndex = 74;
            this.Cata.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabImportacionCata,
            this.TabConsultaCata});
            this.Cata.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.Cata_SelectedPageChanged);
            // 
            // TabImportacionCata
            // 
            this.TabImportacionCata.Controls.Add(this.groupControl1);
            this.TabImportacionCata.Image = ((System.Drawing.Image)(resources.GetObject("TabImportacionCata.Image")));
            this.TabImportacionCata.Name = "TabImportacionCata";
            this.TabImportacionCata.Size = new System.Drawing.Size(744, 472);
            this.TabImportacionCata.Text = "Importacion de Cata";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.progressCata);
            this.groupControl1.Controls.Add(this.btnCancelarImportacion);
            this.groupControl1.Controls.Add(this.btnImportarCata);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Location = new System.Drawing.Point(1, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(742, 469);
            this.groupControl1.TabIndex = 72;
            this.groupControl1.Text = "Lista de Cata";
            // 
            // btnCancelarImportacion
            // 
            this.btnCancelarImportacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarImportacion.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarImportacion.Image")));
            this.btnCancelarImportacion.Location = new System.Drawing.Point(605, 24);
            this.btnCancelarImportacion.Name = "btnCancelarImportacion";
            this.btnCancelarImportacion.Size = new System.Drawing.Size(129, 29);
            this.btnCancelarImportacion.TabIndex = 75;
            this.btnCancelarImportacion.Text = "Cancelar Importación";
            this.btnCancelarImportacion.Click += new System.EventHandler(this.btnCancelarImportacion_Click);
            // 
            // btnImportarCata
            // 
            this.btnImportarCata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportarCata.Image = ((System.Drawing.Image)(resources.GetObject("btnImportarCata.Image")));
            this.btnImportarCata.Location = new System.Drawing.Point(495, 24);
            this.btnImportarCata.Name = "btnImportarCata";
            this.btnImportarCata.Size = new System.Drawing.Size(104, 29);
            this.btnImportarCata.TabIndex = 39;
            this.btnImportarCata.Text = "Importar Cata";
            this.btnImportarCata.Click += new System.EventHandler(this.btnImportarCata_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.Location = new System.Drawing.Point(2, 59);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(738, 410);
            this.txtDescripcion.TabIndex = 74;
            this.txtDescripcion.Text = "";
            // 
            // progressCata
            // 
            this.progressCata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressCata.Location = new System.Drawing.Point(2, 24);
            this.progressCata.Maximum = 10000000;
            this.progressCata.Name = "progressCata";
            this.progressCata.Size = new System.Drawing.Size(487, 29);
            this.progressCata.TabIndex = 73;
            // 
            // TabConsultaCata
            // 
            this.TabConsultaCata.Controls.Add(this.groupControl3);
            this.TabConsultaCata.Controls.Add(this.groupControl7);
            this.TabConsultaCata.Controls.Add(this.groupControl6);
            this.TabConsultaCata.Controls.Add(this.groupControl4);
            this.TabConsultaCata.Controls.Add(this.groupControl2);
            this.TabConsultaCata.Image = ((System.Drawing.Image)(resources.GetObject("TabConsultaCata.Image")));
            this.TabConsultaCata.Name = "TabConsultaCata";
            this.TabConsultaCata.Size = new System.Drawing.Size(744, 472);
            this.TabConsultaCata.Text = "Consulta de Cata";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnExportarExcel);
            this.groupControl3.Controls.Add(this.btnExportarVinculacion);
            this.groupControl3.Controls.Add(this.txtDisponibles);
            this.groupControl3.Controls.Add(this.txtUtilizados);
            this.groupControl3.Controls.Add(this.txtTotal);
            this.groupControl3.Controls.Add(this.label4);
            this.groupControl3.Controls.Add(this.label2);
            this.groupControl3.Controls.Add(this.label1);
            this.groupControl3.Location = new System.Drawing.Point(1, 437);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.ShowCaption = false;
            this.groupControl3.Size = new System.Drawing.Size(742, 34);
            this.groupControl3.TabIndex = 77;
            this.groupControl3.Text = "Buscar Cata";
            // 
            // btnExportarVinculacion
            // 
            this.btnExportarVinculacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarVinculacion.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarVinculacion.Image")));
            this.btnExportarVinculacion.Location = new System.Drawing.Point(610, 5);
            this.btnExportarVinculacion.Name = "btnExportarVinculacion";
            this.btnExportarVinculacion.Size = new System.Drawing.Size(127, 22);
            this.btnExportarVinculacion.TabIndex = 61;
            this.btnExportarVinculacion.Text = "Exportar Vinculación";
            this.btnExportarVinculacion.Click += new System.EventHandler(this.btnExportarVinculacion_Click);
            // 
            // txtDisponibles
            // 
            this.txtDisponibles.Enabled = false;
            this.txtDisponibles.Location = new System.Drawing.Point(336, 5);
            this.txtDisponibles.Name = "txtDisponibles";
            this.txtDisponibles.Size = new System.Drawing.Size(69, 21);
            this.txtDisponibles.TabIndex = 62;
            // 
            // txtUtilizados
            // 
            this.txtUtilizados.Enabled = false;
            this.txtUtilizados.Location = new System.Drawing.Point(192, 5);
            this.txtUtilizados.Name = "txtUtilizados";
            this.txtUtilizados.Size = new System.Drawing.Size(61, 21);
            this.txtUtilizados.TabIndex = 61;
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(46, 5);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(76, 21);
            this.txtTotal.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(259, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 59;
            this.label4.Text = "Disponibles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(126, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 58;
            this.label2.Text = "Utilizados";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 57;
            this.label1.Text = "Total";
            // 
            // groupControl7
            // 
            this.groupControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl7.Controls.Add(this.btnPrevisualizar);
            this.groupControl7.Controls.Add(this.btnSubirAfip);
            this.groupControl7.Location = new System.Drawing.Point(1, 612);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1281, 33);
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
            this.groupControl6.Controls.Add(this.gridControlCata);
            this.groupControl6.Location = new System.Drawing.Point(-1, 55);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(744, 381);
            this.groupControl6.TabIndex = 75;
            this.groupControl6.Text = "Detalle de Catas";
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscarLiquidacion);
            this.groupControl4.Location = new System.Drawing.Point(1196, 1);
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
            this.groupControl2.Controls.Add(this.btnBuscarCata);
            this.groupControl2.Controls.Add(this.checkTodos);
            this.groupControl2.Controls.Add(this.cbLote);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(1, 1);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(742, 53);
            this.groupControl2.TabIndex = 72;
            this.groupControl2.Text = "Buscar Cata";
            // 
            // btnBuscarCata
            // 
            this.btnBuscarCata.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCata.Image")));
            this.btnBuscarCata.Location = new System.Drawing.Point(281, 23);
            this.btnBuscarCata.Name = "btnBuscarCata";
            this.btnBuscarCata.Size = new System.Drawing.Size(76, 26);
            this.btnBuscarCata.TabIndex = 60;
            this.btnBuscarCata.Text = "Buscar";
            this.btnBuscarCata.Click += new System.EventHandler(this.btnBuscarCata_Click);
            // 
            // checkTodos
            // 
            this.checkTodos.AutoSize = true;
            this.checkTodos.Location = new System.Drawing.Point(225, 28);
            this.checkTodos.Name = "checkTodos";
            this.checkTodos.Size = new System.Drawing.Size(55, 17);
            this.checkTodos.TabIndex = 59;
            this.checkTodos.Text = "Todos";
            this.checkTodos.UseVisualStyleBackColor = true;
            // 
            // cbLote
            // 
            this.cbLote.FormattingEnabled = true;
            this.cbLote.Location = new System.Drawing.Point(56, 25);
            this.cbLote.Name = "cbLote";
            this.cbLote.Size = new System.Drawing.Size(163, 21);
            this.cbLote.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "N° Lote";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // opd
            // 
            this.opd.Multiselect = true;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.Location = new System.Drawing.Point(499, 5);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(105, 22);
            this.btnExportarExcel.TabIndex = 63;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // Form_AdministracionGestionCata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 590);
            this.Controls.Add(this.Cata);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionGestionCata";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración - Gestión de CATA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_AdministracionGestionCata_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cata)).EndInit();
            this.Cata.ResumeLayout(false);
            this.TabImportacionCata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.TabConsultaCata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
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
        private DevExpress.XtraTab.XtraTabControl Cata;
        private DevExpress.XtraTab.XtraTabPage TabImportacionCata;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnImportarCata;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.ProgressBar progressCata;
        private DevExpress.XtraTab.XtraTabPage TabConsultaCata;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private DevExpress.XtraEditors.SimpleButton btnPrevisualizar;
        private DevExpress.XtraEditors.SimpleButton btnSubirAfip;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlCata;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalle;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCata;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnBuscarLiquidacion;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.OpenFileDialog opd;
        private DevExpress.XtraEditors.SimpleButton btnCancelarImportacion;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCata;
        private System.Windows.Forms.CheckBox checkTodos;
        private System.Windows.Forms.ComboBox cbLote;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TextBox txtDisponibles;
        private System.Windows.Forms.TextBox txtUtilizados;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnExportarVinculacion;
        private DevExpress.XtraEditors.SimpleButton btnExportarExcel;
    }
}