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
    public partial class UsrcCommande : UserControl
    {
        readonly private FrmCommandes frmCommandes;
        readonly private BindingSource bdgCommandesListe = new BindingSource();
        readonly private BindingSource bdgSuiviListe = new BindingSource();
        readonly private Livre unLivre;
        readonly private Dvd unDvd;
        readonly private Revue uneRevue;
        readonly private string idDocument;
        private List<Suivi> lesSuivis;
        private bool ajoutEnCours;
        private CommandeDoc CommandeSelectionneLivreDvd;
        private CommandeAbo CommandeSelectionneAbo;
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="frm">formaulaire commande appelant</param>
        /// <param name="objet">objet soit (Livre, DVD, Revue)</param>
        public UsrcCommande(FrmCommandes frm, object objet)
        {
            if(objet is Livre livre)
            {
                unLivre = livre;
                idDocument = unLivre.Id;
            }
            else if (objet is Dvd dvd)
            {
                unDvd = dvd;
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
                RemplirDgvListCommande(frmCommandes.GetAllCommandes(unLivre.Id));
                txtInfoObjet.Text = $"Livre : {unLivre.Titre} - {unLivre.Auteur} - {unLivre.Collection} - {unLivre.Genre}";
                PreparerComboSuivi();
            }
            else if(unDvd != null)
            {
                RemplirDgvListCommande(frmCommandes.GetAllCommandes(unDvd.Id));
                txtInfoObjet.Text = $"DVD : {unDvd.Titre} - {unDvd.Realisateur} - {unDvd.Duree} - {unDvd.Genre}";
                btnAjoutCommandeLivre.Text = "Ajouter une nouvelle commande pour ce DVD";
                PreparerComboSuivi();
            }
            else
            {
                RemplirDgvListCommandeAbo(frmCommandes.GetAllCommandesRevues(uneRevue.Id));
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
        public void PreparerComboSuivi()
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
        public void RemplirDgvListCommande(List<CommandeDoc> commandes)
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
        public void RemplirDgvListCommandeAbo(List<CommandeAbo> commandesAbo)
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
        private void DgvListCommande_SelectionChanged(object sender, EventArgs e)
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
            Suivi suiviCommande = lesSuivis.First((Suivi s) => s.Libelle == commande.Suivi);
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
        private void BtnAjoutCommandeLivre_Click(object sender, EventArgs e)
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
        private void BtnAnnulerModif_Click(object sender, EventArgs e)
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
                RemplirDgvListCommande(frmCommandes.GetAllCommandes(idDocument));
            }
            else
            {
                RemplirDgvListCommandeAbo(frmCommandes.GetAllCommandesRevues(idDocument));

            }
        }
        /// <summary>
        /// evenement lors du clique sur le btnValideModif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValiderModif_Click(object sender, EventArgs e)
        {
            if(uneRevue is null)
            {
                Suivi suivi = (Suivi)comboSuivi.SelectedItem;
                if (ajoutEnCours)
                {
                    CommandeDoc commande = new CommandeDoc(ConversionDateBdd(calendrierCommandeLivre.SelectionStart), double.Parse(txtMontant.Text),
                            "1", int.Parse(txtNbExemplaire.Text), idDocument, suivi.Id, suivi.Libelle);
                    frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                    BtnAnnulerModif_Click(null, null);
                }
                else
                {
                    if (CommandeSelectionneLivreDvd != null && CommandeSelectionneLivreDvd.IdSuivi < suivi.Id)
                    {
                        CommandeDoc commande = new CommandeDoc(ConversionDateBdd(calendrierCommandeLivre.SelectionStart), double.Parse(txtMontant.Text),
                            CommandeSelectionneLivreDvd.Id, int.Parse(txtNbExemplaire.Text), idDocument, suivi.Id, suivi.Libelle);
                        frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                        BtnAnnulerModif_Click(null, null);
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
                    BtnAnnulerModif_Click(null, null);
                }
                else
                {
                    if (CommandeSelectionneAbo != null)
                    {

                        CommandeAbo commande = new CommandeAbo(ConversionDateBdd(DateTime.Now), double.Parse(txtMontant.Text), CommandeSelectionneAbo.Id, 
                            ConversionDateBdd(calendrierCommandeLivre.SelectionStart), idDocument);
                        frmCommandes.ModifierAjouterCommande(commande, ajoutEnCours);
                        BtnAnnulerModif_Click(null, null);

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
        private void BtnModifierCommandeLivre_Click(object sender, EventArgs e)
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
        private void BtnRetourDoc_Click(object sender, EventArgs e)
        {
            frmCommandes.FermerVueCommande();
        }
        /// <summary>
        /// evenement lors du clique sur le btnSupprimerCommande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimerCommandeLivre_Click(object sender, EventArgs e)
        {
            if(dgvListCommande.CurrentCell != null)
            {
                DialogResult dialog = MessageBox.Show("Voulez vous vraiment supprimer cette commande ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialog == DialogResult.Yes)
                {
                    if(uneRevue is null)
                    {
                        frmCommandes.SupprCommandeLivreDvd(CommandeSelectionneLivreDvd);
                        RemplirDgvListCommande(frmCommandes.GetAllCommandes(idDocument));
                    }
                    else if(!VerifRevueCommande(frmCommandes.GetExemplairesRevue(CommandeSelectionneAbo.Id)))
                    {
                        frmCommandes.SupprAbonnement(CommandeSelectionneAbo.Id);
                        RemplirDgvListCommandeAbo(frmCommandes.GetAllCommandesRevues(idDocument));
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
