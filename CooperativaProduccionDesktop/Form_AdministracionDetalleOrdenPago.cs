using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DesktopEntities.Models;
using DevExpress.Utils;
using CooperativaProduccion.ViewModels;
using CooperativaProduccion.Helpers;
using DevExpress.XtraEditors.Repository;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionDetalleOrdenPago : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private CooperativaProduccionEntities Context { get; set; }
        private static String[] _columns = new String[]
        {
            "Id",             //0
            "Fecha",          //1
            "Letra",          //2
            "N°", //3
            "Saldo",          //4
            "Afectar",        //5
            
            RetencionTypes.RetencionIIBB,
            RetencionTypes.RetencionEEAOC,
            RetencionTypes.RetencionSaludPublica,
            RetencionTypes.RetencionGADM,
            RetencionTypes.RetencionGCIAS,
            RetencionTypes.RetencionRiego,
            
            "Neto" //12
        };
        private MaskedTextBox _maskedtextbox;


        private int currentRow;
        private decimal coeficiente = decimal.Parse("1.21");
        private decimal iva = decimal.Parse("0.21");
        private decimal gcias = decimal.Parse("0.02");
        private decimal iibb = decimal.Parse("0.175");
        private decimal coeficientegral = decimal.Parse("0.05");

        private Guid _ordendepagoid;

        private IPagosManager _pagosManager;
        private IProductoresManager _productoresManager;

        public Form_AdministracionDetalleOrdenPago(Guid ordendpagoid)
            : this()
        {
            _ordendepagoid = ordendpagoid;
            CargarDatos();
        }

        private Form_AdministracionDetalleOrdenPago()
        {
            InitializeComponent();

            //Context = new CooperativaProduccionEntities();
            
            _pagosManager = new PagosManager();
            _productoresManager = new ProductoresManager();

            this.gridViewPendientes.CellValueChanged += gridViewPendientes_CellValueChanged;

            this.btnPago.Click += this.btnPago_Click;
            this.btnCancelar.Click += this.btnCancelar_Click;
        }

        private bool _cellvaluerounded = false;
        void gridViewPendientes_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var fieldname = e.Column.FieldName;

            if (fieldname == "Afectar")
            {
                if (_cellvaluerounded)
                {
                    //Recalcular();
                    _cellvaluerounded = false;
                    return;
                }

                var value = Math.Round((decimal)e.Value, 2);

                var idcolumn = gridViewPendientes.Columns["Id"];
                var id = (Guid)gridViewPendientes.GetRowCellValue(e.RowHandle, idcolumn);
                var resta = Math.Round(_DetalleDeordenDePago.Where(x => x.Id == id).Select(x => x.RestaPorPagar).Single());

                var definitivo = 0.00m;

                if (value < 0)
                {
                    definitivo = 0.00m;
                }
                else if (value > resta)
                {
                    definitivo = resta;
                }
                else
                {
                    definitivo = value;
                }
                
                _cellvaluerounded = true;
                gridViewPendientes.SetRowCellValue(e.RowHandle, e.Column, definitivo);
            }
        }

        void btnPago_Click(object sender, EventArgs e)
        {
            var neto = txtNeto.Text;
            var formaPago = new Form_AdministracionFormaPago(_ordendepagoid, neto);

            formaPago.Show();
        }

        void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private OrdenDePagoDetalleViewModel _OrdenDePago;
        private List<ConceptoDescripcionDeOrdenDePagoViewModel> _DetalleDeordenDePago;
        
        private void CargarDatos()
        {
            var ordenesdepagovm = _pagosManager.GetOrdenDePago(_ordendepagoid);
            var ordenvm = ordenesdepagovm.Items[0];
            var productorvm = _productoresManager.GetProductor(ordenvm.ProductorId);

            _OrdenDePago = ordenvm;

            ActualizarControlesOrdenDePago(ordenvm, productorvm);

            var conceptosvm = _pagosManager.ListarConceptosDeOrdenDePago(ordenvm.Id);

            _DetalleDeordenDePago = conceptosvm;

            #region Grid Conceptos Imputados

            var source = conceptosvm.Select(x =>
                new RowConceptosImputados()
                {
                    Fecha = x.Fecha,
                    Letra = x.TipoDeFactura,
                    Importe = x.ImportePorPagar,
                    PuntoDeVenta = x.PuntoDeVenta,
                    NumeroDeLiquidacion = x.NumeroDeLiquidacion,
                    Kilos = x.Kilos,
                });

            gridControlConceptosImputados.DataSource = source;

            var columnaFecha = gridViewConceptosImputados.Columns["Fecha"];
            columnaFecha.Width = 60;
            columnaFecha.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            columnaFecha.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnaLetra = gridViewConceptosImputados.Columns["Letra"];
            columnaLetra.Width = 50;
            columnaLetra.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            columnaLetra.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnaPuntoDeVenta = gridViewConceptosImputados.Columns["PuntoDeVenta"];
            columnaPuntoDeVenta.Caption = "Estab.";
            columnaPuntoDeVenta.Width = 50;
            columnaPuntoDeVenta.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            columnaPuntoDeVenta.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnaNumeroDeLiquidacion = gridViewConceptosImputados.Columns["NumeroDeLiquidacion"];
            columnaNumeroDeLiquidacion.Caption = "N°";
            columnaNumeroDeLiquidacion.Width = 70;
            columnaNumeroDeLiquidacion.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            columnaNumeroDeLiquidacion.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnaImporte = gridViewConceptosImputados.Columns["Importe"];
            columnaImporte.Width = 70;
            columnaImporte.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            columnaImporte.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnaKilos = gridViewConceptosImputados.Columns["Kilos"];
            columnaKilos.Width = 70;
            columnaKilos.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            columnaKilos.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            #endregion

            #region Grid Pendientes

            var sourcependientes = conceptosvm.Select(x =>
                new RowConceptosPendientes()
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    Letra = x.TipoDeFactura,
                    PuntoDeVenta = x.PuntoDeVenta,
                    NumeroDeLiquidacion = x.NumeroDeLiquidacion,
                    Saldo = x.RestaPorPagar,
                    Afectar = Math.Round(x.RestaPorPagar, 2),
                })
                .ToList();

            gridControlPendientes.DataSource = new BindingList<RowConceptosPendientes>(sourcependientes);

            gridViewPendientes.Columns["Id"].Visible = false;

            gridViewPendientes.Columns["Fecha"].OptionsColumn.AllowEdit = false;
            gridViewPendientes.Columns["Letra"].OptionsColumn.AllowEdit = false;
            gridViewPendientes.Columns["PuntoDeVenta"].OptionsColumn.AllowEdit = false;
            gridViewPendientes.Columns["NumeroDeLiquidacion"].OptionsColumn.AllowEdit = false;
            gridViewPendientes.Columns["Saldo"].OptionsColumn.AllowEdit = false;
            gridViewPendientes.Columns["Afectar"].OptionsColumn.AllowEdit = true;

            var repository = new RepositoryItemTextEdit();
            repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            repository.Mask.EditMask = @"\d+.\d+";
            repository.Mask.UseMaskAsDisplayFormat = true;
            gridControlPendientes.RepositoryItems.Add(repository);
            gridViewPendientes.Columns["Afectar"].ColumnEdit = repository;

            #endregion
        }

        private void ActualizarControlesOrdenDePago(OrdenDePagoDetalleViewModel ordenvm, ProductorViewModel productorvm)
        {
            var fecha = ordenvm.Fecha;
            var numerodeorden = ordenvm.NumeroDeOrden;
            var detalle = ordenvm.Observaciones;
            var productor = ordenvm.Productor;
            var cuit = ordenvm.CUIT;
            var fet = ordenvm.FET;
            var situacioniva = productorvm.SituacionIVATASADescripcion;
            var provincia = productorvm.Provincia;

            var importeporpagar = ordenvm.ImportePorPagar.ToString();

            var retencioniibb = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionIIBB).Single().Importe.ToString();
            var retencionEEAOC = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionEEAOC).Single().Importe.ToString();
            var retencionSaludPublica = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionSaludPublica).Single().Importe.ToString();
            var retencionGADM = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionGADM).Single().Importe.ToString();
            var retencionGCIAS = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionGCIAS).Single().Importe.ToString();
            var retencionRiego = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionRiego).Single().Importe.ToString();

            var netoporpagar = ordenvm.NetoPorPagar.ToString();

            dpFechaOrden.Value = fecha;
            txtNumOrdenPago.Text = numerodeorden.ToString();
            txtPuntoVenta.Text = DevConstantes.PuntoVenta;
            txtDetalle.Text = detalle;

            txtProductor.Text = productor;
            txtCuit.Text = cuit;
            txtFet.Text = fet;
            txtSituacionIva.Text = situacioniva;
            txtProvincia.Text = provincia;

            txtImporteBruto.Text = importeporpagar;

            //txtCesion.Text = "0.00";
            //txtComision.Text = "0.00";

            txtIIBB.Text = retencioniibb;
            txtEEAOC.Text = retencionEEAOC;
            txtSaludPublica.Text = retencionSaludPublica;
            txtGADM.Text = retencionGADM;
            txtGanancias.Text = retencionGCIAS;
            txtRiego.Text = retencionRiego;

            //txtOtrosConceptos.Text = "0.00";
            //txtCuotaSocial.Text = "0.00";
            //txtAnticipos.Text = "0.00";

            txtNeto.Text = netoporpagar;
        }















        //#region Calcular Totales
        //
        //private void CalcularValores()
        //{
        //    txtImporteBruto.Text = "0";
        //    txtGanancias.Text = "0";
        //    txtGADM.Text = "0";
        //    txtIIBB.Text = "0";
        //    txtSaludPublica.Text = "0";
        //    txtEEAOC.Text = "0";
        //    txtRiego.Text = "0";
        //    txtMonotributo.Text = "0";
        //    txtNeto.Text = "0";
        //   
        //    //CalcularTotalProductores();
        //    CalcularTotalAfectar();
        //    CalcularTotalGanancias();
        //    CalcularTotalIVA();
        //    CalcularTotalIIBB();
        //    CalcularTotalSaludPublica();
        //    CalcularTotalEEAOC();
        //    CalcularTotalRIEGO();
        //    CalcularTotalMonotributo();
        //    CalcularTotalNeto();
        //}
        //
        ////private void CalcularTotalProductores()
        ////{
        ////    txtProductores.Text = "0";
        ////    if (gridViewLiquidacion.SelectedRowsCount > 0)
        ////    {
        ////        for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
        ////        {
        ////            if (gridViewLiquidacion.IsRowSelected(i))
        ////            {
        ////                var ProductorId = new Guid(gridViewLiquidacion.GetRowCellValue(i, "PRODUCTORID").ToString());
        //
        ////                var productores = Context.Vw_Romaneo
        ////                    .Where(x => x.ProductorId == ProductorId)
        ////                    .Distinct()
        ////                    .Count();
        //
        ////                txtProductores.Text = (Int32.Parse(txtProductores.Text) + Int32.Parse(productores.ToString())).ToString();
        ////            }
        ////        }
        ////    }
        ////    else
        ////    {
        ////        txtProductores.Text = "0";
        ////    }
        ////}
        //
        //private float CalcularTotalPesos(Guid PesadaId)
        //{
        //    float totalPesos = 0;
        //    var liquidaciones = Context.Vw_Romaneo
        //        .Where(x => x.PesadaId == PesadaId)
        //        .ToList();
        //    if (liquidaciones != null)
        //    {
        //        foreach (var liquidacion in liquidaciones)
        //        {
        //            totalPesos = totalPesos + Convert.ToSingle(liquidacion.ImporteBruto.Value);
        //        }
        //    }
        //    else
        //    {
        //        totalPesos = 0;
        //    }
        //
        //    return totalPesos;
        //}
        //
        //private void CalcularTotalAfectar()
        //{
        //    txtImporteBruto.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal afectar = decimal.Parse(dgvPendientes.Rows[i].Cells[5].Value.ToString());
        //        txtImporteBruto.Text = decimal.Round((decimal.Parse(txtImporteBruto.Text) + afectar), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalGanancias()
        //{
        //    txtGanancias.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal gcias = decimal.Parse(dgvPendientes.Rows[i].Cells[6].Value.ToString());
        //        txtGanancias.Text = decimal.Round((decimal.Parse(txtGanancias.Text) + gcias), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalIVA()
        //{
        //    txtGADM.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal iva = decimal.Parse(dgvPendientes.Rows[i].Cells[7].Value.ToString());
        //        txtGADM.Text = decimal.Round((decimal.Parse(txtGADM.Text) + iva), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalIIBB()
        //{
        //    txtIIBB.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal iibb = decimal.Parse(dgvPendientes.Rows[i].Cells[8].Value.ToString());
        //        txtIIBB.Text = decimal.Round((decimal.Parse(txtIIBB.Text) + iibb), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalSaludPublica()
        //{
        //    txtSaludPublica.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal salud = decimal.Parse(dgvPendientes.Rows[i].Cells[9].Value.ToString());
        //        txtSaludPublica.Text = decimal.Round((decimal.Parse(txtSaludPublica.Text) + salud), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalEEAOC()
        //{
        //    txtEEAOC.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal eeaoc = decimal.Parse(dgvPendientes.Rows[i].Cells[10].Value.ToString());
        //        txtEEAOC.Text = decimal.Round((decimal.Parse(txtEEAOC.Text) + eeaoc), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalRIEGO()
        //{
        //    txtRiego.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal riego = decimal.Parse(dgvPendientes.Rows[i].Cells[11].Value.ToString());
        //        txtRiego.Text = decimal.Round((decimal.Parse(txtRiego.Text) + riego), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalMonotributo()
        //{
        //    txtMonotributo.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal monotributo = decimal.Parse(dgvPendientes.Rows[i].Cells[12].Value.ToString());
        //        txtMonotributo.Text = decimal.Round((decimal.Parse(txtMonotributo.Text) + monotributo), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //private void CalcularTotalNeto()
        //{
        //    txtNeto.Text = "0";
        //    for (int i = 0; i < dgvPendientes.RowCount; i++)
        //    {
        //        decimal neto = decimal.Parse(dgvPendientes.Rows[i].Cells[13].Value.ToString());
        //        txtNeto.Text = decimal.Round((decimal.Parse(txtNeto.Text) + neto), 2, MidpointRounding.AwayFromZero).ToString();
        //    }
        //}
        //
        //#endregion
    }

    class RowConceptosImputados
    {
        public DateTime Fecha { get; set; }
        public string Letra { get; set; }
        public int PuntoDeVenta { get; set; }
        public long NumeroDeLiquidacion { get; set; }
        public Decimal Importe { get; set; }
        public Decimal Kilos { get; set; }
    }

    class RowConceptosPendientes
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Letra { get; set; }
        public int PuntoDeVenta { get; set; }
        public long NumeroDeLiquidacion { get; set; }
        public Decimal Saldo { get; set; }
        public Decimal Afectar { get; set; }
    }
}