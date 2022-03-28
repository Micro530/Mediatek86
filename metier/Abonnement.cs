using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe Abonnement
    /// </summary>
    public class Abonnement
    {
        /// <summary>
        /// id de l'abonnement
        /// </summary>
        string id;
        /// <summary>
        /// date de fin d'abonnement
        /// </summary>
        string dateFinAbonnement;
        /// <summary>
        /// id de la revue
        /// </summary>
        string idRevue;
        /// <summary>
        /// constructeur de la classe abonnement
        /// </summary>
        /// <param name="id">id de l'abonnement</param>
        /// <param name="dateFinAbonnement">date de fin d'abonnement</param>
        /// <param name="idRevue">id de la revue</param>
        public Abonnement(string id, string dateFinAbonnement, string idRevue)
        {
            this.Id = id;
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
        }
        /// <summary>
        /// id de l'abonnement
        /// </summary>
        public string Id { get => id; set => id = value; }
        /// <summary>
        /// date de fin d'abonnement
        /// </summary>
        public string DateFinAbonnement { get => dateFinAbonnement; set => dateFinAbonnement = value; }
        /// <summary>
        /// id de la revue
        /// </summary>
        public string IdRevue { get => idRevue; set => idRevue = value; }
    }
}
