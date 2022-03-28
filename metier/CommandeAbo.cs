using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe abonnementAbo
    /// </summary>
    public class CommandeAbo : Abonnement
    {
        /// <summary>
        /// date de la commande
        /// </summary>
        private string dateCommande;
        /// <summary>
        /// montant de la commande
        /// </summary>
        private double montant;
        /// <summary>
        /// constructeur de la classe
        /// </summary>
        /// <param name="dateCommande">date de la commande</param>
        /// <param name="montant">montant de la commande</param>
        /// <param name="id">id de l'abonnement</param>
        /// <param name="dateFinAbonnement">date de fin d'abonnement</param>
        /// <param name="idRevue">id de la revue</param>
        public CommandeAbo(string dateCommande, double montant, string id, string dateFinAbonnement, string idRevue)
            : base(id, dateFinAbonnement, idRevue)
        {
            this.DateCommande = dateCommande;
            this.Montant = montant;
        }
        /// <summary>
        /// date de la commande
        /// </summary>
        public string DateCommande { get => dateCommande; set => dateCommande = value; }
        /// <summary>
        /// montant de la commande
        /// </summary>
        public double Montant { get => montant; set => montant = value; }
    }
}
