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
        public void Inserer(SousFamille SousFamille)
        {
            /*bool RefSpecifee = SousFamille.RefSousFamille != -1;

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>(){
                
                new SQLiteParameter("@refFamille", SousFamille.Famille.RefFamille),
                new SQLiteParameter("@nom", SousFamille.Nom)
                
            };

            string Commande;
            
            if (RefSpecifee)
            {
                Parametres.Add(new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille));

                Commande = "INSERT INTO SousFamilles " +
                    "(RefSousFamille, RefFamille, Nom) VALUES " +
                    "(@refSousFamille, @refFamille , @nom);";
                Connexion.ExecuterCommande(Commande, Parametres);
                return;
            }*/

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>(){
                new SQLiteParameter("@refFamille", SousFamille.Famille.RefFamille),
                new SQLiteParameter("@nom", SousFamille.Nom)
            };

            string Commande;
            Commande = "INSERT INTO SousFamilles " +
                "(RefFamille, Nom) VALUES " +
                "(@refFamille , @nom) RETURNING RefSousFamille;";


            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];
            SousFamille.RefSousFamille = Resultat.Attribut<int>(0);

            /*
            DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];

            SousFamille.RefSousFamille = Convert.ToInt32(Resultat["RefSousFamille"]);
            */
        }

        /// <summary>
        /// Méthode d'insertion d'une liste d'objets SousFamille dans la base de données.
        /// </summary>
        /// <param name="ListeSousFamilles">La liste des sous-familles à insérer</param>
        public void Inserer(List<SousFamille> ListeSousFamilles)
        {
            foreach(SousFamille SousFamille in ListeSousFamilles)
            {
                Inserer(SousFamille);
            }
        }


        /// <summary>
        /// Méthode de modification d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à modifier</param>
        public void Modifier(SousFamille SousFamille)
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

            Connexion.ExecuterCommande(Commande, Parametres);
        }


        /// <summary>
        /// Méthode de modification d'une liste de SousFamilles en base de données.
        /// </summary>
        /// <param name="ListeMarques">La liste des sous-familles à modifier</param>
        public void Modifier(List<SousFamille> ListeSousFamilles)
        {
            foreach (SousFamille SousFamille in ListeSousFamilles)
            {
                Modifier(SousFamille);
            }

        }


        /// <summary>
        /// Méthode pour obtenir une SousFamille depuis la base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à chercher (à partir de son id)</param>
        public void Obtenir(SousFamille SousFamille)
        {
            FamilleDAO FamilleDAO = new FamilleDAO(Connexion);

            List<SQLiteParameter> Parametres = new List<SQLiteParameter>() {
                new SQLiteParameter("@refSousFamille", SousFamille.RefSousFamille)
            };

            string Commande = "SELECT RefFamille, Nom FROM SousFamilles WHERE RefSousFamille = @refSousFamille;";

            LigneSQLite Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres)[0];

            SousFamille.Famille = new Famille(Resultat.Attribut<int>(0));
            FamilleDAO.Obtenir(SousFamille.Famille);
            SousFamille.Nom = Resultat.Attribut<string>(1);

            /*DataRow Resultat = Connexion.ExecuterCommandeAvecResultat(Commande, Parametres).Rows[0];

            SousFamille.Famille = new Famille(Convert.ToInt32(Resultat["RefFamille"]));
            FamilleDAO.Obtenir(SousFamille.Famille);
            SousFamille.Nom = Resultat.Field<string>("Nom");*/



        }


        /// <summary>
        /// Méthode pour obtenir une liste SousFamille depuis la base de données.
        /// </summary>
        /// <param name="ListeSousFamille">La liste des sous-familles à chercher (à partir de leur id)</param>
        public void Obtenir(List<SousFamille> ListeSousFamilles)
        {
            foreach (SousFamille SousFamille in ListeSousFamilles)
            {
                Obtenir(SousFamille);
            }
        }


        /// <summary>
        /// Méthode de supression d'une SousFamille en base de données.
        /// </summary>
        /// <param name="SousFamille">La sous-famille à supprimer</param>
        public void Supprimer(SousFamille SousFamille)
        {
        }


        /// <summary>
        /// Méthode de supression d'une liste de SousFamilles en base de données.
        /// </summary>
        /// <param name="ListeSousFamilles">La liste des sous-familles à supprimer</param>
        public void Supprimer(List<SousFamille> ListeSousFamilles)
        {
            foreach(SousFamille SousFamille in ListeSousFamilles)
            {
                Supprimer(SousFamille);
            }
        }


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        public void ViderTable()
        {
            string Commande = "DELETE FROM SousFamilles;";
            Connexion.ExecuterCommande(Commande);
        }
    }
}
