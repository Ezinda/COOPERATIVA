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
    public partial class Form_ProduccionBuscarFardo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_ProduccionBuscarFardo()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        #region Method dev

        public void BuscarFardo(Guid TabacoId, string Clase, double Kilos)
        { 
            
            //var query = from p in Programs
            //            join pl in ProgramLocations
            //                on p.ProgramID equals pl.ProgramID into pp
            //            from pl in pp.DefaultIfEmpty()
            //            where pl == null
            //            select p;

        var result =
                (from pd in Context.PesadaDetalle
                    .Where(x => x.Kilos == Kilos)
                 join c in Context.Vw_Clase
                     .Where(x => x.ID_PRODUCTO == TabacoId
                         && x.NOMBRE == Clase)
                 on pd.ClaseId equals c.ID
                 join f in Context.FardoEnProduccion
                 on pd.Id equals f.PesadaDetalleId into pp
                 from pl in pp.DefaultIfEmpty()
                 where pl == null
                 select new
                 {
                     full = c.DESCRIPCION + c.NOMBRE + pd.Kilos + pd.NumFardo,
                     Id = pd.Id,
                     NumFardo = pd.NumFardo,
                     Kg = pd.Kilos,
                     ClaseId = pd.ClaseId,
                     Clase = c.NOMBRE,
                     Tabaco = c.DESCRIPCION
                 })
                 .OrderBy(x => x.NumFardo)
                 .ToList();

            gridControlFardos.DataSource = result;
            gridViewFardos.Columns[0].Visible = false;
            gridViewFardos.Columns[1].Visible = false;
            gridViewFardos.Columns[4].Visible = false;
        }

        public void Buscar()
        {
            var result =
                (from pd in Context.PesadaDetalle
                 join c in Context.Vw_Clase
                 on pd.ClaseId equals c.ID
                 join f in Context.FardoEnProduccion
                    on pd.Id equals f.PesadaDetalleId into pp
                 from pl in pp.DefaultIfEmpty()
                 where pl == null
                 select new
                 {
                     full = c.DESCRIPCION + c.NOMBRE + pd.Kilos + pd.NumFardo,
                     Id = pd.Id,
                     NumFardo = pd.NumFardo,
                     Kg = pd.Kilos,
                     ClaseId = pd.ClaseId,
                     Clase = c.NOMBRE,
                     Tabaco = c.DESCRIPCION
                 })
                 .OrderBy(x => x.NumFardo)
                 .ToList();

            var busqueda = result
                .Where(r => r.full.Contains(txtBusqueda.Text))
                .ToList();

            gridControlFardos.DataSource = busqueda;
            gridViewFardos.Columns[0].Visible = false;
            gridViewFardos.Columns[1].Visible = false;
            gridViewFardos.Columns[4].Visible = false;
        }

        #endregion

        #region Method Code
        
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
            IEnlace mienlace = this.Owner as Form_ProduccionTransferenciaMateriaPrima;

            if (mienlace != null)
            {
                mienlace.Enviar(
                    new Guid(gridViewFardos.GetRowCellValue(gridViewFardos.FocusedRowHandle, "Id").ToString()),
                    gridViewFardos.GetRowCellValue(gridViewFardos.FocusedRowHandle, "NumFardo").ToString(),
                    gridViewFardos.GetRowCellValue(gridViewFardos.FocusedRowHandle, "Kg").ToString());
            }

            Dispose();
        }

        #endregion
    }
}