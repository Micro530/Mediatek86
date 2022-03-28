
namespace Mediatek86.metier
{
    /// <summary>
    /// Classe Document
    /// </summary>
    public class Document
    {
        /// <summary>
        /// id du document
        /// </summary>
        private readonly string id;
        /// <summary>
        /// titre du document
        /// </summary>
        private readonly string titre;
        /// <summary>
        /// image du document
        /// </summary>
        private readonly string image;
        /// <summary>
        /// idGenre du document
        /// </summary>
        private readonly string idGenre;
        /// <summary>
        /// Genre du document
        /// </summary>
        private readonly string genre;
        /// <summary>
        /// idpublic du document
        /// </summary>
        private readonly string idPublic;
        /// <summary>
        /// le public du document
        /// </summary>
        private readonly string lePublic;
        /// <summary>
        /// id du rayon du document
        /// </summary>
        private readonly string idRayon;
        /// <summary>
        /// rayon du document
        /// </summary>
        private readonly string rayon;
        /// <summary>
        /// constructeur du document
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
        public Document(string id, string titre, string image, string idGenre, string genre, 
            string idPublic, string lePublic, string idRayon, string rayon)
        {
            this.id = id;
            this.titre = titre;
            this.image = image;
            this.idGenre = idGenre;
            this.genre = genre;
            this.idPublic = idPublic;
            this.lePublic = lePublic;
            this.idRayon = idRayon;
            this.rayon = rayon;
        }
        /// <summary>
        /// id du document
        /// </summary>
        public string Id { get => id; }
        /// <summary>
        /// titre du document
        /// </summary>
        public string Titre { get => titre; }
        /// <summary>
        /// image du document
        /// </summary>
        public string Image { get => image; }
        /// <summary>
        /// idGenre du document
        /// </summary>
        public string IdGenre { get => idGenre; }
        /// <summary>
        /// Genre du document
        /// </summary>
        public string Genre { get => genre; }
        /// <summary>
        /// idpublic du document
        /// </summary>
        public string IdPublic { get => idPublic; }
        /// <summary>
        /// le public du document
        /// </summary>
        public string Public { get => lePublic; }
        /// <summary>
        /// id du rayon du document
        /// </summary>
        public string IdRayon { get => idRayon; }
        /// <summary>
        /// rayon du document
        /// </summary>
        public string Rayon { get => rayon; }

    }


}
