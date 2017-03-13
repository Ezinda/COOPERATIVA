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
using System.Linq.Expressions;
using Extensions;
using DevExpress.Utils;
using CooperativaProduccion.ReportModels;
using System.Data.SqlClient;
using System.Globalization;
using CooperativaProduccion.Reports;
using DevExpress.XtraReports.UI;
using CooperativaProduccion.Helpers.GridRecords;
using System.IO;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace CooperativaProduccion
{
    public partial class Form_RomaneoGestionRomaneo : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;

        public Form_RomaneoGestionRomaneo(bool ReimpresionRomaneo, bool ResumenRomaneo,
            bool ResumenCompra, bool ResumenClaseMes, bool ResumenClaseTrimestre)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            CargarCombo();
            Iniciar(ReimpresionRomaneo, ResumenRomaneo, ResumenCompra,
                ResumenClaseMes, ResumenClaseTrimestre);
        }

        private void Iniciar(bool ReimpresionRomaneo, bool ResumenRomaneo,
            bool ResumenCompra, bool ResumenClaseMes, bool ResumenClaseTrimestre)
        {
            btnReimpresionRomaneo.Visibility = ReimpresionRomaneo.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenRomaneo.Visibility = ResumenRomaneo.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenCompra.Visibility = ResumenCompra.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenClasesMes.Visibility = ResumenClaseMes.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
            btnResumenClasesTrimestre.Visibility = ResumenClaseTrimestre.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
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

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarRomaneo_Click(object sender, EventArgs e)
        {
            BuscarRomaneo();
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
                    _formBuscarProductor.target = DevConstantes.Romaneo;
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
                    _formBuscarProductor.nombre = txtProductor.Text;
                    _formBuscarProductor.target = DevConstantes.Romaneo;
                    _formBuscarProductor.BuscarNombre();
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
       
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date &&
                x.FechaRomaneo <= dpHastaRomaneo.Value.Date);

            pred = !string.IsNullOrEmpty(txtFet.Text) ? pred.And(x => x.ProductorId == ProductorId) : pred;

            pred = !string.IsNullOrEmpty(cbTabaco.Text) ? pred.And(x => x.Tabaco == cbTabaco.Text) : pred;

            var result =
                (from a in Context.Vw_Romaneo
                     .Where(pred)
                 select new
                 {
                     ID = a.PesadaId,
                     FECHA = a.FechaRomaneo,
                     NUMROMANEO = a.NumRomaneo,
                     PRODUCTOR = a.NOMBRE,
                     CUIT = a.CUIT,
                     FET = a.nrofet,
                     PROVINCIA = a.Provincia,
                     KILOS = a.TotalKg,
                     BRUTOSINIVA = a.ImporteBruto,
                     TABACO = a.Tabaco
                 })
                .OrderBy(x=>x.NUMROMANEO)
                .ToList();

            gridControlRomaneo.DataSource = result;
            gridViewRomaneo.Columns[0].Visible = false;
            gridViewRomaneo.Columns[1].Caption = "Fecha Romaneo";
            gridViewRomaneo.Columns[1].Width = 60;
            gridViewRomaneo.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].Caption = "Número Romaneo";
            gridViewRomaneo.Columns[2].Width = 60;
            gridViewRomaneo.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[3].Caption = "Productor";
            gridViewRomaneo.Columns[3].Width = 150;
            gridViewRomaneo.Columns[4].Caption = "CUIT";
            gridViewRomaneo.Columns[4].Width = 75;
            gridViewRomaneo.Columns[5].Caption = "FET";
            gridViewRomaneo.Columns[5].Width = 55;
            gridViewRomaneo.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[6].Caption = "Provincia";
            gridViewRomaneo.Columns[6].Width = 60;
            gridViewRomaneo.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].Caption = "Kilos";
            gridViewRomaneo.Columns[7].Width = 70;
            gridViewRomaneo.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].Caption = "Bruto sin IVA";
            gridViewRomaneo.Columns[8].Width = 60;
            gridViewRomaneo.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].Caption = "Tabaco";
            gridViewRomaneo.Columns[9].Width = 80;
            gridViewRomaneo.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewRomaneo.RowCount; i++)
            {
                gridViewRomaneo.SelectRow(i);
            }
            gridViewRomaneo.Columns["KILOS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "KILOS", "TOTAL KILOS ={0}");

        }

        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch
            {
                throw;
            }
        }

        private void btnReimpresionRomaneo_Click(object sender, EventArgs e)
        {
            if (gridViewRomaneo.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewRomaneo.DataRowCount; i++)
                {
                    if (gridViewRomaneo.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewRomaneo.GetRowCellValue(i, "ID").ToString());
                        ImpimirRomaneo(Id);
                    }
                }
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

                var total = Context.Pesada
                    .Where(x => x.Id == PesadaId)
                    .FirstOrDefault();
                if (total != null)
                {
                    reporte.Parameters["totalfardo"].Value = total.TotalFardo;
                    reporte.Parameters["totalKilos"].Value = total.TotalKg;
                    reporte.Parameters["ImporteBruto"].Value = total.ImporteBruto;
                }
                reporte.Parameters["Reimpresion"].Value = string.Empty;//DevConstantes.Reimpresion;
                #endregion

                using (ReportPrintTool tool = new ReportPrintTool(reporte))
                {
                    reporte.ShowPreviewMarginLines = false;
                    tool.PreviewForm.Text = "Etiqueta";
                    if (ValidarDebug().Equals(true))
                    {
                       tool.ShowPreviewDialog();
                    }
                    else
                    {
                        tool.ShowPreviewDialog();
                    }
                }
            }
        }

        public List<RegistroFardo> GenerarReporteFardo(Guid PesadaId)
        {
            List<RegistroFardo> datasource = new List<RegistroFardo>();

            var fardos = Context.Vw_Pesada
                .Where(x => x.PesadaId == PesadaId)
                .OrderBy(x => x.NumFardo)
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

        private bool ValidarDebug()
        {
            var debug = Context.Configuracion
              .Where(x => x.Nombre == DevConstantes.Debug)
              .FirstOrDefault();

            return debug.Valor;
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

        private void btnReimpresionRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRomaneo.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewRomaneo.DataRowCount; i++)
                {
                    if (gridViewRomaneo.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewRomaneo.GetRowCellValue(i, "ID").ToString());
                        ImpimirRomaneo(Id);
                    }
                }
            }
        }

        private void btnResumenRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {
            var filtro = new Form_RomaneoFiltroResumenCompra(DevConstantes.ResumenRomaneo);
            filtro.Show();
        }

        private void btnResumenCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            var filtro = new Form_RomaneoFiltroResumenCompra(DevConstantes.ResumenCompra);
            filtro.Show();
        }

        private void btnResumenClasesMes_ItemClick(object sender, ItemClickEventArgs e)
        {
            var filtro = new Form_RomaneoFiltroResumenCompra(DevConstantes.ResumenClasesMes);
            filtro.Show();
        }

        private void btnResumenClasesTrimestre_ItemClick(object sender, ItemClickEventArgs e)
        {
            var filtro = new Form_RomaneoFiltroResumenCompra(DevConstantes.ResumenClasesTrimestre);
            filtro.Show();
        }

        private void btnExportarRomaneo_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarRomaneo();
        }

        private void ExportarRomaneo()
        {
            string rootdir = DevConstantes.RootDocumentsDirectory;

            CreateIfMissing(rootdir);

            var variedad = cbTabaco.Text;
            var desde = dpDesdeRomaneo.Value.Date;
            var hasta = dpHastaRomaneo.Value.Date;
            var dir = String.Empty;

            if (variedad == DevConstantes.TabacoVirginia)
            {
                dir = Path.Combine(rootdir, "ExportacionRomaneoVirginia");
            }
            else if (variedad == DevConstantes.TabacoBurley)
            {
                dir = Path.Combine(rootdir, "ExportacionRomaneoBurley");
            }

            CreateIfMissing(dir);

            var datasourceEncabezados = GetEncabezados(variedad, desde, hasta);
            var datasourceRenglones = GetRenglones(datasourceEncabezados);

            var timespan = DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            var archivor = Path.Combine(dir, timespan + "R.txt");
            var archivoe = Path.Combine(dir, timespan + "E.txt");

            GenerarArchivoDeEncabezados(datasourceEncabezados, archivoe);
            GenerarArchivoDeRenglones(datasourceRenglones, archivor);

            StartProcess(archivoe);
            StartProcess(archivor);
        }

        private List<RegistroEncabezado> GetEncabezados(string variedad, DateTime desde, DateTime hasta)
        {
            var list = new List<RegistroEncabezado>();

            var datosRomaneos = Context.Vw_Romaneo
                .Where(x =>
                    x.FechaRomaneo >= desde && x.FechaRomaneo <= hasta &&
                    x.Tabaco == variedad &&
                    x.NumInternoLiquidacion != null)
                .Select(x => new
                {
                    x.PesadaId,
                    x.ProductorId,

                    FechaRomaneo = x.FechaRomaneo.Value,
                    NumRomaneo = x.NumRomaneo.Value,
                    x.Tabaco,
                    PuntoVentaLiquidacion = x.PuntoVentaLiquidacion.Value,
                    NumInternoLiquidacion = x.NumInternoLiquidacion.Value,
                    TipoComprobante = x.Letra,
                    FechaInternaLiquidacion = x.FechaInternaLiquidacion.Value,
                    ImporteBruto = x.ImporteBruto.Value
                })
                .OrderBy(x => x.FechaRomaneo)
                .ToList();

            var ids = datosRomaneos.Select(x => x.ProductorId).ToList();

            var datosProductor = Context.Vw_Productor
                .Where(x => ids.Contains(x.ID))
                .Select(x => new
                {
                    x.ID,
                    x.CUIT,
                    x.NOMBRE,
                    x.CALLE,
                    x.Provincia
                })
                .ToList();

            foreach (var item in datosRomaneos)
            {
                var productor = datosProductor.Where(x => x.ID == item.ProductorId).Single();
                var codigoProvincia = String.Empty;
                var variedadTabaco = String.Empty;
                var tipocomprobante = String.Empty;

                var provincia = productor.Provincia ?? String.Empty;

                switch (provincia.Trim())
                {
                    case "Tucumán":
                        codigoProvincia = DevConstantes.AFIPCodigoTucuman;
                        break;
                    case "Catamarca":
                        codigoProvincia = DevConstantes.AFIPCodigoCatamarca;
                        break;
                    default:
                        codigoProvincia = DevConstantes.AFIPCodigoTucuman;
                        break;
                }

                var tabaco = item.Tabaco ?? String.Empty;

                switch (tabaco.Trim())
                {
                    case "TABACO BURLEY":
                        variedadTabaco = DevConstantes.AFIPCodigoVariedadBurley;
                        break;
                    case "TABACO VIRGINIA":
                        variedadTabaco = DevConstantes.AFIPCodigoVariedadVirginia;
                        break;
                    default:
                        throw new Exception("No se puede determinar el código de la variedad de tabaco encontrada.");
                }

                var letra = item.TipoComprobante ?? String.Empty;

                switch (letra.Trim())
                {
                    case "A":
                        tipocomprobante = DevConstantes.AFIPCodigoTipoComprobanteA;
                        break;
                    case "B":
                        tipocomprobante = DevConstantes.AFIPCodigoTipoComprobanteB;
                        break;
                    default:
                        throw new Exception("No se puede determinar el código del tipo de comprobante encontrado.");
                }

                var registro = new RegistroEncabezado()
                {
                    _ID = item.PesadaId,

                    CuitProductor = productor.CUIT,
                    RazonSocialProductor = productor.NOMBRE,
                    CalleProductor = productor.CALLE,
                    CodigoProvinciaProductor = codigoProvincia,

                    FechaRomaneo = item.FechaRomaneo,
                    NumeroRomaneo = item.NumRomaneo,
                    VariedadTabaco = variedadTabaco,

                    PuntoDeVentaFacturaLiquidacion = item.PuntoVentaLiquidacion,
                    // Temporal hasta que esté listo factura electronica
                    NumeroFacturaLiquidacion = item.NumInternoLiquidacion,
                    CodigoTipoComprobante = tipocomprobante,
                    // Temporal hasta que esté listo factura electronica
                    FechaFacturaLiquidacionDI = item.FechaInternaLiquidacion,

                    ImporteNetoGravado = item.ImporteBruto
                };

                list.Add(registro);
            }

            return list;
        }

        private List<RegistroRenglon> GetRenglones(List<RegistroEncabezado> encabezados)
        {
            var list = new List<RegistroRenglon>();
            var idsRomaneos = encabezados.Select(x => x._ID).ToList();

            var datosDetalle = Context.PesadaDetalle
                .Where(x => idsRomaneos.Contains(x.PesadaId.Value))
                .Select(x => new
                {
                    PesadaId = x.PesadaId.Value,

                    ClaseId = x.ClaseId.Value,
                    Kilos = x.Kilos.Value,
                    NumFardo = x.NumFardo.Value
                })
                .OrderBy(x => x.PesadaId)
                .ToList();

            var datosClases = Context.Vw_Clase
                .Select(x => new
                {
                    x.ID,
                    x.NOMBRE
                });

            foreach (var romaneoid in idsRomaneos)
            {
                var numeroromaneo = encabezados.Where(x => x._ID == romaneoid).Select(x => x.NumeroRomaneo).Single();
                var detalle = datosDetalle.Where(x => x.PesadaId == romaneoid).ToList();

                foreach (var item in detalle)
                {
                    var clase = datosClases.Where(x => x.ID == item.ClaseId).Select(x => x.NOMBRE).Single();

                    var registro = new RegistroRenglon()
                    {
                        NumeroRomaneo = numeroromaneo,
                        Clase = clase,
                        PesoFardoEnKilos = Convert.ToDecimal(item.Kilos),
                        CodigoTrazabilidadInterno = item.NumFardo,
                    };

                    list.Add(registro);
                }
            }

            return list;
        }

        private void GenerarArchivoDeEncabezados(List<RegistroEncabezado> datasource, string archivoe)
        {
            var parser = new Helpers.ParserIngresoTabaco();
            var lista = new List<Helpers.EncabezadoIngresoTabaco>();

            foreach (var item in datasource)
            {
                var encabezado = new Helpers.EncabezadoIngresoTabaco();

                encabezado.CodigoDepositoAcopiador.Value = "1";
                encabezado.CuitAdquirienteTabaco.Value = "33708194609";
                encabezado.RazonSocialAdquirientetabaco.Value = "COOP. DE PROD. AGROP. DEL TUC.";
                encabezado.CuitProductor.Value = item.CuitProductor;
                encabezado.RazonSocialProductor.Value = item.RazonSocialProductor;
                encabezado.Calle.Value = item.CalleProductor ?? "LA COCHA";
                encabezado.NumeroPuerta.Value = "0000";
                encabezado.Piso.Value = String.Empty;
                encabezado.OficinaDptoLocal.Value = String.Empty;
                encabezado.Sector.Value = String.Empty;
                encabezado.Torre.Value = String.Empty;
                encabezado.Manzana.Value = String.Empty;
                encabezado.CodigoPostal.Value = "4000";
                encabezado.Localidad.Value = item.CalleProductor ?? "LA COCHA";
                encabezado.CodigoDeProvincia.Value = item.CodigoProvinciaProductor;
                encabezado.CodigoDeProvinciaTabaco.Value = item.CodigoProvinciaProductor;
                encabezado.LocalidadTabaco.Value = item.CalleProductor ?? "LA COCHA";
                encabezado.FechaRomaneo.Value = encabezado.FechaRomaneo.Formatter.GetFormattedValue(item.FechaRomaneo);
                encabezado.NumeroRomaneo.Value = item.NumeroRomaneo.ToString();
                encabezado.VariedadTabaco.Value = item.VariedadTabaco;
                encabezado.PuntoDeVentaFacturaLiquidacion.Value = item.PuntoDeVentaFacturaLiquidacion.ToString();
                encabezado.NumeroFacturaLiquidacion.Value = item.NumeroFacturaLiquidacion.ToString();
                encabezado.TipoComprobante.Value = item.CodigoTipoComprobante;
                encabezado.NumeroDespachoImportacion.Value = String.Empty;
                encabezado.FechaFacturaLiquidacionDI.Value = encabezado.FechaFacturaLiquidacionDI.Formatter.GetFormattedValue(item.FechaFacturaLiquidacionDI);
                encabezado.EmisorComprobante.Value = "2";
                encabezado.ImporteNetoGravado.Value = encabezado.ImporteNetoGravado.Formatter.GetFormattedValue(item.ImporteNetoGravado);
                encabezado.CAI.Value = "0";
                encabezado.TipoOperacion.Value = "1";

                lista.Add(encabezado);
            }

            parser.ImprimirArchivoEncabezados(lista, archivoe);
        }

        private void GenerarArchivoDeRenglones(List<RegistroRenglon> datasource, string archivor)
        {
            var parser = new Helpers.ParserIngresoTabaco();
            var lista = new List<Helpers.RenglonIngresoTabaco>();

            foreach (var item in datasource)
            {
                var renglon = new Helpers.RenglonIngresoTabaco();

                renglon.NumeroRomaneo.Value = item.NumeroRomaneo.ToString();
                renglon.Clase.Value = item.Clase;
                renglon.PesoFardoEnKilos.Value = renglon.PesoFardoEnKilos.Formatter.GetFormattedValue(item.PesoFardoEnKilos);
                renglon.CodigoTrazabilidadInterno.Value = item.CodigoTrazabilidadInterno.ToString();

                lista.Add(renglon);
            }

            parser.ImprimirArchivoRenglones(lista, archivor);
        }

        void IEnlace.Enviar(Guid Id, string fet, string nombre)
        {
            ProductorId = Id;
            txtFet.Text = fet;
            txtProductor.Text = nombre;
            var empleado = Context.Vw_Productor
                .Where(x => x.ID == ProductorId)
                .FirstOrDefault();
            txtCuit.Text = string.IsNullOrEmpty(empleado.CUIT) ?
                string.Empty : empleado.CUIT;
            txtProvincia.Text = string.IsNullOrEmpty(empleado.Provincia) ?
                string.Empty : empleado.Provincia.ToString();
        }
    }
}