using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Drawing;

namespace Hector
{

    /// <summary>
    /// Classe du formulaire principal.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Dictionnaire des Articles.
        /// </summary>
        private Dictionary<string, Article> Articles;
        /// <summary>
        /// Dictionnaire des Marques
        /// </summary>
        private Dictionary<string, Marque> Marques;
        /// <summary>
        /// Dictionnaire des SousFamilles;
        /// </summary>
        private Dictionary<string, SousFamille> SousFamilles;
        /// <summary>
        /// Dictionnaire des Familles.
        /// </summary>
        private Dictionary<string, Famille> Familles;
        
        /// <summary>
        /// Objet DAO des Articles.
        /// </summary>
        private ArticleDAO ArticleDAO;
        /// <summary>
        /// Objet DAO des Marques.
        /// </summary>
        MarqueDAO MarqueDAO;
        /// <summary>
        /// Objet DAO des Familles.
        /// </summary>
        FamilleDAO FamilleDAO;
        /// <summary>
        /// Objet DAO des SousFamilles.
        /// </summary>
        SousFamilleDAO SousFamilleDAO;

        /// <summary>
        /// Le nom du dernier élément de la TreeView qui à été clické 
        /// (permet de réafficher ce que l'utilisateur voyait avant d'actualiser les données).
        /// </summary>
        private string NomDernierElementTreeViewClicke;
        /// <summary>
        /// Le nom de la dernière colonne de la ListView qui à été clickée
        /// (permet de réafficher ce que l'utilisateur voyait avant d'actualiser les données).
        /// </summary>
        private string NomDerniereColonneListViewClickee;


        /// <summary>
        /// Préfixe utilisé pour les clés dans tous les dictionnaires des Articles (TreeViewItem & TreeNode)
        /// </summary>
        private const string PrefixeArticle = "Article_";
        /// <summary>
        /// Préfixe utilisé pour les clés dans tous les dictionnaires des Marques (TreeViewItem & TreeNode)
        /// </summary>
        private const string PrefixeMarque = "Marque_";
        /// <summary>
        /// Préfixe utilisé pour les clés dans tous les dictionnaires des SousFamilles (TreeViewItem & TreeNode)
        /// </summary>
        private const string PrefixeSousFamille = "SousFamille_";
        /// <summary>
        /// Préfixe utilisé pour les clés dans tous les dictionnaires des Familles (TreeViewItem & TreeNode)
        /// </summary>
        private const string PrefixeFamille = "Famille_";

        /// <summary>
        /// Chemin vers la base de données SQLite.
        /// </summary>
        public static string CheminVersSQLite = Application.StartupPath + "\\Hector.SQLite";

        /// <summary>
        /// La connexion vers la base de données
        /// </summary>
        private ConnexionBDD Connexion;


        /// <summary>
        /// Constructeur.
        /// </summary>
        public FormMain()
        {
            //Initialisation de l'IHM
            InitializeComponent();
            
            //Initialisation des objets BDD
            Connexion = new ConnexionBDD(CheminVersSQLite);
            ArticleDAO = new ArticleDAO(Connexion);
            MarqueDAO = new MarqueDAO(Connexion);
            FamilleDAO = new FamilleDAO(Connexion);
            SousFamilleDAO = new SousFamilleDAO(Connexion);

            //Initialisation des dictionnaires
            Articles = new Dictionary<string, Article>();
            Marques = new Dictionary<string, Marque>();
            SousFamilles = new Dictionary<string, SousFamille>();
            Familles = new Dictionary<string, Famille>();

            //On récupère les données stockées en BDD
            ActualiserDonnees();
        }


        /// <summary>
        /// Action lors du clic dans le menu "Fichier" > "Importer"
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void ImporterToolStripMenuItem_Click(object Emetteur, EventArgs Evenement)
        {
            //On affiche une nouvelle fenêtre
            FenetreImporter FormulaireImporter = new FenetreImporter(Connexion);
            FormulaireImporter.ShowDialog();

            //Après importation on récupère les nouvelles données
            ActualiserDonnees();
            //Puis on actualise la ListView pour les prendre en compte
            ActualiserListView(NomDernierElementTreeViewClicke);
        }


        /// <summary>
        /// Action lors du clic dans le menu "Fichier" > "Exporter"
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void ExporterToolStripMenuItem_Click(object Emetteur, EventArgs Evenement)
        {
            //On affiche un nouvelle fenêtre
            FenetreExporter FormulaireExporter = new FenetreExporter(Connexion);
            FormulaireExporter.ShowDialog();
        }


        /// <summary>
        /// Action lors du clic dans le menu "Fichier" > "Actualiser"
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void ActualiserToolStripMenuItem_Click(object Emetteur, EventArgs Evenement)
        {
            //On récupère les nouvelles données
            ActualiserDonnees();
            //Puis on actualise la ListView pour les prendre en compte
            ActualiserListView(NomDernierElementTreeViewClicke);
        }


        /// <summary>
        /// Action lors du clic sur un élément du TreeView (partie gauche de la fenêtre)
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void ArbreArticles_AfterSelect(object Emetteur, TreeViewEventArgs Evenement)
        {
            //On stocke l'élément qui à appelé cette méthode
            NomDernierElementTreeViewClicke = Evenement.Node.Name;
            NomDerniereColonneListViewClickee = null;

            //On met à jour le contenu de la ListView
            ActualiserListView(NomDernierElementTreeViewClicke);
        }


