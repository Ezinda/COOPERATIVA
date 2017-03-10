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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InventarioFardos));
            this.gridViewInventarioDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlInventario = new DevExpress.XtraGrid.GridControl();
            this.gridViewInventario = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkDeposito = new System.Windows.Forms.CheckBox();
            this.cbDeposito = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkClase = new System.Windows.Forms.CheckBox();
            this.checkTabaco = new System.Windows.Forms.CheckBox();
            this.cbClase = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.btnReporte = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportarExcel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInventarioDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInventario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInventario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridViewInventarioDetalle
            // 
            this.gridViewInventarioDetalle.Appearance.Preview.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewInventarioDetalle.Appearance.Preview.Options.UseBackColor = true;
            this.gridViewInventarioDetalle.Appearance.Row.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewInventarioDetalle.Appearance.Row.Options.UseBackColor = true;
            this.gridViewInventarioDetalle.AppearancePrint.Row.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewInventarioDetalle.AppearancePrint.Row.Options.UseBackColor = true;
            this.gridViewInventarioDetalle.GridControl = this.gridControlInventario;
            this.gridViewInventarioDetalle.Name = "gridViewInventarioDetalle";
            this.gridViewInventarioDetalle.OptionsView.ColumnAutoWidth = false;
            this.gridViewInventarioDetalle.OptionsView.ShowGroupPanel = false;
            this.gridViewInventarioDetalle.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewInventarioDetalle_RowStyle);
            this.gridViewInventarioDetalle.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.gridViewInventarioDetalle_CalcRowHeight);
            // 
            // gridControlInventario
            // 
            this.gridControlInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gridViewInventarioDetalle;
            gridLevelNode1.RelationName = "Level1";
            this.gridControlInventario.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControlInventario.Location = new System.Drawing.Point(2, 20);
            this.gridControlInventario.MainView = this.gridViewInventario;
            this.gridControlInventario.MenuManager = this.ribbon;
            this.gridControlInventario.Name = "gridControlInventario";
            this.gridControlInventario.Size = new System.Drawing.Size(864, 470);
            this.gridControlInventario.TabIndex = 0;
            this.gridControlInventario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInventario,
            this.gridViewInventarioDetalle});
            // 
            // gridViewInventario
            // 
            this.gridViewInventario.GridControl = this.gridControlInventario;
            this.gridViewInventario.Name = "gridViewInventario";
            this.gridViewInventario.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewInventario.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewInventario.OptionsBehavior.Editable = false;
            this.gridViewInventario.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Nothing;
            this.gridViewInventario.OptionsView.ColumnAutoWidth = false;
            this.gridViewInventario.OptionsView.ShowGroupPanel = false;
            this.gridViewInventario.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewInventario_RowStyle);
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
            this.ribbon.Size = new System.Drawing.Size(873, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControlInventario);
            this.groupControl2.Location = new System.Drawing.Point(3, 103);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(868, 492);
            this.groupControl2.TabIndex = 22;
            this.groupControl2.Text = "Lista de Fardos";
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
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Location = new System.Drawing.Point(3, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(868, 49);
            this.groupControl1.TabIndex = 23;
            this.groupControl1.Text = "Buscar Productor";
            // 
            // checkDeposito
            // 
            this.checkDeposito.AutoSize = true;
            this.checkDeposito.Location = new System.Drawing.Point(358, 28);
            this.checkDeposito.Name = "checkDeposito";
            this.checkDeposito.Size = new System.Drawing.Size(15, 14);
            this.checkDeposito.TabIndex = 89;
            this.checkDeposito.UseVisualStyleBackColor = true;
            // 
            // cbDeposito
            // 
            this.cbDeposito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeposito.FormattingEnabled = true;
            this.cbDeposito.Location = new System.Drawing.Point(210, 23);
            this.cbDeposito.Name = "cbDeposito";
            this.cbDeposito.Size = new System.Drawing.Size(145, 21);
            this.cbDeposito.TabIndex = 88;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(156, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 87;
            this.label5.Text = "Deposito";
            // 
            // checkClase
            // 
            this.checkClase.AutoSize = true;
            this.checkClase.Location = new System.Drawing.Point(750, 28);
            this.checkClase.Name = "checkClase";
            this.checkClase.Size = new System.Drawing.Size(15, 14);
            this.checkClase.TabIndex = 86;
            this.checkClase.UseVisualStyleBackColor = true;
            // 
            // checkTabaco
            // 
            this.checkTabaco.AutoSize = true;
            this.checkTabaco.Location = new System.Drawing.Point(604, 28);
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
            this.cbClase.Location = new System.Drawing.Point(662, 23);
            this.cbClase.Name = "cbClase";
            this.cbClase.Size = new System.Drawing.Size(82, 21);
            this.cbClase.TabIndex = 84;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(625, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 83;
            this.label4.Text = "Clase";
            // 
            // cbProducto
            // 
            this.cbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(433, 24);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(165, 21);
            this.cbProducto.TabIndex = 82;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbTabaco_SelectedIndexChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(782, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(82, 20);
            this.btnBuscar.TabIndex = 39;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(76, 23);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(76, 21);
            this.dpHasta.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "Fecha Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(379, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 57;
            this.label2.Text = "Producto";
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
            this.groupControl5.Size = new System.Drawing.Size(868, 32);
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
            this.ClientSize = new System.Drawing.Size(873, 633);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_InventarioFardos";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario - Administración de Fardos";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInventarioDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInventario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInventario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
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
        private DevExpress.XtraGrid.GridControl gridControlInventario;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInventario;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
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
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInventarioDetalle;
    }
}