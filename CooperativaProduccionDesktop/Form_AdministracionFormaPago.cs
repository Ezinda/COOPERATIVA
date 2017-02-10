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

namespace CooperativaProduccion
{
    public partial class Form_AdministracionFormaPago : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public Guid OrdenId;
        public Form_AdministracionFormaPago(Guid OrdenPagoId,string neto)
        {
            InitializeComponent(); 
            Context = new CooperativaProduccionEntities();
            CargarDatos(OrdenPagoId,neto);
            OrdenId = OrdenPagoId;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarDatos(Guid OrdenPagoId,string neto)
        {
            var ordenPago = Context.Vw_OrdenPago
                .Where(x => x.OrdenPagoId == OrdenPagoId)
                .FirstOrDefault();
            if (ordenPago != null)
            {
                #region Datos Orden Pago

                dpFechaOrden.Value = ordenPago.Fecha;
                txtNumOrdenPago.Text = ordenPago.NumOrdenPago.ToString();
                txtPuntoVenta.Text = DevConstantes.PuntoVenta;
                txtImporte.Text = neto;

                #endregion

                #region Datos Productor

                txtProductor.Text = ordenPago.NOMBRE;

                #endregion

            }
        }

        private void cbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValor.Items.Clear();

            if (cbOrigen.Text == "Caja" || cbOrigen.Text == "Cuenta Bancaria")
            {
                cbValor.Items.Add("Efectivo");
                cbCta.Visible =      false;
                cbcheque.Visible =   false;
                lblEmision.Visible = false;
                lblVto.Visible =     false;
                dpEmision.Visible =  false;
                dpVto.Visible =      false;
            }
            else
            {
                cbValor.Items.Add("10073234/3 - Banco Macro");
                cbCta.Items.Add("1");
                cbcheque.Items.Add("20006");
                cbcheque.Items.Add("20007");
                cbCta.Visible = true;
                cbcheque.Visible = true;
                lblEmision.Visible = true;
                lblVto.Visible = true;
                dpEmision.Visible = true;
                dpVto.Visible = true;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Pago pago;
                pago = new Pago();
                pago.Id = Guid.NewGuid();
                pago.OrdenPagoId = OrdenId;
                pago.Importe = Convert.ToDecimal(txtImportePagar.Text);
                Context.Pago.Add(pago);
                Context.SaveChanges();
                
            }
            catch
            {
                throw;
            }
            this.Close();
        }

    }
}