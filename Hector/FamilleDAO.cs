using System.Collections.Generic;
using System.Data.SQLite;

namespace Hector
{
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
        public void Inserer(Famille Famille)
        {
            /*bool RefSpecifee = Famille.RefFamille != -1;

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@nom", Famille.Nom)
            };


            string Commande;

            if (RefSpecifee)
            {
                Parametres.Add(new SQLiteParameter("@refFamille", Famille.RefFamille));

                Commande = "INSERT INTO Familles " +
                "(RefFamille, Nom) VALUES " +
                "(@refFamille, @nom);";
                
                Connexion.ExecuterCommande(Commande, Parametres);
                return;
            }*/

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@nom", Famille.Nom)
            };

            string Commande;
            Commande = "INSERT INTO Familles " +
                "(Nom) VALUES " +
                "(@nom) RETURNING RefFamille;";

            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];
            Famille.RefFamille = Resultat.Attribut<int>(0);

            /*DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];

            Famille.RefFamille = Convert.ToInt32(Resultat["RefFamille"]);*/

        }


        /// <summary>
        /// Méthode d'insertion d'une liste d'objets Famille dans la base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des familles à insérer</param>
        public void Inserer(List<Famille> ListeFamilles)
        {
            foreach (Famille Famille in ListeFamilles)
            {
                Inserer(Famille);
            }
        }


        /// <summary>
        /// Méthode de modification d'une Famille en base de données.
        /// </summary>
        /// <param name="Famille">La famille à modifier</param>
        public void Modifier(Famille Famille)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refFamille", Famille.RefFamille),
                new SQLiteParameter("@nom", Famille.Nom)
            };

            string Commande = "UPDATE Familles SET " +
                "Nom = @nom" +
                "WHERE RefFamille = @refFamille;";

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode de modification d'une liste de Familles en base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des familles à modifier</param>
        public void Modifier(List<Famille> ListeFamilles)
        {
            foreach (Famille Famille in ListeFamilles)
            {
                Modifier(Famille);
            }
        }


        /// <summary>
        /// Méthode pour obtenir une Famille depuis la base de données.
        /// </summary>
        /// <param name="Famille">La famille à chercher (à partir de son id)</param>
        public void Obtenir(Famille Famille)
        {
            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refFamille", Famille.RefFamille)
            };

            string Commande = "SELECT Nom FROM Familles WHERE RefFamille = @refFamille;";

            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];
            Famille.Nom = Resultat.Attribut<string>(0);

            /*DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];
                        
            Famille.Nom = Resultat.Field<string>("Nom");*/



        }


        /// <summary>
        /// Méthode pour obtenir une liste de Famille depuis la base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des famille à chercher (à partir de leur id)</param>
        public void Obtenir(List<Famille> ListeFamilles)
        {
            foreach (Famille Famille in ListeFamilles)
            {
                Obtenir(Famille);
            }
        }


        /// <summary>
        /// Méthode de supression d'une Famille en base de données.
        /// </summary>
        /// <param name="Famille">La famille à supprimer</param>
        public void Supprimer(Famille Famille)
        {
        }

        /// <summary>
        /// Méthode de supression d'une liste de Familles en base de données.
        /// </summary>
        /// <param name="ListeFamilles">La liste des familles à supprimer</param>
        public void Supprimer(List<Famille> ListeFamilles)
        {
            foreach(Famille Famille in ListeFamilles)
            {
                Supprimer(Famille);
            }
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        public void ViderTable()
        {
            string Commande = "DELETE FROM Familles;";
            Connexion.ExecuterCommande(Commande);
        }
    }
}
