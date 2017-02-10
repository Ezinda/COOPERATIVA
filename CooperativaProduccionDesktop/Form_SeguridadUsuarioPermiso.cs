using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DesktopEntities.Models;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_SeguridadUsuarioPermiso : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        public Guid UsuarioId;
        public Guid UsuarioEmpresaId;
        
        public Form_SeguridadUsuarioPermiso()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        private void Sys_FormUsuarioPermiso_Load(object sender, EventArgs e)
        {
            ListarUsuarios();
        }

        private void ListarUsuarios()
        {
            var result = (
               from a in Context.Usuario
               select new
               {
                   Id = a.Id,
                   Nombre = a.Nombre,
                   Apellido = a.Apellido,
                   Usuario = a.Usuario1
               }).ToList();

            gridControlUsuario.DataSource = result;
            gridViewUsuario.Columns[0].Visible = false;
            gridViewUsuario.Columns[1].Width = 100;
            gridViewUsuario.Columns[2].Width = 100;
            gridViewUsuario.Columns[3].Width = 100;

        }
 
        private void gridControlUsuario_DoubleClick(object sender, EventArgs e)
        {
            UsuarioId = new Guid(gridViewUsuario
                .GetRowCellValue(gridViewUsuario.FocusedRowHandle, "Id")
                .ToString());
            CargarPermisos(UsuarioId);
         }

        private void CargarPermisos(Guid UsuarioId)
        {
            var dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
            
            var permisos = Context.Usuario
                .Where(x => x.Id == UsuarioId);
       
            if (permisos.Count() > 0)
            {
                #region Módulo Romaneo

                var permiso = permisos.FirstOrDefault();

                checkPreingreso.Checked = permiso.Preingreso.Equals(true) ?
                    true : false;

                checkPesada.Checked = permiso.Pesada.Equals(true) ?
                    true : false;
                
                if (permiso.ReimpresionRomaneo == true)
                {
                    checkListRomaneo.SetItemChecked(0, true);
                }
                else
                {
                    checkListRomaneo.SetItemChecked(0, false);
                }

                if (permiso.ResumenRomaneo == true)
                {
                    checkListRomaneo.SetItemChecked(1, true);
                }
                else
                {
                    checkListRomaneo.SetItemChecked(1, false);
                }

                if (permiso.ResumenCompra == true)
                {
                    checkListRomaneo.SetItemChecked(2, true);
                }
                else
                {
                    checkListRomaneo.SetItemChecked(2, false);
                }

                if (permiso.ResumenClaseMes == true)
                {
                    checkListRomaneo.SetItemChecked(3, true);
                }
                else
                {
                    checkListRomaneo.SetItemChecked(3, false);
                }

                if (permiso.ResumenClaseTrimestre == true)
                {
                    checkListRomaneo.SetItemChecked(4, true);
                }
                else
                {
                    checkListRomaneo.SetItemChecked(4, false);
                }

                checkReclasificacion.Checked = permiso.Reclasificacion.Equals(true) ?
                  true : false;

                checkGestionReclasificacion.Checked = permiso.GestionReclasificacion.Equals(true) ?
                  true : false;

                #endregion

                #region Módulo Seguridad

                checkSeguridad.Checked = permiso.Seguridad.Equals(true) ? 
                    true : false; 

                #endregion

                #region Módulo Configuracion

                checkConfiguracion.Checked = permiso.Configuracion.Equals(true) ? 
                    true : false;

                #endregion

                #region Módulo de Administracion

                if (permiso.Liquidar == true)
                {
                    checkListLiquidacion.SetItemChecked(0, true);
                }
                else
                {
                    checkListLiquidacion.SetItemChecked(0, false);
                }

                if (permiso.LiquidacionSubirAfip == true)
                {
                    checkListLiquidacion.SetItemChecked(1, true);
                }
                else
                {
                    checkListLiquidacion.SetItemChecked(1, false);
                }

                if (permiso.LiquidacionImprimir == true)
                {
                    checkListLiquidacion.SetItemChecked(2, true);
                }
                else
                {
                    checkListLiquidacion.SetItemChecked(2, false);
                }

                checkGenerarOrdenPago.Checked = permiso.GenerarOrdenPago.Equals(true) ? 
                    true : false;

                checkGestionCata.Checked = permiso.GestionCata.Equals(true) ?
                    true : false;

                checkGestionCaja.Checked = permiso.GestionCaja.Equals(true) ?
                    true : false;

                checkGenerarOrdenVenta.Checked = permiso.GenerarOrdenVenta.Equals(true) ?
                    true : false;

                checkGenerarRemitoElectronico.Checked = permiso.GenerarRemitoElectronico.Equals(true) ?
                    true : false;

                checkListaPrecio.Checked = permiso.GestionarListaPrecio.Equals(true) ?
                    true : false;

                #endregion
            }    
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificarPermiso_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar los permisos?",
                "Modificación de permisos", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            ModificarPermisos(UsuarioId);
        }

        private void ModificarPermisos(Guid UsuarioId)
        {
            var usuario = Context.Usuario.Find(UsuarioId);
            if (usuario != null)
            {
                #region Módulo Romaneo

                usuario.Preingreso = checkPreingreso.Checked.Equals(true) ? true : false;
                usuario.Pesada = checkPesada.Checked.Equals(true) ? true : false;
                usuario.ReimpresionRomaneo = checkListRomaneo.GetItemCheckState(0) == CheckState.Checked ? true : false;
                usuario.ResumenRomaneo = checkListRomaneo.GetItemCheckState(1) == CheckState.Checked ? true : false;
                usuario.ResumenCompra = checkListRomaneo.GetItemCheckState(2) == CheckState.Checked ? true : false;
                usuario.ResumenClaseMes = checkListRomaneo.GetItemCheckState(3) == CheckState.Checked ? true : false;
                usuario.ResumenClaseTrimestre = checkListRomaneo.GetItemCheckState(4) == CheckState.Checked ? true : false;
                usuario.Reclasificacion = checkReclasificacion.Checked.Equals(true) ? true : false;
                usuario.GestionReclasificacion = checkGestionReclasificacion.Checked.Equals(true) ? true : false;
                
                #endregion

                #region Módulo Seguridad

                usuario.Seguridad = checkSeguridad.Checked.Equals(true) ? true : false;
          
                #endregion

                #region Módulo Configuracion
            
                usuario.Configuracion = checkConfiguracion.Checked.Equals(true) ? true : false;
             
                #endregion

                #region Módulo de Administracion

                usuario.Liquidar = checkListLiquidacion.GetItemCheckState(0) == CheckState.Checked ? true : false;
                usuario.LiquidacionSubirAfip = checkListLiquidacion.GetItemCheckState(1) == CheckState.Checked ? true : false;
                usuario.LiquidacionImprimir = checkListLiquidacion.GetItemCheckState(2) == CheckState.Checked ? true : false;
                usuario.GenerarOrdenPago = checkGenerarOrdenPago.Checked.Equals(true) ? true : false;
                usuario.GestionCata = checkGestionCata.Checked.Equals(true) ? true : false;
                usuario.GestionCaja = checkGestionCaja.Checked.Equals(true) ? true : false;
                usuario.GenerarOrdenVenta = checkGenerarOrdenVenta.Checked.Equals(true) ? true : false;
                usuario.GenerarRemitoElectronico = checkGenerarRemitoElectronico.Checked.Equals(true) ? true : false;
                usuario.GestionarListaPrecio = checkListaPrecio.Checked.Equals(true) ? true : false;
                
                #endregion

                Context.Entry(usuario).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }

        private void checkGestionRomaneo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkGestionRomaneo.Checked.Equals(true))
            {
                checkListRomaneo.Enabled = true;
                for (int i = 0; i < checkListRomaneo.Items.Count; i++)
                {
                    checkListRomaneo.SetItemChecked(i, true);
                }
            }
            else
            {
                checkListRomaneo.Enabled = false;
                for (int i = 0; i < checkListRomaneo.Items.Count; i++)
                {
                    checkListRomaneo.SetItemChecked(i, false);
                }
            }
        }

        private void checkLiquidacion_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLiquidacion.Checked.Equals(true))
            {
                checkListLiquidacion.Enabled = true;
                for (int i = 0; i < checkListLiquidacion.Items.Count; i++)
                {
                    checkListLiquidacion.SetItemChecked(i, true);
                }
            }
            else
            {
                checkListLiquidacion.Enabled = false;
                for (int i = 0; i < checkListLiquidacion.Items.Count; i++)
                {
                    checkListLiquidacion.SetItemChecked(i, false);
                }
            }
        }
    
    }
}