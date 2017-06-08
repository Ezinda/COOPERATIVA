namespace CooperativaProduccion
{
    partial class Form_ProduccionNicotinaEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProduccionNicotinaEditor));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblCaja = new System.Windows.Forms.Label();
            this.spinCaja = new DevExpress.XtraEditors.SpinEdit();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.lblBlend = new System.Windows.Forms.Label();
            this.cbBlend = new System.Windows.Forms.ComboBox();
            this.timeSpanHora = new DevExpress.XtraEditors.TimeSpanEdit();
            this.lblCorrida = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.txtMinimo = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPM = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnBorrar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlNicotina = new DevExpress.XtraGrid.GridControl();
            this.gridViewNicotina = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanHora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNicotina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNicotina)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 1;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowCategoryInCaption = false;
            this.ribbonControl.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl.ShowToolbarCustomizeItem = false;
            this.ribbonControl.Size = new System.Drawing.Size(586, 27);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(12, 42);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 16);
            this.lblFecha.TabIndex = 72;
            this.lblFecha.Text = "Fecha";
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.Location = new System.Drawing.Point(17, 34);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(83, 16);
            this.lblCaja.TabIndex = 78;
            this.lblCaja.Text = "Desde Caja N°";
            // 
            // spinCaja
            // 
            this.spinCaja.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinCaja.Location = new System.Drawing.Point(106, 33);
            this.spinCaja.Name = "spinCaja";
            this.spinCaja.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinCaja.Properties.DisplayFormat.FormatString = "d";
            this.spinCaja.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.spinCaja.Properties.EditFormat.FormatString = "d";
            this.spinCaja.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.spinCaja.Properties.Mask.EditMask = "[1-9]+[0-9]?+";
            this.spinCaja.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.spinCaja.Size = new System.Drawing.Size(147, 20);
            this.spinCaja.TabIndex = 79;
            // 
            // dateFecha
            // 
            this.dateFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFecha.Location = new System.Drawing.Point(58, 38);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(100, 21);
            this.dateFecha.TabIndex = 73;
            // 
            // lblBlend
            // 
            this.lblBlend.AutoSize = true;
            this.lblBlend.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlend.Location = new System.Drawing.Point(13, 103);
            this.lblBlend.Name = "lblBlend";
            this.lblBlend.Size = new System.Drawing.Size(39, 16);
            this.lblBlend.TabIndex = 76;
            this.lblBlend.Text = "Blend";
            // 
            // cbBlend
            // 
            this.cbBlend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlend.FormattingEnabled = true;
            this.cbBlend.Location = new System.Drawing.Point(58, 102);
            this.cbBlend.Name = "cbBlend";
            this.cbBlend.Size = new System.Drawing.Size(327, 21);
            this.cbBlend.TabIndex = 77;
            // 
            // timeSpanHora
            // 
            this.timeSpanHora.EditValue = System.TimeSpan.Parse("00:00:00");
            this.timeSpanHora.Location = new System.Drawing.Point(58, 65);
            this.timeSpanHora.Name = "timeSpanHora";
            this.timeSpanHora.Properties.AllowEditDays = false;
            this.timeSpanHora.Properties.AllowEditSeconds = false;
            this.timeSpanHora.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeSpanHora.Properties.DisplayFormat.FormatString = "d";
            this.timeSpanHora.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeSpanHora.Properties.Mask.EditMask = "HH:mm";
            this.timeSpanHora.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.timeSpanHora.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.SpinButtons;
            this.timeSpanHora.Properties.TouchUIMaxValue = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.timeSpanHora.Properties.TouchUIMinValue = new System.DateTime(((long)(0)));
            this.timeSpanHora.Size = new System.Drawing.Size(100, 20);
            this.timeSpanHora.TabIndex = 75;
            // 
            // lblCorrida
            // 
            this.lblCorrida.AutoSize = true;
            this.lblCorrida.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrida.Location = new System.Drawing.Point(13, 153);
            this.lblCorrida.Name = "lblCorrida";
            this.lblCorrida.Size = new System.Drawing.Size(51, 16);
            this.lblCorrida.TabIndex = 81;
            this.lblCorrida.Text = "Corrida:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 74;
            this.label1.Text = "Hora";
            // 
            // lblOrden
            // 
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.Location = new System.Drawing.Point(13, 128);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(124, 16);
            this.lblOrden.TabIndex = 80;
            this.lblOrden.Text = "Orden de Produccion:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(267, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 82;
            this.label2.Text = "Hasta Caja N°";
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(353, 33);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Properties.DisplayFormat.FormatString = "d";
            this.spinEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.spinEdit1.Properties.EditFormat.FormatString = "d";
            this.spinEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.spinEdit1.Properties.Mask.EditMask = "[1-9]+[0-9]?+";
            this.spinEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.spinEdit1.Size = new System.Drawing.Size(147, 20);
            this.spinEdit1.TabIndex = 83;
            // 
            // txtMinimo
            // 
            this.txtMinimo.Location = new System.Drawing.Point(164, 71);
            this.txtMinimo.MenuManager = this.ribbonControl;
            this.txtMinimo.Name = "txtMinimo";
            this.txtMinimo.Size = new System.Drawing.Size(104, 20);
            this.txtMinimo.TabIndex = 85;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(130, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 16);
            this.label3.TabIndex = 84;
            this.label3.Text = "% H";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(164, 97);
            this.textEdit1.MenuManager = this.ribbonControl;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(104, 20);
            this.textEdit1.TabIndex = 87;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(134, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 86;
            this.label4.Text = "V 1";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(316, 97);
            this.textEdit2.MenuManager = this.ribbonControl;
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(104, 20);
            this.textEdit2.TabIndex = 89;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(286, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 88;
            this.label5.Text = "V 2";
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(316, 69);
            this.textEdit3.MenuManager = this.ribbonControl;
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(104, 20);
            this.textEdit3.TabIndex = 91;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(282, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 16);
            this.label6.TabIndex = 90;
            this.label6.Text = "fN";
            // 
            // lblPM
            // 
            this.lblPM.AutoSize = true;
            this.lblPM.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPM.Location = new System.Drawing.Point(232, 154);
            this.lblPM.Name = "lblPM";
            this.lblPM.Size = new System.Drawing.Size(77, 16);
            this.lblPM.TabIndex = 93;
            this.lblPM.Text = "% Nicotina: 0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(232, 135);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(55, 16);
            this.lblTotal.TabIndex = 92;
            this.lblTotal.Text = "% ALC: 0";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Location = new System.Drawing.Point(498, 505);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(76, 21);
            this.btnGrabar.TabIndex = 94;
            this.btnGrabar.Text = "Grabar";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblCaja);
            this.groupControl1.Controls.Add(this.spinCaja);
            this.groupControl1.Controls.Add(this.lblPM);
            this.groupControl1.Controls.Add(this.spinEdit1);
            this.groupControl1.Controls.Add(this.lblTotal);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.textEdit3);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.txtMinimo);
            this.groupControl1.Controls.Add(this.textEdit2);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Location = new System.Drawing.Point(548, 42);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(559, 188);
            this.groupControl1.TabIndex = 95;
            this.groupControl1.Text = "Registro";
            this.groupControl1.Visible = false;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrar.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrar.Image")));
            this.btnBorrar.Location = new System.Drawing.Point(164, 461);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(125, 21);
            this.btnBorrar.TabIndex = 98;
            this.btnBorrar.Text = "Borrar registro";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.Location = new System.Drawing.Point(33, 461);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 21);
            this.btnAgregar.TabIndex = 97;
            this.btnAgregar.Text = "Agregar registro";
            // 
            // gridControlNicotina
            // 
            this.gridControlNicotina.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlNicotina.Location = new System.Drawing.Point(16, 185);
            this.gridControlNicotina.MainView = this.gridViewNicotina;
            this.gridControlNicotina.Name = "gridControlNicotina";
            this.gridControlNicotina.Size = new System.Drawing.Size(562, 270);
            this.gridControlNicotina.TabIndex = 96;
            this.gridControlNicotina.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNicotina});
            // 
            // gridViewNicotina
            // 
            this.gridViewNicotina.GridControl = this.gridControlNicotina;
            this.gridViewNicotina.Name = "gridViewNicotina";
            this.gridViewNicotina.OptionsView.ShowGroupPanel = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.Location = new System.Drawing.Point(12, 505);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(76, 21);
            this.btnEliminar.TabIndex = 107;
            this.btnEliminar.Text = "Eliminar";
            // 
            // Form_ProduccionNicotinaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 538);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateFecha);
            this.Controls.Add(this.gridControlNicotina);
            this.Controls.Add(this.lblBlend);
            this.Controls.Add(this.cbBlend);
            this.Controls.Add(this.timeSpanHora);
            this.Controls.Add(this.lblCorrida);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProduccionNicotinaEditor";
            this.Ribbon = this.ribbonControl;
            this.Text = "Editor de Control de Nicotina";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanHora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNicotina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNicotina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblCaja;
        private DevExpress.XtraEditors.SpinEdit spinCaja;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Label lblBlend;
        private System.Windows.Forms.ComboBox cbBlend;
        private DevExpress.XtraEditors.TimeSpanEdit timeSpanHora;
        private System.Windows.Forms.Label lblCorrida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.TextEdit txtMinimo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPM;
        private System.Windows.Forms.Label lblTotal;
        private DevExpress.XtraEditors.SimpleButton btnGrabar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnBorrar;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraGrid.GridControl gridControlNicotina;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNicotina;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
    }
}