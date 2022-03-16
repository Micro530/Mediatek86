using System.Collections.Generic;
using Mediatek86.modele;
using Mediatek86.metier;
using Mediatek86.vue;


namespace Mediatek86.controleur
{
    public class Controle
    {
        private readonly List<Livre> lesLivres;
        private readonly List<Dvd> lesDvd;
        private readonly List<Revue> lesRevues;
        private readonly List<Categorie> lesRayons;
        private readonly List<Categorie> lesPublics;
        private readonly List<Categorie> lesGenres;

        /// <summary>
        /// Ouverture de la fenêtre
        /// </summary>
        public Controle()
        {
            FrmMediatek frmMediatek = new FrmMediatek(this);
            frmMediatek.ShowDialog();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Collection d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return Dao.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Collection d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return Dao.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Collection d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return Dao.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Collection d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return Dao.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Collection d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return Dao.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Collection d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return Dao.GetAllPublics();
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <returns>Collection d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return Dao.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return Dao.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Permet de supprimer le document envoyé
        /// </summary>
        /// <param name="id">id du document à supprimer</param>
        public bool SupprDocument(string id)
        {
            return Dao.SupprDocument(id);
        }
        /// <summary>
        /// Pemret de modifier un livre
        /// </summary>
        /// <param name="livre"></param>
        public void ModifierLivre(Livre livre)
        {
            Dao.ModifierLivre(livre);
        }
        /// <summary>
        /// Pemret de modifier un dvd
        /// </summary>
        /// <param name="dvd"></param>
        public void ModifierDvd(Dvd dvd)
        {
            Dao.ModifierDvd(dvd);
        }
        /// <summary>
        /// Pemret de modifier une Revue
        /// </summary>
        /// <param name="revue"></param>
        public void ModifierRevue(Revue revue)
        {
            Dao.ModifierRevue(revue);
        }
        /// <summary>
        /// Pemret d'ajouter un livre
        /// </summary>
        /// <param name="livre"></param>
        public void AjouterLivre(Livre livre)
        {
            Dao.AjouterLivre(livre);
        }
        /// <summary>
        /// Pemret d'ajouter un dvd
        /// </summary>
        /// <param name="dvd"></param>
        public void AjouterDvd(Dvd dvd)
        {
            Dao.AjouterDvd(dvd);
        }
        /// <summary>
        /// Pemret d'ajouter une Revue
        /// </summary>
        /// <param name="revue"></param>
        public void AjouterRevue(Revue revue)
        {
            Dao.AjouterRevue(revue);
        }
        /// <summary>
        /// Permet de récuparer toutes les commandes d'un Livre_Dvd
        /// </summary>
        /// <param name="idLivre_Dvd"></param>
        /// <returns></returns>
        public List<Commande> GetAllCommandes(string idLivre_Dvd)
        {
            return Dao.GetAllCommandes(idLivre_Dvd);
        }
        /// <summary>
        /// Ouvrir la nouvelle feunetre des commandes
        /// </summary>
        /// <param name="objet"></param>
        public void OuvrirCommande(object objet)
        {
            FrmCommandes frmCommande = new FrmCommandes(objet, this);
            frmCommande.ShowDialog();
        }
        /// <summary>
        /// Retourne tous les suivis possibles
        /// </summary>
        /// <returns></returns>
        public List<Suivi> GetAllSuivi()
        {
            return Dao.GetAllSuivi();
        }
    }

}

