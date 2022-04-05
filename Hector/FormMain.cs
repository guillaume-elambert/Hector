using System;
using System.Windows.Forms;
using System.Data.SQLite;

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

        private void ImporterToolStripMenuItem_Click(object Emetteur, EventArgs Evenement)
        {
            FenetreImporter FormulaireImporter = new FenetreImporter(Connexion);
            FormulaireImporter.ShowDialog();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (ArbreArticles.SelectedNode.Text)
            {
                case "Tous les articles" :
                    //Recupérer tous les articles de la base de données
                    SQLiteCommand Commande = Connexion.getConnexion().CreateCommand();
                    Commande.CommandText = "SELECT * FROM Articles";
                    SQLiteDataReader Resultat = Commande.ExecuteReader();

                    while (Resultat.Read())
                    {
                        Article Article = new Article();
                        Article.Description = (string)Resultat["Description"];
                        Article.Marque = (Marque)Resultat["Marque"];
                        Article.SousFamille = (SousFamille)Resultat["SousFamille"];
                        Article.Prix = (float)Resultat["Prix"];
                        Article.Quantite = (int)Resultat["Quantite"];
                        ListView.Items.Add(Article.ToString());
                    }
                        break;
                case "Familles":
                    //Recuperer juste les articles par rapport à leur famille et non avec les marques
                    SQLiteCommand Commande2 = Connexion.getConnexion().CreateCommand();
                    Commande2.CommandText = "SELECT Description FROM Articles INNER JOIN SousFamille INNER JOIN Famille";
                    SQLiteDataReader Resultat2 = Commande2.ExecuteReader();

                    while (Resultat2.Read())
                    {
                        Article Article = new Article();
                        Article.Description = (string)Resultat2["Description"];
                        ListView.Items.Add(Article.ToStringDescription());
                    }
                    break;
                case "Marques":
                    //Recuperer seulement les marques
                    break;
                case "EcritureEtCorrection":
                    //Recuperer les objets qui appartiennent à la sous famille ecriture et correction
                    break;
                case "SousFamilleEC":
                    break;
                case "Clairefontaine":
                    //Recuperer les elements avec clairefontaines
                    break;
            }
        }
    }
} 

