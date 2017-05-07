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
using CooperativaProduccion.Helpers;
using System.Linq.Expressions;
using Extensions;
using System.Diagnostics;
using System.IO;
using EntityFramework.Extensions;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionGestionCaja : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private long LoteCaja;
        private string printerTicket;

        public Form_AdministracionGestionCaja()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar();
        }

        #region Ingresar Caja

        #region Method Code

        private void btnGenerarLote_Click(object sender, EventArgs e)
        {
            if (Validar(false))
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
        }

        private void dpIngresoCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbProductoIngreso.Focus();
            }
        }

        private void cbProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBruto.Focus();
            }
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
                txtCantidadCajaIngreso.Focus();
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
                txtBruto.Focus();
            }
        }

        private void txtCantidadCaja_KeyPress(object sender, KeyPressEventArgs e)
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
    
        #endregion

        #region Method Dev
       
        private void GenerarLoteCajas()
        {
            if (txtBruto.Text != string.Empty && txtTara.Text != string.Empty 
                && txtNeto.Text != string.Empty)
            {
                try
                {
                    var cborden = cbOrden.SelectedItem as dynamic;
                    Guid OrdenVentaId = cborden.OrdenId;
                    var cbproducto = cbProductoIngreso.SelectedItem as dynamic;
                    Guid ProductoId = cbproducto.ID;
                    int Campaña = dpIngresoCaja.Value.Year;
                    LoteCaja = ContadorNumeroLote(Campaña,ProductoId);
                    for (int i = 0; i < int.Parse(txtCantidadCajaIngreso.Text); i++)
                    {
                        Caja caja;
                        caja = new Caja();
                        caja.Id = Guid.NewGuid();
                        caja.LoteCaja = LoteCaja;
                        caja.NumeroCaja = ContadorNumeroCaja(Campaña, ProductoId);
                        caja.Fecha = dpIngresoCaja.Value.Date;
                        caja.Hora = DateTime.Now.TimeOfDay;
                        caja.Campaña = Campaña;
                        caja.ProductoId = ProductoId;
                        caja.OrdenVentaId = OrdenVentaId;
                        caja.Bruto = decimal.Parse(txtBruto.Text, CultureInfo.InvariantCulture);
                        caja.Tara = decimal.Parse(txtTara.Text, CultureInfo.InvariantCulture);
                        caja.Neto = decimal.Parse(txtNeto.Text, CultureInfo.InvariantCulture);

                        if (checkCata.Checked)
                        {
                            var cata = Context.Cata
                                .Where(x => x.NumCaja == null);

                            if (cata != null)
                            {
                                caja.CataId = cata.FirstOrDefault().Id;
                                Context.Caja.Add(caja);
                                Context.SaveChanges();

                                var NumCata = Context.Cata.Find(caja.CataId);
                                NumCata.NumCaja = caja.NumeroCaja;
                                NumCata.CajaId = caja.Id;
                                NumCata.OrdenVentaId = caja.OrdenVentaId;

                                var ov = Context.OrdenVenta
                                    .Where(x => x.Id == caja.OrdenVentaId)
                                    .FirstOrDefault();

                                if (ov != null)
                                {
                                    NumCata.NumOrden = ov.NumOrden;
                                }
                                Context.Entry(NumCata).State = EntityState.Modified;
                                Context.SaveChanges();
                            }
                        }
                       
                        RegistrarMovimiento(caja.Id, 1, caja.Fecha);
                    }

                    #region Detalle
                    OrdenVentaDetalle detalle;
                    detalle = new OrdenVentaDetalle();
                    detalle.Id = Guid.NewGuid();
                    detalle.ProductoId = ProductoId;
                    detalle.OrdenVentaId = OrdenVentaId;
                    detalle.DesdeCaja = CajaDesde(OrdenVentaId, ProductoId, Campaña);
                    detalle.HastaCaja = CajaHasta(OrdenVentaId, ProductoId, Campaña);
                    detalle.Campaña = Campaña;
                    Context.OrdenVentaDetalle.Add(detalle);
                    Context.SaveChanges();
                    #endregion
                }
                catch
                {
                    throw;
                }
            }
        }
        
        private Guid RegistrarMovimiento(Guid Id, double kilos, DateTime fecha)
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

            var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.Deposito)
                .FirstOrDefault();

            if (deposito != null)
            {
                movimiento.DepositoId = deposito.id;
            }

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private bool Validar(bool Consulta)
        {
            var cata = Context.Cata
                        .Where(x => x.NumCaja == null)
                        .Count();

            if (Consulta.Equals(false))
            {
                if (checkCata.Checked)
                {
                    if (cata < int.Parse(txtCantidadCajaIngreso.Text))
                    {
                        MessageBox.Show("La cantidad de Nro. de CATA disponible es " +
                            "insuficiente para la cantidad de cajas ingresadas",
                            "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else
            {
                int count = 0;
                for (int i = 0; i <= gridViewCajaConsulta.RowCount; i++)
                {
                    if (gridViewCajaConsulta.GetRowCellValue(i, "Cata") == null)
                    {
                        count = count + 1;
                    }
                }

                if (cata < count)
                {
                    MessageBox.Show("La cantidad de Nro. de CATA disponible es " +
                        "insuficiente para la cantidad de cajas ingresadas",
                        "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void CargarCombo()
        {
            var ordenVenta =
                (from o in Context.OrdenVenta 
                 join c in Context.Vw_Cliente
                 on o.ClienteId equals c.ID
                 select new 
                 {
                     OrdenId = o.Id,
                     Descripcion = o.NumOrden + " - " + c.RAZONSOCIAL + " - " + o.Fecha.Year,
                 })
                .OrderBy(x => x.Descripcion)
                .ToList();

            cbOrden.DataSource = ordenVenta;
            cbOrden.DisplayMember = "Descripcion";
            cbOrden.ValueMember = "OrdenId";

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

            var producto = Context.Vw_Producto.ToList();
            cbProductoIngreso.DataSource = producto;
            cbProductoIngreso.DisplayMember = "DESCRIPCION";
            cbProductoIngreso.ValueMember = "ID";

            cbProductoConsulta.DataSource = producto;
            cbProductoConsulta.DisplayMember = "DESCRIPCION";
            cbProductoConsulta.ValueMember = "ID";

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

        private void Deshabilitar()
        {
            txtBruto.Enabled = false;
            txtNeto.Enabled = false;
            txtTara.Enabled = false;
            txtCantidadCajaIngreso.Enabled = false;
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

        private long ContadorNumeroCaja(int campaña,Guid ProductoId)
        {
            long numFardo = 0;
            var caja = Context.Caja
                .Where(x => x.Campaña == campaña 
                    && x.ProductoId == ProductoId)
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            if (caja != null)
            {
                numFardo = caja.NumeroCaja + 1;
            }
            else
            {
                numFardo = 1;
            }
            return numFardo;
        }

        private long ContadorNumeroLote(int campaña,Guid ProductoId)
        {
            long numFardo = 0;
            var caja = Context.Caja
                .Where(x => x.Campaña == campaña
                    && x.ProductoId == ProductoId)
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            if (caja != null)
            {
                numFardo = caja.LoteCaja + 1;
            }
            else
            {
                numFardo = 1;
            }
            return numFardo;
        }

        private long CajaDesde(Guid OrdenVentaId,Guid ProductoId, int Campaña)
        {
            var deposito = Context.Vw_Deposito
               .Where(x => x.nombre == DevConstantes.Warrants)
               .Single();

            var cajaDesde =
                (from c in Context.Caja
                    .Where(x => x.ProductoId == ProductoId
                        && x.Campaña == Campaña
                        && x.OrdenVentaId == OrdenVentaId)
                 join m in Context.Movimiento
                    .Where(x => x.DepositoId != deposito.id)
                    on c.Id equals m.TransaccionId
                 select new
                 {
                     NumeroCaja = c.NumeroCaja
                 })
                 .OrderBy(x => x.NumeroCaja)
                 .FirstOrDefault();

            return cajaDesde.NumeroCaja;
        }

        private long CajaHasta(Guid OrdenVentaId, Guid ProductoId, int Campaña)
        {
            var deposito = Context.Vw_Deposito
               .Where(x => x.nombre == DevConstantes.Warrants)
               .Single();

            var cajaDesde =
                (from c in Context.Caja
                    .Where(x => x.ProductoId == ProductoId
                        && x.Campaña == Campaña
                        && x.OrdenVentaId == OrdenVentaId)
                 join m in Context.Movimiento
                    .Where(x => x.DepositoId != deposito.id)
                    on c.Id equals m.TransaccionId
                 select new
                 {
                     NumeroCaja = c.NumeroCaja
                 })
                 .OrderByDescending(x => x.NumeroCaja)
                 .FirstOrDefault();

            return cajaDesde.NumeroCaja;
        }

        #endregion

        #endregion

        #region Consultar Caja

        #region Method Code

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

        private void cbProductoConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCantidadCajaConsulta.Focus();
            }
        }

        private void cbProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidadCajaConsulta.Focus();
        }
        
        private void btnAsignarCata_Click(object sender, EventArgs e)
        {
            if (Validar(true))
            {
                var resultado = MessageBox.Show("¿Desea asignar número de cata a las cajas?",
                     "Atención", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                AsignarCata();
                BuscarCajaConsulta(txtCantidadCajaConsulta.Text);
            }
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
 
        #endregion

        #region Method Dev

        private void BuscarCajaConsulta(string cajas)
        {
            int Campaña = int.Parse(cbCampaña.Text);
            var ProductoId = Guid.Parse(cbProductoConsulta.SelectedValue.ToString());

            Expression<Func<Caja, bool>> pred = x => true;

            pred = !(checkCampaña.Checked) ?
                pred.And(x => x.Campaña == Campaña) : pred;

            pred = pred.And(x => x.ProductoId == ProductoId);

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
                         NumLote = c.LoteCaja,
                         NumCaja = c.NumeroCaja,
                         Producto = cp.DESCRIPCION,
                         Tara = c.Tara,
                         Neto = c.Neto,
                         Bruto = c.Bruto,
                         Cata = joined.NumCata,
                         Campaña = c.Campaña
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
                       NumLote = c.LoteCaja,
                       NumCaja = c.NumeroCaja,
                       Producto = cp.DESCRIPCION,
                       Tara = c.Tara,
                       Neto = c.Neto,
                       Bruto = c.Bruto,
                       Cata = joined.NumCata,
                       Campaña = c.Campaña
                   })
                   .OrderBy(x => x.Campaña)
                   .ThenBy(x => x.NumCaja)
                   .ToList();

                gridControlCajaConsulta.DataSource = result;
            }

            gridViewCajaConsulta.Columns[0].Visible = false;
            gridViewCajaConsulta.Columns[1].Caption = "N° Lote";
            gridViewCajaConsulta.Columns[1].Width = 110;
            gridViewCajaConsulta.Columns[2].Caption = "N° Caja";
            gridViewCajaConsulta.Columns[2].Width = 110;
            gridViewCajaConsulta.Columns[3].Caption = "Producto";
            gridViewCajaConsulta.Columns[3].Width = 100;
            gridViewCajaConsulta.Columns[4].Caption = "Tara";
            gridViewCajaConsulta.Columns[4].Width = 100;
            gridViewCajaConsulta.Columns[5].Caption = "Neto";
            gridViewCajaConsulta.Columns[5].Width = 100;
            gridViewCajaConsulta.Columns[6].Caption = "Bruto";
            gridViewCajaConsulta.Columns[6].Width = 100;
            gridViewCajaConsulta.Columns[7].Caption = "N° Cata";
            gridViewCajaConsulta.Columns[7].Width = 200;
            gridViewCajaConsulta.Columns[8].Visible = false;

            for (var i = 0; i <= gridViewCajaConsulta.RowCount; i++)
            {
                gridViewCajaConsulta.SelectRow(i);
            }
        }

        private void AsignarCata()
        {
            for (int i = 0; i < gridViewCajaConsulta.DataRowCount; i++)
            {
                Guid CajaId = new Guid(gridViewCajaConsulta
                    .GetRowCellValue(i, "Id")
                    .ToString());

                CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

                var Cata = Context.Cata
                    .Where(x => x.NumCaja == null)
                    .FirstOrDefault();

                var cajaUpdate = Context.Caja
                    .Where(x => x.Id == CajaId)
                    .Update(x => new Caja() { CataId = Cata.Id });

                Context.SaveChanges();

                var caja = Context.Caja
                    .Where(x => x.Id == CajaId)
                    .FirstOrDefault();
                
                var CataUpdate = Context.Cata.Where(x => x.Id == Cata.Id)
                    .Update(x => new Cata() 
                    { 
                        CajaId = CajaId, 
                        NumCaja = caja.NumeroCaja, 
                        OrdenVentaId = caja.OrdenVentaId, 
                        NumOrden = caja.OrdenVenta.NumOrden
                    });

                Context.SaveChanges();
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

        #endregion

        #endregion

    }

    public class OrdenVentaProducto
    {
        public Guid OrdenId { get; set; }
        public Guid ProductoId { get; set; }
        public int Campaña { get; set; }
        public string Descripcion { get; set; }
    }
}