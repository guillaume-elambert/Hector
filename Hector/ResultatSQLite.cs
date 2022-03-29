using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
    internal class ResultatSQLite : List<LigneSQLite>
    {
        public ResultatSQLite(SQLiteDataReader SQLiteDataReader)
        {
            LigneSQLite LigneSQLite;
            int NombreChamps;


            while (SQLiteDataReader.Read())
            {
                LigneSQLite = new LigneSQLite();
                NombreChamps = SQLiteDataReader.FieldCount;

                for (int i = 0; i < NombreChamps; ++i)
                {
                    LigneSQLite.Add(SQLiteDataReader.GetName(i), SQLiteDataReader.GetValue(i));
                }

                this.Add(LigneSQLite);
            }
        }
    }
}
