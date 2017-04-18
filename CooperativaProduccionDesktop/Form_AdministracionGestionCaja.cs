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
                    LoteCaja = ContadorNumeroLote();
                    for (int i = 0; i < int.Parse(txtCantidadCajaIngreso.Text); i++)
                    {
                        Caja caja;
                        caja = new Caja();
                        caja.Id = Guid.NewGuid();
                        caja.LoteCaja = LoteCaja;
                        caja.NumeroCaja = ContadorNumeroCaja();
                        caja.Fecha = dpIngresoCaja.Value.Date;
                        caja.Hora = DateTime.Now.TimeOfDay;

                        Guid OrdenVentaId = Guid.Parse(cbProductoIngreso.SelectedValue.ToString());
                        var ordenVenta = Context.OrdenVenta.Where(x => x.Id == OrdenVentaId).FirstOrDefault();
                        var producto = Context.Vw_Producto
                            .Where(x => x.ID == ordenVenta.ProductoId)
                            .FirstOrDefault();

                        caja.ProductoId = producto.ID;
                        caja.Bruto = decimal.Parse(txtBruto.Text, CultureInfo.InvariantCulture);
                        caja.Tara = decimal.Parse(txtTara.Text, CultureInfo.InvariantCulture);
                        caja.Neto = decimal.Parse(txtNeto.Text, CultureInfo.InvariantCulture);
                        caja.OrdenVentaId = OrdenVentaId;

                        if (checkCata.Checked)
                        {
                            var cata = Context.Cata
                                .Where(x => x.NumCaja == null);

                            if (cata != null)
                            {
                                caja.CataId = cata.FirstOrDefault().Id;

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
                        Context.Caja.Add(caja);
                        Context.SaveChanges();
                        ActualizarContadorCaja();
                        RegistrarMovimiento(caja.Id, 1, caja.Fecha);
                    }
                    ActualizarContadorLote();
                }
                catch
                {
                    throw;
                }
            }
        }

        private void ActualizarContadorLote()
        {
            var contadorLote = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.Lote))
                .FirstOrDefault();

            var countLote = Context.Contador.Find(contadorLote.Id);

            if (countLote != null)
            {
                countLote.Valor = countLote.Valor + 1;
                Context.Entry(countLote).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }

        private void ActualizarContadorCaja()
        {
            var contadorCaja = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.Caja))
                .FirstOrDefault();

            var countCaja = Context.Contador.Find(contadorCaja.Id);

            if (countCaja != null)
            {
                countCaja.Valor = countCaja.Valor + 1;
                Context.Entry(countCaja).State = EntityState.Modified;
                Context.SaveChanges();
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
                 join p in Context.Vw_Producto
                 on o.ProductoId equals p.ID
                 select new OrdenVentaProducto
                 {
                     Id = o.Id,
                     Descripcion = o.NumOrden + " - " + p.DESCRIPCION,
                 })
                .OrderBy(x => x.Descripcion)
                .ToList();

            cbProductoIngreso.DataSource = ordenVenta;
            cbProductoIngreso.DisplayMember = "Descripcion";
            cbProductoIngreso.ValueMember = "Id";

            var producto = Context.Vw_Producto.ToList();
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
                select new
                {
                    Id = c.Id,
                    NumLote = c.LoteCaja,
                    NumCaja = c.NumeroCaja,
                    Producto = cp.DESCRIPCION,
                    Bruto = c.Bruto,
                    Tara = c.Tara,
                    Neto = c.Neto,
                    Cata = c.Cata.NumCata,
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
            gridViewCaja.Columns[4].Caption = "Bruto";
            gridViewCaja.Columns[4].Width = 100;
            gridViewCaja.Columns[5].Caption = "Tara";
            gridViewCaja.Columns[5].Width = 100;
            gridViewCaja.Columns[6].Caption = "Neto";
            gridViewCaja.Columns[6].Width = 100;
            gridViewCaja.Columns[7].Caption = "N° Cata";
            gridViewCaja.Columns[7].Width = 200;
            gridViewCaja.Columns[8].Visible = false;
        }

        private long ContadorNumeroCaja()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.Caja))
                .FirstOrDefault();

            if (contador != null)
            {
                return long.Parse(contador.Valor.ToString());
            }
            else
            {
                return 1;
            }
        }

        private long ContadorNumeroLote()
        {
            var contador =
                Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.Lote))
                .FirstOrDefault();

            if (contador != null)
            {
                return long.Parse(contador.Valor.ToString());
            }
            else
            {
                return 1;
            }
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
                BuscarCajaConsulta();
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
                BuscarCajaConsulta();
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
     
        #endregion

        #region Method Dev

        private void BuscarCajaConsulta()
        {
            var ProductoId = Guid.Parse(cbProductoConsulta.SelectedValue.ToString());
            var cantidad = int.Parse(txtCantidadCajaConsulta.Text);

            var result = 
                (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId) 
                 join p in Context.Vw_Producto
                 on c.ProductoId equals p.ID into pr
                 from cp in pr.DefaultIfEmpty()
                 select new
                 {
                     Id = c.Id,
                     NumLote = c.LoteCaja,
                     NumCaja = c.NumeroCaja,
                     Producto = cp.DESCRIPCION,
                     Bruto = c.Bruto,
                     Tara = c.Tara,
                     Neto = c.Neto,
                     Cata = c.Cata.NumCata,
                     Fecha = c.Fecha
                 })
                 .Take(cantidad)
                 .OrderBy(x => x.NumCaja)
                 .ToList();

            gridControlCajaConsulta.DataSource = result;
            gridViewCajaConsulta.Columns[0].Visible = false;
            gridViewCajaConsulta.Columns[1].Caption = "N° Lote";
            gridViewCajaConsulta.Columns[1].Width = 110;
            gridViewCajaConsulta.Columns[2].Caption = "N° Caja";
            gridViewCajaConsulta.Columns[2].Width = 110;
            gridViewCajaConsulta.Columns[3].Caption = "Producto";
            gridViewCajaConsulta.Columns[3].Width = 100;
            gridViewCajaConsulta.Columns[4].Caption = "Bruto";
            gridViewCajaConsulta.Columns[4].Width = 100;
            gridViewCajaConsulta.Columns[5].Caption = "Tara";
            gridViewCajaConsulta.Columns[5].Width = 100;
            gridViewCajaConsulta.Columns[6].Caption = "Neto";
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

                var Caja = Context.Caja.Find(CajaId);

                if (Caja != null)
                {
                    if (Caja.CataId == null)
                    {
                        var Cata = Context.Cata
                            .Where(x => x.NumCaja == null);

                        if (Cata != null)
                        {
                            Caja.CataId = Cata.FirstOrDefault().Id;
                            Context.Entry(Caja).State = EntityState.Modified;
                            Context.SaveChanges();

                            var ActualizarCata = Context.Cata.Find(Caja.CataId);
                            ActualizarCata.NumCaja = Caja.NumeroCaja;
                            ActualizarCata.CajaId = Caja.Id;

                            var ordenVenta = Context.OrdenVenta
                                .Where(x => x.Id == Caja.OrdenVentaId)
                                .FirstOrDefault();

                            if (ordenVenta != null)
                            {
                                ActualizarCata.OrdenVentaId = Caja.OrdenVentaId;
                                ActualizarCata.NumOrden = ordenVenta.NumOrden;
                            }
                            Context.Entry(ActualizarCata).State = EntityState.Modified;
                            Context.SaveChanges();
                        }
                    }
                }
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

            if (txtCantidadCajaConsulta.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una cantidad de cajas válidas",
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

        #endregion

        #endregion

    }

    public class OrdenVentaProducto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
    }
}