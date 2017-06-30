using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO;
using DesktopEntities.Models;
using System.Linq.Expressions;
using Extensions;
using System.Globalization;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionGestionCata : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private string target;
        private int numerolote;
        private long numerocata;
        private List<CataCaja> ListCataCaja = new List<CataCaja>();

        public Form_AdministracionGestionCata()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        #region Importacion Cata

        #region Method Code

        private void btnImportarCata_Click(object sender, EventArgs e)
        {
            opd.Multiselect = true;
            opd.Filter = "txt files (*.txt)|*.txt";
            if (opd.ShowDialog() == DialogResult.OK)
            {
                var resultado = MessageBox.Show("¿Desea importar el archivo txt de Cata?",
                    "Importar Archivo", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                backgroundWorker1.RunWorkerAsync(opd.FileNames);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportarCata(opd.FileNames);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressCata.Value = e.ProgressPercentage;

            if (target == "cerrar")
            {
                this.Close();
            }
            else
            {
                txtDescripcion.AppendText(e.UserState.ToString());
                txtDescripcion.AppendText(Environment.NewLine);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        
        private void btnCancelarImportacion_Click(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerSupportsCancellation = true;

            if (backgroundWorker1.IsBusy)
            {
                //  this.backgroundWorker1.CancelAsync();
                backgroundWorker1.CancelAsync();
                this.target = "cerrar";
                this.Enabled = false;
                return;
            }
            else
            {
                this.backgroundWorker1.CancelAsync();
                backgroundWorker1.CancelAsync();
                this.target = "cerrar";
                this.Close();
            }
        }

        private void Form_AdministracionGestionCata_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        #endregion

        #region Method Dev

        private void ImportarCata(string[] Files)
        {
            foreach (var s in Files)
            {
                int counter = 1;
                string line;
                int cero = 0;
                progressCata.Maximum = File.ReadAllLines(s).Length;
                FileStream stream = new FileStream(s, FileMode.Open);
                txtDescripcion.Text = "";
                progressCata.Value = 0;
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(stream);
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Substring(0, 1).Contains("1"))
                        {
                            numerolote = int.TryParse(line.Substring(17, 6), out numerolote) ?
                                int.Parse(line.Substring(17, 6)) : cero;
                        }
                        if (numerolote != cero)
                        {
                            if (line.Contains("2;"))
                            {
                                numerocata = Int64.Parse(line.Substring(2, 14));
                                if (numerocata != cero)
                                {
                                    var catas = Context.Cata
                                        .Where(x => x.Lote == numerolote
                                            && x.NumCata == numerocata)
                                        .Any();
                                    if (catas.Equals(false))
                                    {
                                        try
                                        {
                                            Cata cata;
                                            cata = new Cata();
                                            cata.Id = Guid.NewGuid();
                                            cata.Lote = numerolote;
                                            cata.NumCata = numerocata;
                                            Context.Cata.Add(cata);
                                            Context.SaveChanges();
                                        }
                                        catch
                                        {
                                            throw;
                                        }
                                    }
                                }
                            }
                        }

                        if (numerocata != cero)
                        {
                            backgroundWorker1.ReportProgress(counter, "Nro Lote: " + numerolote + " - Nro CATA: " + numerocata);
                        }
                        counter++;
                    }
                    file.Close();
                    stream.Close();
                    MessageBox.Show("El archivo de afip se subió con exito.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        
        #endregion
              
        #endregion

        #region Consulta Cata

        #region Method Code
        
        private void btnBuscarCata_Click(object sender, EventArgs e)
        {
            BuscarCata();
        }
              
        private void btnExportarVinculacion_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea exportar el archivo de vinculación?",
                       "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }

            Task.Factory.StartNew(() => CrearTxtVinculacion());

        }

        private void Cata_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            gridControlCata.DataSource = null;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string path = @"C:\SystemDocumentsCooperativa";

            CreateIfMissing(path);

            path = @"C:\SystemDocumentsCooperativa\ExcelCata";

            CreateIfMissing(path);

            var Hora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
              CultureInfo.InvariantCulture).Replace(":", "").Replace(".", "")
              .Replace("-", "").Replace(" ", "");

            string fileName = @"C:\SystemDocumentsCooperativa\ExcelCata\" + Hora + " - ExcelCata.xls";

            gridControlCata.ExportToXls(fileName);
            StartProcess(fileName);
        }

        #endregion

        #region Method Dev

        private void CargarCombo()
        {
            var caja = (from c in Context.Cata
                        group c by c.Lote into g
                        select new { Lote = g.Key })
                       .OrderBy(x => x.Lote)
                       .ToList();

            cbLote.DataSource = caja;
            cbLote.DisplayMember = "lote";
            cbLote.ValueMember = "lote";
        }

        private void BuscarCata()
        {
            Expression<Func<Cata, bool>> pred = x => true;

            var lote = Int64.Parse(cbLote.SelectedValue.ToString());

            pred = !(checkTodos.Checked) ? 
                pred.And(x => x.Lote == lote) : pred;

            var result = 
                (from a in Context.Cata.Where(pred)
                join p in Context.Vw_Producto
                    on a.Caja.ProductoId equals p.ID into cp
                from joined in cp.DefaultIfEmpty()
                select new CataCaja
                {
                    Id = a.Id,
                    Lote = a.Lote.Value,
                    Cata = a.NumCata,
                    NumOrden = a.NumOrden ?? 0,
                    NumCaja = a.NumCaja ?? 0,
                    Producto = joined.DESCRIPCION,
                    Campaña = (int?)a.Caja.Campaña ?? 0,
                    Neto = (decimal?)a.Caja.Neto ?? 0,
                    Tara = (decimal?)a.Caja.Tara ?? 0,
                    Bruto = (decimal?)a.Caja.Bruto ?? 0
                })
                .OrderBy(x => x.Lote)
                .ThenBy(x => x.Cata)
                .ToList();

            ListCataCaja = result.ToList<CataCaja>();
            gridControlCata.DataSource = result;
            gridViewCata.Columns[0].Visible = false;
            gridViewCata.Columns[1].Caption = "Lote";
            gridViewCata.Columns[1].Width = 90;
            gridViewCata.Columns[2].Caption = "CATA";
            gridViewCata.Columns[2].Width = 150;
            gridViewCata.Columns[3].Caption = "N° Orden";
            gridViewCata.Columns[3].Width = 90;
            gridViewCata.Columns[4].Caption = "N° Caja";
            gridViewCata.Columns[4].Width = 90;
            
            txtTotal.Text = CalcularTotalCata();
            txtUtilizados.Text = CalcularTotalCataUtilizadas();
            txtDisponibles.Text = CalcularTotalCataDisponibles();

        }

        private string CalcularTotalCata()
        {
            string totalcata;
            int cero = 0;
            var lote = Int64.Parse(cbLote.SelectedValue.ToString());
            Expression<Func<Cata, bool>> pred = x => true;
            
            pred = !(checkTodos.Checked) ? pred.And(x => x.Lote == lote) : pred;

            var count = Context.Cata
                .Where(pred)
                .Count();

            if (count != 0)
            {
                totalcata = count.ToString();
            }
            else
            {
                totalcata = cero.ToString();
            }
            return totalcata;
        }

        private string CalcularTotalCataDisponibles()
        {
            string totalcata;
            int cero = 0;
            var lote = Int64.Parse(cbLote.SelectedValue.ToString());

            Expression<Func<Cata, bool>> pred = x => true;

            pred = !(checkTodos.Checked) ? pred.And(x => x.Lote == lote) : pred;

            pred = pred.And(x => x.NumCaja == null);

            var count = Context.Cata
                .Where(pred)
                .Count();
            
            if (count != 0)
            {
                totalcata = count.ToString();
            }
            else
            {
                totalcata = cero.ToString();
            }
            return totalcata;
        }

        private string CalcularTotalCataUtilizadas()
        {
            string totalcata;
            int cero = 0;
            var lote = Int64.Parse(cbLote.SelectedValue.ToString());
            Expression<Func<Cata, bool>> pred = x => true;

            pred = !(checkTodos.Checked) ? pred.And(x => x.Lote == lote) : pred;

            pred = pred.And(x => x.NumCaja != null);

            var count = Context.Cata
                .Where(pred)
                .Count();
            if (count != 0)
            {
                totalcata = count.ToString();
            }
            else
            {
                totalcata = cero.ToString();
            }
            return totalcata;
        }

        private void CrearTxtVinculacion()
        {
            string path = @"C:\SystemDocumentsCooperativa";
            CreateIfMissing(path);
            path = @"C:\SystemDocumentsCooperativa\TxtVinculacion";
            CreateIfMissing(path);
            string fileName = @"C:\SystemDocumentsCooperativa\TxtVinculacion\VINCULACION_" + cbLote.Text + ".txt";
            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file 
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string Cuit = DevConstantes.CuitEmpresa.Replace("-","");
                    sw.WriteLine("1;"+Cuit+";46;"+cbLote.Text);

                    foreach (var cata in ListCataCaja.Where(x=>x.NumCaja!=0))
                    {
                        sw.WriteLine("2;" + cata.Cata + ";2;1;" + cata.Bruto + ";"
                            + cata.NumOrden.ToString().PadLeft(8, '0')
                            + cata.NumCaja.ToString().PadLeft(19, '0'));
                    }
                }

                MessageBox.Show("Archivo de vinculación creado.",
                   "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
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

        #endregion   

        #endregion
 
    }

    internal class CataCaja
    {
        public Guid Id { get; set; }
        public long Lote { get; set; }
        public long? Cata { get; set; }
        public long? NumOrden { get; set; }
        public long NumCaja { get; set; }
        public string Producto { get; set; }
        public int Campaña { get; set; }
        public decimal Neto { get; set; }
        public decimal Tara { get; set; }
        public decimal Bruto { get; set; }
    }
}