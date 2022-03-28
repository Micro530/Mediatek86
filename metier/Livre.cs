

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe de livreDvd
    /// </summary>
    public class Livre : LivreDvd
    {
        /// <summary>
        /// isbn du livre
        /// </summary>
        private readonly string isbn;
        /// <summary>
        /// auteur di livre
        /// </summary>
        private readonly string auteur;
        /// <summary>
        /// collection du livre
        /// </summary>
        private readonly string collection;
        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        /// <param name="id">id du document</param>
        /// <param name="titre">titre du document</param>
        /// <param name="image">image du document</param>
        /// <param name="isbn">isbn du livre</param>
        /// <param name="auteur">auteru du livre</param>
        /// <param name="collection">collection du livre</param>
        /// <param name="idGenre">idGenre du document</param>
        /// <param name="genre">Genre du document</param>
        /// <param name="idPublic">idpublic du document</param>
        /// <param name="lePublic">le public du document</param>
        /// <param name="idRayon">id du rayon du document</param>
        /// <param name="rayon">rayon du document</param>
        public Livre(string id, string titre, string image, string isbn, string auteur, string collection, 
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            :base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.isbn = isbn;
            this.auteur = auteur;
            this.collection = collection;
        }
        /// <summary>
        /// isbn du livre
        /// </summary>
        public string Isbn { get => isbn; }
        /// <summary>
        /// auteur du livre
        /// </summary>
        public string Auteur { get => auteur; }
        /// <summary>
        /// collection du livre
        /// </summary>
        public string Collection { get => collection; }

    }
}
