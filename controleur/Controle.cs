using System.Collections.Generic;
using Mediatek86.modele;
using Mediatek86.metier;
using Mediatek86.vue;


namespace Mediatek86.controleur
{
    /// <summary>
    /// Classe Controle
    /// </summary>
    public class Controle
    {
        /// <summary>
        /// Instance du formulaire Authentifiation
        /// </summary>
        readonly private FrmAuthentification frmAuthentification;
        /// <summary>
        /// Ouverture de la fenêtre
        /// </summary>
        public Controle()
        {
            frmAuthentification = new FrmAuthentification(this);
            frmAuthentification.ShowDialog();
        }
        /// <summary>
        /// Permet d'ouvrir le formulaire mediatek
        /// </summary>
        /// <param name="service">le service de l'utilisateur connecté</param>
        public void OuvertureFrmMediatek(string service)
        {
            FrmMediatek frmMediatek = new FrmMediatek(this, service);
            frmAuthentification.Dispose();
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
        public List<CommandeDoc> GetAllCommandes(string idLivre_Dvd)
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
        /// <summary>
        /// creer une commande de livre dvd
        /// </summary>
        /// <param name="commande"></param>
        public void CreerCommandeLivreDvd(CommandeDoc commande)
        {
            Dao.CreerCommandeLivreDvd(commande);
        }
        /// <summary>
        /// Permet d'envoyer un commande livreDvd a modifier
        /// </summary>
        /// <param name="commande">commande a modifier</param>
        public void ModifierCommandeLivreDvd(CommandeDoc commande)
        {
            Dao.ModifierCommandeLivreDvd(commande);
        }
        /// <summary>
        /// Permet de suprimer une commande de Livre_Dvd
        /// </summary>
        /// <param name="commande">commande a supprimer</param>
        /// <returns>si la suppression à pu se faire</returns>
        public bool SupprCommandeLivreDvd(CommandeDoc commande)
        {
            return Dao.SupprCommandeLivreDvd(commande);
        }
        /// <summary>
        /// récupère est retourne une liste de commandes par rapport a une id de revue
        /// </summary>
        /// <param name="idRevue">id recherché</param>
        /// <returns>une liste de commandes</returns>
        public List<CommandeAbo> GetAllCommandesRevues(string idRevue)
        {
            return Dao.GetAllCommandesRevues(idRevue);
        }
        /// <summary>
        /// Pemret de créer un commandes de revue
        /// </summary>
        /// <param name="commandeAbo">la commande a créer</param>
        public void CreerCommandeRevue(CommandeAbo commandeAbo)
        {
            Dao.CreerCommandeRevue(commandeAbo);
        }
        /// <summary>
        /// Permet de supprimer un abonnement
        /// </summary>
        /// <param name="idCommande">abonnement à supprimer</param>
        public void SupprAbonnement(string idCommande)
        {
            Dao.SupprAbonnement(idCommande);
        }
        /// <summary>
        /// récupère et retourne les revus avec un abonnement de moins de 30 jours
        /// </summary>
        /// <returns></returns>
        public List<Revue> GetAbonnementMoinsTrenteJours()
        {
            return Dao.GetAbonnementMoinsTrenteJours();
        }
        /// <summary>
        /// Méthode d'authentifiaction
        /// </summary>
        /// <param name="identifiant">l'identifiant</param>
        /// <param name="pwd">le mot de passe</param>
        /// <returns>le nom du service</returns>
        public string Authentification(string identifiant, string pwd)
        {
            return Dao.Authetification(identifiant, pwd);
        }
    }

}

 