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

                    ///This creates a new table for each new user
                    // string newTable = "SELECT * INTO @username FROM calendar";
                    // SqlCommand cmd_newTable = new SqlCommand(newTable, con);
                    //cmd_newTable.Parameters.AddWithValue("@username", user.name); // This doesn't work for some inexplicable reason

                    //This works...
                    //cmd_newTable.CommandText = "SELECT * INTO " + user.name + " FROM calendar";
                    //Alternative for no Data copy
                    //cmd_newTable.CommandText = "SELECT TOP 0 INTO " + user.name + " FROM calendar";

                    //cmd_newTable.ExecuteNonQuery();


                    con.Close();

                    con.Open();
                    string insert = "Insert INTO Registry (Benutzername, Passwort) VALUES(@username, @password);";
                    //insert = String.Format("INSERT INTO Registry (Benutzername, Passwort) values('{0}', '{1}');", user.name, user.password);
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
                //string exist = "SELECT Benutzername FROM Registry WHERE EXISTS(SELECT * FROM Registry WHERE Benutzername = '@username' AND Passwort = '@password');";
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
            //This recursively saves given Event to every invitees calendar
            //if(kaEvent.members.Count > 0)
            //{
                //KaEvent invitee = new KaEvent(kaEvent); //Shallow copy
                //invitee.owner = new User(kaEvent.members[0]);
                //kaEvent.members.RemoveAt(0);
                //try
                //{
                    //SaveEvent(invitee);
                //}
                //catch (Exception e) //This isn't pretty on Client side, but it shouldn't abort if just one of the members names is wrong
                //{
                    //Console.WriteLine(e.GetType().FullName);
                    //Console.WriteLine(e.Message);
                //}
                
            //}

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string insert = "insert into " + kaEvent.owner.name +
            //    " (" + kaEvent.Titel + ", " + kaEvent.Ort + ", " + kaEvent.Ganztaegig + ", " + kaEvent.Beginn + ", " + kaEvent.Ende + ", " + kaEvent.Prioritaet + ", " + kaEvent.Beschreibung + ", " + kaEvent.Haeufigkeit + ", " + kaEvent.Haeufigkeit_Anzahl + ", " +
            //    kaEvent.Immer_Wiederholen + ", " + kaEvent.Wiederholungen + ", " + kaEvent.Wiederholen_bis + ", " + kaEvent.XMontag + ", " + kaEvent.XDienstag + ", " + kaEvent.XMittwoch + ", " + kaEvent.XDonnerstag + ", " + kaEvent.XFreitag + ", " + kaEvent.XSamstag + ", " + kaEvent.XSonntag + ") " +
            //    "values(@Titel, @Ort, @Ganztaegig, @Beginn, @Ende, @Prioritaet, @Beschreibung, @Haeufigkeit, @Haeufigkeit_Anzahl, @Immer_Wiederholen, @Wiederholungen, @Wiederholen_bis, @XMontag, @XDienstag, @XMittwoch, @XDonnerstag, @XFreitag, @XSamstag, @XSonntag)";
            //string ins2 = String.Format("INSERT INTO {0} (Titel, Beginn, Ende)  VALUES ({1}, {2}, {3})", kaEvent.owner.name, kaEvent.Titel, kaEvent.Beginn.ToUniversalTime(), kaEvent.Ende.ToUniversalTime());
            string ins3 = String.Format("INSERT INTO Calendar (Titel, Ort, Beginn, Ende, Beschreibung, Benutzername)  VALUES (@Titel, @Ort, @Beginn, @Ende, @Beschreibung, @Benutzername)", kaEvent.owner.name);
            //Titel, Beginn, Ende
            //string insert = String.Format("INSERT INTO {0} (Titel, Ort, Ganztaegig, Beginn, Ende, Prioritaet, Beschreibung, Haeufigkeit, Haeufigkeit_Anzahl, Immer_Wiederholen, Wiederholungen, Wiederholen_bis, XMontag, XDienstag, XMittwoch, XDonnerstag, XFreitag, XSamstag, XSonntag) " +
            //    "VALUES (@Titel, @Ort, @Ganztaegig, @Beginn, @Ende, @Prioritaet, @Beschreibung, @Haeufigkeit, @Haeufigkeit_Anzahl, @Immer_Wiederholen, @Wiederholungen, @Wiederholen_bis, @XMontag, @XDienstag, @XMittwoch, @XDonnerstag, @XFreitag, @XSamstag, @XSonntag)");


            SqlCommand cmd_insert = new SqlCommand(ins3, con);

            
            cmd_insert.Parameters.AddWithValue("@Titel", kaEvent.Titel);
            cmd_insert.Parameters.AddWithValue("@Ort", kaEvent.Ort);
            cmd_insert.Parameters.AddWithValue("@Beginn", kaEvent.Beginn);
            cmd_insert.Parameters.AddWithValue("@Ende", kaEvent.Ende);
            cmd_insert.Parameters.AddWithValue("@Beschreibung", kaEvent.Beschreibung);
            cmd_insert.Parameters.AddWithValue("@Benutzername", kaEvent.owner.name);



            //Cheezy workaround
            /*cmd_insert.CommandText = "insert into " + kaEvent.owner.name +
                " (Titel, Ort, Ganztaegig, Beginn, Ende, Prioritaet, Beschreibung, Haeufigkeit, Haeufigkeit_Anzahl, Immer_Wiederholen, Wiederholungen, Wiederholen_bis, XMontag, XDienstag, XMittwoch, XDonnerstag, XFreitag, XSamstag, XSonntag) " +
                "values(" + kaEvent.Titel + ", " + kaEvent.Ort + ", " + kaEvent.Ganztaegig + ", " + kaEvent.Beginn + ", " + kaEvent.Ende + ", " + kaEvent.Prioritaet + ", " + kaEvent.Beschreibung + ", " + kaEvent.Haeufigkeit + ", " + kaEvent.Haeufigkeit_Anzahl + ", " +
                kaEvent.Immer_Wiederholen + ", " + kaEvent.Wiederholungen + ", " + kaEvent.Wiederholen_bis + ", " + kaEvent.XMontag + ", " + kaEvent.XDienstag + ", " + kaEvent.XMittwoch + ", " + kaEvent.XDonnerstag + ", " + kaEvent.XFreitag + ", " + kaEvent.XSamstag + ", " + kaEvent.XSonntag + ")";

            cmd_insert.CommandText = "insert into " + kaEvent.owner.name +
                "(Titel, Ort, Ganztaegig, Beginn, Ende, Prioritaet, Beschreibung, Haeufigkeit, Haeufigkeit_Anzahl, Immer_Wiederholen, Wiederholungen, Wiederholen_bis, XMontag, XDienstag, XMittwoch, XDonnerstag, XFreitag, XSamstag, XSonntag) " +
                "values(@Titel, @Ort, @Ganztaegig, @Beginn, @Ende, @Prioritaet, @Beschreibung, @Haeufigkeit, @Haeufigkeit_Anzahl, @Immer_Wiederholen, @Wiederholungen, @Wiederholen_bis, @XMontag, @XDienstag, @XMittwoch, @XDonnerstag, @XFreitag, @XSamstag, @XSonntag)";*/

            Console.WriteLine(cmd_insert.CommandText);//debugging

            /*
            ///To insert into seperate tables ... DOESNT WORK WHY????
            cmd_insert.Parameters.AddWithValue("@username", kaEvent.owner.name); //Name of owner is sufficient, hence no need for another user object reference
            */

            /*
            cmd_insert.Parameters.AddWithValue("@Titel", kaEvent.Titel);
            cmd_insert.Parameters.AddWithValue("@Ort", kaEvent.Ort);
            cmd_insert.Parameters.AddWithValue("@Ganztaegig", kaEvent.Ganztaegig);

            cmd_insert.Parameters.AddWithValue("@Beginn", kaEvent.Beginn);
            cmd_insert.Parameters.AddWithValue("@Ende", kaEvent.Ende);

            cmd_insert.Parameters.AddWithValue("@Prioritaet", kaEvent.Prioritaet);
            cmd_insert.Parameters.AddWithValue("@Beschreibung", kaEvent.Beschreibung);
            cmd_insert.Parameters.AddWithValue("@Haeufigkeit", kaEvent.Haeufigkeit);
            cmd_insert.Parameters.AddWithValue("@Haeufigkeit_Anzahl", kaEvent.Haeufigkeit_Anzahl);
            cmd_insert.Parameters.AddWithValue("@Immer_Wiederholen", kaEvent.Immer_Wiederholen);
            cmd_insert.Parameters.AddWithValue("@Wiederholungen", kaEvent.Wiederholungen);
            cmd_insert.Parameters.AddWithValue("@Wiederholen_bis", kaEvent.Wiederholen_bis);

            cmd_insert.Parameters.AddWithValue("@XMontag", kaEvent.XMontag);
            cmd_insert.Parameters.AddWithValue("@XDienstag", kaEvent.XDienstag);
            cmd_insert.Parameters.AddWithValue("@XMittwoch", kaEvent.XMittwoch);
            cmd_insert.Parameters.AddWithValue("@XDonnerstag", kaEvent.XDonnerstag);
            cmd_insert.Parameters.AddWithValue("@XFreitag", kaEvent.XFreitag);
            cmd_insert.Parameters.AddWithValue("@XSamstag", kaEvent.XSamstag);
            cmd_insert.Parameters.AddWithValue("@XSonntag", kaEvent.XSonntag);
            */

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
            string delete = ("DELETE FROM Calendar WHERE User-ID = @user-id");
            
            SqlCommand cmd_delete = new SqlCommand(delete, con);
            //cmd_delete.Parameters.AddWithValue("@User-ID",UserID);
           
            cmd_delete.ExecuteNonQuery();
            MessageBox.Show("Termin wurde erfolgreich gelöscht");
       
            con.Close();
        }




        public List<KaEvent> read(string owner)
        {
            List<KaEvent> ka = new List<KaEvent>();
            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";

            SqlConnection con = new SqlConnection(connectionString);
            //SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");

            con.Open();

            //Pruefen ob der Benutzer existiert
            string exist = "SELECT * FROM calendar Where username =";
            exist = String.Format("SELECT * FROM calendar where Benutzername = '{0}'", owner);

            SqlCommand com = new SqlCommand(exist, con);
            //com.Parameters.AddWithValue("@table", owner);


            KaEvent temp = new KaEvent();
            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    temp.TerminID = reader.GetInt32(0);
                    temp.Titel = reader.GetString(1);
                    temp.Ort = reader.GetString(2);
                    //temp.Ganztaegig = reader.GetInt32(3);
                    temp.Beginn = reader.GetDateTime(4);
                    temp.Ende = reader.GetDateTime(5);
                    //temp.Prioritaet = reader.GetInt32(6);
                    temp.Beschreibung = reader.GetString(7);
                    //temp.Haeufigkeit = reader.GetString(8);
                    //temp.Haeufigkeit_Anzahl = reader.GetInt32(9);
                    //temp.Immer_Wiederholen = reader.GetInt32(10);
                    //temp.Wiederholungen = reader.GetInt32(11);
                    //temp.Wiederholen_bis = reader.GetDateTime(12);
                    //temp.XMontag = reader.GetInt32(13);
                    //temp.XDienstag = reader.GetInt32(14);
                    //temp.XMittwoch = reader.GetInt32(15);
                    //temp.XDonnerstag = reader.GetInt32(16);
                    //temp.XFreitag = reader.GetInt32(17);
                    //temp.XSamstag = reader.GetInt32(18);
                    //temp.XSonntag = reader.GetInt32(19);

         
                    Console.WriteLine(temp.Titel);
                }
                ka.Add(temp);

            }

            return ka;

        }

    }


}

            







            

