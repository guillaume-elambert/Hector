using System;
using System.Collections.Generic;
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
            object[] Valeurs = { };
            string Commande = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, Prix, Quantite) VALUES (?, ?, ?, ?, ?, ?);";
            Connexion.ExecuterCommande(Commande);

        }


        /// <summary>
        /// Méthode de modification d'un Article en base de données.
        /// </summary>
        /// <param name="Article">L'article à modifier</param>
        public void Modifier(Article Article)
        {
        }


        /// <summary>
        /// Méthode pour obtenir un Article depuis la base de données.
        /// </summary>
        /// <param name="Article">L'article à chercher (à partir de son id)</param>
        public Article Obtenir(Article Article)
        {
            return null;
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
