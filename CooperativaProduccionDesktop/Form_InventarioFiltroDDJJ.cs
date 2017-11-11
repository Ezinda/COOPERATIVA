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
using CooperativaProduccion.Reports;
using System.Globalization;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.ReportModels;
using System.Linq.Expressions;
using Extensions;
using DevExpress.XtraPrinting;
using System.IO;
using System.Diagnostics;
using System.Data.Entity.SqlServer;

namespace CooperativaProduccion
{
    public partial class Form_InventarioFiltroDDJJ : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public string Origen;

        public Form_InventarioFiltroDDJJ()
        {
            InitializeComponent();
            Iniciar();
        }

        #region Method Code
  
        private void btnAceptar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
              
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GenerarDDJJ();
            this.Close();
        }

        private string CrearSubDirectorio(string nombre)
        {
            var rootPath = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(rootPath);

            CreateIfMissing(rootPath + @"\" + nombre);

            return rootPath + @"\" + nombre;
        }


        private void GenerarDDJJ()
        {
            try
            {
                var filename = CrearSubDirectorio("DDJJ")
                    + @"/"
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture)
                        .Replace(":", "")
                        .Replace(".", "")
                        .Replace("-", "")
                        .Replace(" ", "")
                    + "-DDJJ.xls";

                int campaña = int.Parse(cbCampaña.Text);
                
                CrearExcel(filename, campaña);

                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No se puede crear directorio")
                {
                    MessageBox.Show(ex.InnerException.ToString(), "ERROR al intentar crear un directorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message == "Error Office.Interop")
                {
                    MessageBox.Show(ex.InnerException.ToString(), "ERROR con Office.Interop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.ToString(), "ERROR no reconocido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Method Dev

        private void Iniciar()
        {
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        private void CargarCombo()
        {
            var campaña =
              (from c in Context.Caja
                  .Where(x => x.OrdenVentaId == null)
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

        private void CrearExcel(string filename,int campaña)
        {
            try
            {
                var oXL = new Microsoft.Office.Interop.Excel.Application();

                oXL.Visible = false;
                oXL.UserControl = false;

                var oWB = (Microsoft.Office.Interop.Excel._Workbook)oXL.Workbooks.Open(System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "/DDJJ.xls");
                var oSheetAnexo = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets["anexo"];

                #region Materia Prima
                var materiaprima =
                    (from m in Context.Movimiento
                     join p in Context.Vw_Pesada
                         on m.TransaccionId equals p.PesadaDetalleId
                     join d in Context.Vw_Deposito
                        .Where(x => x.nombre == DevConstantes.MateriaPrima)
                         on m.DepositoId equals d.id
                     group new { m, p, d } by new
                     {
                         Deposito = d.nombre,
                         TipoTabaco = p.DESCRIPCION,
                         Tabaco = p.DESCRIPCION,
                         Subrubro = p.DESCRIPCION,
                         m.Unidad,
                         Campaña = m.Fecha.Value.Year
                     } into g
                     select new
                     {
                         g.Key.Deposito,
                         g.Key.TipoTabaco,
                         g.Key.Tabaco,
                         g.Key.Subrubro,
                         g.Key.Unidad,
                         Campaña = g.Key.Campaña,
                         Ingreso = g.Sum(c => c.m.Ingreso),
                         Egreso = g.Sum(c => c.m.Egreso),
                         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                     })
                     .ToList();
                #endregion  


                var depositoCajaAnterior =
                    (from m2 in Context.Movimiento.Where(x => x.Fecha.Value.Year < campaña).AsEnumerable()
                     join c in Context.Caja.Where(x => x.Campaña < campaña).AsEnumerable()
                     on m2.TransaccionId equals c.Id
                     join pr in Context.Vw_Producto.AsEnumerable()
                         on c.ProductoId equals pr.ID
                     join d2 in Context.Vw_Deposito.AsEnumerable()
                        .Where(x => x.id == DevConstantes.DepositoCaja)
                         on m2.DepositoId equals d2.id
                     group new { m2, c, pr, d2 } by new
                     {
                         CajaId = c.Id,
                         Deposito = d2.nombre,
                         TipoTabaco = pr.DESCRIPCION + " - " + c.Campaña,
                         Tabaco = pr.DESCRIPCION,
                         Subrubro = pr.SUBRUBRO,
                         m2.Unidad,
                         c.Campaña,
                         Bruto = Convert.ToDouble(c.Bruto)
                     } into g
                     select new
                     {
                         g.Key.CajaId,
                         g.Key.Deposito,
                         g.Key.TipoTabaco,
                         g.Key.Tabaco,
                         g.Key.Subrubro,
                         g.Key.Unidad,
                         g.Key.Campaña,
                         g.Key.Bruto,
                         Ingreso = g.Sum(c => c.m2.Ingreso),
                         Egreso = g.Sum(c => c.m2.Egreso),
                         Saldo = (g.Sum(c => c.m2.Ingreso) - g.Sum(c => c.m2.Egreso)) * g.Key.Bruto
                     })
                     .ToList();

                var depositoCajaActual =
                    (from m2 in Context.Movimiento
                        .Where(x => x.Fecha.Value.Year == campaña).AsEnumerable()
                     join c in Context.Caja
                        .Where(x => x.Campaña == campaña).AsEnumerable()
                     on m2.TransaccionId equals c.Id
                     join pr in Context.Vw_Producto.AsEnumerable()
                         on c.ProductoId equals pr.ID
                     join d2 in Context.Vw_Deposito
                        .Where(x => x.id == DevConstantes.DepositoCaja).AsEnumerable()
                         on m2.DepositoId equals d2.id
                     group new { m2, c, pr, d2 } by new
                     {
                         CajaId = c.Id,
                         Deposito = d2.nombre,
                         TipoTabaco = pr.DESCRIPCION + " - " + c.Campaña,
                         Tabaco = pr.DESCRIPCION,
                         Subrubro = pr.SUBRUBRO,
                         m2.Unidad,
                         c.Campaña,
                         Bruto = Convert.ToDouble(c.Bruto)
                     } into g
                     select new
                     {
                         g.Key.CajaId,
                         g.Key.Deposito,
                         g.Key.TipoTabaco,
                         g.Key.Tabaco,
                         g.Key.Subrubro,
                         g.Key.Unidad,
                         g.Key.Campaña,
                         g.Key.Bruto,
                         Ingreso = g.Sum(c => c.m2.Ingreso),
                         Egreso = g.Sum(c => c.m2.Egreso),
                         Saldo = g.Sum(c => c.m2.Ingreso) - g.Sum(c => c.m2.Egreso) * g.Key.Bruto
                     })
                     .ToList();

                //C5
                var inicialVirginiaHoja = materiaprima
                    .Where(x => x.Campaña < campaña 
                        && x.Subrubro == "61")
                        .Sum(x => x.Saldo);
                //D5
                var inicialBurleyHoja = materiaprima
                    .Where(x => x.Campaña < campaña
                        && x.Subrubro == "60")
                        .Sum(x => x.Saldo);

                //C6
                var stockVirginiaAnterior = depositoCajaAnterior
                    .Where(x => x.Subrubro == "61")
                    .Select(x => x.Saldo)
                    .Sum();
                //D6
                var stockBurleyAnterior = depositoCajaAnterior
                    .Where(x => x.Subrubro == "60")
                    .Select(x => x.Saldo)
                    .Sum();

                //C8
                var hojasProducidasVirginia = materiaprima
                    .Where(x => x.Campaña == campaña
                        && x.TipoTabaco == DevConstantes.TabacoVirginia)
                        .FirstOrDefault();
                
                //D8
                var hojasProducidasBurley = materiaprima
                    .Where(x => x.Campaña == campaña
                      && x.TipoTabaco == DevConstantes.TabacoBurley)
                        .FirstOrDefault();

                //C9
                var cajasVirginia = depositoCajaActual
                    .Where(x => x.Subrubro == "61")
                    .Select(x => x.Saldo)
                    .Sum();
                //D9
                var cajasBurley = depositoCajaActual
                    .Where(x => x.Subrubro == "60")
                    .Select(x => x.Saldo)
                    .Sum();
                
                //C13
                var romaneoVirginia = materiaprima
                  .Where(x => x.Campaña == campaña
                      && x.Subrubro == DevConstantes.TabacoVirginia)
                      .Sum(x => x.Ingreso);
                //D13
                var romaneoBurley = materiaprima
                  .Where(x => x.Campaña == campaña
                      && x.Subrubro == DevConstantes.TabacoBurley)
                      .Sum(x => x.Ingreso);

                //C11
                var mermasVirginia = romaneoVirginia - hojasProducidasVirginia.Egreso - cajasVirginia;

                //D11
                var mermasBurley = romaneoBurley - hojasProducidasBurley.Egreso - cajasBurley;

                #region MyRegion

                

                // Stock actual de Virginia - Cajas
                //var actualVirginiaDespalillado =
                //      (from c in Context.Caja
                //       .Where(x => x.Campaña == campaña)
                //       join p in Context.Vw_Producto.Where(x => x.SUBRUBRO == "61")
                //       on c.ProductoId equals p.ID
                //       select new
                //       {
                //           Saldo = (Decimal?)c.Bruto
                //       })
                //       .Select(x => x.Saldo).Sum() ?? 0;

                // Stock actual de Burley - Cajas
                //var actualBurleyDespalillado =
                //      (from c in Context.Caja
                //       .Where(x => x.Campaña == campaña)
                //       join p in Context.Vw_Producto.Where(x => x.SUBRUBRO == "60")
                //       on c.ProductoId equals p.ID
                //       select new
                //       {
                //           Saldo = (Decimal?)c.Bruto
                //       })
                //       .Select(x => x.Saldo).Sum() ?? 0;

                // Romaneo Virginia
                //var romaneoVirginia = Context.Vw_ResumenRomaneoVirginia
                //    .Where(x => x.fechaRomaneo.Value.Year == campaña)
                //    .Select(x => x.Totalkg)
                //    .Sum(x => x.Value);

                //var hojaVirginia =
                //    (from m in Context.Movimiento
                //        .Where(x => x.Fecha.Value.Year == campaña)
                //     join p in Context.Vw_Pesada
                //        .Where(x => x.DESCRIPCION == DevConstantes.TabacoVirginia)
                //         on m.TransaccionId equals p.PesadaDetalleId
                //     join d in Context.Vw_Deposito.Where(x => x.id == DevConstantes.DepositoMateriaPrima)
                //         on m.DepositoId equals d.id
                //     group new { m, p, d } by new
                //     {
                //         Deposito = d.nombre,
                //         TipoTabaco = p.DESCRIPCION,
                //         Tabaco = p.DESCRIPCION,
                //         m.Unidad,
                //         Campaña = 1
                //     } into g
                //     select new
                //     {
                //         g.Key.Deposito,
                //         g.Key.TipoTabaco,
                //         g.Key.Tabaco,
                //         g.Key.Unidad,
                //         Campaña = g.Key.Campaña,
                //         Ingreso = g.Sum(c => c.m.Ingreso),
                //         Egreso = g.Sum(c => c.m.Egreso),
                //         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                //     })
                //     .FirstOrDefault();

                //Romaneo Burley
                //var romaneoBurley =
                //    (from m in Context.Movimiento
                //     .Where(x => x.Fecha.Value.Year == campaña)
                //     join p in Context.Vw_Pesada
                //     .Where(x => x.DESCRIPCION == DevConstantes.TabacoBurley)
                //     on m.TransaccionId equals p.PesadaDetalleId
                //     join d in Context.Vw_Deposito.Where(x => x.id == DevConstantes.DepositoMateriaPrima)
                //         on m.DepositoId equals d.id
                //     group new { m, p, d } by new
                //     {
                //         Deposito = d.nombre,
                //         TipoTabaco = p.DESCRIPCION,
                //         Tabaco = p.DESCRIPCION,
                //         m.Unidad,
                //         Campaña = 1
                //     } into g
                //     select new
                //     {
                //         g.Key.Deposito,
                //         g.Key.TipoTabaco,
                //         g.Key.Tabaco,
                //         g.Key.Unidad,
                //         Campaña = g.Key.Campaña,
                //         Ingreso = g.Sum(c => c.m.Ingreso),
                //         Egreso = g.Sum(c => c.m.Egreso),
                //         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                //     })
                //    .FirstOrDefault();

                // Merma Virginia
                //var mermaVirginia = decimal.Parse(romaneoVirginia.ToString()) - actualVirginiaDespalillado;

                // Merma Burley
                //var mermaBurley = decimal.Parse(romaneoBurley.Ingreso.ToString()) - actualBurleyDespalillado - decimal.Parse(romaneoBurley.Saldo.ToString());

                // Venta Virginia
                //var ventaVirginia =
                //    (from c in Context.Caja
                //     .Where(x => x.Campaña == campaña)
                //     join p in Context.Vw_Producto.Where(x => x.SUBRUBRO == "61")
                //     on c.ProductoId equals p.ID
                //     join o in Context.OrdenVenta
                //     on c.OrdenVentaId equals o.Id
                //     join r in Context.Remito.Where(x => x.FechaRemito.Value.Year == campaña)
                //     on o.Id equals r.OrdenVentaId
                //     select new
                //     {
                //         Saldo = (Decimal?)c.Bruto
                //     })
                //    .Select(x => x.Saldo).Sum() ?? 0;

                // Venta Burley
                //var ventaBurley =
                //    (from c in Context.Caja
                //     .Where(x => x.Campaña == campaña)
                //     join p in Context.Vw_Producto.Where(x => x.SUBRUBRO == "60")
                //     on c.ProductoId equals p.ID
                //     join o in Context.OrdenVenta
                //     on c.OrdenVentaId equals o.Id
                //     join r in Context.Remito.Where(x => x.FechaRemito.Value.Year == campaña)
                //     on o.Id equals r.OrdenVentaId
                //     select new
                //     {
                //         Saldo = (Decimal?)c.Bruto
                //     })
                //    .Select(x => x.Saldo).Sum() ?? 0;

                // Producido Virginia - Ingreso de Cajas
                //var producidoVirginia =
                //   (from m2 in Context.Movimiento.Where(x => x.Fecha.Value.Year == campaña)
                //    join c in Context.Caja
                //            on m2.TransaccionId equals c.Id
                //    join pr in Context.Vw_Producto.Where(x => x.SUBRUBRO == "61")
                //        on c.ProductoId equals pr.ID
                //    join d2 in Context.Vw_Deposito.Where(x => x.id == DevConstantes.DepositoCaja)
                //        on m2.DepositoId equals d2.id
                //    group new { m2, c, pr, d2 } by new
                //    {
                //        Deposito = d2.nombre,
                //        TipoTabaco = pr.DESCRIPCION + " - " + c.Campaña,
                //        Tabaco = pr.DESCRIPCION,
                //        m2.Unidad,
                //        c.Campaña
                //    } into g
                //    select new
                //    {
                //        g.Key.Deposito,
                //        g.Key.TipoTabaco,
                //        g.Key.Tabaco,
                //        g.Key.Unidad,
                //        g.Key.Campaña,
                //        Ingreso = g.Sum(c => c.m2.Ingreso),
                //        Egreso = g.Sum(c => c.m2.Egreso),
                //        Saldo = g.Sum(c => c.m2.Ingreso) - g.Sum(c => c.m2.Egreso)
                //    })
                //    .FirstOrDefault();

                // Producido Burley - Ingreso de Cajas
                //var producidoBurley =
                //  (from m2 in Context.Movimiento.Where(x => x.Fecha.Value.Year == campaña)
                //   join c in Context.Caja
                //           on m2.TransaccionId equals c.Id
                //   join pr in Context.Vw_Producto.Where(x => x.SUBRUBRO == "60")
                //       on c.ProductoId equals pr.ID
                //   join d2 in Context.Vw_Deposito.Where(x => x.id == DevConstantes.DepositoCaja)
                //       on m2.DepositoId equals d2.id
                //   group new { m2, c, pr, d2 } by new
                //   {
                //       Deposito = d2.nombre,
                //       TipoTabaco = pr.DESCRIPCION + " - " + c.Campaña,
                //       Tabaco = pr.DESCRIPCION,
                //       m2.Unidad,
                //       c.Campaña
                //   } into g
                //   select new
                //   {
                //       g.Key.Deposito,
                //       g.Key.TipoTabaco,
                //       g.Key.Tabaco,
                //       g.Key.Unidad,
                //       g.Key.Campaña,
                //       Ingreso = g.Sum(c => c.m2.Ingreso),
                //       Egreso = g.Sum(c => c.m2.Egreso),
                //       Saldo = g.Sum(c => c.m2.Ingreso) - g.Sum(c => c.m2.Egreso)
                //   })
                //    .FirstOrDefault();
                #endregion

                oSheetAnexo.Cells[3, 3] = "TIPOS DE TABACO al " + DateTime.Now.ToShortDateString();

                // Celda B3
                //oSheetAnexo.Cells[6, 3] = inicialVirginiaDespalillado;
                //oSheetAnexo.Cells[6, 4] = inicialBurleyDespalillado;

                ////  oSheetAnexo.Cells[8, 3] = //hojaVirginia.Saldo;
                //oSheetAnexo.Cells[8, 4] = romaneoBurley.Saldo;

                //oSheetAnexo.Cells[9, 3] = actualVirginiaDespalillado;
                //oSheetAnexo.Cells[9, 4] = actualBurleyDespalillado;

                //oSheetAnexo.Cells[11, 3] = mermaVirginia;
                //oSheetAnexo.Cells[11, 4] = mermaBurley;

                //oSheetAnexo.Cells[13, 3] = romaneoVirginia;
                //oSheetAnexo.Cells[13, 4] = romaneoBurley.Ingreso;

                //oSheetAnexo.Cells[18, 3] = ventaVirginia;
                //oSheetAnexo.Cells[18, 4] = ventaBurley;
                
                //oSheetAnexo.Cells[27, 3] = decimal.Parse(producidoVirginia.ToString()) - ventaVirginia;
                //oSheetAnexo.Cells[27, 4] = decimal.Parse(producidoBurley.Ingreso.ToString()) - ventaBurley;

                //  ((Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets["anexo"]).Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVeryHidden;

                oWB.SaveAs(filename,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
                    Type.Missing,
                    Type.Missing,
                    false,
                    false,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing);

                oWB.Close();

                oXL.Quit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Office.Interop", ex);
            }
        }

        #endregion
    }
}