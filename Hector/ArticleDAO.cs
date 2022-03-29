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
        public void Inserer(Article Article)
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
            Connexion.ExecuterCommande(Commande, Parametres);

        }

        /// <summary>
        /// Méthode d'insertion d'une liste d'objets Article dans la base de données.
        /// </summary>
        /// <param name="ListeArticles">La liste des articles à insérer</param>
        public void Inserer(List<Article> ListeArticles)
        {
            foreach (Article Article in ListeArticles)
            {
                Inserer(Article);
            }
        }


        /// <summary>
        /// Méthode de modification d'un Article en base de données.
        /// </summary>
        /// <param name="Article">L'article à modifier</param>
        public void Modifier(Article Article)
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

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode de modification une liste d'Articles en base de données.
        /// </summary>
        /// <param name="ListeArticles">La liste des articles à modifier.</param>
        public void Modifier(List<Article> ListeArticles)
        {
            foreach (Article Article in ListeArticles)
            {
                Modifier(Article);
            }
        }


        /// <summary>
        /// Méthode pour obtenir un Article depuis la base de données.
        /// </summary>
        /// <param name="Article">L'article à chercher (à partir de son id)</param>
        public void Obtenir(Article Article)
        {
            SousFamilleDAO SousFamilleDAO = new SousFamilleDAO(Connexion);
            MarqueDAO MarqueDAO = new MarqueDAO(Connexion);

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refArticle", Article.RefArticle)
            };

            string Commande = "SELECT Description, RefSousFamille, RefMarque, PrixHT, Quantite FROM Articles WHERE RefArticle = @refArticle;";

            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];

            Article.Description = Resultat.Attribut<string>(0);
            Article.Prix = Resultat.Attribut<float>(3);
            Article.Quantite = Resultat.Attribut<int>(4);
            Article.Marque = new Marque(Resultat.Attribut<int>(2));
            Article.SousFamille = new SousFamille(Resultat.Attribut<int>(1));

            SousFamilleDAO.Obtenir(Article.SousFamille);
            MarqueDAO.Obtenir(Article.Marque);

            /*DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];

            Article.Description = Resultat.Field<string>("Description");
            Article.Prix = Resultat.Field<float>("PrixHT");
            Article.Quantite = Convert.ToInt32(Resultat["Quantite"]);
            Article.SousFamille = new SousFamille(Convert.ToInt32(Resultat["RefSousFamille"]));

            SousFamilleDAO.Obtenir(Article.SousFamille);*/
        }

        /// <summary>
        /// Méthode pour obtenir une liste d'Articles depuis la base de données.
        /// </summary>

        /// <param name="ListeArticles">Les articles à chercher (à partir de leur id)</param>
        public void Obtenir(List<Article> ListeArticles)
        {
            foreach (Article Article in ListeArticles)
            {
                Obtenir(Article);
            }
        }


        /// <summary>
        /// Méthode de supression d'un Article en base de données.
        /// </summary>
        /// <param name="Article"></param>
        public void Supprimer(Article Article)
        {
        }

        /// <summary>
        /// Méthode de supression d'une liste d'Articles en base de données.
        /// </summary>
        /// <param name="ListeArticles"></param>
        public void Supprimer(List<Article> ListeArticles)
        {
            foreach (Article Article in ListeArticles)
            {
                Supprimer(Article);
            }
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        public void ViderTable()
        {
            string Commande = "DELETE FROM Articles;";
            Connexion.ExecuterCommande(Commande);

        }
    }
}
