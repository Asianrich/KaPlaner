using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace KaObjects.Storage
{
    public class Database : IDatabase
    {
        /// <summary>
        /// Hilfsklasse um die Datenbanktabellen ähnlich eines Enums zu übergeben.
        /// </summary>
        static class DatabaseTable
        {
            public static readonly string Calendar = "calendar";
            public static readonly string Invites = "Invites";
        }
        readonly string connectionString;
        
        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Datenbank Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Login(User user)
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
                //string exist = "SELECT Benutzername FROM Registry WHERE Benutzername = 'yxc' AND Passwort = 'yxc'";
                string exist = String.Format("SELECT Benutzername FROM Registry WHERE Benutzername = '{0}' AND Passwort = '{1}'", user.name, user.password);
                SqlCommand cmd = new SqlCommand(exist, con);
                cmd.Parameters.AddWithValue("@username", user.name);
                cmd.Parameters.AddWithValue("@password", user.password);
                Console.WriteLine("Received {0} {1}", user.name, user.password);

                SqlDataReader reader = cmd.ExecuteReader();
                cmd.Dispose();
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
        public bool RegisterUser(User user, string password_bestaetigen)
        {
            if (String.IsNullOrEmpty(user.name) || String.IsNullOrEmpty(user.password) || String.IsNullOrEmpty(password_bestaetigen))
            {
                //MessageBox.Show("Die Felder fuer das Passwort duerfen nicht leer sein.");
                return false;
            }
            else if (String.Equals(user.password, password_bestaetigen))
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                //Pruefen ob der Benutzer schon existiert
                if (UserExist(user.name))
                {
                    con.Close();
                    //MessageBox.Show(String.Format("Benutzername {0} existiert bereits.", user.name));
                    return false;
                }
                else
                {
                    con.Close();

                    con.Open();
                    string insert = "Insert INTO Registry (Benutzername, Passwort) VALUES(@username, @password);";
                    SqlCommand cmd_insert = new SqlCommand(insert, con);
                    cmd_insert.Parameters.AddWithValue("@username", user.name);
                    cmd_insert.Parameters.AddWithValue("@password", user.password);

                    int test = cmd_insert.ExecuteNonQuery();
                    cmd_insert.Dispose();
                    Console.WriteLine("Rows affected {0}", test);

                    con.Close();
                    return true;
                }
            }
            else
            {
                //MessageBox.Show("Die von Ihnen eingegebenen Passwoerter sind nicht identisch.");
                return false;
            }
        }


        /// <summary>
        /// Speichert Termine in der calendar Tabelle.
        /// </summary>
        /// <param name="kaEvent">Das zu speichernde Event</param>
        public void SaveEvent(KaEvent kaEvent)
        {
            Save(kaEvent, DatabaseTable.Calendar);
        }

        /// <summary>
        /// Speichert Termin in der Invites Tabelle.
        /// Gibt InviteID zurück.
        /// </summary>
        /// <param name="kaEvent">Das zu speichernde Event</param>
        /// <returns>TerminID des Invites bei Erfolg</returns>
        public int SaveInvite(KaEvent kaEvent)
        {
            Save(kaEvent, DatabaseTable.Invites);

            return GetInviteID();

            int GetInviteID()
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string commandString = String.Format("SELECT InviteID FROM Invites WHERE Benutzername = '{0}' AND PastID = {1}", kaEvent.owner.name, kaEvent.TerminID);

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                SqlDataReader readInviteID = sqlCommand.ExecuteReader();
                sqlCommand.Dispose();

                int inviteID;

                if (readInviteID.Read())
                    inviteID = readInviteID.GetInt32(0);
                else
                    inviteID = 0;

                readInviteID.Close();
                sqlConnection.Close();

                return inviteID;
            }
        }

        /// <summary>
        /// Speichert Event in der angegebenen Tabelle.
        /// </summary>
        /// <param name="kaEvent">Das zu speichernde Event</param>
        /// <param name="table">Die zu benutzende Tabelle</param>
        private void Save(KaEvent kaEvent, string table)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string commandString;
            string exist;

            //Gibt es die TerminID bereits?
            if(table == DatabaseTable.Invites)
                exist = String.Format("SELECT InviteID FROM {0} WHERE PastID = {1} AND Benutzername = '{2}'", table, kaEvent.TerminID, kaEvent.owner.name);
            else
                exist = String.Format("SELECT TerminID FROM {0} WHERE TerminID = {1}", table, kaEvent.TerminID);
            SqlCommand cmd_exist = new SqlCommand(exist, con);

            SqlDataReader exist_reader = cmd_exist.ExecuteReader();
            cmd_exist.Dispose();

            //Update falls bereits existent
            if (exist_reader.Read())
            {
                if(table == DatabaseTable.Invites)
                    commandString = String.Format("UPDATE {0} SET Titel = @Titel, Ort = @Ort, Beginn = @Beginn, Ende = @Ende, Beschreibung = @Beschreibung WHERE Benutzername = '{1}' AND PastID = {2}", table, kaEvent.owner.name, kaEvent.TerminID);
                else
                    commandString = String.Format("UPDATE {0} SET Titel = @Titel, Ort = @Ort, Beginn = @Beginn, Ende = @Ende, Beschreibung = @Beschreibung WHERE TerminID = {1}", table, kaEvent.TerminID);
            }
            //Insert falls nicht
            else
            {
                if(table == DatabaseTable.Invites)
                    commandString = String.Format("INSERT INTO {0} (Titel, Ort, Beginn, Ende, Beschreibung, Benutzername, PastID) VALUES (@Titel, @Ort, @Beginn, @Ende, @Beschreibung, '{1}', {2})", table, kaEvent.owner.name, kaEvent.TerminID);
                else
                    commandString = String.Format("INSERT INTO {0} (Titel, Ort, Beginn, Ende, Beschreibung, Benutzername) VALUES (@Titel, @Ort, @Beginn, @Ende, @Beschreibung, '{1}')", table, kaEvent.owner.name);
            }
            exist_reader.Close();

            SqlCommand SqlCommand = new SqlCommand(commandString, con);

            AddParameters(SqlCommand);
            DebugExecute(SqlCommand);

            SqlCommand.Dispose();

            con.Close();

            ///Fügt die immergleichen Parameter in den SQL Befehl ein.
            void AddParameters(SqlCommand sqlCommand)
            {
                sqlCommand.Parameters.AddWithValue("@Titel", kaEvent.Titel);
                sqlCommand.Parameters.AddWithValue("@Ort", kaEvent.Ort);
                sqlCommand.Parameters.AddWithValue("@Beginn", kaEvent.Beginn);
                sqlCommand.Parameters.AddWithValue("@Ende", kaEvent.Ende);
                sqlCommand.Parameters.AddWithValue("@Beschreibung", kaEvent.Beschreibung);
            }

            void DebugExecute(SqlCommand sqlCommand)
            {
                Console.WriteLine("Rows affected {0}.", sqlCommand.ExecuteNonQuery());
                Console.WriteLine(sqlCommand.CommandText); //debugging
            }
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

            //string select = "SELECT * FROM calendar WHERE @Beginn ";// Select auf Tabelle des Nutzers (alle Events eines Monats)

            con.Close();

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
        public List<KaEvent> Read(string owner)
        {
            List<KaEvent> ka = new List<KaEvent>();

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            //Pruefen ob der Benutzer existiert
            //string exist = "SELECT * FROM calendar Where username =";
            string exist = String.Format("SELECT * FROM calendar where Benutzername = '{0}'", owner);

            SqlCommand com = new SqlCommand(exist, con);

            KaEvent temp = new KaEvent();
            SqlDataReader reader = com.ExecuteReader();
            com.Dispose();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    temp.TerminID = reader.GetInt32(0);
                    temp.Titel = reader.GetString(1);
                    temp.Ort = reader.GetString(2);
                    temp.Beginn = reader.GetDateTime(4);
                    temp.Ende = reader.GetDateTime(5);
                    temp.Beschreibung = reader.GetString(6);
                    temp.owner = new User(reader.GetString(7));

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
        public void DeleteEvent(int TerminID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string delete = ("DELETE FROM calendar WHERE TerminID = @TerminID");
            //DecoderReplacementFallback Datenbank;

            SqlCommand cmd_delete = new SqlCommand(delete, con);

            cmd_delete.Parameters.AddWithValue("@TerminID", TerminID);

            Console.WriteLine(cmd_delete.CommandText); //debugging  

            cmd_delete.ExecuteNonQuery();
            cmd_delete.Dispose();

            con.Close();
        }


        /// <summary>
        /// Fuegt die IP-Adresse und die ID eines neuen 
        /// Servers in die Tabelle Serverlist hinzu.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="id"></param>
        /// <returns>Kein Rueckgabewert. (void)</returns>
        public void NewServerEntry(string ip, int id)
        {
            //TODO UnitTests

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            string ins = String.Format("INSERT INTO ServerList(ServerID, IPAdresse) VALUES (@serverid, @ip)");

            SqlCommand cmd_insert = new SqlCommand(ins, con);
            cmd_insert.Parameters.AddWithValue("@serverid", id);
            cmd_insert.Parameters.AddWithValue("@ip", ip);

            cmd_insert.ExecuteNonQuery();
            cmd_insert.Dispose();
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
        public bool ServerExist(int ServerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string checkServer = String.Format("SELECT ServerID FROM Serverlist WHERE ServerID = '{0}'", ServerID);

            SqlCommand checkCommand = new SqlCommand(checkServer, con);
            SqlDataReader reader = checkCommand.ExecuteReader();
            checkCommand.Dispose();

            bool result = false;

            if (reader.Read())
            {
                con.Close();
                result = true;
            }

            con.Close();
            return result;
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

            string checkUser = String.Format("SELECT Benutzername FROM Registry WHERE Benutzername = '{0}'", user);

            SqlCommand checkCommand = new SqlCommand(checkUser, con);
            SqlDataReader reader = checkCommand.ExecuteReader();
            checkCommand.Dispose();

            bool read = false;
            if (reader.Read())
            {
                read = true;
            }
            con.Close();
            return read;
        }


        /// <summary>
        /// Prueft ob InviteList leer ist
        /// </summary>
        /// <param>Keine Uebergabeparameter</param>
        /// <returns>
        /// Die Anzahl der Eintraege in der Tabelle Memberlist.
        /// </returns>
        public int CheckInviteList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string Check = string.Format("SELECT COUNT(InviteID) FROM InviteList");
            int read = 0;

            SqlCommand checkCommand = new SqlCommand(Check, con);
            SqlDataReader reader = checkCommand.ExecuteReader();
            checkCommand.Dispose();

            if (reader.Read())
            {
                read = reader.GetInt32(0);
            }

            con.Close();

            return read;
        }


        /// <summary>
        /// Speichert das gesendete Event in der Invites Tabelle der Datenbank.
        /// Die InviteList wird mit den Verknüpfungen befüllt.
        /// </summary>
        /// <param name="username">Der Name des eingeladenen Nutzers</param>
        /// <param name="kaEvent">Das Event zu dem eingeladen wird</param>
        public void SaveInvites(string username, KaEvent kaEvent)
        {
            //Wenn es den eingeladenen Nutzer in der Datenbank gibt.
            if (UserExist(username))
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                //Das Event (Alter Nutzer) in der Invites Tabelle speichern.
                int inviteID = SaveInvite(kaEvent);

                //Referenz auf den neuen Nutzer in der InviteList vermerken.
                string saveEvent = string.Format("INSERT INTO InviteList(InviteID, [Invitee]) VALUES ({0}, '{1}')", inviteID, username);

                con.Close();
                con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand saveEventCommand = new SqlCommand(saveEvent, con);

                Console.WriteLine(string.Join("; ", saveEventCommand.Parameters)); //debugging
                Console.WriteLine("Rows affected {0}.", saveEventCommand.ExecuteNonQuery());
                Console.WriteLine(saveEventCommand.CommandText); //debugging

                saveEventCommand.Dispose();
                con.Close();
            }
        }


        /// <summary>
        /// Liest die Invites von dem ausgewaehlten Nutzer aus der Invites Tabelle
        /// und gibt sie zurueck.
        /// </summary>
        /// <param name="user">Name des Nutzers dessen Einladungen wir erhalten wollen.</param>
        /// <returns>Liste von KaEvent-Objekten</returns>
        public List<KaEvent> ReadInvites(string user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            List<KaEvent> invites = new List<KaEvent>();

            //Liest alle TerminIDs fuer einen bestimmten User aus der Memberlist aus.
            string readInvitation = string.Format("SELECT InviteID FROM InviteList WHERE [Invitee] = '{0}'", user);

            SqlCommand readEventCommand = new SqlCommand(readInvitation, con);
            SqlDataReader reader = readEventCommand.ExecuteReader();
            readEventCommand.Dispose();

            List<int> id = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id.Add(reader.GetInt32(0));
                }
            }

            reader.Close();
            con.Close();

            foreach (int termin in id)
            {
                string readDates = string.Format("SELECT * FROM Invites WHERE InviteID = {0}", termin);
                con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand read = new SqlCommand(readDates, con);
                SqlDataReader reader2 = read.ExecuteReader();
                read.Dispose();

                if (reader2.Read())
                {
                    invites.Add(new KaEvent()
                    {
                        Titel = reader2.GetString(1),
                        Ort = reader2.GetString(2),
                        Beginn = reader2.GetDateTime(4),
                        Ende = reader2.GetDateTime(5),
                        Beschreibung = reader2.GetString(6),
                        owner = new User(user)
                    });
                }

                reader2.Close();
                con.Close();
            }
            //try
            //{
            //    while (reader.Read())
            //    {
            //        // Liest die Termine mit den zuvor ermittelten TerminIDs aus der Tabelle calendar

            //        SqlCommand read = new SqlCommand(readDates, con);
            //        SqlDataReader reader2 = read.ExecuteReader();


            //        reader2.Close();
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    //MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    reader.Close();
            //    con.Close();
            //}

            return invites;
        }


        /// <summary>
        /// Gitbt die IP-Adresse des mit serverID ausgewaehlten Servers
        /// aus der Serverlist zurueck.
        /// </summary>
        /// <param name="ServerID"></param>
        /// <returns>IP-Adresse des gesuchten Servers</returns>
        public string GetServer(int ServerID)
        {
            string read = "";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string readID = String.Format("SELECT IPAdresse FROM Serverlist WHERE ServerID = '{0}'", ServerID);

            SqlCommand readCommand = new SqlCommand(readID, con);
            SqlDataReader reader = readCommand.ExecuteReader();
            readCommand.Dispose();

            if (reader.Read())
            {
                read = reader.GetString(0);
                read = read.Trim();
            }
            con.Close();
            return read;
        }

        /// <summary>
        /// Gitbt die Anzahl an registrierten User auf einem Server zureuck.
        /// </summary>
        /// <param >Kein Uebergabeparameter</param>
        /// <returns>Anzahl User pro Server</returns>
        public int GetUserCount()
        {
            int anzahl = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string readCount = String.Format("SELECT COUNT(*) AS ANZ FROM Registry");

                SqlCommand readCommand = new SqlCommand(readCount, con);
                SqlDataReader reader = readCommand.ExecuteReader();
                readCommand.Dispose();

                if (reader.Read())
                {
                    anzahl = reader.GetInt32(0);
                }
                con.Close();

            }
            catch (Exception ex)
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
        public int GetServerCount()
        {
            int Count = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string readCount = String.Format("SELECT COUNT(*) AS ANZ FROM ServerList");

                SqlCommand readCommand = new SqlCommand(readCount, con);
                SqlDataReader reader = readCommand.ExecuteReader();
                readCommand.Dispose();

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

        /// <summary>
        /// Eine Einladung akzeptieren oder nicht
        /// </summary>
        /// <param name="kaEvent">Den jeweiligen Termin</param>
        /// <param name="user">Der jeweilige eingeladene</param>
        /// <param name="choice">True=Accept, False = Ablehnen</param>
        public void AnswerInvite(KaEvent kaEvent, string user, bool choice)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string del_Com = String.Format("DELETE FROM Invites WHERE InviteID = {0}", kaEvent.TerminID);

            if (choice)
            {
                kaEvent.owner.name = user;

                SaveEvent(kaEvent);

                SqlCommand delSave = new SqlCommand(del_Com, con);
                delSave.ExecuteNonQuery();
                delSave.Dispose();
            }

            del_Com = String.Format("DELETE FROM InviteList WHERE InviteID = {0}", kaEvent.TerminID);
            SqlCommand delInvite = new SqlCommand(del_Com, con);
            delInvite.ExecuteNonQuery();

            delInvite.Dispose();
            con.Close();
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