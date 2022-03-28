

namespace Mediatek86.metier
{
    /// <summary>
    /// classe categorie
    /// </summary>
    public abstract class Categorie
    {
        /// <summary>
        /// id de la catégorie
        /// </summary>
        private readonly string id;
        /// <summary>
        /// libelle de la catégorie
        /// </summary>
        private readonly string libelle;
        /// <summary>
        /// constructeur de la classe
        /// </summary>
        /// <param name="id">id de la catégorie</param>
        /// <param name="libelle">libelle de la catégorie</param>
        protected Categorie(string id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }
        /// <summary>
        /// id de la catégorie
        /// </summary>
        public string Id { get => id; }
        /// <summary>
        /// libelle de la catégorie
        /// </summary>
        public string Libelle { get => libelle; }

        /// <summary>
        /// Récupération du libellé pour l'affichage dans les combos
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.libelle;
        }

    }
}
