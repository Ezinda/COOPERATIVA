using CooperativaProduccion.Helpers;
using CooperativaProduccion.ReportModels;
using CooperativaProduccion.Reports;
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
    public partial class Form_ProduccionTemperaturaImpresion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        public Form_ProduccionTemperaturaImpresion(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionTemperaturaImpresion_Load;
            this.dateDesde.ValueChanged += dateDesde_ValueChanged;
            this.btnImprimir.Click += btnImprimir_Click;
        }

        void Form_ProduccionTemperaturaImpresion_Load(object sender, EventArgs e)
        {
            this.dateDesde.Value = DateTime.Now.Date;

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
        }

        private DevExpress.XtraReports.UI.XtraReport _GenerarReporte(Guid blendId, DateTime desde, DateTime hasta)
        {
            var blend = _blendManager.GetBlend(blendId);
            var temperaturas = _blendManager.ListarControlesDeTemperatura(blendId, desde, hasta);
            var records = new List<ProduccionTemperaturaRecord>();
            var details = new List<ProduccionTemperaturaDetalleRecord>();

            temperaturas.ForEach(x =>
            {
                records.Add(new ProduccionTemperaturaRecord()
                {
                    _Fecha = x.Fecha.ToShortDateString(),

                    Corrida = x.Corrida,
                    Minimo = x.Minimo,
                    Meta = x.Meta,
                    Maximo = x.Maximo
                });

                details.AddRange(x.Lineas.Select(y => new ProduccionTemperaturaDetalleRecord()
                {
                    Fecha = x.Fecha.ToShortDateString(),
                    Hora = y.Hora.ToString(@"hh\:mm"),
                    Turno = String.Empty,
                    Caja = y.Caja,
                    TempEmpaque = y.TemperaturaEmpaque,
                    Ejecuto = String.Empty,
                    TempAmbiente = y.TemperaturaAmbiente,
                    Observaciones = y.Observaciones
                }).ToList());
            }); 
            
            var reporte = new ProduccionTemperaturaReport();
            reporte.DataSource = records;
            reporte.Subreport.ReportSource.DataSource = details;
            
            reporte.Parameters["Blend"].Value = blend.Descripcion.Trim();
            reporte.Parameters["OrdenProduccion"].Value = blend.OrdenProduccion;
            return reporte;
        }

        void dateDesde_ValueChanged(object sender, EventArgs e)
        {
            this.dateHasta.Value = this.dateDesde.Value;
        }

        void btnImprimir_Click(object sender, EventArgs e)
        {
            var reporte = _GenerarReporte((Guid)this.cbBlend.SelectedValue, this.dateDesde.Value.Date, this.dateHasta.Value.Date);

            Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
            wr.Text = "Producción: Reporte de Muestras";
            wr.documentViewerReports.DocumentSource = reporte;
            
            wr.Show();
        }

        class Blend
        {
            public Guid Id { get; set; }

            public string Descripcion { get; set; }
        }
    }
}
