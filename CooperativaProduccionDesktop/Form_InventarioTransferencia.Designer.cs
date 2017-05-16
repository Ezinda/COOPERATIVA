namespace CooperativaProduccion
{
    partial class Form_InventarioTransferencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InventarioTransferencia));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Cata = new DevExpress.XtraTab.XtraTabControl();
            this.TabTransferenciaCaja = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.cbDepositoDestino = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTransferencia = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlCaja = new DevExpress.XtraGrid.GridControl();
            this.gridViewCaja = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.cbDepositoOrigen = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidadCaja = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbProductoIngreso = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.btnExportarExcel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cata)).BeginInit();
            this.Cata.SuspendLayout();
            this.TabTransferenciaCaja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowQatLocationSelector = false;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(1089, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // Cata
            // 
            this.Cata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cata.Location = new System.Drawing.Point(3, 54);
            this.Cata.Name = "Cata";
            this.Cata.SelectedTabPage = this.TabTransferenciaCaja;
            this.Cata.Size = new System.Drawing.Size(1088, 445);
            this.Cata.TabIndex = 77;
            this.Cata.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabTransferenciaCaja});
            // 
            // TabTransferenciaCaja
            // 
            this.TabTransferenciaCaja.Controls.Add(this.groupControl3);
            this.TabTransferenciaCaja.Controls.Add(this.groupControl5);
            this.TabTransferenciaCaja.Controls.Add(this.groupControl1);
            this.TabTransferenciaCaja.Image = ((System.Drawing.Image)(resources.GetObject("TabTransferenciaCaja.Image")));
            this.TabTransferenciaCaja.Name = "TabTransferenciaCaja";
            this.TabTransferenciaCaja.Size = new System.Drawing.Size(1082, 414);
            this.TabTransferenciaCaja.Text = "Transferencia de Cajas";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnExportarExcel);
            this.groupControl3.Controls.Add(this.cbDepositoDestino);
            this.groupControl3.Controls.Add(this.label4);
            this.groupControl3.Controls.Add(this.btnTransferencia);
            this.groupControl3.Location = new System.Drawing.Point(1, 380);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.ShowCaption = false;
            this.groupControl3.Size = new System.Drawing.Size(1080, 33);
            this.groupControl3.TabIndex = 77;
            this.groupControl3.Text = "Buscar Cata";
            // 
            // cbDepositoDestino
            // 
            this.cbDepositoDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepositoDestino.FormattingEnabled = true;
            this.cbDepositoDestino.Location = new System.Drawing.Point(103, 7);
            this.cbDepositoDestino.Name = "cbDepositoDestino";
            this.cbDepositoDestino.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDepositoDestino.Size = new System.Drawing.Size(177, 21);
            this.cbDepositoDestino.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 57;
            this.label4.Text = "Deposito Destino";
            // 
            // btnTransferencia
            // 
            this.btnTransferencia.Image = ((System.Drawing.Image)(resources.GetObject("btnTransferencia.Image")));
            this.btnTransferencia.Location = new System.Drawing.Point(286, 6);
            this.btnTransferencia.Name = "btnTransferencia";
            this.btnTransferencia.Size = new System.Drawing.Size(102, 22);
            this.btnTransferencia.TabIndex = 7;
            this.btnTransferencia.Text = "Transferencia";
            this.btnTransferencia.Click += new System.EventHandler(this.btnGenerarLote_Click);
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.gridControlCaja);
            this.groupControl5.Location = new System.Drawing.Point(1, 52);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(1082, 328);
            this.groupControl5.TabIndex = 76;
            this.groupControl5.Text = "Detalle de cajas";
            // 
            // gridControlCaja
            // 
            this.gridControlCaja.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCaja.Location = new System.Drawing.Point(2, 20);
            this.gridControlCaja.MainView = this.gridViewCaja;
            this.gridControlCaja.MenuManager = this.ribbon;
            this.gridControlCaja.Name = "gridControlCaja";
            this.gridControlCaja.Size = new System.Drawing.Size(1078, 306);
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
            this.gridViewCaja.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
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
            this.groupControl1.Controls.Add(this.dpHasta);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.btnBuscar);
            this.groupControl1.Controls.Add(this.cbDepositoOrigen);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtCantidadCaja);
            this.groupControl1.Controls.Add(this.label10);
            this.groupControl1.Controls.Add(this.cbProductoIngreso);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.dpDesde);
            this.groupControl1.Controls.Add(this.label15);
            this.groupControl1.Location = new System.Drawing.Point(1, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1080, 50);
            this.groupControl1.TabIndex = 72;
            this.groupControl1.Text = "Filtros de búsquedas";
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(169, 23);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(77, 21);
            this.dpHasta.TabIndex = 92;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 93;
            this.label2.Text = "Hasta";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(974, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(91, 22);
            this.btnBuscar.TabIndex = 91;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbDepositoOrigen
            // 
            this.cbDepositoOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepositoOrigen.FormattingEnabled = true;
            this.cbDepositoOrigen.Location = new System.Drawing.Point(590, 23);
            this.cbDepositoOrigen.Name = "cbDepositoOrigen";
            this.cbDepositoOrigen.Size = new System.Drawing.Size(178, 21);
            this.cbDepositoOrigen.TabIndex = 89;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(499, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 90;
            this.label1.Text = "Deposito Origen";
            // 
            // txtCantidadCaja
            // 
            this.txtCantidadCaja.Location = new System.Drawing.Point(876, 24);
            this.txtCantidadCaja.Name = "txtCantidadCaja";
            this.txtCantidadCaja.Size = new System.Drawing.Size(60, 21);
            this.txtCantidadCaja.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(774, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 88;
            this.label10.Text = "Cantidad de Cajas";
            // 
            // cbProductoIngreso
            // 
            this.cbProductoIngreso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductoIngreso.FormattingEnabled = true;
            this.cbProductoIngreso.Location = new System.Drawing.Point(309, 23);
            this.cbProductoIngreso.Name = "cbProductoIngreso";
            this.cbProductoIngreso.Size = new System.Drawing.Size(178, 21);
            this.cbProductoIngreso.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(251, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 78;
            this.label5.Text = "Producto";
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(43, 24);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.Size = new System.Drawing.Size(77, 21);
            this.dpDesde.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 16);
            this.label15.TabIndex = 76;
            this.label15.Text = "Desde";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.Location = new System.Drawing.Point(394, 6);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(102, 22);
            this.btnExportarExcel.TabIndex = 63;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // Form_InventarioTransferencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 511);
            this.Controls.Add(this.Cata);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_InventarioTransferencia";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario - Transferencia de Mercadería";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cata)).EndInit();
            this.Cata.ResumeLayout(false);
            this.TabTransferenciaCaja.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraTab.XtraTabControl Cata;
        private DevExpress.XtraTab.XtraTabPage TabTransferenciaCaja;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraGrid.GridControl gridControlCaja;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCaja;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox txtCantidadCaja;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.SimpleButton btnTransferencia;
        private System.Windows.Forms.ComboBox cbProductoIngreso;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dpDesde;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbDepositoOrigen;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.ComboBox cbDepositoDestino;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnExportarExcel;
    }
}