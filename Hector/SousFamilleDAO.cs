using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class SousFamilleDAO : DAO<SousFamille>
    {
        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        private ConnexionBDD Connexion;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion à la base de données</param>
        public SousFamilleDAO(ConnexionBDD Connexion)
        {
            this.Connexion = Connexion;
        }


        /// <summary>
        /// Méthode d'insertion d'un objet SousFamille dans la base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à insérer</param>
        public void Inserer(SousFamille SousFamille)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille),
                new SQLiteParameter("@refFamille", SousFamille.Famille.RefFamille),
                new SQLiteParameter("@nom", SousFamille.Nom)
            };

            string Commande = "INSERT INTO SousFamilles " +
                "(RefSousFamille, RefFamille, Nom) VALUES " +
                "(@refSousFamille, @refFamille , @nom);";
            Connexion.ExecuterCommande(Commande, Parametres);

        }


        /// <summary>
        /// Méthode de modification d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à modifier</param>
        public void Modifier(SousFamille SousFamille)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille),
                new SQLiteParameter("@refFamille", SousFamille.Famille.RefFamille),
                new SQLiteParameter("@nom", SousFamille.Nom)
            };

            string Commande = "UPDATE SousFamilles SET " +
                "RefFamille = @refFamille" +
                "Nom = @nom" +
                "WHERE RefSousFamille = @refSousFamille;";

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode pour obtenir une SousFamille depuis la base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à chercher (à partir de son id)</param>
        public void Obtenir(SousFamille SousFamille)
        {
            FamilleDAO FamilleDAO = new FamilleDAO(Connexion);

            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille)
            };

            string Commande = "SELECT RefFamille, Nom FROM SousFamilles WHERE RefSousFamille = @refSousFamille;";

            using (SQLiteDataReader Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres))
            {
                SousFamille.Famille = new Famille(Resultat.GetInt32(1));
                FamilleDAO.Obtenir(SousFamille.Famille);
                SousFamille.Nom = Resultat.GetString(2);
            }


        }


        /// <summary>
        /// Méthode de supression d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille"></param>
        public void Supprimer(SousFamille SousFamille)
        {
        }
    }
}
