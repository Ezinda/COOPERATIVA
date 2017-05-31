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
    public partial class Form_ProduccionHumedadEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        private List<LineaDetalle> _detalle;

        private DateTime _fechaSeleccionada;
        private Guid _blendSeleccionado;

        public Form_ProduccionHumedadEditor(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionHumedadEditor_Load;
            this.dateFecha.ValueChanged += dateFecha_ValueChanged;
            this.dateFecha.LostFocus += dateFecha_LostFocus;
            this.cbBlend.SelectedValueChanged += cbBlend_SelectedValueChanged;
            this.cbBlend.LostFocus += cbBlend_LostFocus;
            this.gridViewHumedad.ShownEditor += gridViewTemperatura_ShownEditor;
            this.gridViewHumedad.RowCellClick += gridViewTemperatura_RowCellClick;
            //this.gridViewMuestra.CellValueChanged += gridViewMuestra_CellValueChanged;
            this.btnAgregar.Click += btnAgregar_Click;
            this.btnBorrar.Click += btnBorrar_Click;
            this.btnGrabar.Click += btnGrabar_Click;
        }

        void Form_ProduccionHumedadEditor_Load(object sender, EventArgs e)
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
            this.cbBlend.SelectedIndex = -1;

            _detalle = new List<LineaDetalle>()
            {
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
            };

            this.gridControlHumedad.DataSource = new BindingList<LineaDetalle>(_detalle);

            this.gridViewHumedad.OptionsMenu.EnableColumnMenu = false;
            //this.gridViewTemperatura.OptionsView.ColumnAutoWidth = false;
            this.gridViewHumedad.Columns["Hora"].Caption = "Hora muestra";
            this.gridViewHumedad.Columns["TemperaturaEmpaque"].Caption = "Temp Empaque";
            this.gridViewHumedad.Columns["Caja"].Caption = "Nro Caja";
            this.gridViewHumedad.Columns["Capsula"].Caption = "Nro Capsula Brab";
            this.gridViewHumedad.Columns["HoraEntrada"].Caption = "Hora ent";
            this.gridViewHumedad.Columns["HoraSalida"].Caption = "Hora sal";
            this.gridViewHumedad.Columns["Humedad"].Caption = "Hum Brab";

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
            this.gridViewHumedad.Columns["Hora"].ColumnEdit = timespanEditorMask;
            this.gridViewHumedad.Columns["HoraEntrada"].ColumnEdit = timespanEditorMask;
            this.gridViewHumedad.Columns["HoraSalida"].ColumnEdit = timespanEditorMask;
        }

        private void Clear()
        {
            this.dateFecha.Value = DateTime.Now;
            cbBlend_SelectedValueChanged(cbBlend, EventArgs.Empty);

            _detalle = new List<LineaDetalle>()
            {
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
                new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m },
            };

            this.gridControlHumedad.DataSource = new BindingList<LineaDetalle>(_detalle);
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
            gridViewHumedad.ActiveEditor.SelectAll();
        }

        void gridViewTemperatura_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            gridViewHumedad.ActiveEditor.SelectAll();
        }

        void btnAgregar_Click(object sender, EventArgs e)
        {
            _detalle.Add(new LineaDetalle() { Hora = DateTime.MinValue.TimeOfDay, Caja = 0, TemperaturaEmpaque = 0m, Capsula = 0, HoraEntrada = DateTime.MinValue.TimeOfDay, HoraSalida = DateTime.MinValue.TimeOfDay, Humedad = 0m });
            this.gridViewHumedad.RefreshData();
        }

        void btnBorrar_Click(object sender, EventArgs e)
        {
            _detalle.RemoveAt(this.gridViewHumedad.GetFocusedDataSourceRowIndex());
            this.gridViewHumedad.RefreshData();
        }

        void btnGrabar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Confirma que desea dar de alta estos nuevos registros de control de humedad?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            if (this.cbBlend.SelectedValue == null)
            {
                MessageBox.Show("Se debe seleccionar un Blend", "No se puede grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var blend = _blendManager.GetBlend((Guid)this.cbBlend.SelectedValue);
            var lineas = new List<LineaDetalleControlDeHumedadViewModel>();

            foreach (var linea in _detalle)
            {
                if (linea.Hora == DateTime.MinValue.TimeOfDay && linea.Caja == 0 && linea.TemperaturaEmpaque == 0m && linea.Capsula == 0 && linea.HoraEntrada == DateTime.MinValue.TimeOfDay && linea.HoraSalida == DateTime.MinValue.TimeOfDay && linea.Humedad == 0m)
                {
                    continue;
                }

                lineas.Add(new LineaDetalleControlDeHumedadViewModel()
                {
                    Hora = linea.Hora,
                    Caja = linea.Caja,
                    TemperaturaEmpaque = linea.TemperaturaEmpaque,
                    Capsula = linea.Capsula,
                    HoraEntrada = linea.HoraEntrada,
                    HoraSalida = linea.HoraSalida,
                    Humedad = linea.Humedad
                });
            }

            var control = new ControlDeHumedadViewModel()
            {
                Fecha = this.dateFecha.Value.Date,
                Blend = blend,
                Corrida = _blendManager.GetSiguienteCorrida((Guid)this.cbBlend.SelectedValue, this.dateFecha.Value.Date),
                Lineas = lineas
            };

            try
            {
                _blendManager.AddControlHumedad(control);

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

            public long Capsula { get; set; }

            public TimeSpan HoraEntrada { get; set; }

            public TimeSpan HoraSalida { get; set; }

            public decimal Humedad { get; set; }
        }
    }
}
