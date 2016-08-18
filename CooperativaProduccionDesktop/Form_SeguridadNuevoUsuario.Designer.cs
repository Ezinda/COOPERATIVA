namespace CooperativaProduccion
{
    partial class Form_SeguridadNuevoUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SeguridadNuevoUsuario));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnGenerarUser = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtRePass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCrearUsuario = new DevExpress.XtraEditors.SimpleButton();
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
            this.ribbon.Size = new System.Drawing.Size(312, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtApellido);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.txtNombre);
            this.groupControl2.Controls.Add(this.btnGenerarUser);
            this.groupControl2.Location = new System.Drawing.Point(3, 30);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(308, 114);
            this.groupControl2.TabIndex = 19;
            this.groupControl2.Text = "Nuevo Usuario";
            // 
            // txtApellido
            // 
            this.txtApellido.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellido.Location = new System.Drawing.Point(61, 55);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(234, 23);
            this.txtApellido.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Nombre:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(61, 26);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(234, 23);
            this.txtNombre.TabIndex = 36;
            // 
            // btnGenerarUser
            // 
            this.btnGenerarUser.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarUser.Image")));
            this.btnGenerarUser.Location = new System.Drawing.Point(138, 84);
            this.btnGenerarUser.Name = "btnGenerarUser";
            this.btnGenerarUser.Size = new System.Drawing.Size(157, 23);
            this.btnGenerarUser.TabIndex = 28;
            this.btnGenerarUser.Text = "Generar Cuenta";
            this.btnGenerarUser.Click += new System.EventHandler(this.btnGenerarUser_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtRePass);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtPass);
            this.groupControl1.Controls.Add(this.label41);
            this.groupControl1.Controls.Add(this.label42);
            this.groupControl1.Controls.Add(this.txtUser);
            this.groupControl1.Location = new System.Drawing.Point(3, 146);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(308, 114);
            this.groupControl1.TabIndex = 18;
            this.groupControl1.Text = "Nuevo Usuario";
            // 
            // txtRePass
            // 
            this.txtRePass.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRePass.Location = new System.Drawing.Point(122, 81);
            this.txtRePass.Name = "txtRePass";
            this.txtRePass.Size = new System.Drawing.Size(173, 23);
            this.txtRePass.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Confirmar Contraseña:";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(122, 52);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(173, 23);
            this.txtPass.TabIndex = 30;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(9, 30);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(47, 13);
            this.label41.TabIndex = 26;
            this.label41.Text = "Usuario:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 57);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(67, 13);
            this.label42.TabIndex = 29;
            this.label42.Text = "Contraseña:";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(122, 25);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(173, 23);
            this.txtUser.TabIndex = 27;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(240, 266);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(71, 23);
            this.btnCancelar.TabIndex = 33;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCrearUsuario
            // 
            this.btnCrearUsuario.Image = ((System.Drawing.Image)(resources.GetObject("btnCrearUsuario.Image")));
            this.btnCrearUsuario.Location = new System.Drawing.Point(134, 266);
            this.btnCrearUsuario.Name = "btnCrearUsuario";
            this.btnCrearUsuario.Size = new System.Drawing.Size(100, 23);
            this.btnCrearUsuario.TabIndex = 32;
            this.btnCrearUsuario.Text = "Crear Usuario";
            this.btnCrearUsuario.Click += new System.EventHandler(this.btnCrearUsuario_Click);
            // 
            // Form_SeguridadNuevoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 294);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrearUsuario);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.ribbon);
            this.MaximizeBox = false;
            this.Name = "Form_SeguridadNuevoUsuario";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo Usuario";
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
        public System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtNombre;
        private DevExpress.XtraEditors.SimpleButton btnGenerarUser;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public System.Windows.Forms.TextBox txtRePass;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        public System.Windows.Forms.TextBox txtUser;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnCrearUsuario;
    }
}