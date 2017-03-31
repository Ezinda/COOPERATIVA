namespace CooperativaProduccion
{
    partial class Form_AdministracionBuscarProductor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionBuscarProductor));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevoProductor = new DevExpress.XtraBars.BarButtonItem();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlProductor = new DevExpress.XtraGrid.GridControl();
            this.gridViewProductor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductor)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnNuevoProductor});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowCategoryInCaption = false;
            this.ribbon.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.ShowQatLocationSelector = false;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(704, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // btnNuevoProductor
            // 
            this.btnNuevoProductor.Caption = "Nuevo Productor";
            this.btnNuevoProductor.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNuevoProductor.Glyph")));
            this.btnNuevoProductor.Id = 1;
            this.btnNuevoProductor.Name = "btnNuevoProductor";
            this.btnNuevoProductor.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControlProductor);
            this.groupControl2.Location = new System.Drawing.Point(2, 63);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(700, 392);
            this.groupControl2.TabIndex = 21;
            this.groupControl2.Text = "Lista de Productores";
            // 
            // gridControlProductor
            // 
            this.gridControlProductor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProductor.Location = new System.Drawing.Point(2, 20);
            this.gridControlProductor.MainView = this.gridViewProductor;
            this.gridControlProductor.MenuManager = this.ribbon;
            this.gridControlProductor.Name = "gridControlProductor";
            this.gridControlProductor.Size = new System.Drawing.Size(696, 370);
            this.gridControlProductor.TabIndex = 1;
            this.gridControlProductor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProductor});
            this.gridControlProductor.DoubleClick += new System.EventHandler(this.gridControlProductor_DoubleClick);
            // 
            // gridViewProductor
            // 
            this.gridViewProductor.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewProductor.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewProductor.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewProductor.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewProductor.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewProductor.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewProductor.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewProductor.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewProductor.GridControl = this.gridControlProductor;
            this.gridViewProductor.Name = "gridViewProductor";
            this.gridViewProductor.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewProductor.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewProductor.OptionsBehavior.Editable = false;
            this.gridViewProductor.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Nothing;
            this.gridViewProductor.OptionsView.ShowGroupPanel = false;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(206, 33);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(282, 23);
            this.txtBusqueda.TabIndex = 2;
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(488, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(29, 23);
            this.btnBuscar.TabIndex = 3;
            // 
            // Form_AdministracionBuscarProductor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 456);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionBuscarProductor";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Productores";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonItem btnNuevoProductor;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControlProductor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProductor;
        public System.Windows.Forms.TextBox txtBusqueda;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
    }
}