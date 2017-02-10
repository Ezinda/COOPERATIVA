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
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_ConfiguracionImpresion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context { get; set; }
       
        public Form_ConfiguracionImpresion()
        {
            InitializeComponent(); 
            Context = new CooperativaProduccionEntities();
            Iniciar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar(checkBalanza.Checked, checkReclasificacion.Checked, 
                checkGestionReclasificacion.Checked, checkDebug.Checked);
        }

        private void Modificar(bool balanza, bool reclasificacion, bool gestionreclasificacion,bool debug)
        {
            var configuracion = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.ImpresiónBalanza)
                .FirstOrDefault();
            var configbalanza = Context.Configuracion.Find(configuracion.Id);
            configbalanza.Valor = balanza;
            Context.Entry(configbalanza).State = EntityState.Modified;
            Context.SaveChanges();

            configuracion = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.ImpresiónReclasificacion)
                .FirstOrDefault();
            var configreclasificacion = Context.Configuracion.Find(configuracion.Id);
            configreclasificacion.Valor = reclasificacion;
            Context.Entry(configreclasificacion).State = EntityState.Modified;
            Context.SaveChanges();

            configuracion = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.ImpresiónGestionReclasificacion)
                .FirstOrDefault();
            var configGestionreclasificacion = Context.Configuracion.Find(configuracion.Id);
            configGestionreclasificacion.Valor = gestionreclasificacion;
            Context.Entry(configGestionreclasificacion).State = EntityState.Modified;
            Context.SaveChanges();
            
            configuracion = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.Debug)
                .FirstOrDefault();
            var configdebug = Context.Configuracion.Find(configuracion.Id);
            configdebug.Valor = debug;
            Context.Entry(configdebug).State = EntityState.Modified;
            Context.SaveChanges();

            MessageBox.Show("Configuración Actualizada","Atención", MessageBoxButtons.OKCancel);

        }

        private void Iniciar()
        {
            CooperativaProduccionEntities Context = new CooperativaProduccionEntities();

            var balanza = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.ImpresiónBalanza)
                .FirstOrDefault();

            if (balanza != null)
            {
                checkBalanza.Checked = balanza.Valor;
            }

            var reclasificacion = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.ImpresiónReclasificacion)
                .FirstOrDefault();

            if (reclasificacion != null)
            {
                checkReclasificacion.Checked = reclasificacion.Valor;
            } 
            
            var gestionreclasificacion = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.ImpresiónGestionReclasificacion)
                .FirstOrDefault();

            if (gestionreclasificacion != null)
            {
                checkGestionReclasificacion.Checked = gestionreclasificacion.Valor;
            }
            
            var debug = Context.Configuracion
                .Where(x => x.Nombre == DevConstantes.Debug)
                .FirstOrDefault();

            if (debug != null)
            {
                checkDebug.Checked = debug.Valor;
            }
        }
    }
}