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
    public partial class Form_ProduccionNicotinaEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private const decimal constanteACL = 0.2703m;

        private const int constanteNicotina1 = 4;
        
        private const decimal constanteNicotina2 = 0.0676m;

        private IBlendManager _blendManager;

        private DateTime _fechaSeleccionada;
        private Guid _blendSeleccionado;
        private List<LineaDetalle> _detalle;

        public Form_ProduccionNicotinaEditor(IBlendManager blendManager)
        {
            InitializeComponent();

            //_context = new CooperativaProduccionEntities();
            _blendManager = blendManager;

            this.Load += Form_ProduccionNicotinaEditor_Load;
            this.dateFecha.ValueChanged += dateFecha_ValueChanged;
            this.dateFecha.LostFocus += dateFecha_LostFocus;
            this.cbBlend.SelectedValueChanged += cbBlend_SelectedValueChanged;
            this.cbBlend.LostFocus += cbBlend_LostFocus;
            this.gridControlNicotina.ProcessGridKey += gridControlNicotina_ProcessGridKey;
            this.gridViewNicotina.KeyDown += gridViewNicotina_KeyDown;
            this.gridViewNicotina.GotFocus += gridViewNicotina_GotFocus;
            this.gridViewNicotina.CellValueChanged += gridViewNicotina_CellValueChanged;
            this.btnGrabar.Click += btnGrabar_Click;
        }

        void Form_ProduccionNicotinaEditor_Load(object sender, EventArgs e)
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
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
            };

            this.gridControlNicotina.DataSource = new BindingList<LineaDetalle>(_detalle);

            this.gridViewNicotina.OptionsMenu.EnableColumnMenu = false;
            //this.gridViewNicotina.OptionsView.ColumnAutoWidth = false;
            this.gridViewNicotina.Columns["CajaDesde"].Caption = "Desde".ToUpper();
            this.gridViewNicotina.Columns["CajaDesde"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["CajaHasta"].Caption = "Hasta".ToUpper();
            this.gridViewNicotina.Columns["CajaHasta"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["PorcentajeH"].Caption = "%H".ToUpper();
            this.gridViewNicotina.Columns["PorcentajeH"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["FactorN"].Caption = "fN".ToUpper();
            this.gridViewNicotina.Columns["FactorN"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["Valor1"].Caption = "V 1".ToUpper();
            this.gridViewNicotina.Columns["Valor1"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["Valor2"].Caption = "V 2".ToUpper();
            this.gridViewNicotina.Columns["Valor2"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["PorcentajeALC"].Caption = "%ALC".ToUpper();
            this.gridViewNicotina.Columns["PorcentajeALC"].OptionsColumn.AllowEdit = false;
            this.gridViewNicotina.Columns["PorcentajeALC"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNicotina.Columns["PorcentajeNicotina"].Caption = "%Nicotina".ToUpper();
            this.gridViewNicotina.Columns["PorcentajeNicotina"].OptionsColumn.AllowEdit = false;
            this.gridViewNicotina.Columns["PorcentajeNicotina"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        }

        private void Clear()
        {
            this.dateFecha.Value = DateTime.Now;
            this.timeSpanHora.TimeSpan = DateTime.Now.TimeOfDay;
            this.spinCaja.Value = 0m;
            cbBlend_SelectedValueChanged(cbBlend, EventArgs.Empty);
            _detalle = new List<LineaDetalle>()
            {
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
                new LineaDetalle { CajaDesde = 0, CajaHasta = 0, PorcentajeH = 0m, FactorN = 0m, Valor1 = 0m, Valor2 = 0m, PorcentajeALC = 0m, PorcentajeNicotina = 0m },
            };

            this.gridControlNicotina.DataSource = new BindingList<LineaDetalle>(_detalle);
        }

        private int _GetOrdenProduccion(int periodo, Guid blendId)
        {
            return _blendManager.GetOrdenProduccion(periodo, blendId);
        }

        private long _GetNumeroDeCorrida(Guid blendId, DateTime fecha)
        {
            return _blendManager.GetSiguienteCorrida(blendId, fecha);
        }

        private bool _CheckIfFHParam(string fieldName)
        {
            return (fieldName == "PorcentajeH");
        }

        private bool _CheckIfACLParam(string fieldName)
        {
            return (fieldName == "Valor1" || fieldName == "FactorN" || _CheckIfFHParam(fieldName));
        }

        private bool _CheckIfNicotinaParam(string fieldName)
        {
            return (fieldName == "Valor1" || fieldName == "Valor2" || _CheckIfFHParam(fieldName));
        }

        private decimal _CalcFH(LineaDetalle row)
        {
            return 100 / (100 - row.PorcentajeH);
        }

        private decimal _CalcACL(LineaDetalle row)
        {
            return Math.Round(row.Valor1 * row.FactorN * Form_ProduccionNicotinaEditor.constanteACL * _CalcFH(row), 2, MidpointRounding.AwayFromZero);
        }

        private decimal _CalcNicotina(LineaDetalle row)
        {
            return Math.Round((2 * row.Valor2 - row.Valor1) * Form_ProduccionNicotinaEditor.constanteNicotina1 * Form_ProduccionNicotinaEditor.constanteNicotina2 * row.FactorN * _CalcFH(row), 2, MidpointRounding.AwayFromZero);
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

        void gridControlNicotina_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                var view = gridControlNicotina.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView;

                if ((e.Modifiers == Keys.None
                        && view.IsLastRow
                        && view.FocusedColumn.VisibleIndex == view.VisibleColumns.Count - 1)
                    || (e.Modifiers == Keys.Shift
                        && view.IsFirstRow
                        && view.FocusedColumn.VisibleIndex == 0))
                {
                    if (view.IsEditing)
                    {
                        view.CloseEditor();
                    }

                    this.SelectNextControl(gridControlNicotina, e.Modifiers == Keys.None, true, true, true);

                    e.Handled = true;
                }
            }
        }

        void gridViewNicotina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (e.Shift && gridViewNicotina.FocusedColumn == gridViewNicotina.Columns["CajaDesde"])
                {
                    gridViewNicotina.MovePrev();

                    e.Handled = true;
                }
                else if (gridViewNicotina.FocusedColumn == gridViewNicotina.Columns["Valor2"])
                {
                    gridViewNicotina.MoveNext();
                    gridViewNicotina.FocusedColumn = gridViewNicotina.Columns["CajaDesde"];

                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        void gridViewNicotina_GotFocus(object sender, EventArgs e)
        {
            gridViewNicotina.FocusedRowHandle = 0;

            gridViewNicotina.ShowEditor();
        }

        void gridViewNicotina_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PorcentajeALC" || e.Column.FieldName == "PorcentajeNicotina")
            {
                return;
            }

            if (e.Column.FieldName == "CajaDesde")
            {
                this.gridViewNicotina.SetRowCellValue(e.RowHandle, this.gridViewNicotina.Columns["CajaHasta"],
                    ((Int64)this.gridViewNicotina.GetRowCellValue(e.RowHandle, e.Column)) + 25);

                this.gridControlNicotina.RefreshDataSource();
            }

            if (_CheckIfACLParam(e.Column.FieldName))
            {
                var row = _detalle[gridViewNicotina.GetDataSourceRowIndex(e.RowHandle)];

                var acl = _CalcACL(row);

                this.gridViewNicotina.SetRowCellValue(e.RowHandle, this.gridViewNicotina.Columns["PorcentajeALC"], acl);

                this.gridControlNicotina.RefreshDataSource();
            }

            if (_CheckIfNicotinaParam(e.Column.FieldName))
            {
                var row = _detalle[gridViewNicotina.GetDataSourceRowIndex(e.RowHandle)];

                var acl = _CalcNicotina(row);

                this.gridViewNicotina.SetRowCellValue(e.RowHandle, this.gridViewNicotina.Columns["PorcentajeNicotina"], acl);

                this.gridControlNicotina.RefreshDataSource();
            }
        }

        void btnGrabar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Confirma que desea dar de alta este control?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            var blend = _blendManager.GetBlend((Guid)this.cbBlend.SelectedValue);
            var lineas = new List<LineaDetalleControlDeNicotinaViewModel>();

            foreach (var linea in _detalle)
            {
                if (linea.CajaDesde == 0 && linea.CajaHasta == 0 && linea.PorcentajeH == 0m && linea.FactorN == 0m && linea.Valor1 == 0m && linea.Valor2 == 0m && linea.PorcentajeALC == 0m && linea.PorcentajeNicotina == 0m)
                {
                    continue;
                }

                lineas.Add(new LineaDetalleControlDeNicotinaViewModel()
                {
                    CajaDesde = linea.CajaDesde,
                    CajaHasta = linea.CajaHasta,
                    PorcentajeHumedad = linea.PorcentajeH,
                    Valor1 = linea.Valor1,
                    Valor2 = linea.Valor2,
                    PorcentajeALC = linea.PorcentajeALC,
                    PorcentajeNicotina = linea.PorcentajeNicotina,
                });
            }

            var control = new ControlDeNicotinaViewModel()
            {
                Fecha = this.dateFecha.Value.Date,
                Blend = blend,
                Corrida = _blendManager.GetSiguienteCorrida((Guid)this.cbBlend.SelectedValue, this.dateFecha.Value.Date),
                Hora = this.timeSpanHora.TimeSpan,
                Lineas = lineas
            };

            try
            {
                _blendManager.AddControlNicotina(control);

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
            public long CajaDesde { get; set; }
                           
            public long CajaHasta { get; set; }

            public decimal PorcentajeH { get; set; }

            public decimal FactorN { get; set; }

            public decimal Valor1 { get; set; }

            public decimal Valor2 { get; set; }

            public decimal PorcentajeALC { get; set; }

            public decimal PorcentajeNicotina { get; set; }
        }
    }
}
