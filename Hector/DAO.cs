using System.Collections.Generic;

namespace Hector
{
    /// <summary>
    /// Interface des DAO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface DAO<T>
    {
        /// <summary>
        /// Méthode d'insertion d'un objet dans la base de données.
        /// </summary>
        /// <param name="Objet">L'objet à insérer</param>
        /// <returns>true si réussi, false sinon</returns>
        bool Inserer(T Objet);

        
        /// <summary>
        /// Méthode d'insertion d'une liste d'objets dans la base de données.
        /// </summary>
        /// <param name="ListeObjets">La liste des objets à insérer</param>
        /// <returns>true si toutes les insertions réussies, false sinon</returns>
        bool Inserer(List<T> ListeObjets);


        /// <summary>
        /// Méthode de supression d'un objet en base de données.
        /// </summary>
        /// <param name="Objet">L'objet à supprimer</param>
        /// <returns>true si réussi, false sinon</returns>
        bool Supprimer(T Objet);


        /// <summary>
        /// Méthode de supression d'une liste d'objets en base de données.
        /// </summary>
        /// <param name="ListeObjets">La liste des objets à supprimer</param>
        /// <returns>true si toutes les suppressions réussies, false sinon</returns>
        bool Supprimer(List<T> ListeObjets);
        

        /// <summary>
        /// Méthode de modification d'un objet en base de données.
        /// </summary>
        /// <param name="Objet">L'objet à modifier</param>
        /// <returns>true si réussi, false sinon</returns>
        bool Modifier(T Objet);


        /// <summary>
        /// Méthode de modification une liste d'objets en base de données.
        /// </summary>
        /// <param name="ListeObjets">La liste des objets à modifier.</param>
        /// <returns>true si toutes les modifications réussies, false sinon</returns>
        bool Modifier(List<T> ListeObjets);
        

        /// <summary>
        /// Méthode pour obtenir un objet depuis la base de données.
        /// </summary>
        /// <param name="Objet">L'objet à chercher (à partir de sa clé primaire)</param>
        /// <returns>true si réussi, false sinon</returns>
        bool Obtenir(T Objet);


        /// <summary>
        /// Méthode pour obtenir une liste d'objets depuis la base de données.
        /// </summary>
        /// <param name="ListeObjets">Les objets à chercher (à partir de leur clé primaire)</param>
        /// <returns>true si toutes les obtentions réussies, false sinon</returns>
        bool Obtenir(List<T> ListeObjets);

        
        /// <summary>
        /// Méthode pour obtenir tous les objets depuis la base de données.
        /// </summary>
        /// <returns>La liste des objets stockés en base de données</returns>
        Dictionary<string, T> ObtenirTout();


        /// <summary>
        /// Méthode pour supprimer le contenu de la table.
        /// </summary>
        /// <returns>true si réussi, false sinon</returns>
        bool ViderTable();
    }
}
