using Mediatek86.controleur;
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
    public partial class FrmAuthentification : Form
    {
        /// <summary>
        /// Instance du controle
        /// </summary>
        readonly private Controle controle;
        /// <summary>
        /// Nom du service
        /// </summary>
        string nomService;
        /// <summary>
        /// Constructeur du formulaire
        /// </summary>
        /// <param name="controle"></param>
        public FrmAuthentification(Controle controle)
        {
            this.controle = controle;
            InitializeComponent();
        }
        /// <summary>
        /// Évenement du clique sur le BtnConnexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            nomService = controle.Authentification(txtIdentifiant.Text, txtPwd.Text);
            if(nomService is null)
            {
                MessageBox.Show("Identifiant ou mot de passe érroné", "Échec de l'authentification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(nomService.Equals("culture"))
            {
                MessageBox.Show("Vos privilèges ne vous permettent pas d'accéder à cette application", "Échec de l'authentification", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                controle.OuvertureFrmMediatek(nomService);
            }
        }
    }
}
