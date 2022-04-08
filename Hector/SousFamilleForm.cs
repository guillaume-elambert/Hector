using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hector
{
    /// <summary>
    /// Formulaire pour la modification ou la création d'une SousFamille.
    /// </summary>
    internal partial class SousFamilleForm : Form
    {
        /// <summary>
        /// La connexion vers la BDD.
        /// </summary>
        ConnexionBDD Connexion;
        /// <summary>
        /// Le DAO des sous-familles.
        /// </summary>
        SousFamilleDAO SousFamilleDAO;

        /// <summary>
        /// La famille de la sous-famille
        /// </summary>
        Famille Famille;
        /// <summary>
        /// La sous-famille
        /// </summary>
        SousFamille SousFamille;


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion vers la BDD</param>
        /// <param name="Familles">La liste des familles disponibles</param>
        /// <param name="Famille">La famille par dfaut de la sous-famille</param>
        /// <param name="SousFamille">La sous famille</param>
        public SousFamilleForm(ConnexionBDD Connexion, Dictionary<string, Famille> Familles, Famille Famille = null, SousFamille SousFamille = null)
        {
            InitializeComponent();

            //Initialisation des variables
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

            //Si on a passé une famille en paramètre, on la séléctionne dans le ComboBox
            if (Famille != null)
            {
                FamilleComboBox.SelectedItem = Famille;
            }
            UpdateConfirmButton();
        }


        /// <summary>
        /// Event déclenché lors d'un clic sur le bouton de confirmation.
        /// Ajoute ou modifie la sous-famille.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
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
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void NomTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            UpdateConfirmButton();
        }


        /// <summary>
        /// Event déclenché lorsque l'item sélectionné dans la ComboBox change
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
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
            if (NomTextBox.Text == "" || NomTextBox.Text.Length > 50 || FamilleComboBox.SelectedIndex == -1 || Famille == null)
            {
                ConfirmButton.Enabled = false;
            }
            else
            {
                ConfirmButton.Enabled = true;
            }
        }


        /// <summary>
        /// Méthode pour fermer la fernêtre lors de l'appuie sur la touche "Echap" ou "Entrée".
        /// </summary>
        /// <param name="Touche">La touche</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys Touche)
        {
            if (Form.ModifierKeys == Keys.None && Touche == Keys.Escape)
            {
                Dispose(true);
                return true;
            }

            if (Form.ModifierKeys == Keys.None && Touche == Keys.Enter)
            {
                if (ConfirmButton.Enabled)
                {
                    ConfirmButton.PerformClick();
                    return true;
                }
            }

            return base.ProcessDialogKey(Touche);
        }
    }
}
