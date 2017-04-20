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
    public partial class Form_AdministracionNuevaOrdenVenta : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid OrdenVentaId;

        public Form_AdministracionNuevaOrdenVenta(Guid Id)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
                       
        }

        private void Form_AdministracionNuevaOrdenVenta_Load(object sender, EventArgs e)
        {

        }

     
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           
            var resultado = MessageBox.Show("¿Desea eliminar esta orden de venta?",
                 "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar esta orden de venta?",
                   "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
           
        }

 

     
    }
}