namespace CooperativaProduccion
{
    partial class Form_InventarioFardos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InventarioFardos));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlFardos = new DevExpress.XtraGrid.GridControl();
            this.gridViewFardos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtNumRomaneo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkPeriodo = new System.Windows.Forms.CheckBox();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtNumFardo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarProductor = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscarFet = new DevExpress.XtraEditors.SimpleButton();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFet = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.btnReporte = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportarExcel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFardos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFardos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
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
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowCategoryInCaption = false;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(948, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControlFardos);
            this.groupControl2.Location = new System.Drawing.Point(3, 135);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(945, 460);
            this.groupControl2.TabIndex = 22;
            this.groupControl2.Text = "Lista de Fardos";
            // 
            // gridControlFardos
            // 
            this.gridControlFardos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFardos.Location = new System.Drawing.Point(2, 21);
            this.gridControlFardos.MainView = this.gridViewFardos;
            this.gridControlFardos.MenuManager = this.ribbon;
            this.gridControlFardos.Name = "gridControlFardos";
            this.gridControlFardos.Size = new System.Drawing.Size(941, 437);
            this.gridControlFardos.TabIndex = 0;
            this.gridControlFardos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFardos});
            // 
            // gridViewFardos
            // 
            this.gridViewFardos.GridControl = this.gridControlFardos;
            this.gridViewFardos.Name = "gridViewFardos";
            this.gridViewFardos.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewFardos.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewFardos.OptionsBehavior.Editable = false;
            this.gridViewFardos.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Nothing;
            this.gridViewFardos.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.txtNumRomaneo);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.checkPeriodo);
            this.groupControl1.Controls.Add(this.dpHasta);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.dpDesde);
            this.groupControl1.Controls.Add(this.txtNumFardo);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Location = new System.Drawing.Point(2, 33);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(812, 49);
            this.groupControl1.TabIndex = 23;
            this.groupControl1.Text = "Buscar Productor";
            // 
            // txtNumRomaneo
            // 
            this.txtNumRomaneo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumRomaneo.Location = new System.Drawing.Point(692, 24);
            this.txtNumRomaneo.Name = "txtNumRomaneo";
            this.txtNumRomaneo.Size = new System.Drawing.Size(110, 22);
            this.txtNumRomaneo.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(586, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 16);
            this.label4.TabIndex = 62;
            this.label4.Text = "Número Romaneo:";
            // 
            // checkPeriodo
            // 
            this.checkPeriodo.AutoSize = true;
            this.checkPeriodo.Location = new System.Drawing.Point(370, 29);
            this.checkPeriodo.Name = "checkPeriodo";
            this.checkPeriodo.Size = new System.Drawing.Size(15, 14);
            this.checkPeriodo.TabIndex = 1;
            this.checkPeriodo.UseVisualStyleBackColor = true;
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(262, 25);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(102, 21);
            this.dpHasta.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(190, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "Fecha Hasta:";
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(83, 25);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.Size = new System.Drawing.Size(102, 21);
            this.dpDesde.TabIndex = 59;
            // 
            // txtNumFardo
            // 
            this.txtNumFardo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumFardo.Location = new System.Drawing.Point(480, 24);
            this.txtNumFardo.Name = "txtNumFardo";
            this.txtNumFardo.Size = new System.Drawing.Size(102, 22);
            this.txtNumFardo.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(393, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 57;
            this.label2.Text = "Número Fardo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 55;
            this.label3.Text = "Fecha Desde:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(8, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(106, 35);
            this.btnBuscar.TabIndex = 39;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnBuscarProductor);
            this.groupControl3.Controls.Add(this.btnBuscarFet);
            this.groupControl3.Controls.Add(this.txtCuit);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Controls.Add(this.txtNombre);
            this.groupControl3.Controls.Add(this.label7);
            this.groupControl3.Controls.Add(this.txtFet);
            this.groupControl3.Controls.Add(this.label8);
            this.groupControl3.Location = new System.Drawing.Point(2, 85);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(812, 47);
            this.groupControl3.TabIndex = 24;
            this.groupControl3.Text = "Nuevo Preingreso";
            // 
            // btnBuscarProductor
            // 
            this.btnBuscarProductor.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProductor.Image")));
            this.btnBuscarProductor.Location = new System.Drawing.Point(557, 22);
            this.btnBuscarProductor.Name = "btnBuscarProductor";
            this.btnBuscarProductor.Size = new System.Drawing.Size(25, 22);
            this.btnBuscarProductor.TabIndex = 62;
            this.btnBuscarProductor.Click += new System.EventHandler(this.btnBuscarProductor_Click);
            // 
            // btnBuscarFet
            // 
            this.btnBuscarFet.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarFet.Image")));
            this.btnBuscarFet.Location = new System.Drawing.Point(150, 22);
            this.btnBuscarFet.Name = "btnBuscarFet";
            this.btnBuscarFet.Size = new System.Drawing.Size(25, 22);
            this.btnBuscarFet.TabIndex = 61;
            this.btnBuscarFet.Click += new System.EventHandler(this.btnBuscarFet_Click);
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCuit.Location = new System.Drawing.Point(643, 22);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(159, 22);
            this.txtCuit.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(590, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 59;
            this.label6.Text = "C.U.I.T:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNombre.Location = new System.Drawing.Point(245, 22);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(313, 22);
            this.txtNombre.TabIndex = 58;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(181, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 57;
            this.label7.Text = "Productor:";
            // 
            // txtFet
            // 
            this.txtFet.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtFet.Location = new System.Drawing.Point(60, 22);
            this.txtFet.Name = "txtFet";
            this.txtFet.Size = new System.Drawing.Size(92, 22);
            this.txtFet.TabIndex = 56;
            this.txtFet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFet_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 55;
            this.label8.Text = "N° FET:";
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscar);
            this.groupControl4.Location = new System.Drawing.Point(820, 33);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(123, 99);
            this.groupControl4.TabIndex = 40;
            this.groupControl4.Text = "Opciones";
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.btnReporte);
            this.groupControl5.Controls.Add(this.btnExportarExcel);
            this.groupControl5.Location = new System.Drawing.Point(3, 599);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.ShowCaption = false;
            this.groupControl5.Size = new System.Drawing.Size(943, 32);
            this.groupControl5.TabIndex = 41;
            this.groupControl5.Text = "Nuevo Preingreso";
            // 
            // btnReporte
            // 
            this.btnReporte.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.Appearance.Options.UseFont = true;
            this.btnReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnReporte.Image")));
            this.btnReporte.Location = new System.Drawing.Point(5, 5);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(106, 22);
            this.btnReporte.TabIndex = 43;
            this.btnReporte.Text = "Reporte";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.Appearance.Options.UseFont = true;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.Location = new System.Drawing.Point(117, 5);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(106, 22);
            this.btnExportarExcel.TabIndex = 42;
            this.btnExportarExcel.Text = "Exportar Excel";
            // 
            // Form_InventarioFardos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 633);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Name = "Form_InventarioFardos";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario - Administración de Fardos";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFardos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFardos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControlFardos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFardos;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.CheckBox checkPeriodo;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dpDesde;
        public System.Windows.Forms.TextBox txtNumFardo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        public System.Windows.Forms.TextBox txtNumRomaneo;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        public System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtFet;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.SimpleButton btnReporte;
        private DevExpress.XtraEditors.SimpleButton btnExportarExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscarProductor;
        private DevExpress.XtraEditors.SimpleButton btnBuscarFet;
    }
}