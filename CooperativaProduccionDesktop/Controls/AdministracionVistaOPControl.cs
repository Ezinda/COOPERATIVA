using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopEntities.Models;
using Extensions;
using System.Linq.Expressions;
using DevExpress.Utils;
using CooperativaProduccion.Helpers;
using CooperativaProduccion.ViewModels;
using GestionRRHH.Services;

namespace CooperativaProduccion.Controls
{
    public partial class AdministracionVistaOPControl : UserControl
    {
        private OrdenesDePagoDetalleViewModel _ordenes;

        private Form_AdministracionBuscarProductor _formBuscarProductor;

        private IPagosManager _pagosManager;
        private IProductoresManager _productoresManager;
        private IMessageBoxService _messageBoxService;

        private Guid _productorid;

        public AdministracionVistaOPControl()
        {
            InitializeComponent();

            _pagosManager = new PagosManager();
            _productoresManager = new ProductoresManager();
            _messageBoxService = new MessageBoxService();

            this.btnBuscarLiquidacion.Click += this.btnBuscarLiquidacion_Click;

            this.txtFet.KeyPress += this.txtFet_KeyPress;
            this.btnBuscarFet.Click += this.btnBuscarFet_Click;

            this.txtProductor.KeyPress += this.txtProductor_KeyPress;
            this.btnBuscarProductor.Click += this.btnBuscarProductor_Click;

            this.gridControlOrdenPago.DoubleClick += this.gridControlOrdenPago_DoubleClick;
            this.gridViewOrdenPago.CustomUnboundColumnData += gridViewOrdenPago_CustomUnboundColumnData;

            this.btnFormaPago.Click += this.btnFormaPago_Click;
        }

        void btnBuscarLiquidacion_Click(object sender, EventArgs e)
        {
            if (_productorid != Guid.Empty)
            {
                BuscarOrdenesDePagoDeProductor();
            }
            else
            {
                BuscarOrdenesDePago();
            }
        }

