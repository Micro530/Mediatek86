

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe Genre
    /// </summary>
    public class Genre : Categorie
    {
        /// <summary>
        /// Constructeur de la classe Genre
        /// </summary>
        /// <param name="id">id de la catégorie</param>
        /// <param name="libelle">libellé de la catégorie</param>
        public Genre(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
