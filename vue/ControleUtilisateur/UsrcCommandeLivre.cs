using Mediatek86.metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediatek86.vue.ControleUtilisateur
{
    public partial class UsrcCommandeLivre : UserControl
    {
        private FrmCommandes frmCommandes;
        private BindingSource bdgCommandesListe = new BindingSource();
        private BindingSource bdgSuiviListe = new BindingSource();
        private Livre unLivre;
        private List<Suivi> lesSuivis;
        private bool ajoutEnCours;
        private Commande CommandeSelectionne;
        public UsrcCommandeLivre(FrmCommandes frm, Livre livre)
        {
            InitializeComponent();
            frmCommandes = frm;
            unLivre = livre;
            Init();
        }
        public void Init()
        {
            remplirDgvListCommande(frmCommandes.GetAllCommandes(unLivre.Id));
            txtInfoObjet.Text = $"{unLivre.Titre}  {unLivre.Auteur}  {unLivre.Collection}  {unLivre.Genre}";
            lesSuivis = frmCommandes.GetAllSuivi();
            bdgSuiviListe.DataSource = lesSuivis;
            comboSuivi.DataSource = bdgSuiviListe;
            comboSuivi.SelectedIndex = -1;
        }
        public void remplirDgvListCommande(List<Commande> commandes)
        {
            bdgCommandesListe.DataSource = commandes;
            dgvListCommande.DataSource = bdgCommandesListe;
            dgvListCommande.Columns["id"].Visible = false;
            dgvListCommande.Columns["idLivreDvd"].Visible = false;
            dgvListCommande.Columns["idSuivi"].Visible = false;
            dgvListCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvListCommande.Columns["id"].DisplayIndex = 0;
        }

        private void dgvListCommande_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListCommande.CurrentCell != null)
            {
                try
                {
                    CommandeSelectionne = (Commande)bdgCommandesListe.List[bdgCommandesListe.Position];
                    AfficheCommandeInfos(CommandeSelectionne);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                VideCommandeInfos();
            }
        }
        public void AfficheCommandeInfos(Commande commande)
        {
            Suivi suiviCommande = lesSuivis.Cast<Suivi>().Where((Suivi s) => s.Libelle == commande.Suivi).First();
            comboSuivi.SelectedItem = suiviCommande;
            CalendrierCommandeLivre.SetDate(DateTime.Parse(commande.DateCommande));
            txtMontant.Text = commande.Montant.ToString();
            txtNbExemplaire.Text = commande.NbExemplaire.ToString();
        }
        public void VideCommandeInfos()
        {
            comboSuivi.SelectedIndex = -1;
            CalendrierCommandeLivre.SelectionStart = DateTime.Now;
            txtMontant.Text = "";
            txtNbExemplaire.Text = "";
            comboSuivi.Enabled = true;
        }

        private void btnAjoutCommandeLivre_Click(object sender, EventArgs e)
        {
            grbListCommande.Enabled = false;
            grbInfoCommande.Enabled = true;
            ajoutEnCours = true;
            VideCommandeInfos();
            comboSuivi.SelectedIndex = 0;
            comboSuivi.Enabled = false;
        }

        private void btnAnnulerModif_Click(object sender, EventArgs e)
        {
            grbListCommande.Enabled = true;
            grbInfoCommande.Enabled = false;
            ajoutEnCours = false;
            txtMontant.Enabled = true;
            txtNbExemplaire.Enabled = true;
            CalendrierCommandeLivre.Enabled = true;
            VideCommandeInfos();
            remplirDgvListCommande(frmCommandes.GetAllCommandes(unLivre.Id));
        }

        private void btnValiderModif_Click(object sender, EventArgs e)
        {
            Suivi suivi = (Suivi)comboSuivi.SelectedItem;
            if(CommandeSelectionne.IdSuivi <= suivi.Id)
            {
                Commande commande = new Commande(ConversionDateBdd(CalendrierCommandeLivre.SelectionStart), double.Parse(txtMontant.Text), 
                    CommandeSelectionne.Id, int.Parse(txtNbExemplaire.Text), CommandeSelectionne.IdLivreDvd, suivi.Id, suivi.Libelle);
                frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                btnAnnulerModif_Click(null, null);
            }
            else
            {
                MessageBox.Show("Changement du statut de la commande incohérent, vérifiez les modifications", "Statut incohérent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        /// <summary>
        /// Permet de convertir la date en version française en version anglaise avec la syntaxe exacte de la base de données
        /// </summary>
        /// <param name="datePrestation"></param>
        /// <returns></returns>
        private string ConversionDateBdd(DateTime datePrestation)
        {
            return datePrestation.Year + "-" + datePrestation.Month + "-" + datePrestation.Day; //+ " " + datePrestation.Hour + ":" + datePrestation.Minute
                //+ ":" + datePrestation.Second;
        }

        private void btnModifierCommandeLivre_Click(object sender, EventArgs e)
        {
            grbListCommande.Enabled = false;
            grbInfoCommande.Enabled = true;
            ajoutEnCours = false;
            txtMontant.Enabled = false;
            txtNbExemplaire.Enabled = false;
            CalendrierCommandeLivre.Enabled = false;
        }

        private void btnRetourDoc_Click(object sender, EventArgs e)
        {
            frmCommandes.fermerVueCommande();
        }

        private void btnSupprimerCommandeLivre_Click(object sender, EventArgs e)
        {
            if(dgvListCommande.CurrentCell != null)
            {
                DialogResult dialog = MessageBox.Show("Voulez vous vraiment supprimer cette commande ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialog == DialogResult.Yes)
                {
                    frmCommandes.SupprCommandeLivreDvd(CommandeSelectionne);
                    remplirDgvListCommande(frmCommandes.GetAllCommandes(unLivre.Id));
                }
            }
        }
    }
}