        void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFet.Text))
                {
                    BuscarProductorPorFET();
                }
                else
                {
                    txtProductor.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                UnsetProductor();
            }
        }

        void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductorPorFET();
        }

        void txtProductor_KeyPress(object sender, KeyPressEventArgs e)
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
                    UnsetProductor();
                }
            }
        }

        void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        void gridControlOrdenPago_DoubleClick(object sender, EventArgs e)
        {
            AdministrarDetalleDeOrdenDePago();
        }

        void gridViewOrdenPago_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var fieldname = e.Column.FieldName;

                if (fieldname == "NetoPorPagar")
                {
                    var row = e.Row as RowOrdenPago;
                    var orden = _ordenes.Items.Where(x => x.PesadaId == row.ID).Single();

                    e.Value = orden.NetoPorPagar;
                }
                else
                {
                    var retencion = _ordenes.Retenciones.Where(x => x.Nombre == fieldname).FirstOrDefault();

                    if (retencion == null)
                    {
                        return;
                    }

                    var row = e.Row as RowOrdenPago;
                    var orden = _ordenes.Items.Where(x => x.PesadaId == row.ID).Single();
                    var retencionaplicada = orden.RetencionesAplicadas.Where(x => x.Nombre == retencion.Nombre).Single();

                    e.Value = retencionaplicada.Importe;
                }
            }
        }

        void btnFormaPago_Click(object sender, EventArgs e)
        {
            AdministrarDetalleDeOrdenDePago();
        }

        private void SetProductor(Guid id)
        {
            _productorid = id;

            var productor = _productoresManager.GetProductor(_productorid);

            txtFet.Text = productor.FET;
            txtProductor.Text = productor.Nombre;
            txtCuit.Text = productor.CUIT;
            txtProvincia.Text = productor.Provincia;
        }

        private void UnsetProductor()
        {
            _productorid = Guid.Empty;

            txtProductor.Text = String.Empty;
            txtCuit.Text = String.Empty;
            txtProvincia.Text = String.Empty;
        }

        private void BuscarOrdenesDePago()
        {
            var usarperiodo = checkPeriodo.Checked;
            var desde = dpDesdeOrdenPago.Value.Date;
            var hasta = dpHastaOrdenPago.Value.Date;

            OrdenesDePagoDetalleViewModel ordenesvm;

            if (usarperiodo)
            {
                ordenesvm = _pagosManager.ListarOrdenesDePago(desde, hasta);
            }
            else
            {
                ordenesvm = _pagosManager.ListarOrdenesDePago();
            }

            _ordenes = ordenesvm;

            ActualizarControlesGeneracionDeOrdenes();
        }

        private void BuscarOrdenesDePagoDeProductor()
        {
            var usarperiodo = checkPeriodo.Checked;
            var desde = dpDesdeOrdenPago.Value.Date;
            var hasta = dpHastaOrdenPago.Value.Date;

            OrdenesDePagoDetalleViewModel ordenesvm;

            if (usarperiodo)
            {
                ordenesvm = _pagosManager.ListarOrdenesDePagoDeProductor(_productorid, desde, hasta);
            }
            else
            {
                ordenesvm = _pagosManager.ListarOrdenesDePagoDeProductor(_productorid);
            }

            _ordenes = ordenesvm;

            ActualizarControlesGeneracionDeOrdenes();
        }

        private void ActualizarControlesGeneracionDeOrdenes()
        {
            var source = _ordenes.Items
                .Select(x => new RowOrdenPago()
                {
                    ID = x.Id,
                    PRODUCTORID = x.ProductorId,
                    FECHA = x.Fecha,
                    PRODUCTOR = x.Productor,
                    FET = x.FET,
                    CUIT = x.CUIT,
                    ImportePorPagar = x.ImportePorPagar,
                })
                .ToList();

            gridViewOrdenPago.Columns.Clear();

            gridControlOrdenPago.DataSource = new BindingList<RowOrdenPago>(source);

            gridViewOrdenPago.Columns["ID"].Visible = false;
            gridViewOrdenPago.Columns["PRODUCTORID"].Visible = false;

            //gridViewOrdenPago.Columns[2].Caption = "Fecha";
            //gridViewOrdenPago.Columns[2].Width = 60;
            //gridViewOrdenPago.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[3].Caption = "Productor";
            //gridViewOrdenPago.Columns[3].Width = 150;
            //gridViewOrdenPago.Columns[4].Caption = "FET";
            //gridViewOrdenPago.Columns[4].Width = 55;
            //gridViewOrdenPago.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[5].Caption = "CUIT";
            //gridViewOrdenPago.Columns[5].Width = 60;
            //gridViewOrdenPago.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[6].Caption = "SUBTOTAL";
            //gridViewOrdenPago.Columns[6].Width = 40;
            //gridViewOrdenPago.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[7].Caption = "Ganancias";
            //gridViewOrdenPago.Columns[7].Width = 60;
            //gridViewOrdenPago.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[8].Caption = "IVA";
            //gridViewOrdenPago.Columns[8].Width = 60;
            //gridViewOrdenPago.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[9].Caption = "IIBB";
            //gridViewOrdenPago.Columns[9].Width = 60;
            //gridViewOrdenPago.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[10].Caption = "Salud Pública";
            //gridViewOrdenPago.Columns[10].Width = 60;
            //gridViewOrdenPago.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[11].Caption = "EEAOC";
            //gridViewOrdenPago.Columns[11].Width = 60;
            //gridViewOrdenPago.Columns[11].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[11].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[12].Caption = "C. Riego";
            //gridViewOrdenPago.Columns[12].Width = 60;
            //gridViewOrdenPago.Columns[12].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[12].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[13].Caption = "Monotributo";
            //gridViewOrdenPago.Columns[13].Width = 60;
            //gridViewOrdenPago.Columns[13].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[13].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridViewOrdenPago.Columns[14].Caption = "Neto";
            //gridViewOrdenPago.Columns[14].Width = 60;
            //gridViewOrdenPago.Columns[14].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gridViewOrdenPago.Columns[14].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            foreach (var item in _ordenes.Retenciones)
            {
                var unbColumn = gridViewOrdenPago.Columns.AddField(item.Nombre);

                unbColumn.VisibleIndex = gridViewOrdenPago.Columns.Count;
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                unbColumn.OptionsColumn.AllowEdit = false;
            }

            var unbColumnNeto = gridViewOrdenPago.Columns.AddField("NetoPorPagar");

            unbColumnNeto.VisibleIndex = gridViewOrdenPago.Columns.Count;
            unbColumnNeto.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            unbColumnNeto.OptionsColumn.AllowEdit = false;

            gridViewOrdenPago.BestFitColumns();

            gridViewOrdenPago.SelectAll();
        }

        private void BuscarProductor()
        {
            var textodebusqueda = txtProductor.Text;

            _formBuscarProductor = new Form_AdministracionBuscarProductor();
            _formBuscarProductor.fet = textodebusqueda;
            _formBuscarProductor.target = DevConstantes.OrdenPago;
            _formBuscarProductor.ProductorSeleccionado += formBuscarProductor_ProductorSeleccionado;
            var count = _formBuscarProductor.BuscarPorProductor();

            if (count == 0)
            {
                _messageBoxService.Show("No se encuentra Nº de FET", "No se ha encontrado el número de FET ingresado.");
            }
            else if (count > 1)
            {
                _formBuscarProductor.ShowDialog(this);
            }
        }

        private void BuscarProductorPorFET()
        {
            var textodebusqueda = txtFet.Text;

            _formBuscarProductor = new Form_AdministracionBuscarProductor();
            _formBuscarProductor.fet = textodebusqueda;
            _formBuscarProductor.target = DevConstantes.OrdenPago;
            _formBuscarProductor.ProductorSeleccionado += formBuscarProductor_ProductorSeleccionado;
            var count = _formBuscarProductor.BuscarPorFet();

            if (count == 0)
            {
                _messageBoxService.Show("No se encuentra Nº de FET", "No se ha encontrado el número de FET ingresado.");
            }
            else if (count > 1)
            {
                _formBuscarProductor.ShowDialog(this);
            }
        }

        private void AdministrarDetalleDeOrdenDePago()
        {
            var rowhandle = gridViewOrdenPago.FocusedRowHandle;

            if (rowhandle >= 0)
            {
                var Id = (Guid)gridViewOrdenPago.GetRowCellValue(gridViewOrdenPago.FocusedRowHandle, "ID");

                var detallePago = new Form_AdministracionDetalleOrdenPago(Id);
                detallePago.Show();
            }
        }

        private void formBuscarProductor_ProductorSeleccionado(object sender, EventArgs e)
        {
            SetProductor(_formBuscarProductor.IdProductorSeleccionado);
        }

        class RowOrdenPago
        {
            public Guid ID { get; set; }
            public Guid PRODUCTORID { get; set; }
            public DateTime FECHA { get; set; }
            public string PRODUCTOR { get; set; }
            public string FET { get; set; }
            public string CUIT { get; set; }
            public decimal ImportePorPagar { get; set; }
        }
    }
}