using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DesktopEntities.Models;
using System.Configuration;
using System.Security.Cryptography;
using System.Data.Entity;

namespace CooperativaProduccion
{
    public partial class Form_SeguridadGestionUsuario : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CooperativaProduccionEntities Context;
        public Guid UsuarioId;

        public Form_SeguridadGestionUsuario()
        {
            InitializeComponent();
            Context = new CooperativaProduccionEntities();
        }

        private void Form_SysGestionUsuario_Load(object sender, EventArgs e)
        {
            Deshabilitar();
            ListarUsuarios();
        }

        private void ListarUsuarios()
        {
            var result = (
               from a in Context.Usuario
               select new
               {
                   Id = a.Id,
                   Nombre = a.Nombre,
                   Apellido = a.Apellido,
                   Usuario = a.Usuario1
               }).ToList();

            gridControlUsuario.DataSource = result;
            gridViewUsuario.Columns[0].Visible = false;
            gridViewUsuario.Columns[1].Width = 100;
            gridViewUsuario.Columns[2].Width = 100;
            gridViewUsuario.Columns[3].Width = 100;
        }

        private void gridControlUsuario_DoubleClick(object sender, EventArgs e)
        {
            UsuarioId = new Guid(gridViewUsuario
                .GetRowCellValue(gridViewUsuario.FocusedRowHandle, "Id")
                .ToString());
            CargarUsuario(UsuarioId);
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea eliminar usuario?",
            "Eliminar Usuario", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            EliminarUsuario(UsuarioId);
        }

        private void EliminarUsuario(Guid UsuarioId)
        {
            if (gridViewUsuario.SelectedRowsCount > 0)
            {
                var usuario = Context.Usuario.Find(UsuarioId);
                if (!String.IsNullOrEmpty(usuario.Password))
                {
                    Context.Entry(usuario).State = EntityState.Deleted;
                    Context.SaveChanges();

                    MessageBox.Show("Usuario eliminado.",
                        "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarUsuarios();
                    Limpiar();
                    Deshabilitar();
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun registro.",
                    "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar usuario?",
                "Modificar Usuario", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            Habilitar();
        }
        
        private void Deshabilitar()
        {
            txtApellido.Enabled = false;
            txtNombre.Enabled = false;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            btnGrabar.Visible = false;
        }

        private void Habilitar()
        {
            txtApellido.Enabled = true;
            txtNombre.Enabled = true;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            btnGrabar.Visible = true;
        }
   
        private void CargarUsuario(Guid UsuarioId)
        {
            var usuarioExiste = Context.Usuario
                .Where(x => x.Id.Equals(UsuarioId))
                .ToList();
            if (usuarioExiste.Count==1)
            {
                var usuario = usuarioExiste.FirstOrDefault();
                txtApellido.Text = usuario.Apellido;
                txtNombre.Text = usuario.Nombre;
                txtUser.Text = usuario.Usuario1;
                txtPass.Text = Decrypt(usuario.Password,true);
            }
            
        }

        private void ModificarUsuario(Guid UsuarioId)
        {
            if (gridViewUsuario.SelectedRowsCount > 0)
            {
                var usuario = Context.Usuario.Find(UsuarioId);
                if (!String.IsNullOrEmpty(usuario.Id.ToString()))
                {
                    usuario.Nombre = txtNombre.Text;
                    usuario.Apellido = txtApellido.Text;
                    usuario.Usuario1 = txtUser.Text;
                    usuario.Password = Encrypt(txtPass.Text, true);

                    Context.Entry(usuario).State = EntityState.Modified;
                    Context.SaveChanges();

                    MessageBox.Show("Usuario Modificado.",
                        "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarUsuarios();
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun registro.", "Se requiere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "Pass";

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea modificar usuario?",
                   "Modificar Usuario", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
            {
                return;
            }
            ModificarUsuario(UsuarioId);
            Deshabilitar();
        }
    }
}