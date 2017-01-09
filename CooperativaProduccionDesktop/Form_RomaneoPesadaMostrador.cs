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
    public partial class Form_RomaneoPesadaMostrador : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string nombre;
        public string cuit;
        public string totalkg;
        public string clase;
        public string numFardo;

        public Form_RomaneoPesadaMostrador()
        {
            InitializeComponent();
        }

        #region Method Dev

        public void CargarDatos()
        {
            lblProductor.Text = nombre;
            lblCuit.Text = cuit;
        }

        public void CargarFardo()
        {
            //lblClase.Text = clase;
            lblClase.Invoke((MethodInvoker)(() => lblClase.Text = clase));
            //lblFardo.Text = numFardo;
            lblFardo.Invoke((MethodInvoker)(() => lblFardo.Text = numFardo));
            //lblTotalKg.Text = totalkg;
            lblTotalKg.Invoke((MethodInvoker)(() => lblTotalKg.Text = totalkg));
            
        }
        
        #endregion
    }
}