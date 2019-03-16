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
    class Database : IDatabase
    {
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
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\Source\\Repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
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
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\Source\\Repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                con.Open();

                //Pruefen ob der Benutzer existiert
                string exist = "SELECT Benutzername FROM Registry WHERE EXISTS(SELECT * FROM Registry WHERE Benutzername = @username AND Passwort = @password);";

                SqlCommand cmd = new SqlCommand(exist, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
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

    }
}
