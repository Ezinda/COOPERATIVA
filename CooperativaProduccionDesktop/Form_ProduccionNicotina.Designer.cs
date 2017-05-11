namespace CooperativaProduccion
{
    partial class Form_ProduccionNicotina
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionNicotina));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupResultados = new DevExpress.XtraEditors.GroupControl();
            this.gridControlNicotina = new DevExpress.XtraGrid.GridControl();
            this.gridViewNicotina = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupOpciones = new DevExpress.XtraEditors.GroupControl();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.groupBusqueda = new DevExpress.XtraEditors.GroupControl();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.cbBlend = new System.Windows.Forms.ComboBox();
            this.lblBlend = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupResultados)).BeginInit();
            this.groupResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNicotina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNicotina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupOpciones)).BeginInit();
            this.groupOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBusqueda)).BeginInit();
            this.groupBusqueda.SuspendLayout();
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
            this.ribbonControl.Size = new System.Drawing.Size(693, 27);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // groupResultados
            // 
            this.groupResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupResultados.Controls.Add(this.gridControlNicotina);
            this.groupResultados.Location = new System.Drawing.Point(12, 93);
            this.groupResultados.Name = "groupResultados";
            this.groupResultados.Size = new System.Drawing.Size(669, 361);
            this.groupResultados.TabIndex = 90;
            this.groupResultados.Text = "Resultados de búsqueda";
            // 
            // gridControlNicotina
            // 
            this.gridControlNicotina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlNicotina.Location = new System.Drawing.Point(2, 20);
            this.gridControlNicotina.MainView = this.gridViewNicotina;
            this.gridControlNicotina.Name = "gridControlNicotina";
            this.gridControlNicotina.Size = new System.Drawing.Size(665, 339);
            this.gridControlNicotina.TabIndex = 68;
            this.gridControlNicotina.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNicotina});
            // 
            // gridViewNicotina
            // 
            this.gridViewNicotina.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewNicotina.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewNicotina.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewNicotina.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewNicotina.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewNicotina.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewNicotina.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewNicotina.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewNicotina.GridControl = this.gridControlNicotina;
            this.gridViewNicotina.Name = "gridViewNicotina";
            this.gridViewNicotina.OptionsBehavior.Editable = false;
            this.gridViewNicotina.OptionsSelection.MultiSelect = true;
            this.gridViewNicotina.OptionsView.ShowGroupPanel = false;
            // 
            // groupOpciones
            // 
            this.groupOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOpciones.Controls.Add(this.btnNuevo);
            this.groupOpciones.Location = new System.Drawing.Point(555, 37);
            this.groupOpciones.Name = "groupOpciones";
            this.groupOpciones.Size = new System.Drawing.Size(126, 50);
            this.groupOpciones.TabIndex = 89;
            this.groupOpciones.Text = "Opciones";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.Location = new System.Drawing.Point(5, 23);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(116, 22);
            this.btnNuevo.TabIndex = 39;
            this.btnNuevo.Text = "Nuevo Control";
            // 
            // groupBusqueda
            // 
            this.groupBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBusqueda.Controls.Add(this.lblFecha);
            this.groupBusqueda.Controls.Add(this.dateFecha);
            this.groupBusqueda.Controls.Add(this.btnBuscar);
            this.groupBusqueda.Controls.Add(this.cbBlend);
            this.groupBusqueda.Controls.Add(this.lblBlend);
            this.groupBusqueda.Location = new System.Drawing.Point(12, 37);
            this.groupBusqueda.Name = "groupBusqueda";
            this.groupBusqueda.Size = new System.Drawing.Size(537, 50);
            this.groupBusqueda.TabIndex = 88;
            this.groupBusqueda.Text = "Búsqueda";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(10, 25);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 16);
            this.lblFecha.TabIndex = 64;
            this.lblFecha.Text = "Fecha";
            // 
            // dateFecha
            // 
            this.dateFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFecha.Location = new System.Drawing.Point(56, 23);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(100, 21);
            this.dateFecha.TabIndex = 65;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(456, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(76, 21);
            this.btnBuscar.TabIndex = 60;
            this.btnBuscar.Text = "Buscar";
            // 
            // cbBlend
            // 
            this.cbBlend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBlend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlend.FormattingEnabled = true;
            this.cbBlend.Location = new System.Drawing.Point(215, 23);
            this.cbBlend.Name = "cbBlend";
            this.cbBlend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbBlend.Size = new System.Drawing.Size(220, 21);
            this.cbBlend.TabIndex = 58;
            // 
            // lblBlend
            // 
            this.lblBlend.AutoSize = true;
            this.lblBlend.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlend.Location = new System.Drawing.Point(170, 25);
            this.lblBlend.Name = "lblBlend";
            this.lblBlend.Size = new System.Drawing.Size(39, 16);
            this.lblBlend.TabIndex = 57;
            this.lblBlend.Text = "Blend";
            // 
            // Form_ProduccionNicotina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 468);
            this.Controls.Add(this.groupResultados);
            this.Controls.Add(this.groupOpciones);
            this.Controls.Add(this.groupBusqueda);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProduccionNicotina";
            this.Ribbon = this.ribbonControl;
            this.Text = "Producción: Control de Nicotina";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupResultados)).EndInit();
            this.groupResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNicotina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNicotina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupOpciones)).EndInit();
            this.groupOpciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBusqueda)).EndInit();
            this.groupBusqueda.ResumeLayout(false);
            this.groupBusqueda.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraEditors.GroupControl groupResultados;
        private DevExpress.XtraGrid.GridControl gridControlNicotina;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNicotina;
        private DevExpress.XtraEditors.GroupControl groupOpciones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.GroupControl groupBusqueda;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ComboBox cbBlend;
        private System.Windows.Forms.Label lblBlend;
    }
}