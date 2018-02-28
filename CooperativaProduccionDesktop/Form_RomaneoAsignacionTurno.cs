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

namespace CooperativaProduccion
{
    public partial class Form_RomaneoAsignacionTurno : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities _context { get; set; }
       
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        
        private Guid _productorId;
    
        public Form_RomaneoAsignacionTurno()
        {
            InitializeComponent();

            _context = new CooperativaProduccionEntities();
            _productorId = Guid.Empty;
            txtTotalKg.Text = CalcularTotalKilos(dpFechaSolicitud.Value.Date).ToString();
  
        }

        private void Buscar()
        {
            var result = (
              from a in _context.Vw_Productor
              select new
              {
                  full = a.nrofet + a.NOMBRE + a.CUIT,
                  ID = a.ID,
                  FET = a.nrofet,
                  PRODUCTOR = a.NOMBRE,
                  CUIT = a.CUIT,
                  PROVINCIA = a.Provincia,
                  TELEFONO = a.TELEFONO
              });

            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var count = result
                    .Where(r => r.FET.Equals(txtFet.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtFet.Text;
                    _formBuscarProductor.target = DevConstantes.AsignacionTurno;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();
                    
                    if (busqueda != null)
                    {
                        _productorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET;
                        txtNombre.Text = busqueda.PRODUCTOR;
                        txtProvincia.Text = busqueda.PROVINCIA;
                        txtCuit.Text = busqueda.CUIT;
                        txtTelefono.Text = busqueda.TELEFONO;
                    }
                    else
                    {
                        MessageBox.Show("N° de Fet no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                var count = result
                    .Where(r => r.PRODUCTOR.Contains(txtNombre.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.nombre = txtNombre.Text;
                    _formBuscarProductor.target = DevConstantes.AsignacionTurno;
                    _formBuscarProductor.BuscarNombre();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtNombre.Text))
                      .FirstOrDefault();

                    if (busqueda != null)
                    {
                        _productorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET;
                        txtNombre.Text = busqueda.PRODUCTOR;
                        txtProvincia.Text = busqueda.PROVINCIA;
                        txtCuit.Text = busqueda.CUIT;
                        txtTelefono.Text = busqueda.TELEFONO;
                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            _productorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = _context.Vw_Productor
                .Where(x => x.ID == _productorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ?
                           string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ?
                string.Empty : empleado.Provincia;
            txtTelefono.Text = empleado.TELEFONO;
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }

            if (e.KeyChar == 8)
            {
                txtNombre.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
            }
        }

        private void btnBuscarProductorLegajo_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }

            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
            }
        }

        private void btnBuscarProductorNombre_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txtKilos_TextChanged(object sender, EventArgs e)
        {
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
            GrabarTurno();
        }

        private void GrabarTurno()
        {
            try
            {
                Turno turno;
                turno = new Turno();
                turno.Id = Guid.NewGuid();
                turno.ProductorId = _productorId;
                turno.FechaSolicitud = DateTime.Now.Date;
                DateTime fechaTurno = dpFechaSolicitud.Value.Date;
                turno.FechaTurno = fechaTurno;
                Decimal kilos = Decimal.Parse(txtKilos.Text);
                turno.Kilos = kilos;
                _context.Turno.Add(turno);
                _context.SaveChanges();
                ImprimirTurno(turno.Id);
                IEnlaceActualizar mienlace = this.Owner as Form_RomaneoBuscarTurno;
                if (mienlace != null)
                {
                    mienlace.Enviar(true);
                }
                this.Dispose();
            }
            catch
            {
                throw;
            }
        }

        private void ImprimirTurno(Guid TurnoId)
        {
            var turno = _context.Turno
                .Where(x => x.Id == TurnoId)
                .FirstOrDefault();

            if (turno != null)
            {
                var reporte = new TurnoReport();

                var productor = _context.Vw_Productor
                    .Where(x => x.ID == turno.ProductorId)
                    .FirstOrDefault();

                reporte.Parameters["acopio"].Value = "ACOPIO CAMPAÑA " + DateTime.Now.Year;

                reporte.Parameters["productor"].Value = productor.NOMBRE;
                reporte.Parameters["fet"].Value = productor.nrofet;
                reporte.Parameters["dni"].Value = productor.CUIT;
                reporte.Parameters["domicilio"].Value = productor.DOMICILIO;
                reporte.Parameters["tel"].Value = productor.TELEFONO;

                reporte.Parameters["fechaSolicitud"].Value = turno.FechaSolicitud.ToShortDateString();
                reporte.Parameters["fechaTurno"].Value = turno.FechaTurno.ToShortDateString();
                reporte.Parameters["kg"].Value = turno.Kilos;

                using (ReportPrintTool tool = new ReportPrintTool(reporte))
                {
                    reporte.ShowPreviewMarginLines = false;
                    tool.PreviewForm.Text = "Etiqueta";
                    tool.ShowPreviewDialog();
                }
            }
        }

        private void dpFechaSolicitud_ValueChanged(object sender, EventArgs e)
        {
            txtTotalKg.Text = CalcularTotalKilos(dpFechaSolicitud.Value.Date).ToString();
        }
        
        private decimal CalcularTotalKilos(DateTime fecha)
        {
            decimal totalKilos = 0;
            
            var turnos = _context.Turno
                .Where(x => x.FechaTurno == fecha.Date)
                .ToList();
            
            if (turnos != null)
            {
                foreach (var turno in turnos)
                {
                    totalKilos = totalKilos + turno.Kilos;
                }
            }
            else
            {
                totalKilos = 0;
            }

            return totalKilos;
        }

    }
}