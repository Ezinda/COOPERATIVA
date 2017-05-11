namespace CooperativaProduccion
{
    partial class Form_ProduccionTemperatura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionTemperatura));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupResultados = new DevExpress.XtraEditors.GroupControl();
            this.gridControlTemperatura = new DevExpress.XtraGrid.GridControl();
            this.gridViewTemperatura = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTemperatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTemperatura)).BeginInit();
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
            this.groupResultados.Controls.Add(this.gridControlTemperatura);
            this.groupResultados.Location = new System.Drawing.Point(12, 95);
            this.groupResultados.Name = "groupResultados";
            this.groupResultados.Size = new System.Drawing.Size(669, 361);
            this.groupResultados.TabIndex = 84;
            this.groupResultados.Text = "Resultados de búsqueda";
            // 
            // gridControlTemperatura
            // 
            this.gridControlTemperatura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTemperatura.Location = new System.Drawing.Point(2, 20);
            this.gridControlTemperatura.MainView = this.gridViewTemperatura;
            this.gridControlTemperatura.Name = "gridControlTemperatura";
            this.gridControlTemperatura.Size = new System.Drawing.Size(665, 339);
            this.gridControlTemperatura.TabIndex = 68;
            this.gridControlTemperatura.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTemperatura});
            // 
            // gridViewTemperatura
            // 
            this.gridViewTemperatura.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewTemperatura.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewTemperatura.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewTemperatura.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewTemperatura.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewTemperatura.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewTemperatura.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewTemperatura.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewTemperatura.GridControl = this.gridControlTemperatura;
            this.gridViewTemperatura.Name = "gridViewTemperatura";
            this.gridViewTemperatura.OptionsBehavior.Editable = false;
            this.gridViewTemperatura.OptionsSelection.MultiSelect = true;
            this.gridViewTemperatura.OptionsView.ShowGroupPanel = false;
            // 
            // groupOpciones
            // 
            this.groupOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOpciones.Controls.Add(this.btnNuevo);
            this.groupOpciones.Location = new System.Drawing.Point(555, 39);
            this.groupOpciones.Name = "groupOpciones";
            this.groupOpciones.Size = new System.Drawing.Size(126, 50);
            this.groupOpciones.TabIndex = 83;
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
            this.groupBusqueda.Location = new System.Drawing.Point(12, 39);
            this.groupBusqueda.Name = "groupBusqueda";
            this.groupBusqueda.Size = new System.Drawing.Size(537, 50);
            this.groupBusqueda.TabIndex = 82;
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
            // Form_ProduccionTemperatura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 468);
            this.Controls.Add(this.groupResultados);
            this.Controls.Add(this.groupOpciones);
            this.Controls.Add(this.groupBusqueda);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProduccionTemperatura";
            this.Ribbon = this.ribbonControl;
            this.Text = "Producción: Control de Temperatura";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupResultados)).EndInit();
            this.groupResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTemperatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTemperatura)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridControlTemperatura;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTemperatura;
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