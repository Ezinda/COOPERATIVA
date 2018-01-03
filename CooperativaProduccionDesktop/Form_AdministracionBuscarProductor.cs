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

        private Guid _idSeleccionado;

        public Form_AdministracionBuscarProductor()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();

            _idSeleccionado = Guid.Empty;
        }

        public Guid IdProductorSeleccionado
        {
            get
            {
                return _idSeleccionado;
            }
        }

        public event EventHandler ProductorSeleccionado;

        private void OnProductorSeleccionado()
        {
            var handle = ProductorSeleccionado;

            if (handle != null)
            {
                handle(this, EventArgs.Empty);
            }
        }

        #region Method Code

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }
        }

        private void gridControlProductor_DoubleClick(object sender, EventArgs e)
        {
            var id = (Guid)gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID");
            var fet = gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET") as String;
            var productor = gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR") as String;

            _idSeleccionado = id;

            if (target.Equals(DevConstantes.Preingreso))
            {
                IEnlace mienlace = this.Owner as Form_RomaneoPreingreso;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }
                this.Dispose();
            }
            else if (target.Equals(DevConstantes.Pesada))
            {
                IEnlace mienlace = this.Owner as Form_RomaneoPesada;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }
                this.Dispose();
            }
            else if (target.Equals(DevConstantes.Liquidacion))
            {
                IEnlace mienlace = this.Owner as Form_AdministracionLiquidacion;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }
                this.Dispose();
            }
            else if (target.Equals(DevConstantes.Romaneo))
            {
                IEnlace mienlace = this.Owner as Form_RomaneoGestionRomaneo;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }
                this.Dispose();
            }
            else if (target.Equals(DevConstantes.Pesada))
            {
                IEnlace mienlace = this.Owner as Form_RomaneoPesada;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }
                this.Dispose();
            }
            else if (target.Equals(DevConstantes.OrdenPago))
            {
                //IEnlace mienlace = this.Owner as IEnlace;
                //if (mienlace != null)
                //{
                //    mienlace.Enviar(
                //        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                //        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                //        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                //}

                this.Dispose();
            }
            else if (target.Equals(DevConstantes.AsignacionTurno))
            {
                IEnlace mienlace = this.Owner as IEnlace;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "ID").ToString()),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "FET").ToString(),
                        gridViewProductor.GetRowCellValue(gridViewProductor.FocusedRowHandle, "PRODUCTOR").ToString());
                }

                this.Dispose();
            }
            else if (target.Equals(DevConstantes.BuscarProductorTurno))
            {
                IEnlace mienlace = this.Owner as IEnlace;
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

        #endregion

        #region Method dev

        public void Buscar()
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

        public int BuscarPorProductor()
        {
            int result = 0;

            using (var context = new CooperativaProduccionEntities())
            {
                var texto = this.fet ?? String.Empty;
                
                texto = texto.Trim();

                var list = context.Vw_Productor.Where(x => x.NOMBRE.Contains(texto)).ToList();
                var count = list.Count;

                if (count == 0)
                {
                    result = 0;

                    return result;
                }
                else if (count == 1)
                {
                    _idSeleccionado = list[0].ID.Value;

                    OnProductorSeleccionado();

                    result = 1;

                    return result;
                }
                else
                {
                    this.BuscarFet();

                    result = count;

                    return result;
                }
            }
        }

        public int BuscarPorFet()
        {
            int result = 0;

            using (var context = new CooperativaProduccionEntities())
            {
                var texto = this.fet ?? String.Empty;

                texto = texto.Trim();

                var list = context.Vw_Productor.Where(x => x.nrofet == texto).ToList();
                var count = list.Count;

                if (count == 0)
                {
                    result = 0;

                    return result;
                }
                else if (count == 1)
                {
                    _idSeleccionado = list[0].ID.Value;

                    OnProductorSeleccionado();

                    result = 1;

                    return result;
                }
                else
                {
                    this.BuscarFet();

                    result = count;

                    return result;
                }
            }
        }

        public void BuscarFet()
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
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

        #endregion
    }
}