namespace CooperativaProduccion
{
    partial class Form_ProduccionMuestrasEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionMuestrasEditor));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblCaja = new System.Windows.Forms.Label();
            this.spinCaja = new DevExpress.XtraEditors.SpinEdit();
            this.memoObservaciones = new DevExpress.XtraEditors.MemoEdit();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.lblBlend = new System.Windows.Forms.Label();
            this.lblPM = new System.Windows.Forms.Label();
            this.cbBlend = new System.Windows.Forms.ComboBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.timeSpanHora = new DevExpress.XtraEditors.TimeSpanEdit();
            this.lblCorrida = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.gridControlMuestra = new DevExpress.XtraGrid.GridControl();
            this.gridViewMuestra = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoObservaciones.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanHora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMuestra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMuestra)).BeginInit();
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
            this.ribbonControl.Size = new System.Drawing.Size(614, 27);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Location = new System.Drawing.Point(526, 507);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(76, 21);
            this.btnGrabar.TabIndex = 77;
            this.btnGrabar.Text = "Grabar";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(12, 42);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 16);
            this.lblFecha.TabIndex = 62;
            this.lblFecha.Text = "Fecha";
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.Location = new System.Drawing.Point(402, 103);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(47, 16);
            this.lblCaja.TabIndex = 68;
            this.lblCaja.Text = "Caja N°";
            // 
            // spinCaja
            // 
            this.spinCaja.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinCaja.Location = new System.Drawing.Point(455, 102);
            this.spinCaja.Name = "spinCaja";
            this.spinCaja.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinCaja.Properties.DisplayFormat.FormatString = "d";
            this.spinCaja.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.spinCaja.Properties.EditFormat.FormatString = "d";
            this.spinCaja.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.spinCaja.Properties.Mask.EditMask = "[1-9]+[0-9]?+";
            this.spinCaja.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.spinCaja.Size = new System.Drawing.Size(147, 20);
            this.spinCaja.TabIndex = 69;
            // 
            // memoObservaciones
            // 
            this.memoObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoObservaciones.Location = new System.Drawing.Point(105, 453);
            this.memoObservaciones.Name = "memoObservaciones";
            this.memoObservaciones.Size = new System.Drawing.Size(497, 25);
            this.memoObservaciones.TabIndex = 76;
            // 
            // dateFecha
            // 
            this.dateFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFecha.Location = new System.Drawing.Point(58, 38);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(100, 21);
            this.dateFecha.TabIndex = 63;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObservaciones.Location = new System.Drawing.Point(13, 453);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(86, 16);
            this.lblObservaciones.TabIndex = 75;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // lblBlend
            // 
            this.lblBlend.AutoSize = true;
            this.lblBlend.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlend.Location = new System.Drawing.Point(13, 103);
            this.lblBlend.Name = "lblBlend";
            this.lblBlend.Size = new System.Drawing.Size(39, 16);
            this.lblBlend.TabIndex = 66;
            this.lblBlend.Text = "Blend";
            // 
            // lblPM
            // 
            this.lblPM.AutoSize = true;
            this.lblPM.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPM.Location = new System.Drawing.Point(13, 424);
            this.lblPM.Name = "lblPM";
            this.lblPM.Size = new System.Drawing.Size(43, 16);
            this.lblPM.TabIndex = 74;
            this.lblPM.Text = "P.M.: 0";
            // 
            // cbBlend
            // 
            this.cbBlend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlend.FormattingEnabled = true;
            this.cbBlend.Location = new System.Drawing.Point(58, 102);
            this.cbBlend.Name = "cbBlend";
            this.cbBlend.Size = new System.Drawing.Size(327, 21);
            this.cbBlend.TabIndex = 67;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(13, 405);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(115, 16);
            this.lblTotal.TabIndex = 73;
            this.lblTotal.Text = "TOTAL SOBRE 1/2: 0";
            // 
            // timeSpanHora
            // 
            this.timeSpanHora.EditValue = System.TimeSpan.Parse("00:00:00");
            this.timeSpanHora.Location = new System.Drawing.Point(58, 65);
            this.timeSpanHora.Name = "timeSpanHora";
            this.timeSpanHora.Properties.AllowEditDays = false;
            this.timeSpanHora.Properties.AllowEditSeconds = false;
            this.timeSpanHora.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeSpanHora.Properties.DisplayFormat.FormatString = "d";
            this.timeSpanHora.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeSpanHora.Properties.Mask.EditMask = "HH:mm";
            this.timeSpanHora.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.timeSpanHora.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.SpinButtons;
            this.timeSpanHora.Properties.TouchUIMaxValue = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.timeSpanHora.Properties.TouchUIMinValue = new System.DateTime(((long)(0)));
            this.timeSpanHora.Size = new System.Drawing.Size(100, 20);
            this.timeSpanHora.TabIndex = 65;
            // 
            // lblCorrida
            // 
            this.lblCorrida.AutoSize = true;
            this.lblCorrida.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrida.Location = new System.Drawing.Point(13, 153);
            this.lblCorrida.Name = "lblCorrida";
            this.lblCorrida.Size = new System.Drawing.Size(51, 16);
            this.lblCorrida.TabIndex = 71;
            this.lblCorrida.Text = "Corrida:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "Hora";
            // 
            // lblOrden
            // 
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.Location = new System.Drawing.Point(13, 128);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(124, 16);
            this.lblOrden.TabIndex = 70;
            this.lblOrden.Text = "Orden de Produccion:";
            // 
            // gridControlMuestra
            // 
            this.gridControlMuestra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlMuestra.Location = new System.Drawing.Point(16, 184);
            this.gridControlMuestra.MainView = this.gridViewMuestra;
            this.gridControlMuestra.Name = "gridControlMuestra";
            this.gridControlMuestra.Size = new System.Drawing.Size(586, 218);
            this.gridControlMuestra.TabIndex = 72;
            this.gridControlMuestra.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMuestra});
            // 
            // gridViewMuestra
            // 
            this.gridViewMuestra.GridControl = this.gridControlMuestra;
            this.gridViewMuestra.Name = "gridViewMuestra";
            this.gridViewMuestra.OptionsView.ShowGroupPanel = false;
            // 
            // Form_ProduccionMuestrasEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 540);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblCaja);
            this.Controls.Add(this.spinCaja);
            this.Controls.Add(this.memoObservaciones);
            this.Controls.Add(this.dateFecha);
            this.Controls.Add(this.lblObservaciones);
            this.Controls.Add(this.lblBlend);
            this.Controls.Add(this.lblPM);
            this.Controls.Add(this.cbBlend);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.timeSpanHora);
            this.Controls.Add(this.lblCorrida);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.gridControlMuestra);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProduccionMuestrasEditor";
            this.Ribbon = this.ribbonControl;
            this.Text = "Editor de Muestra";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoObservaciones.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanHora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMuestra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMuestra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraEditors.SimpleButton btnGrabar;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblCaja;
        private DevExpress.XtraEditors.SpinEdit spinCaja;
        private DevExpress.XtraEditors.MemoEdit memoObservaciones;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.Label lblBlend;
        private System.Windows.Forms.Label lblPM;
        private System.Windows.Forms.ComboBox cbBlend;
        private System.Windows.Forms.Label lblTotal;
        private DevExpress.XtraEditors.TimeSpanEdit timeSpanHora;
        private System.Windows.Forms.Label lblCorrida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOrden;
        private DevExpress.XtraGrid.GridControl gridControlMuestra;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMuestra;
    }
}