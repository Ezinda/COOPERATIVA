namespace CooperativaProduccion.Controls
{
    partial class AdministracionVistaOPControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministracionVistaOPControl));
            this.groupControl10 = new DevExpress.XtraEditors.GroupControl();
            this.btnFormaPago = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrdenPago = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrdenPago = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewLiquidacionDetalles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarLiquidacion = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarProductor = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscarFet = new DevExpress.XtraEditors.SimpleButton();
            this.txtProvincia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFet = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProductor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkPeriodo = new System.Windows.Forms.CheckBox();
            this.dpHastaOrdenPago = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dpDesdeOrdenPago = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).BeginInit();
            this.groupControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl10
            // 
            this.groupControl10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl10.Controls.Add(this.btnFormaPago);
            this.groupControl10.Location = new System.Drawing.Point(3, 502);
            this.groupControl10.Name = "groupControl10";
            this.groupControl10.ShowCaption = false;
            this.groupControl10.Size = new System.Drawing.Size(1235, 32);
            this.groupControl10.TabIndex = 82;
            this.groupControl10.Text = "Buscar Romaneo";
            // 
            // btnFormaPago
            // 
            this.btnFormaPago.Image = ((System.Drawing.Image)(resources.GetObject("btnFormaPago.Image")));
            this.btnFormaPago.Location = new System.Drawing.Point(9, 5);
            this.btnFormaPago.Name = "btnFormaPago";
            this.btnFormaPago.Size = new System.Drawing.Size(81, 22);
            this.btnFormaPago.TabIndex = 40;
            this.btnFormaPago.Text = "F. Pago";
            // 
            // groupControl6
            // 
            this.groupControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl6.Controls.Add(this.gridControlOrdenPago);
            this.groupControl6.Location = new System.Drawing.Point(3, 88);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(1235, 413);
            this.groupControl6.TabIndex = 81;
            this.groupControl6.Text = "Lista de Órdenes de Pago";
            // 
            // gridControlOrdenPago
            // 
            this.gridControlOrdenPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOrdenPago.Location = new System.Drawing.Point(2, 21);
            this.gridControlOrdenPago.MainView = this.gridViewOrdenPago;
            this.gridControlOrdenPago.Name = "gridControlOrdenPago";
            this.gridControlOrdenPago.Size = new System.Drawing.Size(1231, 390);
            this.gridControlOrdenPago.TabIndex = 68;
            this.gridControlOrdenPago.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrdenPago,
            this.gridViewLiquidacionDetalles});
            // 
            // gridViewOrdenPago
            // 
            this.gridViewOrdenPago.GridControl = this.gridControlOrdenPago;
            this.gridViewOrdenPago.Name = "gridViewOrdenPago";
            this.gridViewOrdenPago.OptionsBehavior.Editable = false;
            this.gridViewOrdenPago.OptionsView.ShowGroupPanel = false;
            // 
            // gridViewLiquidacionDetalles
            // 
            this.gridViewLiquidacionDetalles.GridControl = this.gridControlOrdenPago;
            this.gridViewLiquidacionDetalles.Name = "gridViewLiquidacionDetalles";
            this.gridViewLiquidacionDetalles.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscarLiquidacion);
            this.groupControl4.Location = new System.Drawing.Point(1152, 3);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(86, 82);
            this.groupControl4.TabIndex = 80;
            this.groupControl4.Text = "Opciones";
            // 
            // btnBuscarLiquidacion
            // 
            this.btnBuscarLiquidacion.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarLiquidacion.Image")));
            this.btnBuscarLiquidacion.Location = new System.Drawing.Point(3, 23);
            this.btnBuscarLiquidacion.Name = "btnBuscarLiquidacion";
            this.btnBuscarLiquidacion.Size = new System.Drawing.Size(81, 22);
            this.btnBuscarLiquidacion.TabIndex = 39;
            this.btnBuscarLiquidacion.Text = "Buscar";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.txtCuit);
            this.groupControl3.Controls.Add(this.label5);
            this.groupControl3.Controls.Add(this.btnBuscarProductor);
            this.groupControl3.Controls.Add(this.btnBuscarFet);
            this.groupControl3.Controls.Add(this.txtProvincia);
            this.groupControl3.Controls.Add(this.label4);
            this.groupControl3.Controls.Add(this.txtFet);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Controls.Add(this.txtProductor);
            this.groupControl3.Controls.Add(this.label7);
            this.groupControl3.Location = new System.Drawing.Point(3, 53);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.ShowCaption = false;
            this.groupControl3.Size = new System.Drawing.Size(1146, 32);
            this.groupControl3.TabIndex = 79;
            this.groupControl3.Text = "Buscar Romaneo";
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Location = new System.Drawing.Point(616, 7);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(153, 20);
            this.txtCuit.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(581, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 16);
            this.label5.TabIndex = 71;
            this.label5.Text = "CUIT:";
            // 
            // btnBuscarProductor
            // 
            this.btnBuscarProductor.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProductor.Image")));
            this.btnBuscarProductor.Location = new System.Drawing.Point(535, 6);
            this.btnBuscarProductor.Name = "btnBuscarProductor";
            this.btnBuscarProductor.Size = new System.Drawing.Size(28, 22);
            this.btnBuscarProductor.TabIndex = 70;
            // 
            // btnBuscarFet
            // 
            this.btnBuscarFet.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarFet.Image")));
            this.btnBuscarFet.Location = new System.Drawing.Point(155, 6);
            this.btnBuscarFet.Name = "btnBuscarFet";
            this.btnBuscarFet.Size = new System.Drawing.Size(28, 22);
            this.btnBuscarFet.TabIndex = 69;
            // 
            // txtProvincia
            // 
            this.txtProvincia.Enabled = false;
            this.txtProvincia.Location = new System.Drawing.Point(836, 7);
            this.txtProvincia.Name = "txtProvincia";
            this.txtProvincia.Size = new System.Drawing.Size(117, 20);
            this.txtProvincia.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(778, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 67;
            this.label4.Text = "Provincia:";
            // 
            // txtFet
            // 
            this.txtFet.Location = new System.Drawing.Point(43, 7);
            this.txtFet.Name = "txtFet";
            this.txtFet.Size = new System.Drawing.Size(112, 20);
            this.txtFet.TabIndex = 66;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 65;
            this.label6.Text = "FET:";
            // 
            // txtProductor
            // 
            this.txtProductor.Location = new System.Drawing.Point(266, 7);
            this.txtProductor.Name = "txtProductor";
            this.txtProductor.Size = new System.Drawing.Size(269, 20);
            this.txtProductor.TabIndex = 64;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(189, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 63;
            this.label7.Text = "Productor:";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.checkPeriodo);
            this.groupControl2.Controls.Add(this.dpHastaOrdenPago);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.dpDesdeOrdenPago);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(3, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1146, 49);
            this.groupControl2.TabIndex = 78;
            this.groupControl2.Text = "Buscar Órdenes de Pago";
            // 
            // checkPeriodo
            // 
            this.checkPeriodo.AutoSize = true;
            this.checkPeriodo.Location = new System.Drawing.Point(370, 28);
            this.checkPeriodo.Name = "checkPeriodo";
            this.checkPeriodo.Size = new System.Drawing.Size(15, 14);
            this.checkPeriodo.TabIndex = 62;
            this.checkPeriodo.UseVisualStyleBackColor = true;
            // 
            // dpHastaOrdenPago
            // 
            this.dpHastaOrdenPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHastaOrdenPago.Location = new System.Drawing.Point(266, 24);
            this.dpHastaOrdenPago.Name = "dpHastaOrdenPago";
            this.dpHastaOrdenPago.Size = new System.Drawing.Size(99, 20);
            this.dpHastaOrdenPago.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(189, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Fecha Hasta:";
            // 
            // dpDesdeOrdenPago
            // 
            this.dpDesdeOrdenPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesdeOrdenPago.Location = new System.Drawing.Point(84, 24);
            this.dpDesdeOrdenPago.Name = "dpDesdeOrdenPago";
            this.dpDesdeOrdenPago.Size = new System.Drawing.Size(99, 20);
            this.dpDesdeOrdenPago.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Fecha Desde:";
            // 
            // AdministracionVistaOPControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl10);
            this.Controls.Add(this.groupControl6);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Name = "AdministracionVistaOPControl";
            this.Size = new System.Drawing.Size(1241, 537);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).EndInit();
            this.groupControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrdenPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrdenPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl10;
        private DevExpress.XtraEditors.SimpleButton btnFormaPago;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlOrdenPago;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrdenPago;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalles;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnBuscarLiquidacion;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton btnBuscarProductor;
        private DevExpress.XtraEditors.SimpleButton btnBuscarFet;
        private System.Windows.Forms.TextBox txtProvincia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProductor;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.CheckBox checkPeriodo;
        private System.Windows.Forms.DateTimePicker dpHastaOrdenPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpDesdeOrdenPago;
        private System.Windows.Forms.Label label3;
    }
}
