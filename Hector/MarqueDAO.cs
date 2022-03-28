using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class MarqueDAO : DAO<Marque>
    {

        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        private ConnexionBDD Connexion;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion à la base de données</param>
        public MarqueDAO(ConnexionBDD Connexion)
        {
            this.Connexion = Connexion;
        }


        /// <summary>
        /// Méthode d'insertion d'un objet Marque dans la base de données.
        /// </summary>
        /// <param name="Marque">La marque à insérer</param>
        public void Inserer(Marque Marque)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refMarque", Marque.RefMarque),
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande = "INSERT INTO Marques " +
                "(RefMarque, Nom) VALUES " +
                "(@refMarque, @nom);";
            Connexion.ExecuterCommande(Commande, Parametres);

        }


        /// <summary>
        /// Méthode de modification d'une Marque en base de données.
        /// </summary>
        /// <param name="Marque">La marque à modifier</param>
        public void Modifier(Marque Marque)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refMarque", Marque.RefMarque),
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande = "UPDATE Marques SET " +
                "Nom = @nom" +
                "WHERE RefMarque = @refMarque;";

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode pour obtenir une Marque depuis la base de données.
        /// </summary>
        /// <param name="Marque">La marque à chercher (à partir de son id)</param>
        public void Obtenir(Marque Marque)
        {
            SQLiteParameter[] Parametres = {
                new SQLiteParameter("@refMarque", Marque.RefMarque)
            };

            string Commande = "SELECT Nom FROM Marques WHERE RefMarque = @refMarque;";

            using (SQLiteDataReader Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres))
            {
                Marque.Nom = Resultat.GetString(1);
            }


        }


        /// <summary>
        /// Méthode de supression d'une Marque en base de données.
        /// </summary>
        /// <param name="Marque"></param>
        public void Supprimer(Marque Marque)
        {
        }
    }
}
