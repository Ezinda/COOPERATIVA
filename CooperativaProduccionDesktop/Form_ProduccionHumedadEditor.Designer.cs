namespace CooperativaProduccion
{
    partial class Form_ProduccionHumedadEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionHumedadEditor));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnBorrar = new DevExpress.XtraEditors.SimpleButton();
            this.gridViewHumedad = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlHumedad = new DevExpress.XtraGrid.GridControl();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.lblBlend = new System.Windows.Forms.Label();
            this.cbBlend = new System.Windows.Forms.ComboBox();
            this.lblCorrida = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHumedad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHumedad)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 1;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowCategoryInCaption = false;
            this.ribbonControl.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl.ShowToolbarCustomizeItem = false;
            this.ribbonControl.Size = new System.Drawing.Size(586, 27);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrar.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrar.Image")));
            this.btnBorrar.Location = new System.Drawing.Point(143, 434);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(125, 21);
            this.btnBorrar.TabIndex = 104;
            this.btnBorrar.Text = "Borrar registro";
            // 
            // gridViewHumedad
            // 
            this.gridViewHumedad.GridControl = this.gridControlHumedad;
            this.gridViewHumedad.Name = "gridViewHumedad";
            this.gridViewHumedad.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlHumedad
            // 
            this.gridControlHumedad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlHumedad.Location = new System.Drawing.Point(12, 142);
            this.gridControlHumedad.MainView = this.gridViewHumedad;
            this.gridControlHumedad.Name = "gridControlHumedad";
            this.gridControlHumedad.Size = new System.Drawing.Size(562, 286);
            this.gridControlHumedad.TabIndex = 101;
            this.gridControlHumedad.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewHumedad});
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.Location = new System.Drawing.Point(12, 434);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 21);
            this.btnAgregar.TabIndex = 103;
            this.btnAgregar.Text = "Agregar registro";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Location = new System.Drawing.Point(498, 442);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(76, 21);
            this.btnGrabar.TabIndex = 102;
            this.btnGrabar.Text = "Grabar";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(12, 41);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 16);
            this.lblFecha.TabIndex = 89;
            this.lblFecha.Text = "Fecha";
            // 
            // dateFecha
            // 
            this.dateFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFecha.Location = new System.Drawing.Point(58, 37);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(100, 21);
            this.dateFecha.TabIndex = 90;
            // 
            // lblBlend
            // 
            this.lblBlend.AutoSize = true;
            this.lblBlend.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlend.Location = new System.Drawing.Point(13, 65);
            this.lblBlend.Name = "lblBlend";
            this.lblBlend.Size = new System.Drawing.Size(39, 16);
            this.lblBlend.TabIndex = 91;
            this.lblBlend.Text = "Blend";
            // 
            // cbBlend
            // 
            this.cbBlend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlend.FormattingEnabled = true;
            this.cbBlend.Location = new System.Drawing.Point(58, 64);
            this.cbBlend.Name = "cbBlend";
            this.cbBlend.Size = new System.Drawing.Size(327, 21);
            this.cbBlend.TabIndex = 92;
            // 
            // lblCorrida
            // 
            this.lblCorrida.AutoSize = true;
            this.lblCorrida.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrida.Location = new System.Drawing.Point(13, 115);
            this.lblCorrida.Name = "lblCorrida";
            this.lblCorrida.Size = new System.Drawing.Size(51, 16);
            this.lblCorrida.TabIndex = 94;
            this.lblCorrida.Text = "Corrida:";
            // 
            // lblOrden
            // 
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.Location = new System.Drawing.Point(13, 90);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(124, 16);
            this.lblOrden.TabIndex = 93;
            this.lblOrden.Text = "Orden de Produccion:";
            // 
            // Form_ProduccionHumedadEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 478);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.gridControlHumedad);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateFecha);
            this.Controls.Add(this.lblBlend);
            this.Controls.Add(this.cbBlend);
            this.Controls.Add(this.lblCorrida);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProduccionHumedadEditor";
            this.Ribbon = this.ribbonControl;
            this.Text = "Editor de Control de Humedad";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHumedad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHumedad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraEditors.SimpleButton btnBorrar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHumedad;
        private DevExpress.XtraGrid.GridControl gridControlHumedad;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraEditors.SimpleButton btnGrabar;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Label lblBlend;
        private System.Windows.Forms.ComboBox cbBlend;
        private System.Windows.Forms.Label lblCorrida;
        private System.Windows.Forms.Label lblOrden;
    }
}