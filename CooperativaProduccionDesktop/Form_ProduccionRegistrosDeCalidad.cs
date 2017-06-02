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
    public partial class Form_ProduccionRegistrosDeCalidad : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IBlendManager _blendManager;

        private List<BlendViewModel> _blends;

        public Form_ProduccionRegistrosDeCalidad(IBlendManager blendManager)
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

            var ordenesDeProduccion = _blendManager.ListarOrdenesDeProduccion(periodo)
                .OrderBy(x => x.OrdenDeProduccion)
                .ToList();

            var registrosHumedad = _blendManager.ListarControlesDeHumedad(blendsId, desde, hasta);

            var listaHumedad = new List<CooperativaProduccion.ReportModels.ProduccionRegistroDatosDeCalidadAnualLamina>();

            foreach (var orden in ordenesDeProduccion)
            {
                if (orden.OrdenDeProduccion <= 0)
                {
                    continue;
                }

                var parteRegistrosHumedad = registrosHumedad.Where(x => x.Blend.Id == orden.Id).ToList();

                foreach (var item in parteRegistrosHumedad)
                {
                    foreach (var lineaControlHumedad in item.Lineas)
	                {
	                	 listaHumedad.Add(new CooperativaProduccion.ReportModels.ProduccionRegistroDatosDeCalidadAnualLamina()
                        {
                            Empresa = "COOPERATIVA",
                            Fecha = item.Fecha,
                            Hora = lineaControlHumedad.Hora,
                            Blend = item.Blend.Descripcion,
                            OrdenProduccion = orden.OrdenDeProduccion,
                            Corrida = item.Corrida,
                            CajaReferente = lineaControlHumedad.Caja,
                            
                            Brab = lineaControlHumedad.Humedad,

                            Temp = lineaControlHumedad.TemperaturaEmpaque,
                        });
	                }
                }
            }

            var muestras = _blendManager.ListarMuestrasConDetalle(blendsId, desde, hasta);

            var listaMuestra = new List<CooperativaProduccion.ReportModels.ProduccionRegistroDatosDeCalidadAnualLinea>();

            foreach (var orden in ordenesDeProduccion)
            {
                if (orden.OrdenDeProduccion <= 0)
                {
                    continue;
                }

                var parteMuestras = muestras.Where(x => x.Blend.Id == orden.Id).ToList();

                foreach (var item in parteMuestras)
                {
                    listaMuestra.Add(new CooperativaProduccion.ReportModels.ProduccionRegistroDatosDeCalidadAnualLinea()
                    {
                        Empresa = "COOPERATIVA",
                        Fecha = item.Fecha,
                        Hora = item.Hora,
                        Blend = item.Blend.Descripcion,
                        OrdenProduccion = orden.OrdenDeProduccion,
                        Corrida = item.Corrida,
                        CajaReferente = item.Caja,
                        PesoMuestra = Convert.ToInt32(item.PesoMuestra),
                        Uno = item.Lineas.Where(x => x.Tamanio == "1' y m/").Single().Porcentaje,
                        UnMedio = item.Lineas.Where(x => x.Tamanio == "1/2").Single().Porcentaje,
                        TotalUnMedio = -1m,
                        UnCuarto = item.Lineas.Where(x => x.Tamanio == "1/4").Single().Porcentaje,
                        UnOctavo = item.Lineas.Where(x => x.Tamanio == "1/8").Single().Porcentaje,
                        Pan = item.Lineas.Where(x => x.Tamanio == "PAN").Single().Porcentaje,
                        TotalUnCuarto = -1m,
                        PaloObjetable = item.Lineas.Where(x => x.Tamanio == "V/O BJ").Single().Porcentaje,
                        PaloTotal = item.Lineas.Where(x => x.Tamanio == "P. TOTAL").Single().Porcentaje,
                        PesoMuestraGr = 0,
                        UnoSinteticoGr = String.Empty,
                        UnoSinteticoCant = String.Empty,
                        UnoSinteticoPorc = String.Empty,
                        DosNaturalGr = String.Empty,
                        DosNaturalCant = String.Empty,
                        DosNaturalPorc = String.Empty,
                        TresAnimalGr = String.Empty,
                        TresAnimalCant = String.Empty,
                        TresAnimalPorc = String.Empty,
                        CuatroVegetalGr = String.Empty,
                        CuatroVegetalCant = String.Empty,
                        CuatroVegetalPorc = String.Empty,

                        Suma = -1m
                    });
                }
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
                    + "-RegistroDeCalidad.xls";

                CrearExcel(filename, periodo, listaHumedad, listaMuestra);

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
            List<CooperativaProduccion.ReportModels.ProduccionRegistroDatosDeCalidadAnualLamina> listaHumedad,
            List<CooperativaProduccion.ReportModels.ProduccionRegistroDatosDeCalidadAnualLinea> listaMuestra)
        {
            try
            {
                var oXL = new Microsoft.Office.Interop.Excel.Application();

                oXL.Visible = false;
                oXL.UserControl = false;

                var oWB = (Microsoft.Office.Interop.Excel._Workbook)oXL.Workbooks.Open(System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "/RegistroDatosDeCalidadTemplate.xls");

                var oSheetLamina = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets["Lamina"];

                for (int i = 0; i < listaHumedad.Count; i++)
                {
                    var row = (i + 24);

                    oSheetLamina.Cells[row, 01].Value2 = listaHumedad[i].Empresa;
                    oSheetLamina.Cells[row, 02].Value2 = listaHumedad[i].Fecha;
                    oSheetLamina.Cells[row, 03].Value2 = listaHumedad[i].Hora.ToString(@"hh\:mm");
                    oSheetLamina.Cells[row, 04].Value2 = listaHumedad[i].Blend;
                    oSheetLamina.Cells[row, 05].Value2 = listaHumedad[i].OrdenProduccion;
                    oSheetLamina.Cells[row, 06].Value2 = listaHumedad[i].Corrida;
                    oSheetLamina.Cells[row, 07].Value2 = listaHumedad[i].CajaReferente;
                    // +1
                    oSheetLamina.Cells[row, 09].Value2 = listaHumedad[i].Brab;
                    // +4
                    oSheetLamina.Cells[row, 14].Value2 = listaHumedad[i].Temp;
                }
                
                var oSheetLinea = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets["Linea"];

                // Celda F2
                oSheetLinea.Cells[2, 6] = "AÑO " + periodo;

                //oSheet.get_Range("A17", "AG" + (17 - 1) + lista.Count).Value2 = lista.ToArray();

                for (int i = 0; i < listaMuestra.Count; i++)
                {
                    var row = (i + 17);

                    oSheetLinea.Cells[row, 01].Value2 = listaMuestra[i].Empresa;
                    oSheetLinea.Cells[row, 02].Value2 = listaMuestra[i].Fecha;
                    oSheetLinea.Cells[row, 03].Value2 = listaMuestra[i].Hora.ToString(@"hh\:mm");
                    oSheetLinea.Cells[row, 04].Value2 = listaMuestra[i].Blend;
                    oSheetLinea.Cells[row, 05].Value2 = listaMuestra[i].OrdenProduccion;
                    oSheetLinea.Cells[row, 06].Value2 = listaMuestra[i].Corrida;
                    oSheetLinea.Cells[row, 07].Value2 = listaMuestra[i].CajaReferente;
                    oSheetLinea.Cells[row, 08].Value2 = listaMuestra[i].PesoMuestra;
                    oSheetLinea.Cells[row, 09].Value2 = listaMuestra[i].Uno;
                    oSheetLinea.Cells[row, 10].Value2 = listaMuestra[i].UnMedio;
                    oSheetLinea.Cells[row, 11].Value2 = listaMuestra[i].TotalUnMedio;
                    oSheetLinea.Cells[row, 12].Value2 = listaMuestra[i].UnCuarto;
                    oSheetLinea.Cells[row, 13].Value2 = listaMuestra[i].UnOctavo;
                    oSheetLinea.Cells[row, 14].Value2 = listaMuestra[i].Pan;
                    oSheetLinea.Cells[row, 15].Value2 = listaMuestra[i].TotalUnCuarto;
                    oSheetLinea.Cells[row, 16].Value2 = listaMuestra[i].PaloObjetable;
                    oSheetLinea.Cells[row, 17].Value2 = listaMuestra[i].PaloTotal;
                    oSheetLinea.Cells[row, 18].Value2 = listaMuestra[i].PesoMuestraGr;
                    oSheetLinea.Cells[row, 19].Value2 = listaMuestra[i].UnoSinteticoGr;
                    oSheetLinea.Cells[row, 20].Value2 = listaMuestra[i].UnoSinteticoCant;
                    oSheetLinea.Cells[row, 21].Value2 = listaMuestra[i].UnoSinteticoPorc;
                    oSheetLinea.Cells[row, 22].Value2 = listaMuestra[i].DosNaturalGr;
                    oSheetLinea.Cells[row, 23].Value2 = listaMuestra[i].DosNaturalCant;
                    oSheetLinea.Cells[row, 24].Value2 = listaMuestra[i].DosNaturalPorc;
                    oSheetLinea.Cells[row, 25].Value2 = listaMuestra[i].TresAnimalGr;
                    oSheetLinea.Cells[row, 26].Value2 = listaMuestra[i].TresAnimalCant;
                    oSheetLinea.Cells[row, 27].Value2 = listaMuestra[i].TresAnimalPorc;
                    oSheetLinea.Cells[row, 28].Value2 = listaMuestra[i].CuatroVegetalGr;
                    oSheetLinea.Cells[row, 29].Value2 = listaMuestra[i].CuatroVegetalCant;
                    oSheetLinea.Cells[row, 30].Value2 = listaMuestra[i].CuatroVegetalPorc;
                    oSheetLinea.Cells[row, 33].Value2 = listaMuestra[i].Suma;
                }

                // Formula de TotalUnMedio
                oSheetLinea.get_Range("K17", "K" + ((17 - 1) + listaMuestra.Count)).Formula = "=I17+J17";

                // Formula de TotalUnCuarto
                oSheetLinea.get_Range("O17", "O" + ((17 - 1) + listaMuestra.Count)).Formula = "=K17+L17";

                // Formula de Suma
                oSheetLinea.get_Range("AG17", "AG" + ((17 - 1) + listaMuestra.Count)).Formula = "=SUM(I17 + J17 + L17 + M17 + N17)";

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

                oWB.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Office.Interop", ex);
            }
        }
    }
}
