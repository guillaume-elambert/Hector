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
        public MarqueDAO() { }

        /// <summary>
        /// Méthode d'insertion d'un objet Marque dans la base de données.
        /// </summary>
        /// <param name="Marque">La marque à insérer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Inserer(Marque Marque)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande = "INSERT INTO Marques (Nom) " +
                "SELECT @nom " +
                "WHERE NOT EXISTS ( " +
                "   SELECT 1 FROM Marques WHERE Nom = @nom " +
                ") " +
                "RETURNING RefMarque;";

            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;


            LigneSQLite Resultat = ResultatSQLite[0];
            Marque.RefMarque = Resultat.Attribut<int>(0);

            return true;

        }


        /// <summary>
        /// Méthode d'insertion d'une liste d'objets Marque dans la base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marque à insérer</param>
        /// <returns>true si toutes les insertions réussies, false sinon</returns>
        public bool Inserer(List<Marque> ListeMarques)
        {
            bool ARetourner = true;

            foreach (Marque Marque in ListeMarques)
            {
                ARetourner &= Inserer(Marque);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode de modification d'une Marque en base de données.
        /// </summary>
        /// <param name="Marque">La marque à modifier</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Modifier(Marque Marque)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refMarque", Marque.RefMarque),
                new SQLiteParameter("@nom", Marque.Nom)
            };

            string Commande = "UPDATE Marques SET " +
                "Nom = @nom" +
                "WHERE RefMarque = @refMarque;";

            return Connexion.ExecuterCommande(Commande, Parametres) != -1;
        }


        /// <summary>
        /// Méthode de modification d'une liste de Marques en base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marques à modifier</param>
        /// <returns>true si toutes les modifications réussies, false sinon</returns>
        public bool Modifier(List<Marque> ListeMarques)
        {
            bool ARetourner = true;

            foreach (Marque Marque in ListeMarques)
            {
                ARetourner &= Modifier(Marque);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour obtenir une Marque depuis la base de données.
        /// </summary>
        /// <param name="Marque">La marque à chercher (à partir de son id)</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Obtenir(Marque Marque)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refMarque", Marque.RefMarque)
            };

            string Commande = "SELECT Nom FROM Marques WHERE RefMarque = @refMarque;";

            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;

            LigneSQLite Resultat = ResultatSQLite[0];
            Marque.Nom = Resultat.Attribut<string>(0);

            return true;
        }

        
        /// <summary>
        /// Méthode pour obtenir une liste de Marques depuis la base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marque à chercher (à partir de leur id)</param>
        /// <returns>true si toutes les obtentions réussies, false sinon</returns>
        public bool Obtenir(List<Marque> ListeMarques)
        {
            bool ARetourner = true;

            foreach (Marque Marque in ListeMarques)
            {
                ARetourner &= Obtenir(Marque);
            }

            return ARetourner;
        }



        /// <summary>
        /// Méthode pour obtenir toutes les Marques depuis la base de données.
        /// </summary>
        /// <returns>La liste des marques stockées en base de données</returns>
        public List<Marque> ObtenirTout()
        {

            string Commande = "SELECT RefMarque, Nom FROM Marques;";

            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return null;
            
            List<Marque> ListeMarques = new List<Marque>();
            Marque Marque;

            foreach (LigneSQLite Ligne in ResultatSQLite)
            {
                Marque = new Marque();
                Marque.RefMarque = Ligne.Attribut<int>(0);
                Marque.Nom = Ligne.Attribut<string>(1);
                ListeMarques.Add(Marque);
            }

            return ListeMarques;
        }

        /// <summary>
        /// Méthode de supression d'une Marque en base de données.
        /// </summary>
        /// <param name="Marque">La marque à supprimer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Supprimer(Marque Marque)
        {
            return true;
        }

        /// <summary>
        /// Méthode de supression d'une liste de Marques en base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des marques à supprimer</param>
        /// <returns>true si toutes les suppression réussies, false sinon</returns>
        public bool Supprimer(List<Marque> ListeMarques)
        {
            bool ARetourner = true;

            foreach (Marque Marque in ListeMarques)
            {
                ARetourner &= Supprimer(Marque);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        /// <returns>true si réussi, false sinon</returns>
        public bool ViderTable()
        {
            string Commande = "DELETE FROM Marques;";
            return Connexion.ExecuterCommande(Commande) != -1;
        }
    }
}
