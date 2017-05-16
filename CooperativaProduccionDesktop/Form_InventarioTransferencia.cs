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
using EntityFramework.Extensions;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace CooperativaProduccion
{
    public partial class Form_InventarioTransferencia : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_InventarioTransferencia()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        private void CargarCombo()
        {
            var producto = Context.Vw_Producto.ToList();

            cbProductoIngreso.DataSource = producto;
            cbProductoIngreso.DisplayMember = "DESCRIPCION";
            cbProductoIngreso.ValueMember = "ID";
            
            var origen = Context.Vw_Deposito.ToList();
            cbDepositoOrigen.DataSource = origen;
            cbDepositoOrigen.DisplayMember = "nombre";
            cbDepositoOrigen.ValueMember = "ID";

            var destino = Context.Vw_Deposito.ToList();
            cbDepositoDestino.DataSource = destino;
            cbDepositoDestino.DisplayMember = "nombre";
            cbDepositoDestino.ValueMember = "ID";

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCajaConsulta(txtCantidadCaja.Text);
        }

        private void BuscarCajaConsulta(string cajas)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            var ProductoId = Guid.Parse(cbProductoIngreso.SelectedValue.ToString());

            var DepositoId = Guid.Parse(cbDepositoOrigen.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(cajas))
            {
                var cantidad = int.Parse(txtCantidadCaja.Text);

                var result =
                    (from c in Context.Caja
                        .Where(x => x.ProductoId == ProductoId 
                            && x.Fecha >= dpDesde.Value.Date 
                            && x.Fecha <= dpHasta.Value.Date)
                     join p in Context.Vw_Producto
                        on c.ProductoId equals p.ID
                     join ca in Context.Cata
                        on c.CataId equals ca.Id into cat
                     from joined in cat.DefaultIfEmpty()
                     join m in Context.Movimiento
                        .Where(x => x.Actual == true)
                        on c.Id equals m.TransaccionId
                     join d in Context.Vw_Deposito
                        .Where(x => x.id == DepositoId)
                        on m.DepositoId equals d.id
                     select new
                     {
                         Id = c.Id,
                         NumLote = c.LoteCaja,
                         NumCaja = c.NumeroCaja,
                         Producto = p.DESCRIPCION,
                         Bruto = c.Bruto,
                         Tara = c.Tara,
                         Neto = c.Neto,
                         Cata = joined.NumCata,
                         Fecha = c.Fecha,
                         DepositoId = m.DepositoId,
                         Deposito = d.nombre
                     })
                     .Take(cantidad)
                     .OrderBy(x => x.NumCaja)
                     .ToList();

                gridControlCaja.DataSource = result;
            }
            else
            {
                var result =
                    (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId
                         && x.Fecha >= dpDesde.Value.Date
                         && x.Fecha <= dpHasta.Value.Date)
                     join p in Context.Vw_Producto
                        on c.ProductoId equals p.ID
                     join ca in Context.Cata
                        on c.CataId equals ca.Id into cat
                     from joined in cat.DefaultIfEmpty()
                     join m in Context.Movimiento
                        .Where(x => x.Actual == true)
                        on c.Id equals m.TransaccionId
                     join d in Context.Vw_Deposito
                        .Where(x => x.id == DepositoId)
                        on m.DepositoId equals d.id
                     select new
                     {
                         Id = c.Id,
                         NumLote = c.LoteCaja,
                         NumCaja = c.NumeroCaja,
                         Producto = p.DESCRIPCION,
                         Bruto = c.Bruto,
                         Tara = c.Tara,
                         Neto = c.Neto,
                         Cata = joined.NumCata,
                         Fecha = c.Fecha,
                         DepositoId = m.DepositoId,
                         Deposito = d.nombre
                     })
                     .OrderBy(x => x.NumCaja)
                     .ToList();

                gridControlCaja.DataSource = result;
            }
            gridViewCaja.Columns[0].Visible = false;
            gridViewCaja.Columns[1].Caption = "N° Lote";
            gridViewCaja.Columns[1].Width = 110;
            gridViewCaja.Columns[2].Caption = "N° Caja";
            gridViewCaja.Columns[2].Width = 110;
            gridViewCaja.Columns[3].Caption = "Producto";
            gridViewCaja.Columns[3].Width = 100;
            gridViewCaja.Columns[4].Caption = "Bruto";
            gridViewCaja.Columns[4].Width = 100;
            gridViewCaja.Columns[5].Caption = "Tara";
            gridViewCaja.Columns[5].Width = 100;
            gridViewCaja.Columns[6].Caption = "Neto";
            gridViewCaja.Columns[6].Width = 100;
            gridViewCaja.Columns[7].Caption = "N° Cata";
            gridViewCaja.Columns[7].Width = 200;
            gridViewCaja.Columns[8].Visible = false;
            gridViewCaja.Columns[9].Visible = false;
            gridViewCaja.Columns[10].Caption = "Deposito";
            gridViewCaja.Columns[10].Width = 200;
        }

        private void btnGenerarLote_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea realizar la transferencia de cajas"
                + " desde el deposito origen : "
                + cbDepositoOrigen.Text + " al deposito destino: " + cbDepositoDestino.Text + "?",
                       "Atención", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }

            Transferencia();
            BuscarCajaConsulta(txtCantidadCaja.Text);
        }

        private void Transferencia()
        {
            for (int i = 0; i <= gridViewCaja.RowCount -1; i++)
            {
                if (gridViewCaja.IsRowSelected(i))
                {
                    Guid CajaId = new Guid(gridViewCaja.GetRowCellValue(i, "Id").ToString());

                    var ingreso = Context.Movimiento.Where(x => x.TransaccionId == CajaId).Sum(x => x.Ingreso == null ? 0 : x.Ingreso).Value;

                    var egreso = Context.Movimiento.Where(x => x.TransaccionId == CajaId).Sum(x => x.Egreso == null ? 0 : x.Egreso).Value;

                    if (ingreso - egreso > 0)
                    {
                        var caja = Context.Caja.Where(x => x.Id == CajaId).FirstOrDefault();

                        UpdateMovimientoActual(caja.Id);
                        RegistrarMovimientoEgreso(caja.Id);
                        RegistrarMovimientoIngreso(caja.Id);
                    }
                }
            }
        }

        private void UpdateMovimientoActual(Guid Id)
        {
            var movimiento = Context.Movimiento
                 .Where(x => x.TransaccionId == Id)
                     .Update(x => new Movimiento() { Actual = false });

            Context.SaveChanges();
        }

        private Guid RegistrarMovimientoEgreso(Guid Id)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = dpDesde.Value.Date;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Caja;
            movimiento.Ingreso = 0;
            movimiento.Egreso = 1;
            movimiento.Actual = false;
            movimiento.Anulado = false;

            var DepositoId = Guid.Parse(cbDepositoOrigen.SelectedValue.ToString());

            movimiento.DepositoId = DepositoId;
            
            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private Guid RegistrarMovimientoIngreso(Guid Id)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = dpDesde.Value.Date;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Caja;
            movimiento.Ingreso = 1;
            movimiento.Egreso = 0;
            movimiento.Actual = true;
            movimiento.Anulado = false;

            var DepositoId = Guid.Parse(cbDepositoDestino.SelectedValue.ToString());

            movimiento.DepositoId = DepositoId;

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelInventarioTransferencia";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelInventarioTransferencia\"
                + Hora + " - ExcelInventarioTransferencia.xls";

            gridControlCaja.ExportToXls(fileName);
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