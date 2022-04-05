using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hector
{
    public partial class FormMain : Form
    {
        private List<Article> Articles;
        private List<Marque> Marques;
        private List<SousFamille> SousFamilles;
        private List<Famille> Familles;
        
        private ArticleDAO ArticleDAO;

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
            InitializeComponent();
            

            Connexion = new ConnexionBDD(CheminVersSQLite);
            ArticleDAO = new ArticleDAO(Connexion);

            Articles = new List<Article>();
            Marques = new List<Marque>();
            SousFamilles = new List<SousFamille>();
            Familles = new List<Famille>();

            ActualiserDonnées();
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


        private void ArbreArticles_AfterSelect(object sender, TreeViewEventArgs e)
        { 
            ListView.Items.Clear();
            ListView.Columns.Clear();
            ListViewItem Ligne;

            switch (ArbreArticles.SelectedNode.Name)
            {
                case "Articles" :
                    if (Articles == null || Articles.Count == 0) return;

                    ListView.Columns.Add("Référence", 100, HorizontalAlignment.Left);
                    ListView.Columns.Add("Déscription", 200, HorizontalAlignment.Left);
                    ListView.Columns.Add("Prix", 50, HorizontalAlignment.Left);
                    ListView.Columns.Add("Quantité", 60, HorizontalAlignment.Left);

                    foreach (Article Article in Articles)
                    {

                        string[] Valeurs = { 
                            Article.RefArticle,
                            Article.Description,
                            Article.Prix.ToString(),
                            Article.Quantite.ToString()
                        };
                        
                        Ligne = new ListViewItem(Valeurs);                        
                        ListView.Items.Add(Ligne);
                    }
                    
                    break;
                    
                case "Familles":
                    if (Familles == null || Familles.Count == 0) return;


                    ListView.Columns.Add("Référence", 80, HorizontalAlignment.Left);
                    ListView.Columns.Add("Nom", 100, HorizontalAlignment.Left);

                    foreach (Famille Famille in Familles)
                    {
                        string[] Valeurs = {
                            Famille.RefFamille.ToString(),
                            Famille.Nom 
                        };

                        Ligne = new ListViewItem(Valeurs);
                        ListView.Items.Add(Ligne);
                    }
                    break;
                    
                case "Marques":
                    if (Marques == null || Marques.Count == 0) return;
                    
                    ListView.Columns.Add("Référence", 80, HorizontalAlignment.Left);
                    ListView.Columns.Add("Nom", 100, HorizontalAlignment.Left);

                    foreach (Marque Marque in Marques)
                    {
                        string[] Valeurs = {
                            Marque.RefMarque.ToString(),
                            Marque.Nom
                        };

                        Ligne = new ListViewItem(Valeurs);
                        ListView.Items.Add(Ligne);
                    }
                    break;

                default:
                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "Article_.*")) {
                        string RefArticle = Regex.Match(ArbreArticles.SelectedNode.Name, "Article_(.*)").Groups[1].Value;

                        ListView.Columns.Add("Référence", 100, HorizontalAlignment.Left);
                        ListView.Columns.Add("Déscription", 200, HorizontalAlignment.Left);
                        ListView.Columns.Add("Prix", 50, HorizontalAlignment.Left);
                        ListView.Columns.Add("Quantité", 60, HorizontalAlignment.Left);

                        foreach (Article Article in Articles)
                        {

                            if (RefArticle != Article.RefArticle) continue;
                            
                            string[] Valeurs = {
                                Article.RefArticle,
                                Article.Description,
                                Article.Prix.ToString(),
                                Article.Quantite.ToString()
                            };

                            Ligne = new ListViewItem(Valeurs);
                            ListView.Items.Add(Ligne);
                        }
                        
                        return;
                    }

                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "Marque_.*"))
                    {
                        string RefMarque = Regex.Match(ArbreArticles.SelectedNode.Name, "Marque_(.*)").Groups[1].Value;

                        ListView.Columns.Add("Référence", 80, HorizontalAlignment.Left);
                        ListView.Columns.Add("Nom", 100, HorizontalAlignment.Left);

                        foreach (Marque Marque in Marques)
                        {
                            if (RefMarque != Marque.RefMarque.ToString()) continue;
                            
                            string[] Valeurs = {
                                Marque.RefMarque.ToString(),
                                Marque.Nom
                            };

                            Ligne = new ListViewItem(Valeurs);
                            ListView.Items.Add(Ligne);
                        }
                        return;
                    }

                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "SousFamille_.*"))
                    {
                        string RefSousFamille = Regex.Match(ArbreArticles.SelectedNode.Name, "SousFamille_(.*)").Groups[1].Value;

                        ListView.Columns.Add("Référence", 80, HorizontalAlignment.Left);
                        ListView.Columns.Add("Nom", 100, HorizontalAlignment.Left);

                        foreach (SousFamille SousFamille in SousFamilles)
                        {
                            if (RefSousFamille != SousFamille.RefSousFamille.ToString()) continue;

                            string[] Valeurs = {
                                SousFamille.RefSousFamille.ToString(),
                                SousFamille.Nom
                            };

                            Ligne = new ListViewItem(Valeurs);
                            ListView.Items.Add(Ligne);
                        }
                        return;
                    }

                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "Famille_.*"))
                    {
                        string RefFamille = Regex.Match(ArbreArticles.SelectedNode.Name, "Famille_(.*)").Groups[1].Value;

                        ListView.Columns.Add("Référence", 80, HorizontalAlignment.Left);
                        ListView.Columns.Add("Nom", 100, HorizontalAlignment.Left);

                        foreach (Famille Famille in Familles)
                        {
                            if (RefFamille != Famille.RefFamille.ToString()) continue;

                            string[] Valeurs = {
                                Famille.RefFamille.ToString(),
                                Famille.Nom
                            };

                            Ligne = new ListViewItem(Valeurs);
                            ListView.Items.Add(Ligne);
                        }
                        return;
                    }

                    break;
            }
        }

        private void ActualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActualiserDonnées();
        }

        private void ActualiserDonnées()
        {
            Articles = ArticleDAO.ObtenirTout();
            Marques.Clear();
            SousFamilles.Clear();
            Familles.Clear();

            ArbreArticles.Nodes.Clear();

            if (Articles == null || Articles.Count == 0) return;

            //On ajoute les noeuds parents
            ArbreArticles.Nodes.Add("Articles", "Tous les articles");
            ArbreArticles.Nodes.Add("Familles", "Familles");
            ArbreArticles.Nodes.Add("Marques", "Marques");

            string PrefixeArticle = "Article_";
            string PrefixeMarque = "Marque_";
            string PrefixeSousFamille = "SousFamille_";
            string PrefixeFamille = "Famille_";

            foreach (Article Article in Articles)
            {
                //ArbreArticles.Nodes["Articles"].Nodes.Add(Article.RefArticle);
                
                if (!Marques.Contains(Article.Marque))
                {
                    //On stocke la marque dans la liste des marques
                    Marques.Add(Article.Marque);

                    //On ajoute la marque dans l'arbre
                    ArbreArticles.Nodes["Marques"].Nodes.Add(
                        PrefixeMarque + Article.Marque.RefMarque.ToString(),
                        Article.Marque.Nom
                    );
                }


                if (!SousFamilles.Contains(Article.SousFamille))
                {
                    //On stocke la sous-famille dans la liste des sous-familles
                    SousFamilles.Add(Article.SousFamille);
                }

                if (!Familles.Contains(Article.SousFamille.Famille))
                {
                    //On stocke la famille dans la liste des familles
                    Familles.Add(Article.SousFamille.Famille);

                    //On ajoute la famille dans l'arbre
                    ArbreArticles.Nodes["Familles"].Nodes.Add(
                        PrefixeFamille + Article.SousFamille.Famille.RefFamille.ToString(),
                        Article.SousFamille.Famille.Nom
                     );
                }
            }

            TreeNode LeNoeudFamille, LeNoeudSousFamille;

            //Pour chaque marque on ajoute les sous-familles dans l'arbre
            foreach (Famille Famille in Familles)
            {
                LeNoeudFamille = ArbreArticles.Nodes["Familles"].Nodes[PrefixeFamille + Famille.RefFamille.ToString()];

                //Pour chaque sous-famille on ajoute la sous-famille et ses articles dans l'arbre
                foreach (SousFamille SousFamille in Famille.SousFamilles)
                {

                    //On ajoute un noeud pour la sous famille
                    LeNoeudSousFamille = LeNoeudFamille.Nodes.Add(
                        PrefixeSousFamille + SousFamille.RefSousFamille.ToString(),
                        SousFamille.Nom
                    );

                    //On ajoute les articles de la sous-famille dans l'arbre
                    foreach (Article Article in SousFamille.Articles)
                    {
                        //On ajoute un noeud pour l'article
                        LeNoeudSousFamille.Nodes.Add(
                            PrefixeArticle + Article.RefArticle,
                            Article.Description
                        );
                    }
                }
                //ArbreArticles.Nodes[1].Nodes.Find(Famille.Nom, false)[0].Nodes.Add(Famille.SousFamilles.ToArray);

            }

            TreeNode LeNoeudMarque;
                
            foreach(Marque Marque in Marques)
            {
                LeNoeudMarque = ArbreArticles.Nodes["Marques"].Nodes[PrefixeMarque + Marque.RefMarque.ToString()];

                foreach(Article Article in Marque.Articles)
                {
                    LeNoeudMarque.Nodes.Add(
                        PrefixeArticle + Article.RefArticle,
                        Article.Description
                    );
                }
            }
        }
    }
} 

