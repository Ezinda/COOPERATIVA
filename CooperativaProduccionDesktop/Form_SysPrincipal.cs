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
using System.Threading;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraReports.UI;
using System.Net;
using System.IO;

namespace CooperativaProduccion
{
    public partial class Form_SysPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Usuario _CurrentUser;

        public Form_SysPrincipal()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            ShowSplashForm();
            _CurrentUser = CheckCredentials();
            SolicitarCredenciales(_CurrentUser);
            CloseSessionForm();
        }
        
        #region Method Code

        private void btnNuevoUsuario_ItemClick(object sender, ItemClickEventArgs e)
        {
            var nuevoUsuario = new Form_SeguridadNuevoUsuario();
            nuevoUsuario.Show();
        }

        private void btnVerUsuario_ItemClick(object sender, ItemClickEventArgs e)
        {
            var verUsuario = new Form_SeguridadGestionUsuario();
            verUsuario.Show();
        }

        private void btnPreingreso_ItemClick(object sender, ItemClickEventArgs e)
        {
            var preingreso = new Form_RomaneoPreingreso();
            preingreso.Show();
        }

        private void btnPesada_ItemClick(object sender, ItemClickEventArgs e)
        {
            var pesada = new Form_RomaneoPesada();
            pesada.Show();
        }

        private void btnFardos_ItemClick(object sender, ItemClickEventArgs e)
        {
            var fardos = new Form_InventarioFardos();
            fardos.Show();
        }

        private void btnClasificacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var reclasificacion = new Form_RomaneoReclasificacion();
            reclasificacion.Show();
        }

        private void btnListaPrecio_ItemClick(object sender, ItemClickEventArgs e)
        {
            var listaPrecio = new Form_AdministracionListaPrecio();
            listaPrecio.Show();
        }

        #endregion

        #region Method Dev

        private Usuario CheckCredentials()
        {
            Thread.Sleep(3200);

            SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SolicitarCredenciales, null);

            while (!SplashScreen1.Credenciales.Exitoso) ;

            return SplashScreen1.Credenciales.Usuario;
        }

        private void ShowSplashForm()
        {
            SplashScreenManager.ShowForm(this, typeof(SplashScreen1), true, true, false);
        }

        private void SolicitarCredenciales(Usuario usuario)
        {
            // #region Permisos de Empresas

            //var empresas = Context.UsuarioEmpresa
            //    .Where(x => x.UsuarioId.Equals(usuario.Id))
            //    .ToList();
            //foreach (var empresa in empresas)
            //{
            //    if (empresa.Empresa == "CMP")
            //    {
            //        CMP = empresa.Empresa;
            //    }
            //    else if (empresa.Empresa == "TDA")
            //    {
            //        TDA = empresa.Empresa;
            //    }
            //    else if (empresa.Empresa == "TDB")
            //    {
            //        TDB = empresa.Empresa;
            //    }
            //    else if (empresa.Empresa == "TDI")
            //    {
            //        TDI = empresa.Empresa;
            //    }
            //    else if (empresa.Empresa == "LACARTUJANA")
            //    {
            //        LACARTUJANA = empresa.Empresa;
            //    }
            //    else if (empresa.Empresa == "MARGESI")
            //    {
            //        MARGESI = empresa.Empresa;
            //    }
            //    else if (empresa.Empresa == "EMPAQUESDT")
            //    {
            //        EMPAQUESDT = empresa.Empresa;
            //    }
            //}
            //#endregion

            //var permisos = Context.Usuario
            //    .Where(x => x.Id == usuario.Id)
            //    .FirstOrDefault();

            //#region Permisos de Acceso a Cubos

            //if (permisos.GenerarCuboLiquidacion.Equals(false)
            //    && permisos.GenerarCuboEmpleado.Equals(false))
            //{
            //    ribbonPageGroupInformes.Visible = false;
            //}
            //else
            //{
            //    ribbonPageGroupInformes.Visible = true;
            //    btnCuboEmpleados.Visibility = permisos.GenerarCuboEmpleado.Equals(true) ?
            //            BarItemVisibility.Always : BarItemVisibility.Never;
            //    btnCuboLiquidaciones.Visibility = permisos.GenerarCuboLiquidacion.Equals(true) ?
            //            BarItemVisibility.Always : BarItemVisibility.Never;
            //}

            //#endregion

            //#region Permisos de Novedades

            //grabarNovedad = permisos.PuedeGrabarNovedad.Equals(true) ? true : false;
            //modificarNovedad = permisos.PuedeModificarNovedad.Equals(true) ? true : false;
            //eliminarNovedad = permisos.PuedeEliminarNovedad.Equals(true) ? true : false;
            //listarNovedad = permisos.PuedeListarNovedad.Equals(true) ? true : false;

            //grabarCapacitacion = permisos.PuedeGrabarCapacitacion.Equals(true) ? true : false;
            //modificarCapacitacion = permisos.PuedeModificarCapacitacion.Equals(true) ? true : false;
            //eliminarCapacitacion = permisos.PuedeEliminarCapacitacion.Equals(true) ? true : false;
            //listarCapacitacion = permisos.PuedeListarCapacitacion.Equals(true) ? true : false;

            //grabarEvaluacion = permisos.PuedeGrabarEvaluacion.Equals(true) ? true : false;
            //modificarEvaluacion = permisos.PuedeModificarEvaluacion.Equals(true) ? true : false;
            //eliminarEvaluacion = permisos.PuedeEliminarEvaluacion.Equals(true) ? true : false;
            //listarEvaluacion = permisos.PuedeListarEvaluacion.Equals(true) ? true : false;

            //grabarAccidente = permisos.PuedeGrabarAccidente.Equals(true) ? true : false;
            //modificarAccidente = permisos.PuedeModificarAccidente.Equals(true) ? true : false;
            //eliminarAccidente = permisos.PuedeEliminarAccidente.Equals(true) ? true : false;
            //listarAccidente = permisos.PuedeListarAccidente.Equals(true) ? true : false;

            //grabarSancion = permisos.PuedeGrabarSancion.Equals(true) ? true : false;
            //modificarSancion = permisos.PuedeModificarSancion.Equals(true) ? true : false;
            //eliminarSancion = permisos.PuedeEliminarSancion.Equals(true) ? true : false;
            //listarSancion = permisos.PuedeListarSancion.Equals(true) ? true : false;

            //#endregion

            //#region Permiso de Seguridad

            //ribbonSeguridad.Visible = permisos.PuedeAccederSeguridad.Equals(false) ?
            //    false : true;

            //#endregion

            //#region Permiso de Datos Empleado

            //accederPersonales = permisos.PuedeAccederPersonales.Equals(true) ? true : false;
            //accederContractuales = permisos.PuedeAccederContractuales.Equals(true) ? true : false;
            //accederOrganigrama = permisos.PuedeAccederOrganigrama.Equals(true) ? true : false;
            //accederPrevisionales = permisos.PuedeAccederPrevisionales.Equals(true) ? true : false;
            //accederFamiliares = permisos.PuedeAccederFamiliares.Equals(true) ? true : false;
            //accederLiquidaciones = permisos.PuedeAccederLiquidaciones.Equals(true) ? true : false;
            //accederNovedades = permisos.PuedeAccederNovedades.Equals(true) ? true : false;
            //accederCapacitaciones = permisos.PuedeAccederCapacitaciones.Equals(true) ? true : false;
            //accederEvaluaciones = permisos.PuedeAccederEvaluaciones.Equals(true) ? true : false;
            //accederAccidentes = permisos.PuedeAccederAccidentes.Equals(true) ? true : false;
            //accederSanciones = permisos.PuedeAccederSanciones.Equals(true) ? true : false;
            //accederAfip = permisos.PuedeAccederNovedadesAfip.Equals(true) ? true : false;

            //#endregion

        }

        private void CloseSessionForm()
        {
            SplashScreenManager.CloseForm();
        }

        #endregion

        private void btnLiquidacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var liquidacion = new Form_AdministracionLiquidacion();
            liquidacion.Show();
        }

        private void btnOrdenPago_ItemClick(object sender, ItemClickEventArgs e)
        {
            var op = new Form_AdministracionOrdenPago();
            op.Show();
        }

        private void btnGestionCata_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gc = new Form_AdministracionGestionCata();
            gc.Show();
        }

        private void btnIngresoCaja_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gc = new Form_AdministracionGestionCaja();
            gc.Show();
        }

        private void btnOrdenVenta_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ov = new Form_AdministracionOrdenVenta();
            ov.Show();
        }

        private void btnRemitoElectronico_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ov = new Form_AdministracionRemitoElectronico();
            ov.Show();
        }

    }
}