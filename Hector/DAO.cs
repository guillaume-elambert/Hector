using System.Collections.Generic;

namespace Hector
{
    internal interface DAO<T>
    {
        bool Inserer(T Objet);
        bool Inserer(List<T> ListeObjets);
        bool Supprimer(T Objet);
        bool Supprimer(List<T> ListeObjets);
        bool Modifier(T Objet);
        bool Modifier(List<T> ListeObjets);
        bool Obtenir(T Objet);
        bool Obtenir(List<T> ListeObjets);

        bool ViderTable();
    }
}
