using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySqlConnector;
using Serilog;

namespace Mediatek86.bdd
{
    /// <summary>
    /// Classe BddMySql
    /// </summary>
    public class BddMySql
    {
        /// <summary>
        /// Unique instance de la classe
        /// </summary>
        private static BddMySql instance = null;
        
        /// <summary>
        /// objet de connexion à la BDD à partir d'une chaîne de connexion
        /// </summary>
        private readonly MySqlConnection connection;

        /// <summary>
        /// objet contenant le résultat d'une requête "select" (curseur)
        /// </summary>
        private MySqlDataReader reader;

        /// <summary>
        /// Constructeur privé pour créer la connexion à la BDD et l'ouvrir
        /// </summary>
        /// <param name="stringConnect">chaine de connexion</param>
        private BddMySql(string stringConnect)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            try
            {
                connection = new MySqlConnection(stringConnect);
                connection.Open();
                Log.Information("Ouverture de la connexion à la BDD" );
            }
            catch (MySqlException e)
            {
                Log.Warning("Erreur sur l'ouverture de la connexion avec la BDD");
                ErreurGraveBddNonAccessible(e);
            }
        }

        /// <summary>
        /// Crée une instance unique de la classe
        /// </summary>
        /// <param name="stringConnect">chaine de connexion</param>
        /// <returns>instance unique de la classe</returns>
        public static BddMySql GetInstance(string stringConnect)
        {
            if (instance is null)
            {
                instance = new BddMySql(stringConnect);
            }
            return instance;
        }

        /// <summary>
        /// Exécute une requête type "select" et valorise le curseur
        /// </summary>
        /// <param name="stringQuery"> la requete</param>
        /// <param name="parameters">les parametres</param>
        public void ReqSelect(string stringQuery, Dictionary<string, object> parameters)
        {
            MySqlCommand command;
            
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                reader = command.ExecuteReader();
            }
            catch (MySqlException e)
            {
                Log.Warning(e, "Erreur dans la BDD");
                MessageBox.Show(e.Message, "Erreur dans la BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e.Message);
            }
            catch (InvalidOperationException e)
            {
                ErreurGraveBddNonAccessible(e);
            }
        }

        /// <summary>
        /// Tente de lire la ligne suivante du curseur
        /// </summary>
        /// <returns>false si fin de curseur atteinte</returns>
        public bool Read()
        {
            if (reader is null)
            {
                return false;
            }
            try
            {
                return reader.Read();
            }
            catch(Exception e)
            {
                Log.Information(e, "Impossible de lire le curseur");
                return false;
            }
        }

        /// <summary>
        /// Retourne le contenu d'un champ dont le nom est passé en paramètre
        /// </summary>
        /// <param name="nameField">nom du champ</param>
        /// <returns>valeur du champ</returns>
        public object Field(string nameField)
        {
            if (reader is null)
            {
                return null;
            }
            try
            {
                return reader[nameField];
            }
            catch(Exception e)
            {
                Log.Information(e, "Nom de champs introuvable");
                return null;
            }
        }

        /// <summary>
        /// Exécution d'une requête autre que "select"
        /// </summary>
        /// <param name="stringQuery">requête autre que select</param>
        /// <param name="parameters">dictionnire contenant les parametres</param>
        public bool ReqUpdate(string stringQuery, Dictionary<string, object> parameters)
        {
            MySqlCommand command;
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                Log.Warning(e, "Operation impossible");
                MessageBox.Show(e.Message, "Operation impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                ErreurGraveBddNonAccessible(e);
                return false;
            }
        }

        /// <summary>
        /// Fermeture du curseur
        /// </summary>
        public void Close()
        {
            if (!(reader is null))
            {
                reader.Close();
            }
        }

        /// <summary>
        /// Pas d'accès à la BDD : arrêt de l'application
        /// </summary>
        private void ErreurGraveBddNonAccessible(Exception e)
        {
            MessageBox.Show("Base de données non accessibles", "Erreur grave");
            Log.Fatal("Base de données non accessibles");
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
    }
}
