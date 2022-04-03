﻿using System.Windows.Forms;

namespace Hector
{
    partial class FormMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tous les articles");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Stylos, feutres & rollers");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ecriture & Correction", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Papier laser");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Papier", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Familles", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Clairefontaine");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Marques", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ActualiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImporterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExporterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.ArbreArticles = new System.Windows.Forms.TreeView();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.ListView = new System.Windows.Forms.ListView();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FichierToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // FichierToolStripMenuItem
            // 
            this.FichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActualiserToolStripMenuItem,
            this.ImporterToolStripMenuItem,
            this.ExporterToolStripMenuItem});
            this.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem";
            this.FichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.FichierToolStripMenuItem.Text = "Fichier";
            // 
            // ActualiserToolStripMenuItem
            // 
            this.ActualiserToolStripMenuItem.Name = "ActualiserToolStripMenuItem";
            this.ActualiserToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ActualiserToolStripMenuItem.Text = "Actualiser";
            // 
            // ImporterToolStripMenuItem
            // 
            this.ImporterToolStripMenuItem.Name = "ImporterToolStripMenuItem";
            this.ImporterToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ImporterToolStripMenuItem.Text = "Importer";
            this.ImporterToolStripMenuItem.Click += new System.EventHandler(this.ImporterToolStripMenuItem_Click);
            // 
            // ExporterToolStripMenuItem
            // 
            this.ExporterToolStripMenuItem.Name = "ExporterToolStripMenuItem";
            this.ExporterToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ExporterToolStripMenuItem.Text = "Exporter";
            // 
            // StatusStrip
            // 
            this.StatusStrip.Location = new System.Drawing.Point(0, 428);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(800, 22);
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 24);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.ArbreArticles);
            this.SplitContainer.Panel1.Controls.Add(this.TreeView);
            this.SplitContainer.Panel1MinSize = 200;
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.ListView);
            this.SplitContainer.Size = new System.Drawing.Size(800, 404);
            this.SplitContainer.SplitterDistance = 266;
            this.SplitContainer.TabIndex = 2;
            // 
            // ArbreArticles
            // 
            this.ArbreArticles.Location = new System.Drawing.Point(12, 3);
            this.ArbreArticles.Name = "ArbreArticles";
            treeNode1.Checked = true;
            treeNode1.Name = "RacinesArticles";
            treeNode1.Text = "Tous les articles";
            treeNode2.Name = "SousFamilleEC";
            treeNode2.Text = "Stylos, feutres & rollers";
            treeNode3.Name = "EcritureEtCorrection";
            treeNode3.Text = "Ecriture & Correction";
            treeNode4.Name = "SousFamilleP";
            treeNode4.Text = "Papier laser";
            treeNode5.Name = "Papier";
            treeNode5.Text = "Papier";
            treeNode6.Name = "Familles";
            treeNode6.Text = "Familles";
            treeNode7.Name = "Clairefontaine";
            treeNode7.Text = "Clairefontaine";
            treeNode8.Name = "Marques";
            treeNode8.Text = "Marques";
            this.ArbreArticles.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode8});
            this.ArbreArticles.Size = new System.Drawing.Size(251, 398);
            this.ArbreArticles.TabIndex = 1;
            this.ArbreArticles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // TreeView
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Name = "TreeView";
            this.TreeView.Size = new System.Drawing.Size(266, 404);
            this.TreeView.TabIndex = 0;
            // 
            // ListView
            // 
            this.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView.HideSelection = false;
            this.ListView.Location = new System.Drawing.Point(0, 0);
            this.ListView.Name = "ListView";
            this.ListView.Size = new System.Drawing.Size(530, 404);
            this.ListView.TabIndex = 0;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "FormMain";
            this.Text = "Hector";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ActualiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImporterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExporterToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.ListView ListView;
        private ColorDialog colorDialog1;
        private TreeView ArbreArticles;
    }
}

