namespace CooperativaProduccion
{
    partial class Form_ProduccionConfiguracionBlend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionConfiguracionBlend));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.label4 = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.btnCerrarBlend = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevoBlend = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
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
            this.ribbon.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(330, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnSalir);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.cbProducto);
            this.groupControl2.Controls.Add(this.btnCerrarBlend);
            this.groupControl2.Controls.Add(this.btnNuevoBlend);
            this.groupControl2.Location = new System.Drawing.Point(2, 28);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(327, 84);
            this.groupControl2.TabIndex = 21;
            this.groupControl2.Text = "Configuración";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 80;
            this.label4.Text = "Blend";
            // 
            // cbProducto
            // 
            this.cbProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(78, 23);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(244, 24);
            this.cbProducto.TabIndex = 79;
            // 
            // btnCerrarBlend
            // 
            this.btnCerrarBlend.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarBlend.Appearance.Options.UseFont = true;
            this.btnCerrarBlend.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarBlend.Image")));
            this.btnCerrarBlend.Location = new System.Drawing.Point(130, 53);
            this.btnCerrarBlend.Name = "btnCerrarBlend";
            this.btnCerrarBlend.Size = new System.Drawing.Size(100, 26);
            this.btnCerrarBlend.TabIndex = 61;
            this.btnCerrarBlend.Text = "Cerrar Blend";
            this.btnCerrarBlend.Click += new System.EventHandler(this.btnCerrarBlend_Click);
            // 
            // btnNuevoBlend
            // 
            this.btnNuevoBlend.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoBlend.Appearance.Options.UseFont = true;
            this.btnNuevoBlend.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoBlend.Image")));
            this.btnNuevoBlend.Location = new System.Drawing.Point(12, 53);
            this.btnNuevoBlend.Name = "btnNuevoBlend";
            this.btnNuevoBlend.Size = new System.Drawing.Size(112, 26);
            this.btnNuevoBlend.TabIndex = 53;
            this.btnNuevoBlend.Text = "Nuevo Blend";
            this.btnNuevoBlend.Click += new System.EventHandler(this.btnNuevoBlend_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(236, 53);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(86, 26);
            this.btnSalir.TabIndex = 81;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // Form_ProduccionConfiguracionBlend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 113);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_ProduccionConfiguracionBlend";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Blend";
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
        private DevExpress.XtraEditors.SimpleButton btnCerrarBlend;
        private DevExpress.XtraEditors.SimpleButton btnNuevoBlend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbProducto;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
    }
}