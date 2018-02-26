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
using CooperativaProduccion.Helpers.GridRecords;
using System.Linq.Expressions;
using Extensions;
using CooperativaProduccion.Reports;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoBuscarTurno : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace,IEnlaceActualizar
    {
        public CooperativaProduccionEntities Context { get; set; }
        public volatile string fet;
        public volatile string target;
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid _productorId;

        public Form_RomaneoBuscarTurno()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            _productorId = Guid.Empty;

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

            Expression<Func<Turno, bool>> pred = x => true;

            pred = pred.And(x => x.FechaTurno >= dpDesde.Value.Date 
                    && x.FechaTurno <= dpHasta.Value.Date);

            pred = _productorId != Guid.Empty ? pred.And(x => x.ProductorId == _productorId) : pred;
            
            var turnos = Context.Turno.Where(pred).ToList();
            
            var gridTurnos = new List<GridTurno>();
            
            foreach (var item in turnos)
            {

                var turno = new GridTurno();
                var productor = Context.Vw_Productor
                    .Where(x=>x.ID == item.ProductorId)
                    .FirstOrDefault();

                turno.Id = item.Id;
                turno.NOMBRE = productor.NOMBRE;
                turno.nrofet = productor.nrofet;
                turno.Provincia = productor.Provincia;
                turno.Telefono = productor.TELEFONO;
                turno.FechaSolicitud = item.FechaSolicitud.ToShortDateString();
                turno.FechaTurno = item.FechaTurno.ToShortDateString();
                turno.Kilos = item.Kilos.ToString("N2");
                
                gridTurnos.Add(turno);
            }
            
            if (gridTurnos.Count() > 0)
            {
                gridControlPringreso.DataSource = gridTurnos;
                gridViewPreingreso.Columns[0].Visible = false;
                gridViewPreingreso.Columns[1].Width = 90;
                gridViewPreingreso.Columns[2].Width = 90;
                gridViewPreingreso.Columns[3].Width = 90;
                gridViewPreingreso.Columns[4].Width = 250;
                gridViewPreingreso.Columns[5].Width = 120;
                gridViewPreingreso.Columns[6].Width = 120;
                gridViewPreingreso.Columns[7].Width = 120;
                gridViewPreingreso.Columns[8].Width = 120;
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
                    ESTADO = a.Estado == true ? 
                        DevConstantes.Pentiende : DevConstantes.Cerrado,
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
       
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            _productorId = Id;
            txtFet.Text = fet;
            txtProductor.Text = nombre;
        }
        private void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            var turno = new Form_RomaneoAsignacionTurno();
            turno.ShowDialog(this);
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }

            if (e.KeyChar == 8)
            {
                txtProductor.Text = string.Empty;
                _productorId = Guid.Empty;
            }
        }

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void txtProductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }

            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                _productorId = Guid.Empty;
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
                  PROVINCIA = a.Provincia,
                  TELEFONO = a.TELEFONO
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
                    _formBuscarProductor.target = DevConstantes.AsignacionTurno;
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
                        _productorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET;
                        txtProductor.Text = busqueda.PRODUCTOR;
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
                    _formBuscarProductor.nombre = txtProductor.Text;
                    _formBuscarProductor.target = DevConstantes.AsignacionTurno;
                    _formBuscarProductor.BuscarNombre();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtProductor.Text))
                      .FirstOrDefault();

                    if (busqueda != null)
                    {
                        _productorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET;
                        txtProductor.Text = busqueda.PRODUCTOR;
                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void Enviar(bool Enviar)
        {
            Buscar();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (gridViewPreingreso.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewPreingreso.DataRowCount; i++)
                {
                    if (gridViewPreingreso.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewPreingreso.GetRowCellValue(i, "Id").ToString());
                        ImprimirTurno(Id);
                    }
                }
            }
        }

        private void ImprimirTurno(Guid TurnoId)
        {
            var turno = Context.Turno
                .Where(x => x.Id == TurnoId)
                .FirstOrDefault();

            if (turno != null)
            {
                var reporte = new TurnoReport();

                var productor = Context.Vw_Productor
                    .Where(x => x.ID == turno.ProductorId)
                    .FirstOrDefault();

                reporte.Parameters["acopio"].Value = "ACOPIO CAMPAÑA " + DateTime.Now.Year;
                
                reporte.Parameters["productor"].Value = productor.NOMBRE;
                reporte.Parameters["fet"].Value = productor.nrofet;
                reporte.Parameters["dni"].Value = productor.CUIT;
                reporte.Parameters["domicilio"].Value = productor.DOMICILIO;
                reporte.Parameters["tel"].Value = productor.TELEFONO;

                reporte.Parameters["fechaSolicitud"].Value = turno.FechaSolicitud.ToShortDateString();
                reporte.Parameters["fechaTurno"].Value = turno.FechaTurno.ToShortDateString();
                reporte.Parameters["kg"].Value = turno.Kilos;

                using (ReportPrintTool tool = new ReportPrintTool(reporte))
                {
                    reporte.ShowPreviewMarginLines = false;
                    tool.PreviewForm.Text = "Etiqueta";
                    tool.ShowPreviewDialog();
                }
            }
        }

        private void btnReportePeriodos_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelTurnos";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelTurnos\" + Hora + " - ExcelTurnos.xls";

            gridControlPringreso.ExportToXls(fileName);
            StartProcess(fileName);
        }

        private void CreateIfMissing(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }

        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
            }
            catch
            {
                throw;
            }
        }
    }
}