using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;


namespace KaObjects.Storage
{
    public class Database : IDatabase {

        string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Datenbank Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool login(User user)
        {
            if (String.IsNullOrEmpty(user.password) || String.IsNullOrEmpty(user.name))
            {
                MessageBox.Show("Die Felder fuer Passwort und Benutzername duerfen nicht leer sein.");
                return false;
            }
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                //Pruefen ob der Benutzer existiert
                string exist = "SELECT Benutzername FROM Registry WHERE Benutzername = 'yxc' AND Passwort = 'yxc'";
                exist = String.Format("SELECT Benutzername FROM Registry WHERE Benutzername = '{0}' AND Passwort = '{1}'", user.name, user.password);
                SqlCommand cmd = new SqlCommand(exist, con);
                cmd.Parameters.AddWithValue("@username", user.name);
                cmd.Parameters.AddWithValue("@password", user.password);
                Console.WriteLine("Received {0} {1}", user.name, user.password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine(reader.GetSqlValue(0) + "test" + reader.GetSqlString(0));
                    con.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show(String.Format("Benutzername {0} oder Passwort nicht korrekt.", user.name));
                    return false;
                }
            }
        }

        /// <summary>
        /// Registry
        /// create a new user
        /// </summary>
        /// <param name="TerminID"></param>
        /// <returns></returns>
        public bool registerUser(User user,string password_bestaetigen)
        {
            if (String.IsNullOrEmpty(user.name) || String.IsNullOrEmpty(user.password) || String.IsNullOrEmpty(password_bestaetigen))
            {
                MessageBox.Show("Die Felder fuer das Passwort duerfen nicht leer sein.");
                return false;
            }
            else if (String.Equals(user.password, password_bestaetigen))
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                //Pruefen ob der Benutzer schon existiert
                string exist = "SELECT Benutzername FROM Registry WHERE EXISTS(SELECT * FROM Registry WHERE Benutzername = @username);";
                SqlCommand cmd_exist = new SqlCommand(exist, con);
                cmd_exist.Parameters.AddWithValue("@username", user.name);
                SqlDataReader reader_exists = cmd_exist.ExecuteReader();

                if (reader_exists.Read())
                {
                    MessageBox.Show(String.Format("Benutzername {0} existiert bereits.", user.name));
                    return false;
                }
                else
                {
                    reader_exists.Close();

                    con.Close();

                    con.Open();
                    string insert = "Insert INTO Registry (Benutzername, Passwort) VALUES(@username, @password);";
                    SqlCommand cmd_insert = new SqlCommand(insert, con);
                    cmd_insert.Parameters.AddWithValue("@username", user.name);
                    cmd_insert.Parameters.AddWithValue("@password", user.password);

                    int test = cmd_insert.ExecuteNonQuery();
                    Console.WriteLine("Rows affected {0}", test);

                    con.Close();
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Die von Ihnen eingegebenen Passwoerter sind nicht identisch.");
                return false;
            }
        }


        /// <summary>
        /// Termine in Datenbank speichern 
        /// </summary>
        /// <param name="TerminID"></param>
        /// <returns></returns>
        public void SaveEvent(KaEvent kaEvent)
        {

            // TOFIX: Zur Überprüfung ob Memberliste leer ist, die Methode
            // CheckMemberList hier aufrufen

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string ins3 = String.Format("INSERT INTO Calendar (Titel, Ort, Beginn, Ende, Beschreibung, Benutzername)  VALUES (@Titel, @Ort, @Beginn, @Ende, @Beschreibung, @Benutzername)", kaEvent.owner.name);

            SqlCommand cmd_insert = new SqlCommand(ins3, con);
            
            cmd_insert.Parameters.AddWithValue("@Titel", kaEvent.Titel);
            cmd_insert.Parameters.AddWithValue("@Ort", kaEvent.Ort);
            cmd_insert.Parameters.AddWithValue("@Beginn", kaEvent.Beginn);
            cmd_insert.Parameters.AddWithValue("@Ende", kaEvent.Ende);
            cmd_insert.Parameters.AddWithValue("@Beschreibung", kaEvent.Beschreibung);
            cmd_insert.Parameters.AddWithValue("@Benutzername", kaEvent.owner.name);

            Console.WriteLine(cmd_insert.CommandText);//debugging  

            cmd_insert.ExecuteNonQuery();
            con.Close();
            return; //Können wir überprüfen ob es geklappt hat?
        }


