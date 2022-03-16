
namespace Mediatek86.vue.ControleUtilisateur
{
    partial class UsrcCommandeLivre
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvListCommande = new System.Windows.Forms.DataGridView();
            this.txtInfoObjet = new System.Windows.Forms.TextBox();
            this.grbListCommande = new System.Windows.Forms.GroupBox();
            this.btnRetourDoc = new System.Windows.Forms.Button();
            this.btnSupprimerCommandeLivre = new System.Windows.Forms.Button();
            this.btnModifierCommandeLivre = new System.Windows.Forms.Button();
            this.btnAjoutCommandeLivre = new System.Windows.Forms.Button();
            this.grbInfoCommande = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateCommande = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNbExemplaire = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboSuivi = new System.Windows.Forms.ComboBox();
            this.btnValiderModif = new System.Windows.Forms.Button();
            this.btnAnnulerModif = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListCommande)).BeginInit();
            this.grbListCommande.SuspendLayout();
            this.grbInfoCommande.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvListCommande
            // 
            this.dgvListCommande.AllowUserToAddRows = false;
            this.dgvListCommande.AllowUserToDeleteRows = false;
            this.dgvListCommande.AllowUserToResizeColumns = false;
            this.dgvListCommande.AllowUserToResizeRows = false;
            this.dgvListCommande.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListCommande.Location = new System.Drawing.Point(6, 121);
            this.dgvListCommande.Name = "dgvListCommande";
            this.dgvListCommande.ReadOnly = true;
            this.dgvListCommande.RowHeadersVisible = false;
            this.dgvListCommande.Size = new System.Drawing.Size(847, 197);
            this.dgvListCommande.TabIndex = 0;
            this.dgvListCommande.SelectionChanged += new System.EventHandler(this.dgvListCommande_SelectionChanged);
            // 
            // txtInfoObjet
            // 
            this.txtInfoObjet.Location = new System.Drawing.Point(17, 85);
            this.txtInfoObjet.Name = "txtInfoObjet";
            this.txtInfoObjet.ReadOnly = true;
            this.txtInfoObjet.Size = new System.Drawing.Size(394, 20);
            this.txtInfoObjet.TabIndex = 1;
            // 
            // grbListCommande
            // 
            this.grbListCommande.Controls.Add(this.btnRetourDoc);
            this.grbListCommande.Controls.Add(this.btnSupprimerCommandeLivre);
            this.grbListCommande.Controls.Add(this.btnModifierCommandeLivre);
            this.grbListCommande.Controls.Add(this.btnAjoutCommandeLivre);
            this.grbListCommande.Controls.Add(this.dgvListCommande);
            this.grbListCommande.Controls.Add(this.txtInfoObjet);
            this.grbListCommande.Location = new System.Drawing.Point(10, 34);
            this.grbListCommande.Name = "grbListCommande";
            this.grbListCommande.Size = new System.Drawing.Size(859, 324);
            this.grbListCommande.TabIndex = 2;
            this.grbListCommande.TabStop = false;
            // 
            // btnRetourDoc
            // 
            this.btnRetourDoc.Location = new System.Drawing.Point(17, 19);
            this.btnRetourDoc.Name = "btnRetourDoc";
            this.btnRetourDoc.Size = new System.Drawing.Size(136, 57);
            this.btnRetourDoc.TabIndex = 5;
            this.btnRetourDoc.Text = "Retour aux Documents";
            this.btnRetourDoc.UseVisualStyleBackColor = true;
            // 
            // btnSupprimerCommandeLivre
            // 
            this.btnSupprimerCommandeLivre.ForeColor = System.Drawing.Color.Red;
            this.btnSupprimerCommandeLivre.Location = new System.Drawing.Point(702, 19);
            this.btnSupprimerCommandeLivre.Name = "btnSupprimerCommandeLivre";
            this.btnSupprimerCommandeLivre.Size = new System.Drawing.Size(136, 57);
            this.btnSupprimerCommandeLivre.TabIndex = 4;
            this.btnSupprimerCommandeLivre.Text = "Supprimer la commande selectionnée";
            this.btnSupprimerCommandeLivre.UseVisualStyleBackColor = true;
            // 
            // btnModifierCommandeLivre
            // 
            this.btnModifierCommandeLivre.Location = new System.Drawing.Point(555, 19);
            this.btnModifierCommandeLivre.Name = "btnModifierCommandeLivre";
            this.btnModifierCommandeLivre.Size = new System.Drawing.Size(136, 57);
            this.btnModifierCommandeLivre.TabIndex = 3;
            this.btnModifierCommandeLivre.Text = "Modifier la commande selectionnée";
            this.btnModifierCommandeLivre.UseVisualStyleBackColor = true;
            // 
            // btnAjoutCommandeLivre
            // 
            this.btnAjoutCommandeLivre.Location = new System.Drawing.Point(409, 19);
            this.btnAjoutCommandeLivre.Name = "btnAjoutCommandeLivre";
            this.btnAjoutCommandeLivre.Size = new System.Drawing.Size(136, 57);
            this.btnAjoutCommandeLivre.TabIndex = 2;
            this.btnAjoutCommandeLivre.Text = "Ajouter une commande pour ce livre";
            this.btnAjoutCommandeLivre.UseVisualStyleBackColor = true;
            // 
            // grbInfoCommande
            // 
            this.grbInfoCommande.Controls.Add(this.btnAnnulerModif);
            this.grbInfoCommande.Controls.Add(this.btnValiderModif);
            this.grbInfoCommande.Controls.Add(this.comboSuivi);
            this.grbInfoCommande.Controls.Add(this.label4);
            this.grbInfoCommande.Controls.Add(this.txtNbExemplaire);
            this.grbInfoCommande.Controls.Add(this.label3);
            this.grbInfoCommande.Controls.Add(this.txtMontant);
            this.grbInfoCommande.Controls.Add(this.label2);
            this.grbInfoCommande.Controls.Add(this.label1);
            this.grbInfoCommande.Controls.Add(this.txtDateCommande);
            this.grbInfoCommande.Enabled = false;
            this.grbInfoCommande.Location = new System.Drawing.Point(10, 364);
            this.grbInfoCommande.Name = "grbInfoCommande";
            this.grbInfoCommande.Size = new System.Drawing.Size(859, 258);
            this.grbInfoCommande.TabIndex = 3;
            this.grbInfoCommande.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date de la commande";
            // 
            // txtDateCommande
            // 
            this.txtDateCommande.Location = new System.Drawing.Point(131, 34);
            this.txtDateCommande.Name = "txtDateCommande";
            this.txtDateCommande.Size = new System.Drawing.Size(273, 20);
            this.txtDateCommande.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Montant";
            // 
            // txtMontant
            // 
            this.txtMontant.Location = new System.Drawing.Point(512, 34);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(179, 20);
            this.txtMontant.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nombre d\'exemplaires";
            // 
            // txtNbExemplaire
            // 
            this.txtNbExemplaire.Location = new System.Drawing.Point(130, 87);
            this.txtNbExemplaire.Name = "txtNbExemplaire";
            this.txtNbExemplaire.Size = new System.Drawing.Size(274, 20);
            this.txtNbExemplaire.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(441, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "État de suivi";
            // 
            // comboSuivi
            // 
            this.comboSuivi.FormattingEnabled = true;
            this.comboSuivi.Location = new System.Drawing.Point(512, 86);
            this.comboSuivi.Name = "comboSuivi";
            this.comboSuivi.Size = new System.Drawing.Size(179, 21);
            this.comboSuivi.TabIndex = 7;
            // 
            // btnValiderModif
            // 
            this.btnValiderModif.Location = new System.Drawing.Point(17, 171);
            this.btnValiderModif.Name = "btnValiderModif";
            this.btnValiderModif.Size = new System.Drawing.Size(136, 57);
            this.btnValiderModif.TabIndex = 8;
            this.btnValiderModif.Text = "Valider les modifications";
            this.btnValiderModif.UseVisualStyleBackColor = true;
            // 
            // btnAnnulerModif
            // 
            this.btnAnnulerModif.Location = new System.Drawing.Point(191, 171);
            this.btnAnnulerModif.Name = "btnAnnulerModif";
            this.btnAnnulerModif.Size = new System.Drawing.Size(136, 57);
            this.btnAnnulerModif.TabIndex = 9;
            this.btnAnnulerModif.Text = "Annuler";
            this.btnAnnulerModif.UseVisualStyleBackColor = true;
            // 
            // UsrcCommandeLivre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbInfoCommande);
            this.Controls.Add(this.grbListCommande);
            this.Name = "UsrcCommandeLivre";
            this.Size = new System.Drawing.Size(883, 659);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListCommande)).EndInit();
            this.grbListCommande.ResumeLayout(false);
            this.grbListCommande.PerformLayout();
            this.grbInfoCommande.ResumeLayout(false);
            this.grbInfoCommande.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListCommande;
        private System.Windows.Forms.TextBox txtInfoObjet;
        private System.Windows.Forms.GroupBox grbListCommande;
        private System.Windows.Forms.Button btnRetourDoc;
        private System.Windows.Forms.Button btnSupprimerCommandeLivre;
        private System.Windows.Forms.Button btnModifierCommandeLivre;
        private System.Windows.Forms.Button btnAjoutCommandeLivre;
        private System.Windows.Forms.GroupBox grbInfoCommande;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDateCommande;
        private System.Windows.Forms.ComboBox comboSuivi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNbExemplaire;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMontant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAnnulerModif;
        private System.Windows.Forms.Button btnValiderModif;
    }
}
