using CooperativaProduccion.Helpers;
using CooperativaProduccion.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CooperativaProduccion
{
    public partial class Form_ProduccionTemperaturaEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        private List<LineaDetalle> _detalle;

        private DateTime _fechaSeleccionada;
        private Guid _blendSeleccionado;

        public Form_ProduccionTemperaturaEditor(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionMuestras_Load;
            this.dateFecha.ValueChanged += dateFecha_ValueChanged;
            this.dateFecha.LostFocus += dateFecha_LostFocus;
            this.cbBlend.SelectedValueChanged += cbBlend_SelectedValueChanged;
            this.cbBlend.LostFocus += cbBlend_LostFocus;
            this.gridViewTemperatura.ShownEditor += gridViewTemperatura_ShownEditor;
            this.gridViewTemperatura.RowCellClick += gridViewTemperatura_RowCellClick;
            //this.gridViewMuestra.CellValueChanged += gridViewMuestra_CellValueChanged;
            this.btnAgregar.Click += btnAgregar_Click;
            this.btnBorrar.Click += btnBorrar_Click;
            this.btnGrabar.Click += btnGrabar_Click;
        }

        void Form_ProduccionMuestras_Load(object sender, EventArgs e)
        {
            this.dateFecha.Enabled = true;
            this.dateFecha.Value = DateTime.Now.Date;
            
            var blends = _blendManager.ListarBlends()
                .Select(x => new Blend()
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion
                })
                .OrderBy(x => x.Descripcion)
                .ToList();

            this.cbBlend.DisplayMember = "Descripcion";
            this.cbBlend.ValueMember = "Id";
            this.cbBlend.DataSource = blends;

            _blendSeleccionado = this.cbBlend.SelectedValue == null ? Guid.Empty : (Guid)this.cbBlend.SelectedValue;

            _detalle = new List<LineaDetalle>()
            {
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
            };

            this.gridControlTemperatura.DataSource = new BindingList<LineaDetalle>(_detalle);

            this.gridViewTemperatura.OptionsMenu.EnableColumnMenu = false;
            //this.gridViewTemperatura.OptionsView.ColumnAutoWidth = false;
            this.gridViewTemperatura.Columns["TemperaturaEmpaque"].Caption = "Temp. °C empaque";
            this.gridViewTemperatura.Columns["TemperaturaAmbiente"].Caption = "Temp. ambiente";

            var timespanEditorMask = new DevExpress.XtraEditors.Repository.RepositoryItemTimeSpanEdit();
            timespanEditorMask.AllowEditDays = false;
            timespanEditorMask.AllowEditSeconds = false;
            //timespanEditorMask.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            timespanEditorMask.DisplayFormat.FormatString = "d";
            timespanEditorMask.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timespanEditorMask.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            timespanEditorMask.Mask.EditMask = "HH:mm";
            timespanEditorMask.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.SpinButtons;
            //timespanEditorMask.TouchUIMaxValue = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            //timespanEditorMask.TouchUIMinValue = new System.DateTime(((long)(0)));
            this.gridViewTemperatura.Columns["Hora"].ColumnEdit = timespanEditorMask;

            var memoEditorMask = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridViewTemperatura.Columns["Observaciones"].ColumnEdit = memoEditorMask;
        }

        private void Clear()
        {
            this.dateFecha.Value = DateTime.Now;
            cbBlend_SelectedValueChanged(cbBlend, EventArgs.Empty);

            txtMinimo.Text = String.Empty;
            txtMeta.Text = String.Empty;
            txtMaximo.Text = String.Empty;

            _detalle = new List<LineaDetalle>()
            {
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty },
            };

            this.gridControlTemperatura.DataSource = new BindingList<LineaDetalle>(_detalle);
        }

        private int _GetOrdenProduccion(int periodo, Guid blendId)
        {
            return _blendManager.GetOrdenProduccion(periodo, blendId);
        }

        private long _GetNumeroDeCorrida(Guid blendId, DateTime fecha)
        {
            return _blendManager.GetSiguienteCorrida(blendId, fecha);
        }

        void dateFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        void dateFecha_LostFocus(object sender, EventArgs e)
        {
            if (_fechaSeleccionada != this.dateFecha.Value.Date)
            {
                _fechaSeleccionada = this.dateFecha.Value.Date;

                //if (_blendSeleccionado != null && _blendSeleccionado != Guid.Empty)
                //{
                //    this.lblOrden.Text = "Orden de Produccion: " + _GetOrdenProduccion(_fechaSeleccionada.Year, _blendSeleccionado);
                //    this.lblCorrida.Text = "Corrida: " + _GetNumeroDeCorrida(_blendSeleccionado, _fechaSeleccionada);
                //}
                //else
                //{
                //    this.lblOrden.Text = "Orden de Produccion: ";
                //    this.lblCorrida.Text = "Corrida: ";
                //}
            }
        }

        void cbBlend_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cbBlend.SelectedValue == null)
            {
                this.lblOrden.Text = "Orden de Produccion: ";
                this.lblCorrida.Text = "Corrida: ";
                return;
            }
        }

        void cbBlend_LostFocus(object sender, EventArgs e)
        {
            if (this.cbBlend.SelectedValue == null || _blendSeleccionado != (Guid)this.cbBlend.SelectedValue)
            {
                if (this.cbBlend.SelectedValue == null)
                {
                    this.lblOrden.Text = "Orden de Produccion: ";
                    this.lblCorrida.Text = "Corrida: ";
                    return;
                }

                _blendSeleccionado = (Guid)this.cbBlend.SelectedValue;

                this.lblOrden.Text = "Orden de Produccion: " + _GetOrdenProduccion(_fechaSeleccionada.Year, _blendSeleccionado);
                this.lblCorrida.Text = "Corrida: " + _GetNumeroDeCorrida(_blendSeleccionado, _fechaSeleccionada);
            }
        }

        void gridViewTemperatura_ShownEditor(object sender, EventArgs e)
        {
            gridViewTemperatura.ActiveEditor.SelectAll();
        }

        void gridViewTemperatura_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            gridViewTemperatura.ActiveEditor.SelectAll();
        }

        void btnAgregar_Click(object sender, EventArgs e)
        {
            _detalle.Add(new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, TemperaturaAmbiente = 0m, Observaciones = String.Empty });
            this.gridViewTemperatura.RefreshData();
        }

        void btnBorrar_Click(object sender, EventArgs e)
        {
            _detalle.RemoveAt(this.gridViewTemperatura.GetFocusedDataSourceRowIndex());
            this.gridViewTemperatura.RefreshData();
        }

        void btnGrabar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Confirma que desea dar de alta estos nuevos registros de control de temperatura?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            var blend = _blendManager.GetBlend((Guid)this.cbBlend.SelectedValue);
            var lineas = new List<LineaDetalleControlDeTempraturaViewModel>();
            
            foreach (var linea in _detalle)
            {
                if (linea.Hora == DateTime.MinValue.TimeOfDay && linea.Caja == 0 && linea.TemperaturaEmpaque == 0m && linea.TemperaturaAmbiente == 0m && linea.Observaciones == String.Empty)
                {
                    continue;
                }

                lineas.Add(new LineaDetalleControlDeTempraturaViewModel() { Hora = linea.Hora, Caja = linea.Caja, TemperaturaEmpaque = linea.TemperaturaEmpaque, TemperaturaAmbiente = linea.TemperaturaAmbiente, Observaciones = linea.Observaciones.Trim() });
            }

            var control = new ControlDeTemperaturaViewModel()
            {
                Fecha = this.dateFecha.Value.Date,
                Blend = blend,
                Corrida = _blendManager.GetSiguienteCorrida((Guid)this.cbBlend.SelectedValue, this.dateFecha.Value.Date),
                Minimo = txtMinimo.Text.Trim(),
                Meta = txtMeta.Text.Trim(),
                Maximo = txtMaximo.Text.Trim(),
                Lineas = lineas
            };
            
            try
            {
                _blendManager.AddControlTemperatura(control);

                Clear();
            }
            catch (Exception ex)
            {
                if (ex.Message == "No existe Caja")
                {
                    MessageBox.Show("No se puede encontrar el número de caja ingresado", "No se puede grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        class Blend
        {
            public Guid Id { get; set; }

            public string Descripcion { get; set; }
        }

        class LineaDetalle
        {
            public TimeSpan Hora { get; set; }

            public long Caja { get; set; }

            public decimal TemperaturaEmpaque { get; set; }

            public decimal TemperaturaAmbiente { get; set; }

            public string Observaciones { get; set; }
        }
    }
}
