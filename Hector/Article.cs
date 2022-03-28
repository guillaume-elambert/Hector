using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class Article
    {
        public string RefArticle { get; set; }
        public string Description { get; set; }
        public Marque Marque { get; set; }
        public SousFamille SousFamille { get; set; }
        public float Prix { get; set; }
        public int Quantite { get; set; }


        public Article(string RefArticle, string Description, Marque Marque, SousFamille SousFamille, float Prix, int Quantite)
        {
            this.RefArticle = RefArticle;
            this.Description = Description;
            this.Marque = Marque;
            this.SousFamille = SousFamille;
            this.Prix = Prix;
            this.Quantite = Quantite;
        }
    }
}
