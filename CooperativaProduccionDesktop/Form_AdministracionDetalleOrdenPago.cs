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
  
        public Form_AdministracionDetalleOrdenPago(Guid OrdenPagoId)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            AddNewRow();
            CargarDatos(OrdenPagoId);
           
        }

        private void CargarDatos(Guid OrdenPagoId)
        {
            var ordenPago = Context.Vw_OrdenPago
                .Where(x => x.OrdenPagoId == OrdenPagoId)
                .FirstOrDefault();
            if (ordenPago != null)
            {
                #region Datos Orden Pago

                dpFechaOrden.Value = ordenPago.Fecha.Value;
                txtNumOrdenPago.Text = ordenPago.OrdenPago.Value.ToString();
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
                    txtSituacionIva.Text = productor.IVA;
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
                        .Where(x=>x.numAfipLiquidacion != null)
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
                        this.dgvPendientes.Rows.Add(resultp.ID, resultp.FECHA, resultp.LETRA,
                            resultp.NUMAFIP,resultp.Saldo,resultp.Saldo);
                    }
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

            //Add Header Texts to be displayed on the Columns
            d1.HeaderText = "Id";
            d2.HeaderText = "Fecha";
            d3.HeaderText = "Letra";
            d4.HeaderText = "N° Comprobante";
            d5.HeaderText = "Saldo";
            d6.HeaderText = "Afectar";
            
            d1.Visible = false;
            d2.Width = 40;
            d3.Width = 30;
            d4.Width = 60;
            d5.Width = 50;
            d6.Width = 50;
            //Add the Columns to the DataGridView
            dgvPendientes.Columns.AddRange(d1, d2, d3, d4,d5,d6);
        }

        private void AddNewRow()
        {
            AddNewRowPendiente();
        }


    }
}