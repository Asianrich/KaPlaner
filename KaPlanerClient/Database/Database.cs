using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace KaPlaner.Database
{
    class Database : IDatabase
    {
        public Database()
        {

        }

        public bool registerUser(string username, string password, string password_bestaetigen)
        {
            //Registrierung
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(password_bestaetigen))
            {
                MessageBox.Show("Die Felder fuer das Passwort duerfen nicht leer sein.");
                return false;
            }
            else if (String.Equals(password, password_bestaetigen))
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\Source\\Repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                con.Open();
                string insert = "insert into Registry (Benutzername,Passwort) values(@username, @password)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            else
            {
                MessageBox.Show("Die von Ihnen eingegebenen Passwoerter sind nicht identisch.");
                return false;
            }
        }
        /*
        public bool login(string username, string password)
        {
            //Login
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(username))
            {
                MessageBox.Show("Die Felder fuer Passwort und Benutzername duerfen nicht leer sein.");
                return false;
            }
            else
            {
                //SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\Source\\Repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                //con.Open();

                //Pruefen ob Benutzerdaten existieren
                //string exist = "SELECT Benutzername, Passwort FROM Registry WHERE EXISTS (SELECT * FROM Registry WHERE Benutzername=@username AND Passwort=@password)";
                //SqlCommand cmd = new SqlCommand(exist, con);
                //cmd.ExecuteNonQuery();
                //con.Close();
                //return true;
            }
        }*/
    }
}
