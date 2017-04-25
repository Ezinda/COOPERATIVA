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

        public Form_AdministracionActualizarOrdenVenta(Guid Id)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Deshabilitar();
            //CargarDatos(Id);
        }

        private void Form_AdministracionActualizarOrdenVenta_Load(object sender, EventArgs e)
        {

        }

        //private void CargarDatos(Guid Id)
        //{
        //    OrdenVentaId = Id;

        //    var ordenVenta =
        //         (from o in Context.OrdenVenta
        //              .Where(x => x.Id == Id)
        //          join p in Context.Vw_Producto
        //          on o.ProductoId equals p.ID
        //          join c in Context.Vw_Cliente
        //          on o.ClienteId equals c.ID
        //          select new
        //          {
        //              OrdenVentaId = o.Id,
        //              NumOperacion = o.NumOperacion,
        //              NumOrden = o.NumOrden,
        //              OperacionCliente = o.NumOperacion + " - " + c.RAZONSOCIAL,
        //              Cliente = c.RAZONSOCIAL,
        //              Producto = p.DESCRIPCION,
        //              OrdenProducto = o.NumOrden + " - " + p.DESCRIPCION,
        //              Fecha = o.Fecha
        //          })
        //          .ToList();

        //    cbOrden.DataSource = ordenVenta;
        //    cbOrden.DisplayMember = "OrdenProducto";
        //    cbOrden.ValueMember = "OrdenVentaId";
            
        //    if (ordenVenta != null)
        //    {
        //        txtOperacion.Text = ordenVenta.FirstOrDefault().NumOperacion.ToString();
        //        txtCliente.Text = ordenVenta.FirstOrDefault().Cliente;
        //    }

        //    var caja = Context.Caja
        //        .Where(x => x.OrdenVentaId == Id)
        //        .Any();

        //    if (caja.Equals(true))
        //    {
        //        txtCajaDesde.Enabled = true;
        //        txtCajaHasta.Enabled = true;

        //        var cajaDesde = Context.Caja
        //           .Where(x => x.OrdenVentaId == Id)
        //           .OrderBy(x => x.NumeroCaja)
        //           .FirstOrDefault();

        //        txtCajaDesde.Text = cajaDesde.NumeroCaja.ToString();

        //        var cajaHasta = Context.Caja
        //            .Where(x => x.OrdenVentaId == Id)
        //            .OrderByDescending(x => x.NumeroCaja)
        //            .FirstOrDefault();

        //        txtCajaHasta.Text = cajaHasta.NumeroCaja.ToString();
        //    }
        //    else
        //    {
        //        txtCajaDesde.Text = "0";
        //        txtCajaHasta.Text = "0";
        //    }
        //}
        
        private void Modificar()
        {
            if (btnModificar.Text == DevConstantes.HabilitarEdicion)
            {
                Habilitar();
                btnModificar.Text = DevConstantes.Modificar;
            }
            else
            {
                if (ValidarCampos())
                {
                    //ModificarDatos();
                }
            }      
        }

        //private void ModificarDatos()
        //{
        //    var ordenProducto = cbOrden.SelectedItem as dynamic;

        //    long cbOrdenProducto = ordenProducto.NumOrden;

        //    var orden = Context.OrdenVenta
        //        .Where(x => x.NumOrden == cbOrdenProducto)
        //        .FirstOrDefault();

        //    if (orden != null)
        //    {
        //        var ordenVenta = Context.OrdenVenta.Find(orden.Id);
                
        //        if (ordenVenta != null)
        //        {
        //            ordenVenta.DesdeCaja = long.Parse(txtCajaDesde.Text);
        //            ordenVenta.HastaCaja = long.Parse(txtCajaHasta.Text);
        //            Context.Entry(ordenVenta).State = EntityState.Modified;
        //            Context.SaveChanges();

        //            for(long i = ordenVenta.DesdeCaja.Value ; i<= ordenVenta.HastaCaja; i++)
        //            {
        //                var caja = Context.Caja
        //                    .Where(x => x.NumeroCaja == i)
        //                    .FirstOrDefault();

        //                AsociarOVaCaja(ordenVenta.Id,caja.Id);
        //                //RegistrarMovimiento(caja.Id, 1, ordenVenta.Fecha);   
        //            }
        //        }

        //        IEnlaceActualizar mienlace = this.Owner as Form_AdministracionOrdenVenta;

        //        if (mienlace != null)
        //        {
        //            mienlace.Enviar(true);
        //        }

        //        this.Close();
        //    }
        //}

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
                Context.Entry(cata).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }

        private bool ValidarCampos()
        {
            long valorDesde = 0;

            long valorHasta = 0;

            bool desde = long.TryParse(txtCajaDesde.Text, out valorDesde);
            
            bool hasta = long.TryParse(txtCajaHasta.Text, out valorHasta);
            
            if (desde == true && hasta == true)
            {
                for (long i = valorDesde; i <= valorHasta; i++)
                {
                    var caja = Context.Caja
                        .Where(x => x.NumeroCaja == i)
                        .Any();

                    if (caja.Equals(false))
                    {
                        MessageBox.Show("No existe la caja N°: " + i,
                            "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            MessageBox.Show("Debe ingresar número de cajas válidas.",
                         "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Deshabilitar();
            var resultado = MessageBox.Show("¿Desea eliminar esta orden de venta?",
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
            var ordenProducto = cbOrden.SelectedItem as dynamic;

            long cbOrdenProducto = ordenProducto.NumOrden;

            var orden = Context.OrdenVenta
                .Where(x => x.NumOrden == cbOrdenProducto)
                .FirstOrDefault();

            if (orden != null)
            {
                var ordenVenta = Context.OrdenVenta.Find(orden.Id);
                if (ordenVenta != null)
                {
                    Context.Entry(ordenVenta).State = EntityState.Deleted;
                    Context.SaveChanges();
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
                btnModificar.Text = DevConstantes.HabilitarEdicion;
            }
            else
            {
                btnModificar.Text = DevConstantes.Modificar;
            }
        }

        private void Deshabilitar()
        {
            txtOperacion.Enabled = false;
            txtCliente.Enabled = false;
            cbOrden.Enabled = false;
        }

        private void Habilitar()
        {
            txtOperacion.Enabled = true;
            txtCliente.Enabled = true;
            cbOrden.Enabled = true;
            txtCajaDesde.Enabled = true;
            txtCajaHasta.Enabled = true;
        }

        //private Guid RegistrarMovimiento(Guid Id, double kilos, DateTime fecha)
        //{
        //    Movimiento movimiento;

        //    movimiento = new Movimiento();
        //    movimiento.Id = Guid.NewGuid();
        //    movimiento.Fecha = fecha;
        //    movimiento.TransaccionId = Id;
        //    movimiento.Documento = DevConstantes.Transferencia;
        //    movimiento.Unidad = DevConstantes.Caja;
        //    movimiento.Ingreso = 0;
        //    movimiento.Egreso = kilos;

        //    var deposito = Context.Vw_Deposito
        //        .Where(x => x.nombre == DevConstantes.Deposito)
        //        .FirstOrDefault();

        //    if (deposito != null)
        //    {
        //        movimiento.DepositoId = deposito.id;
        //    }

        //    Context.Movimiento.Add(movimiento);
        //    Context.SaveChanges();

        //    return movimiento.Id;
        //}        
    }
}