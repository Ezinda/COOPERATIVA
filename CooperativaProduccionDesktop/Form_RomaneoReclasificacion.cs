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

namespace CooperativaProduccion
{
    public partial class Form_RomaneoReclasificacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_RomaneoReclasificacion()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        #region Method Code

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarClasificacion();
        }

        private void txtBuscador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar(txtFardo.Text);
            }
        }

        #endregion

        #region Method Dev

        private void Buscar(string numFardo)
        {
            long fardo = long.Parse(numFardo);
            var result = Context.PesadaDetalle
                    .Where(x => x.NumFardo == fardo )
                    .FirstOrDefault();

            if (!string.IsNullOrEmpty(result.Id.ToString()))
            {
                //if (result.ReclasificacionId != null)
                //{
                //    var reclasificacion = Context.Clase
                //   .Where(x => x.Id == result.ReclasificacionId)
                //   .FirstOrDefault();
                //    txtClase.Text = reclasificacion.Nombre;
                //}
                //else
                //{
                //    var clase = Context.Clase
                //        .Where(x => x.Id == result.ClaseId)
                //        .FirstOrDefault();
                //    txtClase.Text = clase.Nombre;
                //}
            }
        }

        private void ActualizarClasificacion()
        {
            if (ValidarReclasificacion())
            {
                Reclasificar(txtFardo.Text);
            }
        }

        private bool ValidarReclasificacion()
        {
            if (txtFardo.Text == null && txtClase.Text == null && cbClase.Text == null)
            {
                MessageBox.Show("No se ha seleccionado un fardo.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void Reclasificar(string numFardo)
        {
            long fardo = long.Parse(numFardo);
            var result = Context.PesadaDetalle
                    .Where(x => x.NumFardo == fardo )
                    .FirstOrDefault();

            if (!string.IsNullOrEmpty(result.Id.ToString()))
            {
                var pesadaDetalle = Context.PesadaDetalle.Find(result.Id);
                var clase = Context.Vw_Clase
                    .Where(x => x.NOMBRE.Equals(txtReclasificacion.Text))
                    .FirstOrDefault();
                if (clase != null)
                {
                    pesadaDetalle.ReclasificacionId = new Guid(txtReclasificacion.Text);

                    pesadaDetalle.ReclasificacionPrecio =
                                   Context.Entry(pesadaDetalle).State = EntityState.Modified;
                    Context.SaveChanges();

                    Limpiar();
                }
            }
        }

        private void Limpiar()
        {
            txtFardo.Text = string.Empty;
            txtClase.Text = string.Empty;
        }

        #endregion

        private void txtFardo_TextChanged(object sender, EventArgs e)
        {
            if (txtFardo.Text != string.Empty)
            {
                txtClase.Focus();
            }
        }
    }
}