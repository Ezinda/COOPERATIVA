using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopEntities.Models;
using CooperativaProduccion.ViewModels;
using CooperativaProduccion.Helpers;
using GestionRRHH.Services;

namespace CooperativaProduccion.Controls
{
    public partial class AdministracionGeneracionOPControl : UserControl
    {
        private CooperativaProduccionEntities Context;
        private LiquidacionesParaOPViewModel _liquidaciones;
        private IPagosManager _pagosManager;
        private IMessageBoxService _messageBoxService;

        public AdministracionGeneracionOPControl()
        {
            InitializeComponent();

            Context = new CooperativaProduccionEntities();

            _liquidaciones = new LiquidacionesParaOPViewModel()
            {
                Retenciones = new List<RetencionDetalleViewModel>(),
                Items = new List<LiquidacionParaOPViewModel>()
            };

            _pagosManager = new PagosManager();
            _messageBoxService = new MessageBoxService();

            // Generacion de Ordenes de Pago
            this.btnBuscarLiquidaciones.Click += this.btnBuscarLiquidaciones_Click;

            this.txtPorcentajePago.KeyPress += this.txtPorcentajePago_KeyPress;
            this.btnPorcentajePago.Click += this.btnPorcentajePago_Click;
            this.txtPagoPorKilo.KeyPress += this.txtPagoPorKilo_KeyPress;
            this.btnPagoPorKilo.Click += this.btnPagoPorKilo_Click;

            this.btnGenerarOP.Click += this.btnGenerarOP_Click;

            this.gridViewLiquidacion.SelectionChanged += gridViewLiquidacion_SelectionChanged;
            this.gridViewLiquidacion.CustomUnboundColumnData += gridViewLiquidacion_CustomUnboundColumnData;
        }

        public decimal PorcentajePorPagar
        {
            get
            {
                var result = decimal.Parse(txtPorcentajePago.Text);

                return result;
            }
            set
            {
                txtPorcentajePago.Text = Convert.ToString(value);
            }
        }

        public decimal KilosPorPagar
        {
            get
            {
                var result = decimal.Parse(txtPagoPorKilo.Text);

                return result;
            }
            set
            {
                txtPagoPorKilo.Text = Convert.ToString(value);
            }
        }

        void btnBuscarLiquidaciones_Click(object sender, EventArgs e)
        {
            var desde = dpDesdeLiquidacion.Value.Date;
            var hasta = dpHastaLiquidacion.Value.Date;

            BuscarLiquidaciones(desde, hasta);
        }

        void txtPorcentajePago_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtPorcentajePago.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                var desde = dpDesdeLiquidacion.Value.Date;
                var hasta = dpHastaLiquidacion.Value.Date;

