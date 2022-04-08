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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ChampsFormulaire = new System.Windows.Forms.TableLayoutPanel();
            this.QuantiteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MarqueComboBox = new System.Windows.Forms.ComboBox();
            this.QuantiteLabel = new System.Windows.Forms.Label();
            this.SousFamilleComboBox = new System.Windows.Forms.ComboBox();
            this.PrixLabel = new System.Windows.Forms.Label();
            this.FamilleComboBox = new System.Windows.Forms.ComboBox();
            this.DescTextBox = new System.Windows.Forms.TextBox();
            this.MarqueLabel = new System.Windows.Forms.Label();
            this.SousFamilleLabel = new System.Windows.Forms.Label();
            this.RefTextBox = new System.Windows.Forms.TextBox();
            this.FamilleLabel = new System.Windows.Forms.Label();
            this.DescLabel = new System.Windows.Forms.Label();
            this.RefLabel = new System.Windows.Forms.Label();
            this.PrixNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.ChampsFormulaire.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantiteNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrixNumericUpDown)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ChampsFormulaire);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(341, 327);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ChampsFormulaire
            // 
            this.ChampsFormulaire.ColumnCount = 2;
            this.ChampsFormulaire.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ChampsFormulaire.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ChampsFormulaire.Controls.Add(this.QuantiteNumericUpDown, 1, 6);
            this.ChampsFormulaire.Controls.Add(this.MarqueComboBox, 1, 4);
            this.ChampsFormulaire.Controls.Add(this.QuantiteLabel, 0, 6);
            this.ChampsFormulaire.Controls.Add(this.SousFamilleComboBox, 1, 3);
            this.ChampsFormulaire.Controls.Add(this.PrixLabel, 0, 5);
            this.ChampsFormulaire.Controls.Add(this.FamilleComboBox, 1, 2);
            this.ChampsFormulaire.Controls.Add(this.DescTextBox, 1, 1);
            this.ChampsFormulaire.Controls.Add(this.MarqueLabel, 0, 4);
            this.ChampsFormulaire.Controls.Add(this.SousFamilleLabel, 0, 3);
            this.ChampsFormulaire.Controls.Add(this.RefTextBox, 1, 0);
            this.ChampsFormulaire.Controls.Add(this.FamilleLabel, 0, 2);
            this.ChampsFormulaire.Controls.Add(this.DescLabel, 0, 1);
            this.ChampsFormulaire.Controls.Add(this.RefLabel, 0, 0);
            this.ChampsFormulaire.Controls.Add(this.PrixNumericUpDown, 1, 5);
            this.ChampsFormulaire.Location = new System.Drawing.Point(2, 2);
            this.ChampsFormulaire.Margin = new System.Windows.Forms.Padding(2);
            this.ChampsFormulaire.Name = "ChampsFormulaire";
            this.ChampsFormulaire.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ChampsFormulaire.RowCount = 7;
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.ChampsFormulaire.Size = new System.Drawing.Size(339, 277);
            this.ChampsFormulaire.TabIndex = 2;
            // 
            // QuantiteNumericUpDown
            // 
            this.QuantiteNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuantiteNumericUpDown.Location = new System.Drawing.Point(74, 244);
            this.QuantiteNumericUpDown.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.QuantiteNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.QuantiteNumericUpDown.Name = "QuantiteNumericUpDown";
            this.QuantiteNumericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.QuantiteNumericUpDown.Size = new System.Drawing.Size(261, 20);
            this.QuantiteNumericUpDown.TabIndex = 7;
            this.QuantiteNumericUpDown.ValueChanged += new System.EventHandler(this.QuantiteNumericUpDown_ValueChanged);
            // 
            // MarqueComboBox
            // 
            this.MarqueComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarqueComboBox.FormattingEnabled = true;
            this.MarqueComboBox.Location = new System.Drawing.Point(74, 166);
            this.MarqueComboBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.MarqueComboBox.Name = "MarqueComboBox";
            this.MarqueComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MarqueComboBox.Size = new System.Drawing.Size(261, 21);
            this.MarqueComboBox.TabIndex = 5;
            this.MarqueComboBox.SelectedIndexChanged += new System.EventHandler(this.MarqueComboBox_SelectedIndexChanged);
            // 
            // QuantiteLabel
            // 
            this.QuantiteLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.QuantiteLabel.AutoSize = true;
            this.QuantiteLabel.Location = new System.Drawing.Point(6, 249);
            this.QuantiteLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.QuantiteLabel.Name = "QuantiteLabel";
            this.QuantiteLabel.Size = new System.Drawing.Size(47, 13);
            this.QuantiteLabel.TabIndex = 7;
            this.QuantiteLabel.Text = "Quantité";
            // 
            // SousFamilleComboBox
            // 
            this.SousFamilleComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SousFamilleComboBox.FormattingEnabled = true;
            this.SousFamilleComboBox.Location = new System.Drawing.Point(74, 127);
            this.SousFamilleComboBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.SousFamilleComboBox.Name = "SousFamilleComboBox";
            this.SousFamilleComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SousFamilleComboBox.Size = new System.Drawing.Size(261, 21);
            this.SousFamilleComboBox.TabIndex = 4;
            this.SousFamilleComboBox.SelectedIndexChanged += new System.EventHandler(this.SousFamilleComboBox_SelectedIndexChanged);
            // 
            // PrixLabel
            // 
            this.PrixLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PrixLabel.AutoSize = true;
            this.PrixLabel.Location = new System.Drawing.Point(6, 208);
            this.PrixLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PrixLabel.Name = "PrixLabel";
            this.PrixLabel.Size = new System.Drawing.Size(42, 13);
            this.PrixLabel.TabIndex = 6;
            this.PrixLabel.Text = "Prix HT";
            // 
            // FamilleComboBox
            // 
            this.FamilleComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FamilleComboBox.FormattingEnabled = true;
            this.FamilleComboBox.Location = new System.Drawing.Point(74, 88);
            this.FamilleComboBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.FamilleComboBox.Name = "FamilleComboBox";
            this.FamilleComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FamilleComboBox.Size = new System.Drawing.Size(261, 21);
            this.FamilleComboBox.TabIndex = 3;
            this.FamilleComboBox.SelectedIndexChanged += new System.EventHandler(this.FamilleComboBox_SelectedIndexChanged);
            // 
            // DescTextBox
            // 
            this.DescTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescTextBox.Location = new System.Drawing.Point(74, 49);
            this.DescTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DescTextBox.Size = new System.Drawing.Size(261, 20);
            this.DescTextBox.TabIndex = 2;
            this.DescTextBox.TextChanged += new System.EventHandler(this.DescTextBox_TextChanged);
            // 
            // MarqueLabel
            // 
            this.MarqueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MarqueLabel.AutoSize = true;
            this.MarqueLabel.Location = new System.Drawing.Point(6, 169);
            this.MarqueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MarqueLabel.Name = "MarqueLabel";
            this.MarqueLabel.Size = new System.Drawing.Size(43, 13);
            this.MarqueLabel.TabIndex = 5;
            this.MarqueLabel.Text = "Marque";
            // 
            // SousFamilleLabel
            // 
            this.SousFamilleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SousFamilleLabel.AutoSize = true;
            this.SousFamilleLabel.Location = new System.Drawing.Point(6, 130);
            this.SousFamilleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SousFamilleLabel.Name = "SousFamilleLabel";
            this.SousFamilleLabel.Size = new System.Drawing.Size(66, 13);
            this.SousFamilleLabel.TabIndex = 4;
            this.SousFamilleLabel.Text = "Sous-Famille";
            // 
            // RefTextBox
            // 
            this.RefTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RefTextBox.Location = new System.Drawing.Point(74, 10);
            this.RefTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.RefTextBox.Name = "RefTextBox";
            this.RefTextBox.ReadOnly = true;
            this.RefTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RefTextBox.Size = new System.Drawing.Size(261, 20);
            this.RefTextBox.TabIndex = 1;
            this.RefTextBox.TextChanged += new System.EventHandler(this.RefTextBox_TextChanged);
            // 
            // FamilleLabel
            // 
            this.FamilleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FamilleLabel.AutoSize = true;
            this.FamilleLabel.Location = new System.Drawing.Point(6, 91);
            this.FamilleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FamilleLabel.Name = "FamilleLabel";
            this.FamilleLabel.Size = new System.Drawing.Size(39, 13);
            this.FamilleLabel.TabIndex = 3;
            this.FamilleLabel.Text = "Famille";
            // 
            // DescLabel
            // 
            this.DescLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DescLabel.AutoSize = true;
            this.DescLabel.Location = new System.Drawing.Point(6, 52);
            this.DescLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DescLabel.Name = "DescLabel";
            this.DescLabel.Size = new System.Drawing.Size(60, 13);
            this.DescLabel.TabIndex = 2;
            this.DescLabel.Text = "Description";
            // 
            // RefLabel
            // 
            this.RefLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RefLabel.AutoSize = true;
            this.RefLabel.Location = new System.Drawing.Point(6, 13);
            this.RefLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RefLabel.Name = "RefLabel";
            this.RefLabel.Size = new System.Drawing.Size(57, 13);
            this.RefLabel.TabIndex = 1;
            this.RefLabel.Text = "Réference";
            // 
            // PrixNumericUpDown
            // 
            this.PrixNumericUpDown.DecimalPlaces = 2;
            this.PrixNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrixNumericUpDown.Location = new System.Drawing.Point(74, 205);
            this.PrixNumericUpDown.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.PrixNumericUpDown.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.PrixNumericUpDown.Name = "PrixNumericUpDown";
            this.PrixNumericUpDown.Size = new System.Drawing.Size(261, 20);
            this.PrixNumericUpDown.TabIndex = 6;
            this.PrixNumericUpDown.ValueChanged += new System.EventHandler(this.PrixNumericUpDown_ValueChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.ConfirmButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 286);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(341, 41);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConfirmButton.Location = new System.Drawing.Point(10, 10);
            this.ConfirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(319, 20);
            this.ConfirmButton.TabIndex = 8;
            this.ConfirmButton.Text = "Ajouter";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // ArticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 327);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(357, 366);
            this.MinimumSize = new System.Drawing.Size(357, 366);
            this.Name = "ArticleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter / Modifier un article";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ChampsFormulaire.ResumeLayout(false);
            this.ChampsFormulaire.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantiteNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrixNumericUpDown)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.TableLayoutPanel ChampsFormulaire;
        private System.Windows.Forms.Label QuantiteLabel;
        private System.Windows.Forms.Label PrixLabel;
        private System.Windows.Forms.Label MarqueLabel;
        private System.Windows.Forms.Label SousFamilleLabel;
        private System.Windows.Forms.Label FamilleLabel;
        private System.Windows.Forms.Label DescLabel;
        private System.Windows.Forms.Label RefLabel;
        private System.Windows.Forms.ComboBox MarqueComboBox;
        private System.Windows.Forms.ComboBox SousFamilleComboBox;
        private System.Windows.Forms.TextBox DescTextBox;
        private System.Windows.Forms.ComboBox FamilleComboBox;
        private System.Windows.Forms.TextBox RefTextBox;
        private System.Windows.Forms.NumericUpDown QuantiteNumericUpDown;
        private System.Windows.Forms.NumericUpDown PrixNumericUpDown;
    }
}