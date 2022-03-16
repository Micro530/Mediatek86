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
                    Commande commande = (Commande)bdgCommandesListe.List[bdgCommandesListe.Position];
                    AfficheCommandeInfos(commande);
                }
                catch
                {
                    //VideCommandeZones();
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
            txtDateCommande.Text = commande.DateCommande.ToString();
            txtMontant.Text = commande.Montant.ToString();
            txtNbExemplaire.Text = commande.NbExemplaire.ToString();
        }
        public void VideCommandeInfos()
        {
            comboSuivi.SelectedItem = -1;
            txtDateCommande.Text = "";
            txtMontant.Text = "";
            txtNbExemplaire.Text = "";
        }

    }
}
