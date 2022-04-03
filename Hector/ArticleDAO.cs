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
            if (ResultatSQLite == null) return false;

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

        public List<Article> ObtenirArticles()
        {
            SQLiteCommand Commande = Connexion.getConnexion().CreateCommand();
            Commande.CommandText = "SELECT * FROM Articles";
            List<Article> TousLesArticles = new List<Article>();

            SQLiteDataReader Resultat = Commande.ExecuteReader();

            while (Resultat.Read())
            {
                Article Article = new Article();
                Article.Description = (string)Resultat["Description"];
                Article.Marque = (Marque)Resultat["Marque"];
                Article.SousFamille = (SousFamille)Resultat["SousFamille"];
                Article.Prix = (float)Resultat["Prix"];
                Article.Quantite = (int)Resultat["Quantite"];
                TousLesArticles.Add(Article);
            }
            return TousLesArticles;

        }
    }
}
