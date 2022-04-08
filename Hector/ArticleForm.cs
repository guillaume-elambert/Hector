using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hector
{
    /// <summary>
    /// Formulaire pour la modification ou la création d'un Article.
    /// </summary>
    internal partial class ArticleForm : Form
    {
        /// <summary>
        /// Les types d'action possible
        /// </summary>
        enum ModeEnum
        {
            Add,
            Edit
        }


        /// <summary>
        /// Dictionnaire des Marques
        /// </summary>
        private Dictionary<string, Marque> Marques;
        /// <summary>
        /// Dictionnaire des SousFamilles;
        /// </summary>
        private Dictionary<string, SousFamille> SousFamilles;
        /// <summary>
        /// Dictionnaire des Familles.
        /// </summary>
        private Dictionary<string, Famille> Familles;


        /// <summary>
        /// L'article à créer ou modifier
        /// </summary>
        private Article Article;

        /// <summary>
        /// L'objet de connexeion vers la BDD
        /// </summary>
        private ConnexionBDD Connexion;

        /// <summary>
        /// L'objet DAO des Articles
        /// </summary>
        ArticleDAO ArticleDAO;

        /// <summary>
        /// L'action à effectuer
        /// </summary>
        private ModeEnum Mode;



        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion vers la BDD</param>
        /// <param name="Marques">La liste des Marques disponibles</param>
        /// <param name="SousFamilles">La liste des SousFamilles disponibles</param>
        /// <param name="Familles">La liste des Familles disponibles</param>
        /// <param name="SousFamille">La sous-famille par défaut de l'article</param> 
        /// <param name="Marque">La marque par défaut de l'article.</param>
        /// <param name="Article">L'article à modifier</param>
        public ArticleForm(ConnexionBDD Connexion, Dictionary<string, Marque> Marques, Dictionary<string, SousFamille> SousFamilles, Dictionary<string, Famille> Familles, SousFamille SousFamille = null, Marque Marque = null, Article Article = null)
        {
            InitializeComponent();

            //Initialisation des variables
            this.Connexion = Connexion;
            this.Marques = Marques;
            this.SousFamilles = SousFamilles;
            this.Familles = Familles;
            this.Article = Article;


            ArticleDAO = new ArticleDAO(Connexion);

            ConfirmButton.Enabled = false;


            // Si l'article est null ==> création d'un nouvel Article
            if (Article == null)
            {
                this.Article = new Article();
                Text = "Créer un nouvel Article";
                ConfirmButton.Text = "Ajouter l'Article";
                RefTextBox.ReadOnly = false;
                Mode = ModeEnum.Add;
            }
            // Sinon, récupérer l'Article avec un DAO et remplir les différents champs avec les valeurs de l'Article
            else
            {
                Text = "Modifier l'Article [" + this.Article.RefArticle + "]";
                ConfirmButton.Text = "Modifier l'Article";
                Mode = ModeEnum.Edit;

                ArticleDAO ArticleDAO = new ArticleDAO(Connexion);
                ChargerArticle(this.Article);
            }

            InitialiserComboBox(SousFamille, Marque);
        }


        /// <summary>
        /// Charge les données de chaque ComboBox (ie : La liste des familles, La liste des sous familles de cette famille et la liste des marques)
        /// </summary>
        public void InitialiserComboBox(SousFamille SousFamille = null, Marque Marque = null)
        {
            //On lie les ComboBox avec les dictionnaires de familles, sous familles et marques
            FamilleComboBox.DataSource = new BindingSource(Familles.Values.ToArray(), null);
            MarqueComboBox.DataSource = new BindingSource(Marques.Values.ToArray(), null);
            SousFamilleComboBox.DataSource = new BindingSource(SousFamilles.Values.ToArray(), null);


            //Si on modifie un Article, on initialise la valeur de la comboBox correspondante à la famille originale de l'Article
            if (Mode == ModeEnum.Edit)
            {
                //On ajoute toutes les sous-familles de la famille de l'Article dans la ComboBox
                SousFamilleComboBox.DataSource = new BindingSource(Article.SousFamille.Famille.SousFamilles.Values.ToArray(), null);

                //On initialise les choix par défaut des ComboBox des familles, sous-familles et marques
                FamilleComboBox.SelectedItem = Article.SousFamille.Famille;
                SousFamilleComboBox.SelectedItem = Article.SousFamille;
                MarqueComboBox.SelectedItem = Article.Marque;
                return;
            }


            //Entrée : On a passé une sous-famille en paramètre du constructeur
            //      => On selectionne la sous-famille et sa famille dans leur ComboBox respectifs
            if (SousFamille != null)
            {
                FamilleComboBox.SelectedItem = SousFamille.Famille;
                SousFamilleComboBox.DataSource = new BindingSource(SousFamille.Famille.SousFamilles.Values.ToArray(), null);
                SousFamilleComboBox.SelectedItem = SousFamille;
            }


            //Entrée : On a passé une marque en paramètre du constructeur
            //      => On selectionne la marque dans sa ComboBox
            if (Marque != null)
            {
                MarqueComboBox.SelectedItem = Marque;
            }
        }


        /// <summary>
        /// Initialise les champs "Référence", "Description", "Prix HT" et quantité en fonction d'un Article
        /// </summary>
        /// <param name="Article">Article à partir duquel initialiser les champs du formulaire</param>
        public void ChargerArticle(Article Article)
        {
            RefTextBox.Text = Article.RefArticle;
            DescTextBox.Text = Article.Description;
            PrixNumericUpDown.Value = Convert.ToDecimal(Article.Prix);
            QuantiteNumericUpDown.Value = Article.Quantite;
        }


        /// <summary>
        /// Valide la création ou la modification d'un Article dans la BDD.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void ConfirmButton_Click(object Emetteur, EventArgs Evenement)
        {
            //On initialise les valeurs de l'article
            Article.RefArticle = RefTextBox.Text;
            Article.Description = DescTextBox.Text;
            Article.Prix = Convert.ToSingle(PrixNumericUpDown.Value);
            Article.Quantite = Convert.ToInt32(QuantiteNumericUpDown.Value);

            Article.Marque = (Marque)MarqueComboBox.SelectedItem;
            Article.SousFamille = (SousFamille)SousFamilleComboBox.SelectedItem;

            //Entrée : On veut modifier un article
            if (Mode == ModeEnum.Edit)
            {
                ArticleDAO.Modifier(Article);
            }
            //Sino c'est qu'on veut l'ajouter
            else
            {
                ArticleDAO.Inserer(Article);
            }

            Close();
        }


        /// <summary>
        /// Méthode appelée lorsque l'on modifie le champ du nom.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void RefTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            ValidationFormulaire();
        }


        /// <summary>
        /// Méthode appelée lorsque l'on modifie le champ de la description.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void DescTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            ValidationFormulaire();
        }


        /// <summary>
        /// Méthode appelée lorsque l'on modifie le champ de la famille.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void SousFamilleComboBox_SelectedIndexChanged(object Emetteur, EventArgs Evenement)
        {
            ValidationFormulaire();
        }


        /// <summary>
        /// Méthode appelée lorsque l'on modifie le champ de la marque.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void MarqueComboBox_SelectedIndexChanged(object Emetteur, EventArgs Evenement)
        {
            ValidationFormulaire();
        }


        /// <summary>
        /// Méthode appelée lorsque l'on modifie le champ du prix.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void PrixNumericUpDown_ValueChanged(object Emetteur, EventArgs Evenement)
        {
            ValidationFormulaire();
        }


        /// <summary>
        /// Méthode appelée lorsque l'on modifie le champ de la quantité.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void QuantiteNumericUpDown_ValueChanged(object Emetteur, EventArgs Evenement)
        {
            ValidationFormulaire();
        }


        /// <summary>
        /// Recharge la liste de sous-famille de la comboBox correspondante lorsque la famille sélectionnée change.
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void FamilleComboBox_SelectedIndexChanged(object Emetteur, EventArgs Evenement)
        {
            Famille FamilleSelectionnee = (Famille)FamilleComboBox.SelectedItem;

            if (FamilleSelectionnee == null) return;

            //On ajoute toutes les sous-familles de la famille séléctionnée dans la ComboBox
            SousFamilleComboBox.DataSource = new BindingSource(FamilleSelectionnee.SousFamilles.Values.ToArray(), null);
            SousFamilleComboBox.SelectedIndex = 0;

            ValidationFormulaire();
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


        /// <summary>
        /// Vérifie si chaque champs a une valeur valide
        /// Desc         : Champ non null
        /// Famille      : Champ non null
        /// Sous Famille : Champ non null
        /// Marque       : Champ non null
        /// Prix         : float (virgule et pas point)
        /// </summary>
        /// <returns>Vrai si tout les champs sont valides, faux sinon.</returns>
        public bool VerifierChamps()
        {
            if (RefTextBox.Text == "" || RefTextBox.Text.Length > 8)
            {
                return false;
            }

            if (DescTextBox.Text == "" || RefTextBox.Text.Length > 150)
            {
                return false;
            }

            if (FamilleComboBox.SelectedItem == null)
            {
                return false;
            }

            if (SousFamilleComboBox.SelectedItem == null)
            {
                return false;
            }

            if (MarqueComboBox.SelectedItem == null)
            {
                return false;
            }

            if (PrixNumericUpDown.Value < 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Active ou désactive le bouton de validation des modifications en fonction de la validité de chacun des champs.
        /// </summary>
        public void ValidationFormulaire()
        {
            if (VerifierChamps())
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
