using System.Collections.Generic;

namespace Hector
{
    internal class Marque
    {
        public int RefMarque { get; set; }
        public string Nom { get; set; }

        public List<Article> Articles { get; set; }


        public Marque() : this(-1, null) { }

        public Marque(int RefMarque) : this(RefMarque, null) { }

        public Marque(int RefMarque, string Nom)
        {
            this.RefMarque = RefMarque;
            this.Nom = Nom;
            Articles = new List<Article>();
        }

        public void AjouterArticle(Article Article)
        {
            if (Articles.Contains(Article)) return;
            Articles.Add(Article);
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
