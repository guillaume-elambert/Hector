using System.Data.SQLite;

using System.Data;

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
        /// Méthode pour vider la base de données
        /// </summary>
        public void ViderBaseDonnees()
        {

            //Liste de tables de la base de données
            string[] Tables = new string[] {
                ////"Articles",
                "Familles",
                "Marques",
                "SousFamilles",
                "sqlite_sequence"
            };

            string Commande = "";

            foreach (string Table in Tables)
            {
                Commande += "DELETE FROM " + Table + ";";
            }

            try
            {
                Connexion.Open();
                SQLiteCommand CommandeSQLite = new SQLiteCommand(Commande, Connexion);
                CommandeSQLite.CommandType = CommandType.Text;
                CommandeSQLite.ExecuteNonQuery();
            }
            finally
            {
                Connexion.Close();
            }
        }


        /// <summary>
        /// Méthode pour executer une commande
        /// </summary>
        /// <param name="Commande">La/les commande(s) à effectuer.</param>
        /// <returns></returns>
        public int ExecuterCommande(string Commande)
        {
            try
            { 
                Connexion.Open();
                SQLiteCommand CommandeSQLite = new SQLiteCommand(Commande, Connexion);
                CommandeSQLite.CommandType = CommandType.Text;
                return CommandeSQLite.ExecuteNonQuery();
            }
            finally
            {
                Connexion.Close();
            }
}
    }
}
