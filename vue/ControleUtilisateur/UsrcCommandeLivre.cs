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
        private Dvd unDvd;
        private Revue uneRevue;
        private string idDocument;
        private List<Suivi> lesSuivis;
        private bool ajoutEnCours;
        private CommandeDoc CommandeSelectionneLivreDvd;
        private CommandeAbo CommandeSelectionneAbo;
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="frm">formaulaire commande appelant</param>
        /// <param name="objet">objet soit (Livre, DVD, Revue)</param>
        public UsrcCommandeLivre(FrmCommandes frm, object objet)
        {
            if(objet is Livre)
            {
                unLivre = (Livre)objet;
                idDocument = unLivre.Id;
            }
            else if (objet is Dvd)
            {
                unDvd = (Dvd)objet;
                idDocument = unDvd.Id;
            }
            else
            {
                uneRevue = (Revue)objet;
                idDocument = uneRevue.Id;
            }
            InitializeComponent();
            frmCommandes = frm;
            Init();
        }
        /// <summary>
        /// Méthode initialisant le traitement pour gérer les pages
        /// </summary>
        public void Init()
        {
            if(unLivre != null)
            {
                remplirDgvListCommande(frmCommandes.GetAllCommandes(unLivre.Id));
                txtInfoObjet.Text = $"Livre : {unLivre.Titre} - {unLivre.Auteur} - {unLivre.Collection} - {unLivre.Genre}";
                preparerComboSuivi();
            }
            else if(unDvd != null)
            {
                remplirDgvListCommande(frmCommandes.GetAllCommandes(unDvd.Id));
                txtInfoObjet.Text = $"DVD : {unDvd.Titre} - {unDvd.Realisateur} - {unDvd.Duree} - {unDvd.Genre}";
                btnAjoutCommandeLivre.Text = "Ajouter une nouvelle commande pour ce DVD";
                preparerComboSuivi();
            }
            else
            {
                remplirDgvListCommandeAbo(frmCommandes.GetAllCommandesRevues(uneRevue.Id));
                txtInfoObjet.Text = $"Revue : {uneRevue.Titre} - {uneRevue.Empruntable} - {uneRevue.Periodicite} - {uneRevue.DelaiMiseADispo}";
                comboSuivi.Visible = false;
                lblCombo.Visible = false;
                lblDate.Text = "Date Fin d'Abonnement";
                lblNb.Visible = false;
                txtNbExemplaire.Visible = false;
                btnModifierCommandeLivre.Visible = false;
                btnAjoutCommandeLivre.Text = "Ajouter un nouvel abonnement pour cette revue";

            }

        }
        /// <summary>
        /// Permet de remplir le combo suivi
        /// </summary>
        public void preparerComboSuivi()
        {
            lesSuivis = frmCommandes.GetAllSuivi();
            bdgSuiviListe.DataSource = lesSuivis;
            comboSuivi.DataSource = bdgSuiviListe;
            comboSuivi.SelectedIndex = -1;
        }
        /// <summary>
        /// Remplir le dataGridView des commandes
        /// </summary>
        /// <param name="commandes"> les commandes a insérer</param>
        public void remplirDgvListCommande(List<CommandeDoc> commandes)
        {
            bdgCommandesListe.DataSource = commandes;
            dgvListCommande.DataSource = bdgCommandesListe;
            dgvListCommande.Columns["id"].Visible = false;
            dgvListCommande.Columns["idLivreDvd"].Visible = false;
            dgvListCommande.Columns["idSuivi"].Visible = false;
            dgvListCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvListCommande.Columns["id"].DisplayIndex = 0;
        }
        /// <summary>
        /// Remplir le dataGridView des commandes d'abonnement
        /// </summary>
        /// <param name="commandesAbo"> les commandes a insérer</param>
        public void remplirDgvListCommandeAbo(List<CommandeAbo> commandesAbo)
        {
            bdgCommandesListe.DataSource = commandesAbo;
            dgvListCommande.DataSource = bdgCommandesListe;
            dgvListCommande.Columns["id"].Visible = false;
            dgvListCommande.Columns["idRevue"].Visible = false;
            dgvListCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvListCommande.Columns["id"].DisplayIndex = 0;
        }
        /// <summary>
        /// Evenement suis  a une changement de selection dans le datagridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvListCommande_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListCommande.CurrentCell != null)
            {
                if(uneRevue is null)
                {
                    try
                    {
                        CommandeSelectionneLivreDvd = (CommandeDoc)bdgCommandesListe.List[bdgCommandesListe.Position];
                        AfficheCommandeInfos(CommandeSelectionneLivreDvd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        CommandeSelectionneAbo = (CommandeAbo)bdgCommandesListe.List[bdgCommandesListe.Position];
                        AfficheCommandeInfos(CommandeSelectionneAbo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                
            }
            else
            {
                VideCommandeInfos();
            }
        }
        /// <summary>
        /// Permet de remplir les champs conportant les informations de la commandes selectionné pouyr une livre/dvd
        /// </summary>
        /// <param name="commande">la commande selectionné</param>
        public void AfficheCommandeInfos(CommandeDoc commande)
        {
            Suivi suiviCommande = lesSuivis.Cast<Suivi>().Where((Suivi s) => s.Libelle == commande.Suivi).First();
            comboSuivi.SelectedItem = suiviCommande;
            calendrierCommandeLivre.SetDate(DateTime.Parse(commande.DateCommande));
            txtMontant.Text = commande.Montant.ToString();
            txtNbExemplaire.Text = commande.NbExemplaire.ToString();
        }
        /// <summary>
        /// Permet de remplir les champs de la commande slectionné pour una abonement
        /// </summary>
        /// <param name="commande">la commande selectionné</param>
        public void AfficheCommandeInfos(CommandeAbo commande)
        {
            calendrierCommandeLivre.SetDate(DateTime.Parse(commande.DateFinAbonnement));
            txtMontant.Text = commande.Montant.ToString();
        }
        /// <summary>
        /// Permet de vider les chmaps des informations de la commande selectionné
        /// </summary>
        public void VideCommandeInfos()
        {
            comboSuivi.SelectedIndex = -1;
            txtMontant.Text = "";
            txtNbExemplaire.Text = "";
            comboSuivi.Enabled = true;
            calendrierCommandeLivre.SelectionStart = DateTime.Now;
        }
        /// <summary>
        /// Evenement lors du clique sur le btnAjoutCommande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutCommandeLivre_Click(object sender, EventArgs e)
        {
            grbListCommande.Enabled = false;
            grbInfoCommande.Enabled = true;
            ajoutEnCours = true;
            VideCommandeInfos();
            if(uneRevue is null)
            {
                comboSuivi.SelectedIndex = 0;
            }
            comboSuivi.Enabled = false;
        }
        /// <summary>
        /// evenement lors du clique sur le btnAnuulerModif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnulerModif_Click(object sender, EventArgs e)
        {
            grbListCommande.Enabled = true;
            grbInfoCommande.Enabled = false;
            ajoutEnCours = false;
            txtMontant.Enabled = true;
            txtNbExemplaire.Enabled = true;
            calendrierCommandeLivre.Enabled = true;
            VideCommandeInfos();
            if (uneRevue is null)
            {
                remplirDgvListCommande(frmCommandes.GetAllCommandes(idDocument));
            }
            else
            {
                remplirDgvListCommandeAbo(frmCommandes.GetAllCommandesRevues(idDocument));

            }
        }
        /// <summary>
        /// evenement lors du clique sur le btnValideModif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValiderModif_Click(object sender, EventArgs e)
        {
            if(uneRevue is null)
            {
                Suivi suivi = (Suivi)comboSuivi.SelectedItem;
                if (ajoutEnCours)
                {
                    CommandeDoc commande = new CommandeDoc(ConversionDateBdd(calendrierCommandeLivre.SelectionStart), double.Parse(txtMontant.Text),
                            "1", int.Parse(txtNbExemplaire.Text), idDocument, suivi.Id, suivi.Libelle);
                    frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                    btnAnnulerModif_Click(null, null);
                }
                else
                {
                    if (CommandeSelectionneLivreDvd != null && CommandeSelectionneLivreDvd.IdSuivi < suivi.Id)
                    {
                        CommandeDoc commande = new CommandeDoc(ConversionDateBdd(calendrierCommandeLivre.SelectionStart), double.Parse(txtMontant.Text),
                            CommandeSelectionneLivreDvd.Id, int.Parse(txtNbExemplaire.Text), idDocument, suivi.Id, suivi.Libelle);
                        frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                        btnAnnulerModif_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Changement du statut de la commande incohérent, vérifiez les modifications", "Statut incohérent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (ajoutEnCours)
                {
                    CommandeAbo commande = new CommandeAbo(ConversionDateBdd(DateTime.Now), double.Parse(txtMontant.Text), "0", ConversionDateBdd(calendrierCommandeLivre.SelectionStart), idDocument);
                    frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                    btnAnnulerModif_Click(null, null);
                }
                else
                {
                    if (CommandeSelectionneAbo != null)
                    {

                        CommandeAbo commande = new CommandeAbo(ConversionDateBdd(DateTime.Now), double.Parse(txtMontant.Text), CommandeSelectionneAbo.Id, 
                            ConversionDateBdd(calendrierCommandeLivre.SelectionStart), idDocument);
                        frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                        btnAnnulerModif_Click(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Changement du statut de la commande incohérent, vérifiez les modifications", "Statut incohérent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }                 
            
        }
        /// <summary>
        /// Permet de convertir la date en version française en version anglaise avec la syntaxe exacte de la base de données
        /// </summary>
        /// <param name="datePrestation">Date a convertir</param>
        /// <returns></returns>
        private string ConversionDateBdd(DateTime datePrestation)
        {
            return datePrestation.Year + "-" + datePrestation.Month + "-" + datePrestation.Day; 
        }
        /// <summary>
        /// evenement lors du clique su le btnModiferCommande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifierCommandeLivre_Click(object sender, EventArgs e)
        {
            grbListCommande.Enabled = false;
            grbInfoCommande.Enabled = true;
            ajoutEnCours = false;
            txtMontant.Enabled = false;
            txtNbExemplaire.Enabled = false;
            calendrierCommandeLivre.Enabled = false;
        }
        /// <summary>
        /// evenement lors du cilque su le btnRetourDoc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetourDoc_Click(object sender, EventArgs e)
        {
            frmCommandes.fermerVueCommande();
        }
        /// <summary>
        /// evenement lors du clique sur le btnSupprimerCommande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimerCommandeLivre_Click(object sender, EventArgs e)
        {
            if(dgvListCommande.CurrentCell != null)
            {
                DialogResult dialog = MessageBox.Show("Voulez vous vraiment supprimer cette commande ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialog == DialogResult.Yes)
                {
                    if(uneRevue is null)
                    {
                        frmCommandes.SupprCommandeLivreDvd(CommandeSelectionneLivreDvd);
                        remplirDgvListCommande(frmCommandes.GetAllCommandes(idDocument));
                    }
                    else if(!VerifRevueCommande(frmCommandes.GetExemplairesRevue(CommandeSelectionneAbo.Id)))
                    {
                        frmCommandes.SupprAbonnement(CommandeSelectionneAbo.Id);
                        remplirDgvListCommandeAbo(frmCommandes.GetAllCommandesRevues(idDocument));
                    }
                }
            }
        }
        /// <summary>
        /// evenement lors du clique sur le btnVerifRevueCommande
        /// </summary>
        /// <param name="lesExemplaires">les exemplaire de la revus selectionné</param>
        /// <returns></returns>
        public bool VerifRevueCommande(List<Exemplaire> lesExemplaires)
        {
            foreach(Exemplaire exemp in lesExemplaires)
            {
                if(ParutionDansAbonnement(DateTime.Parse(CommandeSelectionneAbo.DateCommande), DateTime.Parse(CommandeSelectionneAbo.DateFinAbonnement), exemp.DateAchat))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Méthode permettant de retrouner vrai sur la date de parution est entre les deux autres dates
        /// </summary>
        /// <param name="dateCommande">date de la commande</param>
        /// <param name="dateFinAbonnement">date de la fin d'abonnement</param>
        /// <param name="dateParution">date d'achat</param>
        /// <returns></returns>
        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            if(dateParution >= dateCommande)
            {
                if(dateParution <= dateFinAbonnement)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
