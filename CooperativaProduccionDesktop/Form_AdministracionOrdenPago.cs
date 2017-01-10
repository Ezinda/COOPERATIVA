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
using System.Linq.Expressions;
using Extensions;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using System.Data.Entity;
namespace CooperativaProduccion
{
    public partial class Form_AdministracionOrdenPago : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;

        public Form_AdministracionOrdenPago()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar();
            Buscar(false);
        }

        #region Generación de Ordenes de Pago

        #region Method Code

        private void btnBuscarLiquidaciones_Click(object sender, EventArgs e)
        {
            Buscar(true);
        }

        private void txtPorcentajePago_KeyPress(object sender, KeyPressEventArgs e)
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
                Buscar(true);
            }
        }

        private void txtPagoPorKilo_KeyPress(object sender, KeyPressEventArgs e)
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
                Buscar(true);
            }
        }

        private void btnPorcentajePago_Click(object sender, EventArgs e)
        {
            Buscar(true);
        }

        private void btnPagoPorKilo_Click(object sender, EventArgs e)
        {
            Buscar(true);
        }

        private void gridControlLiquidacion_Click(object sender, EventArgs e)
        {
            CalcularValores();
        }

        private void btnGenerarOP_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea generar ordenes de pago?",
              "Confirmación de Datos", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            GenerarOP();
            Buscar(true);
        }

        #endregion

        #region Method Dev

        private void Iniciar()
        {
            dpDesdeLiquidacion.Value = DateTime.Now.Date.AddYears(-16);
            dpFechaPago.Value = DateTime.Now.Date;
            txtPorcentajePago.Text = "100.00";
            txtCuotaSocial.Text = "0.00";
            txtOtrosConceptos.Text = "0.00";
            txtAnticipos.Text = "0.00";
            txtTotalAfectado.Text = "0.00";
            txtComision.Text = "0.00";
            txtGanancias.Text = "0.00";
            txtIVA.Text = "0.00";
            txtIIBB.Text = "0.00";
            txtSaludPublica.Text = "0.00";
            txtEEAOC.Text = "0.00";
            txtRiego.Text = "0.00";
            txtMonotributo.Text = "0.00";
            txtNeto.Text = "0.00";
            txtNumeroOP.Text = ContadorNumeroOP().ToString();
            
        }

        private void Buscar(bool buscar)
        {
            decimal coeficiente = decimal.Parse("1.21");
            decimal iva = decimal.Parse("0.21");
            decimal gcias = decimal.Parse("0.02");
            decimal iibb = decimal.Parse("0.0175");
            decimal coeficientegral = decimal.Parse("0.005");

            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = buscar.Equals(true) ? pred.And(x => x.fechaAfipLiquidacion.Value >= dpDesdeLiquidacion.Value.Date
                && x.fechaAfipLiquidacion.Value <= dpHastaLiquidacion.Value.Date) : pred;
  
            var result = (
               from a in Context.Vw_Romaneo
                   .Where(pred)
                   .Where(x => x.OrdenPagoId == null 
                       && x.RomaneoPendiente == false)
                   .AsEnumerable()
               select new
               {
                   ID = a.PesadaId,
                   PRODUCTORID = a.ProductorId,
                   NUMINTLIQ = a.numInternoLiquidacion,
                   NUMAFIPLIQ = a.numAfipLiquidacion,
                   FECHA = a.fechaAfipLiquidacion,                   
                   PRODUCTOR = a.NOMBRE,
                   FET = a.nrofet,
                   NUMEROCOMPROBANTE = a.numAfipLiquidacion,
                   KILOS = a.Totalkg,
                   BRUTOSINIVA = a.ImporteBruto,
                   AFECTAR = txtPorcentajePago.Text != "0.00" ?
                    decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) :
                    decimal.Parse("0.00"),
                   GCIAS = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * gcias).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00"),
                   IVA = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * iva).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00"),
                   IIBB = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * iibb).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00"),
                   SaludPublica = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * coeficientegral).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00"),
                   EEAOC = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * coeficientegral).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00"),
                   Riego = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * coeficientegral).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00"),
                   Monotributo = txtPorcentajePago.Text != "0.00" ?
                   decimal.Round(decimal.Parse(((decimal.Round(decimal.Parse(((a.ImporteBruto * decimal.Parse(txtPorcentajePago.Text)) / 100).ToString()), 2, MidpointRounding.AwayFromZero) / coeficiente) * coeficientegral).ToString()), 2, MidpointRounding.AwayFromZero) :
                   decimal.Parse("0.00")
               })
               .OrderByDescending(x => x.FECHA)
               .ThenBy(x => x.FET)
               .ToList();

            gridControlLiquidacion.DataSource = result;
            gridViewLiquidacion.Columns[0].Visible = false;
            gridViewLiquidacion.Columns[1].Visible = false;
            gridViewLiquidacion.Columns[2].Visible = false;
            gridViewLiquidacion.Columns[3].Visible = false;
            //gridViewLiquidacion.Columns[4].Caption = "Fecha Liquidación";
            //gridViewLiquidacion.Columns[4].Width = 60;
            //gridViewLiquidacion.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[5].Caption = "Productor";
            //gridViewLiquidacion.Columns[5].Width = 150;
            //gridViewLiquidacion.Columns[6].Caption = "FET";
            //gridViewLiquidacion.Columns[6].Width = 55;
            //gridViewLiquidacion.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[7].Caption = "Comprobante";
            //gridViewLiquidacion.Columns[7].Width = 60;
            //gridViewLiquidacion.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[8].Caption = " Salgo Kilos";
            //gridViewLiquidacion.Columns[8].Width = 40;
            //gridViewLiquidacion.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[9].Caption = "Saldo $";
            //gridViewLiquidacion.Columns[9].Width = 60;
            //gridViewLiquidacion.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[10].Caption = "Afectar";
            //gridViewLiquidacion.Columns[10].Width = 60;
            //gridViewLiquidacion.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            ////gridViewLiquidacion.Columns[9].Caption = "Otros";
            ////gridViewLiquidacion.Columns[9].Width = 60;
            ////gridViewLiquidacion.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ////gridViewLiquidacion.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewLiquidacion.Columns[11].Visible = false;
            //gridViewLiquidacion.Columns[12].Visible = false; 
            //gridViewLiquidacion.Columns[13].Visible = false;
            //gridViewLiquidacion.Columns[14].Visible = false; 
            //gridViewLiquidacion.Columns[15].Visible = false;
            //gridViewLiquidacion.Columns[16].Visible = false;
            //gridViewLiquidacion.Columns[17].Visible = false;

            for (var i = 0; i <= gridViewLiquidacion.RowCount; i++)
            {
                gridViewLiquidacion.SelectRow(i);
            }
            CalcularValores();
        }

        #region Calcular Totales

        private void CalcularValores()
        {
            txtTotalKg.Text = "0";
            txtTotalPesos.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewLiquidacion.GetRowCellValue(i, "ID").ToString());
                        txtTotalKg.Text = (float.Parse(txtTotalKg.Text) + CalcularTotalKilos(Id)).ToString();
                        txtTotalPesos.Text = (float.Parse(txtTotalPesos.Text) + CalcularTotalPesos(Id)).ToString();
                    }
                }
            }
            else
            {
                txtTotalKg.Text = "0";
                txtTotalPesos.Text = "0.00";
            }

            CalcularTotalProductores();
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

        private float CalcularTotalKilos(Guid PesadaId)
        {
            float totalKilos = 0;
            var pesadas = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId).ToList();
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    totalKilos = totalKilos + Convert.ToSingle(pesada.Kilos.Value);
                }
            }
            else
            {
                totalKilos = 0;
            }

            return totalKilos;
        }

        private void CalcularTotalProductores()
        {
            txtProductores.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        var ProductorId = new Guid(gridViewLiquidacion.GetRowCellValue(i, "PRODUCTORID").ToString());

                        var productores = Context.Vw_Romaneo
                            .Where(x => x.ProductorId == ProductorId)
                            .Distinct()
                            .Count();

                        txtProductores.Text = (Int32.Parse(txtProductores.Text) + Int32.Parse(productores.ToString())).ToString();
                    }
                }
            }
            else
            {
                txtProductores.Text = "0";
            }
        }

        private float CalcularTotalPesos(Guid PesadaId)
        {
            float totalPesos = 0;
            var liquidaciones = Context.Vw_Romaneo
                .Where(x => x.PesadaId == PesadaId 
                    && x.RomaneoPendiente == false)
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
            txtTotalAfectar.Text = "0";
            txtTotalAfectado.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal afectar = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "AFECTAR").ToString());
                        txtTotalAfectar.Text = decimal.Round((decimal.Parse(txtTotalAfectar.Text) + afectar), 2, MidpointRounding.AwayFromZero).ToString();
                        txtTotalAfectado.Text = decimal.Round((decimal.Parse(txtTotalAfectado.Text) + afectar), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtTotalAfectar.Text = "0.00";
                txtTotalAfectado.Text = "0.00";
            }
        }

        private void CalcularTotalGanancias()
        {
            txtGanancias.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal gcias = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "GCIAS").ToString());
                        txtGanancias.Text = decimal.Round((decimal.Parse(txtGanancias.Text) + gcias), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtGanancias.Text = "0.00";
            }
        }

        private void CalcularTotalIVA()
        {
            txtIVA.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal iva = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "IVA").ToString());
                        txtIVA.Text = decimal.Round((decimal.Parse(txtIVA.Text) + iva), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtIVA.Text = "0.00";
            }
        }

        private void CalcularTotalIIBB()
        {
            txtIIBB.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal iibb = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "IIBB").ToString());
                        txtIIBB.Text = decimal.Round((decimal.Parse(txtIIBB.Text) + iibb), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtIIBB.Text = "0.00";
            }
        }

        private void CalcularTotalSaludPublica()
        {
            txtSaludPublica.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal salud = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "SaludPublica").ToString());
                        txtSaludPublica.Text = decimal.Round((decimal.Parse(txtSaludPublica.Text) + salud), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtSaludPublica.Text = "0.00";
            }
        }

        private void CalcularTotalEEAOC()
        {
            txtEEAOC.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal eeaoc = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "EEAOC").ToString());
                        txtEEAOC.Text = decimal.Round((decimal.Parse(txtEEAOC.Text) + eeaoc), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtEEAOC.Text = "0.00";
            }
        }

        private void CalcularTotalRIEGO()
        {
            txtRiego.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal riego = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Riego").ToString());
                        txtRiego.Text = decimal.Round((decimal.Parse(txtRiego.Text) + riego), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtRiego.Text = "0.00";
            }
        }

        private void CalcularTotalMonotributo()
        {
            txtMonotributo.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal monotributo = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Monotributo").ToString());
                        txtMonotributo.Text = decimal.Round((decimal.Parse(txtRiego.Text) + monotributo), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtMonotributo.Text = "0.00";
            }
        }

        private void CalcularTotalNeto()
        {
            txtNeto.Text = "0";
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        decimal monotributo = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Monotributo").ToString());
                        txtNeto.Text = decimal.Round((decimal.Parse(txtTotalAfectado.Text) + decimal.Parse(txtGanancias.Text) + 
                            decimal.Parse(txtIVA.Text) + decimal.Parse(txtIIBB.Text) + decimal.Parse(txtSaludPublica.Text) +
                            decimal.Parse(txtEEAOC.Text) + decimal.Parse(txtRiego.Text) + decimal.Parse(txtMonotributo.Text)), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            else
            {
                txtNeto.Text = "0.00";
            }
        }

        #endregion  

        private void GenerarOP()
        {
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        try
                        {

                            #region Generar Orden Pago

                            OrdenPago ordenPago;
                            ordenPago = new OrdenPago();
                            ordenPago.Id = Guid.NewGuid();
                            ordenPago.NumIntOrdenPago = ContadorNumeroInternoOP();
                            ordenPago.NumOrdenPago = Int64.Parse(txtNumeroOP.Text);
                            Guid ProductorId = new Guid (gridViewLiquidacion.GetRowCellValue(i, "PRODUCTORID").ToString());
                            ordenPago.ProductorId = ProductorId;
                            ordenPago.Fecha = dpFechaPago.Value.Date;
                            decimal afectar = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "AFECTAR").ToString());
                            ordenPago.Subtotal = afectar;
                            decimal gcias = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "GCIAS").ToString());
                            ordenPago.Ganancias = gcias;
                            decimal iva = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "IVA").ToString());
                            ordenPago.IVA = iva;
                            decimal iibb = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "IIBB").ToString());
                            ordenPago.IIBB = iibb;
                            decimal salud = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "SaludPublica").ToString());
                            ordenPago.SaludPublica = salud;
                            decimal eeaoc = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "EEAOC").ToString());
                            ordenPago.EEAOC = eeaoc;
                            decimal riego = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Riego").ToString());
                            ordenPago.Riego = riego;
                            decimal monotributo = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Monotributo").ToString());
                            ordenPago.Monotributo = monotributo;
                            ordenPago.Neto = afectar + gcias + iva + iibb + salud + eeaoc + riego + monotributo;
                            ordenPago.Detalle = txtObservaciones.Text;

                            #region Actualizar Liquidacion con OrdenPagoId

                            Guid PesadaId = new Guid(gridViewLiquidacion.GetRowCellValue(i, "ID").ToString());
                            var liquidacion = Context.Pesada.Find(PesadaId);
                     
                            liquidacion.OrdenPagoId = ordenPago.Id;
                            Context.Entry(liquidacion).State = EntityState.Modified;
                            Context.SaveChanges();

                            #endregion
   
                            Context.OrdenPago.Add(ordenPago);
                            Context.SaveChanges();

                            #endregion                         

                        }
                        catch
                        {
                            throw;
                        }   
                    }
                }
            }
        }

        private int ContadorNumeroInternoOP()
        {
            var count = Context.OrdenPago.Count();
            if (count != 0)
            {
                var codigo = Context.OrdenPago
                    .Max(x => x.NumIntOrdenPago)
                    .ToString();
                return (Int16.Parse(codigo) + 1);
            }
            else
            {
                return 1;
            }
        }

        private int ContadorNumeroOP()
        {
            var count = Context.OrdenPago.Count();
            if (count != 0)
            {
                var codigo = Context.OrdenPago
                    .Max(x => x.NumOrdenPago)
                    .ToString();
                return (Int16.Parse(codigo) + 1);
            }
            else
            {
                return 1;
            }
        }

        #endregion     

        #endregion     

        #region Consulta de Ordenes de Pago

        #region Method Code

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFet.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtProductor.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtProductor.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void txtProductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtProductor.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtFet.Focus();
                }

                if (e.KeyChar == 8)
                {
                    txtFet.Text = string.Empty;
                    txtCuit.Text = string.Empty;
                    txtProvincia.Text = string.Empty;
                    ProductorId = Guid.Empty;
                }
            }
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarLiquidacion_Click(object sender, EventArgs e)
        {
            BuscarOrdenPago();
        }
  
        private void gridControlOrdenPago_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewOrdenPago.SelectedRowsCount > 0)
            {
                var Id = new Guid(gridViewOrdenPago
                    .GetRowCellValue(gridViewOrdenPago.FocusedRowHandle, "ID")
                    .ToString());
                var detallePago = new Form_AdministracionDetalleOrdenPago(Id);
                detallePago.Show();
            }
        }

        private void btnFormaPago_Click(object sender, EventArgs e)
        {
            if (gridViewOrdenPago.SelectedRowsCount > 0)
            {
                var Id = new Guid(gridViewOrdenPago
                    .GetRowCellValue(gridViewOrdenPago.FocusedRowHandle, "ID")
                    .ToString());
                var detallePago = new Form_AdministracionDetalleOrdenPago(Id);
                detallePago.Show();
            }
        }

        #endregion

        #region Method Dev

        private void BuscarProductor()
        {
            var result = (
                from a in Context.Vw_Productor
                select new
                {
                    full = a.nrofet + a.NOMBRE + a.CUIT,
                    ID = a.ID,
                    FET = a.nrofet,
                    PRODUCTOR = a.NOMBRE,
                    CUIT = a.CUIT,
                    PROVINCIA = a.Provincia
                });
            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var count = result
                    .Where(r => r.FET.Equals(txtFet.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtFet.Text;
                    _formBuscarProductor.target = DevConstantes.OrdenPago;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtProductor.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();

                    }
                    else
                    {
                        MessageBox.Show("N° de Fet no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtProductor.Text))
            {
                var count = result
                    .Where(r => r.PRODUCTOR.Contains(txtProductor.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtProductor.Text;
                    _formBuscarProductor.target = DevConstantes.OrdenPago;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtProductor.Text))
                      .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtProductor.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtProductor.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = empleado.CUIT;
            txtProvincia.Text = empleado.Provincia;
        }

        private void BuscarOrdenPago()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            Expression<Func<Vw_OrdenPago, bool>> pred = x => true;

            pred = checkPeriodo.Checked.Equals(true) ? pred.And(x => x.Fecha.Value >= dpDesdeOrdenPago.Value.Date
                && x.Fecha.Value <= dpHastaOrdenPago.Value.Date) : pred;

            if (ProductorId != Guid.Empty)
            {
                pred = pred.And(x => x.ProductorId == ProductorId);
            }

            var result = (
               from a in Context.Vw_OrdenPago.Where(pred).AsEnumerable()
               select new
               {
                   ID = a.OrdenPagoId,
                   PRODUCTORID = a.ProductorId,
                   FECHA = a.Fecha,
                   PRODUCTOR = a.NOMBRE,
                   FET = a.nrofet,
                   CUIT = a.CUIT,
                   SUBTOTAL = a.Subtotal,
                   GANANCIAS = a.Ganancias,
                   IVA = a.IVA,
                   IIBB = a.IIBB,
                   SaludPublica = a.SaludPublica,
                   EEAOC = a.EEAOC,
                   Riego = a.Riego,
                   Monotributo = a.Monotributo,
                   NETO = a.Neto
               })
               .OrderByDescending(x => x.FECHA)
               .ThenBy(x => x.FET)
               .ToList();

            gridControlOrdenPago.DataSource = result;
            gridViewOrdenPago.Columns[0].Visible = false;
            gridViewOrdenPago.Columns[1].Visible = false;
            gridViewOrdenPago.Columns[2].Caption = "Fecha";
            gridViewOrdenPago.Columns[2].Width = 60;
            gridViewOrdenPago.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[3].Caption = "Productor";
            gridViewOrdenPago.Columns[3].Width = 150;
            gridViewOrdenPago.Columns[4].Caption = "FET";
            gridViewOrdenPago.Columns[4].Width = 55;
            gridViewOrdenPago.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[5].Caption = "CUIT";
            gridViewOrdenPago.Columns[5].Width = 60;
            gridViewOrdenPago.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[6].Caption = "SUBTOTAL";
            gridViewOrdenPago.Columns[6].Width = 40;
            gridViewOrdenPago.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[7].Caption = "Ganancias";
            gridViewOrdenPago.Columns[7].Width = 60;
            gridViewOrdenPago.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[8].Caption = "IVA";
            gridViewOrdenPago.Columns[8].Width = 60;
            gridViewOrdenPago.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[9].Caption = "IIBB";
            gridViewOrdenPago.Columns[9].Width = 60;
            gridViewOrdenPago.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[10].Caption = "Salud Pública";
            gridViewOrdenPago.Columns[10].Width = 60;
            gridViewOrdenPago.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[11].Caption = "EEAOC";
            gridViewOrdenPago.Columns[11].Width = 60;
            gridViewOrdenPago.Columns[11].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[11].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[12].Caption = "C. Riego";
            gridViewOrdenPago.Columns[12].Width = 60;
            gridViewOrdenPago.Columns[12].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[12].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[13].Caption = "Monotributo";
            gridViewOrdenPago.Columns[13].Width = 60;
            gridViewOrdenPago.Columns[13].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[13].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenPago.Columns[14].Caption = "Neto";
            gridViewOrdenPago.Columns[14].Width = 60;
            gridViewOrdenPago.Columns[14].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenPago.Columns[14].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewOrdenPago.RowCount; i++)
            {
                gridViewOrdenPago.SelectRow(i);
            }
           
        }

        #endregion

        #endregion
    }
}