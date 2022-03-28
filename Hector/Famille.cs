using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class Famille
    {
        public int RefFamille { get; set; }
        public string Nom { get; set; }

        public Famille(int RefFamille) : this(RefFamille, null) { }

        public Famille(int RefFamille, string Nom)
        {
            this.RefFamille = RefFamille;
            this.Nom = Nom;
        }
    }
}
