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
using DevExpress.XtraGrid;
using System.Data.Entity;
using CooperativaProduccion.Helpers;
using CooperativaProduccion.ViewModels;
namespace CooperativaProduccion
{
    public partial class Form_AdministracionOrdenPago : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form_AdministracionOrdenPago()
        {
            InitializeComponent();

            this.administracionGeneracionOPControl.Iniciar();
            this.administracionGeneracionOPControl.BuscarLiquidaciones();
        }
    }
}