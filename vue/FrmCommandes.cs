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
    }
}
