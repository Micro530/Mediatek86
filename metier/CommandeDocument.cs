using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe commandeDocument
    /// </summary>
    public class CommandeDocument
    {
        /// <summary>
        /// id de la commande
        /// </summary>
        private string id;
        /// <summary>
        /// nombre d'exemplaire commandé
        /// </summary>
        private int nbExemplaire;
        /// <summary>
        /// id du document
        /// </summary>
        private string idLivreDvd;
        /// <summary>
        /// id du suivi 
        /// </summary>
        private int idSuivi;
        /// <summary>
        /// libellé du suivi
        /// </summary>
        private string suivi;
        /// <summary>
        /// constructeur de la classe
        /// </summary>
        /// <param name="id">id de la commande</param>
        /// <param name="nbExemplaire">nombre d'exemplaire commandé</param>
        /// <param name="idLivreDvd">id du document</param>
        /// <param name="idSuivi">id du suivi </param>
        /// <param name="suivi">libellé du suivi</param>
        public CommandeDocument(string id, int nbExemplaire, string idLivreDvd, int idSuivi, string suivi)
        {
            this.Id = id;
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.IdSuivi = idSuivi;
            this.Suivi = suivi;
        }
        /// <summary>
        /// id du document
        /// </summary>
        public string Id { get => id; set => id = value; }
        /// <summary>
        /// nombre d'exemplaire dans la commande
        /// </summary>
        public int NbExemplaire { get => nbExemplaire; set => nbExemplaire = value; }
        /// <summary>
        /// id du document
        /// </summary>
        public string IdLivreDvd { get => idLivreDvd; set => idLivreDvd = value; }
        /// <summary>
        /// id du suivi 
        /// </summary>
        public int IdSuivi { get => idSuivi; set => idSuivi = value; }
        /// <summary>
        /// libellé du suivi
        /// </summary>
        public string Suivi { get => suivi; set => suivi = value; }
    }
}
