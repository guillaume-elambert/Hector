using System;
using System.Windows.Forms;

namespace Hector
{
    /// <summary>
    /// Formulaire pour la modification ou la création d'une Famille.
    /// </summary>
    internal partial class FamilleForm : Form
    {
        /// <summary>
        /// La connxion vers la BDD
        /// </summary>
        ConnexionBDD Connexion;
        /// <summary>
        /// Le DAO des familles
        /// </summary>
        FamilleDAO FamilleDAO;
        /// <summary>
        /// La famille
        /// </summary>
        Famille Famille;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">La connexion vers la BDD</param>
        /// <param name="Famille">La famille à modifier</param>
        public FamilleForm(ConnexionBDD Connexion, Famille Famille = null)
        {
            InitializeComponent();

            //Initialisation des variables
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
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
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
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
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
