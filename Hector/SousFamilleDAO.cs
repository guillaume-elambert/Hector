using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
    /// <summary>
    /// Classe DAO des sous-familles
    /// </summary>
    internal class SousFamilleDAO : DAO<SousFamille>
    {
        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        private ConnexionBDD Connexion;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion à la base de données</param>
        public SousFamilleDAO(ConnexionBDD Connexion)
        {
            this.Connexion = Connexion;
        }
        

        /// <summary>
        /// Méthode d'insertion d'un objet SousFamille dans la base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à insérer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Inserer(SousFamille SousFamille)
        {

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>(){
                new SQLiteParameter("@refFamille", SousFamille.Famille.RefFamille),
                new SQLiteParameter("@nom", SousFamille.Nom)
            };

            string Commande = "INSERT INTO SousFamilles (RefFamille, Nom) " +
                "SELECT @refFamille, @nom " +
                "WHERE NOT EXISTS ( " +
                "   SELECT 1 FROM SousFamilles WHERE Nom = @nom " +
                ") RETURNING RefSousFamille;";

            TableSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;

            LigneSQLite Resultat = ResultatSQLite[0];
            SousFamille.RefSousFamille = Resultat.Attribut<int>(0);

            return true;
        }

        /// <summary>
        /// Méthode d'insertion d'une liste d'objets SousFamille dans la base de données.
        /// </summary>
        /// <param name="ListeSousFamilles">La liste des sous-familles à insérer</param>
        /// <returns>true si toutes les insertions réussies, false sinon</returns>
        public bool Inserer(List<SousFamille> ListeSousFamilles)
        {
            bool ARetourner = true;

            foreach (SousFamille SousFamille in ListeSousFamilles)
            {
                ARetourner &= Inserer(SousFamille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode de modification d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à modifier</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Modifier(SousFamille SousFamille)
        {

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille),
                new SQLiteParameter("@refFamille", SousFamille.Famille.RefFamille),
                new SQLiteParameter("@nom", SousFamille.Nom)
            };

            string Commande = "UPDATE SousFamilles SET " +
                "RefFamille = @refFamille" +
                "Nom = @nom" +
                "WHERE RefSousFamille = @refSousFamille;";

            return Connexion.ExecuterCommande(Commande, Parametres) != -1;
        }


        /// <summary>
        /// Méthode de modification d'une liste de SousFamilles en base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des sous-familles à modifier</param>
        /// <returns>true si toutes les modifications réussies, false sinon</returns>
        public bool Modifier(List<SousFamille> ListeSousFamilles)
        {
            bool ARetourner = true;

            foreach (SousFamille SousFamille in ListeSousFamilles)
            {
                ARetourner &= Modifier(SousFamille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour obtenir une SousFamille depuis la base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à chercher (à partir de son id)</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Obtenir(SousFamille SousFamille)
        {
            FamilleDAO FamilleDAO = new FamilleDAO(Connexion);

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille)
            };

            string Commande = "SELECT RefFamille, Nom FROM SousFamilles WHERE RefSousFamille = @refSousFamille;";

            TableSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return false;

            LigneSQLite Resultat = ResultatSQLite[0];
            SousFamille.Famille = new Famille(Resultat.Attribut<int>(0));
            FamilleDAO.Obtenir(SousFamille.Famille);
            SousFamille.Nom = Resultat.Attribut<string>(1);

            return true;
        }


        /// <summary>
        /// Méthode pour obtenir une liste SousFamille depuis la base de données.
        /// </summary>
        /// <param name="ListeSousFamille">La liste des sous-familles à chercher (à partir de leur id)</param>
        /// <returns>true si toutes les obtentions réussies, false sinon</returns>
        public bool Obtenir(List<SousFamille> ListeSousFamilles)
        {
            bool ARetourner = true;

            foreach (SousFamille SousFamille in ListeSousFamilles)
            {
                ARetourner &= Obtenir(SousFamille);
            }

            return ARetourner;
        }



        /// <summary>
        /// Méthode pour obtenir toutes les SousFamilles depuis la base de données.
        /// </summary>
        /// <returns>La liste des sous-familles stockées en base de données</returns>
        public Dictionary<string, SousFamille> ObtenirTout()
        {
            FamilleDAO FamilleDAO = new FamilleDAO(Connexion);

            string Commande = "SELECT RefFamille, Nom FROM SousFamilles;";

            TableSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande);
            if (ResultatSQLite == null || ResultatSQLite.Count == 0) return null;

            Dictionary<string, SousFamille> SousFamilles = new Dictionary<string, SousFamille>();
            Dictionary<int, Famille> Familles = new Dictionary<int, Famille>();

            int RefFamille;
            Famille LaFamille;
            SousFamille SousFamille;

            foreach (LigneSQLite Ligne in ResultatSQLite)
            {
                SousFamille = new SousFamille(Ligne.Attribut<int>(0));
                SousFamille.Nom = Ligne.Attribut<string>(1);

                
                //On récupère la famille de la sous famille
                RefFamille = Ligne.Attribut<int>(0);

                //Si la Famille avait déjà été chargée et créée on la récupère
                if (Familles.ContainsKey(RefFamille))
                {
                    LaFamille = Familles[RefFamille];
                }
                else
                {
                    LaFamille = new Famille(RefFamille);
                    FamilleDAO.Obtenir(LaFamille);
                    Familles.Add(RefFamille, LaFamille);
                }
                
                SousFamille.Famille = LaFamille;

                SousFamilles[SousFamille.RefSousFamille.ToString()] = SousFamille;
            }

            return SousFamilles;
        }


        /// <summary>
        /// Méthode de supression d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à supprimer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Supprimer(SousFamille SousFamille)
        {
            //Liste des paramètres SQL à passer à la requête
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille)
            };

            string Commande = "DELETE FROM SousFamilles WHERE RefSousFamille = @refSousFamille;";
            return Connexion.ExecuterCommande(Commande, Parametres) != -1;
        }


        /// <summary>
        /// Méthode de supression d'une liste de SousFamilles en base de données.
        /// </summary>
        /// <param name="ListeSousFamilles">La liste des sous-familles à supprimer</param>
        /// <returns>true si toutes les suppressions réussies, false sinon</returns>
        public bool Supprimer(List<SousFamille> ListeSousFamilles)
        {
            bool ARetourner = true;

            foreach (SousFamille SousFamille in ListeSousFamilles)
            {
                ARetourner &= Supprimer(SousFamille);
            }

            return ARetourner;
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        /// <returns>true si réussi, false sinon</returns>
        public bool ViderTable()
        {
            string Commande = "DELETE FROM SousFamilles;";
            return Connexion.ExecuterCommande(Commande) != -1;
        }
    }
}
