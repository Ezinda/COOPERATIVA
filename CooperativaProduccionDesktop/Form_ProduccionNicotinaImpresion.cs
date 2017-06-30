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
    public partial class Form_ProduccionNicotinaImpresion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        public Form_ProduccionNicotinaImpresion(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionNicotinaImpresion_Load;
            this.dateDesde.ValueChanged += dateDesde_ValueChanged;
            this.dateHasta.ValueChanged += dateHasta_ValueChanged;
            this.btnImprimir.Click += btnImprimir_Click;
        }

        void Form_ProduccionNicotinaImpresion_Load(object sender, EventArgs e)
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
            var controles = _blendManager.ListarControlesDeNicotina(blendId, desde, hasta);
            var records = new List<ProduccionNicotinaRecord>();

            controles.ForEach(x =>
            {
                foreach (var linea in x.Lineas)
                {
                    records.Add(new ProduccionNicotinaRecord()
                    {
                        _Fecha = x.Fecha.ToShortDateString(),
                        _Hora = x.Hora.ToString(@"hh\:mm"),

                        Cajas = linea.CajaDesde + "-" + linea.CajaHasta,
                        PorcentajeH = linea.PorcentajeHumedad.ToString(),
                        Valor1 = linea.Valor1.ToString(),
                        Valor2 = linea.Valor2.ToString(),
                        PorcentajeALC = linea.PorcentajeALC.ToString(),
                        PorcentajeNicotina = linea.PorcentajeNicotina.ToString()
                    });
                }
            }); 
            
            var reporte = new ProduccionNicotinaReport();
            reporte.DataSource = records;
            
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
            wr.Text = "Producción: Reporte de Nicotina";
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
