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
    public partial class Form_AdministracionActualizarOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid OrdenVentaId;
        private bool NuevaOrden;
     
        public Form_AdministracionActualizarOrdenVenta(Guid Id,bool nuevo)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Iniciar(Id,nuevo);
        }

        private void Iniciar(Guid Id, bool nuevo)
        {
            NuevaOrden = nuevo;
            UbicarBotones(nuevo);
            if (nuevo)
            {
                CargarNuevo(Id);
            }
            else
            {
                Deshabilitar();
                CargarDatos(Id);
            }
        }

        private void Form_AdministracionActualizarOrdenVenta_Load(object sender, EventArgs e)
        {

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
                    .Where(x=>x.ID == ov.ClienteId)
                    .FirstOrDefault();
                txtCliente.Text = cliente.RAZONSOCIAL;
            }

            var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.Warrants)
                .Single();

            var producto =
                (from c in Context.Caja
                 join m in Context.Movimiento
                 .Where(x => x.DepositoId != deposito.id)
                 on c.Id equals m.TransaccionId
                 join p in Context.Vw_Producto
                 on c.ProductoId equals p.ID
                 group new { c, p } by new
                 {
                     ProductoId = p.ID,
                     Producto = p.DESCRIPCION,
                     Campaña = c.Campaña
                 } into g
                 select new
                 {
                     ProductoId = g.Key.ProductoId,
                     Producto = g.Key.Producto + " - " + g.Key.Campaña,
                     Campaña = g.Key.Campaña
                 })
                .ToList();

            cbOrden.DataSource = producto;
            cbOrden.DisplayMember = "Producto";
            cbOrden.ValueMember = "ProductoId";

        }

        private void CargarDatos(Guid Id)
        {
            OrdenVentaId = Id;

            var ordenVenta =
                 (from o in Context.OrdenVenta
                      .Where(x => x.Id == Id)
                  join ovd in Context.OrdenVentaDetalle
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
                      ProductoId = p.ID,
                      OrdenProducto = o.NumOrden + " - " + p.DESCRIPCION + " - " + o.Fecha.Year,
                      Campaña = o.Fecha
                  })
                  .ToList();

            cbOrden.DataSource = ordenVenta;
            cbOrden.DisplayMember = "OrdenProducto";
            cbOrden.ValueMember = "OrdenVentaId";

            var Producto = cbOrden.SelectedItem as dynamic;
            
            Guid ProductoId = Producto.ProductoId;

            int Campaña = Producto.Campaña.Year;

            if (ordenVenta.Any())
            {
                txtOperacion.Text = ordenVenta.FirstOrDefault().NumOperacion.ToString();
                txtCliente.Text = ordenVenta.FirstOrDefault().Cliente;
            }

            var caja = Context.Caja
                .Where(x => x.OrdenVentaId == Id)
                .Any();

            if (caja)
            {
                var deposito = Context.Vw_Deposito
                .Where(x => x.nombre == DevConstantes.Warrants)
                .Single();

                var cajaDesde =
                    (from c in Context.Caja
                     .Where(x => x.OrdenVentaId == Id
                         && x.ProductoId == ProductoId
                         && x.Campaña == Campaña)
                     join m in Context.Movimiento
                     .Where(x => x.DepositoId != deposito.id)
                     on c.Id equals m.TransaccionId
                     select new
                     {
                         NumeroCaja = c.NumeroCaja
                     })
                     .OrderBy(x => x.NumeroCaja)
                     .FirstOrDefault();

                if (cajaDesde != null)
                {
                    txtCajaDesde.Text = cajaDesde.NumeroCaja.ToString();
                }

                var cajaHasta =
                    (from c in Context.Caja
                         .Where(x => x.OrdenVentaId == Id
                             && x.ProductoId == ProductoId
                             && x.Campaña == Campaña)
                     join m in Context.Movimiento
                     .Where(x => x.DepositoId != deposito.id)
                     on c.Id equals m.TransaccionId
                     select new
                     {
                         NumeroCaja = c.NumeroCaja
                     })
                    .OrderByDescending(x => x.NumeroCaja)
                    .FirstOrDefault();

                if (cajaHasta != null)
                {
                    txtCajaHasta.Text = cajaHasta.NumeroCaja.ToString();
                }
            }
        }
        
        private void Modificar()
        {
            if (btnModificar.Text == DevConstantes.Guardar)
            {
                GuardarDatos(OrdenVentaId);   
            }
            else if (btnModificar.Text == DevConstantes.Modificar)
            {
                ModificarDatos();
            }      
        }

        private void GuardarDatos(Guid OrdenVentaId)
        {
            var Orden = cbOrden.SelectedItem as dynamic;

            Guid ProductoId = Orden.ProductoId;

            int Campaña = Orden.Campaña;

            try
            {
                OrdenVentaDetalle detalle;
                detalle = new OrdenVentaDetalle();
                detalle.Id = Guid.NewGuid();
                detalle.OrdenVentaId = OrdenVentaId;
                detalle.Campaña = Campaña;
                detalle.ProductoId = ProductoId;
                detalle.DesdeCaja = long.Parse(txtCajaDesde.Text);
                detalle.HastaCaja = long.Parse(txtCajaHasta.Text);
                Context.OrdenVentaDetalle.Add(detalle);
                Context.SaveChanges();

                for (long i = detalle.DesdeCaja.Value; i <= detalle.HastaCaja; i++)
                {
                    var caja = Context.Caja
                        .Where(x => x.NumeroCaja == i)
                        .FirstOrDefault();

                    AsociarOVaCaja(detalle.OrdenVentaId, caja.Id);
                }

            }
            catch
            {
                throw;
            }

            IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;

            if (mienlace != null)
            {
                mienlace.Enviar(true);
            }

            this.Close();
        }

        private void ModificarDatos()
        {
            var Orden = cbOrden.SelectedItem as dynamic;

            Guid OrdenVentaId = Orden.OrdenVentaId;

            Guid ProductoId = Orden.ProductoId;

            int Campaña = Orden.Campaña.Year;

            var orden = Context.OrdenVentaDetalle
                .Where(x => x.OrdenVentaId == OrdenVentaId 
                    && x.ProductoId == ProductoId
                    && x.Campaña == Campaña)
                .FirstOrDefault();

            if (orden != null)
            {
                var ordenVenta = Context.OrdenVentaDetalle.Find(orden.Id);

                if (ordenVenta != null)
                {
                    ordenVenta.DesdeCaja = long.Parse(txtCajaDesde.Text);
                    ordenVenta.HastaCaja = long.Parse(txtCajaHasta.Text);
                    Context.Entry(ordenVenta).State = EntityState.Modified;
                    Context.SaveChanges();

                    for (long i = ordenVenta.DesdeCaja.Value; i <= ordenVenta.HastaCaja; i++)
                    {
                        var caja = Context.Caja
                            .Where(x => x.NumeroCaja == i)
                            .FirstOrDefault();

                        AsociarOVaCaja(ordenVenta.Id, caja.Id);
                    }
                }

                IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;

                if (mienlace != null)
                {
                    mienlace.Enviar(true);
                }

                this.Close();
            }
        }

        private void AsociarOVaCaja(Guid OrdenVentaId,Guid CajaId)
        {
            var caja = Context.Caja.Find(CajaId);
            caja.OrdenVentaId = OrdenVentaId;
            Context.Entry(caja).State = EntityState.Modified;
            Context.SaveChanges();

            if(caja.CataId != null)
            {
                var cata = Context.Cata.Find(caja.CataId);
                cata.OrdenVentaId = OrdenVentaId;
                var orden = Context.OrdenVenta
                    .Where(x => x.Id == OrdenVentaId)
                    .First();
                cata.NumOrden = orden.NumOrden;
                Context.Entry(cata).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
    
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
            Eliminar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar esta orden de venta?",
                   "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            Modificar();
        }

        private void Eliminar()
        {
            var cborden = cbOrden.SelectedItem as dynamic;

            Guid OrdenVentaId = cborden.OrdenVentaId;

            var ordenVenta = Context.OrdenVenta
                .Where(x => x.Id == OrdenVentaId)
                .FirstOrDefault();

            if (ordenVenta != null)
            {
                var catas = Context.Cata
                    .Where(x => x.OrdenVentaId == OrdenVentaId)
                    .ToList();

                foreach (var item in catas)
                {
                    var cata = Context.Cata.Find(item.Id);
                    cata.NumOrden = null;
                    cata.OrdenVentaId = null;
                    Context.Entry(cata).State = EntityState.Modified;
                    Context.SaveChanges();
                }

                var cajas = Context.Caja
                    .Where(x => x.OrdenVentaId == OrdenVentaId)
                    .ToList();

                foreach (var item in cajas)
                {
                    var caja = Context.Caja.Find(item.Id);
                    caja.OrdenVentaId = null;
                    Context.Entry(caja).State = EntityState.Modified;
                    Context.SaveChanges();
                }
                
                var detalles = Context.OrdenVentaDetalle
                    .Where(x => x.OrdenVentaId == OrdenVentaId)
                    .ToList();

                if (detalles.Any().Equals(true))
                {
                    foreach (var item in detalles)
                    {
                        var ov = Context.OrdenVentaDetalle.Find(item.Id);
                        Context.Entry(ov).State = EntityState.Deleted;
                        Context.SaveChanges();
                    }
                }

                IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;
                if (mienlace != null)
                {
                    mienlace.Enviar(true);
                }
                
                this.Close();
            }
        }

        private void UbicarBotones(bool Nuevo)
        {
            if (Nuevo.Equals(true))
            {
                btnModificar.Text = DevConstantes.Guardar;
                btnModificar.Location = new Point(419, 82);
                btnEliminar.Visible = false;
            }
            else
            {
                btnModificar.Text = DevConstantes.Modificar;
            }
        }

        private void Deshabilitar()
        {
            cbOrden.Enabled = false;
        }

        private void Habilitar()
        {
            cbOrden.Enabled = true;
        }

        private void cbOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Producto = cbOrden.SelectedItem as dynamic;
            Guid ProductoId = Producto.ProductoId;
            int Campaña = 0;
            if (NuevaOrden)
            {
                Campaña = Producto.Campaña;
            }
            else
            {
                Campaña = Producto.Campaña.Year;
            }
            CargarCajas(ProductoId, Campaña);
        }

        private void CargarCajas(Guid ProductoId, int Campaña)
        {
            var deposito = Context.Vw_Deposito
               .Where(x => x.nombre == DevConstantes.Warrants)
               .Single();

            var caja =
                    (from c in Context.Caja
                     .Where(x=> x.ProductoId == ProductoId
                         && x.Campaña == Campaña)
                     join m in Context.Movimiento
                     .Where(x => x.DepositoId != deposito.id)
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
                        .Where(x => x.DepositoId != deposito.id)
                        on c.Id equals m.TransaccionId
                     select new
                     {
                         NumeroCaja = c.NumeroCaja
                     })
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
                     .Where(x => x.DepositoId != deposito.id)
                     on c.Id equals m.TransaccionId
                     select new
                     {
                         NumeroCaja = c.NumeroCaja
                     })
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
    }
}