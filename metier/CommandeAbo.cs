using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class CommandeAbo : Abonnement
    {
        private string dateCommande;
        private double montant;

        public CommandeAbo(string dateCommande, double montant, string id, string dateFinAbonnement, string idRevue)
            : base(id, dateFinAbonnement, idRevue)
        {
            this.DateCommande = dateCommande;
            this.Montant = montant;
        }

        public string DateCommande { get => dateCommande; set => dateCommande = value; }
        public double Montant { get => montant; set => montant = value; }
    }
}
