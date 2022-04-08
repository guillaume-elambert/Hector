namespace Hector
{
    partial class SousFamilleForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.NomLabel = new System.Windows.Forms.Label();
            this.FamilleLabel = new System.Windows.Forms.Label();
            this.ReferenceLabel = new System.Windows.Forms.Label();
            this.RefTextBox = new System.Windows.Forms.TextBox();
            this.NomTextBox = new System.Windows.Forms.TextBox();
            this.FamilleComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(343, 183);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.NomLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.FamilleLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ReferenceLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.RefTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.NomTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.FamilleComboBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(322, 155);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // NomLabel
            // 
            this.NomLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NomLabel.AutoSize = true;
            this.NomLabel.Location = new System.Drawing.Point(2, 122);
            this.NomLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NomLabel.Name = "NomLabel";
            this.NomLabel.Size = new System.Drawing.Size(29, 13);
            this.NomLabel.TabIndex = 4;
            this.NomLabel.Text = "Nom";
            // 
            // FamilleLabel
            // 
            this.FamilleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FamilleLabel.AutoSize = true;
            this.FamilleLabel.Location = new System.Drawing.Point(2, 19);
            this.FamilleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FamilleLabel.Name = "FamilleLabel";
            this.FamilleLabel.Size = new System.Drawing.Size(39, 13);
            this.FamilleLabel.TabIndex = 1;
            this.FamilleLabel.Text = "Famille";
            // 
            // ReferenceLabel
            // 
            this.ReferenceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ReferenceLabel.AutoSize = true;
            this.ReferenceLabel.Location = new System.Drawing.Point(2, 70);
            this.ReferenceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ReferenceLabel.Name = "ReferenceLabel";
            this.ReferenceLabel.Size = new System.Drawing.Size(57, 13);
            this.ReferenceLabel.TabIndex = 5;
            this.ReferenceLabel.Text = "Réference";
            // 
            // RefTextBox
            // 
            this.RefTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RefTextBox.Enabled = false;
            this.RefTextBox.Location = new System.Drawing.Point(88, 66);
            this.RefTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RefTextBox.Name = "RefTextBox";
            this.RefTextBox.ReadOnly = true;
            this.RefTextBox.Size = new System.Drawing.Size(226, 20);
            this.RefTextBox.TabIndex = 6;
            // 
            // NomTextBox
            // 
            this.NomTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NomTextBox.Location = new System.Drawing.Point(88, 118);
            this.NomTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NomTextBox.Name = "NomTextBox";
            this.NomTextBox.Size = new System.Drawing.Size(226, 20);
            this.NomTextBox.TabIndex = 7;
            this.NomTextBox.TextChanged += new System.EventHandler(this.NomTextBox_TextChanged);
            // 
            // FamilleComboBox
            // 
            this.FamilleComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.FamilleComboBox.FormattingEnabled = true;
            this.FamilleComboBox.Location = new System.Drawing.Point(88, 15);
            this.FamilleComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.FamilleComboBox.Name = "FamilleComboBox";
            this.FamilleComboBox.Size = new System.Drawing.Size(226, 21);
            this.FamilleComboBox.TabIndex = 8;
            this.FamilleComboBox.SelectedValueChanged += new System.EventHandler(this.FamilleComboBox_SelectedValueChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.ConfirmButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 183);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(343, 39);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConfirmButton.Location = new System.Drawing.Point(10, 10);
            this.ConfirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(322, 20);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // SousFamilleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 222);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.MaximumSize = new System.Drawing.Size(359, 261);
            this.MinimumSize = new System.Drawing.Size(359, 261);
            this.Name = "SousFamilleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Créer / Modifier une sous-famille";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label NomLabel;
        private System.Windows.Forms.Label FamilleLabel;
        private System.Windows.Forms.Label ReferenceLabel;
        private System.Windows.Forms.TextBox RefTextBox;
        private System.Windows.Forms.TextBox NomTextBox;
        private System.Windows.Forms.ComboBox FamilleComboBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button ConfirmButton;
    }
}