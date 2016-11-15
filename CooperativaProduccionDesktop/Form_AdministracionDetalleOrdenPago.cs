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

namespace CooperativaProduccion
{
    public partial class Form_AdministracionDetalleOrdenPago : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public MaskedTextBox mask;
        private int currentRow;
        private bool resetRow = false;
        private Guid OrdenPagoId;

        #region Coeficiente Retencion

        private decimal coeficiente = decimal.Parse("1.21");
        private decimal iva = decimal.Parse("0.21");
        private decimal gcias = decimal.Parse("0.02");
        private decimal iibb = decimal.Parse("0.175");
        private decimal coeficientegral = decimal.Parse("0.05");

        #endregion

        public Form_AdministracionDetalleOrdenPago(Guid OrdenPagoId)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            AddNewRow();
            mask = new MaskedTextBox();
            mask.Visible = false;
            dgvPendientes.Controls.Add(mask);
            mask.Focus();
            mask.SelectionStart = 0;
            dgvPendientes.CellEndEdit +=
                new DataGridViewCellEventHandler(dgvPendientes_CellEndEdit);
            dgvPendientes.DataBindingComplete +=
                new DataGridViewBindingCompleteEventHandler(dgvPendientes_DataBindingComplete);
            CargarDatos(OrdenPagoId);

        }

        private void CargarDatos(Guid Id)
        {
            var ordenPago = Context.Vw_OrdenPago
                .Where(x => x.OrdenPagoId == Id)
                .FirstOrDefault();
            if (ordenPago != null)
            {
                #region Datos Orden Pago

                OrdenPagoId = ordenPago.OrdenPagoId;
                dpFechaOrden.Value = ordenPago.Fecha.Value;
                txtNumOrdenPago.Text = ordenPago.NumOrdenPago.Value.ToString();
                txtPuntoVenta.Text = DevConstantes.PuntoVenta;
                txtDetalle.Text = ordenPago.detalle;

                #endregion

                #region Datos Productor

            
                txtProductor.Text = ordenPago.NOMBRE;
                txtFet.Text = ordenPago.nrofet;
                txtCuit.Text = ordenPago.CUIT;

                var productor = Context.Vw_Productor
                    .Where(x => x.ID == ordenPago.ProductorId)
                    .FirstOrDefault();

                if (productor != null)
                {
                    txtSituacionIva.Text = productor.IVA.Equals("MT") ? 
                        "Monotributo" : "Responsable Inscripto";
                    txtProvincia.Text = productor.Provincia;
                }

                #endregion

                #region Totalizadores - Retenciones

                txtImporteBruto.Text = ordenPago.Subtotal.Value.ToString();
                txtCesion.Text = "0.00";
                txtComision.Text = "0.00";
                txtGanancias.Text = ordenPago.Ganancias.Value.ToString();
                txtIVA.Text = ordenPago.IVA.Value.ToString();
                txtIIBB.Text = ordenPago.IIBB.Value.ToString();
                txtSaludPublica.Text = ordenPago.SaludPublica.Value.ToString();
                txtEEAOC.Text = ordenPago.EEAOC.Value.ToString();
                txtRiego.Text = ordenPago.Riego.Value.ToString();
                txtMonotributo.Text = ordenPago.Monotributo.Value.ToString();
                txtOtrosConceptos.Text = "0.00";
                txtCuotaSocial.Text = "0.00";
                txtAnticipos.Text = "0.00";
                txtNeto.Text = ordenPago.Neto.Value.ToString();

                #endregion

                #region Grid Conceptos Imputados

                var result = (
                    from a in Context.Vw_Romaneo
                        .Where(x => x.ProductorId == ordenPago.ProductorId)
                        .Where(x => x.numAfipLiquidacion != null)
                    select new
                    {
                        ID = a.PesadaId,
                        FECHA = a.fechaAfipLiquidacion,
                        LETRA = a.Letra,
                        NUMAFIP = a.numAfipLiquidacion,
                        NETO = a.ImporteBruto,
                        KILOS = a.Totalkg
                    })
                    .OrderBy(x => x.FECHA)
                    .ToList();

                gridControlConceptosImputados.DataSource = result;
                gridViewConceptosImputados.Columns[0].Visible = false;
                gridViewConceptosImputados.Columns[1].Caption = "Fecha";
                gridViewConceptosImputados.Columns[1].Width = 60;
                gridViewConceptosImputados.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[2].Caption = "Letra";
                gridViewConceptosImputados.Columns[2].Width = 50;
                gridViewConceptosImputados.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[3].Caption = "Número Comprobante";
                gridViewConceptosImputados.Columns[3].Width = 70;
                gridViewConceptosImputados.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[4].Caption = "Importe";
                gridViewConceptosImputados.Columns[4].Width = 70;
                gridViewConceptosImputados.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[5].Caption = "Kilos";
                gridViewConceptosImputados.Columns[5].Width = 70;
                gridViewConceptosImputados.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewConceptosImputados.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                #endregion

                #region Grid Pendientes

                var resultPendiente = (
                    from a in Context.Vw_Romaneo
                        .Where(x => x.ProductorId == ordenPago.ProductorId)
                        .Where(x => x.numAfipLiquidacion != null)
                    select new
                    {
                        ID = a.PesadaId,
                        FECHA = a.fechaAfipLiquidacion,
                        LETRA = a.Letra,
                        NUMAFIP = a.numAfipLiquidacion,
                        Saldo = a.ImporteBruto,
                        Afectar = a.ImporteBruto

                    })
                    .OrderBy(x => x.FECHA)
                    .ToList();

                if (dgvPendientes.RowCount > 0)
                {
                    dgvPendientes.Rows.Clear();
                }

                if (resultPendiente.Count > 0)
                {
                    foreach (var resultp in resultPendiente)
                    {
                        this.dgvPendientes.Rows.Add(resultp.ID, resultp.FECHA.Value.ToShortDateString(), resultp.LETRA,
                            resultp.NUMAFIP, resultp.Saldo, resultp.Saldo,string.Empty,string.Empty,string.Empty,
                            string.Empty,string.Empty,string.Empty,string.Empty,string.Empty);
                    }
                    this.dgvPendientes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    this.dgvPendientes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    this.dgvPendientes.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvPendientes.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

                #endregion
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewRowPendiente()
        {
            DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d3 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d4 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d5 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d6 = new DataGridViewTextBoxColumn();

            DataGridViewColumn d7 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d8 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d9 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d10 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d11 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d12 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d13 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d14 = new DataGridViewTextBoxColumn();

            //Add Header Texts to be displayed on the Columns
            d1.HeaderText = "Id";
            d2.HeaderText = "Fecha";
            d3.HeaderText = "Letra";
            d4.HeaderText = "N° Comprobante";
            d5.HeaderText = "Saldo";
            d6.HeaderText = "Afectar";

            d7.HeaderText = "Gcias";
            d8.HeaderText = "IVA";
            d9.HeaderText = "IIBB";
            d10.HeaderText = "Salud Publica";
            d11.HeaderText = "EEAOC";
            d12.HeaderText = "Riego";
            d13.HeaderText = "Monotributo";
            d14.HeaderText = "Neto";
            
            d1.Visible = false;
            d2.Width = 70;
            d3.Width = 50;
            d4.Width = 138;
            d5.Width = 94;
            d6.Width = 94;

            d7.Visible = false;
            d8.Visible = false;
            d9.Visible = false;
            d10.Visible = false;
            d11.Visible = false;
            d12.Visible = false;
            d13.Visible = false;
            d14.Visible = false;
            
            //Add the Columns to the DataGridView
            dgvPendientes.Columns.AddRange(d1, d2, d3, d4, d5, d6, d7,
                d8, d9, d10, d11, d12, d13, d14);
        }

        private void AddNewRow()
        {
            AddNewRowPendiente();
        }

        private void dgvPendientes_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvPendientes.CurrentCell.ColumnIndex == 5)
            {
                mask.Focus();
                mask.SelectionStart = 0;
                Rectangle rect = dgvPendientes.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                mask.Location = rect.Location;
                mask.Size = rect.Size;
                mask.Text = "";

                if (dgvPendientes[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    mask.Text = dgvPendientes[e.ColumnIndex, e.RowIndex].Value.ToString();
                    mask.Focus();
                    mask.SelectionStart = 0;
                }
                mask.Visible = true;
            }
        }

        private void dgvPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (mask.Visible)
            {
                if (dgvPendientes.CurrentCell.ColumnIndex == 5)
                {
                    dgvPendientes.CurrentCell.Value = mask.Text;
                    if (mask.Text != string.Empty)
                    {
                        var precio = (Math.Round(decimal.Parse(mask.Text), 2)).ToString("n2");
                        
                        dgvPendientes.CurrentCell.Value = precio;

                        decimal gciasTotal = decimal.Round((decimal.Round(decimal.Parse(precio.ToString()), 2, MidpointRounding.AwayFromZero) * gcias), 2, MidpointRounding.AwayFromZero);
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[6].Value =gciasTotal;

                        decimal ivaTotal = decimal.Round((decimal.Round(decimal.Parse(precio.ToString()), 2, MidpointRounding.AwayFromZero) * iva), 2, MidpointRounding.AwayFromZero);
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[7].Value = ivaTotal;

                        decimal iibbTotal = decimal.Round((decimal.Round(decimal.Parse(precio.ToString()), 2, MidpointRounding.AwayFromZero) * iibb), 2, MidpointRounding.AwayFromZero);
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[8].Value = iibbTotal;

                        decimal coeficienteTotal = decimal.Round((decimal.Round(decimal.Parse(precio.ToString()), 2, MidpointRounding.AwayFromZero) * coeficientegral), 2, MidpointRounding.AwayFromZero);

                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[9].Value = coeficienteTotal;    
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[10].Value = coeficienteTotal;
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[11].Value = coeficienteTotal;
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[12].Value = coeficienteTotal;

                        decimal netoTotal = 
                            decimal.Round((decimal.Round(decimal.Parse(precio.ToString()), 2, MidpointRounding.AwayFromZero) + gciasTotal + ivaTotal + iibbTotal + (coeficienteTotal*4)), 2, MidpointRounding.AwayFromZero);
                        dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex].Cells[13].Value = netoTotal;
                        CalcularValores();
     
                        
                    }
                    mask.Visible = false;
                }
            }
        }

        private void dgvPendientes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                dgvPendientes.CurrentCell = dgvPendientes.Rows[currentRow].Cells[0];
            }
        }

        private void dgvPendientes_SelectionChanged(object sender, EventArgs e)
        {
            dgvPendientes.SelectionChanged += new EventHandler(dgvPendientes_SelectionChanged);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (dgvPendientes.CurrentCell != null)
            {
                int icolumn = dgvPendientes.CurrentCell.ColumnIndex;
                int irow = dgvPendientes.CurrentCell.RowIndex;
                DataGridViewCell cell;
                if (keyData == Keys.Enter)
                {
                    if (dgvPendientes.CurrentCell.ColumnIndex == 5)
                    {
                        if (dgvPendientes.CurrentCell.RowIndex + 1 < dgvPendientes.RowCount)
                        {
                            cell = dgvPendientes.Rows[dgvPendientes.CurrentCell.RowIndex + 1].Cells[5];
                            dgvPendientes.CurrentCell = cell;
                            dgvPendientes.BeginEdit(true);
                            mask.Focus();
                            mask.SelectionStart = 0;
                            
                            return true;
                        }
                    }
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);

                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Calcular Totales

        private void CalcularValores()
        {
            txtImporteBruto.Text = "0";
            txtGanancias.Text = "0";
            txtIVA.Text = "0";
            txtIIBB.Text = "0";
            txtSaludPublica.Text = "0";
            txtEEAOC.Text = "0";
            txtRiego.Text = "0";
            txtMonotributo.Text = "0";
            txtNeto.Text = "0";
           
            //CalcularTotalProductores();
            CalcularTotalAfectar();
            CalcularTotalGanancias();
            CalcularTotalIVA();
            CalcularTotalIIBB();
            CalcularTotalSaludPublica();
            CalcularTotalEEAOC();
            CalcularTotalRIEGO();
            CalcularTotalMonotributo();
            CalcularTotalNeto();
        }

        //private void CalcularTotalProductores()
        //{
        //    txtProductores.Text = "0";
        //    if (gridViewLiquidacion.SelectedRowsCount > 0)
        //    {
        //        for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
        //        {
        //            if (gridViewLiquidacion.IsRowSelected(i))
        //            {
        //                var ProductorId = new Guid(gridViewLiquidacion.GetRowCellValue(i, "PRODUCTORID").ToString());

        //                var productores = Context.Vw_Romaneo
        //                    .Where(x => x.ProductorId == ProductorId)
        //                    .Distinct()
        //                    .Count();

        //                txtProductores.Text = (Int32.Parse(txtProductores.Text) + Int32.Parse(productores.ToString())).ToString();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        txtProductores.Text = "0";
        //    }
        //}

        private float CalcularTotalPesos(Guid PesadaId)
        {
            float totalPesos = 0;
            var liquidaciones = Context.Vw_Romaneo
                .Where(x => x.PesadaId == PesadaId)
                .ToList();
            if (liquidaciones != null)
            {
                foreach (var liquidacion in liquidaciones)
                {
                    totalPesos = totalPesos + Convert.ToSingle(liquidacion.ImporteBruto.Value);
                }
            }
            else
            {
                totalPesos = 0;
            }

            return totalPesos;
        }

        private void CalcularTotalAfectar()
        {
            txtImporteBruto.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal afectar = decimal.Parse(dgvPendientes.Rows[i].Cells[5].Value.ToString());
                txtImporteBruto.Text = decimal.Round((decimal.Parse(txtImporteBruto.Text) + afectar), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalGanancias()
        {
            txtGanancias.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal gcias = decimal.Parse(dgvPendientes.Rows[i].Cells[6].Value.ToString());
                txtGanancias.Text = decimal.Round((decimal.Parse(txtGanancias.Text) + gcias), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalIVA()
        {
            txtIVA.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal iva = decimal.Parse(dgvPendientes.Rows[i].Cells[7].Value.ToString());
                txtIVA.Text = decimal.Round((decimal.Parse(txtIVA.Text) + iva), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalIIBB()
        {
            txtIIBB.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal iibb = decimal.Parse(dgvPendientes.Rows[i].Cells[8].Value.ToString());
                txtIIBB.Text = decimal.Round((decimal.Parse(txtIIBB.Text) + iibb), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalSaludPublica()
        {
            txtSaludPublica.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal salud = decimal.Parse(dgvPendientes.Rows[i].Cells[9].Value.ToString());
                txtSaludPublica.Text = decimal.Round((decimal.Parse(txtSaludPublica.Text) + salud), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalEEAOC()
        {
            txtEEAOC.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal eeaoc = decimal.Parse(dgvPendientes.Rows[i].Cells[10].Value.ToString());
                txtEEAOC.Text = decimal.Round((decimal.Parse(txtEEAOC.Text) + eeaoc), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalRIEGO()
        {
            txtRiego.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal riego = decimal.Parse(dgvPendientes.Rows[i].Cells[11].Value.ToString());
                txtRiego.Text = decimal.Round((decimal.Parse(txtRiego.Text) + riego), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalMonotributo()
        {
            txtMonotributo.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal monotributo = decimal.Parse(dgvPendientes.Rows[i].Cells[12].Value.ToString());
                txtMonotributo.Text = decimal.Round((decimal.Parse(txtMonotributo.Text) + monotributo), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        private void CalcularTotalNeto()
        {
            txtNeto.Text = "0";
            for (int i = 0; i < dgvPendientes.RowCount; i++)
            {
                decimal neto = decimal.Parse(dgvPendientes.Rows[i].Cells[13].Value.ToString());
                txtNeto.Text = decimal.Round((decimal.Parse(txtNeto.Text) + neto), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        #endregion  

        private void btnPago_Click(object sender, EventArgs e)
        {
            var formaPago = new Form_AdministracionFormaPago(OrdenPagoId,txtNeto.Text);
            formaPago.Show();  
        }
    }
}