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
using EntityFramework.Extensions;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionActualizarOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid OrdenVentaId;
        private Guid DetalleId;
        public Form_AdministracionActualizarOrdenVenta(Guid Id,Guid? OrdenVentaDetalleId,bool nuevo)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar(Id,OrdenVentaDetalleId);
        }

        #region Method Code

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Deshabilitar();
            var resultado = MessageBox.Show("¿Desea eliminar este detalle de orden?",
                 "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            var Producto = cbProducto.SelectedItem as dynamic;
            Guid ProductoId = Producto.ID;
            var Campaña = cbCampaña.SelectedItem as dynamic;
            int año = Campaña.Campaña;

            Eliminar(OrdenVentaId,DetalleId, ProductoId, año, true);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar esta orden de venta?",
               "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            var Producto = cbProducto.SelectedItem as dynamic;
            Guid ProductoId = Producto.ID;
            var Campaña = cbCampaña.SelectedItem as dynamic;
            int año = Campaña.Campaña;
            Modificar(OrdenVentaId, ProductoId, año);
        }

        private void cbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
    
        }

        #endregion

        #region Method Dev

        private void Iniciar(Guid Id, Guid? OrdenVentaDetalleId)
        {
            UbicarBotones(OrdenVentaDetalleId);
            if (OrdenVentaDetalleId == null)
            {
                CargarNuevo(Id);
            }
            else
            {
                Deshabilitar();
                CargarDatos(Id,OrdenVentaDetalleId.Value);
            }
        }

        private void CargarNuevo(Guid Id)
        {
            var ov = Context.OrdenVenta
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            if (ov != null)
            {
                OrdenVentaId = Id;
                txtOperacion.Text = ov.NumOperacion.ToString();
                var cliente = Context.Vw_Cliente
                    .Where(x => x.ID == ov.ClienteId)
                    .FirstOrDefault();
                txtCliente.Text = cliente.RAZONSOCIAL;
            }

            var campaña =
                (from c in Context.Caja
                    .Where(x => x.OrdenVentaId == null)
                 group new { c } by new
                 {
                     Campaña = c.Campaña
                 } into g
                 select new
                 {
                     Campaña = g.Key.Campaña
                 })
                 .OrderBy(x => x.Campaña)
                 .ToList();

            var deposito = Context.Vw_Deposito
              .Where(x => x.nombre == DevConstantes.Warrants
                  || x.nombre == DevConstantes.Deposito)
              .ToList();

            cbDeposito.DataSource = deposito;
            cbDeposito.DisplayMember = "nombre";
            cbDeposito.ValueMember = "id";

            cbCampaña.DataSource = campaña;
            cbCampaña.DisplayMember = "Campaña";
            cbCampaña.ValueMember = "Campaña";

            var producto = Context.Vw_Producto.ToList();
            cbProducto.DataSource = producto;
            cbProducto.DisplayMember = "Descripcion";
            cbProducto.ValueMember = "Id";

          
        }

        private void CargarDatos(Guid Id,Guid OrdenVentaDetalleId)
        {
            var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.Warrants
                    || x.nombre == DevConstantes.Deposito)
                .ToList();
            cbDeposito.DataSource = deposito;
            cbDeposito.DisplayMember = "nombre";
            cbDeposito.ValueMember = "id";

            OrdenVentaId = Id;

            DetalleId = OrdenVentaDetalleId;

            var ordenVenta =
                 (from o in Context.OrdenVenta
                    .Where(x => x.Id == Id)
                  join ovd in Context.OrdenVentaDetalle
                    .Where(x => x.Id == OrdenVentaDetalleId)
                    on o.Id equals ovd.OrdenVentaId
                  join p in Context.Vw_Producto
                    on ovd.ProductoId equals p.ID
                  join c in Context.Vw_Cliente
                    on o.ClienteId equals c.ID
                  select new
                  {
                      OrdenVentaId = o.Id,
                      NumOperacion = o.NumOperacion,
                      NumOrden = o.NumOrden,
                      OperacionCliente = o.NumOperacion + " - " + c.RAZONSOCIAL,
                      Cliente = c.RAZONSOCIAL,
                      Producto = p.DESCRIPCION,
                      ID = p.ID,
                      Campaña = ovd.Campaña
                  })
                  .ToList();

            cbCampaña.DataSource = ordenVenta;
            cbCampaña.DisplayMember = "Campaña";
            cbCampaña.ValueMember = "Campaña";
            
            cbProducto.DataSource = ordenVenta;
            cbProducto.DisplayMember = "Producto";
            cbProducto.ValueMember = "ID";

            var Producto = cbProducto.SelectedItem as dynamic;
            Guid ProductoId = Producto.ID;
            var Campaña = cbCampaña.SelectedItem as dynamic;
            int año = Campaña.Campaña;

            if (ordenVenta.Any())
            {
                txtOperacion.Text = ordenVenta.FirstOrDefault().NumOperacion.ToString();

                txtCliente.Text = ordenVenta.FirstOrDefault().Cliente;
            }
            var detalle = Context.OrdenVentaDetalle
                .Where(x => x.Id == OrdenVentaDetalleId)
                .FirstOrDefault();

            txtCajaDesde.Text = detalle.DesdeCaja.Value.ToString();

            txtCajaHasta.Text = detalle.HastaCaja.Value.ToString();

            var cajaDesde =
                 (from c in Context.Caja
                      .Where(x => x.ProductoId == ProductoId
                          && x.Campaña == año
                          && x.NumeroCaja == detalle.DesdeCaja)
                  join m in Context.Movimiento
                     on c.Id equals m.TransaccionId
                  select new
                  {
                      DepositoId = m.DepositoId
                  })
                 .FirstOrDefault();

            cbDeposito.SelectedValue = cajaDesde.DepositoId;
        }

        private void Modificar(Guid OrdenVentaId, Guid ProductoId, int año)
        {
            if (btnModificar.Text == DevConstantes.Guardar)
            {
                GuardarDatos(OrdenVentaId, ProductoId, año,true);
            }
            //else if (btnModificar.Text == DevConstantes.Modificar)
            //{
            //    ModificarDatos(OrdenVentaId, ProductoId, año);
            //}
        }

        private void GuardarDatos(Guid OrdenVentaId, Guid ProductoId, int año, bool nuevoDetalle)
        {
            try
            {

                OrdenVentaDetalle detalle;
                detalle = new OrdenVentaDetalle();
                detalle.Id = Guid.NewGuid();
                detalle.OrdenVentaId = OrdenVentaId;
                detalle.Campaña = año;
                detalle.ProductoId = ProductoId;
                detalle.DesdeCaja = long.Parse(txtCajaDesde.Text);
                detalle.HastaCaja = long.Parse(txtCajaHasta.Text);
                Context.OrdenVentaDetalle.Add(detalle);
                Context.SaveChanges();

                AsociarOVaCaja(OrdenVentaId, ProductoId, año, detalle.DesdeCaja.Value, detalle.HastaCaja.Value);

            }
            catch
            {
                throw;
            }

            if (nuevoDetalle)
            {
                IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;

                if (mienlace != null)
                {
                    mienlace.Enviar(true);
                }

                this.Close();
            }
        }

        private void ModificarDatos(Guid OrdenVentaId, Guid ProductoId, int año)
        {
            //Eliminar(OrdenVentaId, ProductoId, año, false);

            //GuardarDatos(OrdenVentaId, ProductoId, año, false);

            //IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;

            //if (mienlace != null)
            //{
            //    mienlace.Enviar(true);
            //}

            //this.Close();
        }

        private void AsociarOVaCaja(Guid OrdenVentaId, Guid ProductoId, int año, long desde, long hasta)
        {
            var cajas = Context.Caja
                   .Where(x => x.ProductoId == ProductoId
                       && x.Campaña == año
                       && x.NumeroCaja >= desde
                       && x.NumeroCaja <= hasta)
                       .Update(x => new Caja() { OrdenVentaId = OrdenVentaId });

            var orden = Context.OrdenVenta
                .Where(x => x.Id == OrdenVentaId)
                .FirstOrDefault();

            var catas = Context.Cata
                   .Where(x => x.Caja.ProductoId == ProductoId
                       && x.Caja.Campaña == año
                       && x.Caja.NumeroCaja >= desde
                       && x.Caja.NumeroCaja <= hasta)
                       .Update(x => new Cata() { OrdenVentaId = OrdenVentaId, NumOrden = orden.NumOrden });

            Context.SaveChanges();
        }

        private void Eliminar(Guid OrdenVentaId,Guid OrdenVentaDetalleId, Guid ProductoId, int año, bool eliminarDetalle)
        {
            var ordenVenta = Context.OrdenVenta
                .Where(x => x.Id == OrdenVentaId)
                .FirstOrDefault();

            if (ordenVenta != null)
            {
                var catas = Context.Cata
                    .Where(x => x.OrdenVentaId == OrdenVentaId
                        && x.Caja.ProductoId == ProductoId
                        && x.Caja.Campaña == año)
                        .Update(x => new Cata() { OrdenVentaId = null , NumOrden = null});

                var cajas = Context.Caja
                    .Where(x => x.OrdenVentaId == OrdenVentaId
                        && x.ProductoId == ProductoId
                        && x.Campaña == año)
                        .Update(x => new Caja() { OrdenVentaId = null });

                Context.OrdenVentaDetalle.RemoveRange(Context.OrdenVentaDetalle.Where(x => x.Id == OrdenVentaDetalleId));

                Context.SaveChanges();

                if (eliminarDetalle)
                {
                    IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;
                    if (mienlace != null)
                    {
                        mienlace.Enviar(true);
                    }

                    this.Close();
                }
            }
        }

        private void CargarCajas(Guid ProductoId, int Campaña, Guid DepositoId)
        {
            var deposito = Context.Vw_Deposito
               .Where(x => x.id == DepositoId)
               .Select(x => x.id)
               .FirstOrDefault();

            var caja =
                    (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId
                         && x.Campaña == Campaña)
                     join m in Context.Movimiento
                     .Where(x => x.DepositoId == deposito)
                     on c.Id equals m.TransaccionId
                     select new
                     {
                         NumeroCaja = c.NumeroCaja
                     })
                     .Any();

            if (caja)
            {
                var cajaDesde =
                    (from c in Context.Caja
                         .Where(x => x.ProductoId == ProductoId
                             && x.Campaña == Campaña)
                     join m in Context.Movimiento
                        .Where(x => x.DepositoId == deposito)
                        on c.Id equals m.TransaccionId
                     group new { c, m } by new
                     {
                         NumeroCaja = c.NumeroCaja
                     } into g
                     select new
                     {
                         g.Key.NumeroCaja,
                         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                     })
                    .Where(x => x.Saldo > 0)
                     .OrderBy(x => x.NumeroCaja)
                     .FirstOrDefault();

                if (cajaDesde != null)
                {
                    txtCajaDesde.Text = cajaDesde.NumeroCaja.ToString();
                }

                var cajaHasta =
                    (from c in Context.Caja
                         .Where(x => x.ProductoId == ProductoId
                             && x.Campaña == Campaña)
                     join m in Context.Movimiento
                     .Where(x => x.DepositoId == deposito)
                     on c.Id equals m.TransaccionId
                     group new { c, m } by new
                     {
                         NumeroCaja = c.NumeroCaja
                     } into g
                     select new
                     {
                         g.Key.NumeroCaja,
                         Saldo = g.Sum(c => c.m.Ingreso) - g.Sum(c => c.m.Egreso)
                     })
                    .Where(x => x.Saldo > 0)
                    .OrderByDescending(x => x.NumeroCaja)
                    .FirstOrDefault();

                if (cajaHasta != null)
                {
                    txtCajaHasta.Text = cajaHasta.NumeroCaja.ToString();
                }
            }
            else
            {
                txtCajaDesde.Text = string.Empty;
                txtCajaHasta.Text = string.Empty;
            }
        }

        private void UbicarBotones(Guid? OrdenVentaDetalleId)
        {
            if (OrdenVentaDetalleId == null)
            {
                btnModificar.Text = DevConstantes.Guardar;
                btnModificar.Location = new Point(333, 113);
                btnEliminar.Visible = false;
            }
            else
            {
                btnModificar.Visible = false;
            }
        }

        private void Deshabilitar()
        {
            cbCampaña.Enabled = false;
            cbProducto.Enabled = false;
        }

        private void Habilitar()
        {
            cbCampaña.Enabled = true;
            cbProducto.Enabled = true;
        }

        #endregion

        private void cbDeposito_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cbDeposito_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var Producto = cbProducto.SelectedItem as dynamic;
            Guid ProductoId = Producto.ID;
            var Campaña = cbCampaña.SelectedItem as dynamic;
            int año = Campaña.Campaña;
            var deposito = cbDeposito.SelectedItem as dynamic;
            Guid DepositoId = deposito.id;

            CargarCajas(ProductoId, año, DepositoId);
        }

        private void cbProducto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var Producto = cbProducto.SelectedItem as dynamic;
            Guid ProductoId = Producto.ID;
            var Campaña = cbCampaña.SelectedItem as dynamic;
            int año = Campaña.Campaña;
            var deposito = cbDeposito.SelectedItem as dynamic;
            Guid DepositoId = deposito.id;

            CargarCajas(ProductoId, año, DepositoId);
        }
    }
}