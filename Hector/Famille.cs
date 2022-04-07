using System.Collections.Generic;

namespace Hector
{
    /// <summary>
    /// Classe des familles.
    /// </summary>
    internal class Famille
    {
        /// <summary>
        /// La référence de la famille.
        /// </summary>
        public int RefFamille { get; set; }
        /// <summary>
        /// Le nom de la famille.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// La liste des sous-familles de la famille.
        /// </summary>
        public Dictionary<string, SousFamille> SousFamilles { get; set; }



        /// <summary>
        /// Constructeur.
        /// </summary>
        public Famille() : this(-1, null) { }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefFamille">La référence de la famille</param>
        public Famille(int RefFamille) : this(RefFamille, null) { }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefFamille">La référence de la famille</param>
        /// <param name="Nom">Le nom de la famille</param>
        public Famille(int RefFamille, string Nom)
        {
            this.RefFamille = RefFamille;
            this.Nom = Nom;
            SousFamilles = new Dictionary<string, SousFamille>();
        }


        /// <summary>
        /// Méthode qui permet d'ajouter une sous-famille à la liste.
        /// </summary>
        /// <param name="SousFamille"></param>
        public void AjouterSousFamille(SousFamille SousFamille)
        {
            SousFamilles[SousFamille.RefSousFamille.ToString()] = SousFamille;
        }


        /// <summary>
        /// Méthode qui permet de retourner une chaîne de caractères qui représente l'objet
        /// </summary>
        /// <returns>Une chaîne de caractères qui représente l'objet</returns>
        public override string ToString()
        {
            return Nom;
        }
    }
}
