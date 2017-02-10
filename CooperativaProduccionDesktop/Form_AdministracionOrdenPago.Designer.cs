namespace CooperativaProduccion
{
    partial class Form_AdministracionOrdenPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionOrdenPago));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Pago = new DevExpress.XtraTab.XtraTabControl();
            this.TabGeneracionOrdenPago = new DevExpress.XtraTab.XtraTabPage();
            this.administracionGeneracionOPControl = new CooperativaProduccion.Controls.AdministracionGeneracionOPControl();
            this.TabConsultaLiquidacion = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirAfip = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pago)).BeginInit();
            this.Pago.SuspendLayout();
            this.TabGeneracionOrdenPago.SuspendLayout();
            this.TabConsultaLiquidacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
            this.groupControl7.SuspendLayout();
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
            this.ribbon.Size = new System.Drawing.Size(1270, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // Pago
            // 
            this.Pago.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pago.Location = new System.Drawing.Point(1, 41);
            this.Pago.Name = "Pago";
            this.Pago.SelectedTabPage = this.TabGeneracionOrdenPago;
            this.Pago.Size = new System.Drawing.Size(1274, 493);
            this.Pago.TabIndex = 74;
            this.Pago.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabGeneracionOrdenPago,
            this.TabConsultaLiquidacion});
            // 
            // TabGeneracionOrdenPago
            // 
            this.TabGeneracionOrdenPago.Controls.Add(this.administracionGeneracionOPControl);
            this.TabGeneracionOrdenPago.Image = ((System.Drawing.Image)(resources.GetObject("TabGeneracionOrdenPago.Image")));
            this.TabGeneracionOrdenPago.Name = "TabGeneracionOrdenPago";
            this.TabGeneracionOrdenPago.Size = new System.Drawing.Size(1268, 462);
            this.TabGeneracionOrdenPago.Text = "Generación de Ordenes de Pago";
            // 
            // administracionGeneracionOPControl
            // 
            this.administracionGeneracionOPControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.administracionGeneracionOPControl.Location = new System.Drawing.Point(0, 0);
            this.administracionGeneracionOPControl.Name = "administracionGeneracionOPControl";
            this.administracionGeneracionOPControl.Size = new System.Drawing.Size(1268, 462);
            this.administracionGeneracionOPControl.TabIndex = 0;
            // 
            // TabConsultaLiquidacion
            // 
            this.TabConsultaLiquidacion.Controls.Add(this.groupControl7);
            this.TabConsultaLiquidacion.Image = ((System.Drawing.Image)(resources.GetObject("TabConsultaLiquidacion.Image")));
            this.TabConsultaLiquidacion.Name = "TabConsultaLiquidacion";
            this.TabConsultaLiquidacion.Size = new System.Drawing.Size(1268, 462);
            this.TabConsultaLiquidacion.Text = "Consulta de Ordenes de Pago";
            // 
            // groupControl7
            // 
            this.groupControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl7.Controls.Add(this.btnPrevisualizar);
            this.groupControl7.Controls.Add(this.btnSubirAfip);
            this.groupControl7.Location = new System.Drawing.Point(1, 582);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1190, 33);
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
            // Form_AdministracionOrdenPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 532);
            this.Controls.Add(this.Pago);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionOrdenPago";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orden de Pago a Productores";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pago)).EndInit();
            this.Pago.ResumeLayout(false);
            this.TabGeneracionOrdenPago.ResumeLayout(false);
            this.TabConsultaLiquidacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraTab.XtraTabControl Pago;
        private DevExpress.XtraTab.XtraTabPage TabGeneracionOrdenPago;
        private DevExpress.XtraTab.XtraTabPage TabConsultaLiquidacion;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private DevExpress.XtraEditors.SimpleButton btnPrevisualizar;
        private DevExpress.XtraEditors.SimpleButton btnSubirAfip;
        private Controls.AdministracionGeneracionOPControl administracionGeneracionOPControl;
    }
}