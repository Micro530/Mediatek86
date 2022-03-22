using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class Abonnement
    {
        string id;
        string dateFinAbonnement;
        string idRevue;

        public Abonnement(string id, string dateFinAbonnement, string idRevue)
        {
            this.Id = id;
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
        }

        public string Id { get => id; set => id = value; }
        public string DateFinAbonnement { get => dateFinAbonnement; set => dateFinAbonnement = value; }
        public string IdRevue { get => idRevue; set => idRevue = value; }
    }
}
