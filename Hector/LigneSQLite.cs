using System;
using System.Collections.Generic;
using System.Linq;

namespace Hector
{
    /// <summary>
    /// Cette classe permet de gérer les lignes des résultats des requêtes SQLite.
    /// </summary>
    internal class LigneSQLite : Dictionary<string, object>
    {

        /// <summary>
        /// Méthode qui permet d'obtenir un attribut grâce à son indice dans la ligne et de le caster avec la classe T.
        /// </summary>
        /// <typeparam name="T">La classe utilisée pour caster l'attribut.</typeparam>
        /// <param name="Position">L'indice due l'attribut dans la ligne.</param>
        /// <returns>La valeur castée de l'attribut.</returns>
        public T Attribut<T>(int Position)
        {
            //On fait appel à la méthode Attribut<T>(string Cle) pour obtenir la valeur de l'attribut.
            return Attribut<T>(this.ElementAt(Position).Key);
        }


        /// <summary>
        /// Méthode qui permet d'obtenir un attribut grâce à son nom et de le caster avec la classe T.
        /// </summary>
        /// <typeparam name="T">La classe utilisée pour caster l'attribut.</typeparam>
        /// <param name="Cle">Le nom de l'attribut.</param>
        /// <returns>La valeur castée de l'attribut.</returns>
        public T Attribut<T>(string Cle)
        {
            //On fait appel à la méthode de base pour obtenir la valeur de l'attribut.
            return Attribut<T>(new KeyValuePair<string, object>(Cle, this[Cle]));
        }


        /// <summary>
        /// Méthode qui permet d'obtenir un attribut grâce à paire clé-valeur et de le caster avec la classe T.
        /// </summary>
        /// <typeparam name="T">La classe utilisée pour caster l'attribut.</typeparam>
        /// <param name="PairCleValeur">La paire clé-valeur de l'attribut.</param>
        /// <returns>La valeur castée de l'attribut.</returns>
        private T Attribut<T>(KeyValuePair<string, object> PairCleValeur)
        {
            object Valeur = PairCleValeur.Value;

            //On sort si la valeur est nulle
            if (Valeur == null) return default(T);

            //On tente un premier cast
            //Si on réussi on retourne la valeur
            if (Valeur.GetType().IsAssignableFrom(typeof(T)))
                return (T)Valeur;

            //On retroune la valeur du cast
            return (T)((IConvertible)Valeur).ToType(typeof(T), null);
        }


        /// <summary>
        /// Opérateur [].
        /// </summary>
        /// <param name="Position">L'indice de l'element souhaité.<</param>
        /// <returns>L'objet stocké à l'indice passé en paramètre.</returns>
        public object this[int Position]
        {
            get { return this.ElementAt(Position).Value; }
            set { this[Position] = value; }
        }

    }
}
