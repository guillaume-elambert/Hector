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
    internal partial class SousFamilleForm : Form
    {
        ConnexionBDD Connexion;
        SousFamilleDAO SousFamilleDAO;

        Famille Famille;
        SousFamille SousFamille;
        Dictionary<string, Famille> Familles;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Famille"></param>
        /// <param name="SousFamille"></param>
        public SousFamilleForm(ConnexionBDD Connexion, Dictionary<string, Famille> Familles, Famille Famille = null, SousFamille SousFamille = null)
        {
            InitializeComponent();

            this.SousFamille = SousFamille;
            this.Connexion = Connexion;
            SousFamilleDAO = new SousFamilleDAO(this.Connexion);

            // On remplit la combo box
            FamilleComboBox.DataSource = new BindingSource(Familles.Values.ToArray(), null);

            // Si on veut créer une nouvelle sous-famille
            if (SousFamille == null)
            {
                this.SousFamille = new SousFamille();
                Text = "Créer une nouvelle sous-famille";
                ConfirmButton.Text = "Ajouter la sous-famille";
                RefTextBox.Text = "Réference générée automatiquement";
            }
            // Si on veut modifier une sous-famille existante
            else
            {
                this.Famille = SousFamille.Famille;
                Text = "Modifier la sous-famille [" + SousFamille + "]";
                ConfirmButton.Text = "Modifier la sous-famille";
                //FamilleComboBox.Enabled = false;

                RefTextBox.Text = SousFamille.RefSousFamille.ToString();
                NomTextBox.Text = SousFamille.Nom;
            }

            FamilleComboBox.SelectedIndex = 0;
            if (Famille != null)
            {
                FamilleComboBox.SelectedIndex = FamilleComboBox.Items.IndexOf(Famille);
            }
            UpdateConfirmButton();
        }

        
        /// <summary>
        /// Event déclenché lors d'un clic sur le bouton de confirmation.
        /// Ajoute ou modifie la sous-famille.
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void ConfirmButton_Click(object Emetteur, EventArgs Evenement)
        {
            SousFamille.Nom = NomTextBox.Text;
            
            if (SousFamille.Famille != null)
            {
                SousFamille.Famille.SousFamilles.Remove(SousFamille.RefSousFamille.ToString());
            }
            
            SousFamille.Famille = Famille;

            // Si on veut créer une sous-famille
            if (SousFamille.RefSousFamille == -1)
            {
                SousFamilleDAO.Inserer(SousFamille);
            }
            // Si on veut modifier une sous-famille existante
            else
            {
                SousFamilleDAO.Modifier(SousFamille);
            }

            Famille.AjouterSousFamille(SousFamille);
            Close();
        }

        
        /// <summary>
        /// Event déclenché lorsque le texte de la NameTextBox change.
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void NomTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            UpdateConfirmButton();
        }

        
        /// <summary>
        /// Event déclenché lorsque l'item sélectionné dans la ComboBox change
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void FamilleComboBox_SelectedValueChanged(object Emetteur, EventArgs Evenement)
        {
            Famille = (Famille)FamilleComboBox.SelectedValue;
            UpdateConfirmButton();
        }

        
        /// <summary>
        /// Fonction qui active ou non le bouton de confirmation en fonction de la validité des champs.
        /// </summary>
        private void UpdateConfirmButton()
        {
            if (NomTextBox.Text != "" && FamilleComboBox.SelectedIndex != -1 && Famille != null)
            {
                ConfirmButton.Enabled = true;
            }
            else
            {
                ConfirmButton.Enabled = false;
            }
        }


    }
}
