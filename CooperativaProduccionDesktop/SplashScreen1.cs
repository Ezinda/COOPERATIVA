using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DesktopEntities.Models;
using System.Configuration;
using System.Security.Cryptography;

namespace CooperativaProduccion
{
    public partial class SplashScreen1 : SplashScreen
    {
        private DesktopEntities.Models.CooperativaProduccionEntities _dbContext;
        public static volatile dynamic Credenciales = new { Exitoso = false };

        public SplashScreen1()
        {
            InitializeComponent();
            _dbContext = new DesktopEntities.Models.CooperativaProduccionEntities();
        }

        public enum SplashScreenCommand
        {
            SolicitarCredenciales
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);

            SplashScreenCommand command = (SplashScreenCommand)cmd;

            if (command == SplashScreenCommand.SolicitarCredenciales)
            {
                SolicitarCredenciales();
            }
        }

        #endregion

        private void SolicitarCredenciales()
        {
            labelControlUser.Visible = true;
            txtUsuario.Visible = true;
            labelControlPassword.Visible = true;
            txtPass.Visible = true;
            btnLogin.Visible = true;
            btnSalir.Visible = true;
            txtUsuario.Text = string.Empty;
            txtPass.Text = string.Empty;

            labelControlTarea.Text = string.Empty;
            marqueeProgressBarControl1.Properties.Paused = true;
            marqueeProgressBarControl1.Visible = false;
            txtUsuario.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MostrarProgreso();

            if (!ComprobarCredenciales(txtUsuario.Text, txtPass.Text))
            {
                SolicitarCredenciales();
            }
        }

        private bool ComprobarCredenciales(string user, string password)
        {
            Usuario usuario;
            password = Encrypt(txtPass.Text, true);
            var acceso = _dbContext.Usuario
                .Where(x => x.Usuario1.Equals(user)
                    && x.Password.Equals(password));
            if (acceso.Count() == 1)
            {
                usuario = acceso.FirstOrDefault();
                
                Credenciales = new
                {
                    Exitoso = true,
                    Usuario = usuario,
                    UserName = usuario.Nombre
                };
                
                return true;
            }
            else
            {
                MessageBox.Show("Usuario Inexistente", "Atención", MessageBoxButtons.OK);
                return false;
            }       
        }
   
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "Pass";// (string)settingsReader.GetValue("SecurityKey",
            //                              typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private void SplashScreen1_Load(object sender, EventArgs e)
        {

        }

        private void MostrarProgreso()
        {
            labelControlUser.Visible = false;
            txtUsuario.Visible = false;
            labelControlPassword.Visible = false;
            txtPass.Visible = false;
            btnLogin.Visible = false;
            btnSalir.Visible = false;

            labelControlTarea.Text = "Cargando ventana principal...";
            marqueeProgressBarControl1.Properties.Paused = false;
            marqueeProgressBarControl1.Visible = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPass.Focus();
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.Focus();
            }
        }

    }
}