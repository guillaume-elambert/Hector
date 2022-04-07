
namespace Hector
{
    partial class ArticleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.RefTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboMarque = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PrixTextBox = new System.Windows.Forms.TextBox();
            this.QuantiteLabel = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.BoutonConfirm = new System.Windows.Forms.Button();
            this.ButtonAnnuler = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Référence de l\'article";
            // 
            // RefTextBox
            // 
            this.RefTextBox.Location = new System.Drawing.Point(126, 22);
            this.RefTextBox.Name = "RefTextBox";
            this.RefTextBox.Size = new System.Drawing.Size(201, 20);
            this.RefTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Marque";
            // 
            // ComboMarque
            // 
            this.ComboMarque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMarque.FormattingEnabled = true;
            this.ComboMarque.Location = new System.Drawing.Point(126, 71);
            this.ComboMarque.Name = "ComboMarque";
            this.ComboMarque.Size = new System.Drawing.Size(201, 21);
            this.ComboMarque.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sous-Famille";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(126, 124);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Prix de l\'article";
            // 
            // PrixTextBox
            // 
            this.PrixTextBox.Location = new System.Drawing.Point(126, 175);
            this.PrixTextBox.Name = "PrixTextBox";
            this.PrixTextBox.Size = new System.Drawing.Size(201, 20);
            this.PrixTextBox.TabIndex = 7;
            // 
            // QuantiteLabel
            // 
            this.QuantiteLabel.AutoSize = true;
            this.QuantiteLabel.Location = new System.Drawing.Point(13, 232);
            this.QuantiteLabel.Name = "QuantiteLabel";
            this.QuantiteLabel.Size = new System.Drawing.Size(47, 13);
            this.QuantiteLabel.TabIndex = 8;
            this.QuantiteLabel.Text = "Quantité";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(126, 230);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(201, 20);
            this.numericUpDown1.TabIndex = 9;
            // 
            // BoutonConfirm
            // 
            this.BoutonConfirm.Location = new System.Drawing.Point(311, 415);
            this.BoutonConfirm.Name = "BoutonConfirm";
            this.BoutonConfirm.Size = new System.Drawing.Size(161, 23);
            this.BoutonConfirm.TabIndex = 10;
            this.BoutonConfirm.Text = "Confirmer";
            this.BoutonConfirm.UseVisualStyleBackColor = true;
            // 
            // ButtonAnnuler
            // 
            this.ButtonAnnuler.Location = new System.Drawing.Point(144, 415);
            this.ButtonAnnuler.Name = "ButtonAnnuler";
            this.ButtonAnnuler.Size = new System.Drawing.Size(161, 23);
            this.ButtonAnnuler.TabIndex = 11;
            this.ButtonAnnuler.Text = "Annuler";
            this.ButtonAnnuler.UseVisualStyleBackColor = true;
            // 
            // ArticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 450);
            this.Controls.Add(this.ButtonAnnuler);
            this.Controls.Add(this.BoutonConfirm);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.QuantiteLabel);
            this.Controls.Add(this.PrixTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboMarque);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RefTextBox);
            this.Controls.Add(this.label1);
            this.Name = "ArticleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajout ou Modification d\'un article";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RefTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboMarque;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PrixTextBox;
        private System.Windows.Forms.Label QuantiteLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button BoutonConfirm;
        private System.Windows.Forms.Button ButtonAnnuler;
    }
}