using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class CommandeDocument
    {
        private string id;
        private int nbExemplaire;
        private string idLivreDvd;
        private int idSuivi;
        private string suivi;

        public CommandeDocument(string id, int nbExemplaire, string idLivreDvd, int idSuivi, string suivi)
        {
            this.Id = id;
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.IdSuivi = idSuivi;
            this.Suivi = suivi;
        }

        public string Id { get => id; set => id = value; }
        public int NbExemplaire { get => nbExemplaire; set => nbExemplaire = value; }
        public string IdLivreDvd { get => idLivreDvd; set => idLivreDvd = value; }
        public int IdSuivi { get => idSuivi; set => idSuivi = value; }
        public string Suivi { get => suivi; set => suivi = value; }
    }
}
