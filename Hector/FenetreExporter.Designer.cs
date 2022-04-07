namespace Hector
{
    partial class FenetreExporter
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
            this.NomFichierTextBox = new System.Windows.Forms.TextBox();
            this.BoutonChoixFichier = new System.Windows.Forms.Button();
            this.BoutonExporter = new System.Windows.Forms.Button();
            this.BoutonAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NomFichierTextBox
            // 
            this.NomFichierTextBox.Location = new System.Drawing.Point(177, 17);
            this.NomFichierTextBox.Name = "NomFichierTextBox";
            this.NomFichierTextBox.ReadOnly = true;
            this.NomFichierTextBox.Size = new System.Drawing.Size(481, 20);
            this.NomFichierTextBox.TabIndex = 4;
            // 
            // BoutonChoixFichier
            // 
            this.BoutonChoixFichier.Location = new System.Drawing.Point(8, 15);
            this.BoutonChoixFichier.Name = "BoutonChoixFichier";
            this.BoutonChoixFichier.Size = new System.Drawing.Size(162, 23);
            this.BoutonChoixFichier.TabIndex = 3;
            this.BoutonChoixFichier.Text = "Séléctionner un fichier .csv";
            this.BoutonChoixFichier.UseVisualStyleBackColor = true;
            this.BoutonChoixFichier.Click += new System.EventHandler(this.BoutonChoixFichier_Click);
            // 
            // BoutonExporter
            // 
            this.BoutonExporter.Enabled = false;
            this.BoutonExporter.Location = new System.Drawing.Point(549, 59);
            this.BoutonExporter.Name = "BoutonExporter";
            this.BoutonExporter.Size = new System.Drawing.Size(109, 23);
            this.BoutonExporter.TabIndex = 5;
            this.BoutonExporter.Text = "Exporter";
            this.BoutonExporter.UseVisualStyleBackColor = true;
            this.BoutonExporter.Click += new System.EventHandler(this.BoutonExporter_Click);
            // 
            // BoutonAnnuler
            // 
            this.BoutonAnnuler.Location = new System.Drawing.Point(430, 59);
            this.BoutonAnnuler.Name = "BoutonAnnuler";
            this.BoutonAnnuler.Size = new System.Drawing.Size(109, 23);
            this.BoutonAnnuler.TabIndex = 6;
            this.BoutonAnnuler.Text = "Annuler";
            this.BoutonAnnuler.UseVisualStyleBackColor = true;
            this.BoutonAnnuler.Click += new System.EventHandler(this.BoutonAnnuler_Click);
            // 
            // FenetreExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 92);
            this.Controls.Add(this.BoutonAnnuler);
            this.Controls.Add(this.BoutonExporter);
            this.Controls.Add(this.NomFichierTextBox);
            this.Controls.Add(this.BoutonChoixFichier);
            this.Name = "FenetreExporter";
            this.Text = "FenetreExporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NomFichierTextBox;
        private System.Windows.Forms.Button BoutonChoixFichier;
        private System.Windows.Forms.Button BoutonExporter;
        private System.Windows.Forms.Button BoutonAnnuler;
    }
}