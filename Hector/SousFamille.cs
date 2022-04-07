using System.Collections.Generic;

namespace Hector
{
    /// <summary>
    /// Classe des sous-familles
    /// </summary>
    internal class SousFamille
    {
        /// <summary>
        /// Référence de la sous-famille
        /// </summary>
        public int RefSousFamille { get; set; }
        /// <summary>
        /// Famille de la sous-famille
        /// </summary>
        public Famille Famille { get; set; }
        /// <summary>
        /// Nom de la sous-famille
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Liste des articles de la sous-famille
        /// </summary>
        public Dictionary<string, Article> Articles { get; set; }



        /// <summary>
        /// Constructeur
        /// </summary>
        public SousFamille() : this(-1, null, null) { }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefSousFamille">La référence de la sous-famille</param>
        public SousFamille(int RefSousFamille) : this(RefSousFamille, null, null) { }


        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="RefSousFamille">La référence de la sous-famille</param>
        /// <param name="Famille">La famille de la sous-famille</param>
        /// <param name="Nom">Le nom de la sous-famille</param>
        public SousFamille(int RefSousFamille, Famille Famille, string Nom)
        {
            this.RefSousFamille = RefSousFamille;
            this.Famille = Famille;
            this.Nom = Nom;
            Articles = new Dictionary<string, Article>();
        }


        /// <summary>
        /// Méthode qui permet d'ajouter un article à la liste des articles de la sous-famille
        /// </summary>
        /// <param name="Article">L'article à ajouter</param>
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


        /// <summary>
        /// Méthode qui permet de retourner une chaîne de caractères qui représente l'objet
        /// </summary>
        /// <returns>Une chaîne de caractères qui représente l'objet</returns>
        public string ToDebugString()
        {
            return Nom + "; Famille : {" + Famille + "}";
        }
    }
}
