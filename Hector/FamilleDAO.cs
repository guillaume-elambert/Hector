using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
    /// <summary>
    /// Classe DAO des familles.
    /// </summary>
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
        /// <returns>true si réussi, false sinon</returns>
        public bool Inserer(Famille Famille)
        {
            //if(Connexion.ExecuterCommandeAvecRes)

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@nom", Famille.Nom)
            };

            string Commande = "INSERT INTO Familles (Nom) " +
                "SELECT @nom " +
                "WHERE NOT EXISTS ( " +
                "   SELECT 1 FROM Familles WHERE Nom = @nom " +
                ") RETURNING RefFamille;";

            var ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;

            LigneSQLite Resultat = ResultatSQLite[0];
            Famille.RefFamille = Resultat.Attribut<int>(0);

            return true;

        }


        /// <summary>
        /// Méthode d'insertion d'une liste d'objets Famille dans la base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des familles à insérer</param>
        /// <returns>true si toutes les insertions réussies, false sinon</returns>
        public bool Inserer(List<Famille> ListeFamilles)
        {
            bool ARetourner = true;

            foreach (Famille Famille in ListeFamilles)
            {
                ARetourner &= Inserer(Famille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode de modification d'une Famille en base de données.
        /// </summary>
        /// <param name="Famille">La famille à modifier</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Modifier(Famille Famille)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refFamille", Famille.RefFamille),
                new SQLiteParameter("@nom", Famille.Nom)
            };

            string Commande = "UPDATE Familles SET " +
                "Nom = @nom " +
                "WHERE RefFamille = @refFamille;";

            return Connexion.ExecuterCommande(Commande, Parametres) != -1;
        }


        /// <summary>
        /// Méthode de modification d'une liste de Familles en base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des familles à modifier</param>
        /// <returns>true si toutes les modifications réussies, false sinon</returns>
        public bool Modifier(List<Famille> ListeFamilles)
        {
            bool ARetourner = true;

            foreach (Famille Famille in ListeFamilles)
            {
                ARetourner &= Modifier(Famille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour obtenir une Famille depuis la base de données.
        /// </summary>
        /// <param name="Famille">La famille à chercher (à partir de son id)</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Obtenir(Famille Famille)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refFamille", Famille.RefFamille)
            };

            string Commande = "SELECT Nom FROM Familles WHERE RefFamille = @refFamille;";

            TableSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;

            LigneSQLite Resultat = ResultatSQLite[0];
            Famille.Nom = Resultat.Attribut<string>(0);

            return true;
        }


        /// <summary>
        /// Méthode pour obtenir une liste de Famille depuis la base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des famille à chercher (à partir de leur id)</param>
        /// <returns>true si toutes les obtentions réussies, false sinon</returns>
        public bool Obtenir(List<Famille> ListeFamilles)
        {
            bool ARetourner = true;

            foreach (Famille Famille in ListeFamilles)
            {
                ARetourner &= Obtenir(Famille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour obtenir toutes les Familles depuis la base de données.
        /// </summary>
        /// <returns>La liste des familles stockées en base de données</returns>
        public Dictionary<string, Famille> ObtenirTout()
        {

            string Commande = "SELECT RefFamille, Nom FROM Familles;";

            TableSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return null;

            Dictionary<string, Famille> Familles = new Dictionary<string, Famille>();

            Famille Famille;
            foreach (LigneSQLite Ligne in ResultatSQLite)
            {
                Famille = new Famille();
                Famille.RefFamille = Ligne.Attribut<int>(0);
                Famille.Nom = Ligne.Attribut<string>(1);
                Familles[Famille.RefFamille.ToString()] = Famille;
            }

            return Familles;
        }


        /// <summary>
        /// Méthode de supression d'une Famille en base de données.
        /// </summary>
        /// <param name="Famille">La famille à supprimer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Supprimer(Famille Famille)
        {
            //Liste des paramètres SQL à passer à la requête
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refFamille", Famille.RefFamille)
            };

            string Commande = "DELETE FROM Familles WHERE RefFamille = @refFamille;";
            return Connexion.ExecuterCommande(Commande, Parametres) != -1;
        }


        /// <summary>
        /// Méthode de supression d'une liste de Familles en base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des familles à supprimer</param>
        /// <returns>true si toutes les suppressions réussies, false sinon</returns>
        public bool Supprimer(List<Famille> ListeFamilles)
        {
            bool ARetourner = true;

            foreach (Famille Famille in ListeFamilles)
            {
                ARetourner &= Supprimer(Famille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        /// <returns>true si réussi, false sinon</returns>
        public bool ViderTable()
        {
            string Commande = "DELETE FROM Familles;";
            return Connexion.ExecuterCommande(Commande) != -1;
        }
    }
}
