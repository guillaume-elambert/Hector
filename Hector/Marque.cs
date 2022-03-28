using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class Marque
    {
        public int RefMarque { get; set; }
        public string Nom { get; set; }

        public Marque(int RefMarque) : this(RefMarque, null) { }

        public Marque(int RefMarque, string Nom)
        {
            this.RefMarque = RefMarque;
            this.Nom = Nom;
        }
    }
}
