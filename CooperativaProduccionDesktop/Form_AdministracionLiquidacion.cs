﻿using System;
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
using System.Linq.Expressions;
using Extensions;
using System.Data.Entity;
using CooperativaProduccion.Helpers.GridRecords;
using CooperativaProduccion.Reports;
using DevExpress.XtraReports.UI;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using CooperativaProduccion.Helpers;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using System.Globalization;
using CooperativaProduccion.ReportModels;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using EntityFramework.Extensions;

namespace CooperativaProduccion
{
    public partial class Form_AdministracionLiquidacion : DevExpress.XtraBars.Ribbon.RibbonForm, IEnlace
    {
        public CooperativaProduccionEntities Context { get; set; }
        private Form_AdministracionBuscarProductor _formBuscarProductor;
        private Guid ProductorId;

        public Form_AdministracionLiquidacion(bool Liquidar, 
            bool LiquidacionSubirAfip,bool LiquidacionImprimir)
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
            Buscar(false);
            CargarCombo();
            Iniciar(Liquidar, LiquidacionSubirAfip, LiquidacionImprimir);
        }

        private void Iniciar(bool Liquidar, bool LiquidacionSubirAfip, bool LiquidacionImprimir)
        {
            btnLiquidar.Visible = Liquidar;
            btnSubirAfip.Visible = LiquidacionSubirAfip;
            btnPrevisualizarLiquidacionManual.Visible = LiquidacionImprimir;
            txtPorcentajeAjuste.Text = "0.00";
        }

        private void CargarCombo()
        {
            var tipotabaco = Context.Vw_TipoTabaco
                .Where(x => x.RUBRO_ID != null)
                .ToList();

            cbTabacoLiquidacion.DataSource = tipotabaco;
            cbTabacoLiquidacion.DisplayMember = "Descripcion";
            cbTabacoLiquidacion.ValueMember = "Id";

            cbTabaco.DataSource = tipotabaco;
            cbTabaco.DisplayMember = "Descripcion";
            cbTabaco.ValueMember = "Id";
            
            cbTabacoAjuste.DataSource = tipotabaco;
            cbTabacoAjuste.DisplayMember = "Descripcion";
            cbTabacoAjuste.ValueMember = "Id";
        }

        #region Modulo de Proceso de Liquidacion

