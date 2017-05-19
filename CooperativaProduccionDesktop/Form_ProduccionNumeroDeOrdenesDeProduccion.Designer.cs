namespace CooperativaProduccion
{
    partial class Form_ProduccionNumeroDeOrdenesDeProduccion
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
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPeriodo = new DevExpress.XtraEditors.TextEdit();
            this.btnListar = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlBlends = new DevExpress.XtraGrid.GridControl();
            this.gridViewBlends = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBlends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBlends)).BeginInit();
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
            this.ribbonControl.Size = new System.Drawing.Size(409, 27);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 75;
            this.label1.Text = "Periodo";
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Location = new System.Drawing.Point(67, 38);
            this.txtPeriodo.MenuManager = this.ribbonControl;
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.Properties.Mask.EditMask = "\\d+";
            this.txtPeriodo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPeriodo.Size = new System.Drawing.Size(100, 20);
            this.txtPeriodo.TabIndex = 76;
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(173, 36);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(81, 23);
            this.btnListar.TabIndex = 77;
            this.btnListar.Text = "Listar Blends";
            // 
            // gridControlBlends
            // 
            this.gridControlBlends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlBlends.Location = new System.Drawing.Point(15, 65);
            this.gridControlBlends.MainView = this.gridViewBlends;
            this.gridControlBlends.Name = "gridControlBlends";
            this.gridControlBlends.Size = new System.Drawing.Size(382, 302);
            this.gridControlBlends.TabIndex = 97;
            this.gridControlBlends.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBlends});
            // 
            // gridViewBlends
            // 
            this.gridViewBlends.GridControl = this.gridControlBlends;
            this.gridViewBlends.Name = "gridViewBlends";
            this.gridViewBlends.OptionsView.ShowGroupPanel = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(316, 373);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(81, 23);
            this.btnGuardar.TabIndex = 98;
            this.btnGuardar.Text = "Guardar";
            // 
            // Form_ProduccionNumeroDeOrdenesDeProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 408);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gridControlBlends);
            this.Controls.Add(this.btnListar);
            this.Controls.Add(this.txtPeriodo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ribbonControl);
            this.Name = "Form_ProduccionNumeroDeOrdenesDeProduccion";
            this.Ribbon = this.ribbonControl;
            this.Text = "Producción: Número de Ordenes";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBlends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBlends)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtPeriodo;
        private DevExpress.XtraEditors.SimpleButton btnListar;
        private DevExpress.XtraGrid.GridControl gridControlBlends;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBlends;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
    }
}