using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hector
{
    internal class ParseurCSV : List<string[]>
    {
        public FileStream Fichier { get; set; }


        public ParseurCSV(FileStream Fichier)
        {
            this.Fichier = Fichier;
        }

        public ParseurCSV(string CheminVersFichier)
        {
            this.Fichier = new FileStream(CheminVersFichier, FileMode.Open, FileAccess.Read);
        }

        public void Parser(bool PasserPremiereLigne)
        {
            string[] ValeursFichier = new string[] { };

            using (TextFieldParser ParseurFichier = new TextFieldParser(Fichier))
            {
                ParseurFichier.TextFieldType = FieldType.Delimited;
                ParseurFichier.SetDelimiters(";");

                while (!ParseurFichier.EndOfData)
                {
                    if (PasserPremiereLigne)
                    {
                        PasserPremiereLigne = false;
                        ParseurFichier.ReadFields();
                        continue;
                    }

                    this.Add(ValeursFichier.Concat(ParseurFichier.ReadFields()).ToArray());
                }
            }
        }
    }
}
