using System.Collections.Generic;

namespace Hector
{
    internal class Famille
    {
        public int RefFamille { get; set; }
        public string Nom { get; set; }

        public List<SousFamille> SousFamilles { get; set; }


        public Famille() : this(-1, null) { }

        public Famille(int RefFamille) : this(RefFamille, null) { }

        public Famille(int RefFamille, string Nom)
        {
            this.RefFamille = RefFamille;
            this.Nom = Nom;
            SousFamilles = new List<SousFamille>();
        }

        public void AjouterSousFamille(SousFamille SousFamille)
        {
            if (SousFamilles.Contains(SousFamille)) return;
            SousFamilles.Add(SousFamille);
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
