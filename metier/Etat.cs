

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe métier Etat
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="id">id de l'état</param>
        /// <param name="libelle">nom de l'état</param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }
        /// <summary>
        /// id de l'état
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// nom de l'état
        /// </summary>
        public string Libelle { get; set; }
    }
}
