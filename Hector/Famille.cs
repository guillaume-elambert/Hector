using System.Collections.Generic;

namespace Hector
{
    internal class Famille
    {
        public int RefFamille { get; set; }
        public string Nom { get; set; }

        public Dictionary<string, SousFamille> SousFamilles { get; set; }


        public Famille() : this(-1, null) { }

        public Famille(int RefFamille) : this(RefFamille, null) { }

        public Famille(int RefFamille, string Nom)
        {
            this.RefFamille = RefFamille;
            this.Nom = Nom;
            SousFamilles = new Dictionary<string, SousFamille>();
        }

        public void AjouterSousFamille(SousFamille SousFamille)
        {
            SousFamilles[SousFamille.RefSousFamille.ToString()] = SousFamille;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
