namespace CooperativaProduccion
{
    partial class Form_RomaneoReclasificacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_RomaneoReclasificacion));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkAutomaticaClase = new System.Windows.Forms.CheckBox();
            this.txtReclasificacion = new System.Windows.Forms.TextBox();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.txtFardo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
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
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(473, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.checkAutomaticaClase);
            this.groupControl2.Controls.Add(this.txtReclasificacion);
            this.groupControl2.Controls.Add(this.btnSalir);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.txtClase);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.btnActualizar);
            this.groupControl2.Controls.Add(this.txtFardo);
            this.groupControl2.Location = new System.Drawing.Point(3, 30);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(468, 173);
            this.groupControl2.TabIndex = 21;
            this.groupControl2.Text = "Fardo";
            // 
            // checkAutomaticaClase
            // 
            this.checkAutomaticaClase.AutoSize = true;
            this.checkAutomaticaClase.Checked = true;
            this.checkAutomaticaClase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAutomaticaClase.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAutomaticaClase.Location = new System.Drawing.Point(352, 108);
            this.checkAutomaticaClase.Name = "checkAutomaticaClase";
            this.checkAutomaticaClase.Size = new System.Drawing.Size(111, 22);
            this.checkAutomaticaClase.TabIndex = 63;
            this.checkAutomaticaClase.Text = "Automático";
            this.checkAutomaticaClase.UseVisualStyleBackColor = true;
            this.checkAutomaticaClase.CheckedChanged += new System.EventHandler(this.checkAutomaticaClase_CheckedChanged);
            // 
            // txtReclasificacion
            // 
            this.txtReclasificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReclasificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReclasificacion.Location = new System.Drawing.Point(127, 103);
            this.txtReclasificacion.Name = "txtReclasificacion";
            this.txtReclasificacion.Size = new System.Drawing.Size(221, 29);
            this.txtReclasificacion.TabIndex = 1;
            this.txtReclasificacion.TextChanged += new System.EventHandler(this.txtReclasificacion_TextChanged);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(371, 142);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(92, 26);
            this.btnSalir.TabIndex = 61;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 24);
            this.label2.TabIndex = 57;
            this.label2.Text = "Reclasificar";
            // 
            // txtClase
            // 
            this.txtClase.Enabled = false;
            this.txtClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClase.Location = new System.Drawing.Point(127, 63);
            this.txtClase.Name = "txtClase";
            this.txtClase.Size = new System.Drawing.Size(221, 29);
            this.txtClase.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 55;
            this.label3.Text = "Clase";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 54;
            this.label1.Text = "N° Fardo";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Appearance.Options.UseFont = true;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Location = new System.Drawing.Point(273, 142);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(92, 26);
            this.btnActualizar.TabIndex = 53;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txtFardo
            // 
            this.txtFardo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFardo.Location = new System.Drawing.Point(127, 22);
            this.txtFardo.Name = "txtFardo";
            this.txtFardo.Size = new System.Drawing.Size(221, 29);
            this.txtFardo.TabIndex = 2;
            // 
            // Form_RomaneoReclasificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 205);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_RomaneoReclasificacion";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reclasificación de Fardos";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtClase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnActualizar;
        public System.Windows.Forms.TextBox txtFardo;
        public System.Windows.Forms.TextBox txtReclasificacion;
        private System.Windows.Forms.CheckBox checkAutomaticaClase;
    }
}