        /// <summary>
        /// Méthode qui gère le clic sur les colonnes de la ListView
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void ListView_ColumnClick(object Emetteur, ColumnClickEventArgs Evenement)
        {
            //On trie sur la colonne cliquée
            TrierListViewSurColonne(ListView.Columns[Evenement.Column].Name);
        }


        /// <summary>
        /// Méthode qui permet d'aller récupérer les objets en BDD.
        /// </summary>
        private void ActualiserDonnees()
        {
            //On récupère tous les articles
            Articles = ArticleDAO.ObtenirTout();

            //On récupère les marques, familles et sous-familles dans des dictionnaires temporaires
            Dictionary<string, Marque> TempMarques = MarqueDAO.ObtenirTout();
            Dictionary<string, SousFamille> TempSousFamilles = SousFamilleDAO.ObtenirTout();
            Dictionary<string, Famille> TempFamilles = FamilleDAO.ObtenirTout();

            //On supprime toutes les marques, familles et sous familles stockées précédemment
            Marques.Clear();
            SousFamilles.Clear();
            Familles.Clear();

            //On vide la TreeView
            ArbreArticles.Nodes.Clear();

            //On ajoute les noeuds parents
            ArbreArticles.Nodes.Add("Articles", "Tous les articles");
            ArbreArticles.Nodes.Add("Familles", "Familles");
            ArbreArticles.Nodes.Add("Marques", "Marques");

            string RefMarque, RefSousFamille, RefFamille;

            //Entrée : Il n'y pas d'Articles
            //      => On récupère les marques, familles et sous familles
            //         On ajoute ces objets dans les dictionnaires
            if (Articles == null || Articles.Count == 0)
            {
                //On réinitialise le dictionnaire des articles (pour éviter erreur si null)
                Articles = new Dictionary<string, Article>();
            }
                
                
            if (TempFamilles == null || TempFamilles.Count == 0)
            {
                //On réinitialise le dictionnaire des familles (pour éviter erreur si null)
                Familles = new Dictionary<string, Famille>();
                TempFamilles = new Dictionary<string, Famille>();
            }

            //On réinitialise le dictionnaire des familles (pour éviter erreur si null)
            if (TempSousFamilles == null || TempSousFamilles.Count == 0)
            {
                //On réinitialise le dictionnaire des sous-familles (pour éviter erreur si null)
                SousFamilles = new Dictionary<string, SousFamille>();
                TempSousFamilles = new Dictionary<string, SousFamille>();
            }

            if(TempMarques == null || TempMarques.Count == 0)
            {
                //On réinitialise le dictionnaire des marques (pour éviter erreur si null)
                Marques = new Dictionary<string, Marque>();
                TempMarques = new Dictionary<string, Marque>();
            }



            //On parcours tous les Articles (s'il y en a)
            //(pas besoin de récupérer les marques, familles et sous-famille car
            //déjà fait dans ArticleDAO.ObtenirTout() et stockées dans les Articles)
            foreach (Article Article in Articles.Values)
            {
                //On ajoute le noeud de l'article sous le noeud parent "Tous les articles"
                ArbreArticles.Nodes["Articles"].Nodes.Add(
                    PrefixeArticle + Article.RefArticle,
                    Article.Description
                );

                //On récupère les référence sur la marque, la famille et la sous-famille de l'article
                RefMarque = Article.Marque.RefMarque.ToString();
                RefSousFamille = Article.SousFamille.RefSousFamille.ToString();
                RefFamille = Article.SousFamille.Famille.RefFamille.ToString();

                //Entrée : La référence de la marque n'existe pas dans le dictionnaire
                //      => On l'ajoute dans le dictionnaire
                if (!Marques.ContainsKey(RefMarque))
                {
                    //On stocke la marque dans la liste des marques
                    Marques[RefMarque] = Article.Marque;
                }


                //Entrée : La référence de la sous-famille n'existe pas dans le dictionnaire
                //      => On l'ajoute dans le dictionnaire
                if (!SousFamilles.ContainsKey(RefSousFamille))
                {
                    //On stocke la sous-famille dans la liste des sous-familles
                    SousFamilles[RefSousFamille] = Article.SousFamille;
                }


                //Entrée : La référence de la famille n'existe pas dans le dictionnaire
                //      => On l'ajoute dans le dictionnaire
                if (!Familles.ContainsKey(RefFamille))
                {
                    //On stocke la famille dans la liste des familles
                    Familles[RefFamille] = Article.SousFamille.Famille;
                }
            }



            //On ajoute les marques qui n'ont pas d'article dans le dictionnaire
            foreach (Marque Marque in TempMarques.Values)
            {
                RefMarque = Marque.RefMarque.ToString();

                //Entrée : La référence de la marque n'existe pas dans le dictionnaire
                //      => On l'ajoute
                if (!Marques.ContainsKey(RefMarque))
                {
                    Marques[RefMarque] = Marque;
                }
            }

            //On ajoute les familles qui n'ont pas d'article dans le dictionnaire
            foreach (Famille Famille in TempFamilles.Values)
            {
                RefFamille = Famille.RefFamille.ToString();

                //Entrée : La référence de la famille n'existe pas dans le dictionnaire
                //      => On l'ajoute
                if (!Familles.ContainsKey(RefFamille))
                {
                    Familles[RefFamille] = Famille;
                }
            }

            //On ajoute les sous-familles qui n'ont pas d'article dans le dictionnaire
            foreach (SousFamille SousFamille in TempSousFamilles.Values)
            {
                RefSousFamille = SousFamille.RefSousFamille.ToString();
                RefFamille = SousFamille.Famille.RefFamille.ToString();

                //Entrée : La référence de la sous-famille n'existe pas dans le dictionnaire
                //      => On l'ajoute
                if (!SousFamilles.ContainsKey(RefSousFamille))
                {
                    SousFamilles[RefSousFamille] = SousFamille;
                    Familles[RefFamille].SousFamilles[RefSousFamille] = SousFamille;
                }

                //On ajoute les familles qui n'ont pas d'article dans le dictionnaire
                if (!Familles.ContainsKey(RefFamille))
                {
                    Familles[RefFamille] = SousFamille.Famille;
                }
            }

            TreeNode LeNoeudFamille, LeNoeudSousFamille;

            //Pour chaque famille on ajoute les sous-familles dans l'arbre
            foreach (Famille Famille in Familles.Values)
            {
                //On ajoute la famille dans l'arbre
                LeNoeudFamille = ArbreArticles.Nodes["Familles"].Nodes.Add(
                    PrefixeFamille + Famille.RefFamille.ToString(),
                    Famille.Nom
                );

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

            }

            TreeNode LeNoeudMarque;
            
            //Pour chaque marque on ajoute les Article dans l'abre
            foreach(Marque Marque in Marques.Values)
            {
                //On ajoute la marque dans l'arbre
                LeNoeudMarque = ArbreArticles.Nodes["Marques"].Nodes.Add(
                    PrefixeMarque + Marque.RefMarque.ToString(),
                    Marque.Nom
                );

                //On ajoute le articles de la marque dans l'arbre
                foreach(Article Article in Marque.Articles.Values)
                {
                    //On ajoute un noeud pour l'article
                    LeNoeudMarque.Nodes.Add(
                        PrefixeArticle + Article.RefArticle,
                        Article.Description
                    );
                }
            }

            //Compteurs d'éléments
            int NombreArticles = Articles.Count;
            int NombreMarques = Marques.Count;
            int NombreSousFamilles = SousFamilles.Count;
            int NombreFamilles = Familles.Count;
            int NombreEnregistrements = NombreArticles + NombreMarques + NombreSousFamilles + NombreFamilles;

            TexteStatusStrip.Text = "Enregistrements : " + NombreEnregistrements + "  |  " +
                "Articles : " + NombreArticles + " - " +
                "Marques : " + NombreMarques + " - " +
                "Sous-familles : " + NombreSousFamilles + " - " +
                "Familles : " + NombreFamilles;
        }


