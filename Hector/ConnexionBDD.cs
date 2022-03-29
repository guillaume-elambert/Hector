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


        public void Open()
        {
            if (Connexion != null)
            {
                Connexion?.Open();
            }
        }


        public void Close()
        {
            if (Connexion != null)
            {
                Connexion?.Close();
            }
        }

        public void ViderTableSQLiteSequence()
        {
            string Commande = "DELETE FROM sqlite_sequence;";

            this.ExecuterCommande(Commande);
        }


        /// <summary>
        /// Méthode pour vider la base de données
        /// </summary>
        /*public void ViderBaseDonnees()
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
        }*/

        /// <summary>
        /// Méthode pour executer une commande
        /// </summary>
        /// <param name="Commande">La commande à effectuer.</param>
        /// <returns></returns>
        public int ExecuterCommande(string Commande)
        {
            return ExecuterCommande(Commande, new List<SQLiteParameter>());
        }


        /// <summary>
        /// Méthode pour executer une commande
        /// </summary>
        /// <param name="Commande">La commande à effectuer.</param>
        /// <param name="Parametres">Les paramètres de la commande.</param>
        /// <returns></returns>
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

                return CommandeSQLite.ExecuteNonQuery();
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
        /// <returns>L'objet ResultatSQLite correspondant au résultat de la requête</returns>
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


                ResultatSQLite ContenuRequete = new ResultatSQLite(CommandeSQLite.ExecuteReader());
                /*List<object> Ligne;
                int NombreChamps;

                using(SQLiteDataReader Resultat = CommandeSQLite.ExecuteReader())
                {
                    while (Resultat.Read())
                    {
                        Ligne = new List<object>();
                        NombreChamps = Resultat.FieldCount;
                        for(int i = 0; i < NombreChamps; ++i)
                        {
                            Ligne.Add(Resultat.GetValue(i));
                        }

                        ContenuRequete.Add(Ligne);
                    }
                }*/

                return ContenuRequete;
                //DataSet JeuDonnees = new DataSet();
                /*DataTable JeuDonnees = new DataTable();


                using (SQLiteDataAdapter JeuDonneesAdapter = new SQLiteDataAdapter(CommandeSQLite))
                {
                    JeuDonneesAdapter.AcceptChangesDuringFill = false;
                    JeuDonneesAdapter.Fill(JeuDonnees);
                }*/


                /*SQLiteDataReader sql = CommandeSQLite.ExecuteReader();
                JeuDonnees.Load(sql);*/
                //return JeuDonnees;//CommandeSQLite.ExecuteReader();
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
        /// <returns>L'objet ResultatSQLite correspondant au résultat de la requête</returns>
        public ResultatSQLite ExecuterCommandeAvecResultat(string Commande)
        {
            return ExecuterCommandeAvecResultat(Commande, new List<SQLiteParameter>());
        }
    }
}
