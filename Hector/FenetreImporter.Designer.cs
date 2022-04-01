namespace Hector
{
    partial class FenetreImporter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BoutonImporter = new System.Windows.Forms.Button();
            this.ContenuFichierTextBox = new System.Windows.Forms.TextBox();
            this.NomFichierTextBox = new System.Windows.Forms.TextBox();
            this.GroupeBoutonsIntégration = new System.Windows.Forms.GroupBox();
            this.BarreProgressionTotale = new System.Windows.Forms.ProgressBar();
            this.BarreProgressionIntermediaire = new System.Windows.Forms.ProgressBar();
            this.BoutonEcrasement = new System.Windows.Forms.Button();
            this.BoutonAjout = new System.Windows.Forms.Button();
            this.GroupeContenuFichier = new System.Windows.Forms.GroupBox();
            this.GroupeBoutonsIntégration.SuspendLayout();
            this.GroupeContenuFichier.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoutonImporter
            // 
            this.BoutonImporter.Location = new System.Drawing.Point(9, 12);
            this.BoutonImporter.Name = "BoutonImporter";
            this.BoutonImporter.Size = new System.Drawing.Size(162, 23);
            this.BoutonImporter.TabIndex = 0;
            this.BoutonImporter.Text = "Séléctionner un fichier .csv";
            this.BoutonImporter.UseVisualStyleBackColor = true;
            this.BoutonImporter.Click += new System.EventHandler(this.BoutonImporter_Click);
            // 
            // ContenuFichierTextBox
            // 
            this.ContenuFichierTextBox.Location = new System.Drawing.Point(6, 19);
            this.ContenuFichierTextBox.Multiline = true;
            this.ContenuFichierTextBox.Name = "ContenuFichierTextBox";
            this.ContenuFichierTextBox.ReadOnly = true;
            this.ContenuFichierTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContenuFichierTextBox.Size = new System.Drawing.Size(638, 223);
            this.ContenuFichierTextBox.TabIndex = 1;
            // 
            // NomFichierTextBox
            // 
            this.NomFichierTextBox.Location = new System.Drawing.Point(178, 14);
            this.NomFichierTextBox.Name = "NomFichierTextBox";
            this.NomFichierTextBox.ReadOnly = true;
            this.NomFichierTextBox.Size = new System.Drawing.Size(481, 20);
            this.NomFichierTextBox.TabIndex = 2;
            // 
            // GroupeBoutonsIntégration
            // 
            this.GroupeBoutonsIntégration.Controls.Add(this.BarreProgressionTotale);
            this.GroupeBoutonsIntégration.Controls.Add(this.BarreProgressionIntermediaire);
            this.GroupeBoutonsIntégration.Controls.Add(this.BoutonEcrasement);
            this.GroupeBoutonsIntégration.Controls.Add(this.BoutonAjout);
            this.GroupeBoutonsIntégration.Location = new System.Drawing.Point(9, 317);
            this.GroupeBoutonsIntégration.Name = "GroupeBoutonsIntégration";
            this.GroupeBoutonsIntégration.Size = new System.Drawing.Size(650, 105);
            this.GroupeBoutonsIntégration.TabIndex = 5;
            this.GroupeBoutonsIntégration.TabStop = false;
            this.GroupeBoutonsIntégration.Text = "Intégration dans la base de données";
            // 
            // BarreProgressionTotale
            // 
            this.BarreProgressionTotale.Enabled = false;
            this.BarreProgressionTotale.Location = new System.Drawing.Point(7, 83);
            this.BarreProgressionTotale.Name = "BarreProgressionTotale";
            this.BarreProgressionTotale.Size = new System.Drawing.Size(637, 12);
            this.BarreProgressionTotale.TabIndex = 3;
            // 
            // BarreProgressionIntermediaire
            // 
            this.BarreProgressionIntermediaire.Enabled = false;
            this.BarreProgressionIntermediaire.Location = new System.Drawing.Point(7, 65);
            this.BarreProgressionIntermediaire.Name = "BarreProgressionIntermediaire";
            this.BarreProgressionIntermediaire.Size = new System.Drawing.Size(637, 12);
            this.BarreProgressionIntermediaire.TabIndex = 2;
            // 
            // BoutonEcrasement
            // 
            this.BoutonEcrasement.Enabled = false;
            this.BoutonEcrasement.Location = new System.Drawing.Point(397, 23);
            this.BoutonEcrasement.Name = "BoutonEcrasement";
            this.BoutonEcrasement.Size = new System.Drawing.Size(180, 23);
            this.BoutonEcrasement.TabIndex = 1;
            this.BoutonEcrasement.Text = "Écraser";
            this.BoutonEcrasement.UseVisualStyleBackColor = true;
            this.BoutonEcrasement.Click += new System.EventHandler(this.BoutonEcrasement_Click);
            // 
            // BoutonAjout
            // 
            this.BoutonAjout.Enabled = false;
            this.BoutonAjout.Location = new System.Drawing.Point(73, 23);
            this.BoutonAjout.Name = "BoutonAjout";
            this.BoutonAjout.Size = new System.Drawing.Size(180, 23);
            this.BoutonAjout.TabIndex = 0;
            this.BoutonAjout.Text = "Ajouter";
            this.BoutonAjout.UseVisualStyleBackColor = true;
            this.BoutonAjout.Click += new System.EventHandler(this.BoutonAjout_Click);
            // 
            // GroupeContenuFichier
            // 
            this.GroupeContenuFichier.Controls.Add(this.ContenuFichierTextBox);
            this.GroupeContenuFichier.Location = new System.Drawing.Point(9, 53);
            this.GroupeContenuFichier.Name = "GroupeContenuFichier";
            this.GroupeContenuFichier.Size = new System.Drawing.Size(650, 248);
            this.GroupeContenuFichier.TabIndex = 6;
            this.GroupeContenuFichier.TabStop = false;
            this.GroupeContenuFichier.Text = "Contenu du fichier";
            // 
            // FenetreImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 434);
            this.Controls.Add(this.GroupeBoutonsIntégration);
            this.Controls.Add(this.NomFichierTextBox);
            this.Controls.Add(this.BoutonImporter);
            this.Controls.Add(this.GroupeContenuFichier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FenetreImporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importer";
            this.GroupeBoutonsIntégration.ResumeLayout(false);
            this.GroupeContenuFichier.ResumeLayout(false);
            this.GroupeContenuFichier.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BoutonImporter;
        private System.Windows.Forms.TextBox ContenuFichierTextBox;
        private System.Windows.Forms.TextBox NomFichierTextBox;
        private System.Windows.Forms.GroupBox GroupeBoutonsIntégration;
        private System.Windows.Forms.Button BoutonEcrasement;
        private System.Windows.Forms.Button BoutonAjout;
        private System.Windows.Forms.GroupBox GroupeContenuFichier;
        private System.Windows.Forms.ProgressBar BarreProgressionTotale;
        private System.Windows.Forms.ProgressBar BarreProgressionIntermediaire;
    }
}