        /// <summary>
        /// Loads every Event in a month to store in a list
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<KaEvent> LoadEvents(User user, DateTime month)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

#pragma warning disable CS0219 // The variable 'select' is assigned but its value is never used
            string select = "SELECT * FROM calendar WHERE @Beginn ";// Select auf Tabelle des Nutzers (alle Events eines Monats)
#pragma warning restore CS0219 // The variable 'select' is assigned but its value is never used

            return null; //kaEvents
        }

        /// <summary>
        /// Liest die Termine von einem bestimmten Benutzer aus der Datenbank aus
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>
        /// Gibt die Termine des Benutzers in einer Liste  
        /// mit KaEvent-Objekten zurueck.
        /// </returns>
        public List<KaEvent> read(string owner)
        {
            List<KaEvent> ka = new List<KaEvent>();

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            //Pruefen ob der Benutzer existiert
            string exist = "SELECT * FROM calendar Where username =";
            exist = String.Format("SELECT * FROM calendar where Benutzername = '{0}'", owner);

            SqlCommand com = new SqlCommand(exist, con);

            KaEvent temp = new KaEvent();
            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    temp.TerminID = reader.GetInt32(0);
                    temp.Titel = reader.GetString(1);
                    temp.Ort = reader.GetString(2);
                    temp.Beginn = reader.GetDateTime(4);
                    temp.Ende = reader.GetDateTime(5);
                    temp.Beschreibung = reader.GetString(7);

                    Console.WriteLine(temp.Titel);
                    ka.Add(temp);

                    temp = new KaEvent();
                }
            }
            else
            {
                ka.Add(temp);
            }
            return ka;
        }


        /// <summary>
        /// Loescht einen Termin mit der TerminID aus der Datenbanktabelle calendar
        /// </summary>
        /// <param name="TerminID"></param>
        /// <returns>Kein Rueckgabewert. (void)</returns>
        public void Delete_date(int TerminID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string delete = ("DELETE oescht einen Termin mit der TerminID aus FROM Calendar WHERE TerminID = @TerminID");
            DecoderReplacementFallback Datenbank;
            
            SqlCommand cmd_delete = new SqlCommand(delete, con);
            
            cmd_delete.ExecuteNonQuery();
            MessageBox.Show("Termin wurde erfolgreich geloescht");
       
            con.Close();
        }

        /// <summary>
        /// Fuegt die IP-Adresse und die ID eines neuen 
        /// Servers in die Tabelle Serverlist hinzu.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="id"></param>
        /// <returns>Kein Rueckgabewert. (void)</returns>
        public void newServerEntry(string ip, int id)
        {
            //TODO UnitTests

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            string ins = String.Format("INSERT INTO ServerList(ServerID, IPAdresse) VALUES (@serverid, @ip)");

            SqlCommand cmd_insert = new SqlCommand(ins, con);
            cmd_insert.Parameters.AddWithValue("@serverid", id);
            cmd_insert.Parameters.AddWithValue("@ip", ip);

            cmd_insert.ExecuteNonQuery();
            con.Close();

        }

        /// <summary>
        /// Prueft ob ein Server mit der ServerID in der Serverlist existiert.
        /// </summary>
        /// <param name="ServerID"></param>
        /// <returns>
        /// false, wenn aus der Datenbank keine IP fuer die ServerID ausgelesen werden kann.
        /// true, in allen anderen Faellen
        /// </returns>
        public bool ServerExist (int ServerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string checkServer = String.Format("SELECT ServerID FROM Serverlist WHERE ServerID = '{0}'", ServerID);

            SqlCommand checkCommand = new SqlCommand(checkServer, con);

            SqlDataReader reader = checkCommand.ExecuteReader();

            con.Close();

            if (reader.GetString(0) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Prueft ob ein User mit einem bestimmten Namen in Registry existiert.
        /// </summary>
        /// <param name="user">Objekt vom Typ User</param>
        /// <returns>
        /// false, wenn aus der Datenbank keine Bentuzername fuer die UserID 
        /// ausgelesen werden kann.
        /// true, in allen anderen Faellen
        /// </returns>
        public bool UserExist(string user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string checkUser= String.Format("SELECT Benutzername FROM Registry WHERE Benutzername = '{0}'", user);

            SqlCommand checkCommand = new SqlCommand(checkUser, con);

            SqlDataReader reader = checkCommand.ExecuteReader();

            con.Close();

            if (reader.GetString(0) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Prueft ob MemberListe leer ist
        /// </summary>
        /// <param>Keine Uebergabeparameter</param>
        /// <returns>
        /// Die Anzahl der Eintraege in der Tabelle Memberlist.
        /// </returns>
        public int CheckMemberList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string Check = string.Format("SELECT COUNT(TerminID) FROM Memberlist");
            int read = 0;
           
                SqlCommand checkCommand = new SqlCommand(Check, con);

                SqlDataReader reader = checkCommand.ExecuteReader();

                if (reader.Read())
                { 
                    read = reader.GetInt32(0);
                }

            con.Close();

            return read;
        }


        /// <summary>
        /// Speichert Mitglieder eines Termins in der Memberlist
        /// </summary>
        /// <param name="member">Liste von Beteiligten an einem Bestimmten Termin</param>
        /// <param name="TerminID">ID des behandelten Termins</param>
        /// <returns>Keine Rueckgabewerte</returns>
        public void SaveInvites (string user, int TerminID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            if(UserExist(user))
            {
                string saveEvent = string.Format("INSERT INTO Memberlist(TerminID, User) VALUES ({0}, {0})", TerminID, user);

                SqlCommand saveEventCommand = new SqlCommand(saveEvent, con);

                saveEventCommand.Parameters.AddWithValue("@TerminID", TerminID);
                saveEventCommand.Parameters.AddWithValue("@User", user);
            }
        }


        /// <summary>
        /// Liest die Termine von dem ausgewaehlten Mitglied aus der Memberlist
        /// und gibt sie zurueck.
        /// </summary>
        /// <param name="user">ID des behandelten Termins</param>
        /// <returns>Liste von KaEvent-Objekten</returns>
        public List<KaEvent> ReadInvites(string user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            List<KaEvent> test = new List<KaEvent>();
            int index = 0;

            //Liest alle TerminIDs fuer einen bestimmten User aus der Memberlist aus.
            string readInvitation = string.Format("SELECT TerminID FROM Memberlist WHERE User = '{0}'", user);

            //string exist = "SELECT TerminID FROM Memberlist WHERE EXISTS(SELECT * FROM Memberlist WHERE User = {0}", user);";

            SqlCommand readEventCommand = new SqlCommand(readInvitation, con);

            SqlDataReader reader = readEventCommand.ExecuteReader();

            while(reader.Read())
            {
                // Liest die Termine mit den zuvor ermittelten TerminIDs aus der Tabelle calendar
                string readDates = string.Format("SELECT * FROM calendar WHERE TerminID = '{0}'", reader.GetInt32(index));

                SqlCommand read = new SqlCommand(readDates, con);
                SqlDataReader reader2 = read.ExecuteReader();

                index++;
            }
            con.Close();

            return test;
        }


        /// <summary>
        /// Gitbt die IP-Adresse des mit serverID ausgewaehlten Servers
        /// aus der Serverlist zurueck.
        /// </summary>
        /// <param name="ServerID"></param>
        /// <returns>IP-Adresse des gesuchten Servers</returns>
        public string getServer(int ServerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string readID = String.Format("SELECT IPAdresse FROM Serverlist WHERE ServerID = '{0}'", ServerID);

            SqlCommand readCommand = new SqlCommand(readID, con);

            SqlDataReader reader = readCommand.ExecuteReader();

            string read = reader.GetString(0);

            con.Close();
            return read;
        }

        /// <summary>
        /// Gitbt die Anzahl an registrierten User auf einem Server zureuck.
        /// </summary>
        /// <param >Kein Uebergabeparameter</param>
        /// <returns>Anzahl User pro Server</returns>
        public int getUserCount()
        {
            int anzahl = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string readCount = String.Format("SELECT COUNT(*) AS ANZ FROM Registry");

                SqlCommand readCommand = new SqlCommand(readCount, con);

                SqlDataReader reader = readCommand.ExecuteReader();

                if (reader.Read())
                {
                    anzahl = reader.GetInt32(0);
                }
                con.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return anzahl;
        }


        /// <summary>
        /// Gibt die Anzahl an Kindservern zurueck aus der Serverlist zurueck.
        /// </summary>
        /// <param >Kein Uebergabeparameter</param>
        /// <returns>Anzahl Kindserver </returns>
        public int getServerCount()
        {
            int Count = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string readCount = String.Format("SELECT COUNT(*) AS ANZ FROM ServerList");

                SqlCommand readCommand = new SqlCommand(readCount, con);

                SqlDataReader reader = readCommand.ExecuteReader();

                if (reader.Read())
                {
                    Count = reader.GetInt32(0);
                }
                con.Close();
            }
            catch (Exception)
            {

            }
            return Count;
        }

        //public LinkedList<string> GetWellKnownPeers()
        //{
        //    throw new NotImplementedException();
        //}

        //private void ReadSingleRow(IDataRecord reader)
        //{
        //    throw new NotImplementedException();
        //}
    }
}