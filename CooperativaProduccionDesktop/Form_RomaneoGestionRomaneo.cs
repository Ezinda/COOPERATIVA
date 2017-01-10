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


namespace CooperativaProduccion
{
    public partial class Form_RomaneoGestionRomaneo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;

        public Form_RomaneoGestionRomaneo()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

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
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
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
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
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
            }
            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarRomaneo_Click(object sender, EventArgs e)
        {
            BuscarRomaneo();
        }

        private void BuscarRomaneo()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = checkPeriodo.Checked ? pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date &&
                x.FechaRomaneo <= dpHastaRomaneo.Value.Date) : pred;

            pred = !string.IsNullOrEmpty(txtFet.Text) ? pred.And(x => x.ProductorId == ProductorId) : pred;

            var result = (
               from a in Context.Vw_Romaneo
                   .Where(pred)
                   .Where(x => x.OrdenPagoId == null)
               select new
               {
                   ID = a.PesadaId,
                   FECHA = a.FechaRomaneo,
                   NUMROMANEO = a.NumRomaneo,
                   PRODUCTOR = a.NOMBRE,
                   CUIT = a.CUIT,
                   FET = a.nrofet,
                   PROVINCIA = a.Provincia,
                   LETRA = a.Letra,
                   KILOS = a.Totalkg,
                   BRUTOSINIVA = a.ImporteBruto
               })
               .OrderByDescending(x => x.FECHA)
               .ThenBy(x => x.FET)
               .ToList();

            gridControlRomaneo.DataSource = result;
            gridViewRomaneo.Columns[0].Visible = false;
            gridViewRomaneo.Columns[1].Caption = "Fecha Romaneo";
            gridViewRomaneo.Columns[1].Width = 60;
            gridViewRomaneo.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].Caption = "Número Romaneo";
            gridViewRomaneo.Columns[2].Width = 60;
            gridViewRomaneo.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[3].Caption = "Productor";
            gridViewRomaneo.Columns[3].Width = 150;
            gridViewRomaneo.Columns[4].Caption = "CUIT";
            gridViewRomaneo.Columns[4].Width = 75;
            gridViewRomaneo.Columns[5].Caption = "FET";
            gridViewRomaneo.Columns[5].Width = 55;
            gridViewRomaneo.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[6].Caption = "Provincia";
            gridViewRomaneo.Columns[6].Width = 60;
            gridViewRomaneo.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].Caption = "Letra";
            gridViewRomaneo.Columns[7].Width = 40;
            gridViewRomaneo.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].Caption = "Kilos";
            gridViewRomaneo.Columns[8].Width = 40;
            gridViewRomaneo.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].Caption = "Bruto sin IVA";
            gridViewRomaneo.Columns[9].Width = 60;
            gridViewRomaneo.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[10].Caption = "Nro. Int. Liquidación";
            gridViewRomaneo.Columns[10].Width = 60;
            gridViewRomaneo.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewRomaneo.RowCount; i++)
            {
                gridViewRomaneo.SelectRow(i);
            }
        }
     
    }
}