using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe Public
    /// </summary>
    public class Public : Categorie
    {
        /// <summary>
        /// Constructeur de la classe Genre
        /// </summary>
        /// <param name="id">id de la catégorie</param>
        /// <param name="libelle">libellé de la catégorie</param>
        public Public(string id, string libelle):base(id, libelle)
        {
        }

    }
}
