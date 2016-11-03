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
    public partial class Form_RomaneoBuscarPreingreso : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public volatile string fet;
        public volatile string target;

        public Form_RomaneoBuscarPreingreso()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        #region Method Code

        private void gridControlPringreso_DoubleClick(object sender, EventArgs e)
        {
            if (target.Equals(DevConstantes.Pesada))
            {
                IEnlace mienlace = this.Owner as Form_RomaneoPesada;
                if (mienlace != null)
                {
                    mienlace.Enviar(
                        new Guid(gridViewPreingreso.GetRowCellValue(gridViewPreingreso.FocusedRowHandle, "ID").ToString()),
                        gridViewPreingreso.GetRowCellValue(gridViewPreingreso.FocusedRowHandle, "FET").ToString(),
                        gridViewPreingreso.GetRowCellValue(gridViewPreingreso.FocusedRowHandle, "PREINGRESO").ToString());
                }
                this.Dispose();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        #endregion

        #region Method Dev

        public void Buscar()
        {
            var result = (
                from a in Context.Vw_Preingreso
                .Where(x => x.Estado.Equals(true)
                    && x.Fecha >= dpDesde.Value
                    && x.Fecha <= dpHasta.Value)
                .OrderBy(x => x.Fecha)
                .ThenBy(x => x.Hora)
                select new
                {
                    ID = a.PreIngresoId,
                    FET = a.FET,
                    PRODUCTOR = a.Nombre,
                    CUIT = a.Cuit,
                    PROVINCIA = a.Provincia,
                    FECHA = a.Fecha,
                    HORA = a.Hora,
                    ESTADO = a.Estado.Equals(true) ? "PRENDIENTE" : "CERRADO",
                    PREINGRESO = a.NumeroPreingreso,
                    TRANSPORTE = a.Transporte,
                    CHOFER = a.Chofer,
                    PATENTE = a.Patente,
                    REMITO = a.NumRemito
                })
                .Where(x => x.ESTADO.Equals(true))
                .OrderBy(x => x.FECHA)
                .ThenBy(x => x.HORA)
                .ToList();
            if (result.Count() > 0)
            {
                gridControlPringreso.DataSource = result;
                gridViewPreingreso.Columns[0].Visible = false;
                gridViewPreingreso.Columns[1].Width = 90;
                gridViewPreingreso.Columns[2].Width = 90;
                gridViewPreingreso.Columns[3].Width = 150;
                gridViewPreingreso.Columns[4].Width = 100;
                gridViewPreingreso.Columns[5].Width = 100;
            }
        }

        public void BuscarFet()
        {
            var result = (
                from a in Context.Vw_Preingreso
                .Where(x => x.Estado == true
                    && x.FET.Contains(fet))
                .OrderBy(x => x.Fecha)
                .ThenBy(x => x.Hora)
                select new
                {
                    ID = a.PreIngresoId,
                    FET = a.FET,
                    PRODUCTOR = a.Nombre,
                    CUIT = a.Cuit,
                    PROVINCIA = a.Provincia,
                    FECHA = a.Fecha,
                    HORA = a.Hora,
                    ESTADO = a.Estado == true ? "PENDIENTE" : "CERRADO",
                    PREINGRESO = a.NumeroPreingreso,
                    TRANSPORTE = a.Transporte,
                    CHOFER = a.Chofer,
                    PATENTE = a.Patente,
                    REMITO = a.NumRemito
                })
                .OrderBy(x => x.FECHA)
                .ThenBy(x => x.HORA)
                .ToList();
            if (result.Count() > 0)
            {
                gridControlPringreso.DataSource = result;
                gridViewPreingreso.Columns[0].Visible = false;
                gridViewPreingreso.Columns[1].Width = 90;
                gridViewPreingreso.Columns[2].Width = 90;
                gridViewPreingreso.Columns[3].Width = 150;
                gridViewPreingreso.Columns[4].Width = 100;
                gridViewPreingreso.Columns[5].Width = 100;
            }
        }

        #endregion

     }
}