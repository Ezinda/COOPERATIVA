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
            int cantidad = int.Parse(txtCantidadCaja.Text);
            Modificar(OrdenVentaId, ProductoId, año, cantidad);
        }

        private void cbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            //var Producto = cbProducto.SelectedItem as dynamic;
            //Guid ProductoId = Producto.ID;
            //var Campaña = cbCampaña.SelectedItem as dynamic;
            //int año = Campaña.Campaña;
            //CargarCajas(ProductoId, año);
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
            OrdenVentaId = Id;
            DetalleId = OrdenVentaDetalleId;
            var ordenVenta =
                 (from o in Context.OrdenVenta
                    .Where(x => x.Id == Id)
                  join ovd in Context.OrdenVentaDetalle
                    .Where(x=>x.Id == OrdenVentaDetalleId)
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
            var detalle = Context.OrdenVentaDetalle.Where(x => x.Id == OrdenVentaDetalleId).FirstOrDefault();
          //  txtCantidadCaja.Text = detalle.DesdeCaja.Value.ToString();
            
            
        }

        private void Modificar(Guid OrdenVentaId, Guid ProductoId, int año, int cantidad)
        {
            if (btnModificar.Text == DevConstantes.Guardar)
            {
                GuardarDatos(OrdenVentaId, ProductoId, año,cantidad,true);
            }
            //else if (btnModificar.Text == DevConstantes.Modificar)
            //{
            //    ModificarDatos(OrdenVentaId, ProductoId, año);
            //}
        }

        private void GuardarDatos(Guid OrdenVentaId, Guid ProductoId, int año, int cantidad,bool nuevoDetalle)
        {
            try
            {
                OrdenVentaDetalle detalle;
                detalle = new OrdenVentaDetalle();
                detalle.Id = Guid.NewGuid();
                detalle.OrdenVentaId = OrdenVentaId;
                detalle.Campaña = año;
                detalle.ProductoId = ProductoId;
                Context.OrdenVentaDetalle.Add(detalle);
                Context.SaveChanges();

                AsociarOVaCaja(OrdenVentaId, ProductoId, año, cantidad);

                ActualizarDesdeHasta(OrdenVentaId, ProductoId, año);
           
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

        private void ActualizarDesdeHasta(Guid OrdenVentaId, Guid ProductoId, int año)
        {
            long desde;
            long hasta;

            var cajaDesde =
                (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId
                         && x.Campaña == año
                         && x.OrdenVentaId == OrdenVentaId)
                 select new
                 {
                     NumeroCaja = c.NumeroCaja
                 })
                 .OrderBy(x => x.NumeroCaja)
                 .FirstOrDefault();
                         
            desde = cajaDesde.NumeroCaja;
            
            var cajaHasta =
                (from c in Context.Caja
                     .Where(x => x.ProductoId == ProductoId
                         && x.Campaña == año
                         && x.OrdenVentaId == OrdenVentaId)
                select new
                 {
                     NumeroCaja = c.NumeroCaja
                 })
                .OrderByDescending(x => x.NumeroCaja)
                .FirstOrDefault();

            hasta = cajaHasta.NumeroCaja;
            
            var OrdenDetalle = Context.OrdenVentaDetalle
            .Where(x => x.ProductoId == ProductoId
                && x.Campaña == año
                && x.OrdenVentaId == OrdenVentaId)
            .Update(x => new OrdenVentaDetalle() { DesdeCaja = desde, HastaCaja = hasta });

            Context.SaveChanges();
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

        private void AsociarOVaCaja(Guid OrdenVentaId, Guid ProductoId, int año, int cantidad)
        {
            var cajas = Context.Caja
                   .Where(x => x.ProductoId == ProductoId
                       && x.Campaña == año)
                       .OrderBy(x => x.NumeroCaja)
                       .Take(cantidad)
                       .Update(x => new Caja() { OrdenVentaId = OrdenVentaId });

            Context.SaveChanges();

            var orden = Context.OrdenVenta
                .Where(x => x.Id == OrdenVentaId)
                .FirstOrDefault();

            CooperativaProduccionEntities _context = new CooperativaProduccionEntities();

            var caja = _context.Caja
                .Where(x => x.ProductoId == ProductoId
                && x.Campaña == año
                && x.OrdenVentaId == OrdenVentaId).ToList();

            foreach (var item in caja)
            {
                var cata = Context.Cata.Find(item.CataId);
                cata.OrdenVentaId = OrdenVentaId;
                cata.NumOrden = orden.NumOrden;
                Context.Entry(cata).State = EntityState.Modified;
                Context.SaveChanges();
            }
            //var catas = Context.Cata
            //       .Where(x => x.Caja.ProductoId == ProductoId
            //           && x.Caja.Campaña == año)
            //           .Take(cantidad)
            //           .Update(x => new Cata() { OrdenVentaId = OrdenVentaId, NumOrden = orden.NumOrden });

            //Context.SaveChanges();
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

        private void CargarCajas(Guid ProductoId, int Campaña)
        {          
           
        }

        private void UbicarBotones(Guid? OrdenVentaDetalleId)
        {
            if (OrdenVentaDetalleId == null)
            {
                btnModificar.Text = DevConstantes.Guardar;
                btnModificar.Location = new Point(333, 84);
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
    }
}