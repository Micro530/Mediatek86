using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe de DVD
    /// </summary>
    public class Dvd : LivreDvd
    {
        /// <summary>
        /// duree du dvd
        /// </summary>
        private readonly int duree;
        /// <summary>
        /// réalisateur du dvd
        /// </summary>
        private readonly string realisateur;
        /// <summary>
        /// synopsis du dvd
        /// </summary>
        private readonly string synopsis;
        /// <summary>
        /// constructeur de la classe
        /// </summary>
        /// <param name="id">id du document</param>
        /// <param name="titre">titre du document</param>
        /// <param name="image">image du document</param>
        /// <param name="duree">duree du dvd</param>
        /// <param name="realisateur">réalisateur du dvd</param>
        /// <param name="synopsis">synopsis du dvd</param>
        /// <param name="idGenre">idGenre du document</param>
        /// <param name="genre">Genre du document</param>
        /// <param name="idPublic">idpublic du document</param>
        /// <param name="lePublic">le public du document</param>
        /// <param name="idRayon">id du rayon du document</param>
        /// <param name="rayon">rayon du document</param>
        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.duree = duree;
            this.realisateur = realisateur;
            this.synopsis = synopsis;
        }
        /// <summary>
        /// durée du dvd
        /// </summary>
        public int Duree { get => duree; }
        /// <summary>
        /// réalisateur du dvd
        /// </summary>
        public string Realisateur { get => realisateur; }
        /// <summary>
        /// synopsis du dvd
        /// </summary>
        public string Synopsis { get => synopsis; }

    }
}
