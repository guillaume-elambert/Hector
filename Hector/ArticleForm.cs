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
    internal partial class ArticleForm : Form
    {
        enum ModeEnum
        {
            Add,
            Edit
        }
        

        ArticleDAO ArticleDAO;
        FamilleDAO FamilleDAO;
        SousFamilleDAO SousFamilleDAO;
        MarqueDAO MarqueDAO;


        /// <summary>
        /// Dictionnaire des Articles.
        /// </summary>
        private Dictionary<string, Article> Articles;
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

        

        private Article Article;
        private ConnexionBDD Connexion;
        private ModeEnum Mode;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion vers la BDD</param>
        /// <param name="Article">L'article</param>
        public ArticleForm(ConnexionBDD Connexion, Dictionary<string, Article> Articles, Dictionary<string, Marque> Marques, Dictionary<string, SousFamille> SousFamilles, Dictionary<string, Famille> Familles, Article Article)
        {
            InitializeComponent();
            
            this.Connexion = Connexion;
            this.Articles = Articles;
            this.Marques = Marques;
            this.SousFamilles = SousFamilles;
            this.Familles = Familles;

            
            ArticleDAO = new ArticleDAO(Connexion);
            FamilleDAO = new FamilleDAO(Connexion);
            SousFamilleDAO = new SousFamilleDAO(Connexion);
            MarqueDAO = new MarqueDAO(Connexion);
            

            //Si l'article est null, c'est qu'on veut ajouter un article
            if (Article == null)
            {
                Article = new Article();
            }
            this.Article = Article;

            ConfirmButton.Enabled = false;

            
            // Si RefArticle est vide ==> création d'un nouvel Article
            if (this.Article.RefArticle == null)
            {
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
            
            InitialiserComboBox();
        }

        /// <summary>
        /// Charge les données de chaque ComboBox (ie : La liste des familles, La liste des sous familles de cette famille et la liste des marques)
        /// </summary>
        public void InitialiserComboBox()
        {
            FamilleComboBox.DataSource = new BindingSource(Familles.Values.ToArray(), null);
            MarqueComboBox.DataSource = new BindingSource(Marques.Values.ToArray(), null);
            SousFamilleComboBox.DataSource = new BindingSource(SousFamilles.Values.ToArray(), null);

            //Si on modifie un Article, on initialise la valeur de la comboBox correspondante à la famille originale de l'Article
            if (Mode == ModeEnum.Edit)
            {
                //On ajoute toutes les sous-familles de la famille de l'Article dans la ComboBox
                SousFamilleComboBox.DataSource = new BindingSource(Article.SousFamille.Famille.SousFamilles.Values.ToArray(), null);

                //On initialise les choix par défaut des ComboBox des familles, sous-familles et marques
                FamilleComboBox.SelectedIndex = FamilleComboBox.Items.IndexOf(Article.SousFamille.Famille);
                SousFamilleComboBox.SelectedIndex = SousFamilleComboBox.Items.IndexOf(Article.SousFamille);
                MarqueComboBox.SelectedIndex = MarqueComboBox.Items.IndexOf(Article.Marque);
            }
        }
        

        /// <summary>
        /// Initialise les champs "Référence", "Description", "Prix HT" et quantité en fonction d'un Article
        /// </summary>
        /// <param name="Article">Article à partir duquel initialiser les champs énoncés plus haut.</param>
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
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void ConfirmButton_Click(object Emetteur, EventArgs Evenement)
        {
            Article.RefArticle = RefTextBox.Text;
            Article.Description = DescTextBox.Text;
            Article.Prix = Convert.ToSingle(PrixNumericUpDown.Value);
            Article.Quantite = Convert.ToInt32(QuantiteNumericUpDown.Value);
            
            Article.Marque = (Marque)MarqueComboBox.SelectedItem;
            Article.SousFamille = (SousFamille)SousFamilleComboBox.SelectedItem;

            if(Mode == ModeEnum.Edit)
            {
                ArticleDAO.Modifier(Article);
            }
            else
            {
                ArticleDAO.Inserer(Article);
            }
            
            Close();
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
        public bool AreFieldsValid()
        {
            if (string.Compare(DescTextBox.Text, "") == 0)
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

            if(PrixNumericUpDown.Value < 0){
                return false;
            }

            return true;
        }

        /// <summary>
        /// Active ou désactive le bouton de validation des modifications en fonction de la validité de chacun des champs.
        /// </summary>
        public void CheckFields()
        {
            if (AreFieldsValid())
            {
                ConfirmButton.Enabled = true;
            }
            else
            {
                ConfirmButton.Enabled = false;
            }
        }



        private void RefTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            CheckFields();
        }

        private void DescTextBox_TextChanged(object Emetteur, EventArgs Evenement)
        {
            CheckFields();
        }

        private void SousFamilleComboBox_SelectedIndexChanged(object Emetteur, EventArgs Evenement)
        {
            CheckFields();
        }

        private void MarqueComboBox_SelectedIndexChanged(object Emetteur, EventArgs Evenement)
        {
            CheckFields();
        }


        private void PrixNumericUpDown_ValueChanged(object Emetteur, EventArgs Evenement)
        {
            CheckFields();
        }

        private void QuantiteNumericUpDown_ValueChanged(object Emetteur, EventArgs Evenement)
        {
            CheckFields();
        }

        /// <summary>
        /// Recharge la liste de sous-famille de la comboBox correspondante lorsque la famille sélectionnée change.
        /// </summary>
        /// <param name="Emetteur"></param>
        /// <param name="Evenement"></param>
        private void FamilleComboBox_SelectedIndexChanged(object Emetteur, EventArgs Evenement)
        {
            Famille FamilleSelectionnee = (Famille)FamilleComboBox.SelectedItem;

            if (FamilleSelectionnee == null) return;

            SousFamilleComboBox.DataSource = new BindingSource(FamilleSelectionnee.SousFamilles.Values.ToArray(), null);
            SousFamilleComboBox.SelectedIndex = 0;
            
            CheckFields();
        }
    }
}
