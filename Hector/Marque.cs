using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    internal class Marque
    {
        private int RefMarque { get; set; }
        private string Nom { get; set; }

        public Marque(int RefMarque, string Nom)
        {
            this.RefMarque = RefMarque;
            this.Nom = Nom;
        }
    }
}
