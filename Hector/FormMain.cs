using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;

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

        
        private void ExporterToolStripMenuItem_Click(object Emetteur, EventArgs Evenement)
        {
            FenetreExporter FormulaireExporter = new FenetreExporter(Connexion);
            FormulaireExporter.ShowDialog();
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<Article> ListeArticle = new List<Article>();
            ArticleDAO ArticleDAO = new ArticleDAO();
            switch (ArbreArticles.SelectedNode.Text)
            {
                case "Tous les articles" :
                    //Recupérer tous les articles de la base de données
                    ListeArticle = ArticleDAO.ObtenirTousArticles();

                    foreach(Article Article in ListeArticle)
                    {
                        ListViewItem Item = new ListViewItem(Article.RefArticle);
                        Item.SubItems.Add(Article.Description);
                       // Item.SubItems.Add(Article.SousFamille);
                        //Item.SubItems.Add(Article.Marque);
                        Item.SubItems.Add(Article.Prix.ToString());
                        Item.SubItems.Add(Article.Quantite.ToString());
                        ListView.Items.Add(Item);
                    }
                    break;
                case "Familles":
                    //Recuperer juste les articles par rapport à leur famille et non avec les marques
                    FamilleDAO FamilleDAO = new FamilleDAO();
                    List<string> ListeFamille = FamilleDAO.ObtenirFamilles();
                    foreach(string Famille in ListeFamille)
                    {
                        ListView.Items.Add(new ListViewItem(Famille));
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

