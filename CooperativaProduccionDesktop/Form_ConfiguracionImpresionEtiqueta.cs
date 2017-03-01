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
using DevExpress.Utils;
using System.IO;
using System.Drawing.Imaging;
using CooperativaProduccion.Reports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace CooperativaProduccion
{
    public partial class Form_ConfiguracionImpresionEtiqueta : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }

        public Form_ConfiguracionImpresionEtiqueta()
        {
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            Context = new CooperativaProduccionEntities();
            var result = (
             from a in Context.Vw_Clase
             select new
             {
                 Clase = a.NOMBRE,
                 Tabaco = a.DESCRIPCION.Equals(DevConstantes.TabacoReclasificacion) ?
                     DevConstantes.TabacoReclasificacion : a.DESCRIPCION
             })
             .OrderBy(x => x.Tabaco)
             .ThenBy(x => x.Clase)
             .ToList();

            gridControlClase.DataSource = result;
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea generar las etiquetas de clases?",
                "Confirmación de Datos", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            Imprimir();
        }

        private void Imprimir()
        {
            string path = @"C:\SystemDocumentsCooperativa";
            CreateIfMissing(path);
            path = @"C:\SystemDocumentsCooperativa\EtiquetasClases";
            CreateIfMissing(path);
            path = @"C:\SystemDocumentsCooperativa\EtiquetasClases\TabacoVirginia";
            CreateIfMissing(path);
            path = @"C:\SystemDocumentsCooperativa\EtiquetasClases\TabacoBurley";
            CreateIfMissing(path);
            path = @"C:\SystemDocumentsCooperativa\EtiquetasClases\TabacoReclasificacion";
            CreateIfMissing(path);
            if (gridViewClase.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewClase.DataRowCount; i++)
                {
                    if (gridViewClase.IsRowSelected(i))
                    {
                        try
                        {
                            var Tabaco = gridViewClase.GetRowCellValue(i, "Tabaco").ToString();
                            var Clase = gridViewClase.GetRowCellValue(i, "Clase").ToString();

                            var report = new ClasesReport();
                            report.BarCodeClase.Text = Clase;

                            if (Tabaco.Equals(DevConstantes.TabacoBurley))
                            {
                                path = @"C:\SystemDocumentsCooperativa\EtiquetasClases\\TabacoBurley\\" + Clase + ".png";

                            }
                            else if (Tabaco.Equals(DevConstantes.TabacoVirginia))
                            {
                                path = @"C:\SystemDocumentsCooperativa\EtiquetasClases\\TabacoVirginia\\" + Clase + ".png";
                            }
                            else
                            {
                                path = @"C:\SystemDocumentsCooperativa\EtiquetasClases\\TabacoReclasificacion\\" + Clase + ".png";
                            }

                            // Create a report instance.

                            // Get its Image export options.
                            ImageExportOptions imageOptions = report.ExportOptions.Image;

                            // Set Image-specific export options.
                            imageOptions.Format = ImageFormat.Png;

                            // Export the report to Image.
                            report.ExportToImage(path);
                           
                        }

                        catch (Exception Ex)
                        {
                            Console.WriteLine(Ex.ToString());
                        }
                    }
                }
            }
            MessageBox.Show("Se han generado las etiquetas.",
                               "Confirmación", MessageBoxButtons.OKCancel);
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

    }
    //private class ClaseImpresion
    //{
    //    System.Guid ID { get; set; }
    //    string NOMBRE { get; set; }
    //    string COD_PRODUCTO { get; set; }
    //    string DESCRIPCION { get; set; }
    //    Nullable<decimal> PRECIOCOMPRA { get; set; }
    //    Nullable<bool> Vigente { get; set; }
    //}
}