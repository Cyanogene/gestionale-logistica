using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestione_materiali
{
    /// <summary>
    /// Classe utilizzata per mostrare una custom MessageBox.
    /// </summary>
    class Box
    {
        /// <summary>
        /// Mostra un messaggio.
        /// </summary>
        /// <param name="message">Messaggio da visualizzare.</param>
        /// <param name="caption">Titolo del messaggio.</param>
        /// <param name="button">Pulsanti da visualizzare.</param>
        /// <param name="icon">Icona da visualizzare.</param>
        /// <returns></returns>
        public static DialogResult Show(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon)
        {
            DialogResult result = DialogResult.None;
            CustomMessageBox c = new CustomMessageBox();

            c.Text = caption;
            c.Message = message;
            c.MessageIcon = gestione_materiali.Properties.Resources.Informazioni;
            result = c.ShowDialog();

            return result;
        }
    }
}

