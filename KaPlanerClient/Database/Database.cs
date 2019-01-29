using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace KaPlaner.Database
{
    class Database : IDatabase
    {
        public Database()
        {

        }
        public void registerUser(string username, string password)
        {
            //Syntaxe



            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Swathi_Su\Source\Repos\Asianrich\KaPlaner\KaPlanerClient\Data\User_Calendar.mdf;Integrated Security=True"].ConnectionString);
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
            con.Open();
            string insert = "insert into Table (username,password) values(@username, @password)";
            SqlCommand cmd = new SqlCommand(insert, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
            con.Close();





        }

        

    }
}
