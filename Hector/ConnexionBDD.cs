using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Hector
{
    internal class ConnexionBDD
    {
        /// <summary>
        /// La connexion vers la base de données
        /// </summary>
        private SQLiteConnection Connexion;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="CheminVersSQLite">Le chemin vers la BDD</param>
        public ConnexionBDD(string CheminVersSQLite)
        {
            Connexion = new SQLiteConnection("Data Source=" + CheminVersSQLite + ";");
        }

        /// <summary>
        /// Méthode qui permet d'ouvrir la connexion
        /// </summary>
        public void Open()
        {
            if (Connexion != null)
            {
                Connexion?.Open();
            }
        }


        /// <summary>
        /// Méthode qui permet de fermer la connexion
        /// </summary>
        public void Close()
        {
            if (Connexion != null)
            {
                Connexion?.Close();
            }
        }


        /// <summary>
        /// Méthode qui permet de vider la table sqlite_sequence de la base de données
        /// </summary>
        public void ViderTableSQLiteSequence()
        {
            string Commande = "DELETE FROM sqlite_sequence;";

            this.ExecuterCommande(Commande);
        }

        /// <summary>
        /// Méthode pour executer une commande
        /// </summary>
        /// <param name="Commande">La commande à effectuer.</param>
        /// <returns>Le nombre de ligne afféctées par la requête ou -1 si erreur</returns>
        public int ExecuterCommande(string Commande)
        {
            return ExecuterCommande(Commande, new List<SQLiteParameter>());
        }


        /// <summary>
        /// Méthode pour executer une commande
        /// </summary>
        /// <param name="Commande">La commande à effectuer.</param>
        /// <param name="Parametres">Les paramètres de la commande.</param>
        /// <returns>Le nombre de ligne afféctées par la requête ou -1 si erreur</returns>
        public int ExecuterCommande(string Commande, List<SQLiteParameter> Parametres)
        {
            try
            {
                Connexion.Open();
                SQLiteCommand CommandeSQLite = new SQLiteCommand(Commande, Connexion);
                CommandeSQLite.CommandType = CommandType.Text;

                foreach (SQLiteParameter Parametre in Parametres)
                {
                    CommandeSQLite.Parameters.Add(Parametre);
                }

                try
                {
                    return CommandeSQLite.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            finally
            {
                Connexion.Close();
            }
        }



        /// <summary>
        /// Méthode qui permet d'executer une commande qui retourne un résulat.
        /// </summary>
        /// <param name="Commande">La commande à effectuer.</param>
        /// <param name="Parametres">Les paramètres de la commande.</param>
        /// <returns>L'objet ResultatSQLite correspondant au résultat de la requête ou null si erreur</returns>
        public ResultatSQLite ExecuterCommandeAvecResultat(string Commande, List<SQLiteParameter> Parametres)
        {
            try
            {
                Connexion.Open();
                SQLiteCommand CommandeSQLite = new SQLiteCommand(Commande, Connexion);
                CommandeSQLite.CommandType = CommandType.Text;

                foreach (SQLiteParameter Parametre in Parametres)
                {
                    CommandeSQLite.Parameters.Add(Parametre);
                }

                try
                {
                    ResultatSQLite ContenuRequete = new ResultatSQLite(CommandeSQLite.ExecuteReader());
                    return ContenuRequete;
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            finally
            {
                Connexion.Close();
            }
        }


        /// <summary>
        /// Méthode qui permet d'executer une commande qui retourne un résulat.
        /// </summary>
        /// <param name="Commande">La commande à effectuer.</param>
        /// <returns>L'objet ResultatSQLite correspondant au résultat de la requête ou null si erreur</returns>
        public ResultatSQLite ExecuterCommandeAvecResultat(string Commande)
        {
            return ExecuterCommandeAvecResultat(Commande, new List<SQLiteParameter>());
        }
    }
}
