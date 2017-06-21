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
using System.Data.Entity;
using System.Globalization;
using System.Linq.Expressions;
using Extensions;
using CooperativaProduccion.Helpers;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using EntityFramework.Extensions;
using System.Threading.Tasks;

namespace CooperativaProduccion
{
    public partial class Form_InventarioIngresoCaja : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ProductoId;
        private long LoteCaja;
        private string printerTicket;

        public Form_InventarioIngresoCaja()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar();
        }

        private void Iniciar()
        {
            dpIngresoCaja.Focus();
            Habilitar();

            if (IsDebug().Equals(false))
            {
                string strFileConfig = @"Config.ini";
                IniParser parser = new IniParser(strFileConfig);
                printerTicket = parser.GetSetting("AppSettings", "PrinterTicketCaja");
            }
        }

        private void CargarCombo()
        {

            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            var producto = Context.Vw_Producto.ToList();

            cbProductoIngreso.DataSource = producto;
            cbProductoIngreso.DisplayMember = "DESCRIPCION";
            cbProductoIngreso.ValueMember = "ID";

            cbProductoConsulta.DataSource = producto;
            cbProductoConsulta.DisplayMember = "DESCRIPCION";
            cbProductoConsulta.ValueMember = "ID";

            var campaña =
               (from c in Context.Caja
                group new { c } by new
                {
                    Campaña = c.Campaña
                } into g
                select new
                {
                    Campaña = g.Key.Campaña
                })
                .OrderBy(x => x.Campaña)
                .ToList();

            cbCampaña.DataSource = campaña;
            cbCampaña.DisplayMember = "Campaña";
            cbCampaña.ValueMember = "Campaña";
        }

        private void dpIngresoCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbProductoIngreso.Focus();
            }
        }

        private void cbProductoIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBruto.Focus();
            }
        }

        private void Habilitar()
        {
            txtBruto.Enabled = true;
            txtBruto.Text = "0.00";
            txtNeto.Enabled = true;
            txtNeto.Text = "0.00";
            txtTara.Enabled = true;
            txtTara.Text = "0.00";
            txtCantidadCajaIngreso.Enabled = true;
            txtCantidadCajaIngreso.Text = "0";
        }

        private void txtBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtBruto.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtTara.Focus();
            }
        }

        private void txtTara_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtTara.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtNeto.Focus();
            }
        }

        private void txtNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtNeto.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtCantidadCajaIngreso.Focus();
            }
        }

        private void txtCantidadCajaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtCantidadCajaIngreso.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnGenerarLote.Focus();
            }
        }

        private void btnGenerarLote_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea generar el ingreso de cajas?",
                 "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            GenerarLoteCajas();
            BuscarCaja(LoteCaja);
        }

        private void GenerarLoteCajas()
        {
            if (txtBruto.Text != string.Empty && txtTara.Text != string.Empty
                && txtNeto.Text != string.Empty)
            {
                int cantidad = 0;
                bool cant = int.TryParse(txtCantidadCajaIngreso.Text, out cantidad);
                if (cant && cantidad > 0)
                {
                    var producto = Context.Vw_Producto
                                .Where(x => x.DESCRIPCION == cbProductoIngreso.Text)
                                .FirstOrDefault();
                    ProductoId = producto.ID;
                    try
                    {
                        Task task = new Task(() => TransferenciaProduccionDeposito(dpIngresoCaja.Value.Date, producto.ID));
                        task.Start();

                        LoteCaja = ContadorNumeroLote(dpIngresoCaja.Value.Year, ProductoId);
                        for (int i = 0; i < cantidad; i++)
                        {
                            Caja caja;
                            caja = new Caja();
                            caja.Id = Guid.NewGuid();
                            caja.NumeroCaja = ContadorNumeroCaja(dpIngresoCaja.Value.Year, ProductoId);
                            caja.LoteCaja = LoteCaja;
                            caja.Campaña = dpIngresoCaja.Value.Year;
                            caja.Fecha = dpIngresoCaja.Value.Date;
                            caja.Hora = DateTime.Now.TimeOfDay;
                            caja.ProductoId = producto.ID;
                            caja.Bruto = decimal.Parse(txtBruto.Text, CultureInfo.InvariantCulture);
                            caja.Tara = decimal.Parse(txtTara.Text, CultureInfo.InvariantCulture);
                            caja.Neto = decimal.Parse(txtNeto.Text, CultureInfo.InvariantCulture);
                            Context.Caja.Add(caja);
                            Context.SaveChanges();
                            RegistrarMovimientoIngreso(caja.Id, 1, caja.Fecha);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        private void BuscarCaja(long Lote)
        {
            var result =
                (from c in Context.Caja
                     .Where(x => x.LoteCaja == Lote)
                 join p in Context.Vw_Producto
                 on c.ProductoId equals p.ID into pr
                 from cp in pr.DefaultIfEmpty()
                 join ca in Context.Cata
                 on c.CataId equals ca.Id into cat
                 from joined in cat.DefaultIfEmpty()
                 select new
                 {
                     Id = c.Id,
                     NumLote = c.LoteCaja,
                     NumCaja = c.NumeroCaja,
                     Producto = cp.DESCRIPCION,
                     Tara = c.Tara,
                     Neto = c.Neto,
                     Bruto = c.Bruto,
                     Cata = joined.NumCata,
                     Fecha = c.Fecha
                 })
                .OrderBy(x => x.NumCaja)
                .ToList();

            gridControlCaja.DataSource = result;
            gridViewCaja.Columns[0].Visible = false;
            gridViewCaja.Columns[1].Caption = "N° Lote";
            gridViewCaja.Columns[1].Width = 110;
            gridViewCaja.Columns[2].Caption = "N° Caja";
            gridViewCaja.Columns[2].Width = 110;
            gridViewCaja.Columns[3].Caption = "Producto";
            gridViewCaja.Columns[3].Width = 100;
            gridViewCaja.Columns[4].Caption = "Tara";
            gridViewCaja.Columns[4].Width = 100;
            gridViewCaja.Columns[5].Caption = "Neto";
            gridViewCaja.Columns[5].Width = 100;
            gridViewCaja.Columns[6].Caption = "Bruto";
            gridViewCaja.Columns[6].Width = 100;
            gridViewCaja.Columns[7].Caption = "N° Cata";
            gridViewCaja.Columns[7].Width = 200;
            gridViewCaja.Columns[8].Visible = false;
        }

        private long ContadorNumeroCaja(int campaña, Guid ProductoId)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            long numCaja = 0;
            var caja = Context.Caja
                .Where(x => x.Campaña == campaña
                    && x.ProductoId == ProductoId)
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            if (caja != null)
            {
                numCaja = caja.NumeroCaja + 1;
            }
            else
            {
                numCaja = 1;
            }
            return numCaja;
        }

        private long ContadorNumeroLote(int campaña, Guid ProductoId)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            long numLote = 0;
            var caja = Context.Caja
                .Where(x => x.Campaña == campaña
                    && x.ProductoId == ProductoId)
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            if (caja != null)
            {
                numLote = caja.LoteCaja + 1;
            }
            else
            {
                numLote = 1;
            }
            return numLote;
        }

        private void TransferenciaProduccionDeposito(DateTime Fecha, Guid ProductoId)
        {
            var fardos =
               (from f in Context.FardoEnProduccion
                .Where(x => x.ProductoId == ProductoId
                    && x.Fecha <= Fecha)
                join m in Context.Movimiento
                    .Where(x => x.Fecha <= Fecha
                        && x.DepositoId == DevConstantes.ProduccionEnProceso)
                on f.PesadaDetalleId equals m.TransaccionId
                group new { f, m } by new
                {
                    PesadaDetalleId = f.PesadaDetalleId,
                    Kilos = f.Kilos,
                    Fecha = f.Fecha,

                } into g
                select new FardosEgreso
                {
                    PesadaDetalleId = g.Key.PesadaDetalleId,
                    Kilos = g.Key.Kilos,
                    Fecha = g.Key.Fecha,
                    Egreso = g.Sum(c => c.m.Egreso)
                })
                .Where(x=>x.Egreso <= 0)
                .ToList();

            RegistrarMovimientoEgreso(fardos);
         
        }

        private void RegistrarMovimientoEgreso(List<FardosEgreso> fardos)
        {
            List<Movimiento> list = new List<Movimiento>();

            foreach (var item in fardos)
            {
                UpdateMovimientoActual(item.PesadaDetalleId);

                Movimiento movimiento;

                movimiento = new Movimiento();
                movimiento.Id = Guid.NewGuid();
                movimiento.Fecha = item.Fecha;
                movimiento.TransaccionId = item.PesadaDetalleId;
                movimiento.Documento = DevConstantes.Transferencia;
                movimiento.Unidad = DevConstantes.Kg;
                movimiento.Ingreso = 0;
                movimiento.Egreso = item.Kilos;
                movimiento.Actual = false;
                movimiento.Anulado = false;

                var deposito = Context.Vw_Deposito
                    .Where(x => x.id == DevConstantes.ProduccionEnProceso)
                    .FirstOrDefault();

                movimiento.DepositoId = deposito.id;

                list.Add(movimiento);
            }

            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            Context.Movimiento.AddRange(list);
            Context.SaveChanges();
        }

        private void UpdateMovimientoActual(Guid Id)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;

            var movimiento = Context.Movimiento
                 .Where(x => x.TransaccionId == Id
                     && x.DepositoId != DevConstantes.DepositoCaja)
                     .Update(x => new Movimiento() { Actual = false });

            Context.SaveChanges();
        }

        private void RegistrarMovimientoIngreso(Guid Id, double kilos, DateTime fecha)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = fecha;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Caja;
            movimiento.Ingreso = kilos;
            movimiento.Egreso = 0;
            movimiento.Actual = true;
            movimiento.Anulado = false;

            var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.Deposito)
                .FirstOrDefault();

            if (deposito != null)
            {
                movimiento.DepositoId = deposito.id;
            }

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();
        }

        private void cbProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidadCajaConsulta.Focus();
        }

        private void txtCantidadCajaConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtCantidadCajaConsulta.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnBuscarCaja.Focus();
            }
        }

        private void btnBuscarCaja_Click(object sender, EventArgs e)
        {
            if (ValidarConsulta())
            {
                BuscarCajaConsulta(txtCantidadCajaConsulta.Text);
            }
        }

        private bool ValidarConsulta()
        {
            if (cbProductoConsulta.Text == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un producto",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void BuscarCajaConsulta(string cajas)
        {
            int Campaña = int.Parse(cbCampaña.Text);
            var ProductoId = Guid.Parse(cbProductoConsulta.SelectedValue.ToString());

            Expression<Func<Caja, bool>> pred = x => true;

            pred = checkCampaña.Checked ?
                pred.And(x => x.Campaña == Campaña) : pred;

            pred = pred.And(x => x.ProductoId == ProductoId);

            pred = checkSinCata.Checked ? pred.And(x => x.CataId == null) : pred;

            if (!string.IsNullOrEmpty(cajas))
            {
                var cantidad = int.Parse(txtCantidadCajaConsulta.Text);

                var result =
                    (from c in Context.Caja
                         .Where(pred)
                     join p in Context.Vw_Producto
                         on c.ProductoId equals p.ID into pr
                     from cp in pr.DefaultIfEmpty()
                     join ca in Context.Cata
                         on c.CataId equals ca.Id into cat
                     from joined in cat.DefaultIfEmpty()
                     select new
                     {
                         Id = c.Id,
                         Campaña = c.Campaña,
                         NumLote = c.LoteCaja,
                         NumCaja = c.NumeroCaja,
                         Producto = cp.DESCRIPCION,
                         Tara = c.Tara,
                         Neto = c.Neto,
                         Bruto = c.Bruto,
                         Cata = joined.NumCata
                     })
                     .Take(cantidad)
                     .OrderBy(x => x.Campaña)
                     .ThenBy(x => x.NumCaja)
                     .ToList();

                gridControlCajaConsulta.DataSource = result;
            }
            else
            {
                var result =
                  (from c in Context.Caja
                       .Where(pred)
                   join p in Context.Vw_Producto
                        on c.ProductoId equals p.ID into pr
                   from cp in pr.DefaultIfEmpty()
                   join ca in Context.Cata
                        on c.CataId equals ca.Id into cat
                   from joined in cat.DefaultIfEmpty()
                   select new
                   {
                       Id = c.Id,
                       Campaña = c.Campaña,
                       NumLote = c.LoteCaja,
                       NumCaja = c.NumeroCaja,
                       Producto = cp.DESCRIPCION,
                       Tara = c.Tara,
                       Neto = c.Neto,
                       Bruto = c.Bruto,
                       Cata = joined.NumCata
                   })
                   .OrderBy(x => x.Campaña)
                   .ThenBy(x => x.NumCaja)
                   .ToList();

                gridControlCajaConsulta.DataSource = result;
            }

            gridViewCajaConsulta.Columns[0].Visible = false;
            gridViewCajaConsulta.Columns[1].Caption = "Campaña";
            gridViewCajaConsulta.Columns[1].Width = 90;
            gridViewCajaConsulta.Columns[2].Caption = "N° Lote";
            gridViewCajaConsulta.Columns[2].Width = 110;
            gridViewCajaConsulta.Columns[3].Caption = "N° Caja";
            gridViewCajaConsulta.Columns[3].Width = 110;
            gridViewCajaConsulta.Columns[4].Caption = "Producto";
            gridViewCajaConsulta.Columns[4].Width = 100;
            gridViewCajaConsulta.Columns[5].Caption = "Tara";
            gridViewCajaConsulta.Columns[5].Width = 100;
            gridViewCajaConsulta.Columns[6].Caption = "Neto";
            gridViewCajaConsulta.Columns[6].Width = 100;
            gridViewCajaConsulta.Columns[7].Caption = "Bruto";
            gridViewCajaConsulta.Columns[7].Width = 100;
            gridViewCajaConsulta.Columns[8].Caption = "N° Cata";
            gridViewCajaConsulta.Columns[8].Width = 200;


            for (var i = 0; i <= gridViewCajaConsulta.RowCount; i++)
            {
                gridViewCajaConsulta.SelectRow(i);
            }

            foreach (GridColumn column in gridViewCajaConsulta.Columns)
            {
                GridSummaryItem item = column.SummaryItem;
                if (item != null)
                    column.Summary.Remove(item);
            }
            gridViewCajaConsulta.Columns["NumCaja"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "NumCaja", "Total Cajas : {0}");
            gridViewCajaConsulta.Appearance.FooterPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewCajaConsulta.Appearance.FooterPanel.Options.UseTextOptions = true;
        }

        private void Cata_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            CargarCombo();
        }

        private void btnImpimirEtiqueta_Click(object sender, EventArgs e)
        {
            if (IsDebug().Equals(false))
            {
                if (gridViewCajaConsulta.SelectedRowsCount > 0)
                {
                    for (int i = 0; i < gridViewCajaConsulta.DataRowCount; i++)
                    {
                        if (gridViewCajaConsulta.IsRowSelected(i))
                        {
                            string Caja = gridViewCajaConsulta.GetRowCellValue(i, "NumCaja") != null ?
                                gridViewCajaConsulta.GetRowCellValue(i, "NumCaja").ToString() : string.Empty;
                            string Producto = gridViewCajaConsulta.GetRowCellValue(i, "Producto") != null ?
                                gridViewCajaConsulta.GetRowCellValue(i, "Producto").ToString() : string.Empty;
                            string Neto = gridViewCajaConsulta.GetRowCellValue(i, "Neto") != null ?
                                gridViewCajaConsulta.GetRowCellValue(i, "Neto").ToString() : string.Empty;
                            string Cata = gridViewCajaConsulta.GetRowCellValue(i, "Cata") != null ?
                                gridViewCajaConsulta.GetRowCellValue(i, "Cata").ToString() : string.Empty;
                            PrintTicket(Caja, Producto, Neto, Cata);
                        }
                    }
                }
            }
        }

        private void PrintTicket(string caja, string producto, string peso, string cata)
        {
            try
            {
                string impresionFechaHora = DateTime.Now.ToString();
                string s = "^XA";
                s = s + "^FX Top section with company logo, name and address.";
                s = s + "^LH25,50";
                s = s + "^PW900";
                s = s + "^CF0,40";
                s = s + "^FO120,50^FDCOOPERATIVA DE PRODUCTORES^FS";
                s = s + "^CF0,40";
                s = s + "^FO125,85^FDAGROPECUARIOS DEL TUCUMAN^FS";
                s = s + "^FO310,120^FDLTDA.^FS";
                s = s + "^CF0,30";
                s = s + "^FO180,160^FDRUTA 38 KM 699-LA INVERNADA^FS";
                s = s + "^FO260,190^FDDPTO. LA COCHA^FS";
                s = s + "^FX Third section with barcode.";
                s = s + "^CF0,35";
                s = s + "^FO120,230^FDCaja             Producto            Peso Neto  ^FS";
                s = s + "^CFA,25";
                s = s + "^FO120,265^FD" + caja + "   " + producto + "   " + peso + " ^FS";
                s = s + "^FO150,310^FDC.A.T.A. " + cata + " ^FS";
                s = s + "^FO150,350^FD" + impresionFechaHora + " ^FS";
                s = s + "^CF0,40";
                s = s + "^FO120,450^FDCOOPERATIVA DE PRODUCTORES^FS";
                s = s + "^CF0,40";
                s = s + "^FO120,480^FDAGROPECUARIOS DEL TUCUMAN^FS";
                s = s + "^FO300,515^FDLTDA.^FS";
                s = s + "^CF0,30";
                s = s + "^BY3,2,270";
                s = s + "^FO160,550^BC^FD " + cata + " ^FS";
                s = s + "^XZ";

                if (printerTicket != null)
                {
                    RawPrinterHelper.SendStringToPrinter(printerTicket, s);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en el módulo de impresión :", ex);
            }
        }

        private bool IsDebug()
        {
            var debug = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.Debug)
              .FirstOrDefault();

            return debug.Valor;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelCaja";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelCaja\" + Hora + " - ExcelCaja.xls";

            gridControlCajaConsulta.ExportToXls(fileName);
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

    internal class FardosEgreso
    {
        public Guid PesadaDetalleId { get; set; }
        public double? Kilos { get; set; }
        public DateTime Fecha { get; set; }
        public double? Egreso { get; set; }
    }
}