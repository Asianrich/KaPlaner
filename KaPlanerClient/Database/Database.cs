using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using KaPlaner.Objects;
using WindowsFormsApp1;

namespace KaPlaner.Storage
{
    class Database : IDatabase    {
        public Database()
        {

        }

        /// <summary>
        /// Registry
        /// create a new user
        /// </summary>
        public bool registerUser(string username, string password, string password_bestaetigen)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(password_bestaetigen))
            {
                MessageBox.Show("Die Felder fuer das Passwort duerfen nicht leer sein.");
                return false;
            }
            else if (String.Equals(password, password_bestaetigen))
            {
                //SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                con.Open();

                //Pruefen ob der Benutzer schon existiert
                string exist = "SELECT Benutzername FROM Registry WHERE EXISTS(SELECT * FROM Registry WHERE Benutzername = @username);";
                SqlCommand cmd_exist = new SqlCommand(exist, con);
                cmd_exist.Parameters.AddWithValue("@username", username);
                SqlDataReader reader_exists = cmd_exist.ExecuteReader();

                if (reader_exists.Read())
                {
                    MessageBox.Show(String.Format("Benutzername {0} existiert bereits.", username));
                    return false;
                }
                else
                {
                    string insert = "insert into Registry (Benutzername,Passwort) values(@username, @password)";
                    SqlCommand cmd_insert = new SqlCommand(insert, con);
                    cmd_insert.Parameters.AddWithValue("@username", username);
                    cmd_insert.Parameters.AddWithValue("@password", password);
                    reader_exists.Close();
                    cmd_insert.ExecuteNonQuery();
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
        /// Login
        /// login with your login details
        /// </summary>
        public bool login(string username, string password)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(username))
            {
                MessageBox.Show("Die Felder fuer Passwort und Benutzername duerfen nicht leer sein.");
                return false;
            }
            else
            {
                //SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                con.Open();

                //Pruefen ob der Benutzer existiert
                string exist = "SELECT Benutzername FROM Registry WHERE EXISTS(SELECT * FROM Registry WHERE Benutzername = @username AND Passwort = @password);";

                SqlCommand cmd = new SqlCommand(exist, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show(String.Format("Benutzername {0} oder Passwort nicht korrekt.", username));
                    return false;
                }
            }
        }

        public void Save(string Title, string Ort, int Ganztaegig, DateTime Beginn, DateTime Ende, int Prioritaet, string Beschreibung, 
            string Haeufigkeit, int Haeufigkeit_Anzahl, int Immer_Wiederholen, int Wiederholungen, DateTime Wiederholen_bis,
            int XMontag, int XDienstag, int XMittwoch, int XDonnerstag, int XFreitag, int XSamstag, int XSonntag)
        {
            //SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
            con.Open();
            //string insert = "insert into Calendar (Titel,Ort,Tag,Monat,Jahr,Stunde,Minute,Prioritaet,Beschreibung,Haeufigkeit,Beschraenkung,Wochentag,Welcher_Tag) values(@titel, @ort, @monat, @jahr, @stunde, @minute, @priorität, @beschreibung,@haeufigkeit,@beschraenkung,@wochentag,@welcher_tag)";
            string insert = "insert into Calendar " +
                "(Titel, Ort, Ganztaegig, Beginn, Ende, Prioritaet, Beschreibung, Haeufigkeit, Haeufigkeit_Anzahl, Immer_Wiederholen, Wiederholungen, Wiederholen_bis, XMontag, XDienstag, XMittwoch, XDonnerstag, XFreitag, XSamstag, XSonntag) " +
          "values(@Titel, @Ort, @Ganztaegig, @Beginn, @Ende, @Prioritaet, @Beschreibung, @Haeufigkeit, @Haeufigkeit_Anzahl, @Immer_Wiederholen, @Wiederholungen, @Wiederholen_bis, @XMontag, @XDienstag, @XMittwoch, @XDonnerstag, @XFreitag, @XSamstag, @XSonntag)";
            SqlCommand cmd_insert = new SqlCommand(insert, con);
       
            cmd_insert.Parameters.AddWithValue("@Titel", Title);
            cmd_insert.Parameters.AddWithValue("@Ort", Ort);
            cmd_insert.Parameters.AddWithValue("@Ganztaegig", Ganztaegig);

            cmd_insert.Parameters.AddWithValue("@Beginn", Beginn);
            cmd_insert.Parameters.AddWithValue("@Ende", Ende);

            cmd_insert.Parameters.AddWithValue("@Prioritaet", Prioritaet);
            cmd_insert.Parameters.AddWithValue("@Beschreibung", Beschreibung);
            cmd_insert.Parameters.AddWithValue("@Haeufigkeit", Haeufigkeit);
            cmd_insert.Parameters.AddWithValue("@Haeufigkeit_Anzahl", Haeufigkeit_Anzahl);
            cmd_insert.Parameters.AddWithValue("@Immer_Wiederholen", Immer_Wiederholen);
            cmd_insert.Parameters.AddWithValue("@Wiederholungen", Wiederholungen);
            cmd_insert.Parameters.AddWithValue("@Wiederholen_bis", Wiederholen_bis);

            cmd_insert.Parameters.AddWithValue("@XMontag", XMontag);
            cmd_insert.Parameters.AddWithValue("@XDienstag", XDienstag);
            cmd_insert.Parameters.AddWithValue("@XMittwoch", XMittwoch);
            cmd_insert.Parameters.AddWithValue("@XDonnerstag", XDonnerstag);
            cmd_insert.Parameters.AddWithValue("@XFreitag", XFreitag);
            cmd_insert.Parameters.AddWithValue("@XSamstag", XSamstag);
            cmd_insert.Parameters.AddWithValue("@XSonntag", XSonntag);

            cmd_insert.ExecuteNonQuery();
            con.Close();
            return;
        }


       // public bool Wiederholung(string haeufigkeit,string beschraenkung, string wochentag, string welcher_tag)
        //{

        //} 








    }

}

            







            

