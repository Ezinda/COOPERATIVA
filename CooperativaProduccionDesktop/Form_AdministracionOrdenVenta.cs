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
using DevExpress.Utils;
using Extensions;
using System.Linq.Expressions;
using System.IO;
using System.Globalization;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlaceActualizar
    {
        public CooperativaProduccionEntities Context { get; set; }
        public Guid OrdenVentaId;

        public Form_AdministracionOrdenVenta()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar();
        }

        private long ContadorOrdenVenta()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.OrdenVenta))
                .FirstOrDefault();

            if (contador != null)
            {
                return (contador.Valor.Value + 1);
            }
            else
            {
                return 1;
            }
        }

        private long ContadorNumeroOperacion()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.NumeroOperacion))
                .FirstOrDefault();

            if (contador != null)
            {
                return (contador.Valor.Value + 1);
            }
            else
            {
                return 1;
            }
        }

        private void Iniciar()
        {
            txtNumOperacion.Text = ContadorNumeroOperacion().ToString();
            txtNumOrdenVenta.Text = ContadorOrdenVenta().ToString();
            CargarCombo();
        }

        private void BuscarOrdenVenta(Guid OrdenVentaId)
        {

            //if (OrdenVentaId == Guid.Empty)
            //{
            //    var result = (from o in Context.OrdenVenta
            //                  join p in Context.Vw_Producto
            //                  on o.ProductoId equals p.ID
            //                  join c in Context.Vw_Cliente
            //                  on o.ClienteId equals c.ID
            //                  select new
            //                  {
            //                      OrdenVentaId = o.Id,
            //                      NumOperacion = o.NumOperacion,
            //                      NumOrden = o.NumOrden,
            //                      Cliente = c.RAZONSOCIAL,
            //                      Producto = p.DESCRIPCION,
            //                      Fecha = o.Fecha
            //                  })
            //                  .OrderBy(x => x.NumOrden)
            //                  .ToList();
            //    gridControlOrdenVenta.DataSource = result;
            //    gridViewOrdenVenta.Columns[0].Visible = false;
            //}
            //else
            //{
            //    var result =
            //        (from o in Context.OrdenVenta
            //             .Where(x => x.Id == OrdenVentaId)
            //         join p in Context.Vw_Producto
            //         on o.ProductoId equals p.ID
            //         join c in Context.Vw_Cliente
            //         on o.ClienteId equals c.ID
            //         select new
            //         {
            //             OrdenVentaId = o.Id,
            //             NumOperacion = o.NumOperacion,
            //             NumOrden = o.NumOrden,
            //             Cliente = c.RAZONSOCIAL,
            //             Producto = p.DESCRIPCION,
            //             CajaDesde = o.DesdeCaja,
            //             CajaHasta = o.HastaCaja,
            //             Fecha = o.Fecha
            //         })
            //         .OrderBy(x => x.NumOrden)
            //         .ToList();
            //    gridControlOrdenVentaConsulta.DataSource = result;
            //    gridViewOrdenVentaConsulta.Columns[0].Visible = false;
            //}
        }

        private void gridControlOrdenVentaConsulta_DoubleClick(object sender, EventArgs e)
        {
            Guid OrdenVentaId = new Guid(gridViewOrdenVentaConsulta
                .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "OrdenVentaId")
                .ToString());
            var Pendiente = gridViewOrdenVentaConsulta
                .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Pendiente")
                .ToString();
            if (Pendiente.Equals(DevConstantes.SI))
            {
                var ordenventa = new Form_AdministracionActualizarOrdenVenta(OrdenVentaId);
                ordenventa.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Esta orden de venta está cerrada. No se pueden modificar sus datos.",
                    "Atención", MessageBoxButtons.OK);
            }
        }

        private void btnBuscarPendientes_Click(object sender, EventArgs e)
        {
            BuscarPendientes();
        }

        private void BuscarPendientes()
        {
            if (OrdenVenta.SelectedTabPage.Equals(TabConsultaOrdenVenta))
            {
                if (cbOperacionCliente.Text != string.Empty)
                {
                    var operacionCliente = cbOperacionCliente.SelectedItem as dynamic;

                    Guid cbOpCliente = Guid.Parse(operacionCliente.OrdenVentaId.ToString());

                    Expression<Func<OrdenVenta, bool>> pred = x => true;

                    pred = cbOperacionCliente.Text != string.Empty ? pred.And(x => x.Id == cbOpCliente) : pred;

                    pred = checkPendienteEmitirRemito.Checked ? pred.And(x => x.Pendiente == true) : pred;

                    var ordenVenta =
                        (from o in Context.OrdenVenta.Where(pred)
                         join c in Context.Vw_Cliente
                         on o.ClienteId equals c.ID
                         select new
                         {
                             OrdenVentaId = o.Id,
                             NumOperacion = o.NumOperacion,
                             NumOrden = o.NumOrden,
                             Cliente = c.RAZONSOCIAL,
                             Fecha = o.Fecha,
                             Pendiente = o.Pendiente == true ?
                                DevConstantes.SI : DevConstantes.NO
                         })
                         .OrderBy(x => x.NumOrden)
                         .ToList();

                    gridControlOrdenVentaConsulta.DataSource = ordenVenta;
                    gridViewOrdenVentaConsulta.Columns[0].Visible = false;
                    gridViewOrdenVentaConsulta.Columns[1].Caption = "N° Operación";
                    gridViewOrdenVentaConsulta.Columns[1].Width = 120;
                    gridViewOrdenVentaConsulta.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewOrdenVentaConsulta.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    gridViewOrdenVentaConsulta.Columns[2].Caption = "N° Orden";
                    gridViewOrdenVentaConsulta.Columns[2].Width = 120;
                    gridViewOrdenVentaConsulta.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewOrdenVentaConsulta.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    gridViewOrdenVentaConsulta.Columns[3].Caption = "Cliente";
                    gridViewOrdenVentaConsulta.Columns[3].Width = 250;
                    gridViewOrdenVentaConsulta.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewOrdenVentaConsulta.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                    gridViewOrdenVentaConsulta.Columns[4].Caption = "Fecha";
                    gridViewOrdenVentaConsulta.Columns[4].Width = 90;
                    gridViewOrdenVentaConsulta.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewOrdenVentaConsulta.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewOrdenVentaConsulta.Columns[5].Caption = "Pendiente";
                    gridViewOrdenVentaConsulta.Columns[5].Width = 120;
                    gridViewOrdenVentaConsulta.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridViewOrdenVentaConsulta.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
            }
            else
            {
                long numeroOperacion = long.Parse(txtNumOperacion.Text);
                long numeroOrden = long.Parse(txtNumOrdenVenta.Text);

                Expression<Func<OrdenVenta, bool>> pred = x => true;

                pred = txtNumOperacion.Text != string.Empty ? pred.And(x => x.NumOperacion == numeroOperacion) : pred;

                pred = txtNumOrdenVenta.Text != string.Empty ? pred.And(x => x.NumOrden == numeroOrden) : pred;

                pred = checkPendienteEmitirRemito.Checked ? pred.And(x => x.Pendiente == true) : pred;

                var ordenVenta =
                    (from o in Context.OrdenVenta.Where(pred)
                     join c in Context.Vw_Cliente
                     on o.ClienteId equals c.ID
                     select new
                     {
                         OrdenVentaId = o.Id,
                         NumOperacion = o.NumOperacion,
                         NumOrden = o.NumOrden,
                         Cliente = c.RAZONSOCIAL,
                         Fecha = o.Fecha,
                         Pendiente = o.Pendiente == true ?
                            DevConstantes.SI : DevConstantes.NO
                     })
                     .OrderBy(x => x.NumOrden)
                     .ToList();

                gridControlOrdenVenta.DataSource = ordenVenta;
                gridViewOrdenVenta.Columns[0].Visible = false;
                gridViewOrdenVenta.Columns[1].Caption = "N° Operación";
                gridViewOrdenVenta.Columns[1].Width = 120;
                gridViewOrdenVenta.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVenta.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gridViewOrdenVenta.Columns[2].Caption = "N° Orden";
                gridViewOrdenVenta.Columns[2].Width = 120;
                gridViewOrdenVenta.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVenta.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gridViewOrdenVenta.Columns[3].Caption = "Cliente";
                gridViewOrdenVenta.Columns[3].Width = 250;
                gridViewOrdenVenta.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVenta.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                gridViewOrdenVenta.Columns[4].Caption = "Fecha";
                gridViewOrdenVenta.Columns[4].Width = 90;
                gridViewOrdenVenta.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVenta.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVenta.Columns[5].Caption = "Pendiente";
                gridViewOrdenVenta.Columns[5].Width = 120;
                gridViewOrdenVenta.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVenta.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }

        void IEnlaceActualizar.Enviar(bool Enviar)
        {
            if (Enviar.Equals(true))
            {
                BuscarPendientes();
                Iniciar();
            }
        }

        private void btnExportarResumen_Click(object sender, EventArgs e)
        {
            if (gridViewOrdenVentaConsulta.SelectedRowsCount > 0)
            {
                var resultado = MessageBox.Show("¿Desea exportar el archivo de vinculación?",
                          "Atención", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                Guid OrdenVentaId = new Guid(gridViewOrdenVentaConsulta
                    .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "OrdenVentaId")
                    .ToString());
                CrearTxtVinculacion(OrdenVentaId);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una orden de venta para exportar.",
                  "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CrearTxtVinculacion(Guid OrdenVentaId)
        {
            string path = @"C:\SystemDocumentsCooperativa";
            CreateIfMissing(path);
            path = @"C:\SystemDocumentsCooperativa\TxtResumen";
            CreateIfMissing(path);
            string fileName = @"C:\SystemDocumentsCooperativa\TxtResumen\Resumen_" + cbOperacionCliente.Text + ".txt";
            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file 
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string Cuit = DevConstantes.CuitEmpresa.Replace("-", "");
                    string InicioActividades = DateTime.ParseExact(DevConstantes.InicioActividades,
                        "dd-MM-yy", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy");
                    var OrdenVenta = Context.OrdenVenta.Where(x => x.Id == OrdenVentaId).FirstOrDefault();
                    var Cliente = Context.Vw_Cliente.Where(x => x.ID == OrdenVenta.ClienteId).FirstOrDefault();
                    string CuitCliente = Cliente.CUIT.Contains(DevConstantes.XX) ? Cliente.CUITE : Cliente.CUIT;
                    sw.WriteLine("1;S;S;" + Cuit + ";" + InicioActividades + ";;0003;0046;" + CuitCliente + ";"
                        + Cliente.RAZONSOCIAL + ";" + Cliente.DOMICILIO + ";0;;;;;;;;NINGUNA;;MULTICARGAS SRL;;;0;;;");
                    var catas = Context.Cata
                        .Where(x => x.OrdenVentaId == OrdenVenta.Id)
                        .ToList();
                    foreach (var cata in catas)
                    {
                        sw.WriteLine("2;" + cata.NumCata);
                    }
                }

                // Write file contents on console. 
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
                MessageBox.Show("Archivo de resumen creado.",
                   "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
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

        private void btnNuevaOrdenVenta_Click(object sender, EventArgs e)
        {
            Form_AdministracionNuevaOrdenVenta nuevaOrden = new Form_AdministracionNuevaOrdenVenta();
            nuevaOrden.ShowDialog(this);
        }

        private void CargarCombo()
        {
            var producto = Context.Vw_Producto
                .OrderBy(x => x.DESCRIPCION)
                .ToList();

            var ordenVenta =
                (from o in Context.OrdenVenta
                 join c in Context.Vw_Cliente
                 on o.ClienteId equals c.ID
                 select new
                 {
                     OrdenVentaId = o.Id,
                     NumOperacion = o.NumOperacion,
                     NumOrden = o.NumOrden,
                     OperacionCliente = o.NumOperacion + " - " + c.RAZONSOCIAL,
                     Cliente = c.RAZONSOCIAL,
                     Fecha = o.Fecha
                 })
                 .OrderBy(x => x.NumOperacion)
                 .ToList();

            if (ordenVenta.Count() != 0)
            {
                cbOperacionCliente.DataSource = ordenVenta;
                cbOperacionCliente.DisplayMember = "OperacionCliente";
                cbOperacionCliente.ValueMember = "OrdenVentaId";

                OrdenVentaId = ordenVenta.FirstOrDefault().OrdenVentaId;
            }
        }
    }
}