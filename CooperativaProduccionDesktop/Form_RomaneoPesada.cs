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
using DevExpress.XtraEditors;
using System.Data.Entity;
using CooperativaProduccion.Reports;
using System.Globalization;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.ReportModels;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Utils;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoPesada : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Guid ProductorId;
        private Guid PesadaId;
        Form_RomaneoPesadaMostrador pesadaMostrador;
        private string totalfardo;
        private string totalkilo;
        private string importebruto;
        
        public Form_RomaneoPesada()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            pesadaMostrador = new Form_RomaneoPesadaMostrador();
            Iniciar();
        }

        #region Method Code

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }
        }
      
        private void checkBalanzaAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBalanzaAutomatica.Checked == false)
            {
                txtKilos.Enabled = true;
                txtKilos.Text = "";
            }
            else
            {
                txtKilos.Enabled = false;
                txtKilos.Text = "";
            }
        }

        private void btnIniciarPesada_Click(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        private void btnCancelarPesada_Click(object sender, EventArgs e)
        {
            txtKilos.Text = "";
        }

        private void btnAgregarCaja_Click(object sender, EventArgs e)
        {
            if (txtKilos.Text != string.Empty)
            {
                GrabarPesadaDetalle();
                CargarGrilla();
                CalcularTotales();
                PasarFardoMostrador();
            }
            else
            {
                MessageBox.Show("No hay un valor de kg.", "Se Requiere", MessageBoxButtons.OK);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea eliminar la pesada seleccionada?",
               "Eliminar Datos", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            EliminarDatos();
            CargarGrilla();
            CalcularTotales();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea finalizar la pesada?",
                   "Atención", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            totalfardo = txtTotalFardo.Text;
            totalkilo = txtTotalKilo.Text;
            importebruto = txtImporteBruto.Text;
            ActualizarPesada(PesadaId);
            Limpiar();
            ImpimirRomaneo(PesadaId);

        }

        private void btnPesadaMostrador_ItemClick(object sender, ItemClickEventArgs e)
        {
            pesadaMostrador = new Form_RomaneoPesadaMostrador();
            pesadaMostrador.nombre = txtNombre.Text;
            pesadaMostrador.cuit = txtCuit.Text;
            pesadaMostrador.CargarDatos();
            pesadaMostrador.Show();
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            ImprimirEtiqueta();
        }
        
        #endregion

        #region Method Dev

        private void Iniciar()
        {
            cbOpcionCompra.SelectedIndex = 0;
            cbBoca.SelectedIndex = 0;
            CargarCombo();
            checkBalanzaAutomatica.Checked = true;
        }

        private void CalcularTotales()
        {
            txtKilos.Text = GetRandomNumber(1, 100).ToString("n2");
            txtTotalFardo.Text = CalcularTotalFardo(PesadaId).ToString();
            txtTotalKilo.Text = CalcularTotalKilos(PesadaId).ToString();
            txtImporteBruto.Text = CalcularTotalImporteBruto(PesadaId).ToString();
            txtPrecioPromedio.Text = CalcularPrecioPromedio(PesadaId).ToString();
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
                    .Where(r => r.FET.Contains(txtFet.Text))
                    .FirstOrDefault();
                if(preingreso != null)
                {
                    ProductorId = preingreso.ProductorId.Value;
                    txtFet.Text = preingreso.FET.ToString();
                    txtNombre.Text = preingreso.Nombre;
                    txtCuit.Text = preingreso.Cuit;
                    txtProvincia.Text = preingreso.Provincia;
                    txtPreingreso.Text = preingreso.NumeroPreingreso.ToString();
                    GrabarPesada();
                    PasarMostrador(txtNombre.Text,txtCuit.Text);
                }
            }
        }
  
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtPreingreso.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = empleado.CUIT;
            txtProvincia.Text = empleado.Provincia;
        }

        private void CargarCombo()
        {
            var clase = Context.Vw_Clase.ToList();
            cbClase.DataSource = clase;
            cbClase.DisplayMember = "NOMBRE";
            cbClase.ValueMember = "ID";      
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
     
        private float CalcularTotalImporteBruto(Guid PesadaId)
        {
            float totalKilos = 0;
            var pesadas = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId);
            foreach (var pesada in pesadas)
            {
                totalKilos = totalKilos + Convert.ToSingle(pesada.Subtotal);
            }

            return totalKilos;
        }

        private int CalcularTotalFardo(Guid PesadaId)
        {
            int totalFardo = 0;
            var count = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId)
                .Count();
            if (count != 0)
            {
                totalFardo = count;
            }
            else
            {
                totalFardo = 0;
            }
            return totalFardo;
        }

        private float CalcularTotalKilos(Guid PesadaId)
        {
            float totalKilos = 0;
            var pesadas = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId).ToList();
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    totalKilos = totalKilos + Convert.ToSingle(pesada.Kilos.Value);
                }
            }
            else
            {
                totalKilos = 0;
            }

            return totalKilos;
        }

        private decimal CalcularPrecioPromedio(Guid PesadaId)
        {
            decimal totalpromedio = 0;
            decimal precioPromedio = 0;
            var pesadas = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId);
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    precioPromedio = precioPromedio + pesada.PrecioClase.Value;
                    var count = Context.PesadaDetalle
                              .Where(x => x.PesadaId == PesadaId)
                              .Count();
                    totalpromedio = precioPromedio / count;
                }
            }
            else
            {
                precioPromedio = 0;
            }           
            
            return totalpromedio;
        }

        private void GrabarPesada()
        {
            if (ValidarCamposPesada())
            {
                var resultado = MessageBox.Show("¿Desea iniciar una nueva pesada?",
                    "Crear Preingreso", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GuardarDatosPesada();

            }
        }

        private void GrabarPesadaDetalle()
        {
            if (ValidarCamposPesadaDetalle())
            {
                GuardarDatosPesadaDetalle();
            }
        }

        private int ContadorNumeroPesada()
        {
            var count = Context.Pesada.Count();
            if (count != 0)
            {
                var codigo = Context.Pesada
                    .Max(x => x.NumPesada)
                    .ToString();
                return (Int16.Parse(codigo) + 1);
            }
            else
            {
                return 1;
            }
        }

        //private int ContadorNumeroRomaneo()
        //{
        //    var count = Context.Pesada.Count();
        //    if (count != 0)
        //    {
        //        var codigo = Context.Pesada
        //            .Max(x => x.NumRomaneo)
        //            .ToString();
        //        return (Int16.Parse(codigo) + 1);
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        private string ContadorNumeroRomaneo()
        {
            var contador = Context.Contador
                .Max(x => x.Valor);

            string number = DevConstantes.PuntoVenta + "-" + (contador.Value + 1).ToString().PadLeft(8, '0');
            return number;
        }

        private bool ValidarCamposPesada()
        {
            if (ProductorId == null)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidarCamposPesadaDetalle()
        {
            if (ProductorId == null && PesadaId == null && cbClase.Text == string.Empty && txtKilos.Text == string.Empty)
            {
                MessageBox.Show("No se ha seleccionado un productor",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void GuardarDatosPesada()
        {
            try
            {
                Pesada pesada;
                pesada = new Pesada();
                pesada.Id = Guid.NewGuid();
                PesadaId = pesada.Id;
                pesada.NumPesada = ContadorNumeroPesada();
                int numPreingreso = Int32.Parse(txtPreingreso.Text);
                var preingreso = Context.Preingreso
                    .Where(x => x.NumeroPreingreso == numPreingreso)
                    .FirstOrDefault();
                pesada.PreingresoId = preingreso.Id;
                pesada.ProductorId = ProductorId;
                pesada.FechaRomaneo = DateTime.Now.Date;
                pesada.NumRomaneo = ContadorNumeroRomaneo();
                pesada.IvaPorcentaje = decimal.Parse(DevConstantes.Iva);
                Context.Pesada.Add(pesada);
                Context.SaveChanges();

                #region Actualizar contador

                var contador = Context.Contador
                    .Where(x=>x.Nombre.Equals(DevConstantes.PuntoVenta))
                    .FirstOrDefault();
                var count = Context.Contador.Find(contador.Id);
                if (count != null)
                {
                    count.Valor = count.Valor + 1;
                    Context.Entry(count).State = EntityState.Modified;
                    Context.SaveChanges();
                }

                #endregion
            }
            catch
            {
                throw;
            }
        }

        private void GuardarDatosPesadaDetalle()
        {
            try
            {
                PesadaDetalle pesadaDetalle;
                pesadaDetalle = new PesadaDetalle();
                pesadaDetalle.Id = Guid.NewGuid();
                pesadaDetalle.PesadaId = PesadaId;
                pesadaDetalle.ContadorFardo = ContadorNumeroFardo(PesadaId);
                pesadaDetalle.NumFardo = NumeradorFardo();
                Guid ClaseId = new Guid(cbClase.SelectedValue.ToString());
                pesadaDetalle.ClaseId = ClaseId;
                pesadaDetalle.Kilos = float.Parse(Math.Round(decimal.Parse(txtKilos.Text), 0).ToString());
                var clase = Context.Vw_Clase
                    .Where(x => x.Vigente == true
                        && x.ID == ClaseId)
                    .FirstOrDefault();
                pesadaDetalle.PrecioClase = clase.PRECIOCOMPRA;

                Context.PesadaDetalle.Add(pesadaDetalle);
                Context.SaveChanges();

                Movimiento movimiento;
                movimiento = new Movimiento();
                movimiento.Id = Guid.NewGuid();
                movimiento.Fecha = DateTime.Now.Date;
                movimiento.TransaccionId = pesadaDetalle.Id;
                movimiento.Unidad = DevConstantes.Kg;
                movimiento.Ingreso = pesadaDetalle.Kilos;

                Context.Movimiento.Add(movimiento);
                Context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private long ContadorNumeroFardo(Guid PesadaId)
        {
            long numFardo = 0;
            var pesadaDetalle = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId)
                .OrderByDescending(x => x.ContadorFardo)
                .FirstOrDefault();
            if (pesadaDetalle != null)
            {
                numFardo = pesadaDetalle.ContadorFardo.Value + 1;
            }
            else
            {
                numFardo = 1;
            }
            return numFardo;
        }

        private long NumeradorFardo()
        {
            long numFardo = 0;
            var pesadaDetalle = Context.PesadaDetalle
                .OrderByDescending(x => x.NumFardo)
                .FirstOrDefault();
            if (pesadaDetalle != null)
            {
                numFardo = pesadaDetalle.NumFardo.Value + 1;
            }
            else
            {
                numFardo = 1;
            }
            return numFardo;
        }

        private void CargarGrilla()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            var result = (
                from a in Context.Vw_Pesada
                    .Where(x => x.PesadaId == PesadaId)
                select new
                {
                    ID = a.PesadaDetalleId,
                    NUMERO_FARDO = a.NumFardo,
                    CONTADOR_CAJA = a.ContadorFardo,
                    CLASE = a.Clase,
                    KILOS = a.Kilos,
                    SUBTOTAL = a.Subtotal
                })
                .OrderByDescending(x => x.CONTADOR_CAJA)
                .ToList();

            if (result.Count > -1)
            {
                gridControlPesada.DataSource = result;
                gridViewPesada.Columns[0].Visible = false;
                gridViewPesada.Columns[1].Caption = "Número Fardo";
                gridViewPesada.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[2].Caption = "Contador Caja";
                gridViewPesada.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[3].Caption = "Clase";
                gridViewPesada.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[4].Caption = "Kilos";
                gridViewPesada.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewPesada.Columns[5].Visible = false;
            }
        }

        private void EliminarDatos()
        {
            Guid Id;
            if (gridViewPesada.SelectedRowsCount > 0)
            {
                Id = new Guid(gridViewPesada
                    .GetRowCellValue(gridViewPesada.FocusedRowHandle, "ID")
                    .ToString());
                var pesadaDetalle = Context.PesadaDetalle.Find(Id);

                if (pesadaDetalle == null)
                {
                    MessageBox.Show("No existe la pesada.",
                        "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Context.Entry(pesadaDetalle).State = EntityState.Deleted;
                    Context.SaveChanges();
                    CargarGrilla();
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun registro.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPesada(Guid PesadaId)
        {
            var pesada = Context.Pesada.Find(PesadaId);
            if (pesada != null)
            {
                pesada.TotalFardo = Int32.Parse(txtTotalFardo.Text);
                pesada.TotalKg = float.Parse(txtTotalKilo.Text);
                pesada.ImporteBruto = decimal.Parse(txtImporteBruto.Text);

                Context.Entry(pesada).State = EntityState.Modified;
                Context.SaveChanges();

                var preingresoDetalle = Context.PreingresoDetalle
                    .Where(x => x.PreingresoId == pesada.PreingresoId
                        && x.ProductorId == ProductorId)
                    .FirstOrDefault();
                var preingreso = Context.PreingresoDetalle.Find(preingresoDetalle.Id);

                if (preingreso != null)
                {
                    preingreso.Estado = false;

                    Context.Entry(preingreso).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
        }

        private void Limpiar()
        {
            txtFet.Text = "";
            txtNombre.Text = "";
            txtPreingreso.Text = "";
            txtCuit.Text = "";
            txtProvincia.Text = "";
            txtKilos.Text = "";
            txtTotalKilo.Text = "";
            txtTotalFardo.Text = "";
            txtImporteBruto.Text = "";
            txtPrecioPromedio.Text = "";
            gridControlPesada.DataSource = null;
        }

        private void PasarMostrador(string Productor, string Cuit)
        {
            pesadaMostrador.nombre = Productor;
            pesadaMostrador.cuit = Cuit;
            pesadaMostrador.CargarDatos();
        }

        private void PasarFardoMostrador()
        {
            pesadaMostrador.numFardo = gridViewPesada.GetRowCellValue(0, "CONTADOR_CAJA").ToString();
            pesadaMostrador.clase = gridViewPesada.GetRowCellValue(0, "CLASE").ToString();
            pesadaMostrador.totalkg = txtTotalKilo.Text;
            pesadaMostrador.CargarFardo();
        }

        private void ImprimirEtiqueta()
        {
            if (gridViewPesada.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewPesada.DataRowCount; i++)
                {
                    if (gridViewPesada.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewPesada.
                           GetRowCellValue(i, "ID")
                           .ToString());
                        var pesadaDetalle = Context.Vw_Pesada
                            .Where(x => x.PesadaDetalleId == Id)
                            .FirstOrDefault();
                        if (pesadaDetalle.PesadaDetalleId != null)
                        {
                            var reporte = new EtiquetaFardoReport();
                            reporte.Parameters["Fardo"].Value = pesadaDetalle.NumFardo;
                            reporte.Parameters["barCodeNumFardo"].Value = pesadaDetalle.NumFardo;
                            reporte.Parameters["Clase"].Value = pesadaDetalle.Clase;
                            reporte.Parameters["Kilos"].Value = pesadaDetalle.Kilos;
                            reporte.Parameters["Fecha"].Value = pesadaDetalle.FechaRomaneo.Value
                                .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                            using (ReportPrintTool tool = new ReportPrintTool(reporte))
                            {
                                reporte.ShowPreviewMarginLines = false;
                                tool.PreviewForm.Text = "Etiqueta";
                                tool.ShowPreviewDialog();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar registro.", "Atención", MessageBoxButtons.OK);
            }
        }

        private void ImpimirRomaneo(Guid PesadaId)
        {
            var pesada = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .FirstOrDefault();
            if (pesada.PesadaId != null)
            {
                var reporte = new RomaneoReport();
                reporte.Parameters["Productor"].Value = pesada.Productor;
                reporte.Parameters["Fet"].Value = pesada.Fet;
                reporte.Parameters["Localidad"].Value = pesada.Localidad;
                reporte.Parameters["Provincia"].Value = pesada.Provincia;
                reporte.Parameters["NumRomaneo"].Value = pesada.NumRomaneo;
                reporte.Parameters["Fecha"].Value = pesada.FechaRomaneo.Value
                    .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                #region Subreport Fardos

                List<RegistroFardo> datasourceFardo;
                datasourceFardo = GenerarReporteFardo(PesadaId);
                reporte.reportPesadaDetalle.ReportSource.DataSource = datasourceFardo;

                #endregion

                #region Subreport Clase

                List<RegistroPesada> datasourcePesada;
                datasourcePesada = GenerarReporteClase(PesadaId);
                reporte.reportDetalleClase.ReportSource.DataSource = datasourcePesada;

                #endregion

                #region Parametros Totales

                reporte.Parameters["totalfardo"].Value = totalfardo;
                reporte.Parameters["totalKilos"].Value = totalkilo;
                reporte.Parameters["ImporteBruto"].Value = importebruto;

                #endregion

                using (ReportPrintTool tool = new ReportPrintTool(reporte))
                {
                    reporte.ShowPreviewMarginLines = false;
                    tool.PreviewForm.Text = "Etiqueta";
                    tool.ShowPreviewDialog();
                }
            }
        }

        public List<RegistroFardo> GenerarReporteFardo(Guid PesadaId)
        {
            List<RegistroFardo> datasource = new List<RegistroFardo>();

            var fardos = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .ToList();


            foreach (var fardo in fardos)
            {
                RegistroFardo registroFardos = new RegistroFardo();
                registroFardos.NumFardo = fardo.NumFardo.ToString();
                registroFardos.Clase = fardo.Clase;
                registroFardos.Kilos = fardo.Kilos.ToString();

                datasource.Add(registroFardos);
            }
            return datasource;
        }

        public List<RegistroPesada> GenerarReporteClase(Guid PesadaId)
        {
            List<RegistroPesada> datasource = new List<RegistroPesada>();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                conn.Open();

                string sql = @"SELECT * FROM dbo.Vw_ResumenPesadaPorClase where PesadaId='" + PesadaId + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    RegistroPesada registroFardos = new RegistroPesada();
                    registroFardos.Clase = row["Clase"].ToString();
                    registroFardos.Kilos = row["Kilos"].ToString();

                    datasource.Add(registroFardos);
                }

                return datasource;
            }
        }

        #endregion

    }
}