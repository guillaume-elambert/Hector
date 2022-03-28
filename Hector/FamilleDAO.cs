using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class FamilleDAO : DAO<Famille>
    {

        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        private ConnexionBDD Connexion;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion à la base de données</param>
        public FamilleDAO(ConnexionBDD Connexion)
        {
            this.Connexion = Connexion;
        }


        /// <summary>
        /// Méthode d'insertion d'un objet Famille dans la base de données.
        /// </summary>
        /// <param name="Famille">La famille à insérer</param>
        public void Inserer(Famille Famille)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refFamille", Famille.RefFamille),
                new SQLiteParameter("@nom", Famille.Nom)
            };

            string Commande = "INSERT INTO Familles " +
                "(RefFamille, Nom) VALUES " +
                "(@refFamille, @nom);";
            Connexion.ExecuterCommande(Commande, Parametres);

        }


        /// <summary>
        /// Méthode de modification d'une Famille en base de données.
        /// </summary>
        /// <param name="Famille">La famille à modifier</param>
        public void Modifier(Famille Famille)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refFamille", Famille.RefFamille),
                new SQLiteParameter("@nom", Famille.Nom)
            };

            string Commande = "UPDATE Familles SET " +
                "Nom = @nom" +
                "WHERE RefFamille = @refFamille;";

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode pour obtenir une Famille depuis la base de données.
        /// </summary>
        /// <param name="Famille">La famille à chercher (à partir de son id)</param>
        public void Obtenir(Famille Famille)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refFamille", Famille.RefFamille)
            };

            string Commande = "SELECT Nom FROM Familles WHERE RefFamille = @refFamille;";

            using (SQLiteDataReader Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres))
            {
                Famille.Nom = Resultat.GetString(1);
            }


        }


        /// <summary>
        /// Méthode de supression d'une Famille en base de données.
        /// </summary>
        /// <param name="Famille"></param>
        public void Supprimer(Famille Famille)
        {
        }
    }
}
