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
        private string totalfardo;
        private string totalkilo;
        private string importebruto;
        private Form_RomaneoPesadaMostrador _pesadaMostrador;
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Form_RomaneoBuscarPreingreso _formBuscarPreingreso;

        public Form_RomaneoPesada()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            _pesadaMostrador = new Form_RomaneoPesadaMostrador();
            Iniciar();
        }

        #region Method Code

        private void txtFet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }

            if (e.KeyChar == 8)
            {
                txtNombre.Text = string.Empty;
                txtPreingreso.Text = string.Empty;
                txtCuit.Text = string.Empty;
                txtProvincia.Text = string.Empty;
                PasarMostrador(txtNombre.Text, txtCuit.Text);
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
            GrabarPesada();
        }

        private void btnCancelarPesada_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea cancelar la pesada?",
                "Confirmación", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            CancelarPesada();
        }

        private void CancelarPesada()
        {
            var contador = Context.Contador
                .Where(x=>x.Nombre == DevConstantes.PuntoVenta)
                .FirstOrDefault();

            var pesada = Context.Pesada
                .Where(x => x.NumPesada == contador.Valor 
                    && x.ProductorId == ProductorId)
                .FirstOrDefault();
            
            if (pesada != null)
            {
                var romaneo = Context.Pesada.Find(pesada.Id);
                Context.Entry(romaneo).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            if (contador != null)
            {
                var conta = Context.Contador.Find(contador.Id);
                conta.Valor = conta.Valor - 1;
                Context.Entry(conta).State = EntityState.Modified;
                Context.SaveChanges();
            }

            Deshabilitar();
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
                MessageBox.Show("No hay un valor de kg.", 
                    "Se Requiere", MessageBoxButtons.OK);
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
            _pesadaMostrador = new Form_RomaneoPesadaMostrador();
            _pesadaMostrador.nombre = txtNombre.Text;
            _pesadaMostrador.cuit = txtCuit.Text;
            _pesadaMostrador.CargarDatos();
            _pesadaMostrador.Show();
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            ImprimirEtiqueta();
        }
        
        #endregion

        #region Method Dev

        private void Iniciar()
        {
            checkBalanzaAutomatica.Checked = true;
            Deshabilitar();
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
            if (!string.IsNullOrEmpty(txtFet.Text))
            {
                //var result = Context.Vw_Preingreso
                //   .Where(x => x.Estado == true
                //       && x.FET.Contains(txtFet.Text))
                //   .OrderBy(x => x.Fecha)
                //   .ThenBy(x => x.Hora);

                //if (result.Any().Equals(true))
                //{
                //    if (result.Count() > 1)
                //    {
                //        _formBuscarPreingreso = new Form_RomaneoBuscarPreingreso();
                //        _formBuscarPreingreso.fet = txtFet.Text;
                //        _formBuscarPreingreso.target = DevConstantes.Pesada;
                //        _formBuscarPreingreso.BuscarFet();
                //        _formBuscarPreingreso.ShowDialog(this);
                //    }
                //    else if(result.Count() == 1)
                //    {
                //        var preingreso = result
                //            .Where(r => r.FET.Contains(txtFet.Text))
                //            .FirstOrDefault();

                //        if (preingreso != null)
                //        {
                //            ProductorId = preingreso.ProductorId.Value;
                //            txtFet.Text = preingreso.FET.ToString();
                //            txtNombre.Text = preingreso.Nombre;
                //            txtCuit.Text = preingreso.Cuit;
                //            txtProvincia.Text = preingreso.Provincia;
                //            txtPreingreso.Text = preingreso.NumeroPreingreso.ToString();
                //            GrabarPesada();
                //            PasarMostrador(txtNombre.Text, txtCuit.Text);
                //        }
                //    }
                //}
                //else
                //{
                    BuscarProductor();
                //}
            }
        }
  
        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ? 
                string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ? 
                string.Empty : empleado.Provincia.ToString();
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

        private float CalcularPrecioPromedio(Guid PesadaId)
        {
            float totalpromedio = 0;
            float precioPromedio = 0;
            var pesadas = Context.PesadaDetalle
                .Where(x => x.PesadaId == PesadaId);
            if (pesadas != null)
            {
                foreach (var pesada in pesadas)
                {
                    precioPromedio = precioPromedio + float.Parse(pesada.PrecioClase.Value.ToString());
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
                CalcularTotales();
                Habilitar();
                this.btnIniciarPesada.Enabled = false;
                this.txtClase.Focus();
            }
        }

        private void GrabarPesadaDetalle()
        {
            if (ValidarCamposPesadaDetalle())
            {
                GuardarDatosPesadaDetalle();
            }
        }

        private long ContadorNumeroPesada()
        {
            var count = Context.Pesada.Count();
            if (count != 0)
            {
                var codigo = Context.Pesada
                    .Max(x => x.NumPesada)
                    .ToString();
                return (long.Parse(codigo) + 1);
            }
            else
            {
                return 1;
            }
        }

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
            if (ProductorId == null && PesadaId == null && 
                txtClase.Text == string.Empty && txtKilos.Text == string.Empty)
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
                //int numPreingreso = Int32.Parse(txtPreingreso.Text);
                //var preingreso = Context.Preingreso
                //    .Where(x => x.NumeroPreingreso == numPreingreso)
                //    .FirstOrDefault();
                //pesada.PreingresoId = preingreso.Id;
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
                #region Pesada Detalle

                PesadaDetalle pesadaDetalle;
                pesadaDetalle = new PesadaDetalle();
                pesadaDetalle.Id = Guid.NewGuid();
                pesadaDetalle.PesadaId = PesadaId;
                pesadaDetalle.ContadorFardo = ContadorNumeroFardo(PesadaId);
                pesadaDetalle.NumFardo = NumeradorFardo();
                var clase = Context.Vw_Clase
                    .Where(x => x.Vigente == true 
                        && x.NOMBRE == txtClase.Text)
                    .FirstOrDefault();
                pesadaDetalle.ClaseId = clase.ID;
                pesadaDetalle.Kilos = float.Parse(Math.Round(decimal.Parse(txtKilos.Text), 0).ToString());
                pesadaDetalle.PrecioClase = clase.PRECIOCOMPRA;
                Context.PesadaDetalle.Add(pesadaDetalle);
                Context.SaveChanges();

                #endregion

                #region Movimiento

                Movimiento movimiento;
                movimiento = new Movimiento();
                movimiento.Id = Guid.NewGuid();
                movimiento.Fecha = DateTime.Now.Date;
                movimiento.TransaccionId = pesadaDetalle.Id;
                movimiento.Unidad = DevConstantes.Kg;
                movimiento.Ingreso = pesadaDetalle.Kilos;
                Context.Movimiento.Add(movimiento);
                Context.SaveChanges();

                #endregion
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
            txtFet.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPreingreso.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtProvincia.Text = string.Empty;
            txtKilos.Text = string.Empty;
            txtTotalKilo.Text = string.Empty;
            txtTotalFardo.Text = string.Empty;
            txtImporteBruto.Text = string.Empty;
            txtPrecioPromedio.Text = string.Empty;
            gridControlPesada.DataSource = null;
        }

        private void PasarMostrador(string Productor, string Cuit)
        {
            _pesadaMostrador.nombre = Productor;
            _pesadaMostrador.cuit = Cuit;
            _pesadaMostrador.CargarDatos();
        }

        private void PasarFardoMostrador()
        {
            _pesadaMostrador.numFardo = gridViewPesada.GetRowCellValue(0, "CONTADOR_CAJA").ToString();
            _pesadaMostrador.clase = gridViewPesada.GetRowCellValue(0, "CLASE").ToString();
            _pesadaMostrador.totalkg = txtTotalKilo.Text;
            _pesadaMostrador.CargarFardo();
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

        private void BuscarProductor()
        {
            var result =
                (from a in Context.Vw_Productor
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
                    _formBuscarProductor.target = DevConstantes.Pesada;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else if (count == 1)
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();

                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFet.Text = busqueda.FET.ToString();
                        txtNombre.Text = busqueda.PRODUCTOR.ToString();
                        txtCuit.Text = string.IsNullOrEmpty(busqueda.CUIT) ?
                            string.Empty : busqueda.CUIT;
                        txtProvincia.Text = string.IsNullOrEmpty(busqueda.PROVINCIA) ?
                            string.Empty : busqueda.PROVINCIA;
                        PasarMostrador(txtNombre.Text, txtCuit.Text);
                    }
                }
                else
                {
                    MessageBox.Show("N° de Fet no válido.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BuscarProductorPreingreso()
        {
            var result = (
                from a in Context.Vw_Preingreso
                select new
                {
                    ID = a.PreIngresoId,
                    FET = a.FET,
                    PRODUCTOR = a.Nombre,
                    CUIT = a.Cuit,
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
                    _formBuscarProductor.target = DevConstantes.Pesada;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else if (count == 1)
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFet.Text))
                        .FirstOrDefault();

                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID;
                        txtFet.Text = busqueda.FET.ToString();
                        txtNombre.Text = busqueda.PRODUCTOR.ToString();
                        txtProvincia.Text = busqueda.PROVINCIA.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("N° de Fet no válido.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        void Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtNombre.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ?
                           string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ?
                string.Empty : empleado.Provincia;
            PasarMostrador(txtNombre.Text, txtCuit.Text);
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {

        }

        private void Deshabilitar()
        {
            ProductorId = Guid.Empty;
            txtFet.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPreingreso.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtProvincia.Text = string.Empty;
            txtKilos.Text = string.Empty;
            txtTotalKilo.Text = string.Empty;
            txtTotalFardo.Text = string.Empty;
            txtImporteBruto.Text = string.Empty;
            txtPrecioPromedio.Text = string.Empty;
            gridControlPesada.DataSource = null;
            PasarMostrador(txtNombre.Text, txtCuit.Text);
            txtClase.Enabled = false;
            checkBalanzaAutomatica.Enabled = false;
            txtKilos.Enabled = false;
            btnAgregarCaja.Enabled = false;
            gridControlPesada.Enabled = false;
            btnReimprimir.Enabled = false;
            btnEliminar.Enabled = false;
            btnFinalizar.Enabled = false;

        }

        private void Habilitar()
        {
            txtClase.Enabled = true;
            checkBalanzaAutomatica.Enabled = true;
            txtKilos.Enabled = true;
            btnAgregarCaja.Enabled = true;
            gridControlPesada.Enabled = true;
            btnReimprimir.Enabled = true;
            btnEliminar.Enabled = true;
            btnFinalizar.Enabled = true;
        }

        private void txtClase_TextChanged(object sender, EventArgs e)
        {

        }
    }
}