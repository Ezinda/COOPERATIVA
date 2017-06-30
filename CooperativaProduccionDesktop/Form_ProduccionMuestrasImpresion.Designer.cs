namespace CooperativaProduccion
{
    partial class Form_ProduccionMuestrasImpresion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionMuestrasImpresion));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dateDesde = new System.Windows.Forms.DateTimePicker();
            this.cbBlend = new System.Windows.Forms.ComboBox();
            this.lblBlend = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dateHasta = new System.Windows.Forms.DateTimePicker();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
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
            this.ribbonControl.ShowQatLocationSelector = false;
            this.ribbonControl.ShowToolbarCustomizeItem = false;
            this.ribbonControl.Size = new System.Drawing.Size(338, 27);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(11, 84);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 16);
            this.lblDesde.TabIndex = 68;
            this.lblDesde.Text = "Desde";
            // 
            // dateDesde
            // 
            this.dateDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDesde.Location = new System.Drawing.Point(57, 82);
            this.dateDesde.Name = "dateDesde";
            this.dateDesde.Size = new System.Drawing.Size(100, 21);
            this.dateDesde.TabIndex = 69;
            // 
            // cbBlend
            // 
            this.cbBlend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBlend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlend.FormattingEnabled = true;
            this.cbBlend.Location = new System.Drawing.Point(57, 39);
            this.cbBlend.Name = "cbBlend";
            this.cbBlend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbBlend.Size = new System.Drawing.Size(267, 21);
            this.cbBlend.TabIndex = 67;
            // 
            // lblBlend
            // 
            this.lblBlend.AutoSize = true;
            this.lblBlend.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlend.Location = new System.Drawing.Point(12, 41);
            this.lblBlend.Name = "lblBlend";
            this.lblBlend.Size = new System.Drawing.Size(39, 16);
            this.lblBlend.TabIndex = 66;
            this.lblBlend.Text = "Blend";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(180, 84);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 16);
            this.lblHasta.TabIndex = 70;
            this.lblHasta.Text = "Hasta";
            // 
            // dateHasta
            // 
            this.dateHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateHasta.Location = new System.Drawing.Point(224, 80);
            this.dateHasta.Name = "dateHasta";
            this.dateHasta.Size = new System.Drawing.Size(100, 21);
            this.dateHasta.TabIndex = 71;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Location = new System.Drawing.Point(210, 136);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(116, 22);
            this.btnImprimir.TabIndex = 72;
            this.btnImprimir.Text = "Generar reporte";
            // 
            // Form_ProduccionMuestrasImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 170);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dateHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dateDesde);
            this.Controls.Add(this.cbBlend);
            this.Controls.Add(this.lblBlend);
            this.Controls.Add(this.ribbonControl);
            this.Name = "Form_ProduccionMuestrasImpresion";
            this.Ribbon = this.ribbonControl;
            this.Text = "Producción: Reporte de Muestras";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dateDesde;
        private System.Windows.Forms.ComboBox cbBlend;
        private System.Windows.Forms.Label lblBlend;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dateHasta;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
    }
}