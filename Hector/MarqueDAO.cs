using System.Collections.Generic;
using System.Data.SQLite;

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
            /*bool RefSpecifee = Marque.RefMarque != -1;

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande;

            if (RefSpecifee)
            {
                Parametres.Add(new SQLiteParameter("@refMarque", Marque.RefMarque));

                Commande = "INSERT INTO Marques " +
                    "(RefMarque, Nom) VALUES " +
                    "(@refMarque, @nom);";
                
                Connexion.ExecuterCommande(Commande, Parametres);
                return;
            }*/


            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande;
            Commande = "INSERT INTO Marques " +
                    "(Nom) VALUES " +
                    "(@nom) RETURNING RefMarque;";

            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];
            Marque.RefMarque = Resultat.Attribut<int>(0);

            /*DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];

            Marque.RefMarque = Convert.ToInt32(Resultat["RefMarque"]);*/

        }


        /// <summary>
        /// Méthode d'insertion d'une liste d'objets Marque dans la base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marque à insérer</param>
        public void Inserer(List<Marque> ListeMarques)
        {
            foreach (Marque Marque in ListeMarques)
            {
                Inserer(Marque);
            }
        }


        /// <summary>
        /// Méthode de modification d'une Marque en base de données.
        /// </summary>
        /// <param name="Marque">La marque à modifier</param>
        public void Modifier(Marque Marque)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refMarque", Marque.RefMarque),
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande = "UPDATE Marques SET " +
                "Nom = @nom" +
                "WHERE RefMarque = @refMarque;";

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode de modification d'une liste de Marques en base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marques à modifier</param>
        public void Modifier(List<Marque> ListeMarques)
        {
            foreach (Marque Marque in ListeMarques)
            {
                Modifier(Marque);
            }

        }


        /// <summary>
        /// Méthode pour obtenir une Marque depuis la base de données.
        /// </summary>
        /// <param name="Marque">La marque à chercher (à partir de son id)</param>
        public void Obtenir(Marque Marque)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refMarque", Marque.RefMarque)
            };

            string Commande = "SELECT Nom FROM Marques WHERE RefMarque = @refMarque;";

            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];
            Marque.Nom = Resultat.Attribut<string>(0);

            /*
            DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];
            
            Marque.Nom = Resultat.Field<string>("Nom");
            */


        }

        /// <summary>
        /// Méthode pour obtenir une liste de Marques depuis la base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marque à chercher (à partir de leur id)</param>
        public void Obtenir(List<Marque> ListeMarques)
        {
            foreach (Marque Marque in ListeMarques)
            {
                Obtenir(Marque);
            }
        }


        /// <summary>
        /// Méthode de supression d'une Marque en base de données.
        /// </summary>
        /// <param name="Marque">La marque à supprimer</param>
        public void Supprimer(Marque Marque)
        {
        }

        /// <summary>
        /// Méthode de supression d'une liste de Marques en base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marques à supprimer</param>
        public void Supprimer(List<Marque> ListeMarques)
        {
            foreach (Marque Marque in ListeMarques)
            {
                Supprimer(Marque);
            }
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        public void ViderTable()
        {
            string Commande = "DELETE FROM Marques;";
            Connexion.ExecuterCommande(Commande);
        }
    }
}
