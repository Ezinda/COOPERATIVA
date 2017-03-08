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
using System.Globalization;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionListaPrecio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ClaseId; 
        public MaskedTextBox mask;
        private int currentRow;
        private bool resetRow = false;

        public Form_AdministracionListaPrecio()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            AddNewRow();
            Buscar();
            mask = new MaskedTextBox();
            mask.Visible = false;
            dgvListaPrecio.Controls.Add(mask);
            mask.Focus();
            mask.SelectionStart = 0;
            dgvListaPrecio.CellEndEdit +=
                new DataGridViewCellEventHandler(dgvListaPrecio_CellEndEdit);
            dgvListaPrecio.DataBindingComplete +=
                new DataGridViewBindingCompleteEventHandler(dgvListaPrecio_DataBindingComplete);

        }

        #region Method Code

        private void dgvListaPrecio_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvListaPrecio.CurrentCell.ColumnIndex == 2 || dgvListaPrecio.CurrentCell.ColumnIndex == 3)
            {
                mask.Focus();
                mask.SelectionStart = 0;
                Rectangle rect = dgvListaPrecio.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                mask.Location = rect.Location;
                mask.Size = rect.Size;
                mask.Text = "";

                if (dgvListaPrecio[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    mask.Text = dgvListaPrecio[e.ColumnIndex, e.RowIndex].Value.ToString();
                    mask.Focus();
                    mask.SelectionStart = 0;
                }
                mask.Visible = true;
            }
        }

        private void dgvListaPrecio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (mask.Visible)
            {
                if (dgvListaPrecio.CurrentCell.ColumnIndex == 2)
                {
                    dgvListaPrecio.CurrentCell.Value = mask.Text;
                    if (mask.Text != string.Empty)
                    {
                        var precio = (Math.Round(decimal.Parse(mask.Text), 2)).ToString("n2");
                        dgvListaPrecio.CurrentCell.Value = precio;
                        if (dgvListaPrecio[0, dgvListaPrecio.CurrentCell.RowIndex].Value != string.Empty)
                        {
                            Guid ClaseId = Guid.Parse(dgvListaPrecio[0, dgvListaPrecio.CurrentCell.RowIndex].Value.ToString());
                            ModificarClase(ClaseId, precio);
                        }
                    }
                    mask.Visible = false;
                }
                else if (dgvListaPrecio.CurrentCell.ColumnIndex == 3)
                {
                    dgvListaPrecio.CurrentCell.Value = mask.Text;
                    if (mask.Text != string.Empty)
                    {
                        var orden = mask.Text;
                        dgvListaPrecio.CurrentCell.Value = orden;
                        if (dgvListaPrecio[0, dgvListaPrecio.CurrentCell.RowIndex].Value != string.Empty)
                        {
                            Guid ClaseId = Guid.Parse(dgvListaPrecio[0, dgvListaPrecio.CurrentCell.RowIndex].Value.ToString());
                            ModificarOrden(ClaseId, orden);
                        }
                    }
                    mask.Visible = false;
                }
            }
        }

        private void ModificarOrden(Guid Id, string orden)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            int valor;

            if (Id != null)
            {
                var a = int.TryParse(orden, out valor);
                if (int.TryParse(orden, out valor))
                {
                    var clase = Context.Clase
                        .Where(x => x.ClaseId == Id
                            && x.Vigente == true)
                        .FirstOrDefault();

                    if (clase != null)
                    {
                        var cl = Context.Clase.Find(clase.Id);
                        if (cl != null)
                        {
                            cl.Orden = int.Parse(orden);
                            Context.Entry(cl).State = EntityState.Modified;
                            Context.SaveChanges();
                            Buscar();
                        }
                    }
                }
            }
        }

        private void dgvListaPrecio_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                dgvListaPrecio.CurrentCell = dgvListaPrecio.Rows[currentRow].Cells[0];
            }
        }

        private void dgvListaPrecio_SelectionChanged(object sender, EventArgs e)
        {
            dgvListaPrecio.SelectionChanged += new EventHandler(dgvListaPrecio_SelectionChanged);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (dgvListaPrecio.CurrentCell != null)
            {
                int icolumn = dgvListaPrecio.CurrentCell.ColumnIndex;
                int irow = dgvListaPrecio.CurrentCell.RowIndex;
                DataGridViewCell cell;
                if (keyData == Keys.Enter)
                {

                    if (dgvListaPrecio.CurrentCell.ColumnIndex == 2)
                    {
                        if (dgvListaPrecio.CurrentCell.RowIndex + 1 < dgvListaPrecio.RowCount)
                        {
                            cell = dgvListaPrecio.Rows[dgvListaPrecio.CurrentCell.RowIndex + 1].Cells[2];
                            dgvListaPrecio.CurrentCell = cell;
                            dgvListaPrecio.BeginEdit(true);
                            mask.Focus();
                            mask.SelectionStart = 0;

                            return true;
                        }
                    }
                    else if (dgvListaPrecio.CurrentCell.ColumnIndex == 3)
                    {
                        if (dgvListaPrecio.CurrentCell.RowIndex + 1 < dgvListaPrecio.RowCount)
                        {
                            cell = dgvListaPrecio.Rows[dgvListaPrecio.CurrentCell.RowIndex + 1].Cells[3];
                            dgvListaPrecio.CurrentCell = cell;
                            dgvListaPrecio.BeginEdit(true);
                            mask.Focus();
                            mask.SelectionStart = 0;

                            return true;
                        }
                    }
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);

                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Method Dev

        public void Buscar()
        {
            if (dgvListaPrecio.RowCount > 0)
            {
                dgvListaPrecio.Rows.Clear();
            }
            var results =
                (from a in Context.Vw_Clase
                 .Where(x => (x.Vigente == true || x.Vigente == null)
                    && x.ID_PRODUCTO != DevConstantes.Generico)
                    .OrderBy(x => x.DESCRIPCION)
                    .ThenBy(x => x.Orden)
                 select new
                 {
                     ID = a.ID,
                     CLASE = a.NOMBRE,
                     PRECIOCOMPRA = a.PRECIOCOMPRA,
                     ORDEN = a.Orden,
                     PRODUCTO = a.DESCRIPCION
                 })
                .ToList();

            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    this.dgvListaPrecio.Rows.Add(result.ID,result.CLASE,result.PRECIOCOMPRA,
                        result.ORDEN ,result.PRODUCTO);     
                }
                this.dgvListaPrecio.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            }
        }

        private void ModificarClase(Guid Id, string precio)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            decimal valor;

            if (Id != null)
            {
                var a = decimal.TryParse(precio, out valor);
                if (Decimal.TryParse(precio, out valor))
                {
                    var clase = Context.Clase
                        .Where(x => x.ClaseId == Id
                            && x.Vigente != false)
                        .FirstOrDefault();

                    if (clase != null)
                    {
                        if (clase.Valor != valor)
                        {
                            var clases = Context.Clase
                                .Where(x => x.ClaseId == Id)
                                .ToList();

                            foreach (var cla in clases)
                            {
                                var cl = Context.Clase.Find(cla.Id);
                                if (cl != null)
                                {
                                    cl.FechaModificacion = DateTime.Now;
                                    cl.Vigente = false;
                                    Context.Entry(cl).State = EntityState.Modified;
                                    Context.SaveChanges();
                                }
                            }

                            Clase Newclase;
                            Newclase = new Clase();
                            Newclase.Id = Guid.NewGuid();
                            Newclase.ClaseId = Id;
                            Newclase.Valor = valor;
                            Newclase.FechaModificacion = DateTime.Now;
                            Newclase.Vigente = true;
                            Context.Clase.Add(Newclase);
                            Context.SaveChanges();
                            Buscar();
                        }
                    }
                    else
                    {
                        Clase Newclase;
                        Newclase = new Clase();
                        Newclase.Id = Guid.NewGuid();
                        Newclase.ClaseId = Id;
                        Newclase.Valor = valor;
                        Newclase.FechaModificacion = DateTime.Now;
                        Newclase.Vigente = true;
                        Context.Clase.Add(Newclase);
                        Context.SaveChanges();
                        Buscar();
                    }
                }
                else
                {
                    MessageBox.Show("Formato no válido.",
                         "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun registro.",
                              "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewRowPrecio()
        {
            DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d3 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d4 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d5 = new DataGridViewTextBoxColumn();
            
            //Add Header Texts to be displayed on the Columns
            d1.HeaderText = "Id";
            d2.HeaderText = "Clase";
            d3.HeaderText = "Precio Compra";
            d4.HeaderText = "Orden";
            d5.HeaderText = "Producto";

            d1.Visible = false;
            d2.Width = 90;
            d3.Width = 90;
            d4.Width = 90;
            d5.Width = 100;
            
            //Add the Columns to the DataGridView
            dgvListaPrecio.Columns.AddRange(d1, d2, d3, d4 ,d5);
        }

        private void AddNewRow()
        {
            AddNewRowPrecio();
        }

        #endregion
    }
}