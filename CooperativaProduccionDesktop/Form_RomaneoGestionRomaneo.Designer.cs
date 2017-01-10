﻿namespace CooperativaProduccion
{
    partial class Form_RomaneoGestionRomaneo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_RomaneoGestionRomaneo));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
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
            this.dpHastaRomaneo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dpDesdeRomaneo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnBuscarRomaneo = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlRomaneo = new DevExpress.XtraGrid.GridControl();
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewRomaneo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrevisualizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirAfip = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRomaneo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRomaneo)).BeginInit();
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
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.Size = new System.Drawing.Size(1193, 49);
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
            this.groupControl3.Location = new System.Drawing.Point(1, 102);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.ShowCaption = false;
            this.groupControl3.Size = new System.Drawing.Size(1101, 32);
            this.groupControl3.TabIndex = 75;
            this.groupControl3.Text = "Buscar Romaneo";
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Location = new System.Drawing.Point(616, 7);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(153, 21);
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
            this.btnBuscarProductor.Click += new System.EventHandler(this.btnBuscarProductor_Click);
            // 
            // btnBuscarFet
            // 
            this.btnBuscarFet.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarFet.Image")));
            this.btnBuscarFet.Location = new System.Drawing.Point(155, 7);
            this.btnBuscarFet.Name = "btnBuscarFet";
            this.btnBuscarFet.Size = new System.Drawing.Size(28, 22);
            this.btnBuscarFet.TabIndex = 69;
            // 
            // txtProvincia
            // 
            this.txtProvincia.Enabled = false;
            this.txtProvincia.Location = new System.Drawing.Point(836, 7);
            this.txtProvincia.Name = "txtProvincia";
            this.txtProvincia.Size = new System.Drawing.Size(117, 21);
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
            this.txtFet.Size = new System.Drawing.Size(112, 21);
            this.txtFet.TabIndex = 66;
            this.txtFet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFet_KeyPress);
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
            this.txtProductor.Size = new System.Drawing.Size(269, 21);
            this.txtProductor.TabIndex = 64;
            this.txtProductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductor_KeyPress);
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
            this.groupControl2.Controls.Add(this.dpHastaRomaneo);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.dpDesdeRomaneo);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(2, 52);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1100, 49);
            this.groupControl2.TabIndex = 74;
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
            // dpHastaRomaneo
            // 
            this.dpHastaRomaneo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHastaRomaneo.Location = new System.Drawing.Point(266, 24);
            this.dpHastaRomaneo.Name = "dpHastaRomaneo";
            this.dpHastaRomaneo.Size = new System.Drawing.Size(99, 21);
            this.dpHastaRomaneo.TabIndex = 60;
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
            // dpDesdeRomaneo
            // 
            this.dpDesdeRomaneo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesdeRomaneo.Location = new System.Drawing.Point(84, 24);
            this.dpDesdeRomaneo.Name = "dpDesdeRomaneo";
            this.dpDesdeRomaneo.Size = new System.Drawing.Size(99, 21);
            this.dpDesdeRomaneo.TabIndex = 58;
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
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnBuscarRomaneo);
            this.groupControl4.Location = new System.Drawing.Point(1105, 52);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(86, 82);
            this.groupControl4.TabIndex = 77;
            this.groupControl4.Text = "Opciones";
            // 
            // btnBuscarRomaneo
            // 
            this.btnBuscarRomaneo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarRomaneo.Image")));
            this.btnBuscarRomaneo.Location = new System.Drawing.Point(3, 23);
            this.btnBuscarRomaneo.Name = "btnBuscarRomaneo";
            this.btnBuscarRomaneo.Size = new System.Drawing.Size(81, 22);
            this.btnBuscarRomaneo.TabIndex = 39;
            this.btnBuscarRomaneo.Text = "Buscar";
            this.btnBuscarRomaneo.Click += new System.EventHandler(this.btnBuscarRomaneo_Click);
            // 
            // groupControl6
            // 
            this.groupControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl6.Controls.Add(this.gridControlRomaneo);
            this.groupControl6.Location = new System.Drawing.Point(1, 136);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(1189, 352);
            this.groupControl6.TabIndex = 78;
            this.groupControl6.Text = "Lista de Romaneo";
            // 
            // gridControlRomaneo
            // 
            this.gridControlRomaneo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlRomaneo.Location = new System.Drawing.Point(2, 21);
            this.gridControlRomaneo.MainView = this.gridViewRomaneo;
            this.gridControlRomaneo.MenuManager = this.ribbon;
            this.gridControlRomaneo.Name = "gridControlRomaneo";
            this.gridControlRomaneo.Size = new System.Drawing.Size(1185, 329);
            this.gridControlRomaneo.TabIndex = 68;
            this.gridControlRomaneo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLiquidacionDetalle,
            this.gridViewRomaneo});
            // 
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlRomaneo;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
            // 
            // gridViewRomaneo
            // 
            this.gridViewRomaneo.GridControl = this.gridControlRomaneo;
            this.gridViewRomaneo.Name = "gridViewRomaneo";
            this.gridViewRomaneo.OptionsBehavior.Editable = false;
            this.gridViewRomaneo.OptionsSelection.MultiSelect = true;
            this.gridViewRomaneo.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewRomaneo.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl7
            // 
            this.groupControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl7.Controls.Add(this.btnPrevisualizar);
            this.groupControl7.Controls.Add(this.btnSubirAfip);
            this.groupControl7.Location = new System.Drawing.Point(2, 490);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.ShowCaption = false;
            this.groupControl7.Size = new System.Drawing.Size(1188, 32);
            this.groupControl7.TabIndex = 79;
            this.groupControl7.Text = "Buscar Romaneo";
            // 
            // btnPrevisualizar
            // 
            this.btnPrevisualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevisualizar.Image")));
            this.btnPrevisualizar.Location = new System.Drawing.Point(92, 5);
            this.btnPrevisualizar.Name = "btnPrevisualizar";
            this.btnPrevisualizar.Size = new System.Drawing.Size(90, 22);
            this.btnPrevisualizar.TabIndex = 41;
            this.btnPrevisualizar.Text = "Previsualizar";
            // 
            // btnSubirAfip
            // 
            this.btnSubirAfip.Image = ((System.Drawing.Image)(resources.GetObject("btnSubirAfip.Image")));
            this.btnSubirAfip.Location = new System.Drawing.Point(5, 5);
            this.btnSubirAfip.Name = "btnSubirAfip";
            this.btnSubirAfip.Size = new System.Drawing.Size(81, 22);
            this.btnSubirAfip.TabIndex = 40;
            this.btnSubirAfip.Text = "Subir Afip";
            // 
            // Form_RomaneoGestionRomaneo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 525);
            this.Controls.Add(this.groupControl7);
            this.Controls.Add(this.groupControl6);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_RomaneoGestionRomaneo";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Romaneo - Gestión de Romaneo";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRomaneo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRomaneo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
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
        private System.Windows.Forms.DateTimePicker dpHastaRomaneo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpDesdeRomaneo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnBuscarRomaneo;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlRomaneo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRomaneo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalle;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private DevExpress.XtraEditors.SimpleButton btnPrevisualizar;
        private DevExpress.XtraEditors.SimpleButton btnSubirAfip;
    }
}