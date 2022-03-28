using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe commandeDoc
    /// </summary>
    public class CommandeDoc : CommandeDocument
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
        /// <param name="nbExemplaire">nombre d'exemplaire dans la commande</param>
        /// <param name="idLivreDvd">id du document</param>
        /// <param name="idSuivi">id du suivi</param>
        /// <param name="suivi">nom du suivi</param>
        public CommandeDoc(string dateCommande, double montant, string id, int nbExemplaire, string idLivreDvd, int idSuivi, string suivi)
            : base(id, nbExemplaire, idLivreDvd, idSuivi, suivi)
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
