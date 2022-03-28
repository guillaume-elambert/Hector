using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refArticle", Article.RefArticle),
                new SQLiteParameter("@description", Article.Description),
                new SQLiteParameter("@refSousFamille", Article.SousFamille.RefSousFamille),
                new SQLiteParameter("@refMarque", Article.Marque.RefMarque),
                new SQLiteParameter("@prix", Article.Prix),
                new SQLiteParameter("@quantite", Article.Quantite)
            };

            string Commande = "INSERT INTO Articles " +
                "(RefArticle, Description, RefSousFamille, RefMarque, Prix, Quantite) VALUES " +
                "(@refArticle, @description, @refSousFamille, @refMarque, @prix, @quantite);";
            Connexion.ExecuterCommande(Commande, Parametres);

        }


        /// <summary>
        /// Méthode de modification d'un Article en base de données.
        /// </summary>
        /// <param name="Article">L'article à modifier</param>
        public void Modifier(Article Article)
        {
            SQLiteParameter[] Parametres = {
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
                "Prix = @prix," +
                "Quantite = @quantite" +
                "WHERE RefArticle = @refArticle;";

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode pour obtenir un Article depuis la base de données.
        /// </summary>
        /// <param name="Article">L'article à chercher (à partir de son id)</param>
        public void Obtenir(Article Article)
        {
            SousFamilleDAO SousFamilleDAO = new SousFamilleDAO(Connexion);
            MarqueDAO MarqueDAO = new MarqueDAO(Connexion);

            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refArticle", Article.RefArticle)
            };

            string Commande = "SELECT Description, RefSousFamille, RefMarque, Prix, Quantite FROM Articles WHERE RefArticle = @refArticle;";

            using (SQLiteDataReader Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres))
            {
                Article.Description = Resultat.GetString(1);
                Article.Prix = Resultat.GetFloat(4);
                Article.Quantite = Resultat.GetInt32(5);
                Article.SousFamille = new SousFamille(Resultat.GetInt32(2));
                SousFamilleDAO.Obtenir(Article.SousFamille);
            }


        }


        /// <summary>
        /// Méthode de supression d'un Article en base de données.
        /// </summary>
        /// <param name="Article"></param>
        public void Supprimer(Article Article)
        {
        }
    }
}
