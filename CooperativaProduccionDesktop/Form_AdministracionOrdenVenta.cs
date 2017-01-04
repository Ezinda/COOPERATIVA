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
    public partial class Form_AdministracionOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlaceActualizarHistorico,IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarCliente _formBuscarCliente;
        private Guid OrdenVentaId;
        private Guid ClienteId;

        public Form_AdministracionOrdenVenta()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar();
        }

        private void CargarCombo()
        {
            var producto = Context.Vw_Producto
                .OrderBy(x => x.DESCRIPCION)
                .ToList();

            cbProducto.DataSource = producto;
            cbProducto.DisplayMember = "DESCRIPCION";
            cbProducto.ValueMember = "ID";

            var ordenVenta =
                (from o in Context.OrdenVenta
                 join p in Context.Vw_Producto
                 on o.ProductoId equals p.ID
                 join c in Context.Vw_Cliente
                 on o.ClienteId equals c.ID
                 select new
                 {
                     OrdenVentaId = o.Id,
                     NumOperacion = o.NumOperacion,
                     NumOrden = o.NumOrden,
                     OperacionCliente = o.NumOperacion + " - " + c.RAZONSOCIAL,
                     Cliente = c.RAZONSOCIAL,
                     Producto = p.DESCRIPCION,
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

        private void btnGenerarOrdenVenta_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                var resultado = MessageBox.Show("¿Desea generar orden de venta?",
                     "Atención", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GenerarOrdenVenta();
                BuscarPendientes();
                Iniciar();
            }
        }

        private long ContadorNumeroOperacion()
        {
            long numOperacion = 0;
            var ov = Context.OrdenVenta
                .OrderByDescending(x => x.NumOperacion)
                .FirstOrDefault();
            if (ov != null)
            {
                numOperacion = ov.NumOperacion + 1;
            }
            else
            {
                numOperacion = 1;
            }
            return numOperacion;
        }

        private long ContadorNumeroOrden()
        {
            long numOrden = 0;
            var ov = Context.OrdenVenta
                .OrderByDescending(x => x.NumOrden)
                .FirstOrDefault();
            if (ov != null)
            {
                numOrden = ov.NumOrden + 1;
            }
            else
            {
                numOrden = 1;
            }
            return numOrden;
        }

        private void Iniciar()
        {
            txtNumOperacion.Text = ContadorNumeroOperacion().ToString();
            txtNumOperacion.Enabled = false;
            txtNumOrdenVenta.Text = ContadorNumeroOrden().ToString();
            txtNumOrdenVenta.Enabled = false;
            txtCliente.Text = string.Empty;
            cbProducto.Text = string.Empty;
        }

        private bool Validar()
        {
            if (cbProducto.Text == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un producto",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtCliente.Text == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un cliente",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void GenerarOrdenVenta()
        {
            try
            {
                OrdenVenta ov;
                ov = new OrdenVenta();
                ov.Id = Guid.NewGuid();
                ov.NumOperacion = int.Parse(txtNumOperacion.Text);
                ov.NumOrden = int.Parse(txtNumOrdenVenta.Text);
                ov.ClienteId = ClienteId;
                ov.ProductoId = Guid.Parse(cbProducto.SelectedValue.ToString());
                ov.Fecha = DateTime.Now.Date;
                ov.Pendiente = true;
                Context.OrdenVenta.Add(ov);
                Context.SaveChanges();
            }
            catch
            {
                throw;
            }
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

        private void OrdenVenta_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            CargarCombo();
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
            if (cbOperacionCliente.Text != string.Empty)
            {
                var operacionCliente = cbOperacionCliente.SelectedItem as dynamic;

                Guid cbOpCliente = Guid.Parse(operacionCliente.OrdenVentaId.ToString());

                Expression<Func<OrdenVenta, bool>> pred = x => true;

                pred = cbOperacionCliente.Text != string.Empty ? pred.And(x => x.Id == cbOpCliente) : pred;

                pred = checkPendienteEmitirRemito.Checked ? pred.And(x => x.Pendiente == true) : pred;

                var ordenVenta =
                    (from o in Context.OrdenVenta.Where(pred)
                     join p in Context.Vw_Producto
                     on o.ProductoId equals p.ID
                     join c in Context.Vw_Cliente
                     on o.ClienteId equals c.ID
                     select new
                     {
                         OrdenVentaId = o.Id,
                         NumOperacion = o.NumOperacion,
                         NumOrden = o.NumOrden,
                         Cliente = c.RAZONSOCIAL,
                         Producto = p.DESCRIPCION,
                         CajaDesde = o.DesdeCaja,
                         CajaHasta = o.HastaCaja,
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
                gridViewOrdenVentaConsulta.Columns[4].Caption = "Producto";
                gridViewOrdenVentaConsulta.Columns[4].Width = 120;
                gridViewOrdenVentaConsulta.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[5].Caption = "Caja Desde";
                gridViewOrdenVentaConsulta.Columns[5].Width = 120;
                gridViewOrdenVentaConsulta.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[6].Caption = "Caja Hasta";
                gridViewOrdenVentaConsulta.Columns[6].Width = 120;
                gridViewOrdenVentaConsulta.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[7].Caption = "Fecha";
                gridViewOrdenVentaConsulta.Columns[7].Width = 90;
                gridViewOrdenVentaConsulta.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[8].Caption = "Pendiente";
                gridViewOrdenVentaConsulta.Columns[8].Width = 120;
                gridViewOrdenVentaConsulta.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }

        void IEnlaceActualizarHistorico.Enviar(bool Enviar)
        {
            if (Enviar.Equals(true))
            {
                BuscarPendientes();
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
                    string Cuit = DevConstantes.CuitEmpresa.Replace("-","");
                    string InicioActividades = DateTime.ParseExact(DevConstantes.InicioActividades,
                        "dd-MM-yy", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy");
                    var OrdenVenta = Context.OrdenVenta.Where(x=>x.Id == OrdenVentaId).FirstOrDefault();
                    var Cliente = Context.Vw_Cliente.Where(x => x.ID == OrdenVenta.ClienteId).FirstOrDefault();
                    string CuitCliente = Cliente.CUIT.Contains(DevConstantes.XX) ? Cliente.CUITE : Cliente.CUIT;
                    sw.WriteLine("1;S;S;" + Cuit + ";" + InicioActividades + ";;0003;0046;" + CuitCliente + ";"
                        + Cliente.RAZONSOCIAL + ";" + Cliente.DOMICILIO + ";0;;;;;;;;NINGUNA;;MULTICARGAS SRL;;;0;;;");
                    var catas = Context.Cata
                        .Where(x => x.OrdenVentaId == OrdenVenta.Id)
                        .ToList();
                    foreach (var cata in catas)
                    {  
                        sw.WriteLine("2;" + cata.Cata1);
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

        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ClienteId = Id;
            txtCliente.Text = nombre;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuscarCliente();
            }
        }

        private void BuscarCliente()
        {
            var result = 
                (from a in Context.Vw_Cliente
                 select new
                 {
                     full = a.CUIT + a.RAZONSOCIAL + a.CUITE,
                     ID = a.ID,
                     CUIT = a.CUIT.Contains(DevConstantes.XX) ? a.CUITE : a.CUIT,
                     CLIENTE = a.RAZONSOCIAL,
                     PROVINCIA = a.Provincia
                 });

            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                var count = result
                    .Where(x => x.CUIT.Contains(txtCliente.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarCliente = new Form_AdministracionBuscarCliente();
                    _formBuscarCliente.cuit = txtCliente.Text;
                    _formBuscarCliente.target = DevConstantes.OrdenVenta;
                    _formBuscarCliente.BuscarCuit();
                    _formBuscarCliente.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.CUIT.Equals(txtCliente.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ClienteId = busqueda.ID;
                        txtCliente.Text = busqueda.CLIENTE;
                    }
                    else
                    {
                        count = result
                            .Where(x => x.CLIENTE.Contains(txtCliente.Text))
                            .Count();
                        if (count > 1)
                        {
                            _formBuscarCliente = new Form_AdministracionBuscarCliente();
                            _formBuscarCliente.nombre = txtCliente.Text;
                            _formBuscarCliente.target = DevConstantes.OrdenVenta;
                            _formBuscarCliente.BuscarNombre();
                            _formBuscarCliente.ShowDialog(this);
                        }
                        else
                        {
                            var busquedaNombre = result
                                .Where(x => x.CLIENTE.Contains(txtCliente.Text))
                                .FirstOrDefault();

                            if (busquedaNombre != null)
                            {
                                ClienteId = busquedaNombre.ID;
                                txtCliente.Text = busquedaNombre.CLIENTE;
                            }
                        }
                    }
                }
            }
        }
    }
}