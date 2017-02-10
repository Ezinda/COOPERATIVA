namespace CooperativaProduccion
{
    partial class Form_RomaneoPendiente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_RomaneoPendiente));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btnCerrarPesada = new DevExpress.XtraEditors.SimpleButton();
            this.btnContinuarPesada = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
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
            this.ribbon.Size = new System.Drawing.Size(446, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(430, 16);
            this.label3.TabIndex = 55;
            this.label3.Text = "Existe una pesada pendiente. ¿Desea continuar con la misma pesada o cerrarla?";
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(341, 64);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(93, 26);
            this.btnSalir.TabIndex = 63;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCerrarPesada
            // 
            this.btnCerrarPesada.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarPesada.Image")));
            this.btnCerrarPesada.Location = new System.Drawing.Point(221, 64);
            this.btnCerrarPesada.Name = "btnCerrarPesada";
            this.btnCerrarPesada.Size = new System.Drawing.Size(114, 26);
            this.btnCerrarPesada.TabIndex = 62;
            this.btnCerrarPesada.Text = "Cerrar Pendiente";
            this.btnCerrarPesada.Click += new System.EventHandler(this.btnCerrarPesada_Click);
            // 
            // btnContinuarPesada
            // 
            this.btnContinuarPesada.Image = ((System.Drawing.Image)(resources.GetObject("btnContinuarPesada.Image")));
            this.btnContinuarPesada.Location = new System.Drawing.Point(92, 64);
            this.btnContinuarPesada.Name = "btnContinuarPesada";
            this.btnContinuarPesada.Size = new System.Drawing.Size(123, 26);
            this.btnContinuarPesada.TabIndex = 64;
            this.btnContinuarPesada.Text = "Continuar Pesada";
            this.btnContinuarPesada.Click += new System.EventHandler(this.btnContinuarPesada_Click);
            // 
            // Form_RomaneoPendiente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 102);
            this.Controls.Add(this.btnContinuarPesada);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCerrarPesada);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_RomaneoPendiente";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atención - Pesada Pendiente";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.SimpleButton btnCerrarPesada;
        private DevExpress.XtraEditors.SimpleButton btnContinuarPesada;
    }
}