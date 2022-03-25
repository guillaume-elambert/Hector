using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal interface DAO<T>
    {
        void Inserer(T Objet);
        void Supprimer(T Objet);
        void Modifier(T Objet);
        T Obtenir(T Objet);
    }
}