        /// <summary>
        /// Méthode qui permet d'actualiser le contenu de la TreeView en fonction du nom d'un noeud du TreeView
        /// </summary>
        /// <param name="NomElementTreeViewAAfficher">Le nom du nom du TreeView à afficher</param>
        private void ActualiserListView(string NomElementTreeViewAAfficher)
        {
            //On vide le contenu de la ListView
            ListView.Items.Clear();
            ListView.Columns.Clear();
            ListView.Groups.Clear();

            //On supprime le trie sur la ListView
            ListView.ListViewItemSorter = null;

            NomDernierElementTreeViewClicke = NomElementTreeViewAAfficher;

            //Si rien n'a été clické avant, on sort
            if (NomElementTreeViewAAfficher == null) return;


            //On affiche des chose différentes en fonction du dernier objet de l'arbre clické
            switch (NomElementTreeViewAAfficher)
            {
                case "Articles":
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

                    //Entrée: le dernier objet clické est un article spécifique
                    //  => on affiche uniquement les informations de cet article
                    if (Regex.IsMatch(NomElementTreeViewAAfficher, "^Article_.*$"))
                    {
                        string RefArticle = Regex.Match(NomElementTreeViewAAfficher, "^" + PrefixeArticle + "(.*)$").Groups[1].Value;

                        if(!Articles.ContainsKey(RefArticle))
                        {
                            NomDernierElementTreeViewClicke = null;
                            return;
                        }

                        //On affiche dans la ListView uniquement l'article sur lequel on à cliqué
                        AfficherArticlesListView(new Dictionary<string, Article>() {
                            {
                                RefArticle, Articles[RefArticle]
                            }
                        });

                        break;
                    }

                    //Entrée: le dernier objet clické est une marque spécifique
                    //  => on affiche uniquement les articles correspondants
                    if (Regex.IsMatch(NomElementTreeViewAAfficher, "^Marque_.*$"))
                    {
                        string RefMarque = Regex.Match(NomElementTreeViewAAfficher, "^" + PrefixeMarque + "(.*)$").Groups[1].Value;

                        if (!Marques.ContainsKey(RefMarque))
                        {
                            NomDernierElementTreeViewClicke = null;
                            return;
                        }

                        //On affiche dans la ListView uniquement les articles de la marque sur laquelle on à cliqué
                        AfficherArticlesListView(Marques[RefMarque].Articles);

                        break;
                    }


                    //Entrée: le dernier objet clické est une sous-famille spécifique
                    //  => on affiche uniquement les articles correspondants
                    if (Regex.IsMatch(NomElementTreeViewAAfficher, "^SousFamille_.*$"))
                    {
                        string RefSousFamille = Regex.Match(NomElementTreeViewAAfficher, "^" + PrefixeSousFamille + "(.*)$").Groups[1].Value;

                        if (!SousFamilles.ContainsKey(RefSousFamille))
                        {
                            NomDernierElementTreeViewClicke = null;
                            return;
                        }


                        //On affiche dans la ListView uniquement les articles de la sous-famille sur laquelle on à cliqué
                        AfficherArticlesListView(SousFamilles[RefSousFamille].Articles);

                        break;
                    }

                    
                    //Entrée: le dernier objet clické est une famille spécifique
                    //  => on affiche uniquement les sous-familles correspondantes
                    if (Regex.IsMatch(NomElementTreeViewAAfficher, "^Famille_.*$"))
                    {
                        string RefFamille = Regex.Match(NomElementTreeViewAAfficher, "^" + PrefixeFamille + "(.*)$").Groups[1].Value;

                        if (!Familles.ContainsKey(RefFamille))
                        {
                            NomDernierElementTreeViewClicke = null;
                            return;
                        }


                        //On affiche dans la ListView toutes les sous-familles de la famille
                        AfficherSousFamillesListView(Familles[RefFamille].SousFamilles);


                        //On affiche dans la ListView uniquement les articles de la famille sur laquelle on à cliqué
                        /*foreach (SousFamille SousFamille in Familles[RefFamille].SousFamilles.Values)
                        {
                            AfficherArticlesListView(SousFamille.Articles);
                        }*/

                        break;
                    }

                    break;
            }

            //Entrée : L'utilisateur avait clické sur une colonne de la ListView
            //      => On trie les nouvelles valeurs sur la même colonne
            if(NomDerniereColonneListViewClickee != null)
            {
                TrierListViewSurColonne(NomDerniereColonneListViewClickee);
            }
        }

    
        /// <summary>
        /// Méthode qui ajoute des Articles à la TreeView
        /// </summary>
        /// <param name="Articles">Le dictionnaire des Articles à afficher</param>
        private void AfficherArticlesListView(Dictionary<string, Article> Articles)
        {
            //Le préfixe à utiliser dans la clé des colonnes de la ListView
            string Prefixe = PrefixeArticle;
            
            //On ajoute les colonnes dans la ListView si elles n'existent pas déjà
            if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Description")) ListView.Columns.Add(Prefixe + "Description", "Description", 200);
            if (!ListView.Columns.ContainsKey(Prefixe + "Famille")) ListView.Columns.Add(Prefixe + "Famille", "Famille", 100);
            if (!ListView.Columns.ContainsKey(Prefixe + "Sous-famille")) ListView.Columns.Add(Prefixe + "Sous-famille", "Sous-famille", 100);
            if (!ListView.Columns.ContainsKey(Prefixe + "Marque")) ListView.Columns.Add(Prefixe + "Marque", "Marque", 100);
            //if (!ListView.Columns.ContainsKey(Prefixe + "Prix")) ListView.Columns.Add(Prefixe + "Prix", "Prix", 50);
            if (!ListView.Columns.ContainsKey(Prefixe + "Quantité")) ListView.Columns.Add(Prefixe + "Quantité", "Quantité", 60);
            
            
            //On sort s'il n'y a pas d'articles
            if (Articles == null || Articles.Count == 0) return;

            ListViewItem Ligne;

            //On ajoute tous les articles dans la ListView
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
                Ligne.Name = Prefixe + Article.RefArticle;
                ListView.Items.Add(Ligne);
            }
        }


        /// <summary>
        /// Méthode qui ajoute des Marques à la TreeView
        /// </summary>
        /// <param name="Marques">Le dictionnaire des Marques à afficher</param>
        private void AfficherMarquesListView(Dictionary<string, Marque> Marques)
        {
            //Le préfixe à utiliser dans la clé des colonnes de la ListView
            string Prefixe = PrefixeMarque;


            //On ajoute les colonnes dans la ListView si elles n'existent pas déjà
            //if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Nom")) ListView.Columns.Add(Prefixe + "Nom", "Nom", 300);

            //On sort s'il n'y a pas de marques
            if (Marques == null || Marques.Count == 0) return;

            ListViewItem Ligne;

            //On ajoute toutes les marques dans la ListView
            foreach (Marque Marque in Marques.Values)
            {
                string[] Valeurs = {
                    //Marque.RefMarque.ToString(),
                    Marque.Nom
                };

                Ligne = new ListViewItem(Valeurs);
                Ligne.Name = Prefixe + Marque.RefMarque;
                ListView.Items.Add(Ligne);
            }
        }

        private void AfficherFamillesListView(Dictionary<string, Famille> Familles)
        {
            //Le préfixe à utiliser dans la clé des colonnes de la ListView
            string Prefixe = PrefixeFamille;

            //On ajoute les colonnes dans la ListView si elles n'existent pas déjà
            //if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Nom")) ListView.Columns.Add(Prefixe + "Nom", "Nom", 300);

            //On sort s'il n'y a pas de familles
            if (Familles == null || Familles.Count == 0) return;

            ListViewItem Ligne;

            //On ajoute toutes les familles dans la ListView
            foreach (Famille Famille in Familles.Values)
            {
                string[] Valeurs = {
                    //Famille.RefFamille.ToString(),
                    Famille.Nom                    
                };

                Ligne = new ListViewItem(Valeurs);
                Ligne.Name = Prefixe + Famille.RefFamille;
                ListView.Items.Add(Ligne);
            }
        }


        private void AfficherSousFamillesListView(Dictionary<string, SousFamille> SousFamilles)
        {
            //Le préfixe à utiliser dans la clé des colonnes de la ListView
            string Prefixe = PrefixeSousFamille;

            //On ajoute les colonnes dans la ListView si elles n'existent pas déjà
            //if (!ListView.Columns.ContainsKey(Prefixe + "Référence")) ListView.Columns.Add(Prefixe + "Référence", "Référence", 80);
            if (!ListView.Columns.ContainsKey(Prefixe + "Nom")) ListView.Columns.Add(Prefixe + "Nom", "Nom", 300);

            //On sort s'il n'y a pas de sous-familles
            if (SousFamilles == null || SousFamilles.Count == 0) return;

            ListViewItem Ligne;

            //On ajoute toutes les sous-familles dans la ListView
            foreach (SousFamille SousFamille in SousFamilles.Values)
            {
                string[] Valeurs = {
                    //SousFamille.RefSousFamille.ToString(),
                    SousFamille.Nom
                };

                Ligne = new ListViewItem(Valeurs);
                Ligne.Name = Prefixe + SousFamille.RefSousFamille;
                ListView.Items.Add(Ligne);
            }
        }


        /// <summary>
        /// Méthode qui permet de trier la ListView sur une colonne. 
        /// Elle groupe également les objets suivant leur type et la colonne.
        /// </summary>
        /// <param name="NomColonneATrier">Le nom de la colonne de la ListView qu'il faut trier</param>
        private void TrierListViewSurColonne(string NomColonneATrier)
        {
            //On stocke la colonne comme étant la dernière qui a été clickée.
            NomDerniereColonneListViewClickee = NomColonneATrier;

            //On récupère la colonne qui correspond au nom
            ColumnHeader LaColonne = ListView.Columns[NomColonneATrier];
            //S'il n'y en a pas, on sort
            if (LaColonne == null) 
                return;

            //On supprime tous les groupes existants
            ListView.Groups.Clear();

            //Dictionnaire des Groupes (permet de vérifier s'il existe déjà un groupe avec un nom)
            Dictionary<string, ListViewGroup> LesGroupes = new Dictionary<string, ListViewGroup>();

            //On trie la ListView par ordre alphabétique sur la colonne clickée
            ListView.ListViewItemSorter = new ComparateurListViewItem(LaColonne.Index);

            string NomObjectClicke = LaColonne.Name;
            string NomGroupe = "";
            ListViewGroup Groupe;


            //Liste des expressions régulière des noms de colonne possibles
            Regex ExpressionReguliereFamille = new Regex("^" + PrefixeFamille + "(.*)$");
            Regex ExpressionReguliereMarque = new Regex("^" + PrefixeMarque + "(.*)$");
            Regex ExpressionReguliereSousFamille = new Regex("^" + PrefixeSousFamille + "(.*)$");
            Regex ExpressionReguliereArticle = new Regex("^" + PrefixeArticle + "(.*)$");


            //On parcours tous les objets de la ListView
            foreach (ListViewItem Ligne in ListView.Items)
            {
                //Entrée : L'utilisateur à clické sur une colonne lorsque des familles, des sous-familles ou des marques  sont affichées
                //      => On groupe les objets par la première lettre du "Nom"
                if (ExpressionReguliereSousFamille.IsMatch(NomObjectClicke) ||
                     ExpressionReguliereFamille.IsMatch(NomObjectClicke) ||
                     ExpressionReguliereMarque.IsMatch(NomObjectClicke)
                    )
                {

                    //On récupère la première lettre du nom et on la met en majuscule
                    NomGroupe = Ligne.SubItems[0].Text[0].ToString().ToUpper();

                }
                //Entrée : L'utilisateur à clické sur une colonne lorsque des Articles sont affichés
                else if (ExpressionReguliereArticle.IsMatch(NomObjectClicke))
                {
                    //On stocke le nom de la colonne comme étant le premier groupe de 
                    //capture de l'expression régulière "^Article_(.*)$"
                    string NomColonne = ExpressionReguliereArticle.Match(NomObjectClicke).Groups[1].Value;


                    //On regarde à quoi correspond le nom de la colonne
                    switch (NomColonne)
                    {
                        case "Description":
                            //On récupère la première lettre de la description et on la met en majuscule
                            NomGroupe = Ligne.SubItems[1].Text[0].ToString().ToUpper();
                            break;


                        case "Famille":
                            //Le nom du groupe est le nom de la famille
                            NomGroupe = Ligne.SubItems[2].Text;
                            break;


                        case "Sous-famille":
                            //Le nom du groupe est le nom de la sou-famille
                            NomGroupe = Ligne.SubItems[3].Text;
                            break;


                        case "Marque":
                            //Le nom du groupe est le nom de la marque
                            NomGroupe = Ligne.SubItems[4].Text;
                            break;

                        default:
                            //Sécurité si le clic n'a pas été effectué sur une colonne dont le nom
                            //correspond à l'un des choix précédent
                            NomGroupe = null;
                            break;
                    }
                }
                //Entrée : Le contenu de la ListView n'est ni des Articles, ni des Marques, ni des Familles, ni des SousFamilles (cela ne devrait pas arrivé)
                //      => On fait en sorte de sortir
                else
                {
                    NomGroupe = null;
                }

                //Sécurité si le clic n'a pas été effectué sur une colonne dont le nom
                //correspond à l'un des choix précédent => on met fin au traitement
                if (NomGroupe == null) return;

                //Entrée : il n'existe pas de groupe avec le nom défini précédemment
                //      => On créé le groupe puis on l'ajoute au dictionnaire et à la ListView
                if (!LesGroupes.ContainsKey(NomGroupe))
                {
                    Groupe = new ListViewGroup(NomGroupe);
                    ListView.Groups.Add(Groupe);
                    LesGroupes[NomGroupe] = Groupe;
                }
                //Sinon on utilise le groupe déjà existant dans le dictionnaire
                else
                {
                    Groupe = LesGroupes[NomGroupe];
                }

                //On affecte le groupe au ListViewItem
                Ligne.Group = Groupe;
            }
        }

        private void ListView_MouseDoubleClick(object Emetteur, MouseEventArgs Evenement)
        {
            OuvrirFenetreCreerModifier();
        }



        private void ListView_KeyDown(object Emetteur, KeyEventArgs Evenement)
        {
            if (Evenement.KeyCode == Keys.Enter || Evenement.KeyCode == Keys.Space)
            {
                OuvrirFenetreCreerModifier();
                return;
            }

            if(Evenement.KeyCode == Keys.Delete || Evenement.KeyCode == Keys.EraseEof)
            {
                SupprimerElement();
            }
        }

        public void SupprimerElement()
        {
            if (ListView.Columns == null || ListView.Columns.Count == 0 || ListView.SelectedItems == null || ListView.SelectedItems.Count == 0) return;


            //Liste des expressions régulière des noms de colonne possibles
            Regex ExpressionReguliereFamille = new Regex("^" + PrefixeFamille + "(.*)$");
            Regex ExpressionReguliereMarque = new Regex("^" + PrefixeMarque + "(.*)$");
            Regex ExpressionReguliereSousFamille = new Regex("^" + PrefixeSousFamille + "(.*)$");
            Regex ExpressionReguliereArticle = new Regex("^" + PrefixeArticle + "(.*)$");


            Dictionary<string, Article> ArticlesASupprimer = new Dictionary<string, Article>();
            Dictionary<string, SousFamille> SousFamillesASupprimer = new Dictionary<string, SousFamille>();


            //On parcours tous les objets de la ListView qui sont séléctionnés
            foreach (ListViewItem ObjetSelectionne in ListView.SelectedItems) {

                //Si l'élément séléctionné est un article, on le supprime
                if (ExpressionReguliereArticle.IsMatch(ListView.Columns[0].Name))
                {
                    ArticlesASupprimer[ObjetSelectionne.Text] = Articles[ObjetSelectionne.Text];
                }
                //Si l'element séléctionné est une marque, on la supprime
                else if (ExpressionReguliereMarque.IsMatch(ListView.Columns[0].Name))
                {
                    string RefMarque = ExpressionReguliereMarque.Match(ObjetSelectionne.Name).Groups[1].Value;
                    if (MarqueDAO.Supprimer(Marques[RefMarque]))
                    {
                        Marques.Remove(RefMarque);
                    }
                }
                //Si l'élément séléctionné est une famille, on la supprime
                else if (ExpressionReguliereFamille.IsMatch(ListView.Columns[0].Name))
                {
                    string RefFamille = ExpressionReguliereFamille.Match(ObjetSelectionne.Name).Groups[1].Value;

                    if (FamilleDAO.Supprimer(Familles[RefFamille]))
                    {
                        //On supprime toutes les sous-familles de la famille
                        foreach (SousFamille SousFamille in Familles[RefFamille].SousFamilles.Values)
                        {
                            if (!SousFamillesASupprimer.ContainsKey(SousFamille.RefSousFamille.ToString()))
                            {
                                SousFamillesASupprimer[SousFamille.RefSousFamille.ToString()] = SousFamille;
                            }

                        };
                    }
                }
                //Si l'élément séléctionné est une sous-famille, on la supprime
                else if (ExpressionReguliereSousFamille.IsMatch(ListView.Columns[0].Name))
                {
                    string RefSousFamille = ExpressionReguliereSousFamille.Match(ObjetSelectionne.Name).Groups[1].Value;
                    SousFamillesASupprimer[RefSousFamille] = SousFamilles[RefSousFamille];
                }
            }


            //On supprime certaines sous familles et tous leurs articles
            foreach (SousFamille SousFamille in SousFamillesASupprimer.Values)
            {
                if (SousFamilleDAO.Supprimer(SousFamilles[SousFamille.RefSousFamille.ToString()]))
                {
                    //On supprime tous les articles de la sous-famille
                    foreach (Article Article in SousFamille.Articles.Values)
                    {
                        if (!ArticlesASupprimer.ContainsKey(Article.RefArticle))
                        {
                            ArticlesASupprimer[Article.RefArticle] = Article;
                        }
                    }

                    SousFamilles.Remove(SousFamille.RefSousFamille.ToString());
                }
            }

            //On supprime certains articles
            foreach (Article Article in ArticlesASupprimer.Values)
            {
                if (ArticleDAO.Supprimer(Article))
                {
                    Articles.Remove(Article.RefArticle);
                }
            }

            ActualiserDonnees();
            ActualiserListView(NomDernierElementTreeViewClicke);
        }

        
        /// <summary>
        /// Ouvre le menu de création/modification d'élément (Article, Marque, Famille ou Sous-Famille).
        /// </summary>
        private void OuvrirFenetreCreerModifier()
        {
            if (ListView.Columns == null || ListView.Columns.Count == 0) return;

            Form FormulaireAjoutModification = null;
            bool Create = true;
            ListViewItem ObjetSelectionne = null;

            // Si un élément de la ListView est sélectionné, alors on cherche à le modifier
            if (ListView.SelectedItems != null && ListView.SelectedItems.Count == 1)
            {
                Create = false;
                ObjetSelectionne = ListView.SelectedItems[0];
            }


            //Liste des expressions régulière des noms de colonne possibles
            Regex ExpressionReguliereFamille = new Regex("^" + PrefixeFamille + "(.*)$");
            Regex ExpressionReguliereMarque = new Regex("^" + PrefixeMarque + "(.*)$");
            Regex ExpressionReguliereSousFamille = new Regex("^" + PrefixeSousFamille + "(.*)$");
            Regex ExpressionReguliereArticle = new Regex("^" + PrefixeArticle + "(.*)$");

            // On veut créer / modifier un article
            if ( ExpressionReguliereArticle.IsMatch(ListView.Columns[0].Name) )
            {
                if (Create)
                {
                    FormulaireAjoutModification = new ArticleForm(Connexion, Articles, Marques, SousFamilles, Familles, null);
                }
                else
                {
                    FormulaireAjoutModification = new ArticleForm(Connexion, Articles, Marques, SousFamilles, Familles, Articles[ObjetSelectionne.Text]);
                }
            }
            // On veut créé / modifier une marque
            else if (ExpressionReguliereMarque.IsMatch(ListView.Columns[0].Name))
            {
                if (Create)
                {
                    FormulaireAjoutModification = new MarqueForm(Connexion, null);
                }
                else
                {
                    string RefMarque = ExpressionReguliereMarque.Match(ObjetSelectionne.Name).Groups[1].Value;
                    FormulaireAjoutModification = new MarqueForm(Connexion, Marques[RefMarque]);
                }
            }
            // On veur créer / modifier une famille
            else if (ExpressionReguliereFamille.IsMatch(ListView.Columns[0].Name))
            {
                if (Create)
                {
                    FormulaireAjoutModification = new FamilleForm(Connexion, null);
                }
                else
                {
                    string RefFamille = ExpressionReguliereFamille.Match(ObjetSelectionne.Name).Groups[1].Value;
                    FormulaireAjoutModification = new FamilleForm(Connexion, Familles[RefFamille]);
                }
            }
            // On veut créer / modifier une sous-famille
            else if (ExpressionReguliereSousFamille.IsMatch(ListView.Columns[0].Name))
            {
                Famille LaFamille = null;

                //On récupère la famille dans laquelle on se trouve actuellement
                if (ExpressionReguliereFamille.IsMatch(NomDernierElementTreeViewClicke)){
                    string RefFamille = ExpressionReguliereFamille.Match(NomDernierElementTreeViewClicke).Groups[1].Value;
                    LaFamille = Familles[RefFamille];
                }

                if (Create)
                {
                    FormulaireAjoutModification = new SousFamilleForm(Connexion, Familles, LaFamille, null);
                }
                else
                {
                    string RefSousFamille = ExpressionReguliereSousFamille.Match(ObjetSelectionne.Name).Groups[1].Value;
                    FormulaireAjoutModification = new SousFamilleForm(Connexion, Familles, LaFamille, SousFamilles[RefSousFamille]);
                }
            }

            /*// On veut créer/modifier une famille
            else if (ListViewDisplay == "FAMILLES")
            {
                if (Create)
                {
                    Form = new FamilleForm();
                }
                else
                {
                    Form = new FamilleForm(SelectedItem);
                }
            }
            // On veut créer/modifier une marque
            else if (ListViewDisplay == "MARQUES")
            {
                if (Create)
                {
                    Form = new MarqueForm();
                }
                else
                {
                    Form = new MarqueForm(SelectedItem);
                }
            }
            // On veut créer/modifier une sous-famille
            else if (ListViewDisplay == "SOUSFAMILLES")
            {
                if (Create)
                {
                    Form = new SousFamilleForm();
                }
                else
                {
                    Form = new SousFamilleForm(ListViewValue, SelectedItem);
                }
            }*/


            // Affiche la fenêtre si on en a créé une
            if (FormulaireAjoutModification != null)
            {
                FormulaireAjoutModification.ShowDialog();

                ActualiserDonnees();
                ActualiserListView(NomDernierElementTreeViewClicke);
            }
        }



        private void ListView_MouseDown(object Emetteur, MouseEventArgs Evenement)
        {
            //On ne gère que le clic droit
            if (Evenement.Button != MouseButtons.Right || ListView.Columns == null || ListView.Columns.Count == 0) return;

            //Liste des expressions régulière des noms de colonne possibles
            Regex ExpressionReguliereFamille = new Regex("^" + PrefixeFamille + "(.*)$");
            Regex ExpressionReguliereMarque = new Regex("^" + PrefixeMarque + "(.*)$");
            Regex ExpressionReguliereSousFamille = new Regex("^" + PrefixeSousFamille + "(.*)$");
            Regex ExpressionReguliereArticle = new Regex("^" + PrefixeArticle + "(.*)$");

            //On récupère la position du clic
            Point PointDuClick = PointToClient(new Point(MousePosition.X, MousePosition.Y));
            //On récupère l'élément qui a été cliqué
            ListViewItem ObjetSelectionne = ListView.GetItemAt(Evenement.X, Evenement.Y);

            string NomElement = "";
            
            ListView.SelectedItems.Clear();
            if (ObjetSelectionne != null)
            {
                ObjetSelectionne.Selected = true;
                NomElement = ObjetSelectionne.Text.Length > 30 ? ObjetSelectionne.Text.Substring(0, 30) + "..." : ObjetSelectionne.Text;
            }

            bool DesactiverModifSuppr = false;
            MenuItem Ajouter = new MenuItem("Ajouter");
            MenuItem Modifier = new MenuItem("Modifier");
            MenuItem Supprimer = new MenuItem("Supprimer");



            if (ExpressionReguliereArticle.IsMatch(ListView.Columns[0].Name))
            {
                Ajouter.Text = "Ajouter un article";

                if (ObjetSelectionne == null)
                {
                    DesactiverModifSuppr = true;
                    goto AfficherMenu;
                }

                Modifier.Text = "Modifier l'article \"" + NomElement + "\"";
                Supprimer.Text = "Supprimer l'article \"" + NomElement + "\"";

            }
            else if (ExpressionReguliereMarque.IsMatch(ListView.Columns[0].Name))
            {

                Ajouter.Text = "Ajouter une marque";

                if (ObjetSelectionne == null)
                {
                    DesactiverModifSuppr = true;
                    goto AfficherMenu;
                }

                Modifier.Text = "Modifier la marque \"" + NomElement + "\"";
                Supprimer.Text = "Supprimer la marque \"" + NomElement + "\"";
            }
            else if (ExpressionReguliereSousFamille.IsMatch(ListView.Columns[0].Name))
            {

                Ajouter.Text = "Ajouter une sous-famille";

                if (ObjetSelectionne == null)
                {
                    DesactiverModifSuppr = true;
                    goto AfficherMenu;
                }

                Modifier.Text = "Modifier la sous-famille \"" + NomElement + "\"";
                Supprimer.Text = "Supprimer la sous-famille \"" + NomElement + "\"";
            }
            else if (ExpressionReguliereFamille.IsMatch(ListView.Columns[0].Name))
            {

                Ajouter.Text = "Ajouter une famille";

                if (ObjetSelectionne == null)
                {
                    DesactiverModifSuppr = true;
                    goto AfficherMenu;
                }

                Modifier.Text = "Modifier la famille \"" + NomElement + "\"";
                Supprimer.Text = "Supprimer la famille \"" + NomElement + "\"";
            }


        AfficherMenu:

            Ajouter.Click += (sender, e) =>
            {
                ListView.SelectedItems.Clear();
                OuvrirFenetreCreerModifier();
            };

            
            Modifier.Click += (sender, e) =>
            {
                OuvrirFenetreCreerModifier();
            };

            Supprimer.Click += (sender, e) =>
            {
                SupprimerElement();
            };

            if (DesactiverModifSuppr)
            {
                Modifier.Enabled = false;
                Supprimer.Enabled = false;
            }
            
            ContextMenu ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(Ajouter);
            ContextMenu.MenuItems.Add(Modifier);
            ContextMenu.MenuItems.Add(Supprimer);
            ContextMenu.Show(this, PointDuClick);
            
        }


        /// <summary>
        /// Clase qui implémente le tri par ordre alphabétique sur une colonne du ListView
        /// </summary>
        class ComparateurListViewItem : IComparer
        {
            /// <summary>
            /// L'indice de la colonne que l'on souhaite trier.
            /// </summary>
            private int IndiceColonne;

            /// <summary>
            /// Constructeur par défaut
            /// </summary>
            public ComparateurListViewItem()
            {
                IndiceColonne = 0;
            }

            /// <summary>
            /// Constructeur.
            /// </summary>
            /// <param name="IndiceColonne">L'indice de la colonne que l'on souhaite trier.</param>
            public ComparateurListViewItem(int IndiceColonne)
            {
                this.IndiceColonne = IndiceColonne;
            }

            /// <summary>
            /// Méthode qui permet de comparer deux objets ListViewItem.
            /// </summary>
            /// <param name="Objet1">Le premier objet à comparer.</param>
            /// <param name="Objet2">Le second objet à comparer</param>
            /// <returns>
            /// Un entier qui indique la relation lexicale entre les deux comparateurs.
            /// Inférieure à zéro si la chaîne de caractère de Objet1 (ListViewItem) précède Objet2 (ListViewItem) dans l'ordre de tri. 
            /// Zéro si la chaîne de caractère de Objet1 (ListViewItem) précède Objet2 (ListViewItem) dans l'ordre de tri. 
            /// Supérieure à zéro si la chaîne de caractère de Objet1 (ListViewItem) suit Objet2 (ListViewItem) dans l'ordre de tri.
            /// </returns>
            public int Compare(object Objet1, object Objet2)
            {
                return string.Compare(
                    ((ListViewItem)Objet1).SubItems[IndiceColonne].Text, 
                    ((ListViewItem)Objet2).SubItems[IndiceColonne].Text
                );
            }
        }
    }
} 

