using System;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe Exemplaire
    /// </summary>
    public class Exemplaire
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="numero">numéro d'exemplaire</param>
        /// <param name="dateAchat">date achet de l'exemplaire</param>
        /// <param name="photo">photo de l'exemplaire</param>
        /// <param name="idEtat">id de l'état de l'exemplaire</param>
        /// <param name="idDocument">id de document </param>
        public Exemplaire(int numero, DateTime dateAchat, string photo,string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.IdDocument = idDocument;
        }
        /// <summary>
        /// numéro d'exemplaire
        /// </summary>
        public int Numero { get; set; }
        /// <summary>
        /// photo de l'exemplaire
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// date achet de l'exemplaire
        /// </summary>
        public DateTime DateAchat { get; set; }
        /// <summary>
        /// id de l'état de l'exemplaire
        /// </summary>
        public string IdEtat { get; set; }
        /// <summary>
        /// id de document 
        /// </summary>
        public string IdDocument { get; set; }
    }
}
