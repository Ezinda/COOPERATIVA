using CooperativaProduccion.Helpers;
using CooperativaProduccion.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CooperativaProduccion
{
    public partial class Form_ProduccionRegistrosDeRendimiento : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        private List<BlendViewModel> _blends;

        public Form_ProduccionRegistrosDeRendimiento(IBlendManager blendManager)
        {
            InitializeComponent();

            _blendManager = blendManager;

            this.Load += Form_ProduccionRegistrosDeCalidad_Load;
            this.checkedListBlend.ItemCheck += checkedListBlend_ItemCheck;
            this.btnDeseleccionar.Click += btnDeseleccionar_Click;
            this.btnGenerar.Click += btnGenerar_Click;
        }

        void Form_ProduccionRegistrosDeCalidad_Load(object sender, EventArgs e)
        {
            _blends = _blendManager.ListarBlends();

            checkedListBlend.Items.Clear();

            foreach (var blend in _blends)
            {
                checkedListBlend.Items.Add(blend.Descripcion, true);
            }

            btnDeseleccionar.Text = "Deseleccionar todos";
        }

        void checkedListBlend_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBlend.CheckedItems.Count == checkedListBlend.Items.Count)
            {
                btnDeseleccionar.Text = "Deseleccionar todos";
            }
            else if (checkedListBlend.CheckedItems.Count > 0)
            {
                btnDeseleccionar.Text = "Deseleccionar todos";
            }
            else
            {
                btnDeseleccionar.Text = "Seleccionar todos";
            }
        }

        void btnDeseleccionar_Click(object sender, EventArgs e)
        {
            if (btnDeseleccionar.Text == "Deseleccionar todos")
            {
                for (int i = 0; i < checkedListBlend.Items.Count; i++)
                {
                    checkedListBlend.SetItemChecked(i, false);
                }

                btnDeseleccionar.Text = "Seleccionar todos";
            }
            else
            {
                for (int i = 0; i < checkedListBlend.Items.Count; i++)
                {
                    checkedListBlend.SetItemChecked(i, true);
                }
            }
        }

        void btnGenerar_Click(object sender, EventArgs e)
        {
            if (dpDesde.Value.Year != dpHasta.Value.Year)
            {
                MessageBox.Show("El rango de fechas debe tener el mismo año (periodo)", "Error en rango de fecha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkedListBlend.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un blend de la lista.",
                    "No se ha seleccionado blend",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var blendsSeleccionados = checkedListBlend.CheckedItems.OfType<String>().ToList();
            var blendsId = _blends.Where(x => blendsSeleccionados.Contains(x.Descripcion)).Select(x => x.Id).ToArray();
            var periodo = dpDesde.Value.Year;
            var desde = dpDesde.Value.Date;
            var hasta = dpHasta.Value.Date;

            //var ordenesDeProduccion = _blendManager.ListarOrdenesDeProduccion(periodo)
            //    .OrderBy(x => x.OrdenDeProduccion)
            //    .ToList();

            var listaDatosDeRendimiento = _blendManager.ListarDatosDeRendimiento(blendsId, desde, hasta);

            if (listaDatosDeRendimiento.Count == 0)
            {
                MessageBox.Show("No hay registros para listar.", "Sin registros", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            try
            {
                var filename = CrearSubDirectorio("Laboratorio")
                    + @"/"
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture)
                        .Replace(":", "")
                        .Replace(".", "")
                        .Replace("-", "")
                        .Replace(" ", "")
                    + "-CalculoDeRendimiento.xls";

                CrearExcel(filename, periodo, listaDatosDeRendimiento);

                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No se puede crear directorio")
                {
                    MessageBox.Show(ex.InnerException.ToString(), "ERROR al intentar crear un directorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message == "Error Office.Interop")
                {
                    MessageBox.Show(ex.InnerException.ToString(), "ERROR con Office.Interop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.ToString(), "ERROR no reconocido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string CrearSubDirectorio(string nombre)
        {
            var rootPath = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(rootPath);

            CreateIfMissing(rootPath + @"\" + nombre);

            return rootPath + @"\" + nombre;
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
                throw new Exception("No se puede crear directorio", ioex);
            }
        }

        private void CrearExcel(string filename,
            int periodo,
            List<RendimientoViewModel> listaRendimiento)
        {
            try
            {
                var oXL = new Microsoft.Office.Interop.Excel.Application();

                oXL.Visible = false;
                oXL.UserControl = false;

                var oWB = (Microsoft.Office.Interop.Excel._Workbook)oXL.Workbooks.Open(System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "/PlantillaRendimiento.xls");
                var oSheetEnBLanco = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets["PlantillaEnBlanco"];

                var blendPorPeriodo = listaRendimiento
                    .Select(x => x.Blend)
                    .Distinct()
                    .OrderBy(x => x.Periodo)
                    .ThenBy(x => x.Descripcion)
                    .ToList();

                if (blendPorPeriodo.Count > 0)
                {
                    foreach (var blend in blendPorPeriodo)
                    {
                        oSheetEnBLanco.Copy(Type.Missing, oWB.Sheets[oWB.Sheets.Count]);
                        oWB.Sheets[oWB.Sheets.Count].Name = blend.Descripcion + "-" + blend.Periodo;

                        var oSheetBlendPeriodo = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets[blend.Descripcion + "-" + blend.Periodo];
                        var RngToCopy = oSheetBlendPeriodo.get_Range("A7", "L7").EntireRow.Copy(Type.Missing);

                        // Celda B3
                        oSheetBlendPeriodo.Cells[3, 2] = "BLEND: " + blend.Descripcion;

                        var registros = listaRendimiento.Where(x => x.Blend == blend).OrderBy(x => x.Fecha).ToList();

                        for (int i = 0; i < registros.Count; i++)
                        {
                            var row = (i + 7);

                            Microsoft.Office.Interop.Excel.Range RngToInsert = oSheetBlendPeriodo.get_Range("A" + row, Type.Missing).EntireRow;
                            RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy);

                            oSheetBlendPeriodo.Cells[row, 01].Value2 = registros[i].BlendDescripcion;
                            oSheetBlendPeriodo.Cells[row, 02].Value2 = registros[i].Fecha;
                            oSheetBlendPeriodo.Cells[row, 03].Value2 = registros[i].OrdenDeProduccion;
                            oSheetBlendPeriodo.Cells[row, 04].Value2 = registros[i].Corrida;
                            oSheetBlendPeriodo.Cells[row, 05].Value2 = String.Empty;
                            oSheetBlendPeriodo.Cells[row, 06].Value2 = registros[i].PrimeraCaja;
                            oSheetBlendPeriodo.Cells[row, 07].Value2 = registros[i].UltimaCaja;
                            oSheetBlendPeriodo.Cells[row, 08].Value2 = registros[i].NumeroCajas;
                            oSheetBlendPeriodo.Cells[row, 09].Value2 = registros[i].Kilos;
                            oSheetBlendPeriodo.Cells[row, 10].Value2 = String.Empty;
                        }

                    // borrar la ultima fila la cual esta vacia vacia
                    ((Microsoft.Office.Interop.Excel.Range)oSheetBlendPeriodo.Rows[(registros.Count + 7), System.Reflection.Missing.Value]).Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);

                        oSheetBlendPeriodo.get_Range("H5", "H5").Formula = "=SUM(H7:H9684)";
                        oSheetBlendPeriodo.get_Range("I5", "I5").Formula = "=SUM(I7:I9685)";
                        oSheetBlendPeriodo.get_Range("J5", "J5").Formula = "=SUM(J7:J9685)";
                        oSheetBlendPeriodo.get_Range("K5", "K5").Formula = "=SUM(K7:K685)";
                        oSheetBlendPeriodo.get_Range("L5", "L5").Formula = "=IF(ISERROR(+((K5)/(I5))*100), 0, +((K5)/(I5))*100)";

                        RngToCopy = oSheetBlendPeriodo.get_Range("B" + (registros.Count - 1 + 7 + 5), "D" + (registros.Count - 1 + 7 + 5)).EntireRow.Copy(Type.Missing);

                        var taraActual = -1m;
                        var taraActualRow = -1;

                        for (int i = 0; i < registros.Count; i++)
                        {
                            if (taraActual != registros[i].Tara)
                            {
                                taraActual = registros[i].Tara;

                                if (taraActualRow == -1)
                                {
                                    taraActualRow = (registros.Count - 1 + 7 + 5);
                                }
                                else
                                {
                                    taraActualRow++;
                                }

                                Microsoft.Office.Interop.Excel.Range RngToInsert = oSheetBlendPeriodo.get_Range("B" + taraActualRow, Type.Missing).EntireRow;
                                RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy);

                                oSheetBlendPeriodo.Cells[taraActualRow, 02].Value2 = registros[i].PrimeraCaja;
                                oSheetBlendPeriodo.Cells[taraActualRow, 03].Value2 = registros[i].UltimaCaja;
                                oSheetBlendPeriodo.Cells[taraActualRow, 04].Value2 = registros[i].Tara;
                            }
                            else
                            {
                                oSheetBlendPeriodo.Cells[taraActualRow, 03].Value2 = registros[i].UltimaCaja;
                            }
                        }

                        if (taraActualRow != -1)
                        {
                            // borrar la ultima fila la cual esta vacia vacia
                            ((Microsoft.Office.Interop.Excel.Range)oSheetBlendPeriodo.Rows[taraActualRow + 1, System.Reflection.Missing.Value]).Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                        oSheetBlendPeriodo.get_Range("K7", "K" + ((7 - 1) + registros.Count)).Formula = "=(H7*200)";
                        oSheetBlendPeriodo.get_Range("L7", "L" + ((7 - 1) + registros.Count)).Formula = "=IF(ISERROR(+((K7)/(I7))*100), 0, +((K7)/(I7))*100)";
                    }

                ((Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets["PlantillaEnBlanco"]).Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVeryHidden;

                    oWB.SaveAs(filename,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
                        Type.Missing,
                        Type.Missing,
                        false,
                        false,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Type.Missing,
                        Type.Missing,
                        Type.Missing,
                        Type.Missing,
                        Type.Missing);
                }
                else
                {
                    throw new Exception("No hay registros");
                }
                

                oWB.Close();

                oXL.Quit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Office.Interop", ex);
            }
        }
    }
}
