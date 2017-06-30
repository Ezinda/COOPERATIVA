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
    public partial class Form_ProduccionHumedadImpresion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        public Form_ProduccionHumedadImpresion(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionTemperaturaImpresion_Load;
            this.dateDesde.ValueChanged += dateDesde_ValueChanged;
            this.dateHasta.ValueChanged += dateHasta_ValueChanged;
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
            var ordenDeProduccion = _blendManager.GetOrdenProduccion(desde.Year, blendId);
            var humedades = _blendManager.ListarControlesDeHumedad(new [] { blendId }, desde, hasta);
            var records = new List<ProduccionHumedadRecord>();
            var details = new List<ProduccionHumedadDetalleRecord>();
            
            humedades.ForEach(x =>
            {
                records.Add(new ProduccionHumedadRecord()
                {
                    _Fecha = x.Fecha.ToShortDateString(),
            
                    Corrida = x.Corrida,
                });

                details.AddRange(x.Lineas.Select(y => new ProduccionHumedadDetalleRecord()
                {
                    Fecha = x.Fecha.ToShortDateString(),
                    HoraMuestra = y.Hora.ToString(@"hh\:mm"),
                    TemperaturaEmpaque = y.TemperaturaEmpaque.ToString(),
                    NumeroCaja = y.Caja.ToString(),
                    NumeroCapsulaBrab = y.Capsula.ToString(),
                    HoraEntrada = y.HoraEntrada.ToString(@"hh\:mm"),
                    HoraSalida = y.HoraSalida.ToString(@"hh\:mm"),
                    HumBrab = y.Humedad.ToString(),
                    NumeroCapsulaHearson = String.Empty,
                    HumHearson = String.Empty
                }).ToList());
            }); 
            
            var reporte = new ProduccionHumedadReport();
            reporte.DataSource = records;
            reporte.Subreport.ReportSource.DataSource = details;
            
            reporte.Parameters["Blend"].Value = blend.Descripcion.Trim();
            reporte.Parameters["OrdenProduccion"].Value = ordenDeProduccion;
            return reporte;
        }

        void dateDesde_ValueChanged(object sender, EventArgs e)
        {
            this.dateHasta.Value = this.dateDesde.Value;
        }

        void dateHasta_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateDesde.Value.Year != this.dateHasta.Value.Year)
            {
                MessageBox.Show("Las fechas del rango deben pertenecer al mismo año o periodo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.dateHasta.Value = this.dateDesde.Value;
            }
        }

        void btnImprimir_Click(object sender, EventArgs e)
        {
            var reporte = _GenerarReporte((Guid)this.cbBlend.SelectedValue, this.dateDesde.Value.Date, this.dateHasta.Value.Date);

            Form_AdministracionWinReport wr = new Form_AdministracionWinReport();
            wr.Text = "Producción: Reporte de Humedad";
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
