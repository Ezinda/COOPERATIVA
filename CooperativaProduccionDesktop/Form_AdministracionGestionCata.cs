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

namespace CooperativaProduccion
{
    public partial class Form_AdministracionGestionCata : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
        private string target;
        private int numerolote;
        private long numerocata;

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
        
        private void checkTodos_CheckedChanged(object sender, EventArgs e)
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
        
        private void btnExportarVinculacion_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea exportar el archivo de vinculación?",
                       "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            CrearTxtVinculacion();
        }

        private void Cata_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Context = new CooperativaProduccionEntities();
            CargarCombo();
        }

        #endregion

        #region Method Dev

        private void CargarCombo()
        {
            var caja = (from c in Context.Cata
                            .Where(x => x.NumCaja == null)
                         group c by c.Lote into g
                         select new { Lote = g.Key })
                       .OrderBy(x=>x.Lote)
                       .ToList();
            cbLote.DataSource = caja;
            cbLote.DisplayMember = "lote";
            cbLote.ValueMember = "lote";
        }

        private void BuscarCata()
        {
            var lote = Int64.Parse(cbLote.SelectedValue.ToString());
            var result = (
                from a in Context.Cata.Where(x=>x.Lote == lote)
                select new
                {
                    ID = a.Id,
                    Lote = a.Lote,
                    Cata = a.NumCata,
                    NumOrden = a.NumOrden,
                    NumCaja = a.NumCaja
                })
                .ToList();

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

            txtTotal.Text = CalcularTotalCata(lote);
            txtUtilizados.Text = CalcularTotalCataUtilizadas(lote);
            txtDisponibles.Text = CalcularTotalCataDisponibles(lote);

        }

        private string CalcularTotalCata(Int64 Lote)
        {
            string totalcata;
            int cero = 0;
            var count = Context.Cata
                .Where(x => x.Lote == Lote)
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

        private string CalcularTotalCataDisponibles(Int64 Lote)
        {
            string totalcata;
            int cero = 0;
            var count = Context.Cata
                .Where(x => x.Lote == Lote && x.NumCaja == null)
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

        private string CalcularTotalCataUtilizadas(Int64 Lote)
        {
            string totalcata;
            int cero = 0;
            var count = Context.Cata
                .Where(x =>x.Lote == Lote && x.NumCaja != null)
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
                    var lote = long.Parse(cbLote.SelectedValue.ToString());
                    var catas = Context.Cata
                        .Where(x=>x.Lote == lote && x.NumCaja != null)
                        .ToList();
                    foreach (var cata in catas)
                    {
                        var caja = Context.Caja
                            .Where(x=>x.CataId == cata.Id)
                            .FirstOrDefault();
                        sw.WriteLine("2;" + cata.NumCata + ";2;1;" + caja.Bruto + ";"
                            + cata.NumOrden.ToString().PadLeft(8, '0')
                            + cata.NumCaja.ToString().PadLeft(19, '0'));
                    }
                }

                // Write file contents on console. 
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
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

        #endregion

       

        #endregion

  
    }
}