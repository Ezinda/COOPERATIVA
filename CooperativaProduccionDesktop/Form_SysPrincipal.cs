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
        #region Permisos
        private bool ReimpresionRomaneo;   
        private bool ResumenRomaneo;       
        private bool ResumenCompra;        
        private bool ResumenClaseMes;      
        private bool ResumenClaseTrimestre;
        private bool GestionReclasificacion;
        private bool Liquidar;
        private bool LiquidacionSubirAfip;
        private bool LiquidacionImprimir;
        #endregion

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

        private void btnFardos_ItemClick(object sender, ItemClickEventArgs e)
        {
            var fardos = new Form_InventarioInventarios();
            fardos.Show();
        }

        private void btnClasificacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var reclasificacion = new Form_RomaneoReclasificacion(null);
            reclasificacion.Show();
        }

        private void btnListaPrecio_ItemClick(object sender, ItemClickEventArgs e)
        {
            var listaPrecio = new Form_AdministracionListaPrecio();
            listaPrecio.Show();
        }

        private void btnLiquidacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var liquidacion = new Form_AdministracionLiquidacion(Liquidar,
                LiquidacionSubirAfip, LiquidacionImprimir);
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

        private void btnGestionCaja_ItemClick(object sender, ItemClickEventArgs e)
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

        private void btnPesada_ItemClick(object sender, ItemClickEventArgs e)
        {
            var pendiente = Context.Pesada
                .Where(x => x.RomaneoPendiente == true)
                .FirstOrDefault();

            if (pendiente != null)
            {
                var atencion = new Form_RomaneoPendiente(pendiente.Id);
                atencion.Show();
            }
            else
            {
                var pesada = new Form_RomaneoPesada(null);
                pesada.Show();
            }
        }

        private void btnGestionRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {
            var romaneo = new Form_RomaneoGestionRomaneo(ReimpresionRomaneo, ResumenRomaneo,
                ResumenCompra, ResumenClaseMes, ResumenClaseTrimestre);
            romaneo.Show();
        }

        private void btnGestionClasificacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gestionClasificacion = new Form_RomaneoGestionClasificacion(GestionReclasificacion);
            gestionClasificacion.Show();
        }

        private void btnAsignarRoles_ItemClick(object sender, ItemClickEventArgs e)
        {
            var permisos = new Form_SeguridadUsuarioPermiso();
            permisos.Show();
        }

        private void btnConfiguracionImpresion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var configuracion = new Form_ConfiguracionImpresion();
            configuracion.Show();
        }

        private void btnImpresionEtiqueta_ItemClick(object sender, ItemClickEventArgs e)
        {
            var impresion = new Form_ConfiguracionImpresionEtiqueta();
            impresion.Show();
        }

        private void btnIngresoCajas_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ingcajas = new Form_InventarioIngresoCaja();
            ingcajas.Show();
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
            var permisos = Context.Usuario
             .Where(x => x.Id == usuario.Id)
             .FirstOrDefault();

            #region Módulo Romaneo

            ReimpresionRomaneo = permisos.ReimpresionRomaneo.Value;
            ResumenRomaneo = permisos.ResumenRomaneo.Value;
            ResumenCompra = permisos.ResumenCompra.Value;
            ResumenClaseMes = permisos.ResumenClaseMes.Value;
            ResumenClaseTrimestre = permisos.ResumenClaseTrimestre.Value;
            GestionReclasificacion = permisos.GestionReclasificacion.Value;
            Liquidar = permisos.Liquidar.Value;
            LiquidacionSubirAfip = permisos.LiquidacionSubirAfip.Value;
            LiquidacionImprimir = permisos.LiquidacionImprimir.Value;
        
            if (permisos.Preingreso.Equals(false)
                && permisos.Pesada.Equals(false)
                && permisos.ReimpresionRomaneo.Equals(false)
                && permisos.ResumenRomaneo.Equals(false)
                && permisos.ResumenCompra.Equals(false)
                && permisos.ResumenClaseMes.Equals(false)
                && permisos.ResumenClaseTrimestre.Equals(false)
                && permisos.Reclasificacion.Equals(false)
                && permisos.GestionReclasificacion.Equals(false))
            {
                ribbonPageRomaneo.Visible = false;
            }
            else
            {
                ribbonPageRomaneo.Visible = true;
                ribbonPageGroupPorteria.Visible = permisos.Preingreso.Equals(true) ?
                    true : false;
                ribbonPageGroupBalanza.Visible = permisos.Pesada.Equals(true)
                    || permisos.ReimpresionRomaneo.Equals(true)
                    || permisos.ResumenRomaneo.Equals(true)
                    || permisos.ResumenCompra.Equals(true)
                    || permisos.ResumenClaseMes.Equals(true)
                    || permisos.ResumenClaseTrimestre.Equals(true) ? true : false;

                btnPesada.Visibility = permisos.Pesada.Equals(true) ?
                    BarItemVisibility.Always : BarItemVisibility.Never;

                btnGestionRomaneo.Visibility = permisos.ReimpresionRomaneo.Equals(true)
                    || permisos.ResumenRomaneo.Equals(true)
                    || permisos.ResumenCompra.Equals(true)
                    || permisos.ResumenClaseMes.Equals(true)
                    || permisos.ResumenClaseTrimestre.Equals(true) ?
                    BarItemVisibility.Always : BarItemVisibility.Never;

                btnClasificacion.Visibility = permisos.Reclasificacion.Equals(true) ?
                    BarItemVisibility.Always : BarItemVisibility.Never;

            }

            #endregion

            #region Módulo Administración

            if (permisos.Liquidar.Equals(false)
                && permisos.LiquidacionSubirAfip.Equals(false)
                && permisos.LiquidacionImprimir.Equals(false)
                && permisos.GenerarOrdenPago.Equals(false))
            {
                ribbonPageGroupLiquidacion.Visible = false;
            }
            else
            {
                ribbonPageGroupLiquidacion.Visible = true;

                btnLiquidacion.Visibility = permisos.Liquidar.Equals(true)
                || permisos.LiquidacionSubirAfip.Equals(true)
                || permisos.LiquidacionImprimir.Equals(true)
                || permisos.GenerarOrdenPago.Equals(true) ?
                BarItemVisibility.Always : BarItemVisibility.Never;

            }

            if (permisos.GestionCata.Equals(false)
                && permisos.GestionCaja.Equals(false)
                && permisos.GenerarOrdenVenta.Equals(false)
                && permisos.GenerarRemitoElectronico.Equals(false))
            {
                ribbonPageGroupOrdenVenta.Visible = false;
            }
            else
            {
                ribbonPageGroupOrdenVenta.Visible = true;

                btnGestionCata.Visibility = permisos.GestionCata.Equals(true) ?
                    BarItemVisibility.Always : BarItemVisibility.Never;

                btnGestionCaja.Visibility = permisos.GestionCaja.Equals(true) ?
                   BarItemVisibility.Always : BarItemVisibility.Never;

                btnOrdenVenta.Visibility = permisos.GenerarOrdenVenta.Equals(true) ?
                    BarItemVisibility.Always : BarItemVisibility.Never;

                btnRemitoElectronico.Visibility = permisos.GenerarRemitoElectronico.Equals(true) ?
                    BarItemVisibility.Always : BarItemVisibility.Never;
            }

            if (permisos.GestionarListaPrecio.Equals(false))
            {
                ribbonPageListaPrecio.Visible = false;
            }
            else
            {
                ribbonPageListaPrecio.Visible = true;
            }

            #endregion

            #region Módulo Configuracion

            if (permisos.Configuracion.Equals(false))
            {
                ribbonPageConfiguracion.Visible = false;
            }
            else
            {
                ribbonPageConfiguracion.Visible = true;
            }

            #endregion 

            #region Módulo Seguridad 

            if (permisos.Seguridad.Equals(false))
            {
                ribbonPageSeguridad.Visible = false;
            }
            else
            {
                ribbonPageSeguridad.Visible = true;
            }

            #endregion
            
        }

        private void CloseSessionForm()
        {
            SplashScreenManager.CloseForm();
        }

        #endregion

        private void btnTransferenciaMercaderia_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferencia = new Form_InventarioTransferencia();
            transferencia.Show();
        }

        private void btnTransferenciaMateriaPrima_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferencia = new Form_ProduccionTransferenciaMateriaPrima();
            transferencia.Show();
        }

        private void btnMuestras_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Form_ProduccionMuestras(new DataAccess.BlendManager()).Show();
        }

        private void btnControlDeTemperatura_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Form_ProduccionTemperatura(new DataAccess.BlendManager()).Show();
        }

        private void btnControlDeHumedad_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Form_ProduccionHumedad(new DataAccess.BlendManager()).Show();
        }

        private void btnControlDeNicotina_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Form_ProduccionNicotina(new DataAccess.BlendManager()).Show();
        }

        private void btnGestionTransferencias_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferencia = new Form_ProduccionGestionTransferencia();
            transferencia.Show();
        }

        private void btnConfiguracionBlend_ItemClick(object sender, ItemClickEventArgs e)
        {
            var config = new Form_ProduccionConfiguracionBlend();
            config.Show();
        }

        private void btnGestionProduccion_ItemClick(object sender, ItemClickEventArgs e)
        {
            var config = new Form_ProduccionGestionProduccion();
            config.Show();
        }
    }
}