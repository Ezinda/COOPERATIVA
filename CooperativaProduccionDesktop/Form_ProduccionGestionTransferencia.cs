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
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using EntityFramework.Extensions;

namespace CooperativaProduccion
{
    public partial class Form_ProduccionGestionTransferencia : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_ProduccionGestionTransferencia()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        private void CargarCombo()
        {
            var producto = 
                (from c in Context.Vw_TipoTabaco
                select new
                {
                    Id = c.id,
                    Descripcion = c.DESCRIPCION
                })
                .OrderBy(x => x.Descripcion)
                .ToList();

            cbProducto.DataSource = producto;
            cbProducto.DisplayMember = "Descripcion";
            cbProducto.ValueMember = "Id";

            var origen = Context.Vw_Deposito
                .Where(x=>x.id  ==  DevConstantes.ProduccionEnProceso)
                .ToList();
            cbDeposito.DataSource = origen;
            cbDeposito.DisplayMember = "nombre";
            cbDeposito.ValueMember = "ID";

            var destino = Context.Vw_Deposito
                .Where(x => x.id == DevConstantes.DepositoMateriaPrima)
                .ToList();
            cbDepositoDestino.DataSource = destino;
            cbDepositoDestino.DisplayMember = "nombre";
            cbDepositoDestino.ValueMember = "ID";

        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkTabaco.Checked)
            {
                Guid Tabaco = Guid.Parse(cbProducto.SelectedValue.ToString());

                var clase = Context.Vw_Clase
                .Where(x => x.ID_PRODUCTO == Tabaco
                    && x.Vigente == true)
                .ToList();

                cbClase.DataSource = clase;
                cbClase.DisplayMember = "Nombre";
                cbClase.ValueMember = "Id";
            }
        }

        private void checkTabaco_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkTabaco.Checked)
            {
                cbClase.DataSource = null;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            Expression<Func<Movimiento, bool>> pred = x => true;

            pred = pred.And(x => x.Fecha >= dpDesde.Value.Date);

            pred = pred.And(x => x.Fecha <= dpHasta.Value.Date);

            pred = pred.And(x => x.Actual == true);

            var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == cbDeposito.Text)
                .FirstOrDefault();

            pred = pred.And(x => x.DepositoId == deposito.id);

            Expression<Func<Vw_Pesada, bool>> pred2 = x => true;

            pred2 = checkTabaco.Checked ? pred2.And(x => x.DESCRIPCION == cbProducto.Text) : pred2;

            pred2 = checkClase.Checked ? pred2.And(x => x.Clase == cbClase.Text) : pred2;

            if(!string.IsNullOrEmpty(txtFardo.Text))
            {
                long fardo = long.Parse(txtFardo.Text);
                pred2 = pred2.And(x => x.NumFardo == fardo);
            }

            Expression<Func<Vw_Deposito, bool>> pred3 = x => true;

            pred3 = pred3.And(x => x.nombre == cbDeposito.Text);

            var movimientos =
                (from m in Context.Movimiento.Where(pred)
                 join p in Context.Vw_Pesada.Where(pred2)
                     on m.TransaccionId equals p.PesadaDetalleId
                 join d in Context.Vw_Deposito.Where(pred3)
                     on m.DepositoId equals d.id
                 select new
                 {
                     PesadaDetalleId = p.PesadaDetalleId,
                     Fardo = p.NumFardo,
                     Kilos = p.Kilos,
                     Clase = p.Clase,
                     Tabaco = p.DESCRIPCION,
                     DepositoId = d.id,
                     Deposito = d.nombre
                 })
                 .OrderBy(x=>x.Fardo)
                 .ToList();

            gridControlFardo.DataSource = movimientos;
            gridViewFardo.Columns[0].Visible = false;
            gridViewFardo.Columns[1].Width = 100;
            gridViewFardo.Columns[2].Width = 80;
            gridViewFardo.Columns[3].Width = 100;
            gridViewFardo.Columns[4].Width = 120;
            gridViewFardo.Columns[5].Visible = false;
            gridViewFardo.Columns[6].Width = 120;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelProduccionTransferencia";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelProduccionTransferencia\"
            + Hora + " - ExcelProduccionTransferencia.xls";

            gridControlFardo.ExportToXls(fileName);
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

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea realizar la transferencia de cajas"
                + " desde el deposito origen : "
                + cbDeposito.Text + " al deposito destino: " + cbDepositoDestino.Text + "?",
                       "Atención", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            Transferencia();
            Buscar();
        }

        private void Transferencia()
        {
            for (int i = 0; i <= gridViewFardo.RowCount - 1; i++)
            {
                Guid PesadaDetalleId = new Guid(gridViewFardo.GetRowCellValue(i, "PesadaDetalleId").ToString());
                Double Kilos = double.Parse(gridViewFardo.GetRowCellValue(i, "Kilos").ToString());
                Guid DepositoId = new Guid(gridViewFardo.GetRowCellValue(i, "DepositoId").ToString());

                var detalle = Context.Movimiento
                    .Where(x => x.TransaccionId == PesadaDetalleId
                        && x.DepositoId == DepositoId
                        && x.Actual == true)
                    .FirstOrDefault();

                UpdateMovimientoActual(detalle.TransaccionId.Value);
                RegistrarMovimientoEgreso(detalle.TransaccionId.Value, Kilos, DepositoId);
                RegistrarMovimientoIngreso(detalle.TransaccionId.Value, Kilos);
            }
        }

        private Guid RegistrarMovimientoEgreso(Guid Id,double Kilos,Guid DepositoId)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = DateTime.Now.Date;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Kg;
            movimiento.Ingreso = 0;
            movimiento.Egreso = Kilos;
            movimiento.DepositoId = DepositoId;
            movimiento.Actual = false;
            movimiento.Anulado = false;

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private Guid RegistrarMovimientoIngreso(Guid Id, double Kilos)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = DateTime.Now.Date;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Kg;
            movimiento.Ingreso = Kilos;
            movimiento.Egreso = 0;
            movimiento.Actual = true;
            movimiento.Anulado = false;

            var DepositoId = Guid.Parse(cbDepositoDestino.SelectedValue.ToString());

            movimiento.DepositoId = DepositoId;

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private void UpdateMovimientoActual(Guid Id)
        {
            var movimiento = Context.Movimiento
                 .Where(x => x.TransaccionId == Id)
                     .Update(x => new Movimiento() { Actual = false });

            Context.SaveChanges();
        }
    }
}