namespace CooperativaProduccion
{
    partial class Form_AdministracionNuevoRemito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionNuevoRemito));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.dpOrdenVenta = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumOperacion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlDetalleOrdenVenta = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetalleOrdenVenta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizarPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGrabarRemito = new DevExpress.XtraEditors.SimpleButton();
            this.btnBorrarPdf = new DevExpress.XtraEditors.SimpleButton();
            this.txtNombrePdf = new System.Windows.Forms.TextBox();
            this.btnSeleccionarPdf = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumRemito = new System.Windows.Forms.TextBox();
            this.dpRemito = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPuntoVenta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetalleOrdenVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetalleOrdenVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
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
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.Size = new System.Drawing.Size(647, 49);
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.dpOrdenVenta);
            this.groupControl3.Controls.Add(this.label15);
            this.groupControl3.Controls.Add(this.txtCliente);
            this.groupControl3.Controls.Add(this.txtCuit);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Controls.Add(this.txtNumOperacion);
            this.groupControl3.Controls.Add(this.label11);
            this.groupControl3.Controls.Add(this.label1);
            this.groupControl3.Location = new System.Drawing.Point(1, 51);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(645, 83);
            this.groupControl3.TabIndex = 74;
            this.groupControl3.Text = "Orden de Venta";
            // 
            // dpOrdenVenta
            // 
            this.dpOrdenVenta.Enabled = false;
            this.dpOrdenVenta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpOrdenVenta.Location = new System.Drawing.Point(260, 24);
            this.dpOrdenVenta.Name = "dpOrdenVenta";
            this.dpOrdenVenta.Size = new System.Drawing.Size(80, 21);
            this.dpOrdenVenta.TabIndex = 76;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(214, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 75;
            this.label15.Text = "Fecha";
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(83, 54);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(257, 21);
            this.txtCliente.TabIndex = 74;
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Location = new System.Drawing.Point(381, 54);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(155, 21);
            this.txtCuit.TabIndex = 73;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(345, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 16);
            this.label6.TabIndex = 72;
            this.label6.Text = "Cuit";
            // 
            // txtNumOperacion
            // 
            this.txtNumOperacion.Enabled = false;
            this.txtNumOperacion.Location = new System.Drawing.Point(83, 24);
            this.txtNumOperacion.Name = "txtNumOperacion";
            this.txtNumOperacion.Size = new System.Drawing.Size(105, 21);
            this.txtNumOperacion.TabIndex = 64;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 16);
            this.label11.TabIndex = 63;
            this.label11.Text = "N° Interno";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 57;
            this.label1.Text = "Razon Social";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.gridControlDetalleOrdenVenta);
            this.groupControl1.Location = new System.Drawing.Point(2, 136);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(645, 118);
            this.groupControl1.TabIndex = 77;
            this.groupControl1.Text = "Detalle de Cajas";
            // 
            // gridControlDetalleOrdenVenta
            // 
            this.gridControlDetalleOrdenVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDetalleOrdenVenta.Location = new System.Drawing.Point(2, 21);
            this.gridControlDetalleOrdenVenta.MainView = this.gridViewDetalleOrdenVenta;
            this.gridControlDetalleOrdenVenta.MenuManager = this.ribbon;
            this.gridControlDetalleOrdenVenta.Name = "gridControlDetalleOrdenVenta";
            this.gridControlDetalleOrdenVenta.Size = new System.Drawing.Size(641, 95);
            this.gridControlDetalleOrdenVenta.TabIndex = 68;
            this.gridControlDetalleOrdenVenta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetalleOrdenVenta,
            this.gridView2});
            // 
            // gridViewDetalleOrdenVenta
            // 
            this.gridViewDetalleOrdenVenta.GridControl = this.gridControlDetalleOrdenVenta;
            this.gridViewDetalleOrdenVenta.Name = "gridViewDetalleOrdenVenta";
            this.gridViewDetalleOrdenVenta.OptionsBehavior.Editable = false;
            this.gridViewDetalleOrdenVenta.OptionsSelection.MultiSelect = true;
            this.gridViewDetalleOrdenVenta.OptionsView.ShowGroupPanel = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlDetalleOrdenVenta;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.btnPrevisualizarPdf);
            this.groupControl2.Controls.Add(this.btnCancelar);
            this.groupControl2.Controls.Add(this.btnGrabarRemito);
            this.groupControl2.Controls.Add(this.btnBorrarPdf);
            this.groupControl2.Controls.Add(this.txtNombrePdf);
            this.groupControl2.Controls.Add(this.btnSeleccionarPdf);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.txtNumRemito);
            this.groupControl2.Controls.Add(this.dpRemito);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.txtPuntoVenta);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Location = new System.Drawing.Point(3, 256);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(641, 83);
            this.groupControl2.TabIndex = 78;
            this.groupControl2.Text = "Remito Electrónico Generado";
            // 
            // btnPrevisualizarPdf
            // 
            this.btnPrevisualizarPdf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrevisualizarPdf.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevisualizarPdf.Image")));
            this.btnPrevisualizarPdf.Location = new System.Drawing.Point(474, 23);
            this.btnPrevisualizarPdf.Name = "btnPrevisualizarPdf";
            this.btnPrevisualizarPdf.Size = new System.Drawing.Size(162, 23);
            this.btnPrevisualizarPdf.TabIndex = 84;
            this.btnPrevisualizarPdf.Text = "Previsualizar";
            this.btnPrevisualizarPdf.Click += new System.EventHandler(this.btnPrevisualizarPdf_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(558, 52);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(78, 23);
            this.btnCancelar.TabIndex = 83;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGrabarRemito
            // 
            this.btnGrabarRemito.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGrabarRemito.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabarRemito.Image")));
            this.btnGrabarRemito.Location = new System.Drawing.Point(474, 52);
            this.btnGrabarRemito.Name = "btnGrabarRemito";
            this.btnGrabarRemito.Size = new System.Drawing.Size(78, 23);
            this.btnGrabarRemito.TabIndex = 82;
            this.btnGrabarRemito.Text = "Grabar";
            this.btnGrabarRemito.Click += new System.EventHandler(this.btnGrabarRemito_Click);
            // 
            // btnBorrarPdf
            // 
            this.btnBorrarPdf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBorrarPdf.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrarPdf.Image")));
            this.btnBorrarPdf.Location = new System.Drawing.Point(129, 50);
            this.btnBorrarPdf.Name = "btnBorrarPdf";
            this.btnBorrarPdf.Size = new System.Drawing.Size(24, 23);
            this.btnBorrarPdf.TabIndex = 81;
            this.btnBorrarPdf.Click += new System.EventHandler(this.btnBorrarPdf_Click);
            // 
            // txtNombrePdf
            // 
            this.txtNombrePdf.Enabled = false;
            this.txtNombrePdf.Location = new System.Drawing.Point(172, 52);
            this.txtNombrePdf.Name = "txtNombrePdf";
            this.txtNombrePdf.Size = new System.Drawing.Size(272, 21);
            this.txtNombrePdf.TabIndex = 80;
            // 
            // btnSeleccionarPdf
            // 
            this.btnSeleccionarPdf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSeleccionarPdf.Image = ((System.Drawing.Image)(resources.GetObject("btnSeleccionarPdf.Image")));
            this.btnSeleccionarPdf.Location = new System.Drawing.Point(99, 50);
            this.btnSeleccionarPdf.Name = "btnSeleccionarPdf";
            this.btnSeleccionarPdf.Size = new System.Drawing.Size(24, 23);
            this.btnSeleccionarPdf.TabIndex = 79;
            this.btnSeleccionarPdf.Click += new System.EventHandler(this.btnSeleccionarPdf_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 78;
            this.label3.Text = "Adjuntar Remito";
            // 
            // txtNumRemito
            // 
            this.txtNumRemito.Location = new System.Drawing.Point(347, 22);
            this.txtNumRemito.Name = "txtNumRemito";
            this.txtNumRemito.Size = new System.Drawing.Size(97, 21);
            this.txtNumRemito.TabIndex = 77;
            this.txtNumRemito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumRemito_KeyPress);
            // 
            // dpRemito
            // 
            this.dpRemito.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpRemito.Location = new System.Drawing.Point(73, 23);
            this.dpRemito.Name = "dpRemito";
            this.dpRemito.Size = new System.Drawing.Size(80, 21);
            this.dpRemito.TabIndex = 76;
            this.dpRemito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dpRemito_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 75;
            this.label2.Text = "Fecha";
            // 
            // txtPuntoVenta
            // 
            this.txtPuntoVenta.Location = new System.Drawing.Point(269, 22);
            this.txtPuntoVenta.Name = "txtPuntoVenta";
            this.txtPuntoVenta.Size = new System.Drawing.Size(72, 21);
            this.txtPuntoVenta.TabIndex = 64;
            this.txtPuntoVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuntoVenta_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(169, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 63;
            this.label4.Text = "Comprobante N°";
            // 
            // Form_AdministracionNuevoRemito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 341);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionNuevoRemito";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración - Nuevo Remito";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetalleOrdenVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetalleOrdenVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumOperacion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dpOrdenVenta;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControlDetalleOrdenVenta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetalleOrdenVenta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.DateTimePicker dpRemito;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPuntoVenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumRemito;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnGrabarRemito;
        private DevExpress.XtraEditors.SimpleButton btnBorrarPdf;
        private System.Windows.Forms.TextBox txtNombrePdf;
        private DevExpress.XtraEditors.SimpleButton btnSeleccionarPdf;
        private DevExpress.XtraEditors.SimpleButton btnPrevisualizarPdf;
        private System.Windows.Forms.OpenFileDialog ofd;
    }
}