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
        /// Registry
        /// create a new user
        /// </summary>
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
                    Console.WriteLine(reader.GetSqlValue(0)+ "test" + reader.GetSqlString(0) );
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





        // Termine in Datenbank speichern 
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


        // TO FIX: Den Wert der User-ID aus der Datenbank auslesen
        public void Delete_date()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string delete = ("DELETE FROM Calendar WHERE TerminID = @TerminID");
            
            SqlCommand cmd_delete = new SqlCommand(delete, con);
            
            cmd_delete.ExecuteNonQuery();
            MessageBox.Show("Termin wurde erfolgreich gelöscht");
       
            con.Close();
        }


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
        /// Ermittelt die Anzahl an vorhandenen Kindservern
        /// </summary>
        public int AnzahlKindserver(int ServerID)
        {
            int anzahl = 0;
            string selection;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            selection = String.Format("SELECT anzVerbindungen FROM Serverlist WHERE 'Linker Kinderserver' != NULL AND 'Rechter Kinderserver != NULL AND ServerID == '{0}'", ServerID);

            SqlCommand selectcommand = new SqlCommand(selection, con);

           SqlDataReader reader = selectcommand.ExecuteReader();

            anzahl = reader.GetInt32(0);

            con.Close();

            return anzahl;
        }

        /// <summary>
        /// Prueft ob ein User ueberhaupt existiert
        /// </summary>
        public bool UserExist (int ServerID)
        {

            //Warum brauche ich eine ServerID für einen User?
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string check = String.Format("SELECT ServerID FROM Serverlist WHERE ServerID == '{0}'", ServerID);

            SqlCommand checkcommand = new SqlCommand(check, con);

            SqlDataReader reader = checkcommand.ExecuteReader();

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
        public void SaveInvites (List<Package> member, int TerminID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            int index = member.Count();

            for ( int i = 0; i < member.Count(); i++)
            {
                string saveEvent = string.Format("INSERT INTO Memberlist(TerminID, User) VALUES ({0}, {0})", TerminID, member[index].user.name.ToString());

                SqlCommand saveEventCommand = new SqlCommand(saveEvent, con);

                saveEventCommand.Parameters.AddWithValue("@TerminID", TerminID);
                saveEventCommand.Parameters.AddWithValue("@User", member[i].user.name.ToString());
            }
        }

        /// <summary>
        /// Liest die Termine von den ausgewaehlten Mitgliedern aus der Memberlist
        /// und gibt sie zurueck.
        /// </summary>
        public List<KaEvent> ReadInvites(string user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            List<KaEvent> test = new List<KaEvent>();
            int index = 0;

            //Liest alle TerminIDs fuer einen bestimmten User aus der Memberlist aus.
            string readInvitation = string.Format("SELECT TerminID FROM Memberlist WHERE User == '{0}'", user);

            //string exist = "SELECT TerminID FROM Memberlist WHERE EXISTS(SELECT * FROM Memberlist WHERE User = {0}", user);";

            SqlCommand readEventCommand = new SqlCommand(readInvitation, con);

            SqlDataReader reader = readEventCommand.ExecuteReader();

            while(reader.Read())
            {
                // Liest die Termine mit den zuvor ermittelten TerminIDs aus der Tabelle calendar
                string readDates = string.Format("SELECT * FROM calendar WHERE TerminID == '{0}'", reader.GetInt32(index));

                SqlCommand read = new SqlCommand(readDates, con);
                SqlDataReader reader2 = read.ExecuteReader();



                index++;
            }
            con.Close();

            return test;
        }

        private void ReadSingleRow(IDataRecord reader)
        {
            throw new NotImplementedException();
        }

        public string getServer(int serverID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string readID = String.Format("SELECT IPAdresse FROM Serverlist WHERE ServerID == '{0}'", serverID);

            SqlCommand readCommand = new SqlCommand(readID, con);

            SqlDataReader reader = readCommand.ExecuteReader();

            string read = reader.GetString(0);

            con.Close();
            return read;
        }

        /// <summary>
        /// ID des "linken" Kindservers auslesen
        /// </summary>
        public int GetIDlinkerKindserver (int ServerID)
        {
            int ID = 0;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string readID = String.Format("SELECT 'IDlinks' FROM Serverlist WHERE ServerID == '{0}'", ServerID);

            SqlCommand readCommand = new SqlCommand(readID, con);

            SqlDataReader reader = readCommand.ExecuteReader();

            ID = reader.GetInt32(0);

            con.Close();
            return ID;
        }


        /// <summary>
        /// ID des "rechten" Kindservers auslesen
        /// </summary>
        public int GetIDrechterKindserver(int ServerID)
        {
            int ID = 0;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string readID = String.Format("SELECT 'IDrechts' FROM Serverlist WHERE ServerID == '{0}'", ServerID);

            SqlCommand readCommand = new SqlCommand(readID, con);

            SqlDataReader reader = readCommand.ExecuteReader();

            return ID;
        }

        /// <summary>
        /// ID des "linken" Kindservers auslesen
        /// </summary>

        public LinkedList<string> GetWellKnownPeers()
        {
            throw new NotImplementedException();
        }

        public int getUserCount()
        {
            throw new NotImplementedException();
        }

        public bool getUser(User user)
        {
            throw new NotImplementedException();
        }

        public int getServerCount()
        {
            throw new NotImplementedException();
        }
    }
}