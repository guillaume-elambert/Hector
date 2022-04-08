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
    internal partial class FamilleForm : Form
    {
        ConnexionBDD Connexion;
        FamilleDAO FamilleDAO;
        Famille Famille;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Famille"></param>
        public FamilleForm(ConnexionBDD Connexion, Famille Famille = null)
        {
            InitializeComponent();

            this.Connexion = Connexion;
            FamilleDAO = new FamilleDAO(this.Connexion);
            this.Famille = Famille;

            // Si on veut créer une nouvelle famille
            if (Famille == null)
            {
                this.Famille = new Famille();
                Text = "Créer une nouvelle famille";
                ConfirmButton.Text = "Ajouter la famille";
                RefTextBox.Text = "Réference générée automatiquement";
                ConfirmButton.Enabled = false;
            }
            // Si on veut modifier une famille existante
            else
            {
                Text = "Modifier la famille [" + Famille + "]";
                ConfirmButton.Text = "Modifier la famille";
                ConfirmButton.Enabled = true;

                RefTextBox.Text = Famille.RefFamille.ToString();
                NomTextBox.Text = Famille.Nom;
            }
        }

        /// <summary>
        /// Event déclenché lors d'un clic sur le bouton de confirmation.
        /// Ajoute ou modifie la famille
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void ConfirmButton_Click(object Emetteur, EventArgs Evenement)
        {
            Famille.Nom = NomTextBox.Text;
            
            // Si on veut créer une famille
            if (Famille.RefFamille == -1)
            {
                FamilleDAO.Inserer(Famille);
            }
            // Si on veut modifier une famille existante
            else
            {
                FamilleDAO.Modifier(Famille);
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
