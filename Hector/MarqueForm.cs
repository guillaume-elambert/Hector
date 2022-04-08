using System;
using System.Windows.Forms;

namespace Hector
{
    /// <summary>
    /// Formulaire pour la modification ou la création d'une MarqueFamille.
    /// </summary>
    internal partial class MarqueForm : Form
    {
        /// <summary>
        /// Le DAO des Marques.
        /// </summary>
        MarqueDAO MarqueDAO;
        /// <summary>
        /// La connexion vers la BDD
        /// </summary>
        ConnexionBDD Connexion;
        /// <summary>
        /// La marque
        /// </summary>
        Marque Marque;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion vers la BDD</param>
        /// <param name="Marque">La marque à modifier</param>
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
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
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
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void NomTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            if (NomTextBox.Text == "" || NomTextBox.Text.Length > 50)
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
                Close();
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
