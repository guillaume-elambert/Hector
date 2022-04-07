using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
    /// <summary>
    /// Cette classe permet de gérer les résultats des requêtes SQLite.
    /// </summary>
    internal class TableSQLite : List<LigneSQLite>
    {

        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        /// <param name="SQLiteDataReader">L'objet qui contient les résultats de la requête.</param>
        public TableSQLite(SQLiteDataReader SQLiteDataReader)
        {
            LigneSQLite LigneSQLite;
            int NombreChamps;


            //Tant qu'on peut lire une ligne
            while (SQLiteDataReader.Read())
            {
                LigneSQLite = new LigneSQLite();
                NombreChamps = SQLiteDataReader.FieldCount;

                //Pour chaque champs de la ligne, on ajoute la valeur dans la ligne
                for (int i = 0; i < NombreChamps; ++i)
                {
                    LigneSQLite.Add(SQLiteDataReader.GetName(i), SQLiteDataReader.GetValue(i));
                }

                //On ajoute la ligne à la table
                this.Add(LigneSQLite);
            }
        }
    }
}
