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
using CooperativaProduccion.Reports;
using DevExpress.XtraReports.UI;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionActualizarPrecio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities _context { get; set; }
       
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        
        private Guid _claseId;

        public Form_AdministracionActualizarPrecio(Guid ClaseId)
        {
            InitializeComponent();

            _context = new CooperativaProduccionEntities();
            CargarClase(ClaseId);
        }

        private void CargarClase(Guid ClaseId)
        {
            var clase = _context.Vw_Clase
                .Where(x => x.ID == ClaseId 
                    && x.Vigente == true)
                    .FirstOrDefault();
            
            if (clase != null)
            {
                var claseId = _context.Clase.Where(x => x.ClaseId == clase.ID).FirstOrDefault();
                _claseId = claseId.Id;
                txtClase.Text = clase.NOMBRE;
                txtTipoTabaco.Text = clase.DESCRIPCION;
                txtOrden.Text = clase.Orden != null ? clase.Orden.Value.ToString() : string.Empty;
                txtPrecio.Text = clase.PRECIOCOMPRA != null ? clase.PRECIOCOMPRA.Value.ToString("F") : "0";
            }
        }

        public bool NumberField(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 44)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtKilos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumberField(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GrabarPrecio();
        }

        private void GrabarPrecio()
        {
            try
            {
                var clase = _context.Clase.Find(_claseId);
                if (clase != null)
                {
                    clase.FechaModificacion = DateTime.Now.Date;
                    clase.Valor = decimal.Parse(txtPrecio.Text);
                    clase.Vigente = true;
                    _context.Entry(clase).State = EntityState.Modified;
                    _context.SaveChanges();

                    IEnlaceActualizar mienlace = this.Owner as Form_AdministracionListaPrecio;
                    if (mienlace != null)
                    {
                        mienlace.Enviar(true);
                    }
                    this.Dispose();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}