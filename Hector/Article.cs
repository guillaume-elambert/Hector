using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class Article
    {
        private string RefArticle { get; set; }
        private string Description { get; set; }
        private Marque Marque { get; set; }
        private SousFamille SousFamille { get; set; }
        private float Prix { get; set; }
        private int Quantite { get; set; }


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