                BuscarLiquidacionesEnPorcentaje(desde, hasta);
            }
        }

        void btnPorcentajePago_Click(object sender, EventArgs e)
        {
            var desde = dpDesdeLiquidacion.Value.Date;
            var hasta = dpHastaLiquidacion.Value.Date;

            BuscarLiquidacionesEnPorcentaje(desde, hasta);
        }

        void txtPagoPorKilo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtPagoPorKilo.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                var desde = dpDesdeLiquidacion.Value.Date;
                var hasta = dpHastaLiquidacion.Value.Date;

                BuscarLiquidacionesEnKilos(desde, hasta);
            }
        }

        void btnPagoPorKilo_Click(object sender, EventArgs e)
        {
            var desde = dpDesdeLiquidacion.Value.Date;
            var hasta = dpHastaLiquidacion.Value.Date;

            BuscarLiquidacionesEnKilos(desde, hasta);
        }

        void btnGenerarOP_Click(object sender, EventArgs e)
        {
            var confirm = _messageBoxService.ShowConfirm("Confirmación de Datos",
                    "¿Desea generar ordenes de pago?");

            if (confirm)
            {
                GenerarOP();

                var desde = dpDesdeLiquidacion.Value.Date;
                var hasta = dpHastaLiquidacion.Value.Date;

                BuscarLiquidaciones(desde, hasta);
            }
        }

        void gridViewLiquidacion_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            CalcularValores();
        }

        void gridViewLiquidacion_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var fieldname = e.Column.FieldName;

                if (fieldname == "NetoPorPagar")
                {
                    var row = e.Row as RowLiquidacion;
                    var orden = _liquidaciones.Items.Where(x => x.PesadaId == row.ID).Single();

                    e.Value = orden.NetoPorPagar;
                }
                else
                {
                    var retencion = _liquidaciones.Retenciones.Where(x => x.Nombre == fieldname).FirstOrDefault();

                    if (retencion == null)
                    {
                        return;
                    }

                    var row = e.Row as RowLiquidacion;
                    var orden = _liquidaciones.Items.Where(x => x.PesadaId == row.ID).Single();
                    var retencionaplicada = orden.RetencionesAplicadas.Where(x => x.Nombre == retencion.Nombre).Single();

                    e.Value = retencionaplicada.Importe;
                }
            }
        }

        public void Iniciar()
        {
            var numerodeorden = _pagosManager.GetNumeroDeOrden();

            dpDesdeLiquidacion.Value = DateTime.Now.Date.AddYears(-16);
            dpFechaPago.Value = DateTime.Now.Date;

            txtNumeroOP.Text = numerodeorden.ToString();
            
            txtPorcentajePago.Text = "100.00";
            txtOtrosConceptos.Text = "0.00";
            txtAnticipos.Text = "0.00";
            txtTotalAfectado.Text = "0.00";

            txtGanancias.Text = "0.00";
            txtIIBB.Text = "0.00";
            txtSaludPublica.Text = "0.00";
            txtEEAOC.Text = "0.00";
            txtRiego.Text = "0.00";
            txtGADM.Text = "0.00";
            
            txtNeto.Text = "0.00";
        }

        public void BuscarLiquidaciones()
        {
            _liquidaciones = _pagosManager.ListarLiquidacionesParaPagarEnPorcentaje(100);//(porcentajedepago);

            ActualizarControlesGeneracionDeOrdenes();
        }

        public void BuscarLiquidaciones(DateTime desde, DateTime hasta)
        {
            _liquidaciones = _pagosManager.ListarLiquidacionesParaPagarEnPorcentaje(100, desde, hasta);//(porcentajedepago, desde, hasta);

            ActualizarControlesGeneracionDeOrdenes();
        }

        public void BuscarLiquidacionesEnPorcentaje()
        {
            var porcentajedepago = this.PorcentajePorPagar;

            _liquidaciones = _pagosManager.ListarLiquidacionesParaPagarEnPorcentaje(porcentajedepago);

            ActualizarControlesGeneracionDeOrdenes();
        }

        public void BuscarLiquidacionesEnPorcentaje(DateTime desde, DateTime hasta)
        {
            var porcentajedepago = this.PorcentajePorPagar;

            _liquidaciones = _pagosManager.ListarLiquidacionesParaPagarEnPorcentaje(porcentajedepago, desde, hasta);

            ActualizarControlesGeneracionDeOrdenes();
        }

        public void BuscarLiquidacionesEnKilos()
        {
            var kilosapagar = this.KilosPorPagar;

            _liquidaciones = _pagosManager.ListarLiquidacionesParaPagarEnKilos(kilosapagar);

            ActualizarControlesGeneracionDeOrdenes();
        }

        public void BuscarLiquidacionesEnKilos(DateTime desde, DateTime hasta)
        {
            var kilosapagar = this.KilosPorPagar;

            _liquidaciones = _pagosManager.ListarLiquidacionesParaPagarEnKilos(kilosapagar, desde, hasta);

            ActualizarControlesGeneracionDeOrdenes();
        }

        private void ActualizarControlesGeneracionDeOrdenes()
        {
            var source = _liquidaciones.Items
                .Select(x => new RowLiquidacion()
                {
                    ID = x.PesadaId,
                    PRODUCTORID = x.ProductorId,
                    NUMINTLIQ = x.NumeroDeLiquidacionInterno,
                    NUMAFIPLIQ = x.NumeroDeLiquidacionDeAFIP,
                    Fecha = x.FechaDeLiquidacionDeAFIP,
                    Productor = x.NombreDeProductor,
                    FET = x.NumeroDeFET,
                    NroComprobante = x.NumeroDeLiquidacionDeAFIP,
                    Kilos = x.TotalEnKilos,
                    ImporteBruto = x.ImporteBrutoSinIVA,
                    ImportePorPagar = x.ImportePorPagar
                })
                .ToList();

            gridViewLiquidacion.Columns.Clear();

            gridControlLiquidacion.DataSource = new BindingList<RowLiquidacion>(source);

            gridViewLiquidacion.Columns["ID"].Visible = false;
            gridViewLiquidacion.Columns["PRODUCTORID"].Visible = false;
            gridViewLiquidacion.Columns["NUMINTLIQ"].Visible = false;
            gridViewLiquidacion.Columns["NUMAFIPLIQ"].Visible = false;

            foreach (var item in _liquidaciones.Retenciones)
            {
                var unbColumn = gridViewLiquidacion.Columns.AddField(item.Nombre);

                unbColumn.VisibleIndex = gridViewLiquidacion.Columns.Count;
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                unbColumn.OptionsColumn.AllowEdit = false;

                //unbColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //unbColumn.DisplayFormat.FormatString = "c";
                //
                //unbColumn.AppearanceCell.BackColor = Color.LemonChiffon;
            }

            var unbColumnNeto = gridViewLiquidacion.Columns.AddField("NetoPorPagar");

            unbColumnNeto.VisibleIndex = gridViewLiquidacion.Columns.Count;
            unbColumnNeto.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            unbColumnNeto.OptionsColumn.AllowEdit = false;

            gridViewLiquidacion.BestFitColumns();

            gridViewLiquidacion.SelectAll();

            CalcularValores();
        }

        private void CalcularValores()
        {
            var totalkg = 0.0;
            var totalpesos = 0.0m;
            var idsproductores = new List<Guid>();
            var totalproductores = 0.0m;

            var totalafectar = 0.0m;
            var totalafectado = 0.0m;

            var totalganancias = 0.0m;
            var totaliibb = 0.0m;
            var totalsalud = 0.0m;
            var totaleeaoc = 0.0m;
            var totalriego = 0.0m;
            var totalgadm = 0.0m;

            var totalneto = 0.0m;

            var selectedrowhandles = gridViewLiquidacion.GetSelectedRows();

            foreach (var rowhandle in selectedrowhandles)
            {
                var kilos = gridViewLiquidacion.GetRowCellValue(rowhandle, "Kilos") as Nullable<Double>;
                var pesos = gridViewLiquidacion.GetRowCellValue(rowhandle, "ImporteBruto") as Nullable<Decimal>;
                var productorId = (Guid)gridViewLiquidacion.GetRowCellValue(rowhandle, "PRODUCTORID");
                var afectar = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "ImportePorPagar");

                var gcias = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "GCIAS");
                var iibb = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "IIBB");
                var salud = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "SaludPublica");
                var eeaoc = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "EEAOC");
                var riego = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "Riego");
                var gadm = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "GADM");

                var neto = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "NetoPorPagar");
                
                totalkg += kilos ?? 0.0;
                totalpesos += pesos ?? 0.0m;

                if (!idsproductores.Contains(productorId))
                {
                    totalproductores++;
                    idsproductores.Add(productorId);
                }

                totalafectar += afectar;
                totalafectado += afectar;
                
                totalganancias += gcias;
                totaliibb += iibb;
                totalsalud += salud;
                totaleeaoc += eeaoc;
                totalriego += riego;
                totalgadm += gadm;

                totalneto += neto;
            }

            var totalkground = decimal.Round(Convert.ToDecimal(totalkg),
                2,
                MidpointRounding.AwayFromZero);
            var totalpesosround = decimal.Round(totalpesos,
                2,
                MidpointRounding.AwayFromZero);
            var totalafectarround = decimal.Round(totalafectar,
                2,
                MidpointRounding.AwayFromZero);
            var totalafectadoround = decimal.Round(totalafectado,
                2,
                MidpointRounding.AwayFromZero);
            
            var totalgananciasround = decimal.Round(totalganancias,
                2,
                MidpointRounding.AwayFromZero);
            var totaliibbround = decimal.Round(totaliibb,
                2,
                MidpointRounding.AwayFromZero);
            var totalsaludround = decimal.Round(totalsalud,
                2,
                MidpointRounding.AwayFromZero);
            var totaleeaocround = decimal.Round(totaleeaoc,
                2,
                MidpointRounding.AwayFromZero);
            var totalriegoround = decimal.Round(totalriego,
                2,
                MidpointRounding.AwayFromZero);
            var totalgadmround = decimal.Round(totalgadm,
                2,
                MidpointRounding.AwayFromZero);

            var totalnetoround = decimal.Round(totalneto,
                2,
                MidpointRounding.AwayFromZero);

            txtTotalKg.Text = totalkground.ToString();
            txtTotalPesos.Text = totalpesosround.ToString();
            txtProductores.Text = totalproductores.ToString();
            txtTotalAfectar.Text = totalafectarround.ToString();
            txtTotalAfectado.Text = totalafectadoround.ToString();
            
            txtGanancias.Text = totalgananciasround.ToString();
            txtIIBB.Text = totaliibbround.ToString();
            txtSaludPublica.Text = totalsaludround.ToString();
            txtEEAOC.Text = totaleeaocround.ToString();
            txtRiego.Text = totalriegoround.ToString();
            txtGADM.Text = totalgadmround.ToString();
            
            txtNeto.Text = totalnetoround.ToString();
        }

        private void GenerarOP()
        {
            var selectedrowhandles = gridViewLiquidacion.GetSelectedRows();

            if (selectedrowhandles.Count() == 0)
            {
                return;
            }

            var retenciones = new String[]
            {
                RetencionTypes.RetencionIIBB,
                RetencionTypes.RetencionEEAOC,
                RetencionTypes.RetencionSaludPublica,
                RetencionTypes.RetencionGADM,
                RetencionTypes.RetencionGCIAS,
                RetencionTypes.RetencionRiego,
            };

            var retencionesdeordenvm = new List<RetencionAplicadaViewModel>();
            foreach (var item in retenciones)
            {
                var retencionvm = new RetencionAplicadaViewModel()
                {
                    Nombre = item,
                    Importe = 0
                };

                retencionesdeordenvm.Add(retencionvm);
            }

            var netodeorden = 0m;
            var itemsvm = new List<ConceptoDeOrdenDePagoViewModel>();
            foreach (var rowhandle in selectedrowhandles)
            {
                
                var row = gridViewLiquidacion.GetRow(rowhandle) as RowLiquidacion;
                
                var retencionesdeconceptovm = new List<RetencionAplicadaViewModel>();

                foreach (var item in retenciones)
	            {
                    var importe = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, item);

                    var retencionaplicada = new RetencionAplicadaViewModel()
                    {
                        Nombre = item,
                        Importe = importe
                    };
                    retencionesdeconceptovm.Add(retencionaplicada);

                    var retenciondeorden = retencionesdeordenvm.Where(x => x.Nombre == item).Single();
                    retenciondeorden.Importe += importe;
	            }

                var neto = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "NetoPorPagar");
                var kilos = (decimal)gridViewLiquidacion.GetRowCellValue(rowhandle, "Kilos");

                var conceptovm = new ConceptoDeOrdenDePagoViewModel()
                {
                    PesadaId = row.ID,
                    ProductorId = row.PRODUCTORID,
                    KilosAfectados = Convert.ToDecimal(row.Kilos),
                    ImportePorPagar = row.ImportePorPagar,
                    RetencionesAplicadas = retencionesdeconceptovm,
                    
                    NetoPorPagar = neto,
                };

                itemsvm.Add(conceptovm);

                netodeorden += neto;
            }

            var fechadepago = dpFechaPago.Value.Date;
            var observaciones = txtObservaciones.Text.Trim();

            var ordendepagovm = new OrdenDePagoViewModel()
            {
                Id = Guid.NewGuid(),
                NumeroDeOrden = 0,
                NumeroInternoDeOrden = 0,
                FechaDePago = fechadepago,
                RetencionesAplicadas = retencionesdeordenvm,
                Items = itemsvm,
                ImporteNeto = netodeorden,
                Observaciones = observaciones
            };

            _pagosManager.GenerarOrdenDePago(ordendepagovm);
        }

        class RowLiquidacion
        {
            public Guid ID { get; set; }
            public Guid PRODUCTORID { get; set; }
            public Nullable<long> NUMINTLIQ { get; set; }
            public string NUMAFIPLIQ { get; set; } // su valor es igual al de NUMEROCOMPROBANTE
            public Nullable<DateTime> Fecha { get; set; }
            public string Productor { get; set; }
            public string FET { get; set; }
            public string NroComprobante { get; set; }
            public Nullable<double> Kilos { get; set; }
            public Nullable<decimal> ImporteBruto { get; set; }
            public decimal ImportePorPagar { get; set; }
        }
    }
}
