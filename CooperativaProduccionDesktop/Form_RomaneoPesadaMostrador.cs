using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoPesadaMostrador : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlaceMostradorPesada
    {
        Form_RomaneoPesada ownerForm = null;

        public Form_RomaneoPesadaMostrador(Form_RomaneoPesada ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void IEnlaceMostradorPesada.Enviar(string productor, string cuit)
        {
            lblProductor.Text = productor;
            lblCuit.Text = cuit;
        }

    }
}