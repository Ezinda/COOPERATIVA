namespace CooperativaProduccion
{
    partial class Form_ProduccionTemperaturaEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionTemperaturaEditor));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.lblBlend = new System.Windows.Forms.Label();
            this.cbBlend = new System.Windows.Forms.ComboBox();
            this.lblCorrida = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMinimo = new DevExpress.XtraEditors.TextEdit();
            this.txtMeta = new DevExpress.XtraEditors.TextEdit();
            this.txtMaximo = new DevExpress.XtraEditors.TextEdit();
            this.btnGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlTemperatura = new DevExpress.XtraGrid.GridControl();
            this.gridViewTemperatura = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.btnBorrar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTemperatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTemperatura)).BeginInit();
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
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(12, 44);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 16);
            this.lblFecha.TabIndex = 72;
            this.lblFecha.Text = "Fecha";
            // 
            // dateFecha
            // 
            this.dateFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFecha.Location = new System.Drawing.Point(58, 40);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(100, 21);
            this.dateFecha.TabIndex = 73;
            // 
            // lblBlend
            // 
            this.lblBlend.AutoSize = true;
            this.lblBlend.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlend.Location = new System.Drawing.Point(13, 68);
            this.lblBlend.Name = "lblBlend";
            this.lblBlend.Size = new System.Drawing.Size(39, 16);
            this.lblBlend.TabIndex = 74;
            this.lblBlend.Text = "Blend";
            // 
            // cbBlend
            // 
            this.cbBlend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlend.FormattingEnabled = true;
            this.cbBlend.Location = new System.Drawing.Point(58, 67);
            this.cbBlend.Name = "cbBlend";
            this.cbBlend.Size = new System.Drawing.Size(327, 21);
            this.cbBlend.TabIndex = 75;
            // 
            // lblCorrida
            // 
            this.lblCorrida.AutoSize = true;
            this.lblCorrida.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrida.Location = new System.Drawing.Point(13, 118);
            this.lblCorrida.Name = "lblCorrida";
            this.lblCorrida.Size = new System.Drawing.Size(51, 16);
            this.lblCorrida.TabIndex = 77;
            this.lblCorrida.Text = "Corrida:";
            // 
            // lblOrden
            // 
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.Location = new System.Drawing.Point(13, 93);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(124, 16);
            this.lblOrden.TabIndex = 76;
            this.lblOrden.Text = "Orden de Produccion:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(418, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 78;
            this.label1.Text = "Mínimo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(431, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 79;
            this.label2.Text = "Meta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(416, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 80;
            this.label3.Text = "Máximo";
            // 
            // txtMinimo
            // 
            this.txtMinimo.Location = new System.Drawing.Point(470, 67);
            this.txtMinimo.MenuManager = this.ribbonControl;
            this.txtMinimo.Name = "txtMinimo";
            this.txtMinimo.Size = new System.Drawing.Size(104, 20);
            this.txtMinimo.TabIndex = 81;
            // 
            // txtMeta
            // 
            this.txtMeta.Location = new System.Drawing.Point(470, 93);
            this.txtMeta.MenuManager = this.ribbonControl;
            this.txtMeta.Name = "txtMeta";
            this.txtMeta.Size = new System.Drawing.Size(104, 20);
            this.txtMeta.TabIndex = 82;
            // 
            // txtMaximo
            // 
            this.txtMaximo.Location = new System.Drawing.Point(470, 119);
            this.txtMaximo.MenuManager = this.ribbonControl;
            this.txtMaximo.Name = "txtMaximo";
            this.txtMaximo.Size = new System.Drawing.Size(104, 20);
            this.txtMaximo.TabIndex = 83;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Location = new System.Drawing.Point(498, 445);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(76, 21);
            this.btnGrabar.TabIndex = 85;
            this.btnGrabar.Text = "Grabar";
            // 
            // gridControlTemperatura
            // 
            this.gridControlTemperatura.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlTemperatura.Location = new System.Drawing.Point(12, 145);
            this.gridControlTemperatura.MainView = this.gridViewTemperatura;
            this.gridControlTemperatura.Name = "gridControlTemperatura";
            this.gridControlTemperatura.Size = new System.Drawing.Size(562, 286);
            this.gridControlTemperatura.TabIndex = 84;
            this.gridControlTemperatura.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTemperatura});
            // 
            // gridViewTemperatura
            // 
            this.gridViewTemperatura.GridControl = this.gridControlTemperatura;
            this.gridViewTemperatura.Name = "gridViewTemperatura";
            this.gridViewTemperatura.OptionsView.ShowGroupPanel = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.Location = new System.Drawing.Point(12, 437);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 21);
            this.btnAgregar.TabIndex = 87;
            this.btnAgregar.Text = "Agregar registro";
            // 
            // btnBorrar
            // 
            this.btnBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrar.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrar.Image")));
            this.btnBorrar.Location = new System.Drawing.Point(143, 437);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(125, 21);
            this.btnBorrar.TabIndex = 88;
            this.btnBorrar.Text = "Borrar registro";
            // 
            // Form_ProduccionTemperaturaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 478);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.gridControlTemperatura);
            this.Controls.Add(this.txtMaximo);
            this.Controls.Add(this.txtMeta);
            this.Controls.Add(this.txtMinimo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateFecha);
            this.Controls.Add(this.lblBlend);
            this.Controls.Add(this.cbBlend);
            this.Controls.Add(this.lblCorrida);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProduccionTemperaturaEditor";
            this.Ribbon = this.ribbonControl;
            this.Text = "Editor de Control de Temperatura";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTemperatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTemperatura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Label lblBlend;
        private System.Windows.Forms.ComboBox cbBlend;
        private System.Windows.Forms.Label lblCorrida;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtMinimo;
        private DevExpress.XtraEditors.TextEdit txtMeta;
        private DevExpress.XtraEditors.TextEdit txtMaximo;
        private DevExpress.XtraEditors.SimpleButton btnGrabar;
        private DevExpress.XtraGrid.GridControl gridControlTemperatura;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTemperatura;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraEditors.SimpleButton btnBorrar;
    }
}