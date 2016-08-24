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
    public partial class Form_RomaneoPesada : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ProductorId;
       
        public Form_RomaneoPesada()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }
        }

        private void Buscar()
        {
            var result = Context.Vw_Preingreso
                    .Where(x => x.Estado == true)
                    .OrderBy(x => x.Fecha)
                    .ThenBy(x => x.Hora)
                    .ToList();

            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                var preingreso = result
                    .Where(r => r.Fet.Contains(txtFet.Text))
                    .FirstOrDefault();
                if(preingreso.Fet!=null)
                {
                    ProductorId = preingreso.ProductorId;
                    txtFet.Text = preingreso.Fet.ToString();
                    txtNombre.Text = preingreso.Nombre;
                    txtCuit.Text = preingreso.Cuit;
                    txtProvincia.Text = preingreso.Provincia;
                    txtPreingreso.Text = preingreso.NumeroPreingreso.ToString();
                }
            }
        }
  
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtPreingreso.Text = nombre;
            var empleado = Context.Productor
                .Where(x => x.Id == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = empleado.Cuit;
            txtProvincia.Text = empleado.Provincia;
        }

        private void barEditItem2_ItemPress(object sender, ItemClickEventArgs e)
        {

        }

        private void barEditItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
          }
    }
}