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
            
            this.dgvPendientes.CellBeginEdit += this.dgvPendientes_CellBeginEdit;
            this.dgvPendientes.CellEndEdit += this.dgvPendientes_CellEndEdit;
            
            this.btnPago.Click += this.btnPago_Click;
            
            this.btnCancelar.Click += this.btnCancelar_Click;
            
            ConfigurarGrillaDePagosPendientes();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Enter)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (dgvPendientes.CurrentCell == null)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            var columnafectar = dgvPendientes.Columns[_columns[5]];
            int columnindex = dgvPendientes.CurrentCell.ColumnIndex;
            int rowindex = dgvPendientes.CurrentCell.RowIndex;

            if (columnindex != columnafectar.Index)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (rowindex + 1 >= dgvPendientes.RowCount)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            var cell = dgvPendientes.Rows[rowindex + 1].Cells[columnafectar.Index];

            dgvPendientes.CurrentCell = cell;

            dgvPendientes.BeginEdit(true);
            _maskedtextbox.Focus();
            _maskedtextbox.SelectionStart = 0;

            return true;
        }

        void dgvPendientes_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var columnafectar = dgvPendientes.Columns[_columns[5]];

            if (e.ColumnIndex == columnafectar.Index)
            {
                var text = dgvPendientes[e.ColumnIndex, e.RowIndex].Value as String ?? String.Empty;
                var rect = dgvPendientes.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                _maskedtextbox.Text = text;
                _maskedtextbox.Location = rect.Location;
                _maskedtextbox.Size = rect.Size;
                _maskedtextbox.Focus();
                _maskedtextbox.SelectionStart = 0;
                _maskedtextbox.Visible = true;
            }
        }

        void dgvPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!_maskedtextbox.Visible)
            {
                return;
            }

            var columnafectar = dgvPendientes.Columns[_columns[5]];

            if (e.ColumnIndex == columnafectar.Index)
            {
                var text = _maskedtextbox.Text;
                var cell = dgvPendientes.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (text != String.Empty)
                {
                    var afectar = Math.Round(Decimal.Parse(text), 2);
                    var afectarstr = (afectar).ToString("n2");
                    
                    cell.Value = afectarstr;
                    //
                    //var precioround = Decimal.Round(afectar, 2, MidpointRounding.AwayFromZero);
                    //var gciasTotal = Decimal.Round(afectar * gcias, 2, MidpointRounding.AwayFromZero);
                    //var ivaTotal = Decimal.Round(precioround * iva, 2, MidpointRounding.AwayFromZero);
                    //var iibbTotal = Decimal.Round(precioround * iibb, 2, MidpointRounding.AwayFromZero);
                    //var coeficienteTotal = Decimal.Round(precioround * coeficientegral, 2, MidpointRounding.AwayFromZero);
                    //
                    //dgvPendientes.Rows[e.RowIndex].Cells[6].Value = gciasTotal;
                    //dgvPendientes.Rows[e.RowIndex].Cells[7].Value = ivaTotal;
                    //dgvPendientes.Rows[e.RowIndex].Cells[8].Value = iibbTotal;
                    //dgvPendientes.Rows[e.RowIndex].Cells[9].Value = coeficienteTotal;
                    //dgvPendientes.Rows[e.RowIndex].Cells[10].Value = coeficienteTotal;
                    //dgvPendientes.Rows[e.RowIndex].Cells[11].Value = coeficienteTotal;
                    //dgvPendientes.Rows[e.RowIndex].Cells[12].Value = coeficienteTotal;
                    //
                    //var netoTotal = Decimal.Round(precioround + gciasTotal + ivaTotal + iibbTotal + (coeficienteTotal * 4), 2, MidpointRounding.AwayFromZero);
                    //
                    //dgvPendientes.Rows[e.RowIndex].Cells[13].Value = netoTotal;
                    //
                    //CalcularValores();
                }
                else
                {
                    cell.Value = text;
                }

                _maskedtextbox.Visible = false;
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

        private void ConfigurarGrillaDePagosPendientes()
        {
            DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d3 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d4 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d5 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d6 = new DataGridViewTextBoxColumn();

            //DataGridViewColumn d7 = new DataGridViewTextBoxColumn();
            //DataGridViewColumn d8 = new DataGridViewTextBoxColumn();
            //DataGridViewColumn d9 = new DataGridViewTextBoxColumn();
            //DataGridViewColumn d10 = new DataGridViewTextBoxColumn();
            //DataGridViewColumn d11 = new DataGridViewTextBoxColumn();
            //DataGridViewColumn d12 = new DataGridViewTextBoxColumn();
            //DataGridViewColumn d13 = new DataGridViewTextBoxColumn();

            //Add Header Texts to be displayed on the Columns
            d1.HeaderText = _columns[0];
            d2.HeaderText = _columns[1];
            d3.HeaderText = _columns[2];
            d4.HeaderText = _columns[3];
            d5.HeaderText = _columns[4];
            d6.HeaderText = _columns[5];

            //d7.HeaderText =  _columns[6];//RetencionTypes.RetencionGCIAS;
            //d8.HeaderText =  _columns[7];
            //d9.HeaderText =  _columns[8];
            //d10.HeaderText = _columns[9];
            //d11.HeaderText = _columns[10];
            //d12.HeaderText = _columns[11];
            //d13.HeaderText = _columns[12];

            d1.Visible = false;
            d2.Width = 70;
            d3.Width = 50;
            d4.Width = 138;
            d5.Width = 94;
            d6.Width = 94;

            //d7.Visible = false;
            //d8.Visible = false;
            //d9.Visible = false;
            //d10.Visible = false;
            //d11.Visible = false;
            //d12.Visible = false;
            //d13.Visible = false;

            //Add the Columns to the DataGridView
            dgvPendientes.Columns.AddRange(d1, d2, d3, d4, d5, d6);
                //d7, d8, d9, d10, d11, d12, d13);

            _maskedtextbox = new MaskedTextBox();
            dgvPendientes.Controls.Add(_maskedtextbox);

            _maskedtextbox.Visible = false;
        }
        
        private void CargarDatos()
        {
            var ordenesdepagovm = _pagosManager.GetOrdenDePago(_ordendepagoid);
            var ordenvm = ordenesdepagovm.Items[0];
            var productorvm = _productoresManager.GetProductor(ordenvm.ProductorId);

            ActualizarControlesOrdenDePago(ordenvm, productorvm);

            var conceptosvm = _pagosManager.ListarConceptosDeOrdenDePago(ordenvm.Id);

            #region Grid Conceptos Imputados

            var source = conceptosvm.Select(x =>
                new RowConceptosImputados()
                {
                    Fecha = x.Fecha,
                    Letra = x.TipoDeFactura,
                    Importe = x.NetoPorPagar,
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
                    Afectar = x.RestaPorPagar,
                })
                .ToList();

            dgvPendientes.Rows.Clear();

            foreach (var item in sourcependientes)
            {
                this.dgvPendientes.Rows.Add(
                    item.Id,
                    item.Fecha.ToShortDateString(),
                    item.Letra,
                        //item.PuntoDeVenta,
                    item.NumeroDeLiquidacion,
                    item.Saldo,
                    item.Afectar);
            }

            this.dgvPendientes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dgvPendientes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dgvPendientes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dgvPendientes.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvPendientes.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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