using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace KaObjects.Storage
{
    public class Database : IDatabase
    {
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
                cmd_exist.Dispose();

                if (reader_exists.Read())
                {
                    reader_exists.Close();
                    con.Close();
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
                    cmd_insert.Dispose();
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

            string exist = String.Format("SELECT TerminID FROM calendar WHERE TerminID = '{0}'", kaEvent.TerminID);

            SqlCommand cmd_exist = new SqlCommand(exist, con);


            /// Liest in der Datenbank nach einem Termin mit einer bestimmten ID aus 
            SqlDataReader exist_reader = cmd_exist.ExecuteReader();


            if (exist_reader.Read())
            {
                exist_reader.Close();

                string ins3 = String.Format("UPDATE User_Calendar.mdf calendar SET Titel = @Titel, Ort = @Ort, Beginn = @Beginn, Ende = @Ende, Beschreibung = @Beschreibung, Benutzername = @Benutzername WHERE TerminID = @TerminID");

                SqlCommand cmd_update = new SqlCommand(ins3, con);

                cmd_update.Parameters.AddWithValue("@Titel", kaEvent.Titel);
                cmd_update.Parameters.AddWithValue("@Ort", kaEvent.Ort);
                cmd_update.Parameters.AddWithValue("@Beginn", kaEvent.Beginn);
                cmd_update.Parameters.AddWithValue("@Ende", kaEvent.Ende);
                cmd_update.Parameters.AddWithValue("@Beschreibung", kaEvent.Beschreibung);
                cmd_update.Parameters.AddWithValue("@Benutzername", kaEvent.owner.name);

                Console.WriteLine(cmd_update.CommandText); //debugging

                int id = (int)cmd_update.ExecuteScalar();
                cmd_update.Dispose();

                kaEvent.TerminID = id;

                con.Close();
            }
            else
            {
                exist_reader.Close();

                string ins3 = String.Format("INSERT INTO Calendar (Titel, Ort, Beginn, Ende, Beschreibung, Benutzername) Output Inserted.TerminID VALUES (@Titel, @Ort, @Beginn, @Ende, @Beschreibung, @Benutzername)", kaEvent.owner.name);

                SqlCommand cmd_insert = new SqlCommand(ins3, con);

                cmd_insert.Parameters.AddWithValue("@Titel", kaEvent.Titel);
                cmd_insert.Parameters.AddWithValue("@Ort", kaEvent.Ort);
                cmd_insert.Parameters.AddWithValue("@Beginn", kaEvent.Beginn);
                cmd_insert.Parameters.AddWithValue("@Ende", kaEvent.Ende);
                cmd_insert.Parameters.AddWithValue("@Beschreibung", kaEvent.Beschreibung);
                cmd_insert.Parameters.AddWithValue("@Benutzername", kaEvent.owner.name);

                Console.WriteLine(cmd_insert.CommandText); //debugging  

                int id = (int)cmd_insert.ExecuteScalar();
                cmd_insert.Dispose();

                kaEvent.TerminID = id;

                con.Close();
            }

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
            //string select = "SELECT * FROM calendar WHERE @Beginn ";// Select auf Tabelle des Nutzers (alle Events eines Monats)
#pragma warning restore CS0219 // The variable 'select' is assigned but its value is never used

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
        public void Delete_date(int TerminID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string delete = ("DELETE * FROM calendar WHERE TerminID = @TerminID");
            //DecoderReplacementFallback Datenbank;

            SqlCommand cmd_delete = new SqlCommand(delete, con);

            cmd_delete.ExecuteNonQuery();
            MessageBox.Show("Termin wurde erfolgreich geloescht");
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
            checkCommand.Dispose();

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
        public void SaveInvites(string user, KaEvent kaEvent)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            if (UserExist(user))
            {
                string exist = string.Format("Select * from calendar where TerminID = {0} AND Benutzername = '{1}'", kaEvent.TerminID, kaEvent.owner.name);
                SqlCommand exist_com = new SqlCommand(exist, con);
                SqlDataReader read = exist_com.ExecuteReader();
                exist_com.Dispose();
                if (!read.Read())
                {
                    SaveEvent(kaEvent);
                }
                con.Close();

                con = new SqlConnection(connectionString);
                con.Open();

                string saveEvent = string.Format("INSERT INTO Memberlist(TerminID, [User]) VALUES ({0}, '{1}')", kaEvent.TerminID, user);

                SqlCommand saveEventCommand = new SqlCommand(saveEvent, con);

                //saveEventCommand.Parameters.AddWithValue("@TerminID", kaEvent.TerminID);
                //saveEventCommand.Parameters.AddWithValue("@User", user);

                saveEventCommand.ExecuteNonQuery();
                saveEventCommand.Dispose();
            }

            con.Close();
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

            List<KaEvent> invites = new List<KaEvent>();

            //Liest alle TerminIDs fuer einen bestimmten User aus der Memberlist aus.
            string readInvitation = string.Format("SELECT TerminID FROM Memberlist WHERE [User] = '{0}'", user);

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
                string readDates = string.Format("SELECT * FROM calendar WHERE TerminID = {0}", termin);
                con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand read = new SqlCommand(readDates, con);
                SqlDataReader reader2 = read.ExecuteReader();
                read.Dispose();

                if (reader2.Read())
                {
                    invites.Add(new KaEvent()
                    {
                        TerminID = reader2.GetInt32(0),
                        Titel = reader2.GetString(1),
                        Ort = reader2.GetString(2),
                        Beginn = reader2.GetDateTime(4),
                        Ende = reader2.GetDateTime(5),
                        Beschreibung = reader2.GetString(6),
                        owner = new User(reader2.GetString(7))

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

            if (choice)
            {
                User newOwner = new User
                {
                    name = user
                };
                kaEvent.owner = newOwner;

                SaveEvent(kaEvent);
                //Dann aus der EInladungsliste löschen

            }

            string del_Com = String.Format("Delete from memberlist where TerminID = {0} AND [User] = '{0}'", kaEvent.TerminID, user);

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