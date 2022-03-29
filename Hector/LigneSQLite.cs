using System;
using System.Collections.Generic;
using System.Linq;

namespace Hector
{
    internal class LigneSQLite : Dictionary<string, object>
    {

        public T Attribut<T>(int Position)
        {
            return Attribut<T>(this.ElementAt(Position).Key);
        }

        public T Attribut<T>(string Cle)
        {
            return Attribut<T>(new KeyValuePair<string, object>(Cle, this[Cle]));
        }


        private T Attribut<T>(KeyValuePair<string, object> PairCleValeur)
        {
            object Valeur = PairCleValeur.Value;
            if (Valeur == null) return default(T);

            if (Valeur.GetType().IsAssignableFrom(typeof(T)))
                return (T)Valeur;

            return (T)((IConvertible)Valeur).ToType(typeof(T), null);
        }

        public object this[int Position]
        {
            get { return this.ElementAt(Position).Value; }
            set { this[Position] = value; }
        }

    }
}
