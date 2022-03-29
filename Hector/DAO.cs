using System.Collections.Generic;

namespace Hector
{
    internal interface DAO<T>
    {
        void Inserer(T Objet);
        void Inserer(List<T> ListeObjets);
        void Supprimer(T Objet);
        void Supprimer(List<T> ListeObjets);
        void Modifier(T Objet);
        void Modifier(List<T> ListeObjets);
        void Obtenir(T Objet);
        void Obtenir(List<T> ListeObjets);

        void ViderTable();
    }
}
