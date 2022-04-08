using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    internal partial class MarqueForm : Form
    {
        MarqueDAO MarqueDAO;
        ConnexionBDD Connexion;
        Marque Marque;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Marque"></param>
        public MarqueForm(ConnexionBDD Connexion, Marque Marque = null)
        {
            this.Connexion = Connexion;
            MarqueDAO = new MarqueDAO(this.Connexion);
            InitializeComponent();

            this.Marque = Marque;

            // Si on veut créer une nouvelle marque
            if (Marque == null)
            {
                this.Marque = new Marque();
                Text = "Créer une nouvelle marque";
                ConfirmButton.Text = "Ajouter la marque";
                RefTextBox.Text = "Réference générée automatiquement";
                ConfirmButton.Enabled = false;
            }
            // Si on veut modifier une marque existante
            else
            {

                Text = "Modifier la marque [" + Marque + "]";
                ConfirmButton.Text = "Modifier la marque";
                ConfirmButton.Enabled = true;

                RefTextBox.Text = Marque.RefMarque.ToString();
                NomTextBox.Text = Marque.Nom;
            }
        }

        /// <summary>
        /// Event déclenché lors d'un clic sur le bouton de confirmation.
        /// Ajoute ou modifie la marque
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void ConfirmButton_Click(object Emetteur, EventArgs Evenement)
        {
            Marque.Nom = NomTextBox.Text;

            // Si on veut créer une marque
            if (Marque.RefMarque == -1)
            {
                MarqueDAO.Inserer(Marque);
            }
            // Si on veut modifier une marque existante
            else
            {
                MarqueDAO.Modifier(Marque);
            }
            
            Close();
        }

        /// <summary>
        /// Event déclenché lorsque le texte de la NomTextBox change.
        /// Utilisé pour vérifié la validité des champs entrés par l'utilisateur.
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void NomTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            if (NomTextBox.Text == "")
            {
                ConfirmButton.Enabled = false;
            }
            else
            {
                ConfirmButton.Enabled = true;
            }
        }
    }
}
