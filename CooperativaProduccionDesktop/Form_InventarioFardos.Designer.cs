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
            this.checkClase = new System.Windows.Forms.CheckBox();
            this.checkTabaco = new System.Windows.Forms.CheckBox();
            this.cbClase = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.btnReporte = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportarExcel = new DevExpress.XtraEditors.SimpleButton();
            this.checkDeposito = new System.Windows.Forms.CheckBox();
            this.cbDeposito = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFardos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFardos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.ribbon.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(1153, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControlFardos);
            this.groupControl2.Location = new System.Drawing.Point(3, 103);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1148, 492);
            this.groupControl2.TabIndex = 22;
            this.groupControl2.Text = "Lista de Fardos";
            // 
            // gridControlFardos
            // 
            this.gridControlFardos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFardos.Location = new System.Drawing.Point(2, 20);
            this.gridControlFardos.MainView = this.gridViewFardos;
            this.gridControlFardos.MenuManager = this.ribbon;
            this.gridControlFardos.Name = "gridControlFardos";
            this.gridControlFardos.Size = new System.Drawing.Size(1144, 470);
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
            this.groupControl1.Controls.Add(this.checkDeposito);
            this.groupControl1.Controls.Add(this.cbDeposito);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.checkClase);
            this.groupControl1.Controls.Add(this.checkTabaco);
            this.groupControl1.Controls.Add(this.cbClase);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.cbProducto);
            this.groupControl1.Controls.Add(this.btnBuscar);
            this.groupControl1.Controls.Add(this.dpHasta);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.dpDesde);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Location = new System.Drawing.Point(3, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1148, 49);
            this.groupControl1.TabIndex = 23;
            this.groupControl1.Text = "Buscar Productor";
            // 
            // checkClase
            // 
            this.checkClase.AutoSize = true;
            this.checkClase.Location = new System.Drawing.Point(865, 29);
            this.checkClase.Name = "checkClase";
            this.checkClase.Size = new System.Drawing.Size(15, 14);
            this.checkClase.TabIndex = 86;
            this.checkClase.UseVisualStyleBackColor = true;
            // 
            // checkTabaco
            // 
            this.checkTabaco.AutoSize = true;
            this.checkTabaco.Location = new System.Drawing.Point(1038, 29);
            this.checkTabaco.Name = "checkTabaco";
            this.checkTabaco.Size = new System.Drawing.Size(15, 14);
            this.checkTabaco.TabIndex = 85;
            this.checkTabaco.UseVisualStyleBackColor = true;
            this.checkTabaco.CheckedChanged += new System.EventHandler(this.checkTabaco_CheckedChanged);
            // 
            // cbClase
            // 
            this.cbClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClase.FormattingEnabled = true;
            this.cbClase.Location = new System.Drawing.Point(918, 24);
            this.cbClase.Name = "cbClase";
            this.cbClase.Size = new System.Drawing.Size(114, 21);
            this.cbClase.TabIndex = 84;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(882, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 83;
            this.label4.Text = "Clase";
            // 
            // cbProducto
            // 
            this.cbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(661, 24);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(194, 21);
            this.cbProducto.TabIndex = 82;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbTabaco_SelectedIndexChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(1060, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(82, 20);
            this.btnBuscar.TabIndex = 39;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(248, 25);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(80, 21);
            this.dpHasta.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(169, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "Fecha Hasta";
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(83, 25);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.Size = new System.Drawing.Size(80, 21);
            this.dpDesde.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(607, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 57;
            this.label2.Text = "Producto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 55;
            this.label3.Text = "Fecha Desde";
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
            this.groupControl5.Size = new System.Drawing.Size(1148, 32);
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
            // checkDeposito
            // 
            this.checkDeposito.AutoSize = true;
            this.checkDeposito.Location = new System.Drawing.Point(588, 28);
            this.checkDeposito.Name = "checkDeposito";
            this.checkDeposito.Size = new System.Drawing.Size(15, 14);
            this.checkDeposito.TabIndex = 89;
            this.checkDeposito.UseVisualStyleBackColor = true;
            // 
            // cbDeposito
            // 
            this.cbDeposito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeposito.FormattingEnabled = true;
            this.cbDeposito.Location = new System.Drawing.Point(388, 25);
            this.cbDeposito.Name = "cbDeposito";
            this.cbDeposito.Size = new System.Drawing.Size(194, 21);
            this.cbDeposito.TabIndex = 88;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(334, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 87;
            this.label5.Text = "Deposito";
            // 
            // Form_InventarioFardos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 633);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControlFardos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFardos;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dpDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.SimpleButton btnReporte;
        private DevExpress.XtraEditors.SimpleButton btnExportarExcel;
        private System.Windows.Forms.ComboBox cbClase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.CheckBox checkTabaco;
        private System.Windows.Forms.CheckBox checkClase;
        private System.Windows.Forms.CheckBox checkDeposito;
        private System.Windows.Forms.ComboBox cbDeposito;
        private System.Windows.Forms.Label label5;
    }
}