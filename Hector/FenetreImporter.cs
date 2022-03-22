using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    public partial class FenetreImporter : Form
    {
        public FenetreImporter()
        {
            InitializeComponent();
        }


        private void BoutonImporter_Click(object sender, EventArgs e)
        {
            var DialogueFichier = new OpenFileDialog()
            {
                FileName = "",
                Title = "Import .csv file"

            };
            DialogueFichier.InitialDirectory = Application.StartupPath;
            DialogueFichier.Filter = "csv files (*.csv)|*.csv";
            DialogueFichier.ShowDialog();

            FileStream Fichier = (FileStream)DialogueFichier.OpenFile();
            NomFichierTextBox.Text = Fichier.Name;

            using (StreamReader StreamReader = new StreamReader(Fichier))
            {
                string ContenuFichier = StreamReader.ReadToEnd();
                ContenuFichierTextBox.Text = ContenuFichier;
            }
            Fichier.Close();

            BoutonAjout.Enabled = true;
            BoutonEcrasement.Enabled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
