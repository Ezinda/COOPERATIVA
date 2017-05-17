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
    public partial class Form_ProduccionTransferenciaMateriaPrima : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid? ProductoId;

        public Form_ProduccionTransferenciaMateriaPrima()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar();
        }

        #region Method Code

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
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

            if (e.KeyChar == 8)
            {
                txtClase.Text = string.Empty;
                txtKilos.Text = string.Empty;
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

        #endregion

        #region Method Dev

        private void Iniciar()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            txtFardo.BackColor = Color.LightSkyBlue;

            var blend = Context.ConfiguracionBlend
                .Where(x => x.Activo == true)
                .FirstOrDefault();

            if (blend != null)
            {
                var producto = Context.Vw_Producto
                    .Where(x => x.ID == blend.ProductoId)
                    .FirstOrDefault();

                if (producto != null)
                {
                    ProductoId = producto.ID;
                    txtBlend.Text = producto.DESCRIPCION;
                }
            }
            else
            {
                txtBlend.Text = string.Empty;
            }
        }

        private bool ValidarTransferencia()
        {
            if (string.IsNullOrEmpty(txtFardo.Text) && 
                string.IsNullOrEmpty(txtClase.Text))
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
                            .Where(x=>x.Actual == true)
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
            txtFardo.Focus();
            txtFardo.BackColor = Color.LightSkyBlue;
            txtFardo.Text = string.Empty;
            txtClase.Text = string.Empty;
            txtKilos.Text = string.Empty;
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

        private void Transferir(long Fardo)
        {
            var detalle = Context.PesadaDetalle
                .Where(x => x.NumFardo == Fardo)
                .FirstOrDefault();

            if ( detalle != null)
            {
                RegistrarIngresoProceso(detalle.Id, detalle.Kilos.Value);
                UpdateMovimientoActual(detalle.Id);
                RegistrarMovimientoEgreso(detalle.Id, detalle.Kilos.Value);
                RegistrarMovimientoIngreso(detalle.Id, detalle.Kilos.Value);
            }
            Limpiar();
        }

        private Guid RegistrarMovimientoEgreso(Guid Id,double kilos)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = DateTime.Now.Date;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Kg;
            movimiento.Ingreso = 0;
            movimiento.Egreso = kilos;
            movimiento.Actual = false;
            movimiento.Anulado = false;

            var deposito = Context.Vw_Deposito
                .Where(x => x.id == DevConstantes.DepositoMateriaPrima)
                .FirstOrDefault();
            
            movimiento.DepositoId = deposito.id;

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private Guid RegistrarMovimientoIngreso(Guid Id,double kilos)
        {
            Movimiento movimiento;

            movimiento = new Movimiento();
            movimiento.Id = Guid.NewGuid();
            movimiento.Fecha = DateTime.Now.Date;
            movimiento.TransaccionId = Id;
            movimiento.Documento = DevConstantes.Transferencia;
            movimiento.Unidad = DevConstantes.Kg;
            movimiento.Ingreso = kilos;
            movimiento.Egreso = 0;
            movimiento.Actual = true;
            movimiento.Anulado = false;

            var deposito = Context.Vw_Deposito
                .Where(x => x.id == DevConstantes.ProduccionEnProceso)
                .FirstOrDefault();

            movimiento.DepositoId = deposito.id;

            Context.Movimiento.Add(movimiento);
            Context.SaveChanges();

            return movimiento.Id;
        }

        private void RegistrarIngresoProceso(Guid Id, double kilos)
        {
            FardoEnProduccion produccion;

            produccion = new FardoEnProduccion();
            produccion.Id = Guid.NewGuid();
            produccion.Fecha = DateTime.Now.Date;
            produccion.PesadaDetalleId = Id;
            produccion.Kilos = kilos;
            produccion.Unidad = DevConstantes.Kg;
            produccion.Hora = DateTime.Now.TimeOfDay;
            if (ProductoId.HasValue)
            {
                produccion.ProductoId = ProductoId.Value;
            }
            else
            {
                produccion.ProductoId = null;
            }


            Context.FardoEnProduccion.Add(produccion);
            Context.SaveChanges();
        }

        private void UpdateMovimientoActual(Guid Id)
        {
            var movimiento = Context.Movimiento
                 .Where(x => x.TransaccionId == Id)
                     .Update(x => new Movimiento() { Actual = false });

            Context.SaveChanges();
        }

        #endregion

    }
}