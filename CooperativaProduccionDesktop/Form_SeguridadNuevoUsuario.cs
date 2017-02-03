using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Configuration;
using System.Security.Cryptography;
using DesktopEntities.Models;

namespace CooperativaProduccion
{
    public partial class Form_SeguridadNuevoUsuario : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Usuario usuario;
        public CooperativaProduccionEntities Context;
        public Form_SeguridadNuevoUsuario()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        private void btnGenerarUser_Click(object sender, EventArgs e)
        {
            if (ValidarNombre())
            {
                var resultado = MessageBox.Show("¿Desea generar un nuevo nombre de usuario?",
                    "Crear Usuario", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GenerarUser();
            }
        }

        private void GenerarUser()
        {
            txtUser.Text = (txtNombre.Text.Substring(0, 1) + txtApellido.Text).ToLower();
        }

        private bool ValidarNombre()
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("No se ha completado el campo de Nombre.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtApellido.Text == string.Empty)
            {
                MessageBox.Show("No se ha completado el campo de Apellido.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "Pass";
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                var resultado = MessageBox.Show("¿Desea crear un nuevo usuario?",
                    "Crear Usuario", MessageBoxButtons.OKCancel);
                if (resultado != DialogResult.OK)
                {
                    return;
                }
                GuardarDatos();
                this.Close();
            }
        }

        private bool ValidarCampos()
        {
            if (txtUser.Text == string.Empty)
            {
                MessageBox.Show("No se ha completado el campo de Usuario",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPass.Text == string.Empty)
            {
                MessageBox.Show("No se ha completado el campo de Contraseña",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPass.Text != txtRePass.Text)
            {
                MessageBox.Show("Los campos de Contraseña y Confirmacion de Contraseña no coinciden.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void GuardarDatos()
        {
            try
            {
                usuario = new Usuario();
                usuario.Id = Guid.NewGuid();
                usuario.Usuario1 = txtUser.Text;
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Password = Encrypt(txtPass.Text, true);
                
                Context.Usuario.Add(usuario);
                Context.SaveChanges();

                MessageBox.Show("Usuario creado.",
                    "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                throw;
            }
        } 
    }
}