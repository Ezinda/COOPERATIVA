namespace CooperativaProduccion
{
    partial class Form_SysPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SysPrincipal));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnPreingreso = new DevExpress.XtraBars.BarButtonItem();
            this.btnPesada = new DevExpress.XtraBars.BarButtonItem();
            this.btnClasificacion = new DevExpress.XtraBars.BarButtonItem();
            this.btnNuevoUsuario = new DevExpress.XtraBars.BarButtonItem();
            this.btnAsignarRoles = new DevExpress.XtraBars.BarButtonItem();
            this.btnVerUsuario = new DevExpress.XtraBars.BarButtonItem();
            this.btnGestionProductores = new DevExpress.XtraBars.BarButtonItem();
            this.btnFardos = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageRomaneo = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupPorteria = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupBalanza = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClasificacion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupProductores = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupFardos = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPage6 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSeguridad = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnPreingreso,
            this.btnPesada,
            this.btnClasificacion,
            this.btnNuevoUsuario,
            this.btnAsignarRoles,
            this.btnVerUsuario,
            this.btnGestionProductores,
            this.btnFardos});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 10;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageRomaneo,
            this.ribbonPage2,
            this.ribbonPage3,
            this.ribbonPage4,
            this.ribbonPage5,
            this.ribbonPage6});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowCategoryInCaption = false;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(1094, 147);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // btnPreingreso
            // 
            this.btnPreingreso.Caption = "Pre-Ingreso";
            this.btnPreingreso.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPreingreso.Glyph")));
            this.btnPreingreso.Id = 1;
            this.btnPreingreso.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnPreingreso.LargeGlyph")));
            this.btnPreingreso.Name = "btnPreingreso";
            this.btnPreingreso.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPreingreso_ItemClick);
            // 
            // btnPesada
            // 
            this.btnPesada.Caption = "Pesada";
            this.btnPesada.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPesada.Glyph")));
            this.btnPesada.Id = 2;
            this.btnPesada.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnPesada.LargeGlyph")));
            this.btnPesada.Name = "btnPesada";
            this.btnPesada.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPesada_ItemClick);
            // 
            // btnClasificacion
            // 
            this.btnClasificacion.Caption = "Clasificación";
            this.btnClasificacion.Glyph = ((System.Drawing.Image)(resources.GetObject("btnClasificacion.Glyph")));
            this.btnClasificacion.Id = 3;
            this.btnClasificacion.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnClasificacion.LargeGlyph")));
            this.btnClasificacion.Name = "btnClasificacion";
            this.btnClasificacion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClasificacion_ItemClick);
            // 
            // btnNuevoUsuario
            // 
            this.btnNuevoUsuario.Caption = "Nuevo Usuario";
            this.btnNuevoUsuario.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNuevoUsuario.Glyph")));
            this.btnNuevoUsuario.Id = 4;
            this.btnNuevoUsuario.Name = "btnNuevoUsuario";
            this.btnNuevoUsuario.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevoUsuario.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevoUsuario_ItemClick);
            // 
            // btnAsignarRoles
            // 
            this.btnAsignarRoles.Caption = "Asignar Roles";
            this.btnAsignarRoles.Glyph = ((System.Drawing.Image)(resources.GetObject("btnAsignarRoles.Glyph")));
            this.btnAsignarRoles.Id = 5;
            this.btnAsignarRoles.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnAsignarRoles.LargeGlyph")));
            this.btnAsignarRoles.Name = "btnAsignarRoles";
            this.btnAsignarRoles.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // btnVerUsuario
            // 
            this.btnVerUsuario.Caption = "Visualizar Usuarios";
            this.btnVerUsuario.Glyph = ((System.Drawing.Image)(resources.GetObject("btnVerUsuario.Glyph")));
            this.btnVerUsuario.Id = 6;
            this.btnVerUsuario.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnVerUsuario.LargeGlyph")));
            this.btnVerUsuario.Name = "btnVerUsuario";
            this.btnVerUsuario.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnVerUsuario.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnVerUsuario_ItemClick);
            // 
            // btnGestionProductores
            // 
            this.btnGestionProductores.Caption = "Gestión de Productores";
            this.btnGestionProductores.Glyph = ((System.Drawing.Image)(resources.GetObject("btnGestionProductores.Glyph")));
            this.btnGestionProductores.Id = 7;
            this.btnGestionProductores.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnGestionProductores.LargeGlyph")));
            this.btnGestionProductores.Name = "btnGestionProductores";
            // 
            // btnFardos
            // 
            this.btnFardos.Caption = "Administración de Fardos";
            this.btnFardos.Glyph = ((System.Drawing.Image)(resources.GetObject("btnFardos.Glyph")));
            this.btnFardos.Id = 8;
            this.btnFardos.Name = "btnFardos";
            this.btnFardos.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText)));
            this.btnFardos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFardos_ItemClick);
            // 
            // ribbonPageRomaneo
            // 
            this.ribbonPageRomaneo.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupPorteria,
            this.ribbonPageGroupBalanza,
            this.ribbonPageGroupClasificacion});
            this.ribbonPageRomaneo.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPageRomaneo.Image")));
            this.ribbonPageRomaneo.Name = "ribbonPageRomaneo";
            this.ribbonPageRomaneo.Text = "Romaneo";
            // 
            // ribbonPageGroupPorteria
            // 
            this.ribbonPageGroupPorteria.AllowTextClipping = false;
            this.ribbonPageGroupPorteria.ItemLinks.Add(this.btnPreingreso);
            this.ribbonPageGroupPorteria.Name = "ribbonPageGroupPorteria";
            this.ribbonPageGroupPorteria.ShowCaptionButton = false;
            this.ribbonPageGroupPorteria.Text = "Portería";
            // 
            // ribbonPageGroupBalanza
            // 
            this.ribbonPageGroupBalanza.AllowTextClipping = false;
            this.ribbonPageGroupBalanza.ItemLinks.Add(this.btnPesada);
            this.ribbonPageGroupBalanza.Name = "ribbonPageGroupBalanza";
            this.ribbonPageGroupBalanza.ShowCaptionButton = false;
            this.ribbonPageGroupBalanza.Text = "Balanza";
            // 
            // ribbonPageGroupClasificacion
            // 
            this.ribbonPageGroupClasificacion.AllowTextClipping = false;
            this.ribbonPageGroupClasificacion.ItemLinks.Add(this.btnClasificacion);
            this.ribbonPageGroupClasificacion.Name = "ribbonPageGroupClasificacion";
            this.ribbonPageGroupClasificacion.ShowCaptionButton = false;
            this.ribbonPageGroupClasificacion.Text = "Clasificación";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupProductores});
            this.ribbonPage2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPage2.Image")));
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Administración";
            // 
            // ribbonPageGroupProductores
            // 
            this.ribbonPageGroupProductores.AllowTextClipping = false;
            this.ribbonPageGroupProductores.ItemLinks.Add(this.btnGestionProductores);
            this.ribbonPageGroupProductores.Name = "ribbonPageGroupProductores";
            this.ribbonPageGroupProductores.ShowCaptionButton = false;
            this.ribbonPageGroupProductores.Text = "Productores";
            // 
            // ribbonPage3
            // 
            this.ribbonPage3.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPage3.Image")));
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "Producción";
            // 
            // ribbonPage4
            // 
            this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupFardos});
            this.ribbonPage4.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPage4.Image")));
            this.ribbonPage4.Name = "ribbonPage4";
            this.ribbonPage4.Text = "Inventario";
            // 
            // ribbonPageGroupFardos
            // 
            this.ribbonPageGroupFardos.AllowTextClipping = false;
            this.ribbonPageGroupFardos.Glyph = ((System.Drawing.Image)(resources.GetObject("ribbonPageGroupFardos.Glyph")));
            this.ribbonPageGroupFardos.ItemLinks.Add(this.btnFardos);
            this.ribbonPageGroupFardos.Name = "ribbonPageGroupFardos";
            this.ribbonPageGroupFardos.ShowCaptionButton = false;
            this.ribbonPageGroupFardos.Text = "Materia Prima";
            // 
            // ribbonPage5
            // 
            this.ribbonPage5.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPage5.Image")));
            this.ribbonPage5.Name = "ribbonPage5";
            this.ribbonPage5.Text = "Configuración";
            // 
            // ribbonPage6
            // 
            this.ribbonPage6.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSeguridad});
            this.ribbonPage6.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPage6.Image")));
            this.ribbonPage6.Name = "ribbonPage6";
            this.ribbonPage6.Text = "Seguridad";
            // 
            // ribbonPageGroupSeguridad
            // 
            this.ribbonPageGroupSeguridad.AllowTextClipping = false;
            this.ribbonPageGroupSeguridad.ItemLinks.Add(this.btnNuevoUsuario);
            this.ribbonPageGroupSeguridad.ItemLinks.Add(this.btnAsignarRoles);
            this.ribbonPageGroupSeguridad.ItemLinks.Add(this.btnVerUsuario);
            this.ribbonPageGroupSeguridad.Name = "ribbonPageGroupSeguridad";
            this.ribbonPageGroupSeguridad.ShowCaptionButton = false;
            this.ribbonPageGroupSeguridad.Text = "Seguridad";
            // 
            // Form_SysPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 669);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_SysPrincipal";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestión de Tabacaleros";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageRomaneo;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupPorteria;
        private DevExpress.XtraBars.BarButtonItem btnPreingreso;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage5;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage6;
        private DevExpress.XtraBars.BarButtonItem btnPesada;
        private DevExpress.XtraBars.BarButtonItem btnClasificacion;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupBalanza;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClasificacion;
        private DevExpress.XtraBars.BarButtonItem btnNuevoUsuario;
        private DevExpress.XtraBars.BarButtonItem btnAsignarRoles;
        private DevExpress.XtraBars.BarButtonItem btnVerUsuario;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSeguridad;
        private DevExpress.XtraBars.BarButtonItem btnGestionProductores;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupProductores;
        private DevExpress.XtraBars.BarButtonItem btnFardos;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupFardos;
    }
}