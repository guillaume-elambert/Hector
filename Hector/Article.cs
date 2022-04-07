namespace Hector
{
    /// <summary>
    /// Classe des articles.
    /// </summary>
    internal class Article
    {
        /// <summary>
        /// La référence de l'article
        /// </summary>
        public string RefArticle { get; set; }
        /// <summary>
        /// La description de l'article
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// La marque de l'article
        /// </summary>
        public Marque Marque { get; set; }
        /// <summary>
        /// La sous-famille de l'article
        /// </summary>
        public SousFamille SousFamille { get; set; }
        /// <summary>
        /// Le prix de l'article
        /// </summary>
        public float Prix { get; set; }
        /// <summary>
        /// La quantité de l'article
        /// </summary>
        public int Quantite { get; set; }


        /// <summary>
        /// Constructeur
        /// </summary>
        public Article()
        {
            RefArticle = null;
            Quantite = 0;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefArticle">La référence de l'article</param>
        /// <param name="Description">La description de l'article</param>
        /// <param name="Marque">La marque de l'article</param>
        /// <param name="SousFamille">La sous-famille de l'article</param>
        /// <param name="Prix">Le prix de l'article</param>
        /// <param name="Quantite">La quentite de l'article</param>
        public Article(string RefArticle, string Description, Marque Marque, SousFamille SousFamille, float Prix, int Quantite)
        {
            this.RefArticle = RefArticle;
            this.Description = Description;
            this.Marque = Marque;
            this.SousFamille = SousFamille;
            this.Prix = Prix;
            this.Quantite = Quantite;
        }


        /// <summary>
        /// Méthode qui permet de retourner une chaîne de caractères qui représente l'objet
        /// </summary>
        /// <returns>Une chaîne de caractères qui représente l'objet</returns>
        public override string ToString()
        {
            return RefArticle + "; " + Description + "; Marque : {" + Marque + "}; Sous-famille : {" + SousFamille + "}; " + Prix + "; " + Quantite;
        }


        /// <summary>
        /// Méthode qui permet de retourner une chaîne de caractères qui représente l'objet sous forme de tableau CSV
        /// </summary>
        /// <returns>Une chaîne de caractères qui représente l'objet sous forme de tableau CSV</returns>
        public string ToCSV()
        {
            return Description + ";" + RefArticle + ";" + Marque + ";" + SousFamille.Famille + ";" + SousFamille.Nom + ";" + Prix;
        }
    }
}
