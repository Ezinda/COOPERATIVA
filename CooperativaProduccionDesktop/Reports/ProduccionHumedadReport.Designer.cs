namespace CooperativaProduccion.Reports
{
    partial class ProduccionHumedadReport
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Blend = new DevExpress.XtraReports.Parameters.Parameter();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.Subreport = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel146 = new DevExpress.XtraReports.UI.XRLabel();
            this.Corrida = new DevExpress.XtraReports.Parameters.Parameter();
            this.OrdenProduccion = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel143 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel142 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel141 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Blend
            // 
            this.Blend.Name = "Blend";
            this.Blend.Visible = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 46.875F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Subreport,
            this.xrLabel146});
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 164.6667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Subreport
            // 
            this.Subreport.Dpi = 100F;
            this.Subreport.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47.9167F);
            this.Subreport.Name = "Subreport";
            this.Subreport.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("_Fecha", null, "_Fecha"));
            this.Subreport.ReportSource = new CooperativaProduccion.Reports.ProduccionHumedadDetalleReport();
            this.Subreport.SizeF = new System.Drawing.SizeF(750.9999F, 116.75F);
            // 
            // xrLabel146
            // 
            this.xrLabel146.Dpi = 100F;
            this.xrLabel146.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel146.LocationFloat = new DevExpress.Utils.PointFloat(9.958203F, 16.6667F);
            this.xrLabel146.Name = "xrLabel146";
            this.xrLabel146.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel146.SizeF = new System.Drawing.SizeF(731.0417F, 23F);
            this.xrLabel146.StylePriority.UseFont = false;
            this.xrLabel146.StylePriority.UseTextAlignment = false;
            this.xrLabel146.Text = "BLEND: [Parameters.Blend]   O. PRODUCCION N°: [Parameters.OrdenProduccion]   CORR" +
    "IDA: [Corrida]";
            this.xrLabel146.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // Corrida
            // 
            this.Corrida.Name = "Corrida";
            this.Corrida.Visible = false;
            // 
            // OrdenProduccion
            // 
            this.OrdenProduccion.Name = "OrdenProduccion";
            this.OrdenProduccion.Visible = false;
            // 
            // xrLabel143
            // 
            this.xrLabel143.Dpi = 100F;
            this.xrLabel143.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel143.LocationFloat = new DevExpress.Utils.PointFloat(100.0001F, 0F);
            this.xrLabel143.Name = "xrLabel143";
            this.xrLabel143.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel143.SizeF = new System.Drawing.SizeF(550.9582F, 23F);
            this.xrLabel143.StylePriority.UseFont = false;
            this.xrLabel143.StylePriority.UseTextAlignment = false;
            this.xrLabel143.Text = "CONTROL DE PRODUCCIÓN Y CALIDAD";
            this.xrLabel143.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel142
            // 
            this.xrLabel142.Dpi = 100F;
            this.xrLabel142.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel142.LocationFloat = new DevExpress.Utils.PointFloat(650.9999F, 0F);
            this.xrLabel142.Name = "xrLabel142";
            this.xrLabel142.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel142.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel142.StylePriority.UseFont = false;
            // 
            // xrLabel141
            // 
            this.xrLabel141.Dpi = 100F;
            this.xrLabel141.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel141.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
            this.xrLabel141.Name = "xrLabel141";
            this.xrLabel141.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel141.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel141.StylePriority.UseFont = false;
            this.xrLabel141.Text = "COPAT";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(100.0001F, 33.58332F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(550.9582F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "HUMEDAD DE PRODUCTO FINAL";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel141,
            this.xrLabel142,
            this.xrLabel143});
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 62.91669F;
            this.PageHeader.Name = "PageHeader";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(CooperativaProduccion.ReportModels.ProduccionHumedadRecord);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // ProduccionHumedadReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(49, 50, 100, 47);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Blend,
            this.OrdenProduccion,
            this.Corrida});
            this.Version = "16.1";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.Parameters.Parameter Blend;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.Parameters.Parameter Corrida;
        private DevExpress.XtraReports.Parameters.Parameter OrdenProduccion;
        private DevExpress.XtraReports.UI.XRLabel xrLabel146;
        private DevExpress.XtraReports.UI.XRLabel xrLabel143;
        private DevExpress.XtraReports.UI.XRLabel xrLabel142;
        private DevExpress.XtraReports.UI.XRLabel xrLabel141;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        public DevExpress.XtraReports.UI.XRSubreport Subreport;

    }
}
