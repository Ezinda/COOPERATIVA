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
    public partial class Form_InventarioFardos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_InventarioFardos()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        #region Method Code

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var result = (
                from a in Context.Vw_Movimiento
                select new
                {
                    a.Id,
                    a.NumFardo,
                    a.Clase,
                    a.Kilos,
                    a.NumRomaneo,
                    a.Productor,
                    a.Fet,
                    a.Cuit,
                    a.Provincia,
                    a.Fecha,
                    a.Unidad,
                    a.Ingreso,
                    a.Egreso
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            if (result.Count > 0)
            {
                gridControlFardos.DataSource = result;
                gridViewFardos.Columns[0].Visible = false;
            }
        }

        #endregion

        #region Method Dev
        private void CargarCombo()
        {
            var tipotabaco = Context.Vw_TipoTabaco
              .Where(x => x.RUBRO_ID != null)
              .ToList();

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";

            var clase = Context.Vw_Clase
                .Where( x => x.Vigente == true)
                .ToList();

            cbClase.DataSource = clase;
            cbClase.DisplayMember = "Nombre";
            cbClase.ValueMember = "Id";
        }


        #endregion

        private void cbTabaco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkTabaco.Checked)
            {
                Guid Tabaco = Guid.Parse(cbTabaco.SelectedValue.ToString());

                var clase = Context.Vw_Clase
                .Where(x => x.ID_PRODUCTO == Tabaco
                    && x.Vigente == true)
                .ToList();

                cbClase.DataSource = clase;
                cbClase.DisplayMember = "Nombre";
                cbClase.ValueMember = "Id";
            }
        }

        private void checkTabaco_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTabaco.Checked)
            {
                Guid Tabaco = Guid.Parse(cbTabaco.SelectedValue.ToString());

                var clase = Context.Vw_Clase
                .Where(x => x.ID_PRODUCTO == Tabaco
                    && x.Vigente == true)
                .ToList();

                cbClase.DataSource = clase;
                cbClase.DisplayMember = "Nombre";
                cbClase.ValueMember = "Id";
            }
        }
    }
}