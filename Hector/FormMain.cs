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
    public partial class FormMain : Form
    {

        /// <summary>
        /// Chemin vers la base de données SQLite.
        /// </summary>
        public static string CheminVersSQLite = Application.StartupPath + "\\Hector.SQLite";

        /// <summary>
        /// La connexion vers la base de données
        /// </summary>
        private ConnexionBDD Connexion;

        public FormMain()
        {
            Connexion = new ConnexionBDD(CheminVersSQLite);
            InitializeComponent();
        }

        private void ImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FenetreImporter FormulaireImporter = new FenetreImporter(Connexion);
            FormulaireImporter.ShowDialog();   
            //var DialogBox = new show
            /*var openFileDialog1 = new OpenFileDialog()
            {
                FileName = "",
                Title = "Import .csv file"

            };
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();

            var myStream = openFileDialog1.OpenFile();
            using (StreamReader reader = new StreamReader(myStream))
            {
                var fileContent = reader.ReadToEnd();
            }
            //textBox1.Text = fileContent;
            myStream.Close();*/
        }

    }
}
