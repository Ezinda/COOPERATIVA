namespace CooperativaProduccion
{
    partial class Form_RomaneoPreingreso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_RomaneoPreingreso));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnLimpiar = new DevExpress.XtraEditors.SimpleButton();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarProductor = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscarProductor = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGrabarPreingreso = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.mmObservacion = new DevExpress.XtraEditors.MemoEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRemito = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPatente = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtChofer = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTransporte = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.dgvProductores = new System.Windows.Forms.DataGridView();
            this.btnEliminarProductor = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmObservacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductores)).BeginInit();
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
            this.ribbon.Size = new System.Drawing.Size(565, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnLimpiar);
            this.groupControl2.Controls.Add(this.txtCuit);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.txtNombre);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.txtFet);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.btnAgregarProductor);
            this.groupControl2.Controls.Add(this.btnBuscarProductor);
            this.groupControl2.Controls.Add(this.txtBuscador);
            this.groupControl2.Location = new System.Drawing.Point(1, 28);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(562, 118);
            this.groupControl2.TabIndex = 20;
            this.groupControl2.Text = "Nuevo Preingreso";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Appearance.Options.UseFont = true;
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.Location = new System.Drawing.Point(421, 87);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(135, 26);
            this.btnLimpiar.TabIndex = 61;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuit.Location = new System.Drawing.Point(264, 54);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(151, 26);
            this.txtCuit.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(207, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 59;
            this.label4.Text = "C.U.I.T:";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(78, 86);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(337, 26);
            this.txtNombre.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "Productor:";
            // 
            // txtFet
            // 
            this.txtFet.Enabled = false;
            this.txtFet.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFet.Location = new System.Drawing.Point(78, 56);
            this.txtFet.Name = "txtFet";
            this.txtFet.Size = new System.Drawing.Size(124, 26);
            this.txtFet.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 55;
            this.label3.Text = "N° FET:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 54;
            this.label1.Text = "Buscador:";
            // 
            // btnAgregarProductor
            // 
            this.btnAgregarProductor.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarProductor.Appearance.Options.UseFont = true;
            this.btnAgregarProductor.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarProductor.Image")));
            this.btnAgregarProductor.Location = new System.Drawing.Point(421, 55);
            this.btnAgregarProductor.Name = "btnAgregarProductor";
            this.btnAgregarProductor.Size = new System.Drawing.Size(135, 26);
            this.btnAgregarProductor.TabIndex = 53;
            this.btnAgregarProductor.Text = "Agregar Productor";
            this.btnAgregarProductor.Click += new System.EventHandler(this.btnAgregarProductor_Click);
            this.btnAgregarProductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnAgregarProductor_KeyPress);
            // 
            // btnBuscarProductor
            // 
            this.btnBuscarProductor.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProductor.Image")));
            this.btnBuscarProductor.Location = new System.Drawing.Point(531, 24);
            this.btnBuscarProductor.Name = "btnBuscarProductor";
            this.btnBuscarProductor.Size = new System.Drawing.Size(25, 26);
            this.btnBuscarProductor.TabIndex = 39;
            this.btnBuscarProductor.Click += new System.EventHandler(this.btnBuscarProductor_Click);
            // 
            // txtBuscador
            // 
            this.txtBuscador.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(78, 24);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(449, 26);
            this.txtBuscador.TabIndex = 38;
            this.txtBuscador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscador_KeyPress);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCancelar);
            this.groupControl1.Controls.Add(this.btnGrabarPreingreso);
            this.groupControl1.Location = new System.Drawing.Point(1, 612);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(562, 53);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "Opciones";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(438, 23);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(118, 25);
            this.btnCancelar.TabIndex = 40;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGrabarPreingreso
            // 
            this.btnGrabarPreingreso.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabarPreingreso.Appearance.Options.UseFont = true;
            this.btnGrabarPreingreso.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabarPreingreso.Image")));
            this.btnGrabarPreingreso.Location = new System.Drawing.Point(306, 22);
            this.btnGrabarPreingreso.Name = "btnGrabarPreingreso";
            this.btnGrabarPreingreso.Size = new System.Drawing.Size(126, 26);
            this.btnGrabarPreingreso.TabIndex = 39;
            this.btnGrabarPreingreso.Text = "Grabar Preingreso";
            this.btnGrabarPreingreso.Click += new System.EventHandler(this.btnGrabarPreingreso_Click);
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.mmObservacion);
            this.groupControl3.Controls.Add(this.label10);
            this.groupControl3.Controls.Add(this.txtRemito);
            this.groupControl3.Controls.Add(this.label11);
            this.groupControl3.Controls.Add(this.txtPatente);
            this.groupControl3.Controls.Add(this.label12);
            this.groupControl3.Controls.Add(this.txtChofer);
            this.groupControl3.Controls.Add(this.label13);
            this.groupControl3.Controls.Add(this.txtTransporte);
            this.groupControl3.Controls.Add(this.label14);
            this.groupControl3.Location = new System.Drawing.Point(1, 408);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(562, 201);
            this.groupControl3.TabIndex = 23;
            this.groupControl3.Text = "Datos de Preingreso";
            // 
            // mmObservacion
            // 
            this.mmObservacion.Location = new System.Drawing.Point(117, 118);
            this.mmObservacion.MenuManager = this.ribbon;
            this.mmObservacion.Name = "mmObservacion";
            this.mmObservacion.Properties.Appearance.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmObservacion.Properties.Appearance.Options.UseFont = true;
            this.mmObservacion.Size = new System.Drawing.Size(439, 78);
            this.mmObservacion.TabIndex = 49;
            this.mmObservacion.UseOptimizedRendering = true;
            this.mmObservacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mmObservacion_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 20);
            this.label10.TabIndex = 48;
            this.label10.Text = "Observaciones:";
            // 
            // txtRemito
            // 
            this.txtRemito.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemito.Location = new System.Drawing.Point(340, 87);
            this.txtRemito.Name = "txtRemito";
            this.txtRemito.Size = new System.Drawing.Size(182, 26);
            this.txtRemito.TabIndex = 47;
            this.txtRemito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumRemito_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(262, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 20);
            this.label11.TabIndex = 46;
            this.label11.Text = "N° Remito:";
            // 
            // txtPatente
            // 
            this.txtPatente.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatente.Location = new System.Drawing.Point(117, 85);
            this.txtPatente.Name = "txtPatente";
            this.txtPatente.Size = new System.Drawing.Size(135, 26);
            this.txtPatente.TabIndex = 45;
            this.txtPatente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatente_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 90);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 20);
            this.label12.TabIndex = 44;
            this.label12.Text = "Patente:";
            // 
            // txtChofer
            // 
            this.txtChofer.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChofer.Location = new System.Drawing.Point(117, 56);
            this.txtChofer.Name = "txtChofer";
            this.txtChofer.Size = new System.Drawing.Size(439, 26);
            this.txtChofer.TabIndex = 43;
            this.txtChofer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChofer_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 20);
            this.label13.TabIndex = 42;
            this.label13.Text = "Chofer:";
            // 
            // txtTransporte
            // 
            this.txtTransporte.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransporte.Location = new System.Drawing.Point(117, 27);
            this.txtTransporte.Name = "txtTransporte";
            this.txtTransporte.Size = new System.Drawing.Size(439, 26);
            this.txtTransporte.TabIndex = 41;
            this.txtTransporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransporte_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 20);
            this.label14.TabIndex = 40;
            this.label14.Text = "Transporte:";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.dgvProductores);
            this.groupControl4.Controls.Add(this.btnEliminarProductor);
            this.groupControl4.Location = new System.Drawing.Point(0, 148);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(563, 258);
            this.groupControl4.TabIndex = 24;
            this.groupControl4.Text = "Lista de Productores";
            // 
            // dgvProductores
            // 
            this.dgvProductores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductores.Location = new System.Drawing.Point(5, 24);
            this.dgvProductores.MultiSelect = false;
            this.dgvProductores.Name = "dgvProductores";
            this.dgvProductores.RowHeadersVisible = false;
            this.dgvProductores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductores.Size = new System.Drawing.Size(552, 201);
            this.dgvProductores.TabIndex = 63;
            // 
            // btnEliminarProductor
            // 
            this.btnEliminarProductor.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarProductor.Appearance.Options.UseFont = true;
            this.btnEliminarProductor.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarProductor.Image")));
            this.btnEliminarProductor.Location = new System.Drawing.Point(422, 229);
            this.btnEliminarProductor.Name = "btnEliminarProductor";
            this.btnEliminarProductor.Size = new System.Drawing.Size(135, 26);
            this.btnEliminarProductor.TabIndex = 62;
            this.btnEliminarProductor.Text = "Eliminar Productor";
            this.btnEliminarProductor.Click += new System.EventHandler(this.btnEliminarProductor_Click);
            // 
            // Form_RomaneoPreingreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 667);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_RomaneoPreingreso";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Portería - Preingresos";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmObservacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnBuscarProductor;
        public System.Windows.Forms.TextBox txtBuscador;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnGrabarPreingreso;
        private DevExpress.XtraEditors.SimpleButton btnAgregarProductor;
        private DevExpress.XtraEditors.SimpleButton btnLimpiar;
        public System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.MemoEdit mmObservacion;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtRemito;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtPatente;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtChofer;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtTransporte;
        private System.Windows.Forms.Label label14;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnEliminarProductor;
        private System.Windows.Forms.DataGridView dgvProductores;
    }
}