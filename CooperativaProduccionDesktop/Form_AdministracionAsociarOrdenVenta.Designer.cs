namespace CooperativaProduccion
{
    partial class Form_AdministracionAsociarOrdenVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionAsociarOrdenVenta));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlCajaConsulta = new DevExpress.XtraGrid.GridControl();
            this.gridViewCajaConsulta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtCantidadCajaConsulta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBuscarCaja = new DevExpress.XtraEditors.SimpleButton();
            this.cbProductoConsulta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupControl8 = new DevExpress.XtraEditors.GroupControl();
            this.btnAsociarOV = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCajaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCajaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).BeginInit();
            this.groupControl8.SuspendLayout();
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
            this.ribbon.Size = new System.Drawing.Size(907, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl6
            // 
            this.groupControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl6.Controls.Add(this.gridControlCajaConsulta);
            this.groupControl6.Location = new System.Drawing.Point(1, 119);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(905, 331);
            this.groupControl6.TabIndex = 77;
            this.groupControl6.Text = "Lista de Cajas";
            // 
            // gridControlCajaConsulta
            // 
            this.gridControlCajaConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCajaConsulta.Location = new System.Drawing.Point(2, 20);
            this.gridControlCajaConsulta.MainView = this.gridViewCajaConsulta;
            this.gridControlCajaConsulta.MenuManager = this.ribbon;
            this.gridControlCajaConsulta.Name = "gridControlCajaConsulta";
            this.gridControlCajaConsulta.Size = new System.Drawing.Size(901, 309);
            this.gridControlCajaConsulta.TabIndex = 68;
            this.gridControlCajaConsulta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCajaConsulta,
            this.gridViewLiquidacionDetalle});
            // 
            // gridViewCajaConsulta
            // 
            this.gridViewCajaConsulta.GridControl = this.gridControlCajaConsulta;
            this.gridViewCajaConsulta.Name = "gridViewCajaConsulta";
            this.gridViewCajaConsulta.OptionsBehavior.Editable = false;
            this.gridViewCajaConsulta.OptionsSelection.MultiSelect = true;
            this.gridViewCajaConsulta.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewCajaConsulta.OptionsView.ShowGroupPanel = false;
            // 
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlCajaConsulta;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.txtCantidadCajaConsulta);
            this.groupControl2.Controls.Add(this.label11);
            this.groupControl2.Controls.Add(this.btnBuscarCaja);
            this.groupControl2.Controls.Add(this.cbProductoConsulta);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(1, 68);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(905, 50);
            this.groupControl2.TabIndex = 76;
            this.groupControl2.Text = "Buscar Cajas";
            // 
            // txtCantidadCajaConsulta
            // 
            this.txtCantidadCajaConsulta.Location = new System.Drawing.Point(479, 25);
            this.txtCantidadCajaConsulta.Name = "txtCantidadCajaConsulta";
            this.txtCantidadCajaConsulta.Size = new System.Drawing.Size(93, 21);
            this.txtCantidadCajaConsulta.TabIndex = 62;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(372, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 16);
            this.label11.TabIndex = 61;
            this.label11.Text = "Cantidad de Cajas";
            // 
            // btnBuscarCaja
            // 
            this.btnBuscarCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCaja.Image")));
            this.btnBuscarCaja.Location = new System.Drawing.Point(578, 24);
            this.btnBuscarCaja.Name = "btnBuscarCaja";
            this.btnBuscarCaja.Size = new System.Drawing.Size(76, 21);
            this.btnBuscarCaja.TabIndex = 60;
            this.btnBuscarCaja.Text = "Buscar";
            this.btnBuscarCaja.Click += new System.EventHandler(this.btnBuscarCaja_Click);
            // 
            // cbProductoConsulta
            // 
            this.cbProductoConsulta.FormattingEnabled = true;
            this.cbProductoConsulta.Location = new System.Drawing.Point(69, 25);
            this.cbProductoConsulta.Name = "cbProductoConsulta";
            this.cbProductoConsulta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbProductoConsulta.Size = new System.Drawing.Size(290, 21);
            this.cbProductoConsulta.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Producto";
            // 
            // groupControl8
            // 
            this.groupControl8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl8.Controls.Add(this.btnAsociarOV);
            this.groupControl8.Location = new System.Drawing.Point(3, 451);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.ShowCaption = false;
            this.groupControl8.Size = new System.Drawing.Size(903, 31);
            this.groupControl8.TabIndex = 79;
            this.groupControl8.Text = "Buscar Cata";
            // 
            // btnAsociarOV
            // 
            this.btnAsociarOV.Image = ((System.Drawing.Image)(resources.GetObject("btnAsociarOV.Image")));
            this.btnAsociarOV.Location = new System.Drawing.Point(5, 2);
            this.btnAsociarOV.Name = "btnAsociarOV";
            this.btnAsociarOV.Size = new System.Drawing.Size(150, 26);
            this.btnAsociarOV.TabIndex = 60;
            this.btnAsociarOV.Text = "Asociar Orden de Venta";
            this.btnAsociarOV.Click += new System.EventHandler(this.btnAsociarOV_Click);
            // 
            // Form_AdministracionAsociarOrdenVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 518);
            this.Controls.Add(this.groupControl8);
            this.Controls.Add(this.groupControl6);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Name = "Form_AdministracionAsociarOrdenVenta";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración - Asociar Orden de Venta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCajaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCajaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).EndInit();
            this.groupControl8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlCajaConsulta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCajaConsulta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalle;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox txtCantidadCajaConsulta;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCaja;
        private System.Windows.Forms.ComboBox cbProductoConsulta;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.GroupControl groupControl8;
        private DevExpress.XtraEditors.SimpleButton btnAsociarOV;
    }
}