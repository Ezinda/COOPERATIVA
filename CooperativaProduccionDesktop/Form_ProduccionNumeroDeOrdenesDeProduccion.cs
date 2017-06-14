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
    public partial class Form_ProduccionNumeroDeOrdenesDeProduccion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        private int _periodoListado;
        private List<BlendDePeriodoViewModel> _ordenes;
        private List<LineaDetalle> _detalle;

        public Form_ProduccionNumeroDeOrdenesDeProduccion(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionNumeroDeOrdenesDeProduccion_Load;
            this.btnListar.Click += btnListar_Click;
            this.btnGuardar.Click += btnGuardar_Click;
        }

        void Form_ProduccionNumeroDeOrdenesDeProduccion_Load(object sender, EventArgs e)
        {
            this.txtPeriodo.Text = DateTime.Now.Year.ToString();

            _detalle = new List<LineaDetalle>();

            this.gridControlBlends.DataSource = new BindingList<LineaDetalle>(_detalle);

            this.gridViewBlends.Columns["Blend"].OptionsColumn.AllowEdit = false;

            var editorMask = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            editorMask.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            editorMask.Mask.EditMask = "[0-9]+";
            editorMask.Enter += (s, a) => (s as DevExpress.XtraEditors.BaseEdit).SelectAll();
            editorMask.Click += (s, a) => (s as DevExpress.XtraEditors.BaseEdit).SelectAll();
            this.gridViewBlends.Columns["Blend"].ColumnEdit = editorMask;
        }

        void btnListar_Click(object sender, EventArgs e)
        {
            int periodo;

            try
            {
                periodo = Convert.ToInt32(txtPeriodo.Text);

                if (periodo == 0)
                {
                    throw new Exception("Periodo Invalido");
                }
            }
            catch
            {
                _periodoListado = 0;
                _detalle.Clear();
                this.gridControlBlends.RefreshDataSource();
                return;
            }

            _periodoListado = periodo;

            _ordenes = _blendManager.ListarOrdenesDeProduccion(periodo);

            _detalle.Clear();

            _ordenes.ForEach(x => _detalle.Add(new LineaDetalle()
            {
                Blend = x.Descripcion,
                NumeroOrden = x.OrdenDeProduccion
            }));

            this.gridControlBlends.RefreshDataSource();
        }

        void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_periodoListado == 0)
            {
                return;
            }

            foreach (var item in _detalle)
	        {
	        	 _ordenes.Where(x => x.Descripcion == item.Blend).Single().OrdenDeProduccion = item.NumeroOrden;
	        }

            try
            {
                _blendManager.ActualizarOrdenesDeProduccion(_periodoListado, _ordenes);

                MessageBox.Show("Los cambios se han guardado correctamente", "Cambios Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("NO se ha podido guardar los cambios correctamente", "Cambios NO Guardados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        class LineaDetalle
        {
            public string Blend { get; set; }

            public int NumeroOrden { get; set; }
        }
    }
}
