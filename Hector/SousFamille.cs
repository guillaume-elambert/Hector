using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class SousFamille
    {
        public int RefSousFamille { get; set; }
        public Famille Famille { get; set; }
        public string Nom { get; set; }

        public SousFamille(int RefSousFamille) : this(RefSousFamille, null, null) { }

        public SousFamille(int RefSousFamille, Famille Famille, string Nom)
        {
            this.RefSousFamille = RefSousFamille;
            this.Famille = Famille;
            this.Nom = Nom;
        }
    }
}
