using Mediatek86.controleur;
using Mediatek86.metier;
using Mediatek86.vue.ControleUtilisateur;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediatek86.vue
{
    public partial class FrmCommandes : Form
    {
        /// <summary>
        /// Instance du UserControl usrcCommandeLivre
        /// </summary>
        private UsrcCommandeLivre usrcCommandeLivre;
        private Livre unLivre;
        private Dvd unDvd;
        private Revue uneRevue;
        private Object unObjet;
        private Controle controle;
        public FrmCommandes(Object unObjet, Controle controle)
        {
            InitializeComponent();
            this.unObjet = unObjet;
            this.controle = controle;

        }

        private void Commandes_Load(object sender, EventArgs e)
        {
            if (unObjet is Livre)
            {
                unLivre = (Livre)unObjet;
                ConsUsrcCommandeLivre();
            }
            else if (unObjet is Dvd)
            {

            }
            else
            {

            }
        }
        private void ConsUsrcCommandeLivre()
        {
            usrcCommandeLivre = new UsrcCommandeLivre(this, unLivre);
            usrcCommandeLivre.Location = new Point(0, 0);
            this.Controls.Add(usrcCommandeLivre);
        }
        public List<Commande> GetAllCommandes(string idLivre_Dvd)
        {
            return controle.GetAllCommandes(idLivre_Dvd);
        }
        public List<Suivi> GetAllSuivi()
        {
            return controle.GetAllSuivi();
        }
        public void ModifierAjouterCommande(Commande commande, bool ajoutEnCours)
        {
            if (!ajoutEnCours)
            {
                controle.ModifierCommandeLivreDvd(commande);
            }
            else
            {
                controle.CreerCommandeLivreDvd(commande);
                /*
                switch (nomBtn)
                {
                    case "btnValiderModificationLivre":
                        int idLivreTemp = this.lesLivres.Max((Livre l) => int.Parse(l.Id)) + 1;
                        Livre unLivre = new Livre(conversionIDBdd(idLivreTemp), txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text, txbLivresAuteur.Text, txbLivresCollection.Text,
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
                */
            }
        }
        public void fermerVueCommande()
        {
            this.Dispose();
        }
        public bool SupprCommandeLivreDvd(Commande commande)
        {
            return controle.SupprCommandeLivreDvd(commande);
        }
    }
}
