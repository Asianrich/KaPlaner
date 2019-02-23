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
        public void registerUser(string username, string password, string password_bestaetigen)
        {
            //Syntaxe
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(password_bestaetigen))
            {
                MessageBox.Show("Die Felder fuer das Passwort duerfen nicht leer sein.");
            }
            else if (String.Equals(password, password_bestaetigen))
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
                con.Open();
                string insert = "insert into registry (username,password) values(@username, @password)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Die von Ihnen eingegebenen Passwoerter sind nicht identisch.");
            }

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Swathi_Su\Source\Repos\Asianrich\KaPlaner\KaPlanerClient\Data\User_Calendar.mdf;Integrated Security=True"].ConnectionString);
           





        }

        












        }
    }  


    }






}
