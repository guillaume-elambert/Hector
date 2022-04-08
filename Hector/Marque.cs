using System.Collections.Generic;

namespace Hector
{
    /// <summary>
    /// Classe des marques.
    /// </summary>
    internal class Marque
    {
        /// <summary>
        /// Référence de la marque.
        /// </summary>
        public int RefMarque { get; set; }
        /// <summary>
        /// Nom de la marque.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Liste des articles de la marque.
        /// </summary>
        public Dictionary<string, Article> Articles { get; set; }



        /// <summary>
        /// Constructeur
        /// </summary>
        public Marque() : this(-1, null) { }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefMarque">La référence de la marque</param>
        public Marque(int RefMarque) : this(RefMarque, null) { }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefMarque">La référence de la marque.</param>
        /// <param name="Nom">Le nom de la marque.</param>
        public Marque(int RefMarque, string Nom)
        {
            this.RefMarque = RefMarque;
            this.Nom = Nom;
            Articles = new Dictionary<string, Article>();
        }


        /// <summary>
        /// Méthode qui permet d'jouter un article à la liste des articles de la marque.
        /// </summary>
        /// <param name="Article"></param>
        public void AjouterArticle(Article Article)
        {
            Articles[Article.RefArticle] = Article;
        }


        /// <summary>
        /// Méthode qui permet de retourner une chaîne de caractères qui représente l'objet
        /// </summary>
        /// <returns>Une chaîne de caractères qui représente l'objet</returns>
        public override string ToString()
        {
            return Nom;
        }
    }
}
