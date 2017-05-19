using CooperativaProduccion.Helpers;
using CooperativaProduccion.ViewModels;
using DesktopEntities.Models;
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
    public partial class Form_ProduccionMuestrasEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //private CooperativaProduccionEntities _context;

        private IBlendManager _blendManager;

        private DateTime _fechaSeleccionada;
        private Guid _blendSeleccionado;
        private List<LineaDetalle> _detalle;

        public Form_ProduccionMuestrasEditor(IBlendManager blendManager)
        {
            InitializeComponent();

            //_context = new CooperativaProduccionEntities();
            _blendManager = blendManager;

            this.Load += Form_ProduccionMuestras_Load;
            this.dateFecha.ValueChanged += dateFecha_ValueChanged;
            this.dateFecha.LostFocus += dateFecha_LostFocus;
            this.cbBlend.SelectedValueChanged += cbBlend_SelectedValueChanged;
            this.cbBlend.LostFocus += cbBlend_LostFocus;
            this.gridControlMuestra.ProcessGridKey += gridControlMuestra_ProcessGridKey;
            this.gridViewMuestra.KeyDown += gridViewMuestra_KeyDown;
            this.gridViewMuestra.GotFocus += gridViewMuestra_GotFocus;
            this.gridViewMuestra.CellValueChanged += gridViewMuestra_CellValueChanged;
            this.btnGrabar.Click += btnGrabar_Click;
        }

        void Form_ProduccionMuestras_Load(object sender, EventArgs e)
        {
            this.dateFecha.Enabled = false;
            this.dateFecha.Value = DateTime.Now.Date;
            this.timeSpanHora.TimeSpan = DateTime.Now.TimeOfDay;

            //var blends = _context.Vw_Producto
            //    .Select(x => new Blend()
            //    {
            //        Id = x.ID,
            //        Descripcion = x.DESCRIPCION
            //    })
            //    .OrderBy(x => x.Descripcion)
            //    .ToList();
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
                new LineaDetalle() { Tamanio = "1' y m/",  Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "1/2",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "1/4",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "1/8",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "PAN",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "P. TOTAL", Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "V/O BJ",   Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "FIBRA",    Kilos = 0, Porcentaje = 0m },
            };

            this.gridControlMuestra.DataSource = new BindingList<LineaDetalle>(_detalle);

            this.gridViewMuestra.OptionsMenu.EnableColumnMenu = false;
            this.gridViewMuestra.OptionsView.ColumnAutoWidth = false;
            this.gridViewMuestra.Columns["Tamanio"].Caption = "Tamaño".ToUpper();
            this.gridViewMuestra.Columns["Tamanio"].OptionsColumn.AllowEdit = false;
            this.gridViewMuestra.Columns["Tamanio"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewMuestra.Columns["Tamanio"].MinWidth = this.gridViewMuestra.Columns["Tamanio"].GetBestWidth();
            this.gridViewMuestra.Columns["Tamanio"].MaxWidth = this.gridViewMuestra.Columns["Tamanio"].GetBestWidth();
            this.gridViewMuestra.Columns["Kilos"].Caption = "Kgs".ToUpper();
            this.gridViewMuestra.Columns["Kilos"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewMuestra.Columns["Porcentaje"].Caption = "%".ToUpper();
            this.gridViewMuestra.Columns["Porcentaje"].OptionsColumn.AllowEdit = true;
            this.gridViewMuestra.Columns["Porcentaje"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

            var editorMask = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            editorMask.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            editorMask.Mask.EditMask = "[0-9]+";
            editorMask.Enter += (s, a) => (s as DevExpress.XtraEditors.BaseEdit).SelectAll();
            editorMask.Click += (s, a) => (s as DevExpress.XtraEditors.BaseEdit).SelectAll();
            this.gridViewMuestra.Columns["Kilos"].ColumnEdit = editorMask;

            this.memoObservaciones.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        }

        private void Clear()
        {
            this.dateFecha.Value = DateTime.Now;
            this.timeSpanHora.TimeSpan = DateTime.Now.TimeOfDay;
            this.spinCaja.Value = 0m;
            cbBlend_SelectedValueChanged(cbBlend, EventArgs.Empty);
            _detalle = new List<LineaDetalle>()
            {
                new LineaDetalle() { Tamanio = "1' y m/",  Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "1/2",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "1/4",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "1/8",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "PAN",      Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "P. TOTAL", Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "V/O BJ",   Kilos = 0, Porcentaje = 0m },
                new LineaDetalle() { Tamanio = "FIBRA",    Kilos = 0, Porcentaje = 0m },

            };

            this.gridControlMuestra.DataSource = new BindingList<LineaDetalle>(_detalle);

            this.lblPM.Text = "P.M.: 0";
            this.lblTotal.Text = "TOTAL SOBRE 1/2: 0";

            this.memoObservaciones.Text = String.Empty;
        }

        private int _GetOrdenProduccion(int periodo, Guid blendId)
        {
            return _blendManager.GetOrdenProduccion(periodo, blendId);
        }

        private long _GetNumeroDeCorrida(Guid blendId, DateTime fecha)
        {
            return _blendManager.GetSiguienteCorrida(blendId, fecha);
        }

        private bool _CheckIfPMParam(string paramName)
        {
            if (paramName == "1' y m/" ||
                paramName == "1/2" ||
                paramName == "1/4" ||
                paramName == "1/8" ||
                paramName == "PAN")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _CheckIfPMDepend(string paramName)
        {
            if (paramName == "1' y m/" ||
                paramName == "1/2"     ||
                paramName == "1/4"     ||
                paramName == "1/8"     ||
                paramName == "PAN"     ||
                paramName == "V/O BJ"  ||
                paramName == "FIBRA")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _CheckIfTotalParam(string paramName)
        {
            if (paramName == "1' y m/" ||
                paramName == "1/2")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _CheckIfTotalDepend(string paramName)
        {
            //if (paramName == "V/O BJ" ||
            //    paramName == "FIBRA")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }

        private long _CalcPM(List<LineaDetalle> detalle)
        {
            long pm = 0;

            foreach (var linea in detalle)
            {
                if (_CheckIfPMParam(linea.Tamanio))
                {
                    pm += linea.Kilos;
                }
            }

            return pm;
        }

        private decimal _CalcTotalSobreUnMedio(List<LineaDetalle> detalle)
        {
            decimal total = 0;

            foreach (var linea in detalle)
            {
                if (_CheckIfTotalParam(linea.Tamanio))
                {
                    total += linea.Porcentaje;
                }
            }

            return total;
        }

        private void _UpdatePorcentajeEnPM(List<LineaDetalle> detalle)
        {
            var pm = _CalcPM(_detalle);

            foreach (var linea in detalle)
            {
                if (_CheckIfPMDepend(linea.Tamanio))
                {
                    if (pm != 0)
                    {
                        linea.Porcentaje = Math.Round(linea.Kilos * 100 / Convert.ToDecimal(pm), 2);
                    }
                    else
                    {
                        linea.Porcentaje = 0;
                    }
                }
            }
        }

        private void _UpdatePorcentajeEnTotalSobreUnMedio(List<LineaDetalle> detalle)
        {
            //var total = _CalcTotalSobreUnMedio(_detalle);
            //
            //foreach (var linea in detalle)
            //{
            //    if (_CheckIfTotalDepend(linea.Tamanio))
            //    {
            //        linea.Porcentaje = Math.Round(linea.Kilos * 100 / Convert.ToDecimal(total), 2);
            //    }
            //}
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

        void gridControlMuestra_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                var view = gridControlMuestra.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView;

                if ((e.Modifiers == Keys.None
                        && view.IsLastRow
                        )//&& view.FocusedColumn.VisibleIndex == view.VisibleColumns.Count - 1)
                    || (e.Modifiers == Keys.Shift
                        && view.IsFirstRow
                        ))//&& view.FocusedColumn.VisibleIndex == 0))
                {
                    if (view.IsEditing)
                    {
                        view.CloseEditor();
                    }

                    this.SelectNextControl(gridControlMuestra, e.Modifiers == Keys.None, true, true, true);

                    e.Handled = true;
                }
            }
        }

        void gridViewMuestra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (gridViewMuestra.FocusedColumn != gridViewMuestra.Columns["Kilos"])
                {
                    gridViewMuestra.FocusedColumn = gridViewMuestra.Columns["Kilos"];
                }

                if (e.Shift)
                {
                    gridViewMuestra.MovePrev();
                }
                else
                {
                    gridViewMuestra.MoveNext();
                }

                e.Handled = true;
            }
        }

        void gridViewMuestra_GotFocus(object sender, EventArgs e)
        {
            gridViewMuestra.FocusedRowHandle = 0;

            gridViewMuestra.FocusedColumn = gridViewMuestra.VisibleColumns[1];

            //gridView1.ShowEditor();
        }

        void gridViewMuestra_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "Kilos")
            {
                return;
            }

            if (_CheckIfPMParam(this.gridViewMuestra.GetRowCellValue(e.RowHandle, this.gridViewMuestra.Columns["Tamanio"]) as string))
            {
                var pm = _CalcPM(_detalle);

                //this.gridViewMuestra.SetRowCellValue(e.RowHandle, this.gridViewMuestra.Columns["Porcentaje"], Math.Round(kilos * 100 / Convert.ToDecimal(total), 2));

                _UpdatePorcentajeEnPM(_detalle);

                this.lblPM.Text = "P.M.: " + pm;

                var total = _CalcTotalSobreUnMedio(_detalle);

                this.lblTotal.Text = "TOTAL SOBRE 1/2: " + total;

                this.gridControlMuestra.RefreshDataSource();
            }

            if (_CheckIfPMDepend(this.gridViewMuestra.GetRowCellValue(e.RowHandle, this.gridViewMuestra.Columns["Tamanio"]) as string))
            {
                _UpdatePorcentajeEnPM(_detalle);

                this.gridControlMuestra.RefreshDataSource();
            }
        }

        void btnGrabar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Confirma que desea dar de alta esta muestra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            var blend = _blendManager.GetBlend((Guid)this.cbBlend.SelectedValue);
            var lineas = new List<LineaDetalleMuestraViewModel>();

            foreach (var linea in _detalle)
	        {
                lineas.Add(new LineaDetalleMuestraViewModel() { Tamanio = linea.Tamanio, kilos = linea.Kilos, Porcentaje = linea.Porcentaje });
	        }

            var muestra = new MuestraViewModel()
            {
                Fecha = this.dateFecha.Value.Date,
                Hora = this.timeSpanHora.TimeSpan,
                Blend = blend,
                Caja = Convert.ToInt64(this.spinCaja.Value),
                Corrida = _blendManager.GetSiguienteCorrida((Guid)this.cbBlend.SelectedValue, this.dateFecha.Value.Date),
                Lineas = lineas,
                PesoMuestra = Convert.ToInt64(this.lblPM.Text.Replace("P.M.: ", String.Empty)),
                TotalSobreUnMedio = Convert.ToDecimal(this.lblTotal.Text.Replace("TOTAL SOBRE 1/2: ", String.Empty)),
                Observaciones = this.memoObservaciones.Text.Trim()
            };

            try
            {
                _blendManager.AddMuestra(muestra);

                Clear();
            }
            catch (Exception ex)
            {
                if (ex.Message == "No existe Caja")
                {
                    MessageBox.Show("No se puede encontrar el número de caja ingresado", "No se puede grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    spinCaja.Focus();
                    spinCaja.SelectAll();
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
            public string Tamanio { get; set; }

            public int Kilos { get; set; }

            public decimal Porcentaje { get; set; }
        }
    }
}
