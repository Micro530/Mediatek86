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
        /// <summary>
        /// L'eventuel livre récupérer
        /// </summary>
        private Livre unLivre;
        /// <summary>
        /// l'eventurel dvd récupéré
        /// </summary>
        private Dvd unDvd;
        /// <summary>
        /// L'éventuel Revue récupéré
        /// </summary>
        private Revue uneRevue;
        /// <summary>
        /// L'objet récupéré
        /// </summary>
        private Object unObjet;
        /// <summary>
        /// L'instance du controleur
        /// </summary>
        private Controle controle;
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="unObjet">Objet de type (Livre, DVD ou Revue)</param>
        /// <param name="controle">le controleur appelant</param>
        public FrmCommandes(Object unObjet, Controle controle)
        {
            InitializeComponent();
            this.unObjet = unObjet;
            this.controle = controle;

        }
        /// <summary>
        /// Lors du chargement du formaulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commandes_Load(object sender, EventArgs e)
        {
            ConsUsrcCommandeLivre();
        }
        /// <summary>
        /// Permet de construire et d'afficher le UserControle UsrcCommande
        /// </summary>
        private void ConsUsrcCommandeLivre()
        {
            usrcCommandeLivre = new UsrcCommandeLivre(this, unObjet);
            usrcCommandeLivre.Location = new Point(0, 0);
            this.Controls.Add(usrcCommandeLivre);
        }
        /// <summary>
        /// Permet de récupérer et d'envoyer le liste de commande par id de livre_dvd
        /// </summary>
        /// <param name="idLivre_Dvd">id recherché dans les commandes</param>
        /// <returns>liste des commandes</returns>
        public List<CommandeDoc> GetAllCommandes(string idLivre_Dvd)
        {
            return controle.GetAllCommandes(idLivre_Dvd);
        }
        /// <summary>
        /// Permet de récupérer et denvoyer la liste des tyoe de suivi
        /// </summary>
        /// <returns>liste des suivis</returns>
        public List<Suivi> GetAllSuivi()
        {
            return controle.GetAllSuivi();
        }
        /// <summary>
        /// Méthode de modification ou d'ajout du commande
        /// </summary>
        /// <param name="commande">la commande soit type Abo soit Doc</param>
        /// <param name="ajoutEnCours">Si le boulean ajout est à true</param>
        public void ModifierAjouterCommande(Object commande, bool ajoutEnCours)
        {
            if (!ajoutEnCours)
            {
                if(commande is CommandeDoc)
                {
                    controle.ModifierCommandeLivreDvd((CommandeDoc)commande);
                }
            }
            else
            {
                if (commande is CommandeDoc)
                {
                    controle.CreerCommandeLivreDvd((CommandeDoc)commande);
                }
                else
                {
                    controle.CreerCommandeRevue((CommandeAbo)commande);
                }
            }
        }
        /// <summary>
        /// Permet de fermé cette fenetre
        /// </summary>
        public void fermerVueCommande()
        {
            this.Dispose();
        }
        /// <summary>
        /// Méthode de suppression du commande de livre_dvd
        /// </summary>
        /// <param name="commande">commande à supprimer</param>
        /// <returns>si la commande a bien été supprimé</returns>
        public bool SupprCommandeLivreDvd(CommandeDoc commande)
        {
            return controle.SupprCommandeLivreDvd(commande);
        }
        /// <summary>
        /// Permet de récupérer et d'envoyer toutes les commandes de revus depuis un id
        /// </summary>
        /// <param name="idRevue">id de la revue</param>
        /// <returns>une liste de commandes</returns>
        public List<CommandeAbo> GetAllCommandesRevues(string idRevue)
        {
            return controle.GetAllCommandesRevues(idRevue);
        }
        /// <summary>
        /// Permet de récupérer et d'envoyer tous les exemplaires ayant cette id
        /// </summary>
        /// <param name="idDocument">id du cocument recherché</param>
        /// <returns>une liste de d'exmplaires</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            return controle.GetExemplairesRevue(idDocument);
        }
        /// <summary>
        /// Permet de supprimer un abonnement
        /// </summary>
        /// <param name="idCommande">abonnement a supprimer</param>
        public void SupprAbonnement(string idCommande)
        {
            controle.SupprAbonnement(idCommande);
        }
    }
}
