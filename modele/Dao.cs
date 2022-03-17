using Mediatek86.metier;
using System.Collections.Generic;
using Mediatek86.bdd;
using System;
using System.Windows.Forms;
using System.Linq;

namespace Mediatek86.modele
{
    public static class Dao
    {
        #region variables
        private static readonly string server = "localhost";
        private static readonly string userid = "root";
        private static readonly string password = "W4F0I9Us0DJy";
        private static readonly string database = "mediatek86";
        private static readonly string connectionString = "server="+server+";user id="+userid+";password="+password+";database="+database+";SslMode=none";
        #endregion

        #region les GetAll
        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public static List<Categorie> GetAllGenres()
        {
            List<Categorie> lesGenres = new List<Categorie>();
            string req = "Select * from genre order by libelle";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                Genre genre = new Genre((string)curs.Field("id"), (string)curs.Field("libelle"));
                lesGenres.Add(genre);
            }
            curs.Close();
            return lesGenres;
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Collection d'objets Rayon</returns>
        public static List<Categorie> GetAllRayons()
        {
            List<Categorie> lesRayons = new List<Categorie>();
            string req = "Select * from rayon order by libelle";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                Rayon rayon = new Rayon((string)curs.Field("id"), (string)curs.Field("libelle"));
                lesRayons.Add(rayon);
            }
            curs.Close();
            return lesRayons;
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Collection d'objets Public</returns>
        public static List<Categorie> GetAllPublics()
        {
            List<Categorie> lesPublics = new List<Categorie>();
            string req = "Select * from public order by libelle";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                Public lePublic = new Public((string)curs.Field("id"), (string)curs.Field("libelle"));
                lesPublics.Add(lePublic);
            }
            curs.Close();
            return lesPublics;
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public static List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = new List<Livre>();
            string req = "Select l.id, l.ISBN, l.auteur, d.titre, d.image, l.collection, ";
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon ";
            req += "from livre l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
            req += "order by titre ";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                string isbn = (string)curs.Field("ISBN");
                string auteur = (string)curs.Field("auteur");
                string titre = (string)curs.Field("titre");
                string image = (string)curs.Field("image");
                string collection = (string)curs.Field("collection");
                string idgenre = (string)curs.Field("idgenre");
                string idrayon = (string)curs.Field("idrayon");
                string idpublic = (string)curs.Field("idpublic");
                string genre = (string)curs.Field("genre");
                string lepublic = (string)curs.Field("public");
                string rayon = (string)curs.Field("rayon");
                Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idgenre, genre, 
                    idpublic, lepublic, idrayon, rayon);
                lesLivres.Add(livre);
            }
            curs.Close();

            return lesLivres;
        }
        /// <summary>
        /// Retourne toutes les etats de suivis
        /// </summary>
        /// <returns>Liste d'objets Suivi</returns>
        public static List<Suivi> GetAllSuivi()
        {
            List<Suivi> lesSuivis = new List<Suivi>();
            string req = "select * from suivi";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                int id = (int)curs.Field("id");
                string libelle = (string)curs.Field("libelle");
                Suivi suivi = new Suivi(id, libelle);
                lesSuivis.Add(suivi);
            }
            curs.Close();

            return lesSuivis;
        }
        /// <summary>
        /// Retourne toutes les commande à partir de l'id d'un livre_dvd
        /// </summary>
        /// <returns>Liste d'objets Commande</returns>
        public static List<Commande> GetAllCommandes(string idLivre_Dvd)
        {
            List<Commande> lesCommandes = new List<Commande>();
            string req = "Select d.id, d.nbExemplaire, d.idLivreDvd, d.idSuivi, s.libelle as suivi, c.dateCommande, c.montant ";
            req += "from commandedocument d join commande c on d.id=c.id ";
            req += "join suivi s on s.id=d.idSuivi ";
            req += "where d.idLivreDvd = @idLivreDvd";
            //req += "order by dateCommande DESC";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idLivreDvd", idLivre_Dvd}
                };
            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                int nbExemplaire = (int)curs.Field("nbExemplaire");
                string idLivreDvd = (string)curs.Field("idLivreDvd");
                int idSuivi = (int)curs.Field("idSuivi");
                string suivi = (string)curs.Field("suivi");
                string dateCommande = curs.Field("dateCommande").ToString();
                double montant = (double)curs.Field("montant");
                Commande commande = new Commande(dateCommande,montant,id,nbExemplaire,idLivreDvd,idSuivi, suivi);
                lesCommandes.Add(commande);
            }
            curs.Close();

            return lesCommandes;
        }

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public static List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = new List<Dvd>();
            string req = "Select l.id, l.duree, l.realisateur, d.titre, d.image, l.synopsis, ";
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon ";
            req += "from dvd l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
            req += "order by titre ";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                int duree = (int)curs.Field("duree");
                string realisateur = (string)curs.Field("realisateur");
                string titre = (string)curs.Field("titre");
                string image = (string)curs.Field("image");
                string synopsis = (string)curs.Field("synopsis");
                string idgenre = (string)curs.Field("idgenre");
                string idrayon = (string)curs.Field("idrayon");
                string idpublic = (string)curs.Field("idpublic");
                string genre = (string)curs.Field("genre");
                string lepublic = (string)curs.Field("public");
                string rayon = (string)curs.Field("rayon");
                Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon);
                lesDvd.Add(dvd);
            }
            curs.Close();

            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public static List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = new List<Revue>();
            string req = "Select l.id, l.empruntable, l.periodicite, d.titre, d.image, l.delaiMiseADispo, ";
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon ";
            req += "from revue l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
            req += "order by titre ";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                bool empruntable = (bool)curs.Field("empruntable");
                string periodicite = (string)curs.Field("periodicite");
                string titre = (string)curs.Field("titre");
                string image = (string)curs.Field("image");
                int delaiMiseADispo = (int)curs.Field("delaimiseadispo");
                string idgenre = (string)curs.Field("idgenre");
                string idrayon = (string)curs.Field("idrayon");
                string idpublic = (string)curs.Field("idpublic");
                string genre = (string)curs.Field("genre");
                string lepublic = (string)curs.Field("public");
                string rayon = (string)curs.Field("rayon");
                Revue revue = new Revue(id, titre, image, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon, empruntable, periodicite, delaiMiseADispo);
                lesRevues.Add(revue);
            }
            curs.Close();

            return lesRevues;
        }
        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <returns>Liste d'objets Exemplaire</returns>
        public static List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            List<Exemplaire> lesExemplaires = new List<Exemplaire>();
            string req = "Select e.id, e.numero, e.dateAchat, e.photo, e.idEtat ";
            req += "from exemplaire e join document d on e.id=d.id ";
            req += "where e.id = @id ";
            req += "order by e.dateAchat DESC";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", idDocument}
                };

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);

            while (curs.Read())
            {
                string idDocuement = (string)curs.Field("id");
                int numero = (int)curs.Field("numero");
                DateTime dateAchat = (DateTime)curs.Field("dateAchat");
                string photo = (string)curs.Field("photo");
                string idEtat = (string)curs.Field("idEtat");
                Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocuement);
                lesExemplaires.Add(exemplaire);
            }
            curs.Close();

            return lesExemplaires;
        }
        #endregion

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire"></param>
        /// <returns>true si l'insertion a pu se faire</returns>
        public static bool CreerExemplaire(Exemplaire exemplaire)
        {
            try
            {
                string req = "insert into exemplaire values (@idDocument,@numero,@dateAchat,@photo,@idEtat)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idDocument", exemplaire.IdDocument},
                    { "@numero", exemplaire.Numero},
                    { "@dateAchat", exemplaire.DateAchat},
                    { "@photo", exemplaire.Photo},
                    { "@idEtat",exemplaire.IdEtat}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }catch{
                return false;
            }
        }
        /// <summary>
        /// créer une commande
        /// </summary>
        /// <param name="commande"></param>
        /// <returns>id de la commande créer</returns>
        public static string CreerCommande(Commande commande)
        {
            string idString = "";
            try
            {
                List<string> lesId = new List<string>();
                List<int> lesIdInt = new List<int>();
                string req = "Select id ";
                req += "from commande";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@montant", commande.Montant},
                    { "@dateCommande", commande.DateCommande}
                };

                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqSelect(req, parameters);
                while (curs.Read())
                {
                    string id = (string)curs.Field("id");
                    lesId.Add(id);
                }
                curs.Close();
                foreach(string id in lesId)
                {
                    lesIdInt.Add(int.Parse(id));
                }
                idString = (lesIdInt.Max() + 1).ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                string req = "insert into commande values (@id, @dateCommande, @montant)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", idString},
                    { "@montant", commande.Montant},
                    { "@dateCommande", commande.DateCommande}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return idString;
        }
        /// <summary>
        /// créer une commande de document
        /// </summary>
        /// <param name="commande"></param>
        /// <returns>true si l'insertion a pu se faire</returns>
        public static bool CreerCommandeLivreDvd(Commande commande)
        {
            string id = CreerCommande(commande);
            try
            {
                string req = "insert into commandeDocument values (@id, @nbExemplaire, @idLivreDvd, @idSuivi)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", id},
                    { "@nbExemplaire", commande.NbExemplaire},
                    { "@idLivreDvd", commande.IdLivreDvd},
                    { "@idSuivi", commande.IdSuivi}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un document
        /// </summary>
        /// <param name="document">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool ModifierCommande(Commande commande)
        {
            try
            {
                string req = "UPDATE commande set dateCommande = @dateCommande, montant = @montant where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", commande.Id},
                    { "@dateCommande", commande.DateCommande},
                    { "@montant", commande.Montant}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un document
        /// </summary>
        /// <param name="document">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool ModifierCommandeLivreDvd(Commande commande)
        {
            ModifierCommande(commande);
            try
            {
                string req = "UPDATE commandedocument set nbExemplaire = @nbExemplaire, idLivreDvd = @idLivreDvd, idSuivi = @idSuivi where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", commande.Id},
                    { "@nbExemplaire", commande.NbExemplaire},
                    { "@idSuivi", commande.IdSuivi},
                    { "@idLivreDvd", commande.IdLivreDvd}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un document
        /// </summary>
        /// <param name="document">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        private static bool ModifierDocument(Document document)
        {
            try
            {
                string req = "UPDATE document set titre = @titre, image = @image, idRayon = @idRayon, idPublic = @idPublic, idGenre = @idGenre where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", document.Id},
                    { "@titre", document.Titre},
                    { "@image", document.Image},
                    { "@idRayon", document.IdRayon},
                    { "@idPublic",document.IdPublic},
                    { "@idGenre",document.IdGenre}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un livre
        /// </summary>
        /// <param name="livre">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool ModifierLivre(Livre livre)
        {
            try
            {
                Document document = new Document(livre.Id, livre.Titre, livre.Image, livre.IdGenre, livre.Genre, livre.IdPublic,livre.Public, livre.IdRayon, livre.Rayon);
                if (!ModifierDocument(document))
                {
                    throw new Exception();
                }
                string req = "UPDATE livre set ISBN = @ISBN, auteur = @auteur, collection = @collection where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", livre.Id},
                    { "@ISBN", livre.Isbn},
                    { "@auteur", livre.Auteur},
                    { "@collection", livre.Collection}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un dvd
        /// </summary>
        /// <param name="dvd">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool ModifierDvd(Dvd dvd)
        {
            try
            {
                Document document = new Document(dvd.Id, dvd.Titre, dvd.Image, dvd.IdGenre, dvd.Genre, dvd.IdPublic, dvd.Public, dvd.IdRayon, dvd.Rayon);
                if (!ModifierDocument(document))
                {
                    throw new Exception();
                }
                string req = "UPDATE dvd set synopsis = @synopsis, realisateur = @realisateur, duree = @duree where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", dvd.Id},
                    { "@synopsis", dvd.Synopsis},
                    { "@realisateur", dvd.Realisateur},
                    { "@duree", dvd.Duree}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un livre
        /// </summary>
        /// <param name="revue">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool ModifierRevue(Revue revue)
        {
            try
            {
                Document document = new Document(revue.Id, revue.Titre, revue.Image, revue.IdGenre, revue.Genre, revue.IdPublic, revue.Public, revue.IdRayon, revue.Rayon);
                if (!ModifierDocument(document))
                {
                    throw new Exception();
                }
                string req = "UPDATE revue set empruntable = @empruntable, periodicite = @periodicite, delaiMiseADispo = @delaiMiseADispo where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", revue.Id},
                    { "@empruntable", revue.Empruntable},
                    { "@periodicite", revue.Periodicite},
                    { "@delaiMiseADispo", revue.DelaiMiseADispo}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ajout d'un document
        /// </summary>
        /// <param name="document">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        private static bool AjouterDocument(Document document)
        {
            try
            {
                string req = "insert into document (id, titre, image, idRayon, idPublic, idGenre) values (@id, @titre,@image,@idRayon,@idPublic,@idGenre)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", document.Id},
                    { "@titre", document.Titre},
                    { "@image", document.Image},
                    { "@idRayon", document.IdRayon},
                    { "@idPublic",document.IdPublic},
                    { "@idGenre",document.IdGenre}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ajout d'un livre_dvd
        /// </summary>
        /// <param name="id">id a ajouter</param>
        /// <returns>true si la modification à pu se faire</returns>
        private static bool AjouterLivreDvd(string id)
        {
            try
            {
                string req = "insert into livres_dvd (id) values (@id)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", id}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un livre
        /// </summary>
        /// <param name="livre">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool AjouterLivre(Livre livre)
        {
            try
            {
                Document document = new Document(livre.Id, livre.Titre, livre.Image, livre.IdGenre, livre.Genre, livre.IdPublic, livre.Public, livre.IdRayon, livre.Rayon);
                if (!AjouterDocument(document))
                {
                    throw new Exception();
                }
                if (!AjouterLivreDvd(livre.Id))
                {
                    throw new Exception();
                }
                string req = "insert into livre (id, ISBN, auteur, collection) values (@id, @ISBN,@auteur,@collection)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", livre.Id},
                    { "@ISBN", livre.Isbn},
                    { "@auteur", livre.Auteur},
                    { "@collection", livre.Collection}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// suppression d'un element dans la table document
        /// </summary>
        /// <param name="id">id de l'element à supprimer</param>
        public static bool SupprDocument(string id)
        {
            try
            {
                string req = "delete from document where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", id}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// suppression d'une commande LivreDvd
        /// </summary>
        /// <param name="commande">commande à supprimer</param>
        public static bool SupprCommandeLivreDvd(Commande commande)
        {
            try
            {
                string req = "delete from commandedocument where id = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", commande.Id}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un dvd
        /// </summary>
        /// <param name="dvd">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool AjouterDvd(Dvd dvd)
        {
            try
            {
                Document document = new Document(dvd.Id, dvd.Titre, dvd.Image, dvd.IdGenre, dvd.Genre, dvd.IdPublic, dvd.Public, dvd.IdRayon, dvd.Rayon);
                if (!AjouterDocument(document))
                {
                    throw new Exception();
                }
                if (!AjouterLivreDvd(dvd.Id))
                {
                    throw new Exception();
                }
                string req = "insert into dvd(id, synopsis, realisateur, duree) values (@id, @synopsis, @realisateur, @duree)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", dvd.Id},
                    { "@synopsis", dvd.Synopsis},
                    { "@realisateur", dvd.Realisateur},
                    { "@duree", dvd.Duree}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Modification d'un livre
        /// </summary>
        /// <param name="revue">document à modifier</param>
        /// <returns>true si la modification à pu se faire</returns>
        public static bool AjouterRevue(Revue revue)
        {
            try
            {
                Document document = new Document(revue.Id, revue.Titre, revue.Image, revue.IdGenre, revue.Genre, revue.IdPublic, revue.Public, revue.IdRayon, revue.Rayon);
                if (!AjouterDocument(document))
                {
                    throw new Exception();
                }
                string req = "insert into revue (id, empruntable, periodicite, delaiMiseADispo) values (@id, @empruntable, @periodicite, @delaiMiseADispo)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", revue.Id},
                    { "@empruntable", revue.Empruntable},
                    { "@periodicite", revue.Periodicite},
                    { "@delaiMiseADispo", revue.DelaiMiseADispo}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
