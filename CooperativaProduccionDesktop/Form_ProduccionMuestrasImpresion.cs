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
    public partial class Form_ProduccionMuestrasImpresion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        public Form_ProduccionMuestrasImpresion(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionMuestrasImpresion_Load;
            this.dateDesde.ValueChanged += dateDesde_ValueChanged;
            this.btnImprimir.Click += btnImprimir_Click;
        }

        void Form_ProduccionMuestrasImpresion_Load(object sender, EventArgs e)
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
            var muestras = _blendManager.ListarMuestrasConDetalle(blendId, desde, hasta);
            var records = new List<ProduccionMuestraReportRecord>();

            for (int i = 0; i < muestras.Count; i += 4)
            {
                var items = muestras.Skip(i).Take(4).ToList();
                var record = new ProduccionMuestraReportRecord();

                if (items.IsValidIndex(0))
                {
                             record.Caja01 = items[0].Caja;
                          record.Corrida01 = items[0].Corrida;
                            record.Fecha01 = items[0].Fecha.ToShortDateString();
                             record.Hora01 = items[0].Hora.ToString(@"hh\:mm");
                           record.TamUno01 = items[0].Lineas.Where(x => x.Tamanio == "1' y m/").Single().kilos;
                           record.PorUno01 = items[0].Lineas.Where(x => x.Tamanio == "1' y m/").Single().Porcentaje;
                           record.TamDos01 = items[0].Lineas.Where(x => x.Tamanio == "1/2").Single().kilos;
                           record.PorDos01 = items[0].Lineas.Where(x => x.Tamanio == "1/2").Single().Porcentaje;
                        record.TamCuatro01 = items[0].Lineas.Where(x => x.Tamanio == "1/4").Single().kilos;
                        record.PorCuatro01 = items[0].Lineas.Where(x => x.Tamanio == "1/4").Single().Porcentaje;
                          record.TamOcho01 = items[0].Lineas.Where(x => x.Tamanio == "1/8").Single().kilos;
                          record.PorOcho01 = items[0].Lineas.Where(x => x.Tamanio == "1/8").Single().Porcentaje;
                           record.TamPan01 = items[0].Lineas.Where(x => x.Tamanio == "PAN").Single().kilos;
                           record.PorPan01 = items[0].Lineas.Where(x => x.Tamanio == "PAN").Single().Porcentaje;
                        record.TamPTotal01 = items[0].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().kilos;
                        record.PorPTotal01 = items[0].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().Porcentaje;
                          record.TamVObj01 = items[0].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().kilos;
                          record.PorVObj01 = items[0].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().Porcentaje;
                         record.TamFibra01 = items[0].Lineas.Where(x => x.Tamanio == "FIBRA").Single().kilos;
                         record.PorFibra01 = items[0].Lineas.Where(x => x.Tamanio == "FIBRA").Single().Porcentaje;
                    record.Observaciones01 = items[0].Observaciones;
                            record.Total01 = items[0].TotalSobreUnMedio;
                               record.PM01 = items[0].PesoMuestra;
                }

                if (items.IsValidIndex(1))
                {
                             record.Caja02 = items[1].Caja;
                          record.Corrida02 = items[1].Corrida;
                            record.Fecha02 = items[1].Fecha.ToShortDateString();
                             record.Hora02 = items[1].Hora.ToString(@"hh\:mm");
                           record.TamUno02 = items[1].Lineas.Where(x => x.Tamanio == "1' y m/").Single().kilos;
                           record.PorUno02 = items[1].Lineas.Where(x => x.Tamanio == "1' y m/").Single().Porcentaje;
                           record.TamDos02 = items[1].Lineas.Where(x => x.Tamanio == "1/2").Single().kilos;
                           record.PorDos02 = items[1].Lineas.Where(x => x.Tamanio == "1/2").Single().Porcentaje;
                        record.TamCuatro02 = items[1].Lineas.Where(x => x.Tamanio == "1/4").Single().kilos;
                        record.PorCuatro02 = items[1].Lineas.Where(x => x.Tamanio == "1/4").Single().Porcentaje;
                          record.TamOcho02 = items[1].Lineas.Where(x => x.Tamanio == "1/8").Single().kilos;
                          record.PorOcho02 = items[1].Lineas.Where(x => x.Tamanio == "1/8").Single().Porcentaje;
                           record.TamPan02 = items[1].Lineas.Where(x => x.Tamanio == "PAN").Single().kilos;
                           record.PorPan02 = items[1].Lineas.Where(x => x.Tamanio == "PAN").Single().Porcentaje;
                        record.TamPTotal02 = items[1].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().kilos;
                        record.PorPTotal02 = items[1].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().Porcentaje;
                          record.TamVObj02 = items[1].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().kilos;
                          record.PorVObj02 = items[1].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().Porcentaje;
                         record.TamFibra02 = items[1].Lineas.Where(x => x.Tamanio == "FIBRA").Single().kilos;
                         record.PorFibra02 = items[1].Lineas.Where(x => x.Tamanio == "FIBRA").Single().Porcentaje;
                    record.Observaciones02 = items[1].Observaciones;
                            record.Total02 = items[1].TotalSobreUnMedio;
                               record.PM02 = items[1].PesoMuestra;
                }

                if (items.IsValidIndex(2))
                {
                             record.Caja03 = items[2].Caja;
                          record.Corrida03 = items[2].Corrida;
                            record.Fecha03 = items[2].Fecha.ToShortDateString();
                             record.Hora03 = items[2].Hora.ToString(@"hh\:mm");
                           record.TamUno03 = items[2].Lineas.Where(x => x.Tamanio == "1' y m/").Single().kilos;
                           record.PorUno03 = items[2].Lineas.Where(x => x.Tamanio == "1' y m/").Single().Porcentaje;
                           record.TamDos03 = items[2].Lineas.Where(x => x.Tamanio == "1/2").Single().kilos;
                           record.PorDos03 = items[2].Lineas.Where(x => x.Tamanio == "1/2").Single().Porcentaje;
                        record.TamCuatro03 = items[2].Lineas.Where(x => x.Tamanio == "1/4").Single().kilos;
                        record.PorCuatro03 = items[2].Lineas.Where(x => x.Tamanio == "1/4").Single().Porcentaje;
                          record.TamOcho03 = items[2].Lineas.Where(x => x.Tamanio == "1/8").Single().kilos;
                          record.PorOcho03 = items[2].Lineas.Where(x => x.Tamanio == "1/8").Single().Porcentaje;
                           record.TamPan03 = items[2].Lineas.Where(x => x.Tamanio == "PAN").Single().kilos;
                           record.PorPan03 = items[2].Lineas.Where(x => x.Tamanio == "PAN").Single().Porcentaje;
                        record.TamPTotal03 = items[2].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().kilos;
                        record.PorPTotal03 = items[2].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().Porcentaje;
                          record.TamVObj03 = items[2].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().kilos;
                          record.PorVObj03 = items[2].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().Porcentaje;
                         record.TamFibra03 = items[2].Lineas.Where(x => x.Tamanio == "FIBRA").Single().kilos;
                         record.PorFibra03 = items[2].Lineas.Where(x => x.Tamanio == "FIBRA").Single().Porcentaje;
                    record.Observaciones03 = items[2].Observaciones;
                            record.Total03 = items[2].TotalSobreUnMedio;
                               record.PM03 = items[2].PesoMuestra;
                }

                if (items.IsValidIndex(3))
                {
                             record.Caja04 = items[3].Caja;
                          record.Corrida04 = items[3].Corrida;
                            record.Fecha04 = items[3].Fecha.ToShortDateString();
                             record.Hora04 = items[3].Hora.ToString(@"hh\:mm");
                           record.TamUno04 = items[3].Lineas.Where(x => x.Tamanio == "1' y m/").Single().kilos;
                           record.PorUno04 = items[3].Lineas.Where(x => x.Tamanio == "1' y m/").Single().Porcentaje;
                           record.TamDos04 = items[3].Lineas.Where(x => x.Tamanio == "1/2").Single().kilos;
                           record.PorDos04 = items[3].Lineas.Where(x => x.Tamanio == "1/2").Single().Porcentaje;
                        record.TamCuatro04 = items[3].Lineas.Where(x => x.Tamanio == "1/4").Single().kilos;
                        record.PorCuatro04 = items[3].Lineas.Where(x => x.Tamanio == "1/4").Single().Porcentaje;
                          record.TamOcho04 = items[3].Lineas.Where(x => x.Tamanio == "1/8").Single().kilos;
                          record.PorOcho04 = items[3].Lineas.Where(x => x.Tamanio == "1/8").Single().Porcentaje;
                           record.TamPan04 = items[3].Lineas.Where(x => x.Tamanio == "PAN").Single().kilos;
                           record.PorPan04 = items[3].Lineas.Where(x => x.Tamanio == "PAN").Single().Porcentaje;
                        record.TamPTotal04 = items[3].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().kilos;
                        record.PorPTotal04 = items[3].Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().Porcentaje;
                          record.TamVObj04 = items[3].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().kilos;
                          record.PorVObj04 = items[3].Lineas.Where(x => x.Tamanio == "V/O BJ").Single().Porcentaje;
                         record.TamFibra04 = items[3].Lineas.Where(x => x.Tamanio == "FIBRA").Single().kilos;
                         record.PorFibra04 = items[3].Lineas.Where(x => x.Tamanio == "FIBRA").Single().Porcentaje;
                    record.Observaciones04 = items[3].Observaciones;
                            record.Total04 = items[3].TotalSobreUnMedio;
                               record.PM04 = items[3].PesoMuestra;
                }

                records.Add(record);
            }

            var reporte = new ProduccionMuestraReport();
            reporte.DataSource = records;

            reporte.Parameters["Blend"].Value = blend.Descripcion.Trim();
            reporte.Parameters["OrdenProduccion"].Value = ordenDeProduccion;
            reporte.Parameters["Observaciones"].Value = String.Empty;

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
