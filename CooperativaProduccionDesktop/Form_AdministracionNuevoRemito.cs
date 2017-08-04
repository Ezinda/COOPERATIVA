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
using System.Runtime.InteropServices;
using System.Security.Principal;
using EntityFramework.Extensions;

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

        #region Method Code

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

        #endregion

        #region Dev

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
                txtNumRemito.Enabled = false;
                btnSeleccionarPdf.Enabled = false;
                btnBorrarPdf.Enabled = false;
                btnGrabarRemito.Enabled = false;
            }
        }

        private void CargarDatos(Guid Id, bool Orden)
        {
            if (Orden.Equals(true))
            {
                var ordenVenta =
                 (from o in Context.OrdenVenta
                      .Where(x => x.Id == Id)
                      .AsEnumerable()
                  join c in Context.Vw_Cliente
                  on o.ClienteId equals c.ID
                  select new
                  {
                      OrdenVentaId = o.Id,
                      NumOperacion = o.NumOperacion,
                      NumOrden = o.NumOrden,
                      Cliente = c.RAZONSOCIAL,
                      Cuit = c.CUIT.ToUpper().Contains(DevConstantes.XX) ? c.CUITE : c.CUIT,
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
                         .Where(x => x.OrdenVentaId == OrdenVentaId)
                         .AsEnumerable()
                     join p in Context.Vw_Producto
                     on o.ProductoId equals p.ID
                     select new
                     {
                         Campaña = o.Campaña,
                         Producto = p.DESCRIPCION,
                         Desde = o.DesdeCaja,
                         Hasta = o.HastaCaja
                     })
                    .ToList();

                if (detalle != null)
                {
                    gridControlDetalleOrdenVenta.DataSource = detalle;
                    gridViewDetalleOrdenVenta.Columns[0].Caption = "Campaña";
                    gridViewDetalleOrdenVenta.Columns[1].Caption = "Producto";
                    gridViewDetalleOrdenVenta.Columns[2].Caption = "Caja Desde";
                    gridViewDetalleOrdenVenta.Columns[3].Caption = "Caja Hasta";
                }
                dpRemito.Focus();
            }
            else
            {
                var remito =
                    (from r in Context.Remito
                         .Where(x => x.Id == Id)
                         .AsEnumerable()
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
                         OrdenVentaId = r.OrdenVentaId,
                         Cliente = c.RAZONSOCIAL,
                         Cuit = c.CUIT.ToUpper().Contains(DevConstantes.XX) ? c.CUITE : c.CUIT,
                         PathSystem = r.PathSystem,
                         PathOrigin = r.PathOrigin,
                     })
                     .OrderBy(x => x.NumRemito)
                     .FirstOrDefault();

                if (remito != null)
                {
                    txtNumOperacion.Text = remito.NumOperacion.ToString();
                    dpOrdenVenta.Value = remito.FechaOrden.Value;
                    txtCliente.Text = remito.Cliente;
                    txtCuit.Text = remito.Cuit;
                    dpRemito.Value = remito.FechaRemito.Value;
                    txtPuntoVenta.Text = remito.PuntoVenta.ToString().PadLeft(4, '0');
                    txtNumRemito.Text = remito.NumRemito.ToString();
                    path = File.Exists(remito.PathSystem) ?
                        remito.PathSystem :
                        remito.PathOrigin;
                    txtNombrePdf.Text = Path.GetFileName(path);
                }
                                
                var detalle =
                (from o in Context.OrdenVentaDetalle
                     .Where(x => x.OrdenVentaId == remito.OrdenVentaId )
                     .AsEnumerable()
                 join p in Context.Vw_Producto
                 on o.ProductoId equals p.ID
                 select new
                 {
                     Campaña = o.Campaña,
                     Producto = p.DESCRIPCION,
                     Desde = o.DesdeCaja,
                     Hasta = o.HastaCaja
                 })
                .ToList();

                gridControlDetalleOrdenVenta.DataSource = detalle;
                gridViewDetalleOrdenVenta.Columns[0].Caption = "Campaña";
                gridViewDetalleOrdenVenta.Columns[0].Width = 120;
                gridViewDetalleOrdenVenta.Columns[0].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[0].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[1].Caption = "Producto";
                gridViewDetalleOrdenVenta.Columns[1].Width = 120;
                gridViewDetalleOrdenVenta.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[2].Caption = "Caja Desde";
                gridViewDetalleOrdenVenta.Columns[2].Width = 120;
                gridViewDetalleOrdenVenta.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[3].Caption = "Caja Hasta";
                gridViewDetalleOrdenVenta.Columns[3].Width = 120;
                gridViewDetalleOrdenVenta.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewDetalleOrdenVenta.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            }
        }

        private void PrevisualizarPdf()
        {
            if (path != string.Empty)
            {
                //RemoteCredentialsClass rcADM =
                //    new RemoteCredentialsClass(@"\\SERVER-ADM\SystemDocumentsCooperativa\RemitosElectronicos\", "Administrador", "Coopat123");
                //try
                //{
                    //var pathAdm = @"\\SERVER-ADM\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text;

                    //if (File.Exists(pathAdm))
                    //{
                    //    System.Diagnostics.Process.Start(@"\\SERVER-ADM\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text);
                    //}
                    //else
                    //{
                        RemoteCredentialsClass rcPlanta =
                            new RemoteCredentialsClass(@"\\SERVER-PLANTA\SystemDocumentsCooperativa\RemitosElectronicos\", "Administrador", "Coopat123");
                        try
                        {
                            var pathPlanta = @"\\SERVER-PLANTA\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text;

                            if (File.Exists(pathPlanta))
                            {
                                System.Diagnostics.Process.Start(@"\\SERVER-PLANTA\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text);
                            }
                            else
                            {
                                MessageBox.Show("Archivo no encontrado.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        finally
                        {
                            rcPlanta.CloseUNC();
                        }
                    //}
                //}
                //finally
                //{
                //    rcADM.CloseUNC();
                //}
            //}
            //else
            //{
            //    MessageBox.Show("Se debe adjuntar el remito electronico (Archivo pdf)",
            //               "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    .Where(x => x.Id == OrdenVentaId)
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
                    string pathSystem = @"C:\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text;
                    remito.PathSystem = pathSystem;
                                       
                    Context.Remito.Add(remito);
                    Context.SaveChanges();

                    var orden = Context.OrdenVenta.Find(ordenVenta.Id);

                    if (orden != null)
                    {
                        orden.Pendiente = false;

                        Context.Entry(orden).State = EntityState.Modified;
                        Context.SaveChanges();

                        var detalles = Context.OrdenVentaDetalle
                           .Where(x => x.OrdenVentaId == ordenVenta.Id)
                           .ToList();

                        foreach (var item in detalles)
                        {
                            for (long i = item.DesdeCaja.Value; i <= item.HastaCaja; i++)
                            {
                                var caja = Context.Caja
                                    .Where(x => x.Campaña == item.Campaña
                                        && x.ProductoId == item.ProductoId
                                        && x.NumeroCaja == i)
                                    .FirstOrDefault();

                                UpdateMovimientoActual(caja.Id);
                                RegistrarMovimiento(caja.Id, 1, remito.FechaRemito.Value);
                            }
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

        private void UpdateMovimientoActual(Guid Id)
        {
            var movimiento = Context.Movimiento
                 .Where(x => x.TransaccionId == Id)
                     .Update(x => new Movimiento() { Actual = false });

            Context.SaveChanges();
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
            movimiento.Actual = true;
            movimiento.Anulado = false;

            var depositoId = Context.Movimiento
                .Where(x => x.TransaccionId == Id)
                .OrderByDescending(x => x.Fecha)
                .Select(x => x.DepositoId)
                .FirstOrDefault();

            if (depositoId != null)
            {
                movimiento.DepositoId = depositoId;
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
            RemoteCredentialsClass rcPlanta =
                          new RemoteCredentialsClass(@"\\SERVER-PLANTA\SystemDocumentsCooperativa\RemitosElectronicos\", "Administrador", "Coopat123");
            try
            {
                var pathPlanta = @"\\SERVER-PLANTA\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text;

                if (path != string.Empty)
                {
                    File.Copy(path, pathPlanta, true);
                }
                else
                {
                    MessageBox.Show("Se debe adjuntar el remito electronico (Archivo pdf)",
                        "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                rcPlanta.CloseUNC();
            }

            string pathFile = @"C:\SystemDocumentsCooperativa\RemitosElectronicos\" + txtNombrePdf.Text;
            File.Copy(path, pathFile, true);
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

        #endregion

    }

    public class RemoteCredentialsClass
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        // logon types
        const int LOGON32_LOGON_INTERACTIVE = 2;
        const int LOGON32_LOGON_NETWORK = 3;
        const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        // logon providers
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_PROVIDER_WINNT50 = 3;
        const int LOGON32_PROVIDER_WINNT40 = 2;
        const int LOGON32_PROVIDER_WINNT35 = 1;


        private static void RaiseLastError()
        {
            int errorCode = Marshal.GetLastWin32Error();
            string errorMessage = "IO Error: " + errorCode.ToString();

            throw new ApplicationException(errorMessage);
        }

        IntPtr token = IntPtr.Zero;
        WindowsImpersonationContext impersonatedUser = null;

        public RemoteCredentialsClass(string server, string user, string pwd)
        {
            if (user.IndexOf("\\") > 0)
            {
                server = user.Substring(0, user.IndexOf("\\"));
                if (user.Length > server.Length + 1) user = user.Substring(server.Length + 1);
            }
            bool isSuccess = LogonUser(user, server, pwd, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref token);
            if (!isSuccess)
            {
                RaiseLastError();
            }

            WindowsIdentity newIdentity = new WindowsIdentity(token);
            impersonatedUser = newIdentity.Impersonate();
        }

        public void CloseUNC()
        {
            if (impersonatedUser == null) return;
            impersonatedUser.Undo();

            bool isSuccess = CloseHandle(token);
            if (!isSuccess)
            {
                RaiseLastError();
            }
        }
    }
}