using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
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

            string Commande;
            Commande = "INSERT INTO SousFamilles " +
                "(RefFamille, Nom) VALUES " +
                "(@refFamille , @nom) RETURNING RefSousFamille;";

            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null) return false;

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

            ResultatSQLite ResultatSQLite = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres);
            if (ResultatSQLite == null) return false;

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
        /// Méthode de supression d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à supprimer</param>
        /// <returns>true si réussi, false sinon</returns>
        public bool Supprimer(SousFamille SousFamille)
        {
            return true;
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
