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
using System.Drawing.Printing;
using CooperativaProduccion.Helpers;
using EntityFramework.Extensions;

namespace CooperativaProduccion
{
    public partial class Form_ProduccionConfiguracionBlend : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_ProduccionConfiguracionBlend()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar();
        }

        #region Method Code

        private void btnCerrarBlend_Click(object sender, EventArgs e)
        {
            DeshabilitarBlend();
            Iniciar();
        }

        private void btnNuevoBlend_Click(object sender, EventArgs e)
        {
            if (btnNuevoBlend.Text == DevConstantes.NuevoBlend)
            {
                if (BlendActivo())
                {
                    btnNuevoBlend.Text = DevConstantes.GuardarBlend;
                }
            }
            else
            {
                NuevoBlend();
                UpdateFardoEnProceso();
                Iniciar();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Method Dev

        private void Iniciar()
        {
            var producto = Context.Vw_Producto.ToList();
            cbProducto.DataSource = producto;
            cbProducto.DisplayMember = "DESCRIPCION";
            cbProducto.ValueMember = "ID";

            var blend = Context.ConfiguracionBlend
                .Where(x => x.Activo == true)
                .ToList();

            if (blend.Any())
            {
                btnNuevoBlend.Text = DevConstantes.NuevoBlend;
                Deshabilitar();
                cbProducto.SelectedValue = blend.FirstOrDefault().ProductoId;
            }
            else
            {
                btnNuevoBlend.Text = DevConstantes.GuardarBlend;
                Habilitar();
            }
        }

        private void Deshabilitar()
        {
            cbProducto.Enabled = false;
        }

        private void Habilitar()
        {
            cbProducto.Enabled = true;
        }

        private void NuevoBlend()
        {
            try
            {
                ConfiguracionBlend blend;

                blend = new ConfiguracionBlend();
                blend.Id = Guid.NewGuid();
                blend.ProductoId = Guid.Parse(cbProducto.SelectedValue.ToString());
                blend.Fecha = DateTime.Now.Date;
                blend.Hora = DateTime.Now.TimeOfDay;
                blend.Activo = true;
                Context.ConfiguracionBlend.Add(blend);
                Context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void DeshabilitarBlend()
        {
            var blend = Context.ConfiguracionBlend
                .Where(x => x.Activo == true)
                    .Update(x => new ConfiguracionBlend() { Activo = false });

            Context.SaveChanges();
        }

        private bool BlendActivo()
        {
            var blend = Context.ConfiguracionBlend
              .Where(x => x.Activo == true)
              .ToList();

            if (blend.Any())
            {
                DialogResult dr = MessageBox.Show("Existe una configuración de blend activa."
                    + "¿ Desea cerrar la configuración?", "Atención",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    DeshabilitarBlend();
                    Iniciar();
                    return true;
                }
                else if (dr == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateFardoEnProceso()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            DateTime Hoy = DateTime.Now.Date;
            var blend = Context.ConfiguracionBlend
                .Where(x => x.Activo == true 
                    && x.Fecha == Hoy)
                .FirstOrDefault();

            if (blend != null)
            {
                var fardos = Context.FardoEnProduccion
                    .Where(x => x.Fecha == Hoy
                        && x.ProductoId == null)
                  .Update(x => new FardoEnProduccion() { ProductoId = blend.ProductoId });

                Context.SaveChanges();
            }
        }

        #endregion

    }
}