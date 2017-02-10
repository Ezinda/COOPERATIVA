namespace CooperativaProduccion
{
    partial class Form_AdministracionFormaPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdministracionFormaPago));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPuntoVenta = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.dpFechaOrden = new System.Windows.Forms.DateTimePicker();
            this.txtProductor = new System.Windows.Forms.TextBox();
            this.txtNumOrdenPago = new System.Windows.Forms.TextBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbOrigen = new System.Windows.Forms.ComboBox();
            this.cbValor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCta = new System.Windows.Forms.ComboBox();
            this.cbcheque = new System.Windows.Forms.ComboBox();
            this.dpEmision = new System.Windows.Forms.DateTimePicker();
            this.txtImportePagar = new System.Windows.Forms.TextBox();
            this.lblEmision = new System.Windows.Forms.Label();
            this.lblVto = new System.Windows.Forms.Label();
            this.dpVto = new System.Windows.Forms.DateTimePicker();
            this.btnGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.ribbon.Size = new System.Drawing.Size(521, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.txtNumOrdenPago);
            this.groupControl2.Controls.Add(this.txtPuntoVenta);
            this.groupControl2.Controls.Add(this.txtImporte);
            this.groupControl2.Controls.Add(this.txtProductor);
            this.groupControl2.Controls.Add(this.dpFechaOrden);
            this.groupControl2.Location = new System.Drawing.Point(4, 34);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(515, 109);
            this.groupControl2.TabIndex = 23;
            this.groupControl2.Text = "Datos del Pago";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "OP N°:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Importe:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Productor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Fecha Orden:";
            // 
            // txtPuntoVenta
            // 
            this.txtPuntoVenta.Enabled = false;
            this.txtPuntoVenta.Location = new System.Drawing.Point(263, 24);
            this.txtPuntoVenta.Name = "txtPuntoVenta";
            this.txtPuntoVenta.Size = new System.Drawing.Size(100, 21);
            this.txtPuntoVenta.TabIndex = 3;
            this.txtPuntoVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtImporte
            // 
            this.txtImporte.Enabled = false;
            this.txtImporte.Location = new System.Drawing.Point(101, 78);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(150, 21);
            this.txtImporte.TabIndex = 2;
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dpFechaOrden
            // 
            this.dpFechaOrden.Enabled = false;
            this.dpFechaOrden.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpFechaOrden.Location = new System.Drawing.Point(101, 24);
            this.dpFechaOrden.Name = "dpFechaOrden";
            this.dpFechaOrden.Size = new System.Drawing.Size(100, 21);
            this.dpFechaOrden.TabIndex = 0;
            // 
            // txtProductor
            // 
            this.txtProductor.Enabled = false;
            this.txtProductor.Location = new System.Drawing.Point(101, 51);
            this.txtProductor.Name = "txtProductor";
            this.txtProductor.Size = new System.Drawing.Size(407, 21);
            this.txtProductor.TabIndex = 1;
            // 
            // txtNumOrdenPago
            // 
            this.txtNumOrdenPago.Enabled = false;
            this.txtNumOrdenPago.Location = new System.Drawing.Point(371, 24);
            this.txtNumOrdenPago.Name = "txtNumOrdenPago";
            this.txtNumOrdenPago.Size = new System.Drawing.Size(137, 21);
            this.txtNumOrdenPago.TabIndex = 4;
            this.txtNumOrdenPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnCancelar);
            this.groupControl1.Controls.Add(this.btnGrabar);
            this.groupControl1.Controls.Add(this.lblVto);
            this.groupControl1.Controls.Add(this.dpVto);
            this.groupControl1.Controls.Add(this.lblEmision);
            this.groupControl1.Controls.Add(this.txtImportePagar);
            this.groupControl1.Controls.Add(this.dpEmision);
            this.groupControl1.Controls.Add(this.cbcheque);
            this.groupControl1.Controls.Add(this.cbCta);
            this.groupControl1.Controls.Add(this.cbValor);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.cbOrigen);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Location = new System.Drawing.Point(4, 149);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(515, 141);
            this.groupControl1.TabIndex = 24;
            this.groupControl1.Text = "Forma de Pago";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Origen:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Importe:";
            // 
            // cbOrigen
            // 
            this.cbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrigen.FormattingEnabled = true;
            this.cbOrigen.Items.AddRange(new object[] {
            "Caja",
            "Cuenta Bancaria",
            "Chequera"});
            this.cbOrigen.Location = new System.Drawing.Point(79, 24);
            this.cbOrigen.Name = "cbOrigen";
            this.cbOrigen.Size = new System.Drawing.Size(193, 21);
            this.cbOrigen.TabIndex = 11;
            this.cbOrigen.SelectedIndexChanged += new System.EventHandler(this.cbOrigen_SelectedIndexChanged);
            // 
            // cbValor
            // 
            this.cbValor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValor.FormattingEnabled = true;
            this.cbValor.Location = new System.Drawing.Point(79, 51);
            this.cbValor.Name = "cbValor";
            this.cbValor.Size = new System.Drawing.Size(193, 21);
            this.cbValor.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Valor:";
            // 
            // cbCta
            // 
            this.cbCta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCta.FormattingEnabled = true;
            this.cbCta.Location = new System.Drawing.Point(283, 51);
            this.cbCta.Name = "cbCta";
            this.cbCta.Size = new System.Drawing.Size(149, 21);
            this.cbCta.TabIndex = 14;
            this.cbCta.Visible = false;
            // 
            // cbcheque
            // 
            this.cbcheque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcheque.FormattingEnabled = true;
            this.cbcheque.Location = new System.Drawing.Point(437, 51);
            this.cbcheque.Name = "cbcheque";
            this.cbcheque.Size = new System.Drawing.Size(71, 21);
            this.cbcheque.TabIndex = 15;
            this.cbcheque.Visible = false;
            // 
            // dpEmision
            // 
            this.dpEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpEmision.Location = new System.Drawing.Point(82, 108);
            this.dpEmision.Name = "dpEmision";
            this.dpEmision.Size = new System.Drawing.Size(81, 21);
            this.dpEmision.TabIndex = 16;
            this.dpEmision.Visible = false;
            // 
            // txtImportePagar
            // 
            this.txtImportePagar.Location = new System.Drawing.Point(79, 78);
            this.txtImportePagar.Name = "txtImportePagar";
            this.txtImportePagar.Size = new System.Drawing.Size(84, 21);
            this.txtImportePagar.TabIndex = 17;
            this.txtImportePagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblEmision
            // 
            this.lblEmision.AutoSize = true;
            this.lblEmision.Location = new System.Drawing.Point(5, 114);
            this.lblEmision.Name = "lblEmision";
            this.lblEmision.Size = new System.Drawing.Size(78, 13);
            this.lblEmision.TabIndex = 18;
            this.lblEmision.Text = "Fecha Emisión:";
            this.lblEmision.Visible = false;
            // 
            // lblVto
            // 
            this.lblVto.AutoSize = true;
            this.lblVto.Location = new System.Drawing.Point(169, 113);
            this.lblVto.Name = "lblVto";
            this.lblVto.Size = new System.Drawing.Size(27, 13);
            this.lblVto.TabIndex = 20;
            this.lblVto.Text = "Vto:";
            // 
            // dpVto
            // 
            this.dpVto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpVto.Location = new System.Drawing.Point(196, 108);
            this.dpVto.Name = "dpVto";
            this.dpVto.Size = new System.Drawing.Size(76, 21);
            this.dpVto.TabIndex = 19;
            this.dpVto.Visible = false;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Location = new System.Drawing.Point(352, 110);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 21;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(433, 110);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // Form_AdministracionFormaPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 293);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdministracionFormaPago";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forma de Pago";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumOrdenPago;
        private System.Windows.Forms.TextBox txtPuntoVenta;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtProductor;
        private System.Windows.Forms.DateTimePicker dpFechaOrden;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ComboBox cbCta;
        private System.Windows.Forms.ComboBox cbValor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOrigen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnGrabar;
        private System.Windows.Forms.Label lblVto;
        private System.Windows.Forms.DateTimePicker dpVto;
        private System.Windows.Forms.Label lblEmision;
        private System.Windows.Forms.TextBox txtImportePagar;
        private System.Windows.Forms.DateTimePicker dpEmision;
        private System.Windows.Forms.ComboBox cbcheque;
    }
}