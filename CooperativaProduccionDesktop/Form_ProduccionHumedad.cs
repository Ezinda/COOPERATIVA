using CooperativaProduccion.Helpers;
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
    public partial class Form_ProduccionHumedad : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        private List<LineaControl> _detalle;

        public Form_ProduccionHumedad(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionHumedad_Load;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnNuevo.Click += btnNuevo_Click;
        }

        void Form_ProduccionHumedad_Load(object sender, EventArgs e)
        {
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

            _detalle = new List<LineaControl>();

            this.gridControlHumedad.DataSource = new BindingList<LineaControl>(_detalle);

            this.gridViewHumedad.OptionsMenu.EnableColumnMenu = false;
            //this.gridViewMuestras.OptionsView.ColumnAutoWidth = false;
            //this.gridViewMuestra.Columns["Tamanio"].Caption = "Tamaño".ToUpper();
            //this.gridViewMuestra.Columns["Tamanio"].OptionsColumn.AllowEdit = false;
            //this.gridViewMuestra.Columns["Tamanio"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //this.gridViewMuestra.Columns["Tamanio"].MinWidth = this.gridViewMuestra.Columns["Tamanio"].GetBestWidth();
            //this.gridViewMuestra.Columns["Tamanio"].MaxWidth = this.gridViewMuestra.Columns["Tamanio"].GetBestWidth();
            //this.gridViewMuestra.Columns["Kilos"].Caption = "Kgs".ToUpper();
            //this.gridViewMuestra.Columns["Kilos"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //this.gridViewMuestra.Columns["Porcentaje"].Caption = "%".ToUpper();
            //this.gridViewMuestra.Columns["Porcentaje"].OptionsColumn.AllowEdit = false;
            //this.gridViewMuestra.Columns["Porcentaje"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

            //var editorMask = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            //editorMask.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //editorMask.Mask.EditMask = "[0-9]+";
            //editorMask.Enter += (s, a) => (s as DevExpress.XtraEditors.BaseEdit).SelectAll();
            //editorMask.Click += (s, a) => (s as DevExpress.XtraEditors.BaseEdit).SelectAll();
            //this.gridViewMuestra.Columns["Kilos"].ColumnEdit = editorMask;
        }

        void btnBuscar_Click(object sender, EventArgs e)
        {
            var fecha = dateFecha.Value.Date;
            var blendId = (Guid)cbBlend.SelectedValue;

            var controles = _blendManager.ListarControlesDeHumedad(blendId, fecha);
                //.Select(x => new LineaControl()
                //{
                //    Blend = x.Blend.Descripcion,
                //    Fecha = x.Fecha.ToShortDateString(),
                //    Hora = x.Hora.ToString(@"hh\:mm"),
                //    Corrida = x.Corrida.ToString(),
                //    Caja = x.Caja.ToString()
                //})
                //.OrderBy(x => x.Hora)
                //.ThenBy(x => x.Corrida)
                //.ToList();

            _detalle = new List<LineaControl>();

            foreach (var control in controles)
            {
                foreach (var linea in control.Lineas)
                {
                    _detalle.Add(new LineaControl()
                    {
                        Blend = control.Blend.Descripcion,
                        Fecha = control.Fecha.ToShortDateString(),
                        Corrida = control.Corrida.ToString(),
                        Hora = linea.Hora.ToString(@"hh\:mm"),
                        Caja = linea.Caja.ToString()
                    });
                }
            }

            this.gridControlHumedad.DataSource = new BindingList<LineaControl>(_detalle);
        }

        void btnNuevo_Click(object sender, EventArgs e)
        {
            new Form_ProduccionHumedadEditor(_blendManager).Show(this);
        }

        class Blend
        {
            public Guid Id { get; set; }

            public string Descripcion { get; set; }
        }

        class LineaControl
        {
            public string Blend { get; set; }

            public string Fecha { get; set; }

            public string Hora { get; set; }

            public string Corrida { get; set; }

            public string Caja { get; set; }
        }
    }
}
