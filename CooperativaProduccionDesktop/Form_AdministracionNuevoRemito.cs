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

namespace CooperativaProduccion
{
    public partial class Form_AdministracionNuevoRemito : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_AdministracionNuevoRemito(Guid OrdenVentaId)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarDatos(OrdenVentaId);
        }

        private void CargarDatos(Guid Id)
        {        
            var ordenVenta =
                    (from o in Context.OrdenVenta.Where(x => x.Id == Id).AsEnumerable()
                     join p in Context.Vw_Producto
                     on o.ProductoId equals p.ID
                     join c in Context.Vw_Cliente
                     on o.ClienteId equals c.ID
                     select new
                     {
                         OrdenVentaId = o.Id,
                         NumOperacion = o.NumOperacion,
                         NumOrden = o.NumOrden,
                         Cliente = c.RAZONSOCIAL,
                         Cuit = c.CUIT.Contains(DevConstantes.XX) ? c.CUITE : c.CUIT,
                         Producto = p.DESCRIPCION,
                         CajaDesde = o.DesdeCaja,
                         CajaHasta = o.HastaCaja,
                         Fecha = o.Fecha,
                         Pendiente = o.Pendiente == true ?
                            DevConstantes.SI : DevConstantes.NO
                     })
                     .FirstOrDefault();

            if (ordenVenta != null)
            {
                txtNumOperacion.Text = ordenVenta.NumOperacion.ToString();
                dpOrdenVenta.Value = ordenVenta.Fecha;
                txtCliente.Text = ordenVenta.Cliente;
                txtCuit.Text = ordenVenta.Cuit;
            }

        }

      
    }
}