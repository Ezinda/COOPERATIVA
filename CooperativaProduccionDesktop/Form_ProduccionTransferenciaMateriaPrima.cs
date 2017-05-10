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

namespace CooperativaProduccion
{
    public partial class Form_ProduccionTransferenciaMateriaPrima : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_ProduccionTransferenciaMateriaPrima()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        #region Method Code

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
                
        #endregion

        #region Method Dev

        private void ActualizarClasificacion()
        {

        }

        private bool ValidarTransferencia()
        {
            if (string.IsNullOrEmpty(txtFardo.Text) && string.IsNullOrEmpty(txtClase.Text))
            {
                MessageBox.Show("No se ha seleccionado un fardo.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            long fail = 0;
            bool successfullyParsed;
            successfullyParsed = long.TryParse(txtFardo.Text, out fail);
            if (!successfullyParsed)
            {
                MessageBox.Show("Debe ingresar un fardo válido.",
                      "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                long fardo = long.Parse(txtFardo.Text);
                var existe = Context.PesadaDetalle
                    .Where(x => x.NumFardo == fardo)
                    .Any();

                if (!existe)
                {
                    MessageBox.Show("No existe el fardo.",
                      "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    var enproceso =
                        (from d in Context.PesadaDetalle
                            .Where(x => x.NumFardo == fardo)
                         join m in Context.Movimiento
                              on d.Id equals m.TransaccionId
                         join dep in Context.Vw_Deposito
                            .Where(x=>x.id == DevConstantes.ProduccionEnProceso)
                            on m.DepositoId equals dep.id
                         select new
                         {
                             Deposito = dep.nombre
                         })
                        .Any();
                    if (enproceso)
                    {
                        MessageBox.Show("El fardo ya esta en proceso.",
                            "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void Limpiar()
        {
            txtFardo.Text = string.Empty;
            txtClase.Text = string.Empty;
        }

        #endregion

        private void txtReclasificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ActualizarClasificacion();
            }
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            if (ValidarTransferencia())
            {
                long fardo = long.Parse(txtFardo.Text);
                Transferir(fardo);
            }
        }

        private void txtFardo_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFardo.Text))
                {
                    long fardo = long.Parse(txtFardo.Text);
                    MostrarFardo(fardo);
                    btnTransferir.Focus();
                }
            }

            if(e.KeyChar == 8)
            {
                txtClase.Text = string.Empty;
                txtKilos.Text = string.Empty;
            }
        }

        private void MostrarFardo(long fardo)
        {
            var Pesada = 
                (from d in Context.PesadaDetalle
                    .Where(x => x.NumFardo == fardo)
                join c in Context.Vw_Clase
                    on d.ClaseId equals c.ID
                 select new
                 {
                     Fardo = d.NumFardo,
                     Clase = c.NOMBRE,
                     Kilos = d.Kilos
                 })
                 .FirstOrDefault();

            if (Pesada != null)
            {
                txtFardo.Text = Pesada.Fardo.Value.ToString();
                txtClase.Text = Pesada.Clase;
                txtKilos.Text = Pesada.Kilos.Value.ToString();
            }
        }

        private void btnBuscarFardo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFardo.Text))
            {
                long fardo = long.Parse(txtFardo.Text);
                MostrarFardo(fardo);
            }
        }

        private void Transferir(long Fardo)
        {

        }
    }
}