using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hector
{
    public partial class FormMain : Form
    {
        private Dictionary<string, Article> Articles;
        private Dictionary<string, Marque> Marques;
        private Dictionary<string, SousFamille> SousFamilles;
        private Dictionary<string, Famille> Familles;
        
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

            Articles = new Dictionary<string, Article>();
            Marques = new Dictionary<string, Marque>();
            SousFamilles = new Dictionary<string, SousFamille>();
            Familles = new Dictionary<string, Famille>();

            ActualiserDonnees();
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

            switch (ArbreArticles.SelectedNode.Name)
            {
                case "Articles" :
                    //On affiche l'ensemble des articles dans la ListView
                    AfficherArticlesListView(Articles);
                    break;
                    
                case "Familles":
                    //On affiche l'ensemble des familles dans la ListView
                    AfficherFamillesListView(Familles);
                    break;
                    
                case "Marques":
                    //On affiche l'ensemble des marques dans la ListView
                    AfficherMarquesListView(Marques);
                    break;

                default:
                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "^Article_.*$")) {
                        string RefArticle = Regex.Match(ArbreArticles.SelectedNode.Name, "^Article_(.*)$").Groups[1].Value;

                        //On affiche dans la ListView uniquement l'article sur lequel on à cliqué
                        AfficherArticlesListView(new Dictionary<string, Article>() { 
                            {
                                RefArticle, Articles[RefArticle] 
                            } 
                        });

                        break;
                    }

                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "^Marque_.*$"))
                    {
                        string RefMarque = Regex.Match(ArbreArticles.SelectedNode.Name, "^Marque_(.*)$").Groups[1].Value;

                        //On affiche dans la ListView uniquement les articles de la marque sur laquelle on à cliqué
                        AfficherArticlesListView(Marques[RefMarque].Articles);

                        break;
                    }

                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "^SousFamille_.*$"))
                    {
                        string RefSousFamille = Regex.Match(ArbreArticles.SelectedNode.Name, "^SousFamille_(.*)$").Groups[1].Value;

                        //On affiche dans la ListView uniquement les articles de la sous-famille sur laquelle on à cliqué
                        AfficherArticlesListView(SousFamilles[RefSousFamille].Articles);

                        break;
                    }

                    if (Regex.IsMatch(ArbreArticles.SelectedNode.Name, "^Famille_.*$"))
                    {
                        string RefFamille = Regex.Match(ArbreArticles.SelectedNode.Name, "^Famille_(.*)$").Groups[1].Value;

                        //On affiche dans la ListView uniquement les articles de la famille sur laquelle on à cliqué
                        foreach (SousFamille SousFamille in Familles[RefFamille].SousFamilles.Values)
                        {
                            AfficherArticlesListView(SousFamille.Articles);
                        }

                        break;
                    }

                    break;
            }
        }

        private void ActualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActualiserDonnees();
        }

        private void ActualiserDonnees()
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

            string RefMarque, RefSousFamille, RefFamille;

            foreach (Article Article in Articles.Values)
            {
                //ArbreArticles.Nodes["Articles"].Nodes.Add(Article.RefArticle);

                RefMarque = Article.Marque.RefMarque.ToString();
                RefSousFamille = Article.SousFamille.RefSousFamille.ToString();
                RefFamille = Article.SousFamille.Famille.RefFamille.ToString();

                if (!Marques.ContainsKey(RefMarque))
                {
                    //On stocke la marque dans la liste des marques
                    Marques[RefMarque] = Article.Marque;

                    //On ajoute la marque dans l'arbre
                    ArbreArticles.Nodes["Marques"].Nodes.Add(
                        PrefixeMarque + Article.Marque.RefMarque.ToString(),
                        Article.Marque.Nom
                    );
                }


                if (!SousFamilles.ContainsKey(RefSousFamille))
                {
                    //On stocke la sous-famille dans la liste des sous-familles
                    SousFamilles[RefSousFamille] = Article.SousFamille;
                }

                if (!Familles.ContainsKey(RefFamille))
                {
                    //On stocke la famille dans la liste des familles
                    Familles[RefFamille] = Article.SousFamille.Famille;

                    //On ajoute la famille dans l'arbre
                    ArbreArticles.Nodes["Familles"].Nodes.Add(
                        PrefixeFamille + Article.SousFamille.Famille.RefFamille.ToString(),
                        Article.SousFamille.Famille.Nom
                     );
                }
            }

            TreeNode LeNoeudFamille, LeNoeudSousFamille;

            //Pour chaque marque on ajoute les sous-familles dans l'arbre
            foreach (Famille Famille in Familles.Values)
            {
                LeNoeudFamille = ArbreArticles.Nodes["Familles"].Nodes[PrefixeFamille + Famille.RefFamille.ToString()];

                //Pour chaque sous-famille on ajoute la sous-famille et ses articles dans l'arbre
                foreach (SousFamille SousFamille in Famille.SousFamilles.Values)
                {

                    //On ajoute un noeud pour la sous famille
                    LeNoeudSousFamille = LeNoeudFamille.Nodes.Add(
                        PrefixeSousFamille + SousFamille.RefSousFamille.ToString(),
                        SousFamille.Nom
                    );

                    //On ajoute les articles de la sous-famille dans l'arbre
                    foreach (Article Article in SousFamille.Articles.Values)
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
                
            foreach(Marque Marque in Marques.Values)
            {
                LeNoeudMarque = ArbreArticles.Nodes["Marques"].Nodes[PrefixeMarque + Marque.RefMarque.ToString()];

                foreach(Article Article in Marque.Articles.Values)
                {
                    LeNoeudMarque.Nodes.Add(
                        PrefixeArticle + Article.RefArticle,
                        Article.Description
                    );
                }
            }
        }

    
        private void AfficherArticlesListView(Dictionary<string, Article> Articles)
        {
            if (Articles == null || Articles.Count == 0) return;

            string Prefixe = "Article_";
            
            if(!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Déscription")) ListView.Columns.Add(Prefixe + "Déscription", "Déscription", 200);
            if (!ListView.Columns.ContainsKey(Prefixe + "Famille")) ListView.Columns.Add(Prefixe + "Famille", "Famille", 100);
            if (!ListView.Columns.ContainsKey(Prefixe + "Sous-famille")) ListView.Columns.Add(Prefixe + "Sous-famille", "Sous-famille", 100);
            if (!ListView.Columns.ContainsKey(Prefixe + "Marque")) ListView.Columns.Add(Prefixe + "Marque", "Marque", 100);
            //if (!ListView.Columns.ContainsKey(Prefixe + "Prix")) ListView.Columns.Add(Prefixe + "Prix", "Prix", 50);
            if (!ListView.Columns.ContainsKey(Prefixe + "Quantité")) ListView.Columns.Add(Prefixe + "Quantité", "Quantité", 60);

            ListViewItem Ligne;

            foreach (Article Article in Articles.Values)
            {

                string[] Valeurs = {
                    Article.RefArticle,
                    Article.Description,
                    Article.SousFamille.Famille.Nom,
                    Article.SousFamille.Nom,
                    Article.Marque.Nom,
                    //Article.Prix.ToString(),
                    Article.Quantite.ToString()
                };

                Ligne = new ListViewItem(Valeurs);
                ListView.Items.Add(Ligne);
            }
        }


        private void AfficherMarquesListView(Dictionary<string, Marque> Marques)
        {
            if (Marques == null || Marques.Count == 0) return;

            string Prefixe = "Marque_";

            //if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Nom")) ListView.Columns.Add(Prefixe + "Nom", "Nom", 300);

            ListViewItem Ligne;
            
            foreach (Marque Marque in Marques.Values)
            {
                string[] Valeurs = {
                    //Marque.RefMarque.ToString(),
                    Marque.Nom
                };

                Ligne = new ListViewItem(Valeurs);
                ListView.Items.Add(Ligne);
            }
        }

        private void AfficherFamillesListView(Dictionary<string, Famille> Familles)
        {
            if (Familles == null || Familles.Count == 0) return;

            string Prefixe = "Famille_";


            //if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Nom")) ListView.Columns.Add(Prefixe + "Nom", "Nom", 300);

            ListViewItem Ligne;
            
            foreach (Famille Famille in Familles.Values)
            {
                string[] Valeurs = {
                            //Famille.RefFamille.ToString(),
                            Famille.Nom
                        };

                Ligne = new ListViewItem(Valeurs);
                ListView.Items.Add(Ligne);
            }
        }


        private void AfficherSousFamillesListView(Dictionary<string, SousFamille> SousFamilles)
        {
            if (SousFamilles == null || SousFamilles.Count == 0) return;

            string Prefixe = "SousFamille_";

            //if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Nom")) ListView.Columns.Add(Prefixe + "Nom", "Nom", 300);

            ListViewItem Ligne;

            foreach (SousFamille SousFamille in SousFamilles.Values)
            {
                string[] Valeurs = {
                    //SousFamille.RefSousFamille.ToString(),
                    SousFamille.Nom
                };

                Ligne = new ListViewItem(Valeurs);
                ListView.Items.Add(Ligne);
            }
        }
    }
} 

