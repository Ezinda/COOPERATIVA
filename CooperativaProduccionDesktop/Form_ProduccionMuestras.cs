using CooperativaProduccion.Helpers;
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
    public partial class Form_ProduccionMuestras : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //private CooperativaProduccionEntities _context;
        private IBlendManager _blendManager;

        private List<LineaMuestra> _detalle;

        public Form_ProduccionMuestras(IBlendManager blendManager)
        {
            InitializeComponent();

            //_context = new CooperativaProduccionEntities();
            _blendManager = blendManager;

            this.Load += Form_ProduccionMuestras_Load;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnNuevo.Click += btnNuevo_Click;
        }

        void Form_ProduccionMuestras_Load(object sender, EventArgs e)
        {
            this.dateFecha.Value = DateTime.Now.Date;
            
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

            _detalle = new List<LineaMuestra>();

            this.gridControlMuestras.DataSource = new BindingList<LineaMuestra>(_detalle);

            this.gridViewMuestras.OptionsMenu.EnableColumnMenu = false;
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

            _detalle = _blendManager.ListarMuestras(blendId, fecha)
                .Select(x => new LineaMuestra()
                {
                    Blend = x.Blend.Descripcion,
                    Fecha = x.Fecha.ToShortDateString(),
                    Hora = x.Hora.ToString(@"hh\:mm"),
                    Corrida = x.Corrida.ToString(),
                    Caja = x.Caja.ToString()
                })
                .OrderBy(x => x.Hora)
                .ThenBy(x => x.Corrida)
                .ToList();

            this.gridControlMuestras.DataSource = new BindingList<LineaMuestra>(_detalle);
        }

        void btnNuevo_Click(object sender, EventArgs e)
        {
            new Form_ProduccionMuestrasEditor(_blendManager).Show(this);
        }

        class Blend
        {
            public Guid Id { get; set; }

            public string Descripcion { get; set; }
        }

        class LineaMuestra
        {
            public string Blend { get; set; }

            public string Fecha { get; set; }

            public string Hora { get; set; }

            public string Corrida { get; set; }

            public string Caja { get; set; }
        }
    }
}
