using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;
using Mediatek86.controleur;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Mediatek86.vue
{
    /// <summary>
    /// Formulaire FrmMediatek
    /// </summary>
    public partial class FrmMediatek : Form
    {

        #region Variables globales
        /// <summary>
        /// Instance du controle
        /// </summary>
        private readonly Controle controle;
        /// <summary>
        /// Code etat neuf
        /// </summary>
        const string ETATNEUF = "00001";
        /// <summary>
        /// Pathde la racine des photos
        /// </summary>
        private const string pathC = "c:\\";
        /// <summary>
        /// BindingSource de la liste des livres
        /// </summary>
        private readonly BindingSource bdgLivresListe = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des livreDvd
        /// </summary>
        private readonly BindingSource bdgDvdListe = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des Genre
        /// </summary>
        private readonly BindingSource bdgGenres = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des GenreChoix
        /// </summary>
        private readonly BindingSource bdgGenresChoix = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des publics
        /// </summary>
        private readonly BindingSource bdgPublics = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des public choix
        /// </summary>
        private readonly BindingSource bdgPublicsChoix = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des rayons
        /// </summary>
        private readonly BindingSource bdgRayons = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des rayons choix
        /// </summary>
        private readonly BindingSource bdgRayonsChoix = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des revues
        /// </summary>
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        /// <summary>
        /// BindingSource de la liste des exemplaires
        /// </summary>
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        /// <summary>
        /// liste des livres
        /// </summary>
        private List<Livre> lesLivres = new List<Livre>();
        /// <summary>
        /// liste des dvd
        /// </summary>
        private List<Dvd> lesDvd = new List<Dvd>();
        /// <summary>
        /// liste des revues
        /// </summary>
        private List<Revue> lesRevues = new List<Revue>();
        /// <summary>
        /// listes des exemplaires
        /// </summary>
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        /// <summary>
        /// liste des genres
        /// </summary>
        readonly private List<Categorie> lesGenres;
        /// <summary>
        /// liste des publics
        /// </summary>
        readonly private List<Categorie> lesPublics;
        /// <summary>
        /// liste des rayons
        /// </summary>
        readonly private List<Categorie> lesRayons;
        /// <summary>
        /// permet de connaitre si une modification est en cours
        /// </summary>
        private bool ajoutEnCours;
        /// <summary>
        /// le nom du service
        /// </summary>
        readonly private string service;

        #endregion
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="controle">le controle appelant</param>
        /// <param name="service">le nom du service</param>
        internal FrmMediatek(Controle controle, string service)
        {
            InitializeComponent();
            this.controle = controle;
            this.service = service;
            lesGenres = controle.GetAllGenres();
            lesPublics = controle.GetAllPublics();
            lesRayons = controle.GetAllRayons();
            if (!service.Equals("prêt"))
            {
                MessageRevuesMoinsTrenteJours(controle.GetAbonnementMoinsTrenteJours());
            }
        }


        #region modules communs
        /// <summary>
        /// Permet de créer le message si des abonnement sont à moins de 30 jours lors de l'ouverture de l'application
        /// </summary>
        /// <param name="lesRevues">la liste des revue qui ont moins de 30 jours</param>
        private void MessageRevuesMoinsTrenteJours(List<Revue> lesRevues)
        {
            if(lesRevues.Any())
            {
                string message = "Les abonnements  de ces revues de terminent dans moins de 30 jours :\n";
                foreach (Revue revue in lesRevues)
                {
                    message += $"- Revue {revue.Id} - {revue.Titre}\n";
                }
                MessageBox.Show(message, "Abonnements presque terminés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories"></param>
        /// <param name="bdg"></param>
        /// <param name="cbx"></param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Méthode permettant de gérer la demande de confirmation de suppression
        /// </summary>
        /// <param name="message">element du message a rajouter pou personnaliser</param>
        /// <param name="titre">titre de l'element</param>
        /// <param name="id">id de l'element</param>
        /// <returns></returns>
        private bool ClickBtnSupprConfirmation(string titre, string id, string message = "cette element")
        {
            DialogResult dialogResult = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer {message} nommé : '{titre}'\n\nVoulez-vous continuer ?",
                "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult.Equals(DialogResult.Yes))
            {
                return controle.SupprDocument(id);

            }
            return false;

        }
        /// <summary>
        /// Méthode permettant de moddifier un document
        /// </summary>
        private void ModificationAjoutDocument(Object sender)
        {
            string nomBtn = ((Button)sender).Name;
            if (!ajoutEnCours)
            {
                switch (nomBtn)
                {
                    case "btnValiderModificationLivre":
                        Livre unLivre = new Livre(txbLivresNumero.Text, txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text, txbLivresAuteur.Text, txbLivresCollection.Text,
                            ((Genre)comboLivreGenre.SelectedValue).Id, ((Genre)comboLivreGenre.SelectedValue).Libelle, ((Public)comboLivrePublic.SelectedValue).Id,
                            ((Public)comboLivrePublic.SelectedValue).Libelle, ((Rayon)comboLivreRayon.SelectedValue).Id, ((Rayon)comboLivreRayon.SelectedValue).Libelle);
                        controle.ModifierLivre(unLivre);
                        RemplirLivresListeComplete(controle.GetAllLivres());
                        grpLivresInfos.Enabled = false;
                        grpLivresRecherche.Enabled = true;
                        break;
                    case "btnValiderModifDvd":
                        Dvd unDvd = new Dvd(txbDvdNumero.Text, txbDvdTitre.Text, txbDvdImage.Text, int.Parse(txbDvdDuree.Text), txbDvdRealisateur.Text,
                            txbDvdSynopsis.Text, ((Genre)comboDvdGenre.SelectedValue).Id, ((Genre)comboDvdGenre.SelectedValue).Libelle,
                            ((Public)comboDvdPublic.SelectedValue).Id, ((Public)comboDvdPublic.SelectedValue).Libelle, ((Rayon)comboDvdRayon.SelectedValue).Id,
                            ((Rayon)comboDvdRayon.SelectedValue).Libelle);
                        controle.ModifierDvd(unDvd);
                        RemplirDvdListeComplete(controle.GetAllDvd());
                        grpDvdInfos.Enabled = false;
                        grpDvdRecherche.Enabled = true;
                        break;
                    case "btnValideModifRevue":
                        Revue revue = new Revue(txbRevuesNumero.Text, txbRevuesTitre.Text, txbRevuesImage.Text, ((Genre)comboRevueGenre.SelectedValue).Id, ((Genre)comboRevueGenre.SelectedValue).Libelle,
                            ((Public)comboRevuePublic.SelectedValue).Id, ((Public)comboRevuePublic.SelectedValue).Libelle, ((Rayon)comboRevueRayon.SelectedValue).Id,
                            ((Rayon)comboRevueRayon.SelectedValue).Libelle, chkRevuesEmpruntable.Checked, txbRevuesPeriodicite.Text, int.Parse(txbRevuesDateMiseADispo.Text));
                        controle.ModifierRevue(revue);
                        RemplirRevuesListeComplete(controle.GetAllRevues());
                        grpRevuesInfos.Enabled = false;
                        grpRevuesRecherche.Enabled = true;
                        break;
                }
            }
            else
            {
                switch (nomBtn)
                {
                    case "btnValiderModificationLivre":
                        int idLivreTemp = this.lesLivres.Max((Livre l) => int.Parse(l.Id)) + 1;
                        Livre unLivre = new Livre(ConversionIDBdd(idLivreTemp), txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text, txbLivresAuteur.Text, txbLivresCollection.Text,
                            ((Genre)comboLivreGenre.SelectedValue).Id, ((Genre)comboLivreGenre.SelectedValue).Libelle, ((Public)comboLivrePublic.SelectedValue).Id,
                            ((Public)comboLivrePublic.SelectedValue).Libelle, ((Rayon)comboLivreRayon.SelectedValue).Id, ((Rayon)comboLivreRayon.SelectedValue).Libelle);
                        controle.AjouterLivre(unLivre);
                        RemplirLivresListeComplete(controle.GetAllLivres());
                        grpLivresInfos.Enabled = false;
                        grpLivresRecherche.Enabled = true;
                        break;
                    case "btnValiderModifDvd":
                        int idDvdTemp = this.lesDvd.Max((Dvd d) => int.Parse(d.Id)) + 1;
                        Dvd unDvd = new Dvd(idDvdTemp.ToString(), txbDvdTitre.Text, txbDvdImage.Text, int.Parse(txbDvdDuree.Text), txbDvdRealisateur.Text,
                            txbDvdSynopsis.Text, ((Genre)comboDvdGenre.SelectedValue).Id, ((Genre)comboDvdGenre.SelectedValue).Libelle,
                            ((Public)comboDvdPublic.SelectedValue).Id, ((Public)comboDvdPublic.SelectedValue).Libelle, ((Rayon)comboDvdRayon.SelectedValue).Id,
                            ((Rayon)comboDvdRayon.SelectedValue).Libelle);
                        controle.AjouterDvd(unDvd);
                        RemplirDvdListeComplete(controle.GetAllDvd());
                        grpDvdInfos.Enabled = false;
                        grpDvdRecherche.Enabled = true;
                        break;
                    case "btnValideModifRevue":
                        int idRevuesTemp = this.lesRevues.Max((Revue r) => int.Parse(r.Id)) + 1;
                        Revue revue = new Revue(idRevuesTemp.ToString(), txbRevuesTitre.Text, txbRevuesImage.Text, ((Genre)comboRevueGenre.SelectedValue).Id, ((Genre)comboRevueGenre.SelectedValue).Libelle,
                            ((Public)comboRevuePublic.SelectedValue).Id, ((Public)comboRevuePublic.SelectedValue).Libelle, ((Rayon)comboRevueRayon.SelectedValue).Id,
                            ((Rayon)comboRevueRayon.SelectedValue).Libelle, chkRevuesEmpruntable.Checked, txbRevuesPeriodicite.Text, int.Parse(txbRevuesDateMiseADispo.Text));
                        controle.AjouterRevue(revue);
                        RemplirRevuesListeComplete(controle.GetAllRevues());
                        grpRevuesInfos.Enabled = false;
                        grpRevuesRecherche.Enabled = true;
                        break;
                }
            }
            ajoutEnCours = false;
        }
        /// <summary>
        /// Permet de convertire le format de l'id en un format compatible avec ceux utilisé dans la BDD 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string ConversionIDBdd(int id)
        {
            if (id <= 9)
            {
                return "0000" + id;
            }else if(id <= 99)
            {
                return "000" + id;
            }else if(id <= 999)
            {
                return "00" + id;
            }else if(id <= 9999)
            {
                return "0" + id;
            }
            else
            {
                return id.ToString();
            }
        }
        #endregion


        #region Revues
        //-----------------------------------------------------------
        // ONGLET "Revues"
        //------------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controle.GetAllRevues();
            RemplirComboCategorie(lesGenres, bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(lesGenres, bdgGenresChoix, comboRevueGenre);
            RemplirComboCategorie(lesPublics, bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(lesPublics, bdgPublicsChoix, comboRevuePublic);
            RemplirComboCategorie(lesRayons, bdgRayons, cbxRevuesRayons);
            RemplirComboCategorie(lesRayons, bdgRayonsChoix, comboRevueRayon);
            RemplirRevuesListeComplete();
            if (service.Equals("prêt"))
            {
                btnAccederCommandeRevue.Enabled = false;
                btnModifierRevue.Enabled = false;
                btnSupprRevues.Enabled = false;
            }
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["empruntable"].Visible = false;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>
                    {
                        revue
                    };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            chkRevuesEmpruntable.Checked = revue.Empruntable;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            //récupère les genre, les publics et lesrayons du livre et l'affiche dans le combobox
            Genre genreLivre = lesGenres.Cast<Genre>().First(g => g.Libelle == revue.Genre);
            comboRevueGenre.SelectedItem = genreLivre;
            
            Public publicLivre = lesPublics.Cast<Public>().First((Public p) => p.Libelle == revue.Public);
            comboRevuePublic.SelectedItem = publicLivre;

            Rayon rayonLivre = lesRayons.Cast<Rayon>().First((Rayon r) => r.Libelle == revue.Rayon);
            comboRevueRayon.SelectedItem = rayonLivre;

            txbRevuesTitre.Text = revue.Titre;     
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch 
            { 
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            chkRevuesEmpruntable.Checked = false;
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            comboRevueRayon.SelectedIndex = -1;
            comboRevuePublic.SelectedIndex = -1;
            comboRevueGenre.SelectedIndex = -1;
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete(List<Revue> lesRevues = null)
        {
            if(lesRevues != null)
            {
                this.lesRevues = lesRevues;
            }
            RemplirRevuesListe(this.lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        /// <summary>
        /// Evenement lors du clique sur le btnSupprRevues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprRevues_Click(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell.RowIndex != -1)
            {
                Revue revue = (Revue)dgvRevuesListe.CurrentRow.DataBoundItem;
                if (ClickBtnSupprConfirmation(revue.Titre, revue.Id, "cette revue"))
                {
                    lesRevues = controle.GetAllRevues();
                    RemplirRevuesListeComplete();
                }
            }
        }
        /// <summary>
        /// Evenement lors du clique sur le btnModifierRevue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifierRevue_Click(object sender, EventArgs e)
        {
            grpRevuesInfos.Enabled = true;
            grpRevuesRecherche.Enabled = false;
        }
        /// <summary>
        /// evenement lors du clique sur le btnValideModifRevue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValideModifRevue_Click(object sender, EventArgs e)
        {
            ModificationAjoutDocument(sender);
        }
        /// <summary>
        /// evenement lors du clique sur le btnAnnulerRevue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnulerRevue_Click(object sender, EventArgs e)
        {
            grpRevuesInfos.Enabled = false;
            grpRevuesRecherche.Enabled = true;
        }
        /// <summary>
        /// evenement lors de l'ajout d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouterRevue_Click(object sender, EventArgs e)
        {
            grpRevuesInfos.Enabled = true;
            grpRevuesRecherche.Enabled = false;
            ajoutEnCours = true;
            VideRevuesInfos();
        }
        /// <summary>
        /// Evenement lors du clique sur le btnAccederCommandeRevue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAccederCommandeRevue_Click(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell.RowIndex != -1)
            {
                controle.OuvrirCommande((Revue)dgvRevuesListe.CurrentRow.DataBoundItem);
            }
            else
            {
                MessageBox.Show("Vous devez selectionner un DVD avant de pourvoir accèder aux commandes", "Erreur de selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion


        #region Livres

        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controle.GetAllLivres();
            RemplirComboCategorie(lesGenres, bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(lesGenres, bdgGenresChoix, comboLivreGenre);
            RemplirComboCategorie(lesPublics, bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(lesPublics, bdgPublicsChoix, comboLivrePublic);
            RemplirComboCategorie(lesRayons, bdgRayons, cbxLivresRayons);
            RemplirComboCategorie(lesRayons, bdgRayonsChoix, comboLivreRayon);
            RemplirLivresListeComplete();
            if (service.Equals("prêt"))
            {
                btnCommandeLivre.Enabled = false;
                btnModifierLivre.Enabled = false;
                btnSupprLivre.Enabled = false;
            }
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>
                    {
                        livre
                    };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0 
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre"></param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            
            //récupère les genre, les publics et lesrayons du livre et l'affiche dans le combobox
            Genre genreLivre = lesGenres.Cast<Genre>().First((Genre g) => g.Libelle == livre.Genre);
            comboLivreGenre.SelectedItem = genreLivre;

            Public publicLivre = lesPublics.Cast<Public>().First((Public p) => p.Libelle == livre.Public);
            comboLivrePublic.SelectedItem = publicLivre;

            Rayon rayonLivre = lesRayons.Cast<Rayon>().First((Rayon r) => r.Libelle == livre.Rayon);
            comboLivreRayon.SelectedItem = rayonLivre;
            
            txbLivresTitre.Text = livre.Titre;      
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch 
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            comboLivreGenre.SelectedIndex = -1;
            comboLivrePublic.SelectedIndex = -1;
            comboLivreRayon.SelectedIndex = -1;
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete(List<Livre> lesLivres = null)
        {
            if(lesLivres != null)
            {
                this.lesLivres = lesLivres;
            }
            RemplirLivresListe(this.lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }
        /// <summary>
        /// Evenement lors du clique sur le btnSupprLivre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprLivre_Click(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell.RowIndex != -1)
            {
                Livre livre = (Livre)dgvLivresListe.CurrentRow.DataBoundItem;
                if (ClickBtnSupprConfirmation(livre.Titre, livre.Id, "ce livre"))
                {
                    lesLivres = controle.GetAllLivres();
                    RemplirLivresListeComplete();
                }
            }
        }
        /// <summary>
        /// Evenement lors du clique sur le btnValiderLivre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValiderLivre_Click(object sender, EventArgs e)
        {
            ModificationAjoutDocument(sender);
        }
        /// <summary>
        /// evenement lors de l'annulation des modifications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnulerLivre_Click(object sender, EventArgs e)
        {
            grpLivresInfos.Enabled = false;
            grpLivresRecherche.Enabled = true;
            DgvDvdListe_SelectionChanged(null, null);
            ajoutEnCours = false;
        }
        /// <summary>
        /// Evenement lors du clique sur le btnModifierLivre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifierLivre_Click(object sender, EventArgs e)
        {
            grpLivresInfos.Enabled = true;
            grpLivresRecherche.Enabled = false;
        }
        /// <summary>
        /// evenement lors de l'ajout d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouterLivre_Click(object sender, EventArgs e)
        {
            grpLivresInfos.Enabled = true;
            grpLivresRecherche.Enabled = false;
            ajoutEnCours = true;
            VideLivresInfos();
        }
        /// <summary>
        /// Evenement du clique sur le btnCommandeLivre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCommandeLivre_Click(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell.RowIndex != -1)
            {
                controle.OuvrirCommande((Livre)dgvLivresListe.CurrentRow.DataBoundItem);
            }
            else
            {
                MessageBox.Show("Vous devez selectionner un livre avant de pourvoir accèder aux commandes", "Erreur de selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion


        #region Dvd
        //-----------------------------------------------------------
        // ONGLET "DVD"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controle.GetAllDvd();
            RemplirComboCategorie(lesGenres, bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(lesGenres, bdgGenresChoix, comboDvdGenre);
            RemplirComboCategorie(lesPublics, bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(lesPublics, bdgPublicsChoix, comboDvdPublic);
            RemplirComboCategorie(lesRayons, bdgRayons, cbxDvdRayons);
            RemplirComboCategorie(lesRayons, bdgRayonsChoix, comboDvdRayon);
            RemplirDvdListeComplete();
            if (service.Equals("prêt"))
            {
                btnAccederCommandeDvd.Enabled = false;
                btnModifierDvd.Enabled = false;
                btnSupprDvd.Enabled = false;
            }
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>
                    {
                        dvd
                    };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd"></param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdNumero.Text = dvd.Id;
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString() ;
            //récupère les genre, les publics et lesrayons du livre et l'affiche dans le combobox
            Genre genreLivre = lesGenres.Cast<Genre>().First((Genre g) => g.Libelle == dvd.Genre);
            comboDvdGenre.SelectedItem = genreLivre;

            Public publicLivre = lesPublics.Cast<Public>().First((Public p) => p.Libelle == dvd.Public);
            comboDvdPublic.SelectedItem = publicLivre;

            Rayon rayonLivre = lesRayons.Cast<Rayon>().First((Rayon r) => r.Libelle == dvd.Rayon);
            comboDvdRayon.SelectedItem = rayonLivre;
            
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch 
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            comboDvdGenre.SelectedIndex = -1;
            comboDvdPublic.SelectedIndex = -1;
            comboDvdRayon.SelectedIndex = -1;
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete(List<Dvd> lesDvd = null)
        {
            if(lesDvd != null)
            {
                this.lesDvd = lesDvd;
            }
            RemplirDvdListe(this.lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        /// <summary>
        /// Evenement lors du clique sur le btnSupprDvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprDvd_Click(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell.RowIndex != -1)
            {
                Dvd dvd = (Dvd)dgvDvdListe.CurrentRow.DataBoundItem;
                if (ClickBtnSupprConfirmation(dvd.Titre, dvd.Id, "ce dvd"))
                {
                    lesDvd = controle.GetAllDvd();
                    RemplirDvdListeComplete();
                }
            }
        }
        /// <summary>
        /// evenement clique sur le btnModifierDvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifierDvd_Click(object sender, EventArgs e)
        {
            grpDvdInfos.Enabled = true;
            grpDvdRecherche.Enabled = false;
        }
        /// <summary>
        /// evenement clique sur le btnValideModifierDvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValiderModifDvd_Click(object sender, EventArgs e)
        {
            ModificationAjoutDocument(sender);
        }
        /// <summary>
        /// evenement clique btnAnnuler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnulerDvd_Click(object sender, EventArgs e)
        {
            grpDvdInfos.Enabled = false;
            grpDvdRecherche.Enabled = true;
            DgvDvdListe_SelectionChanged(null, null);
        }
        /// <summary>
        /// evenement lors de l'ajout d'un dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouterDvd_Click(object sender, EventArgs e)
        {
            grpDvdInfos.Enabled = true;
            grpDvdRecherche.Enabled = false;
            ajoutEnCours = true;
            VideDvdInfos();
        }

        /// <summary>
        /// Evement lors du clique sur le btnAcederCommandeDVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnActiverCommandeDvd_click(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell.RowIndex != -1)
            {
                controle.OuvrirCommande((Dvd)dgvDvdListe.CurrentRow.DataBoundItem);
            }
            else
            {
                MessageBox.Show("Vous devez selectionner un DVD avant de pourvoir accèder aux commandes", "Erreur de selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion


        #region Réception Exemplaire de presse
        //-----------------------------------------------------------
        // ONGLET "RECEPTION DE REVUES"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet : blocage en saisie des champs de saisie des infos de l'exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controle.GetAllRevues();
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            bdgExemplairesListe.DataSource = exemplaires;
            dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
            dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
            dgvReceptionExemplairesListe.Columns["idDocument"].Visible = false;
            dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
            dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    VideReceptionRevueInfos();
                }
            }
            else
            {
                VideReceptionRevueInfos();
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            AccesReceptionExemplaireGroupBox(false);
            VideReceptionRevueInfos();
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            chkReceptionRevueEmpruntable.Checked = revue.Empruntable;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;         
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch 
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
            // accès à la zone d'ajout d'un exemplaire
            AccesReceptionExemplaireGroupBox(true);
        }

        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controle.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
        }

        /// <summary>
        /// Vide les zones d'affchage des informations de la revue
        /// </summary>
        private void VideReceptionRevueInfos()
        {
            txbReceptionRevuePeriodicite.Text = "";
            chkReceptionRevueEmpruntable.Checked = false;
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            lesExemplaires = new List<Exemplaire>();
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de l'exemplaire
        /// </summary>
        private void VideReceptionExemplaireInfos()
        {
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces"></param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            VideReceptionExemplaireInfos();
            grpReceptionExemplaire.Enabled = acces;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = pathC,
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;         
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch 
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controle.CreerExemplaire(exemplaire))
                    {
                        VideReceptionExemplaireInfos();
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// Sélection d'une ligne complète et affichage de l'image sz l'exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }





        #endregion
        
        
    }
}
