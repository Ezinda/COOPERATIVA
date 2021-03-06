﻿namespace CooperativaProduccion
{
    partial class Form_RomaneoGestionClasificacion
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_RomaneoGestionClasificacion));
            this.gridViewRomaneoDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlRomaneo = new DevExpress.XtraGrid.GridControl();
            this.gridViewRomaneo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.gridViewLiquidacionDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
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
            this.btnBuscarRomaneo = new DevExpress.XtraEditors.SimpleButton();
            this.cbTabaco = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dpHastaRomaneo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dpDesdeRomaneo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRomaneoDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRomaneo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRomaneo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridViewRomaneoDetalle
            // 
            this.gridViewRomaneoDetalle.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewRomaneoDetalle.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRomaneoDetalle.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewRomaneoDetalle.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewRomaneoDetalle.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewRomaneoDetalle.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRomaneoDetalle.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewRomaneoDetalle.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewRomaneoDetalle.GridControl = this.gridControlRomaneo;
            this.gridViewRomaneoDetalle.Name = "gridViewRomaneoDetalle";
            this.gridViewRomaneoDetalle.OptionsView.ShowGroupPanel = false;
            this.gridViewRomaneoDetalle.DoubleClick += new System.EventHandler(this.gridViewRomaneoDetalle_DoubleClick);
            // 
            // gridControlRomaneo
            // 
            this.gridControlRomaneo.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gridViewRomaneoDetalle;
            gridLevelNode1.RelationName = "Level1";
            this.gridControlRomaneo.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControlRomaneo.Location = new System.Drawing.Point(2, 20);
            this.gridControlRomaneo.MainView = this.gridViewRomaneo;
            this.gridControlRomaneo.MenuManager = this.ribbon;
            this.gridControlRomaneo.Name = "gridControlRomaneo";
            this.gridControlRomaneo.Size = new System.Drawing.Size(1128, 350);
            this.gridControlRomaneo.TabIndex = 68;
            this.gridControlRomaneo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRomaneo,
            this.gridViewLiquidacionDetalle,
            this.gridViewRomaneoDetalle});
            // 
            // gridViewRomaneo
            // 
            this.gridViewRomaneo.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewRomaneo.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRomaneo.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewRomaneo.Appearance.FocusedCell.Options.UseFont = true;
            this.gridViewRomaneo.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewRomaneo.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRomaneo.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewRomaneo.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewRomaneo.GridControl = this.gridControlRomaneo;
            this.gridViewRomaneo.Name = "gridViewRomaneo";
            this.gridViewRomaneo.OptionsBehavior.Editable = false;
            this.gridViewRomaneo.OptionsView.ShowGroupPanel = false;
            this.gridViewRomaneo.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridViewRomaneo_MasterRowExpanded);
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
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(1136, 49);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // gridViewLiquidacionDetalle
            // 
            this.gridViewLiquidacionDetalle.GridControl = this.gridControlRomaneo;
            this.gridViewLiquidacionDetalle.Name = "gridViewLiquidacionDetalle";
            this.gridViewLiquidacionDetalle.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl6
            // 
            this.groupControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl6.Controls.Add(this.gridControlRomaneo);
            this.groupControl6.Location = new System.Drawing.Point(2, 149);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(1132, 372);
            this.groupControl6.TabIndex = 81;
            this.groupControl6.Text = "Lista de Romaneo";
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
            this.groupControl3.Location = new System.Drawing.Point(2, 115);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.ShowCaption = false;
            this.groupControl3.Size = new System.Drawing.Size(1132, 32);
            this.groupControl3.TabIndex = 80;
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
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 71;
            this.label5.Text = "CUIT";
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
            this.btnBuscarFet.Click += new System.EventHandler(this.btnBuscarFet_Click);
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
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 67;
            this.label4.Text = "Provincia";
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
            this.label6.Size = new System.Drawing.Size(28, 16);
            this.label6.TabIndex = 65;
            this.label6.Text = "FET";
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
            this.label7.Size = new System.Drawing.Size(61, 16);
            this.label7.TabIndex = 63;
            this.label7.Text = "Productor";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.btnBuscarRomaneo);
            this.groupControl2.Controls.Add(this.cbTabaco);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.dpHastaRomaneo);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.dpDesdeRomaneo);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Location = new System.Drawing.Point(3, 65);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1131, 49);
            this.groupControl2.TabIndex = 79;
            this.groupControl2.Text = "Buscar Romaneo";
            // 
            // btnBuscarRomaneo
            // 
            this.btnBuscarRomaneo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarRomaneo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarRomaneo.Image")));
            this.btnBuscarRomaneo.Location = new System.Drawing.Point(583, 23);
            this.btnBuscarRomaneo.Name = "btnBuscarRomaneo";
            this.btnBuscarRomaneo.Size = new System.Drawing.Size(81, 22);
            this.btnBuscarRomaneo.TabIndex = 39;
            this.btnBuscarRomaneo.Text = "Buscar";
            this.btnBuscarRomaneo.Click += new System.EventHandler(this.btnBuscarRomaneo_Click);
            // 
            // cbTabaco
            // 
            this.cbTabaco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTabaco.FormattingEnabled = true;
            this.cbTabaco.Location = new System.Drawing.Point(416, 24);
            this.cbTabaco.Name = "cbTabaco";
            this.cbTabaco.Size = new System.Drawing.Size(142, 21);
            this.cbTabaco.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(365, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 80;
            this.label1.Text = "Tabaco";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dpHastaRomaneo
            // 
            this.dpHastaRomaneo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHastaRomaneo.Location = new System.Drawing.Point(266, 24);
            this.dpHastaRomaneo.Name = "dpHastaRomaneo";
            this.dpHastaRomaneo.Size = new System.Drawing.Size(93, 21);
            this.dpHastaRomaneo.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(189, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Fecha Hasta";
            // 
            // dpDesdeRomaneo
            // 
            this.dpDesdeRomaneo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesdeRomaneo.Location = new System.Drawing.Point(84, 24);
            this.dpDesdeRomaneo.Name = "dpDesdeRomaneo";
            this.dpDesdeRomaneo.Size = new System.Drawing.Size(93, 21);
            this.dpDesdeRomaneo.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Fecha Desde";
            // 
            // Form_RomaneoGestionClasificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 523);
            this.Controls.Add(this.groupControl6);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_RomaneoGestionClasificacion";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Romaneo - Gestión de Clasificación";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRomaneoDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRomaneo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRomaneo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLiquidacionDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl gridControlRomaneo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRomaneo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLiquidacionDetalle;
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
        private System.Windows.Forms.ComboBox cbTabaco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dpHastaRomaneo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpDesdeRomaneo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnBuscarRomaneo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRomaneoDetalle;
    }
}