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
    public partial class Form_AdministracionRemitoElectronico : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarCliente _formBuscarCliente;
        private Guid OrdenVentaId;
        private Guid ClienteId;

        public Form_AdministracionRemitoElectronico()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ClienteId = Id;
            txtCliente.Text = nombre;
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuscarCliente();
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }

        private void BuscarCliente()
        {
            var result =
                (from a in Context.Vw_Cliente
                 select new
                 {
                     full = a.CUIT + a.RAZONSOCIAL + a.CUITE,
                     ID = a.ID,
                     CUIT = a.CUIT.Contains(DevConstantes.XX) ? a.CUITE : a.CUIT,
                     CLIENTE = a.RAZONSOCIAL,
                     PROVINCIA = a.Provincia
                 });

            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                var count = result
                    .Where(x => x.CUIT.Contains(txtCliente.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarCliente = new Form_AdministracionBuscarCliente();
                    _formBuscarCliente.cuit = txtCliente.Text;
                    _formBuscarCliente.target = DevConstantes.OrdenVenta;
                    _formBuscarCliente.BuscarCuit();
                    _formBuscarCliente.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.CUIT.Equals(txtCliente.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ClienteId = busqueda.ID;
                        txtCliente.Text = busqueda.CLIENTE;
                    }
                    else
                    {
                        count = result
                            .Where(x => x.CLIENTE.Contains(txtCliente.Text))
                            .Count();
                        if (count > 1)
                        {
                            _formBuscarCliente = new Form_AdministracionBuscarCliente();
                            _formBuscarCliente.nombre = txtCliente.Text;
                            _formBuscarCliente.target = DevConstantes.OrdenVenta;
                            _formBuscarCliente.BuscarNombre();
                            _formBuscarCliente.ShowDialog(this);
                        }
                        else
                        {
                            var busquedaNombre = result
                                .Where(x => x.CLIENTE.Contains(txtCliente.Text))
                                .FirstOrDefault();

                            if (busquedaNombre != null)
                            {
                                ClienteId = busquedaNombre.ID;
                                txtCliente.Text = busquedaNombre.CLIENTE;
                            }
                        }
                    }
                }
            }
        }

        private void btnBuscarOrdenVentaPendiente_Click(object sender, EventArgs e)
        {
            BuscarPendientes();
        }

        private void BuscarPendientes()
        {
            Expression<Func<OrdenVenta, bool>> pred = x => true;

            pred = txtCliente.Text != string.Empty ? pred.And(x => x.ClienteId == ClienteId) : pred;

            pred = checkPeriodo.Checked ? pred.And(x => x.Fecha >= dpDesde.Value && x.Fecha <= dpHasta.Value) : pred;

            var ordenVenta =
                (from o in Context.OrdenVenta.Where(pred)
                 join p in Context.Vw_Producto
                 on o.ProductoId equals p.ID
                 join c in Context.Vw_Cliente
                 on o.ClienteId equals c.ID
                 select new
                 {
                     OrdenVentaId = o.Id,
                     NumOperacion = o.NumOperacion,
                     NumOrden = o.NumOrden,
                     Cliente = c.RAZONSOCIAL,
                     Producto = p.DESCRIPCION,
                     CajaDesde = o.DesdeCaja,
                     CajaHasta = o.HastaCaja,
                     Fecha = o.Fecha,
                     Pendiente = o.Pendiente == true ?
                        DevConstantes.SI : DevConstantes.NO
                 })
                 .OrderBy(x => x.NumOrden)
                 .ToList();

            gridControlOrdenVentaPendiente.DataSource = ordenVenta;
            gridViewOrdenVentaPendiente.Columns[0].Visible = false;
            gridViewOrdenVentaPendiente.Columns[1].Caption = "N° Operación";
            gridViewOrdenVentaPendiente.Columns[1].Width = 120;
            gridViewOrdenVentaPendiente.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenVentaPendiente.Columns[2].Caption = "N° Orden";
            gridViewOrdenVentaPendiente.Columns[2].Width = 120;
            gridViewOrdenVentaPendiente.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenVentaPendiente.Columns[3].Caption = "Cliente";
            gridViewOrdenVentaPendiente.Columns[3].Width = 250;
            gridViewOrdenVentaPendiente.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridViewOrdenVentaPendiente.Columns[4].Caption = "Producto";
            gridViewOrdenVentaPendiente.Columns[4].Width = 120;
            gridViewOrdenVentaPendiente.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[5].Caption = "Caja Desde";
            gridViewOrdenVentaPendiente.Columns[5].Width = 120;
            gridViewOrdenVentaPendiente.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[6].Caption = "Caja Hasta";
            gridViewOrdenVentaPendiente.Columns[6].Width = 120;
            gridViewOrdenVentaPendiente.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[7].Caption = "Fecha";
            gridViewOrdenVentaPendiente.Columns[7].Width = 90;
            gridViewOrdenVentaPendiente.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[8].Caption = "Pendiente";
            gridViewOrdenVentaPendiente.Columns[8].Width = 120;
            gridViewOrdenVentaPendiente.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void gridControlOrdenVentaPendiente_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}