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
using Extensions;
using System.Linq.Expressions;
using DevExpress.Utils;
using CooperativaProduccion.Helpers.GridRecords;
using DevExpress.XtraGrid.Views.Grid;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoGestionClasificacion : DevExpress.XtraBars.Ribbon.RibbonForm,IEnlaceActualizar
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;
        private Guid ParcialId;

        public Form_RomaneoGestionClasificacion(bool GestionReclasificaion)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar(GestionReclasificaion);
        }

        private void Iniciar(bool GestionReclasificaion)
        {
           
        }
        
        private void CargarCombo()
        {
            var tipotabaco = Context.Vw_TipoTabaco
                .Where(x => x.RUBRO_ID != null)
                .ToList();

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";
        }

        private void BuscarProductor()
        {
            var result = (
                from a in Context.Vw_Productor
                select new
                {
                    full = a.nrofet + a.NOMBRE + a.CUIT,
                    ID = a.ID,
                    FET = a.nrofet,
                    PRODUCTOR = a.NOMBRE,
                    CUIT = a.CUIT,
                    PROVINCIA = a.Provincia
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
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
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
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtProductor.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();

                    }
                    else
                    {
                        MessageBox.Show("N° de Fet no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtProductor.Text))
            {
                var count = result
                    .Where(r => r.PRODUCTOR.Contains(txtProductor.Text))
                    .Count();
                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtProductor.Text;
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtProductor.Text))
                      .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtProductor.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void BuscarRomaneo()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            List<GridRomaneo> lista = new List<GridRomaneo>();

            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date
                && x.FechaRomaneo <= dpHastaRomaneo.Value.Date);
          
            if (ProductorId != Guid.Empty)
            {
                pred = pred.And(x => x.ProductorId == ProductorId);
            }
                        
            pred = pred.And(x => x.Tabaco == cbTabaco.Text);
            
            var liquidaciones =
                (from a in Context.Vw_Romaneo
                     .Where(pred)
                 select new
                 {
                     ID = a.PesadaId,
                     FECHA = a.FechaRomaneo,
                     FET = a.nrofet,
                     PRODUCTOR = a.NOMBRE,
                     PROVINCIA = a.Provincia,
                     TABACO = a.Tabaco
                 })
               .OrderByDescending(x => x.FECHA)
               .ThenBy(x => x.FET)
               .ToList();

            foreach (var liquidacion in liquidaciones)
            {
                var liquidacionDetalle =
                    (from pd in Context.PesadaDetalle
                        .Where(x => x.PesadaId == liquidacion.ID)
                     join p in Context.Vw_Clase
                     on pd.ClaseId equals p.ID
                     join c in Context.Vw_Clase
                     on pd.ReclasificacionId equals c.ID into romaneo
                     from r in romaneo.DefaultIfEmpty()
                     select new
                     {
                         Id = pd.Id,
                         Clase = p.NOMBRE,
                         Fardo = pd.NumFardo,
                         Kilos = pd.Kilos,
                         Reclasificacion = r.NOMBRE
                     })
                    .ToList();
                                
                var rowsDetalle = liquidacionDetalle.Select(x =>
                    new GridRomaneoDetalle()
                    {
                        Id = x.Id,
                        Clase = x.Clase,
                        Fardos = x.Fardo,
                        Kilos = x.Kilos,
                        Reclasificacion = x.Reclasificacion
                    })
                    .OrderBy(x => x.Fardos)
                    .ToList();

                var rowRomaneo = new GridRomaneo();
                rowRomaneo.PesadaId = liquidacion.ID;
                rowRomaneo.FechaRomaneo = liquidacion.FECHA;
                rowRomaneo.nrofet = liquidacion.FET;
                rowRomaneo.NOMBRE = liquidacion.PRODUCTOR;
                rowRomaneo.Provincia = liquidacion.PROVINCIA;
                rowRomaneo.Tabaco = liquidacion.TABACO;
                rowRomaneo.Detalle = rowsDetalle;
                lista.Add(rowRomaneo);
            }

            gridControlRomaneo.DataSource = new BindingList<GridRomaneo>(lista);
            gridViewRomaneo.Columns[0].Visible = false;
            gridViewRomaneo.Columns[1].Caption = "Fecha de Romaneo";
            gridViewRomaneo.Columns[1].Width = 60;
            gridViewRomaneo.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].Caption = "FET";
            gridViewRomaneo.Columns[2].Width = 60;
            gridViewRomaneo.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[3].Caption = "Productor";
            gridViewRomaneo.Columns[3].Width = 200;
            gridViewRomaneo.Columns[4].Caption = "Provincia";
            gridViewRomaneo.Columns[4].Width = 70;
            gridViewRomaneo.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[5].Caption = "Tabaco";
            gridViewRomaneo.Columns[5].Width = 90;
            gridViewRomaneo.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewRomaneo.RowCount; i++)
            {
                gridViewRomaneo.SelectRow(i);
            }
        }

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFet.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtProductor.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtProductor.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void txtProductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtProductor.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtFet.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtFet.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void btnBuscarRomaneo_Click(object sender, EventArgs e)
        {
            BuscarRomaneo();
        }

        private void gridViewRomaneo_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView master = sender as GridView;
            GridView detail = master.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detail.Columns[0].Visible = false;
            detail.DoubleClick += new EventHandler(gridViewRomaneoDetalle_DoubleClick);
        }

        private void gridViewRomaneoDetalle_DoubleClick(object sender, EventArgs e)
        {
            GridView parcial = sender as GridView;
            ParcialId = new Guid(parcial
                          .GetRowCellValue(parcial.FocusedRowHandle, "Id")
                          .ToString());

            var novedad = new Form_RomaneoReclasificacion(ParcialId);
            novedad.ShowDialog(this);
        }

        public void Enviar(bool Enviar)
        {
            if (Enviar.Equals(true))
            {
                BuscarRomaneo();
            }
        }
    }
}