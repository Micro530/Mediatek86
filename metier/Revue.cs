
namespace Mediatek86.metier
{
    /// <summary>
    /// Classe Renue
    /// </summary>
    public class Revue : Document
    {
        /// <summary>
        /// Constructeur d'une revue
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
        /// <param name="empruntable">si la revue est empruntable</param>
        /// <param name="periodicite">périodicité</param>
        /// <param name="delaiMiseADispo">délai de mise à disposition</param>
        public Revue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon, 
            bool empruntable, string periodicite, int delaiMiseADispo)
             : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            Periodicite = periodicite;
            Empruntable = empruntable;
            DelaiMiseADispo = delaiMiseADispo;
        }

        /// <summary>
        /// Périodicité
        /// </summary>
        public string Periodicite { get; set; }
        /// <summary>
        /// si la revue est empruntable
        /// </summary>
        public bool Empruntable { get; set; }
        /// <summary>
        /// délai de mise à disposition
        /// </summary>
        public int DelaiMiseADispo { get; set; }
    }
}
