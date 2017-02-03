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
    public partial class Form_AdministracionBuscarCliente : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public volatile string fet;
        public volatile string target;
        public volatile string nombre;
        public volatile string cuit;

        public Form_AdministracionBuscarCliente()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        #region Method dev

        public void Buscar()
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
            var busqueda = result
                .Where(r => r.full.Contains(txtBusqueda.Text))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlCliente.DataSource = busqueda;
                gridViewCliente.Columns[0].Visible = false;
                gridViewCliente.Columns[1].Visible = false;
                gridViewCliente.Columns[2].Width = 90;
                gridViewCliente.Columns[3].Width = 150;
                gridViewCliente.Columns[4].Width = 100;
            }
        }

        public void BuscarNombre()
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
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
            var busqueda = result
                .Where(r => r.CLIENTE.Contains(nombre))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlCliente.DataSource = busqueda;
                gridViewCliente.Columns[0].Visible = false;
                gridViewCliente.Columns[1].Visible = false;
                gridViewCliente.Columns[2].Width = 90;
                gridViewCliente.Columns[3].Width = 150;
                gridViewCliente.Columns[4].Width = 100;
            }
        }

        public void BuscarCuit()
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
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
            var busqueda = result
                .Where(r => r.CUIT.Contains(cuit))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlCliente.DataSource = busqueda;
                gridViewCliente.Columns[0].Visible = false;
                gridViewCliente.Columns[1].Visible = false;
                gridViewCliente.Columns[2].Width = 90;
                gridViewCliente.Columns[3].Width = 150;
                gridViewCliente.Columns[4].Width = 100;
            }
        }

        #endregion

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void gridControlCliente_DoubleClick(object sender, EventArgs e)
        {
            if (target.Equals(DevConstantes.OrdenVenta))
            {
                IEnlace mienlace = this.Owner as Form_AdministracionOrdenVenta;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewCliente.GetRowCellValue(gridViewCliente.FocusedRowHandle, "ID").ToString()),
                        gridViewCliente.GetRowCellValue(gridViewCliente.FocusedRowHandle, "CUIT").ToString(),
                        gridViewCliente.GetRowCellValue(gridViewCliente.FocusedRowHandle, "CLIENTE").ToString());
                }
                this.Dispose();
            }
            else if (target.Equals(DevConstantes.Remito))
            {
                IEnlace mienlace = this.Owner as Form_AdministracionRemitoElectronico;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewCliente.GetRowCellValue(gridViewCliente.FocusedRowHandle, "ID").ToString()),
                        gridViewCliente.GetRowCellValue(gridViewCliente.FocusedRowHandle, "CUIT").ToString(),
                        gridViewCliente.GetRowCellValue(gridViewCliente.FocusedRowHandle, "CLIENTE").ToString());
                }
                this.Dispose();
            }
        }
    }
}