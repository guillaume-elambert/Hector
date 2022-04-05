using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
    internal class ArticleDAO : DAO<Article>
    {
        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        private ConnexionBDD Connexion;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion à la base de données</param>
        public ArticleDAO(ConnexionBDD Connexion)
        {
            this.Connexion = Connexion;
        }


        /// <summary>
        /// Méthode d'insertion d'un objet Article dans la base de données.
        /// </summary>
        /// <param name="Article">L'article à insérer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Inserer(Article Article)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refArticle", Article.RefArticle),
                new SQLiteParameter("@description", Article.Description),
                new SQLiteParameter("@refSousFamille", Article.SousFamille.RefSousFamille),
                new SQLiteParameter("@refMarque", Article.Marque.RefMarque),
                new SQLiteParameter("@prix", Article.Prix),
                new SQLiteParameter("@quantite", Article.Quantite)
            };

            string Commande = "INSERT INTO Articles " +
                "(RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) VALUES " +
                "(@refArticle, @description, @refSousFamille, @refMarque, @prix, @quantite);";
            return Connexion.ExecuterCommande(Commande, Parametres) != -1;

        }

        /// <summary>
        /// Méthode d'insertion d'une liste d'objets Article dans la base de données.
        /// </summary>
        /// <param name="ListeArticles">La liste des articles à insérer</param>
        /// <returns>true si toutes les insertions réussies, false sinon</returns>
        public bool Inserer(List<Article> ListeArticles)
        {
            bool ARetourner = true;

            foreach (Article Article in ListeArticles)
            {
                ARetourner &= Inserer(Article);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode de modification d'un Article en base de données.
        /// </summary>
        /// <param name="Article">L'article à modifier</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Modifier(Article Article)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refArticle", Article.RefArticle),
                new SQLiteParameter("@description", Article.Description),
                new SQLiteParameter("@refSousFamille", Article.SousFamille.RefSousFamille),
                new SQLiteParameter("@refMarque", Article.Marque.RefMarque),
                new SQLiteParameter("@prix", Article.Prix),
                new SQLiteParameter("@quantite", Article.Quantite)
            };

            string Commande = "UPDATE Articles SET " +
                "Description = @description, " +
                "RefSousFamille = @refSousFamille," +
                "RefMarque = @refMarque," +
                "PrixHT = @prix," +
                "Quantite = @quantite" +
                "WHERE RefArticle = @refArticle;";

            return Connexion.ExecuterCommande(Commande, Parametres) != -1;
        }


        /// <summary>
        /// Méthode de modification une liste d'Articles en base de données.
        /// </summary>
        /// <param name="ListeArticles">La liste des articles à modifier.</param>
        /// <returns>true si toutes les modifications réussies, false sinon</returns>
        public bool Modifier(List<Article> ListeArticles)
        {
            bool ARetourner = true;

            foreach (Article Article in ListeArticles)
            {
                ARetourner &= Modifier(Article);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour obtenir un Article depuis la base de données.
        /// </summary>
        /// <param name="Article">L'article à chercher (à partir de son id)</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Obtenir(Article Article)
        {
            SousFamilleDAO SousFamilleDAO = new SousFamilleDAO(Connexion);
            MarqueDAO MarqueDAO = new MarqueDAO(Connexion);

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refArticle", Article.RefArticle)
            };

            string Commande = "SELECT Description, RefSousFamille, RefMarque, PrixHT, Quantite FROM Articles WHERE RefArticle = @refArticle;";
            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;

            LigneSQLite Resultat = ResultatSQLite[0];

            Article.Description = Resultat.Attribut<string>(0);
            Article.Prix = Resultat.Attribut<float>(3);
            Article.Quantite = Resultat.Attribut<int>(4);
            Article.Marque = new Marque(Resultat.Attribut<int>(2));
            Article.SousFamille = new SousFamille(Resultat.Attribut<int>(1));

            SousFamilleDAO.Obtenir(Article.SousFamille);
            MarqueDAO.Obtenir(Article.Marque);
            return true;
        }


        /// <summary>
        /// Méthode pour obtenir une liste d'Articles depuis la base de données.
        /// </summary>
        /// <param name="ListeArticles">Les articles à chercher (à partir de leur id)</param>
        /// <returns>true si toutes les obtentions réussies, false sinon</returns>
        public bool Obtenir(List<Article> ListeArticles)
        {
            bool ARetourner = true;

            foreach (Article Article in ListeArticles)
            {
                ARetourner &= Obtenir(Article);
            }

            return ARetourner;
        }

        /// <summary>
        /// Méthode pour obtenir tous les d'Articles depuis la base de données.
        /// </summary>
        /// <returns>La liste des articles stockés en base de données</returns>
        public List<Article> ObtenirTout()
        {
            SousFamilleDAO SousFamilleDAO = new SousFamilleDAO(Connexion);
            MarqueDAO MarqueDAO = new MarqueDAO(Connexion);

            string Commande = "SELECT RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite FROM Articles;";
            
            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return null;

            List<Article> ListeArticles = new List<Article>();
            Dictionary<int, Marque> Marques = new Dictionary<int, Marque>();
            Dictionary<int, SousFamille> SousFamilles = new Dictionary<int, SousFamille>();
            Dictionary<int, Famille> Familles = new Dictionary<int, Famille>();

            int RefMarque, RefSousFamille, RefFamille;
            Marque LaMarque;
            SousFamille LaSousFamille;
            Famille LaFamille;
            Article Article;

            foreach (LigneSQLite Ligne in ResultatSQLite)
            {
                Article = new Article();
                Article.RefArticle = Ligne.Attribut<string>(0);
                Article.Description = Ligne.Attribut<string>(1);
                Article.Prix = Ligne.Attribut<float>(4);
                Article.Quantite = Ligne.Attribut<int>(5);
                
                RefMarque = Ligne.Attribut<int>(3);
                RefSousFamille = Ligne.Attribut<int>(2);



                //Si on a déjà cette marque on utilise celle existante sinon on la charge depuis la base de données
                if (Marques.ContainsKey(RefMarque))
                {
                    LaMarque = Marques[RefMarque];
                } else
                {
                    LaMarque = new Marque(RefMarque);
                    MarqueDAO.Obtenir(LaMarque);
                    Marques.Add(RefMarque, LaMarque);
                }
                
                Article.Marque = LaMarque;


                //Si on a déjà cette sous famille on utilise celle existante sinon on la charge depuis la base de données
                if (SousFamilles.ContainsKey(RefSousFamille))
                {
                    LaSousFamille = SousFamilles[RefSousFamille];
                } else
                {
                    LaSousFamille = new SousFamille(RefSousFamille);
                    SousFamilleDAO.Obtenir(LaSousFamille);
                    SousFamilles.Add(RefSousFamille, LaSousFamille);
                }

                Article.SousFamille = LaSousFamille;

                //On récupère la famille de la sous famille
                RefFamille = LaSousFamille.Famille.RefFamille;

                //Si la Famille avait déjà été chargée et créée on la récupère
                if (Familles.ContainsKey(RefFamille))
                {
                    LaFamille = Familles[RefFamille];
                    LaSousFamille.Famille = LaFamille;
                } else
                {
                    Familles.Add(RefFamille, LaSousFamille.Famille);
                }

                ListeArticles.Add(Article);
            };

            return ListeArticles;
        }


        /// <summary>
        /// Méthode de supression d'un Article en base de données.
        /// </summary>
        /// <param name="Article"></param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Supprimer(Article Article)
        {
            return true;
        }

        /// <summary>
        /// Méthode de supression d'une liste d'Articles en base de données.
        /// </summary>
        /// <param name="ListeArticles"></param>
        /// <returns>true si toutes les suppressions réussies, false sinon</returns>
        public bool Supprimer(List<Article> ListeArticles)
        {
            bool ARetourner = true;

            foreach (Article Article in ListeArticles)
            {
                ARetourner &= Supprimer(Article);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        /// <returns>true si réussi, false sinon</returns>
        public bool ViderTable()
        {
            string Commande = "DELETE FROM Articles;";
            return Connexion.ExecuterCommande(Commande) != -1;

        }
    }
}