        #region Method Code
       
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar(true);
        }

        private void btnLiquidar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea procesar la liquidación?",
              "Confirmación de Datos", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            Liquidar();
            Buscar(true);
        }

        #endregion
        
        #region Method Dev
   
        private void Buscar(bool buscar)
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
        
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = buscar.Equals(true) ? pred.And(x => x.FechaRomaneo >= dpDesdeRomaneo.Value.Date
                && x.FechaRomaneo <= dpHastaRomaneo.Value.Date) : pred;
            pred = pred.And(x => x.Tabaco == cbTabacoLiquidacion.Text);
            pred = pred.And(x => x.NumInternoLiquidacion == null);

            var result = (
               from a in Context.Vw_Romaneo
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
                   LETRA = a.Letra,
                   KILOS = a.TotalKg,
                   BRUTOSINIVA = a.ImporteBruto,
                   NUMLIQUIDACION = a.NumInternoLiquidacion
               })
               .OrderBy(x => x.NUMROMANEO)
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
            gridViewRomaneo.Columns[7].Caption = "Letra";
            gridViewRomaneo.Columns[7].Width = 40;
            gridViewRomaneo.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].Caption = "Kilos";
            gridViewRomaneo.Columns[8].Width = 40;
            gridViewRomaneo.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].Caption = "Bruto";
            gridViewRomaneo.Columns[9].Width = 60;
            gridViewRomaneo.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[10].Caption = "Nro. Int. Liquidación";
            gridViewRomaneo.Columns[10].Width = 60;
            gridViewRomaneo.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewRomaneo.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewRomaneo.RowCount; i++)
            {
                gridViewRomaneo.SelectRow(i);
            }
        }

        private void Liquidar()
        {
            int PuntoVenta = NumeroPuntoVentaLiquidacion();

            if (gridViewRomaneo.SelectedRowsCount > 0)
            {
                for (int i = 0; i <= gridViewRomaneo.RowCount; i++)
                {
                    if (gridViewRomaneo.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewRomaneo.GetRowCellValue(i, "ID").ToString());
                        var Iva = gridViewRomaneo.GetRowCellValue(i, "LETRA").ToString();

                        var romaneo = Context.Pesada.Find(Id);
                        if (romaneo != null)
                        {
                            romaneo.PuntoVentaLiquidacion = NumeroPuntoVentaLiquidacion();
                            romaneo.NumInternoLiquidacion = ContadorNumeroInternoLiquidacion(Iva);
                            var pesada = Context.Pesada
                                .Where(x => x.Id == Id)
                                .FirstOrDefault();
                            romaneo.FechaInternaLiquidacion = pesada.FechaRomaneo;
                            romaneo.condIva = Iva;
                            var vwromaneo = Context.Vw_Romaneo
                                .Where(x => x.PesadaId == Id)
                                .FirstOrDefault();
                            var subtotal = vwromaneo.ImporteBruto;//romaneo.ImporteBruto;
                            var iva = subtotal * (romaneo.IvaPorcentaje / 100);
                            var total = subtotal + iva;

                            if (Iva == DevConstantes.A)
                            {
                                romaneo.ImporteNeto = decimal.Round(subtotal.Value, 2, MidpointRounding.AwayFromZero);
                                romaneo.IvaCalculado = decimal.Round(iva.Value, 2, MidpointRounding.AwayFromZero);
                                romaneo.Total = decimal.Round(total.Value, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                romaneo.ImporteNeto = romaneo.ImporteBruto;
                                romaneo.IvaCalculado = decimal.Round(0, 2, MidpointRounding.AwayFromZero);
                                romaneo.Total = romaneo.ImporteBruto;
                            }

                            Context.Entry(romaneo).State = EntityState.Modified;
                            Context.SaveChanges();

                            #region Actualizar contador

                            if (Iva.Equals(DevConstantes.A))
                            {
                                var contador = Context.Contador
                                    .Where(x => x.Nombre.Equals(DevConstantes.LiquidacionA))
                                    .FirstOrDefault();

                                var count = Context.Contador.Find(contador.Id);
                                if (count != null)
                                {
                                    count.Valor = count.Valor + 1;
                                    Context.Entry(count).State = EntityState.Modified;
                                    Context.SaveChanges();
                                }
                            }
                            else
                            {
                                var contador = Context.Contador
                                    .Where(x => x.Nombre.Equals(DevConstantes.LiquidacionB))
                                    .FirstOrDefault();

                                var count = Context.Contador.Find(contador.Id);
                                if (count != null)
                                {
                                    count.Valor = count.Valor + 1;
                                    Context.Entry(count).State = EntityState.Modified;
                                    Context.SaveChanges();
                                }
                            }

                            #endregion

                            #region nueva liquidacion
                            //Guid Id = new Guid(gridViewRomaneo
                            //    .GetRowCellValue(i, "ID")
                            //    .ToString());

                            //var vwromaneo = Context.Vw_Romaneo
                            //        .Where(x => x.PesadaId == Id)
                            //        .FirstOrDefault();

                            //var productor = Context.Vw_Productor
                            //    .Where(x => x.ID == vwromaneo.ProductorId.Value)
                            //    .FirstOrDefault();

                            //string condIva = productor.IVA == "RI" ? DevConstantes.A : DevConstantes.B;

                            //long NumeroInternoLiquidacion = ContadorNumeroInternoLiquidacion(condIva);

                            //DateTime FechaLiquidacion = DateTime.Parse(gridViewRomaneo
                            //    .GetRowCellValue(i, "FECHA")
                            //    .ToString())
                            //    .Date;

                            //var subtotal = vwromaneo.ImporteBruto;

                            //var romaneo = Context.Pesada
                            //    .Where(x => x.Id == Id)
                            //    .FirstOrDefault();

                            //var iva = subtotal * (romaneo.IvaPorcentaje / 100);

                            //var total = subtotal + iva;

                            //decimal ImporteNeto = productor.IVA == "RI" ?
                            //    decimal.Round(subtotal.Value, 2, MidpointRounding.AwayFromZero) :
                            //    romaneo.ImporteBruto.Value;

                            //decimal IvaCalculado = productor.IVA == "RI" ?
                            //    decimal.Round(iva.Value, 2, MidpointRounding.AwayFromZero) :
                            //    decimal.Round(0, 2, MidpointRounding.AwayFromZero);

                            //decimal Total = productor.IVA == "RI" ?
                            //    decimal.Round(total.Value, 2, MidpointRounding.AwayFromZero) :
                            //    romaneo.ImporteBruto.Value;

                            //var Pesada = Context.Pesada.Where(x => x.Id == Id)
                            //    .Update(x => new Pesada()
                            //    {
                            //        PuntoVentaLiquidacion = PuntoVenta,
                            //        NumInternoLiquidacion = NumeroInternoLiquidacion,
                            //        FechaInternaLiquidacion = FechaLiquidacion,
                            //        condIva = condIva,
                            //        ImporteNeto = ImporteNeto,
                            //        IvaCalculado = IvaCalculado,
                            //        Total = Total
                            //    });

                            //Context.SaveChanges();
                            //ActualizarContador(condIva, NumeroInternoLiquidacion);
                            #endregion
                        }
                    }
                }
            }
        }

        //private void ActualizarContador(string Iva,long Numero)
        //{
        //    string Nombre = Iva == DevConstantes.A ? 
        //        DevConstantes.LiquidacionA : 
        //        DevConstantes.LiquidacionB;

        //    var Contador = Context.Contador.Where(x => x.Nombre == Nombre)
        //    .Update(x => new Contador()
        //    {
        //        Valor = Numero + 1
        //    });

        //    Context.SaveChanges();     
        //}

        private long ContadorNumeroInternoLiquidacion(string Iva)
        {
            if (Iva.Equals(DevConstantes.A))
            {
                CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
                var contador = Context.Contador
                   .Where(x => x.Nombre.Equals(DevConstantes.LiquidacionA))
                   .FirstOrDefault();

                if (contador != null)
                {
                    return (contador.Valor.Value + 1);
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
                var contador = Context.Contador
                   .Where(x => x.Nombre.Equals(DevConstantes.LiquidacionB))
                   .FirstOrDefault();

                if (contador != null)
                {
                    return (contador.Valor.Value + 1);
                }
                else
                {
                    return 1;
                }
            }
        }

        private int NumeroPuntoVentaLiquidacion()
        {
            var contador = Context.Contador
                .Where(x => x.Nombre.Equals(DevConstantes.PuntoVentaLiquidacion))
                .FirstOrDefault();

            if (contador != null)
            {
                return int.Parse(contador.Valor.ToString());
            }
            else
            {
                return 1;
            }
        }

        #endregion

        #endregion

        #region Modulo de Consulta de Liquidacion

        #region Method Code

        private void btnBuscarLiquidacion_Click(object sender, EventArgs e)
        {
            BuscarLiquidacion();
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

        private void btnBuscarFet_Click(object sender, EventArgs e)
        {
            BuscarProductor();
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

        private void btnBuscarProductor_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }
       
        private void Liquidacion_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Limpiar();
        }

        #endregion

        #region Method Dev

        private void BuscarLiquidacion()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
          
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = pred.And(x => x.FechaInternaLiquidacion >= dpDesdeLiquidacion.Value.Date 
                && x.FechaInternaLiquidacion <= dpHastaLiquidacion.Value.Date);

            pred = !string.IsNullOrEmpty(txtFet.Text) ? pred.And(x => x.ProductorId == ProductorId) : pred;

            pred = !string.IsNullOrEmpty(cbTabaco.Text) ? pred.And(x => x.Tabaco == cbTabaco.Text) : pred;

            pred = pred.And(x => x.NumInternoLiquidacion != null);

            List<GridLiquidacion> lista = new List<GridLiquidacion>();

            var liquidaciones =
                (from a in Context.Vw_Romaneo
                 .Where(pred)
                 select new
                 {
                     ID = a.PesadaId,
                     FECHA = a.FechaInternaLiquidacion,
                     NUMINTERNO = a.NumInternoLiquidacion,
                     PRODUCTOR = a.NOMBRE,
                     CUIT = a.CUIT,
                     FET = a.nrofet,
                     PROVINCIA = a.Provincia,
                     LETRA = a.Letra,
                     KILOS = a.TotalKg,
                     BRUTOSINIVA = a.ImporteBruto,
                     TABACO = a.Tabaco,
                     FECHALIQUIDACIONAFIP = a.FechaAfipLiquidacion,
                     NUMEROAFIPLIQUIDACION = a.NumAfipLiquidacion,
                     CAE = a.Cae,
                     FECHAVTOCAE = a.FechaVtoCae
                 })
               .OrderBy(x => x.NUMINTERNO)
               .ToList();

            foreach (var liquidacion in liquidaciones)
            {
                var rowLiquidacion = new GridLiquidacion();
                rowLiquidacion.PesadaId = liquidacion.ID;
                rowLiquidacion.fechaInternaLiquidacion = liquidacion.FECHA;
                rowLiquidacion.numInternoLiquidacion = liquidacion.NUMINTERNO;
                rowLiquidacion.NOMBRE = liquidacion.PRODUCTOR;
                rowLiquidacion.CUIT = liquidacion.CUIT;
                rowLiquidacion.nrofet = liquidacion.FET;
                rowLiquidacion.Provincia = liquidacion.PROVINCIA;
                rowLiquidacion.Letra = liquidacion.LETRA;
                rowLiquidacion.Totalkg = liquidacion.KILOS;
                rowLiquidacion.ImporteBruto = liquidacion.BRUTOSINIVA;
                rowLiquidacion.fechaAfipLiquidacion = liquidacion.FECHALIQUIDACIONAFIP;
                rowLiquidacion.numAfipLiquidacion = liquidacion.NUMEROAFIPLIQUIDACION;
                rowLiquidacion.cae = liquidacion.CAE;
                rowLiquidacion.fechaVtoCae = liquidacion.FECHAVTOCAE;
                lista.Add(rowLiquidacion);
            }

            gridControlLiquidacion.DataSource = new BindingList<GridLiquidacion>(lista);

            gridViewLiquidacion.Columns[0].Visible = false;
            gridViewLiquidacion.Columns[1].Caption = "Fecha Int. Liquidación";
            gridViewLiquidacion.Columns[1].Width = 60;
            gridViewLiquidacion.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[2].Caption = "Nro Int. Liquidación";
            gridViewLiquidacion.Columns[2].Width = 60;
            gridViewLiquidacion.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[3].Caption = "Productor";
            gridViewLiquidacion.Columns[3].Width = 150;
            gridViewLiquidacion.Columns[4].Caption = "CUIT";
            gridViewLiquidacion.Columns[4].Width = 65;
            gridViewLiquidacion.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[5].Caption = "FET";
            gridViewLiquidacion.Columns[5].Width = 55;
            gridViewLiquidacion.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[6].Caption = "Provincia";
            gridViewLiquidacion.Columns[6].Width = 60;
            gridViewLiquidacion.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[6].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[7].Caption = "Letra";
            gridViewLiquidacion.Columns[7].Width = 40;
            gridViewLiquidacion.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[7].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[8].Caption = "Kilos";
            gridViewLiquidacion.Columns[8].Width = 60;
            gridViewLiquidacion.Columns[8].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[8].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[9].Caption = "Bruto";
            gridViewLiquidacion.Columns[9].Width = 90;
            gridViewLiquidacion.Columns[9].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewLiquidacion.Columns[9].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[10].Caption = "Fecha Liquidación";
            gridViewLiquidacion.Columns[10].Width = 60;
            gridViewLiquidacion.Columns[10].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[10].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[11].Caption = "Nro Liquidación";
            gridViewLiquidacion.Columns[11].Width = 60;
            gridViewLiquidacion.Columns[11].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[11].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[12].Caption = "CAE";
            gridViewLiquidacion.Columns[12].Width = 60;
            gridViewLiquidacion.Columns[12].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[12].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[13].Caption = "Fecha Vto CAE";
            gridViewLiquidacion.Columns[13].Width = 60;
            gridViewLiquidacion.Columns[13].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacion.Columns[13].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            
            for (var i = 0; i <= gridViewLiquidacion.RowCount; i++)
            {
                gridViewLiquidacion.SelectRow(i);
            }

            foreach (GridColumn column in gridViewLiquidacion.Columns)
            {
                GridSummaryItem item = column.SummaryItem;
                if (item != null)
                    column.Summary.Remove(item);
            }

            gridViewLiquidacion.Columns["Totalkg"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Totalkg", "{0}");
            gridViewLiquidacion.Columns["ImporteBruto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ImporteBruto", "{0}");
            gridViewLiquidacion.Appearance.FooterPanel.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewLiquidacion.Appearance.FooterPanel.Options.UseTextOptions = true;
        }

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
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
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
                        txtFet.Text = busqueda.FET;
                        txtProductor.Text = busqueda.PRODUCTOR;
                        txtProvincia.Text = busqueda.PROVINCIA;
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
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
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
                        txtFet.Text = busqueda.FET;
                        txtProductor.Text = busqueda.PRODUCTOR;
                        txtProvincia.Text = busqueda.PROVINCIA;
                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtFetAjuste.Text))
            {
                var count = result
                    .Where(r => r.FET.Equals(txtFetAjuste.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.fet = txtFetAjuste.Text;
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
                    _formBuscarProductor.BuscarFet();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                        .Where(x => x.FET.Equals(txtFetAjuste.Text))
                        .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFetAjuste.Text = busqueda.FET;
                        txtProductorAjuste.Text = busqueda.PRODUCTOR;
                        txtProvinciaAjuste.Text = busqueda.PROVINCIA;
                    }
                    else
                    {
                        MessageBox.Show("N° de Fet no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtProductorAjuste.Text))
            {
                var count = result
                    .Where(r => r.PRODUCTOR.Contains(txtProductorAjuste.Text))
                    .Count();

                if (count > 1)
                {
                    _formBuscarProductor = new Form_AdministracionBuscarProductor();
                    _formBuscarProductor.nombre = txtProductorAjuste.Text;
                    _formBuscarProductor.target = DevConstantes.Liquidacion;
                    _formBuscarProductor.BuscarNombre();
                    _formBuscarProductor.ShowDialog(this);
                }
                else
                {
                    var busqueda = result
                      .Where(x => x.PRODUCTOR.Contains(txtProductorAjuste.Text))
                      .FirstOrDefault();
                    if (busqueda != null)
                    {
                        ProductorId = busqueda.ID.Value;
                        txtFetAjuste.Text = busqueda.FET;
                        txtProductorAjuste.Text = busqueda.PRODUCTOR;
                        txtProvinciaAjuste.Text = busqueda.PROVINCIA;
                    }
                    else
                    {
                        MessageBox.Show("Nombre no válido.",
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void Enviar(Guid Id, string fet, string nombre)
        {
            if (Liquidacion.SelectedTabPage.Equals(TabConsultaLiquidacion))
            {
                ProductorId = Id;
                txtFet.Text = fet;
                txtProductor.Text = nombre;
                var empleado = Context.Vw_Productor
                    .Where(x => x.ID == ProductorId)
                    .FirstOrDefault();
                txtCuit.Text = empleado.CUIT;
                txtProvincia.Text = empleado.Provincia;
            }
            else if (Liquidacion.SelectedTabPage.Equals(TabProcesoLiquidacionReajuste))
            {
                ProductorId = Id;
                txtFetAjuste.Text = fet;
                txtProductorAjuste.Text = nombre;
                var empleado = Context.Vw_Productor
                    .Where(x => x.ID == ProductorId)
                    .FirstOrDefault();
                txtCuitAjuste.Text = empleado.CUIT;
                txtProvinciaAjuste.Text = empleado.Provincia;
            }
        }
        
        private void Limpiar()
        {
            gridControlLiquidacion.DataSource = null;
            gridControlRomaneo.DataSource = null;
        }

        private void SubirAfip()
        {
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewLiquidacion.GetRowCellValue(i, "PesadaId").ToString());
                        var pesada = Context.Pesada.Find(Id);
                        if (pesada != null)
                        {
                            if (pesada.Cae == null)
                            {
                                pesada.FechaAfipLiquidacion = DateTime.Now.Date;
                                pesada.FechaVtoCae = DateTime.Now.Date.AddDays(10);
                                pesada.Cae = RandomDigits(14).ToString();
                                pesada.NumAfipLiquidacion = ContadorNumeroLiquidacionAfip();
                                Context.Entry(pesada).State = EntityState.Modified;
                                Context.SaveChanges();

                            }
                        }
                    }
                }
            }
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        private string ContadorNumeroLiquidacionAfip()
        {
            var count = Context.Pesada.Count();
            if (count != 0)
            {

                var codigo = Context.Pesada
                    .Max(x => x.NumInternoLiquidacion);
                if (codigo != null)
                {
                    
                    string number = "0003-" + (Int16.Parse(codigo.ToString()) + 1).ToString().PadLeft(8, '0');
                    return number;

                }
                else
                {
                    return "0003-" + 1.ToString().PadLeft(8, '0');
                }
            }
            else
            {
                return "0003-" + 1.ToString().PadLeft(8, '0');
            }
        }

        private void ImprimirLiquidacion(Guid Id)
        {
            var reporte = new LiquidacionManualReport();
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();
            var liquidacion = Context.Vw_Romaneo
                .Where(x => x.PesadaId == Id)
                .FirstOrDefault();

            #region Tipo Factura

            if (liquidacion.Letra != null)
            {
                //reporte.lblLetra.Text = liquidacion.Letra == DevConstantes.A ? 
                //    DevConstantes.A : DevConstantes.B;
                //reporte.lblCodigo.Text = liquidacion.Letra == DevConstantes.A ? 
                //    DevConstantes.Codigo001 : DevConstantes.Codigo006;
            }

            #endregion

            #region Parametros Cabecera Factura

            reporte.Parameters["nroComprobante"].Value = liquidacion.NumAfipLiquidacion == null ? 
                string.Empty : liquidacion.NumAfipLiquidacion;
            reporte.Parameters["fechaEmision"].Value = liquidacion.FechaAfipLiquidacion == null ? 
                string.Empty : liquidacion.FechaAfipLiquidacion.Value.ToShortDateString();
            reporte.Parameters["cuitEmpresa"].Value = DevConstantes.CuitEmpresa;
            reporte.Parameters["iibb"].Value = DevConstantes.IIBB;
            reporte.Parameters["ines"].Value = DevConstantes.Ines;
            reporte.Parameters["inicioActividades"].Value = DevConstantes.InicioActividades;
            reporte.Parameters["fechaEmision"].Value = liquidacion.FechaInternaLiquidacion == null ? 
                string.Empty : liquidacion.FechaInternaLiquidacion.Value.ToShortDateString();
           
            #endregion

            #region Parametros Datos Productor

            reporte.Parameters["productor"].Value = liquidacion.NOMBRE;

            var productor = Context.Vw_Productor
                .Where(x => x.ID == liquidacion.ProductorId)
                .FirstOrDefault();

            reporte.Parameters["domicilio"].Value = productor.DOMICILIO == null ? 
                string.Empty : productor.DOMICILIO;
            reporte.Parameters["provincia"].Value = productor.Provincia == null ? 
                string.Empty : productor.Provincia;
         
            if (productor.IVA == DevConstantes.MTS)
            {
                reporte.Parameters["iva"].Value = DevConstantes.MonotributoSocial;
            }
            else if (productor.IVA == DevConstantes.MT)
            {
                reporte.Parameters["iva"].Value = DevConstantes.Monotributo;
            }
            else if (productor.IVA == DevConstantes.RI)
            {
                reporte.Parameters["iva"].Value = DevConstantes.ResponsableInscripto;
            }
            else if (productor.IVA == DevConstantes.TP)
            {
                reporte.Parameters["iva"].Value = DevConstantes.TrabajadorPromovido;
            }
            reporte.Parameters["fet"].Value = productor.nrofet == null ? 
                string.Empty : productor.nrofet;
            reporte.Parameters["localidad"].Value = productor.CALLE == null ? 
                string.Empty : productor.CALLE;
            reporte.Parameters["cuitProductor"].Value = productor.CUIT == null ? 
                string.Empty : productor.CUIT;

            #endregion

            #region Parametros Romaneo

            var pesadaDetalle = Context.PesadaDetalle
                .Where(x => x.PesadaId == liquidacion.PesadaId)
                .FirstOrDefault();
            if (pesadaDetalle != null)
            {
                var clase = Context.Vw_Clase
                    .Where(x => x.ID == pesadaDetalle.ClaseId)
                    .FirstOrDefault();
                if (clase != null)
                {
                    reporte.Parameters["numRomaneo"].Value = clase.DESCRIPCION 
                        + " - ROMANEO N°:" + liquidacion.NumRomaneo;
                }
            }
            #endregion

            #region SubReport Detalle Liquidacion

            List<GridLiquidacionDetalle> datasourceDetalle;
            datasourceDetalle = GenerarReporteLiquidacionDetalle(liquidacion.PesadaId);
            reporte.SubreportDetalleLiquidacionA.ReportSource.DataSource = datasourceDetalle;

            #endregion

            var liq = Context.Pesada
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            #region Total en Letras

            if (liquidacion.ImporteBruto != null)
            {
                reporte.Parameters["totalLetra"].Value = Numalet.ToCardinal(liq.Total.ToString()).ToUpper();
            }

            #endregion

            #region Parametros Totales

            reporte.Parameters["totalKilos"].Value = CalcularTotalKilos(Id);
            reporte.Parameters["totalFardos"].Value = CalcularTotalFardo(Id);

            if (liquidacion.ImporteBruto != null)
            {
                if (liquidacion.Letra == DevConstantes.A)
                {
                    reporte.Parameters["subtotal"].Value = decimal.Round(
                        decimal.Parse(liq.ImporteNeto.ToString()), 2, MidpointRounding.AwayFromZero);
                    reporte.Parameters["iva21"].Value = decimal.Round(
                        decimal.Parse(liq.IvaCalculado.ToString()), 2, MidpointRounding.AwayFromZero);
                    reporte.Parameters["total"].Value = decimal.Round(
                        decimal.Parse(liq.Total.ToString()), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    reporte.lblSubTotal.Visible = false;
                    reporte.lblSubTotalValor.Visible = false;
                    reporte.lblIva.Visible = false;
                    reporte.lblIvaValor.Visible = false;
                    reporte.Parameters["total"].Value = decimal.Round(
                        decimal.Parse(liq.Total.ToString()), 2, MidpointRounding.AwayFromZero);
                }
            }

            #endregion

            #region Parametros Cae

            if (liquidacion.Cae != null)
            {
                reporte.Parameters["cae"].Value = liquidacion.Cae;
                reporte.Parameters["fechaVtoCae"].Value = liquidacion.FechaVtoCae.Value.ToShortDateString();
            }

            #endregion

            using (ReportPrintTool tool = new ReportPrintTool(reporte))
            {
                reporte.ShowPreviewMarginLines = false;
                tool.PreviewForm.Text = "Etiqueta";
                tool.ShowPreviewDialog();
            }

        }

        public List<GridLiquidacionDetalle> GenerarReporteLiquidacionDetalle(Guid PesadaId)
        {
            List<GridLiquidacionDetalle> datasource = new List<GridLiquidacionDetalle>();

            var liquidacionDetalles = (
                from a in Context.Vw_ResumenRomaneoPorClase
                    .Where(x => x.PesadaId == PesadaId)
                select new
                {
                    Clase = a.Clase,
                    Fardos = a.Fardos,
                    Kilos = a.Kilos,
                    PrecioClase = a.ClasePrecio,
                    Total = a.Total
                })
                .ToList();

            foreach (var liquidacionDetalle in liquidacionDetalles)
            {
                GridLiquidacionDetalle detalle = new GridLiquidacionDetalle();
                detalle.Clase = liquidacionDetalle.Clase;
                detalle.Fardos = liquidacionDetalle.Fardos;
                detalle.Kilos = liquidacionDetalle.Kilos;
                var liquidacion = Context.Vw_Romaneo
                    .Where(x => x.PesadaId == PesadaId)
                    .FirstOrDefault();
                var liq = Context.Pesada
                    .Where(x => x.Id == PesadaId)
                    .FirstOrDefault();

                if (liquidacion.Letra != null)
                {
                    if (liquidacion.Letra == DevConstantes.A)
                    {
                        //var precioSinIva = liquidacionDetalle.PrecioClase.Value / (1 + (liq.IvaPorcentaje / 100));
                        var precioBruto = liquidacionDetalle.PrecioClase.Value;
                        detalle.ClasePrecio = decimal.Round(precioBruto, 2, MidpointRounding.AwayFromZero);
                        decimal total = decimal.Parse(liquidacionDetalle.Kilos.ToString()) * precioBruto;
                        detalle.Total = decimal.Round(total, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        detalle.ClasePrecio = decimal.Round(decimal.Parse(liquidacionDetalle.PrecioClase.ToString()), 2,
                            MidpointRounding.AwayFromZero);
                        detalle.Total = decimal.Round(decimal.Parse(liquidacionDetalle.Total.ToString()), 2,
                            MidpointRounding.AwayFromZero);
                    }
                }
                datasource.Add(detalle);
            }
            return datasource;
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

        #endregion

        #endregion
   
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
      
        private void btnSubirAfip_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea subir estos registros a afip?",
               "Confirmación de Datos", MessageBoxButtons.OKCancel);

            if (resultado != DialogResult.OK)
            {
                return;
            }
            SubirAfip();
            BuscarLiquidacion();
        }

        private void ResumenLiquidacion_Click(object sender, EventArgs e)
        {
            var filtro = new Form_RomaneoFiltroResumen(DevConstantes.Liquidacion);
            filtro.Show();
        }

        private void btnPrevisualizarLiquidacionManual_Click(object sender, EventArgs e)
        {
            if (gridViewLiquidacion.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridViewLiquidacion.DataRowCount; i++)
                {
                    if (gridViewLiquidacion.IsRowSelected(i))
                    {
                        var Id = new Guid(gridViewLiquidacion.GetRowCellValue(i, "PesadaId").ToString());
                        ImprimirLiquidacion(Id);
                    }
                }
            }
        }

        private void btnBuscarFetAjuste_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void btnBuscarProductorAjuste_Click(object sender, EventArgs e)
        {
            BuscarProductor();
        }

        private void txtFetAjuste_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtFetAjuste.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtProductorAjuste.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtProductorAjuste.Text = string.Empty;
                txtCuitAjuste.Text = string.Empty;
                txtProvinciaAjuste.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void txtProductorAjuste_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(txtProductorAjuste.Text))
                {
                    BuscarProductor();
                }
                else
                {
                    txtFetAjuste.Focus();
                }
            }
            if (e.KeyChar == 8)
            {
                txtFetAjuste.Text = string.Empty;
                txtCuitAjuste.Text = string.Empty;
                txtProvinciaAjuste.Text = string.Empty;
                ProductorId = Guid.Empty;
            }
        }

        private void btnBuscarAjuste_Click(object sender, EventArgs e)
        {
            BuscarLiquidacionAjuste();
        }

        private void BuscarLiquidacionAjuste()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            Expression<Func<Vw_Romaneo, bool>> pred = x => true;

            pred = pred.And(x => x.FechaInternaLiquidacion >= dpDesdeAjuste.Value.Date
                && x.FechaInternaLiquidacion <= dpHastaAjuste.Value.Date);

            pred = !string.IsNullOrEmpty(txtFetAjuste.Text) ? pred.And(x => x.ProductorId == ProductorId) : pred;

            pred = !string.IsNullOrEmpty(cbTabacoAjuste.Text) ? pred.And(x => x.Tabaco == cbTabacoAjuste.Text) : pred;

            pred = pred.And(x => x.NumInternoLiquidacion != null);

            List<GridLiquidacion> lista = new List<GridLiquidacion>();

            var liquidaciones =
                (from a in Context.Vw_Romaneo
                 .Where(pred)
                 group a by new
                 {
                     a.NOMBRE,
                     a.CUIT,
                     a.nrofet,
                     a.Provincia,
                     a.Tabaco
                 } into gl
                 select new
                 {
                     PRODUCTOR = gl.Key.NOMBRE,
                     CUIT = gl.Key.CUIT,
                     FET = gl.Key.nrofet,
                     PROVINCIA = gl.Key.Provincia,
                     BRUTOSINIVA = gl.Sum(a => a.ImporteBruto),
                     TABACO = gl.Key.Tabaco
                 })
               .OrderBy(x => x.PRODUCTOR)
               .ToList();

            //foreach (var liquidacion in liquidaciones)
            //{
            //    var rowLiquidacion = new GridLiquidacion();
            //    rowLiquidacion.PesadaId = liquidacion.ID;
            //    rowLiquidacion.fechaInternaLiquidacion = liquidacion.FECHA;
            //    rowLiquidacion.numInternoLiquidacion = liquidacion.NUMINTERNO;
            //    rowLiquidacion.NOMBRE = liquidacion.PRODUCTOR;
            //    rowLiquidacion.CUIT = liquidacion.CUIT;
            //    rowLiquidacion.nrofet = liquidacion.FET;
            //    rowLiquidacion.Provincia = liquidacion.PROVINCIA;
            //    rowLiquidacion.Letra = liquidacion.LETRA;
            //    rowLiquidacion.Totalkg = liquidacion.KILOS;
            //    rowLiquidacion.ImporteBruto = liquidacion.BRUTOSINIVA;
            //    rowLiquidacion.fechaAfipLiquidacion = liquidacion.FECHALIQUIDACIONAFIP;
            //    rowLiquidacion.numAfipLiquidacion = liquidacion.NUMEROAFIPLIQUIDACION;
            //    rowLiquidacion.cae = liquidacion.CAE;
            //    rowLiquidacion.fechaVtoCae = liquidacion.FECHAVTOCAE;
            //    lista.Add(rowLiquidacion);
            //}

            gridControlLiquidacionAjuste.DataSource = liquidaciones; //new BindingList<GridLiquidacion>(lista);

            gridViewLiquidacionAjuste.Columns[0].Caption = "Productor";
            gridViewLiquidacionAjuste.Columns[0].Width = 150;
            gridViewLiquidacionAjuste.Columns[1].Caption = "CUIT";
            gridViewLiquidacionAjuste.Columns[1].Width = 65;
            gridViewLiquidacionAjuste.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[2].Caption = "FET";
            gridViewLiquidacionAjuste.Columns[2].Width = 55;
            gridViewLiquidacionAjuste.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[3].Caption = "Provincia";
            gridViewLiquidacionAjuste.Columns[3].Width = 60;
            gridViewLiquidacionAjuste.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[3].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[4].Caption = "Bruto";
            gridViewLiquidacionAjuste.Columns[4].Width = 90;
            gridViewLiquidacionAjuste.Columns[4].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewLiquidacionAjuste.Columns[4].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridViewLiquidacionAjuste.Columns[5].Caption = "Tabaco";
            gridViewLiquidacionAjuste.Columns[5].Width = 90;
            gridViewLiquidacionAjuste.Columns[5].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewLiquidacionAjuste.Columns[5].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            for (var i = 0; i <= gridViewLiquidacionAjuste.RowCount; i++)
            {
                gridViewLiquidacionAjuste.SelectRow(i);
            }

            foreach (GridColumn column in gridViewLiquidacionAjuste.Columns)
            {
                GridSummaryItem item = column.SummaryItem;
                if (item != null)
                    column.Summary.Remove(item);
            }

             gridViewLiquidacionAjuste.Columns["BRUTOSINIVA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BRUTOSINIVA", "{0}");
            gridViewLiquidacionAjuste.Appearance.FooterPanel.TextOptions.HAlignment = HorzAlignment.Far;
            gridViewLiquidacionAjuste.Appearance.FooterPanel.Options.UseTextOptions = true;
        }

        private void txtPorcentajeAjuste_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtPorcentajeAjuste.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                txtPorcentajeAjuste.Focus();
            }
        }
    }
}