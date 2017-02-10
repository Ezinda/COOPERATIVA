using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionRRHH.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Show(string title, string text)
        {
            MessageBox.Show(
                text,
                title,
                MessageBoxButtons.OK);
        }
        
        public void ShowInfo(string title, string text)
        {
            MessageBox.Show(
                text,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public void ShowError(string title, string text)
        {
            MessageBox.Show(
                text,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public bool ShowConfirm(string title, string text)
        {
            var result = MessageBox.Show(
                text,
                title,
                MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }
    }

    public interface IMessageBoxService
    {
        void Show(string p1, string p2);

        void ShowInfo(string title, string text);

        void ShowError(string p1, string p2);

        bool ShowConfirm(string p1, string p2);
    }
}
