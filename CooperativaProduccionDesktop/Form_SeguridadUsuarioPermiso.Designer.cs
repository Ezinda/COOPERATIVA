namespace CooperativaProduccion
{
    partial class Form_SeguridadUsuarioPermiso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SeguridadUsuarioPermiso));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlUsuario = new DevExpress.XtraGrid.GridControl();
            this.gridViewUsuario = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnModificarPermiso = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnCerrar = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkGestionReclasificacion = new System.Windows.Forms.CheckBox();
            this.checkReclasificacion = new System.Windows.Forms.CheckBox();
            this.checkGestionRomaneo = new System.Windows.Forms.CheckBox();
            this.checkPesada = new System.Windows.Forms.CheckBox();
            this.checkListRomaneo = new System.Windows.Forms.CheckedListBox();
            this.checkPreingreso = new System.Windows.Forms.CheckBox();
            this.groupControl8 = new DevExpress.XtraEditors.GroupControl();
            this.checkListLiquidacion = new System.Windows.Forms.CheckedListBox();
            this.checkListaPrecio = new System.Windows.Forms.CheckBox();
            this.checkGenerarRemitoElectronico = new System.Windows.Forms.CheckBox();
            this.checkGenerarOrdenVenta = new System.Windows.Forms.CheckBox();
            this.checkGestionCaja = new System.Windows.Forms.CheckBox();
            this.checkGestionCata = new System.Windows.Forms.CheckBox();
            this.checkGenerarOrdenPago = new System.Windows.Forms.CheckBox();
            this.checkLiquidacion = new System.Windows.Forms.CheckBox();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.checkConfiguracion = new System.Windows.Forms.CheckBox();
            this.checkSeguridad = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).BeginInit();
            this.groupControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
            this.groupControl7.SuspendLayout();
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
            this.ribbon.Size = new System.Drawing.Size(611, 27);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControlUsuario);
            this.groupControl2.Location = new System.Drawing.Point(2, 29);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(366, 200);
            this.groupControl2.TabIndex = 18;
            this.groupControl2.Text = "Nuevo Usuario";
            // 
            // gridControlUsuario
            // 
            this.gridControlUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlUsuario.Location = new System.Drawing.Point(2, 21);
            this.gridControlUsuario.MainView = this.gridViewUsuario;
            this.gridControlUsuario.MenuManager = this.ribbon;
            this.gridControlUsuario.Name = "gridControlUsuario";
            this.gridControlUsuario.Size = new System.Drawing.Size(362, 177);
            this.gridControlUsuario.TabIndex = 0;
            this.gridControlUsuario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsuario});
            this.gridControlUsuario.DoubleClick += new System.EventHandler(this.gridControlUsuario_DoubleClick);
            // 
            // gridViewUsuario
            // 
            this.gridViewUsuario.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewUsuario.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewUsuario.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewUsuario.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewUsuario.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewUsuario.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewUsuario.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewUsuario.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewUsuario.GridControl = this.gridControlUsuario;
            this.gridViewUsuario.Name = "gridViewUsuario";
            this.gridViewUsuario.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewUsuario.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewUsuario.OptionsBehavior.Editable = false;
            this.gridViewUsuario.OptionsView.ShowGroupPanel = false;
            // 
            // btnModificarPermiso
            // 
            this.btnModificarPermiso.Image = ((System.Drawing.Image)(resources.GetObject("btnModificarPermiso.Image")));
            this.btnModificarPermiso.Location = new System.Drawing.Point(367, 5);
            this.btnModificarPermiso.Name = "btnModificarPermiso";
            this.btnModificarPermiso.Size = new System.Drawing.Size(115, 23);
            this.btnModificarPermiso.TabIndex = 32;
            this.btnModificarPermiso.Text = "Modificar Permisos";
            this.btnModificarPermiso.Click += new System.EventHandler(this.btnModificarPermiso_Click);
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.btnModificarPermiso);
            this.groupControl4.Controls.Add(this.btnCerrar);
            this.groupControl4.Location = new System.Drawing.Point(2, 445);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.ShowCaption = false;
            this.groupControl4.Size = new System.Drawing.Size(605, 31);
            this.groupControl4.TabIndex = 36;
            this.groupControl4.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(487, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(113, 23);
            this.btnCerrar.TabIndex = 32;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkGestionReclasificacion);
            this.groupControl1.Controls.Add(this.checkReclasificacion);
            this.groupControl1.Controls.Add(this.checkGestionRomaneo);
            this.groupControl1.Controls.Add(this.checkPesada);
            this.groupControl1.Controls.Add(this.checkListRomaneo);
            this.groupControl1.Controls.Add(this.checkPreingreso);
            this.groupControl1.Location = new System.Drawing.Point(3, 231);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(364, 213);
            this.groupControl1.TabIndex = 38;
            this.groupControl1.Text = "Permisos del Módulo de Romaneo";
            // 
            // checkGestionReclasificacion
            // 
            this.checkGestionReclasificacion.AutoSize = true;
            this.checkGestionReclasificacion.Location = new System.Drawing.Point(10, 193);
            this.checkGestionReclasificacion.Name = "checkGestionReclasificacion";
            this.checkGestionReclasificacion.Size = new System.Drawing.Size(232, 17);
            this.checkGestionReclasificacion.TabIndex = 33;
            this.checkGestionReclasificacion.Text = "Puede acceder a Gestión de Reclasificación";
            this.checkGestionReclasificacion.UseVisualStyleBackColor = true;
            // 
            // checkReclasificacion
            // 
            this.checkReclasificacion.AutoSize = true;
            this.checkReclasificacion.Location = new System.Drawing.Point(10, 174);
            this.checkReclasificacion.Name = "checkReclasificacion";
            this.checkReclasificacion.Size = new System.Drawing.Size(166, 17);
            this.checkReclasificacion.TabIndex = 32;
            this.checkReclasificacion.Text = "Puede realizar Reclasificación";
            this.checkReclasificacion.UseVisualStyleBackColor = true;
            // 
            // checkGestionRomaneo
            // 
            this.checkGestionRomaneo.AutoSize = true;
            this.checkGestionRomaneo.Location = new System.Drawing.Point(8, 66);
            this.checkGestionRomaneo.Name = "checkGestionRomaneo";
            this.checkGestionRomaneo.Size = new System.Drawing.Size(208, 17);
            this.checkGestionRomaneo.TabIndex = 31;
            this.checkGestionRomaneo.Text = "Puede acceder a Gestión de Romaneo";
            this.checkGestionRomaneo.UseVisualStyleBackColor = true;
            this.checkGestionRomaneo.CheckedChanged += new System.EventHandler(this.checkGestionRomaneo_CheckedChanged);
            // 
            // checkPesada
            // 
            this.checkPesada.AutoSize = true;
            this.checkPesada.Location = new System.Drawing.Point(8, 45);
            this.checkPesada.Name = "checkPesada";
            this.checkPesada.Size = new System.Drawing.Size(137, 17);
            this.checkPesada.TabIndex = 30;
            this.checkPesada.Text = "Puede realizar Pesadas";
            this.checkPesada.UseVisualStyleBackColor = true;
            // 
            // checkListRomaneo
            // 
            this.checkListRomaneo.FormattingEnabled = true;
            this.checkListRomaneo.Items.AddRange(new object[] {
            "Reimpresión de Romaneo",
            "Resumen de Romaneo",
            "Resumen de Compra",
            "Resumen de Clases por Mes",
            "Resumen de Clases por Trimestre"});
            this.checkListRomaneo.Location = new System.Drawing.Point(22, 84);
            this.checkListRomaneo.Name = "checkListRomaneo";
            this.checkListRomaneo.Size = new System.Drawing.Size(190, 84);
            this.checkListRomaneo.TabIndex = 34;
            // 
            // checkPreingreso
            // 
            this.checkPreingreso.AutoSize = true;
            this.checkPreingreso.Location = new System.Drawing.Point(8, 24);
            this.checkPreingreso.Name = "checkPreingreso";
            this.checkPreingreso.Size = new System.Drawing.Size(165, 17);
            this.checkPreingreso.TabIndex = 29;
            this.checkPreingreso.Text = "Puede realizar Pre - Ingresos";
            this.checkPreingreso.UseVisualStyleBackColor = true;
            // 
            // groupControl8
            // 
            this.groupControl8.Controls.Add(this.checkListLiquidacion);
            this.groupControl8.Controls.Add(this.checkListaPrecio);
            this.groupControl8.Controls.Add(this.checkGenerarRemitoElectronico);
            this.groupControl8.Controls.Add(this.checkGenerarOrdenVenta);
            this.groupControl8.Controls.Add(this.checkGestionCaja);
            this.groupControl8.Controls.Add(this.checkGestionCata);
            this.groupControl8.Controls.Add(this.checkGenerarOrdenPago);
            this.groupControl8.Controls.Add(this.checkLiquidacion);
            this.groupControl8.Location = new System.Drawing.Point(369, 97);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.Size = new System.Drawing.Size(238, 347);
            this.groupControl8.TabIndex = 46;
            this.groupControl8.Text = "Accesos del Módulo de Administración";
            // 
            // checkListLiquidacion
            // 
            this.checkListLiquidacion.FormattingEnabled = true;
            this.checkListLiquidacion.Items.AddRange(new object[] {
            "Liquidar",
            "Subir a Afip",
            "Imprimir Liquidación"});
            this.checkListLiquidacion.Location = new System.Drawing.Point(20, 41);
            this.checkListLiquidacion.Name = "checkListLiquidacion";
            this.checkListLiquidacion.Size = new System.Drawing.Size(190, 52);
            this.checkListLiquidacion.TabIndex = 37;
            // 
            // checkListaPrecio
            // 
            this.checkListaPrecio.AutoSize = true;
            this.checkListaPrecio.Location = new System.Drawing.Point(7, 214);
            this.checkListaPrecio.Name = "checkListaPrecio";
            this.checkListaPrecio.Size = new System.Drawing.Size(181, 17);
            this.checkListaPrecio.TabIndex = 36;
            this.checkListaPrecio.Text = "Puede gestionar Lista de Precios";
            this.checkListaPrecio.UseVisualStyleBackColor = true;
            // 
            // checkGenerarRemitoElectronico
            // 
            this.checkGenerarRemitoElectronico.AutoSize = true;
            this.checkGenerarRemitoElectronico.Location = new System.Drawing.Point(8, 190);
            this.checkGenerarRemitoElectronico.Name = "checkGenerarRemitoElectronico";
            this.checkGenerarRemitoElectronico.Size = new System.Drawing.Size(198, 17);
            this.checkGenerarRemitoElectronico.TabIndex = 35;
            this.checkGenerarRemitoElectronico.Text = "Puede generar Remitos Electrónicos";
            this.checkGenerarRemitoElectronico.UseVisualStyleBackColor = true;
            // 
            // checkGenerarOrdenVenta
            // 
            this.checkGenerarOrdenVenta.AutoSize = true;
            this.checkGenerarOrdenVenta.Location = new System.Drawing.Point(8, 167);
            this.checkGenerarOrdenVenta.Name = "checkGenerarOrdenVenta";
            this.checkGenerarOrdenVenta.Size = new System.Drawing.Size(187, 17);
            this.checkGenerarOrdenVenta.TabIndex = 34;
            this.checkGenerarOrdenVenta.Text = "Puede generar Órdenes de Venta";
            this.checkGenerarOrdenVenta.UseVisualStyleBackColor = true;
            // 
            // checkGestionCaja
            // 
            this.checkGestionCaja.AutoSize = true;
            this.checkGestionCaja.Location = new System.Drawing.Point(8, 144);
            this.checkGestionCaja.Name = "checkGestionCaja";
            this.checkGestionCaja.Size = new System.Drawing.Size(190, 17);
            this.checkGestionCaja.TabIndex = 33;
            this.checkGestionCaja.Text = "Puede acceder a Gestión de Cajas";
            this.checkGestionCaja.UseVisualStyleBackColor = true;
            // 
            // checkGestionCata
            // 
            this.checkGestionCata.AutoSize = true;
            this.checkGestionCata.Location = new System.Drawing.Point(8, 121);
            this.checkGestionCata.Name = "checkGestionCata";
            this.checkGestionCata.Size = new System.Drawing.Size(186, 17);
            this.checkGestionCata.TabIndex = 32;
            this.checkGestionCata.Text = "Puede acceder a Gestión de Cata";
            this.checkGestionCata.UseVisualStyleBackColor = true;
            // 
            // checkGenerarOrdenPago
            // 
            this.checkGenerarOrdenPago.AutoSize = true;
            this.checkGenerarOrdenPago.Location = new System.Drawing.Point(8, 98);
            this.checkGenerarOrdenPago.Name = "checkGenerarOrdenPago";
            this.checkGenerarOrdenPago.Size = new System.Drawing.Size(183, 17);
            this.checkGenerarOrdenPago.TabIndex = 31;
            this.checkGenerarOrdenPago.Text = "Puede generar Órdenes de Pago";
            this.checkGenerarOrdenPago.UseVisualStyleBackColor = true;
            // 
            // checkLiquidacion
            // 
            this.checkLiquidacion.AutoSize = true;
            this.checkLiquidacion.Location = new System.Drawing.Point(8, 22);
            this.checkLiquidacion.Name = "checkLiquidacion";
            this.checkLiquidacion.Size = new System.Drawing.Size(160, 17);
            this.checkLiquidacion.TabIndex = 30;
            this.checkLiquidacion.Text = "Puede realizar Liquidaciones";
            this.checkLiquidacion.UseVisualStyleBackColor = true;
            this.checkLiquidacion.CheckedChanged += new System.EventHandler(this.checkLiquidacion_CheckedChanged);
            // 
            // groupControl7
            // 
            this.groupControl7.Controls.Add(this.checkConfiguracion);
            this.groupControl7.Controls.Add(this.checkSeguridad);
            this.groupControl7.Location = new System.Drawing.Point(372, 29);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.Size = new System.Drawing.Size(238, 66);
            this.groupControl7.TabIndex = 52;
            this.groupControl7.Text = "Permisos de Administrador";
            // 
            // checkConfiguracion
            // 
            this.checkConfiguracion.AutoSize = true;
            this.checkConfiguracion.Location = new System.Drawing.Point(8, 45);
            this.checkConfiguracion.Name = "checkConfiguracion";
            this.checkConfiguracion.Size = new System.Drawing.Size(229, 17);
            this.checkConfiguracion.TabIndex = 31;
            this.checkConfiguracion.Text = "Puede acceder al módulo de Configuración";
            this.checkConfiguracion.UseVisualStyleBackColor = true;
            // 
            // checkSeguridad
            // 
            this.checkSeguridad.AutoSize = true;
            this.checkSeguridad.Location = new System.Drawing.Point(7, 24);
            this.checkSeguridad.Name = "checkSeguridad";
            this.checkSeguridad.Size = new System.Drawing.Size(211, 17);
            this.checkSeguridad.TabIndex = 30;
            this.checkSeguridad.Text = "Puede acceder al módulo de Seguridad";
            this.checkSeguridad.UseVisualStyleBackColor = true;
            // 
            // Form_SeguridadUsuarioPermiso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 477);
            this.Controls.Add(this.groupControl7);
            this.Controls.Add(this.groupControl8);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_SeguridadUsuarioPermiso";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignacion de Permisos";
            this.Load += new System.EventHandler(this.Sys_FormUsuarioPermiso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).EndInit();
            this.groupControl8.ResumeLayout(false);
            this.groupControl8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            this.groupControl7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControlUsuario;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsuario;
        private DevExpress.XtraEditors.SimpleButton btnModificarPermiso;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnCerrar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.CheckBox checkPesada;
        private System.Windows.Forms.CheckBox checkPreingreso;
        private DevExpress.XtraEditors.GroupControl groupControl8;
        private System.Windows.Forms.CheckBox checkListaPrecio;
        private System.Windows.Forms.CheckBox checkGenerarRemitoElectronico;
        private System.Windows.Forms.CheckBox checkGenerarOrdenVenta;
        private System.Windows.Forms.CheckBox checkGestionCaja;
        private System.Windows.Forms.CheckBox checkGestionCata;
        private System.Windows.Forms.CheckBox checkGenerarOrdenPago;
        private System.Windows.Forms.CheckBox checkLiquidacion;
        private System.Windows.Forms.CheckBox checkGestionReclasificacion;
        private System.Windows.Forms.CheckBox checkReclasificacion;
        private System.Windows.Forms.CheckBox checkGestionRomaneo;
        private System.Windows.Forms.CheckedListBox checkListRomaneo;
        private System.Windows.Forms.CheckedListBox checkListLiquidacion;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private System.Windows.Forms.CheckBox checkConfiguracion;
        private System.Windows.Forms.CheckBox checkSeguridad;
    }
}