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
using DevExpress.Utils;
using System.IO;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionNuevoRemito : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private string path = string.Empty;
        private Guid OrdenVentaId;

        public Form_AdministracionNuevoRemito(Guid OrdenVentaId, bool Orden)
        {
            InitializeComponent();
            Iniciar(Orden);
            Context = new CooperativaProduccionEntities();
            CargarDatos(OrdenVentaId,Orden);
        }

        private void Iniciar(bool Orden)
        {
            if (Orden.Equals(true))
            {
                dpRemito.Enabled = true;
                txtPuntoVenta.Enabled = true;
                txtNumRemito.Enabled = true;
                btnSeleccionarPdf.Enabled = true;
                btnBorrarPdf.Enabled = true;
                btnGrabarRemito.Enabled = true;
            }
            else
            {
                dpRemito.Enabled = false;
                txtPuntoVenta.Enabled = false;
                txtNumRemito.Enabled =  false;
                btnSeleccionarPdf.Enabled = false;
                btnBorrarPdf.Enabled = false;
                btnGrabarRemito.Enabled = false;
            }
        }

        private void CargarDatos(Guid Id,bool Orden)
        {
            if (Orden.Equals(true))
            {
                var ordenVenta =
                 (from o in Context.OrdenVenta
                      .Where(x => x.Id == Id).AsEnumerable()

                  join c in Context.Vw_Cliente
                  on o.ClienteId equals c.ID
                  select new
                  {
                      OrdenVentaId = o.Id,
                      NumOperacion = o.NumOperacion,
                      NumOrden = o.NumOrden,
                      Cliente = c.RAZONSOCIAL,
                      Cuit = c.CUIT.Contains(DevConstantes.XX) ? c.CUITE : c.CUIT,
                      Fecha = o.Fecha
                  })
                 .FirstOrDefault();
                
                if (ordenVenta != null)
                {
                    txtNumOperacion.Text = ordenVenta.NumOperacion.ToString();
                    dpOrdenVenta.Value = ordenVenta.Fecha;
                    txtCliente.Text = ordenVenta.Cliente;
                    txtCuit.Text = ordenVenta.Cuit;
                    OrdenVentaId = ordenVenta.OrdenVentaId;
                }

                var detalle =
                    (from o in Context.OrdenVentaDetalle
                         .Where(x => x.OrdenVentaId == Id).AsEnumerable()
                     join p in Context.Vw_Producto
                     on o.ProductoId equals p.ID
                     select new
                     {
                         Producto = p.DESCRIPCION,
                         Desde = o.DesdeCaja,
                         Hasta = o.HastaCaja
                     })
                    .ToList();
                
                if (detalle != null)
                {
                    gridControlDetalleOrdenVenta.DataSource = detalle;
                    gridViewDetalleOrdenVenta.Columns[0].Caption = "Producto";
                    gridViewDetalleOrdenVenta.Columns[1].Caption = "Caja Desde";
                    gridViewDetalleOrdenVenta.Columns[2].Caption = "Caja Hasta";
                    
                    
                    
                }
                dpRemito.Focus();
            }
            else
            {
                var remito =
                    (from r in Context.Remito
                         .Where(x => x.Id == Id).AsEnumerable()
                     join p in Context.Vw_Producto
                     on r.ProductoId equals p.ID
                     join c in Context.Vw_Cliente
                     on r.ClienteId equals c.ID
                     select new
                     {
                         RemitoId = r.Id,
                         FechaRemito = r.FechaRemito,
                         FechaOrden = r.FechaOrden,
                         PuntoVenta = r.PuntoVenta.ToString().PadLeft(4, '0'),
                         NumRemito = r.NumRemito,
                         NumOperacion = r.NumOperacion,
                         NumOrden = r.NumOrden,
                         Cliente = c.RAZONSOCIAL,
                         Cuit = c.CUIT.Contains(DevConstantes.XX) ? c.CUITE : c.CUIT,
                         Producto = p.DESCRIPCION,
                         CajaDesde = r.DesdeCaja,
                         CajaHasta = r.HastaCaja,
                         PathSystem = r.PathSystem,
                         PathOrigin = r.PathOrigin,
                     })
                     .OrderBy(x => x.NumRemito)
                     .ToList();

                gridControlDetalleOrdenVenta.DataSource = remito;
                gridViewDetalleOrdenVenta.Columns[0].Visible = false;
                gridViewDetalleOrdenVenta.Columns[1].Visible = false;
                gridViewDetalleOrdenVenta.Columns[2].Visible = false;
                gridViewDetalleOrdenVenta.Columns[3].Visible = false;
                gridViewDetalleOrdenVenta.Columns[4].Visible = false;
                gridViewDetalleOrdenVenta.Columns[5].Visible = false;
                gridViewDetalleOrdenVenta.Columns[6].Visible = false;
                gridViewDetalleOrdenVenta.Columns[7].Visible = false;
                gridViewDetalleOrdenVenta.Columns[8].Visible = false;
                gridViewDetalleOrdenVenta.Columns[9].Caption = "Producto";
                gridViewDetalleOrdenVenta.Columns[9].Width = 120;
                gridViewDetalleOrdenVenta.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[10].Caption = "Caja Desde";
                gridViewDetalleOrdenVenta.Columns[10].Width = 120;
                gridViewDetalleOrdenVenta.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[11].Caption = "Caja Hasta";
                gridViewDetalleOrdenVenta.Columns[11].Width = 120;
                gridViewDetalleOrdenVenta.Columns[11].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[11].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[12].Visible = false;
                gridViewDetalleOrdenVenta.Columns[13].Visible = false;

                if (remito != null)
                {
                    txtNumOperacion.Text = remito.FirstOrDefault().NumOperacion.ToString();
                    dpOrdenVenta.Value = remito.FirstOrDefault().FechaOrden.Value;
                    txtCliente.Text = remito.FirstOrDefault().Cliente;
                    txtCuit.Text = remito.FirstOrDefault().Cuit;
                    dpRemito.Value = remito.FirstOrDefault().FechaRemito.Value;
                    txtPuntoVenta.Text = remito.FirstOrDefault().PuntoVenta.ToString().PadLeft(4, '0');
                    txtNumRemito.Text = remito.FirstOrDefault().NumRemito.ToString();
                    path = System.IO.File.Exists(remito.FirstOrDefault().PathSystem) ? 
                        remito.FirstOrDefault().PathSystem : 
                        remito.FirstOrDefault().PathOrigin;
                    txtNombrePdf.Text = Path.GetFileName(path);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dpRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPuntoVenta.Focus();
            }
        }

        private void txtPuntoVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNumRemito.Focus();
            }
        }

        private void txtNumRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSeleccionarPdf.Focus();
            }
        }

        private void btnSeleccionarPdf_Click(object sender, EventArgs e)
        {
            this.ofd.Filter = "Pdf Files|*.pdf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
                txtNombrePdf.Text = Path.GetFileName(ofd.FileName);
                
            }
        }

        private void btnBorrarPdf_Click(object sender, EventArgs e)
        {
            txtNombrePdf.Text = string.Empty;
            path = string.Empty;
        }

        private void btnPrevisualizarPdf_Click(object sender, EventArgs e)
        {
            PrevisualizarPdf();
        }

        private void PrevisualizarPdf()
        {
            if (path != string.Empty)
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                MessageBox.Show("Se debe adjuntar el remito electronico (Archivo pdf)",
                           "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGrabarRemito_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                var resultado = MessageBox.Show("¿Desea grabar el remito?",
                     "Atención", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GrabarRemito();
                MessageBox.Show("Remito Grabado",
                    "Confirmación", MessageBoxButtons.OKCancel);
                this.Close();
            }
        }

        private void GrabarRemito()
        {
            try
            {
                //Crea Carpeta SystemDocumentsCooperativa.
                CreateFolder();
                //Copia el Remito Electronico en dicha direccion.
                CopyFile();

                var ordenVenta = Context.OrdenVenta
                    .Where(x=>x.Id == OrdenVentaId)
                    .FirstOrDefault();

                if (ordenVenta != null)
                {
                    Remito remito;
                    remito = new Remito();
                    remito.Id = Guid.NewGuid();
                    remito.NumOperacion = ordenVenta.NumOperacion;
                    remito.NumOrden = ordenVenta.NumOrden;
                    remito.ClienteId = ordenVenta.ClienteId;
                    remito.FechaOrden = ordenVenta.Fecha;
                    remito.FechaRemito = dpRemito.Value;
                    remito.OrdenVentaId = ordenVenta.Id;
                    remito.PuntoVenta = int.Parse(txtPuntoVenta.Text);
                    remito.NumRemito = int.Parse(txtNumRemito.Text);
                    remito.PathOrigin = path;
                    string pathSystem = @"C:\SystemDocumentsCooperativa\RemitosElectronicos\"+txtNombrePdf.Text;
                    remito.PathSystem = pathSystem;
                    remito.File = FileConvertToByte(path);
                    var detalle = Context.OrdenVentaDetalle
                           .Where(x => x.OrdenVentaId == ordenVenta.Id)
                           .FirstOrDefault();

                    if (detalle != null)
                    {
                        remito.ProductoId = detalle.ProductoId;
                        remito.DesdeCaja = detalle.DesdeCaja;
                        remito.HastaCaja = detalle.HastaCaja;
                    }
                    Context.Remito.Add(remito);
                    Context.SaveChanges();

                    var orden = Context.OrdenVenta.Find(ordenVenta.Id);
                    if (orden != null)
                    {
                        orden.Pendiente = false;
                       
                        Context.Entry(orden).State = EntityState.Modified;
                        Context.SaveChanges();

                        for (long i = detalle.DesdeCaja.Value; i <= detalle.HastaCaja; i++)
                        {
                            var caja = Context.Caja
                                .Where(x =>x.Campaña == detalle.Campaña 
                                    && x.ProductoId == detalle.ProductoId 
                                    && x.NumeroCaja == i)
                                .FirstOrDefault();

                            RegistrarMovimiento(caja.Id, 1, remito.FechaRemito.Value);
                        }
                    }
                    IEnlaceActualizar mienlace = this.Owner as Form_AdministracionRemitoElectronico;
                    if (mienlace != null)
                    {
                        mienlace.Enviar(true);
                    }
                    this.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        private Guid RegistrarMovimiento(Guid Id, double kilos, DateTime fecha)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = fecha;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Remito;
            movimiento.Unidad = DevConstantes.Caja;
            movimiento.Ingreso = 0;
            movimiento.Egreso = kilos;

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

        public byte[] FileConvertToByte(string varFilePath)
        {
            byte[] file;
            
            using (var stream = new FileStream(varFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                   return file = reader.ReadBytes((int)stream.Length);
                }
            }
        }

        private bool Validar()
        {
            if (path == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un remito electrónico.",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtNombrePdf.Text == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un remito electrónico.",
                          "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int fail = 0;
            bool successfullyParsed;

            successfullyParsed = int.TryParse(txtPuntoVenta.Text, out fail);
            if (!successfullyParsed)
            {
                MessageBox.Show("Debe ingresar un punto de venta válido.",
                      "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            successfullyParsed = int.TryParse(txtNumRemito.Text, out fail);
            if (!successfullyParsed)
            {
                MessageBox.Show("Debe ingresar un punto de venta válido.",
                      "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void CreateFolder()
        {
            string pathFile = @"C:\SystemDocumentsCooperativa";
            CreateIfMissing(pathFile);
            pathFile = @"C:\SystemDocumentsCooperativa\RemitosElectronicos";
            CreateIfMissing(pathFile);
        }
     
        private void CopyFile()
        {
            string pathFile = @"C:\SystemDocumentsCooperativa\RemitosElectronicos\"+txtNombrePdf.Text;
            if (path != string.Empty)
            {
                System.IO.File.Copy(path, pathFile, true);
            }
            else
            {
                MessageBox.Show("Se debe adjuntar el remito electronico (Archivo pdf)",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}