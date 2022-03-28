using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe suivi
    /// </summary>
    public class Suivi
    {
        /// <summary>
        /// id du suivi
        /// </summary>
        private int id;
        /// <summary>
        /// libelle du suivi
        /// </summary>
        private string libelle;
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Suivi(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }
        /// <summary>
        /// id du suivi
        /// </summary>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Libelle du suivi
        /// </summary>
        public string Libelle { get => libelle; set => libelle = value; }
        /// <summary>
        /// Pour l'affichage dans les combo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Libelle;
        }
    }
}
