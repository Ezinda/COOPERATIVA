﻿using System;
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
    public partial class Form_AdministracionRemitoElectronico : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace,IEnlaceActualizar
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
            if (Remito.SelectedTabPage.Equals(TabIngresoRemito))
            {
                txtCliente.Text = nombre;
            }
            else
            {
                txtClienteRemito.Text = nombre;
            }
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
                     CUIT = a.CUIT.ToUpper().Contains(DevConstantes.XX) ? a.CUITE : a.CUIT,
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
                    _formBuscarCliente.target = DevConstantes.Remito;
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
                            _formBuscarCliente.target = DevConstantes.Remito;
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

        private void BuscarClienteConsulta()
        {
            var result =
                (from a in Context.Vw_Cliente
                 select new
                 {
                     full = a.CUIT + a.RAZONSOCIAL + a.CUITE,
                     ID = a.ID,
                     CUIT = a.CUIT.ToUpper().Contains(DevConstantes.XX) ? a.CUITE : a.CUIT,
                     CLIENTE = a.RAZONSOCIAL,
                     PROVINCIA = a.Provincia
                 });

            if (!string.IsNullOrEmpty(txtClienteRemito.Text))
            {
                var count = result
                    .Where(x => x.CUIT.Contains(txtClienteRemito.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarCliente = new Form_AdministracionBuscarCliente();
                    _formBuscarCliente.cuit = txtClienteRemito.Text;
                    _formBuscarCliente.target = DevConstantes.Remito;
                    _formBuscarCliente.BuscarCuit();
                    _formBuscarCliente.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.CUIT.Equals(txtClienteRemito.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ClienteId = busqueda.ID;
                        txtClienteRemito.Text = busqueda.CLIENTE;
                    }
                    else
                    {
                        count = result
                            .Where(x => x.CLIENTE.Contains(txtClienteRemito.Text))
                            .Count();
                        if (count > 1)
                        {
                            _formBuscarCliente = new Form_AdministracionBuscarCliente();
                            _formBuscarCliente.nombre = txtClienteRemito.Text;
                            _formBuscarCliente.target = DevConstantes.Remito;
                            _formBuscarCliente.BuscarNombre();
                            _formBuscarCliente.ShowDialog(this);
                        }
                        else
                        {
                            var busquedaNombre = result
                                .Where(x => x.CLIENTE.Contains(txtClienteRemito.Text))
                                .FirstOrDefault();

                            if (busquedaNombre != null)
                            {
                                ClienteId = busquedaNombre.ID;
                                txtClienteRemito.Text = busquedaNombre.CLIENTE;
                            }
                        }
                    }
                }
            }
        }

        private void btnBuscarOrdenVentaPendiente_Click(object sender, EventArgs e)
        {
            BuscarOrdenVentaPendiente();
        }

        private void BuscarOrdenVentaPendiente()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            Expression<Func<OrdenVenta, bool>> pred = x => true;

            pred = txtCliente.Text != string.Empty ? pred.And(x => x.ClienteId == ClienteId) : pred;

            pred = pred.And(x => x.Fecha >= dpDesde.Value.Date && x.Fecha <= dpHasta.Value.Date);

            pred = pred.And(x => x.Pendiente == true);

            var ordenVenta =
                (from o in Context.OrdenVenta.Where(pred)
                 join c in Context.Vw_Cliente
                 on o.ClienteId equals c.ID
                 select new
                 {
                     OrdenVentaId = o.Id,
                     NumOperacion = o.NumOperacion,
                     NumOrden = o.NumOrden,
                     Cliente = c.RAZONSOCIAL,
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
            gridViewOrdenVentaPendiente.Columns[4].Caption = "Fecha";
            gridViewOrdenVentaPendiente.Columns[4].Width = 90;
            gridViewOrdenVentaPendiente.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[5].Caption = "Pendiente";
            gridViewOrdenVentaPendiente.Columns[5].Width = 120;
            gridViewOrdenVentaPendiente.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaPendiente.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void gridControlOrdenVentaPendiente_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewOrdenVentaPendiente.SelectedRowsCount > 0)
            {
                var Id = new Guid(gridViewOrdenVentaPendiente
                      .GetRowCellValue(gridViewOrdenVentaPendiente.FocusedRowHandle, "OrdenVentaId")
                      .ToString());
                CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
                var orden = Context.OrdenVenta
                    .Where(x => x.Id == Id)
                    .FirstOrDefault();
                if (orden.Pendiente == true)
                {
                    var nr = new Form_AdministracionNuevoRemito(Id,true);
                    nr.ShowDialog(this);
                }
            }
        }

        void IEnlaceActualizar.Enviar(bool Enviar)
        {
            if (Enviar.Equals(true))
            {
                BuscarOrdenVentaPendiente();
            }
        }

        private void txtClienteRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuscarClienteConsulta();
            }
        }

        private void btnBuscarClienteRemito_Click(object sender, EventArgs e)
        {
            BuscarClienteConsulta();
        }

        private void btnBuscarRemito_Click(object sender, EventArgs e)
        {
            BuscarRemitos();
        }

        private void BuscarRemitos()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            Expression<Func<Remito, bool>> pred = x => true;

            pred = txtClienteRemito.Text != string.Empty ? 
                pred.And(x => x.ClienteId == ClienteId) : pred;

            pred = pred.And(x => x.FechaRemito >= dpDesdeRemito.Value.Date 
                     && x.FechaRemito <= dpHastaRemito.Value.Date);

            var remito =
                (from r in Context.Remito.Where(pred).AsEnumerable()
                 join c in Context.Vw_Cliente
                 on r.ClienteId equals c.ID
                 select new
                 {
                     RemitoId = r.Id,
                     Fecha = r.FechaRemito,
                     PuntoVenta = r.PuntoVenta.ToString().PadLeft(4, '0'),
                     NumRemito = r.NumRemito,
                     NumOperacion = r.NumOperacion,
                     NumOrden = r.NumOrden,
                     FechaOrden = r.FechaOrden,
                     Cliente = c.RAZONSOCIAL                    
                 })
                 .OrderBy(x => x.NumRemito)
                 .ToList();

            gridControlRemito.DataSource = remito;
            gridViewRemito.Columns[0].Visible = false;
            gridViewRemito.Columns[1].Caption = "Fecha Remito";
            gridViewRemito.Columns[1].Width = 90;
            gridViewRemito.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[2].Caption = "Punto Venta";
            gridViewRemito.Columns[2].Width = 90;
            gridViewRemito.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[3].Caption = "Número Remito";
            gridViewRemito.Columns[3].Width = 100;
            gridViewRemito.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[4].Caption = "Número Operación";
            gridViewRemito.Columns[4].Width = 90;
            gridViewRemito.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[5].Caption = "Número Orden";
            gridViewRemito.Columns[5].Width = 90;
            gridViewRemito.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[6].Caption = "Fecha Orden";
            gridViewRemito.Columns[6].Width = 90;
            gridViewRemito.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[7].Caption = "Cliente";
            gridViewRemito.Columns[7].Width = 250;
            gridViewRemito.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRemito.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            
        }

        private void gridControlRemito_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewRemito.SelectedRowsCount > 0)
            {
                var Id = new Guid(gridViewRemito
                    .GetRowCellValue(gridViewRemito.FocusedRowHandle, "RemitoId")
                    .ToString());
                CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
                var remito = Context.Remito
                    .Where(x => x.Id == Id)
                    .FirstOrDefault();
                if (remito != null)
                {
                    var nr = new Form_AdministracionNuevoRemito(Id,false);
                    nr.ShowDialog(this);
                }
            }
        }
        
    }
}