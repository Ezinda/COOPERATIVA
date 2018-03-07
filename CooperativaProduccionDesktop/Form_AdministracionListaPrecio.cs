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
using System.Diagnostics;
using System.IO;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionListaPrecio : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlaceActualizar
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ClaseId; 
       
        public Form_AdministracionListaPrecio()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Buscar();
        }

        #region Method Code
        
        #endregion
   
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
 
        #region Method Dev

        public void Buscar()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

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
                gridListaPrecio.DataSource = results;
                gridViewListaPrecio.Columns["ID"].Visible = false;
            }
        }

        #endregion

        private void btnExportar_Click(object sender, EventArgs e)
        {
      
        }

        private void CreateIfMissing(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }

        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
            }
            catch
            {
                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizarPrecio_Click(object sender, EventArgs e)
        {
            if (gridViewListaPrecio.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewListaPrecio.DataRowCount; i++)
                {
                    if (gridViewListaPrecio.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewListaPrecio.GetRowCellValue(i, "ID").ToString());
                        var actualizar = new Form_AdministracionActualizarPrecio(Id);
                        actualizar.ShowDialog(this);
                    }
                }
            }
        }


        public void Enviar(bool Enviar)
        {
            Buscar();
        }
    }
}