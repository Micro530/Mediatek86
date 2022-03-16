using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class Commande : CommandeDocument
    {
        private DateTime dateCommande;
        private double montant;

        public Commande(DateTime dateCommande, double montant, string id, int nbExemplaire, string idLivreDvd, int idSuivi, string suivi)
            : base(id, nbExemplaire, idLivreDvd, idSuivi, suivi)
        {
            this.DateCommande = dateCommande;
            this.Montant = montant;
        }

        public DateTime DateCommande { get => dateCommande; set => dateCommande = value; }
        public double Montant { get => montant; set => montant = value; }
    }
}
