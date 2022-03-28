using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe LivreDvd
    /// </summary>
    public abstract class LivreDvd : Document
    {
        /// <summary>
        /// constructeur d'un livreDvd
        /// </summary>
        /// <param name="id">id du document</param>
        /// <param name="titre">titre du document</param>
        /// <param name="image">image du document</param>
        /// <param name="idGenre">idGenre du document</param>
        /// <param name="genre">Genre du document</param>
        /// <param name="idPublic">idpublic du document</param>
        /// <param name="lePublic">le public du document</param>
        /// <param name="idRayon">id du rayon du document</param>
        /// <param name="rayon">rayon du document</param>
        protected LivreDvd(string id, string titre, string image, string idGenre, string genre, 
            string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
        }
    }
}

