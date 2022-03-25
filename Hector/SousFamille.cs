using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class SousFamille
    {
        private int RefSousFamille { get; set; }
        private Famille Famille { get; set; }
        private string Nom { get; set; }

        public SousFamille(int RefSousFamille, Famille famille, string Nom)
        {
            this.RefSousFamille = RefSousFamille;
            this.Famille = Famille;
            this.Nom = Nom;
        }
    }
}
