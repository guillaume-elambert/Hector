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
            this.BoutonAjout = new System.Windows.Forms.Button();
            this.BoutonEcrasement = new System.Windows.Forms.Button();
            this.GroupeBoutonsIntégration.SuspendLayout();
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
            this.ContenuFichierTextBox.Location = new System.Drawing.Point(9, 60);
            this.ContenuFichierTextBox.Multiline = true;
            this.ContenuFichierTextBox.Name = "ContenuFichierTextBox";
            this.ContenuFichierTextBox.ReadOnly = true;
            this.ContenuFichierTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContenuFichierTextBox.Size = new System.Drawing.Size(463, 219);
            this.ContenuFichierTextBox.TabIndex = 1;
            // 
            // NomFichierTextBox
            // 
            this.NomFichierTextBox.Location = new System.Drawing.Point(178, 14);
            this.NomFichierTextBox.Name = "NomFichierTextBox";
            this.NomFichierTextBox.ReadOnly = true;
            this.NomFichierTextBox.Size = new System.Drawing.Size(294, 20);
            this.NomFichierTextBox.TabIndex = 2;
            // 
            // GroupeBoutonsIntégration
            // 
            this.GroupeBoutonsIntégration.Controls.Add(this.BoutonEcrasement);
            this.GroupeBoutonsIntégration.Controls.Add(this.BoutonAjout);
            this.GroupeBoutonsIntégration.Location = new System.Drawing.Point(9, 295);
            this.GroupeBoutonsIntégration.Name = "GroupeBoutonsIntégration";
            this.GroupeBoutonsIntégration.Size = new System.Drawing.Size(463, 54);
            this.GroupeBoutonsIntégration.TabIndex = 5;
            this.GroupeBoutonsIntégration.TabStop = false;
            this.GroupeBoutonsIntégration.Text = "Intégration dans la base de données";
            this.GroupeBoutonsIntégration.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // BoutonAjout
            // 
            this.BoutonAjout.Enabled = false;
            this.BoutonAjout.Location = new System.Drawing.Point(28, 23);
            this.BoutonAjout.Name = "BoutonAjout";
            this.BoutonAjout.Size = new System.Drawing.Size(179, 23);
            this.BoutonAjout.TabIndex = 0;
            this.BoutonAjout.Text = "Ajouter";
            this.BoutonAjout.UseVisualStyleBackColor = true;
            // 
            // BoutonEcrasement
            // 
            this.BoutonEcrasement.Enabled = false;
            this.BoutonEcrasement.Location = new System.Drawing.Point(250, 23);
            this.BoutonEcrasement.Name = "BoutonEcrasement";
            this.BoutonEcrasement.Size = new System.Drawing.Size(179, 23);
            this.BoutonEcrasement.TabIndex = 1;
            this.BoutonEcrasement.Text = "Écrasement";
            this.BoutonEcrasement.UseVisualStyleBackColor = true;
            // 
            // FenetreImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.GroupeBoutonsIntégration);
            this.Controls.Add(this.NomFichierTextBox);
            this.Controls.Add(this.ContenuFichierTextBox);
            this.Controls.Add(this.BoutonImporter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FenetreImporter";
            this.Text = "Importer";
            this.GroupeBoutonsIntégration.ResumeLayout(false);
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
    }
}