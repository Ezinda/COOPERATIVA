namespace CooperativaProduccion
{
    partial class Form_ConfiguracionImpresion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ConfiguracionImpresion));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.checkDebug = new System.Windows.Forms.CheckBox();
            this.btnModificar = new DevExpress.XtraEditors.SimpleButton();
            this.checkReclasificacion = new System.Windows.Forms.CheckBox();
            this.checkBalanza = new System.Windows.Forms.CheckBox();
            this.checkGestionReclasificacion = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
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
            this.ribbon.Size = new System.Drawing.Size(352, 27);
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.checkGestionReclasificacion);
            this.groupControl5.Controls.Add(this.btnSalir);
            this.groupControl5.Controls.Add(this.checkDebug);
            this.groupControl5.Controls.Add(this.btnModificar);
            this.groupControl5.Controls.Add(this.checkReclasificacion);
            this.groupControl5.Controls.Add(this.checkBalanza);
            this.groupControl5.Location = new System.Drawing.Point(3, 30);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(346, 141);
            this.groupControl5.TabIndex = 72;
            this.groupControl5.Text = "Configuración de Impresión";
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(260, 112);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(81, 22);
            this.btnSalir.TabIndex = 66;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // checkDebug
            // 
            this.checkDebug.AutoSize = true;
            this.checkDebug.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkDebug.Location = new System.Drawing.Point(11, 103);
            this.checkDebug.Name = "checkDebug";
            this.checkDebug.Size = new System.Drawing.Size(62, 20);
            this.checkDebug.TabIndex = 66;
            this.checkDebug.Text = "Debug";
            this.checkDebug.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.Location = new System.Drawing.Point(173, 112);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(81, 22);
            this.btnModificar.TabIndex = 39;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // checkReclasificacion
            // 
            this.checkReclasificacion.AutoSize = true;
            this.checkReclasificacion.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkReclasificacion.Location = new System.Drawing.Point(11, 51);
            this.checkReclasificacion.Name = "checkReclasificacion";
            this.checkReclasificacion.Size = new System.Drawing.Size(175, 20);
            this.checkReclasificacion.TabIndex = 65;
            this.checkReclasificacion.Text = "Impresión en reclasificación";
            this.checkReclasificacion.UseVisualStyleBackColor = true;
            // 
            // checkBalanza
            // 
            this.checkBalanza.AutoSize = true;
            this.checkBalanza.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkBalanza.Location = new System.Drawing.Point(11, 25);
            this.checkBalanza.Name = "checkBalanza";
            this.checkBalanza.Size = new System.Drawing.Size(139, 20);
            this.checkBalanza.TabIndex = 64;
            this.checkBalanza.Text = "Impresión en balanza";
            this.checkBalanza.UseVisualStyleBackColor = true;
            // 
            // checkGestionReclasificacion
            // 
            this.checkGestionReclasificacion.AutoSize = true;
            this.checkGestionReclasificacion.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkGestionReclasificacion.Location = new System.Drawing.Point(11, 77);
            this.checkGestionReclasificacion.Name = "checkGestionReclasificacion";
            this.checkGestionReclasificacion.Size = new System.Drawing.Size(234, 20);
            this.checkGestionReclasificacion.TabIndex = 67;
            this.checkGestionReclasificacion.Text = "Impresión en gestión de reclasificación";
            this.checkGestionReclasificacion.UseVisualStyleBackColor = true;
            // 
            // Form_ConfiguracionImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 173);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ConfiguracionImpresion";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración - Impresión";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.SimpleButton btnModificar;
        private System.Windows.Forms.CheckBox checkReclasificacion;
        private System.Windows.Forms.CheckBox checkBalanza;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private System.Windows.Forms.CheckBox checkDebug;
        private System.Windows.Forms.CheckBox checkGestionReclasificacion;
    }
}