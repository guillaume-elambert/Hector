using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hector
{
    /// <summary>
    /// Classe de gestion des fichiers CSV
    /// </summary>
    internal class ParseurCSV : List<string[]>
    {
        /// <summary>
        /// Le fichier CSV.
        /// </summary>
        public FileStream Fichier { get; set; }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Fichier">Le fichier CSV.</param>
        public ParseurCSV(FileStream Fichier)
        {
            this.Fichier = Fichier;
        }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="CheminVersFichier">Le chemin vers le fichier</param>
        public ParseurCSV(string CheminVersFichier)
        {
            this.Fichier = new FileStream(CheminVersFichier, FileMode.Open, FileAccess.Read);
        }


        /// <summary>
        /// Parse le fichier CSV.
        /// </summary>
        /// <param name="PasserPremiereLigne">false si on doit parser la 1ère ligne, false sinon</param>
        public void Parser(bool PasserPremiereLigne)
        {
            string[] ValeursFichier = new string[] { };

            try
            {
                using (TextFieldParser ParseurFichier = new TextFieldParser(Fichier))
                {
                    ParseurFichier.TextFieldType = FieldType.Delimited;
                    ParseurFichier.SetDelimiters(";");

                    //On parcourt tout le fichier
                    while (!ParseurFichier.EndOfData)
                    {
                        if (PasserPremiereLigne)
                        {
                            PasserPremiereLigne = false;
                            ParseurFichier.ReadFields();
                            continue;
                        }

                        //On récupère les valeurs et on les ajoute à la liste
                        this.Add(ValeursFichier.Concat(ParseurFichier.ReadFields()).ToArray());
                    }
                }
            }
            catch (Exception Evenement)
            {
                Console.WriteLine(Evenement.Message);
                return;
            }
        }
    }
}
