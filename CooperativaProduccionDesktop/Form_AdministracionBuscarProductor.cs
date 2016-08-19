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
    public partial class Form_AdministracionBuscarProductor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public volatile string fet;
        public volatile string target;
        public volatile string nombre;
        public volatile string cuit;
        public Form_AdministracionBuscarProductor()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }
        }

        public void Buscar()
        {
            var result = (
                   from a in Context.Productor
                   select new
                   {
                       ID = a.Id,
                       full = a.Fet + a.Nombre + a.Cuit,
                       FET = a.Fet,
                       PRODUCTOR = a.Nombre,
                       CUIT = a.Cuit,
                       PROVINCIA = a.Provincia
                   });
            var busqueda = result
                .Where(r => r.full.Contains(txtBusqueda.Text))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlProductor.DataSource = busqueda;
                gridViewProductor.Columns[0].Visible = false;
                gridViewProductor.Columns[1].Visible = false;
                gridViewProductor.Columns[2].Width = 90;
                gridViewProductor.Columns[3].Width = 150;
                gridViewProductor.Columns[4].Width = 100;
                gridViewProductor.Columns[5].Width = 100;
            }
        }

        public void BuscarFet()
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
            var result = (
                    from a in dbContext.Productor
                    select new
                    {
                        ID = a.Id,
                        full = a.Fet + a.Nombre + a.Cuit,
                        FET = a.Fet,
                        PRODUCTOR = a.Nombre,
                        CUIT = a.Cuit,
                        PROVINCIA = a.Provincia
                    });
            var busqueda = result
                .Where(r => r.FET.Contains(fet))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlProductor.DataSource = busqueda;
                gridViewProductor.Columns[0].Visible = false;
                gridViewProductor.Columns[1].Visible = false;
                gridViewProductor.Columns[2].Width = 90;
                gridViewProductor.Columns[3].Width = 150;
                gridViewProductor.Columns[4].Width = 100;
                gridViewProductor.Columns[5].Width = 100;
            }
        }

        public void BuscarNombre()
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
            var result = (
                    from a in dbContext.Productor
                    select new
                    {
                        ID = a.Id,
                        full = a.Fet + a.Nombre + a.Cuit,
                        FET = a.Fet,
                        PRODUCTOR = a.Nombre,
                        CUIT = a.Cuit,
                        PROVINCIA = a.Provincia
                    });
            var busqueda = result
                .Where(r => r.PRODUCTOR.Contains(nombre))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlProductor.DataSource = busqueda;
                gridViewProductor.Columns[0].Visible = false;
                gridViewProductor.Columns[1].Visible = false;
                gridViewProductor.Columns[2].Width = 90;
                gridViewProductor.Columns[3].Width = 150;
                gridViewProductor.Columns[4].Width = 100;
                gridViewProductor.Columns[5].Width = 100;
            }
        }

        public void BuscarCuit()
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
            var result = (
                    from a in dbContext.Productor
                    select new
                    {
                        ID = a.Id,
                        full = a.Fet + a.Nombre + a.Cuit,
                        FET = a.Fet,
                        PRODUCTOR = a.Nombre,
                        CUIT = a.Cuit,
                        PROVINCIA = a.Provincia
                    });
            var busqueda = result
                .Where(r => r.CUIT.Contains(cuit))
                .ToList();
            if (busqueda.Count > 0)
            {
                gridControlProductor.DataSource = busqueda;
                gridViewProductor.Columns[0].Visible = false;
                gridViewProductor.Columns[1].Visible = false;
                gridViewProductor.Columns[2].Width = 90;
                gridViewProductor.Columns[3].Width = 150;
                gridViewProductor.Columns[4].Width = 100;
                gridViewProductor.Columns[5].Width = 100;
            }
        }


        private void gridControlProductor_DoubleClick(object sender, EventArgs e)
        {
            if (target.Equals(DevConstantes.Preingreso))
            {
                IEnlace mienlace = this.Owner as Form_ProduccionPreingreso;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }
                this.Dispose();
            }
        }

    }
}