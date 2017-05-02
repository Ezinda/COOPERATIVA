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
using CooperativaProduccion.Helpers.GridRecords;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using CooperativaProduccion.Properties;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Drawing;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlaceActualizar,IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        public Form_AdministracionBuscarCliente _formBuscarCliente = null;
        public Guid OrdenVentaId;
        public Guid ClienteId;

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
            checkPendienteEmitirRemito.Checked = true;
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
            //Guid OrdenVentaId = new Guid(gridViewOrdenVentaConsulta
            //    .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "OrdenVentaId")
            //    .ToString());
            //var Pendiente = gridViewOrdenVentaConsulta
            //    .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Pendiente")
            //    .ToString();
            //if (Pendiente.Equals(DevConstantes.SI))
            //{
            //    var ordenventa = new Form_AdministracionActualizarOrdenVenta(OrdenVentaId);
            //    ordenventa.ShowDialog(this);
            //}
            //else
            //{
            //    MessageBox.Show("Esta orden de venta está cerrada. No se pueden modificar sus datos.",
            //        "Atención", MessageBoxButtons.OK);
            //}
        }

        private void btnBuscarPendientes_Click(object sender, EventArgs e)
        {
            BuscarPendientes();
        }

        private void BuscarPendientes()
        {
            if (OrdenVenta.SelectedTabPage.Equals(TabConsultaOrdenVenta))
            {
                List<GridOrdenVenta> lista = new List<GridOrdenVenta>();
    
                Expression<Func<OrdenVenta, bool>> pred = x => true;
                
                pred = pred.And(x => x.Fecha >= dpDesdeOrden.Value.Date
                    && x.Fecha <= dpHastaOrden.Value.Date);

                pred = !string.IsNullOrEmpty(txtCliente.Text) && ClienteId != Guid.Empty ? 
                    pred.And(x => x.ClienteId == ClienteId) : pred;

                pred = checkPendienteEmitirRemito.Checked ? 
                    pred.And(x => x.Pendiente == true) : pred;

                var ordenVenta =
                    (from o in Context.OrdenVenta.Where(pred)
                     join c in Context.Vw_Cliente
                     on o.ClienteId equals c.ID
                     select new
                     {
                         Id = o.Id,
                         Fecha = o.Fecha,
                         NumOperacion = o.NumOperacion,
                         NumOrden = o.NumOrden,
                         Cliente = c.RAZONSOCIAL,
                         Pendiente = o.Pendiente == true ?
                            DevConstantes.SI : DevConstantes.NO
                     })
                     .OrderBy(x => x.NumOrden)
                     .ToList();

                foreach (var item in ordenVenta)
                {
                    var detalle =
                        (from ovd in Context.OrdenVentaDetalle
                            .Where(x => x.OrdenVentaId == item.Id)
                         join p in Context.Vw_Producto
                         on ovd.ProductoId equals p.ID
                         select new
                         {
                             Id = ovd.Id,
                             Producto = p.DESCRIPCION,
                             DesdeCaja = ovd.DesdeCaja,
                             HastaCaja = ovd.HastaCaja
                         })
                        .ToList();

                    var rowsDetalle = detalle.Select(x =>
                       new GridOrdenVentaDetalle()
                       {
                           Id = x.Id,
                           Producto = x.Producto,
                           DesdeCaja = x.DesdeCaja,
                           HastaCaja = x.HastaCaja
                       })
                       .OrderBy(x => x.Producto)
                       .ToList();

                    var rowOrden = new GridOrdenVenta();
                    rowOrden.Id = item.Id;
                    rowOrden.Fecha = item.Fecha;
                    rowOrden.NumOperacion = item.NumOperacion;
                    rowOrden.NumOrden = item.NumOrden;
                    rowOrden.Cliente = item.Cliente;
                    rowOrden.Pendiente = item.Pendiente;
                    rowOrden.OrdenVentaDetalle = rowsDetalle;
                    lista.Add(rowOrden);
                }
                
                gridControlOrdenVentaConsulta.DataSource = new BindingList<GridOrdenVenta>(lista);
                gridViewOrdenVentaConsulta.Columns[0].Visible = false;
                gridViewOrdenVentaConsulta.Columns[1].Caption = "Fecha";
                gridViewOrdenVentaConsulta.Columns[1].Width = 90;
                gridViewOrdenVentaConsulta.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[2].Caption = "N° Operación";
                gridViewOrdenVentaConsulta.Columns[2].Width = 120;
                gridViewOrdenVentaConsulta.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gridViewOrdenVentaConsulta.Columns[3].Caption = "N° Orden";
                gridViewOrdenVentaConsulta.Columns[3].Width = 120;
                gridViewOrdenVentaConsulta.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gridViewOrdenVentaConsulta.Columns[4].Caption = "Cliente";
                gridViewOrdenVentaConsulta.Columns[4].Width = 250;
                gridViewOrdenVentaConsulta.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                gridViewOrdenVentaConsulta.Columns[5].Caption = "Pendiente";
                gridViewOrdenVentaConsulta.Columns[5].Width = 120;
                gridViewOrdenVentaConsulta.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewOrdenVentaConsulta.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
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

            var orden = (from o in Context.OrdenVenta
                            .Where(x => x.Id == OrdenVentaId)
                         join c in Context.Vw_Cliente
                         on o.ClienteId equals c.ID
                         select
                         new {
                             Cliente = c.RAZONSOCIAL
                         })
                         .FirstOrDefault();

            string fileName = @"C:\SystemDocumentsCooperativa\TxtResumen\Resumen_" + orden.Cliente + ".txt";

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
            Form_AdministracionNuevaOrdenVenta nuevaOrden = new Form_AdministracionNuevaOrdenVenta(null);
            nuevaOrden.ShowDialog(this);
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

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                BuscarCliente();
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }

        public void Enviar(Guid Id, string fet, string nombre)
        {
            ClienteId = Id;
            txtCliente.Text = nombre;
        }

        private void gridViewOrdenVentaConsulta_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView master = sender as GridView;
            GridView detail = master.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detail.Columns[0].Visible = false;
            detail.Columns[1].Width = 120;
            detail.Columns[1].OptionsColumn.AllowEdit = false;
            detail.Columns[2].OptionsColumn.AllowEdit = false;
            detail.Columns[3].OptionsColumn.AllowEdit = false;
            detail.DoubleClick += gridViewOrdenVentaDetalleConsulta_DoubleClick;
        }

        private void Form_AdministracionOrdenVenta_Load(object sender, EventArgs e)
        {
            gridControlOrdenVentaConsulta.DataSource = new BindingList<GridOrdenVenta>();
            gridViewOrdenVentaConsulta.Columns[0].Visible = false;
            gridViewOrdenVentaConsulta.Columns[1].Caption = "N° Operación";
            gridViewOrdenVentaConsulta.Columns[1].Width = 120;
            gridViewOrdenVentaConsulta.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenVentaConsulta.Columns[1].OptionsColumn.AllowEdit = false;
            gridViewOrdenVentaConsulta.Columns[2].Caption = "N° Orden";
            gridViewOrdenVentaConsulta.Columns[2].Width = 120;
            gridViewOrdenVentaConsulta.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewOrdenVentaConsulta.Columns[2].OptionsColumn.AllowEdit = false;
            gridViewOrdenVentaConsulta.Columns[3].Caption = "Cliente";
            gridViewOrdenVentaConsulta.Columns[3].Width = 250;
            gridViewOrdenVentaConsulta.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridViewOrdenVentaConsulta.Columns[3].OptionsColumn.AllowEdit = false;
            gridViewOrdenVentaConsulta.Columns[4].Caption = "Fecha";
            gridViewOrdenVentaConsulta.Columns[4].Width = 90;
            gridViewOrdenVentaConsulta.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[4].OptionsColumn.AllowEdit = false;
            gridViewOrdenVentaConsulta.Columns[5].Caption = "Pendiente";
            gridViewOrdenVentaConsulta.Columns[5].Width = 120;
            gridViewOrdenVentaConsulta.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewOrdenVentaConsulta.Columns[5].OptionsColumn.AllowEdit = false;

            RepositoryItemButtonEdit buttonEditVerOV = new RepositoryItemButtonEdit();
            buttonEditVerOV.Buttons[0].Kind = ButtonPredefines.Search;
            buttonEditVerOV.TextEditStyle = TextEditStyles.HideTextEditor;
            buttonEditVerOV.ButtonClick += new ButtonPressedEventHandler(buttonEditVerOV_ButtonClick);

            GridColumn unbColumnModificarOV = gridViewOrdenVentaConsulta.Columns.AddField("Ver");
            unbColumnModificarOV.Caption = "Orden de Venta";
            unbColumnModificarOV.Width = 100;
            unbColumnModificarOV.UnboundType = DevExpress.Data.UnboundColumnType.String;
            unbColumnModificarOV.VisibleIndex = gridViewOrdenVentaConsulta.Columns.Count;
            unbColumnModificarOV.ColumnEdit = buttonEditVerOV;
            
            RepositoryItemButtonEdit buttonEditAgregar = new RepositoryItemButtonEdit();
            buttonEditAgregar.Buttons[0].Kind = ButtonPredefines.Plus;
            buttonEditAgregar.TextEditStyle = TextEditStyles.HideTextEditor;
            buttonEditAgregar.ButtonClick += new ButtonPressedEventHandler(buttonEditAgregar_ButtonClick);

            GridColumn unbColumnAgregar = gridViewOrdenVentaConsulta.Columns.AddField("Agregar");
            unbColumnAgregar.Caption = "Agregar Cajas";
            unbColumnAgregar.Width = 100;
            unbColumnAgregar.UnboundType = DevExpress.Data.UnboundColumnType.String;
            unbColumnAgregar.VisibleIndex = gridViewOrdenVentaConsulta.Columns.Count;
            unbColumnAgregar.ColumnEdit = buttonEditAgregar;
            
        }

        private void buttonEditVerOV_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit ed = gridViewOrdenVentaConsulta.ActiveEditor as ButtonEdit;
            if (ed == null) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
            {
                Guid OrdenVentaId = new Guid(gridViewOrdenVentaConsulta
                      .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Id")
                      .ToString());

                var Pendiente = gridViewOrdenVentaConsulta
                    .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Pendiente")
                    .ToString();

                if (Pendiente.Equals(DevConstantes.SI))
                {
                    var ordenventa = new Form_AdministracionNuevaOrdenVenta(OrdenVentaId);
                    ordenventa.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Esta orden de venta está cerrada. No se pueden modificar sus datos.",
                        "Atención", MessageBoxButtons.OK);
                }
            }
        }

        private void buttonEditAgregar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit ed = gridViewOrdenVentaConsulta.ActiveEditor as ButtonEdit;
            if (ed == null) return;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Guid OrdenVentaId = new Guid(gridViewOrdenVentaConsulta
                    .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Id")
                    .ToString());

                var Pendiente = gridViewOrdenVentaConsulta
                    .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Pendiente")
                    .ToString();

                if (Pendiente.Equals(DevConstantes.SI))
                {
                    var ordenventa = new Form_AdministracionActualizarOrdenVenta(OrdenVentaId,true);
                    ordenventa.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Esta orden de venta está cerrada. No se pueden modificar sus datos.",
                        "Atención", MessageBoxButtons.OK);
                }
            }
        }

        private void gridViewOrdenVentaDetalleConsulta_DoubleClick(object sender, EventArgs e)
        {
            Guid OrdenVentaId = new Guid(gridViewOrdenVentaConsulta
               .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Id")
               .ToString());

            var Pendiente = gridViewOrdenVentaConsulta
                .GetRowCellValue(gridViewOrdenVentaConsulta.FocusedRowHandle, "Pendiente")
                .ToString();

            if (Pendiente.Equals(DevConstantes.SI))
            {
                var ordenventa = new Form_AdministracionActualizarOrdenVenta(OrdenVentaId, false);
                ordenventa.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Esta orden de venta está cerrada. No se pueden modificar sus datos.",
                    "Atención", MessageBoxButtons.OK);
            }
        }
    }
}