using System.Collections.Generic;

namespace Hector
{
    internal class SousFamille
    {
        public int RefSousFamille { get; set; }
        public Famille Famille { get; set; }
        public string Nom { get; set; }

        public List<Article> Articles { get; set; }


        public SousFamille() : this(-1, null, null) { }

        public SousFamille(int RefSousFamille) : this(RefSousFamille, null, null) { }

        public SousFamille(int RefSousFamille, Famille Famille, string Nom)
        {
            this.RefSousFamille = RefSousFamille;
            this.Famille = Famille;
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
            return Nom + "; Famille : {" + Famille + "}";
        }
    }
}
