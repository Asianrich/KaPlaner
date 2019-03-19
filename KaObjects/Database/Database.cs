﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;



namespace KaObjects.Storage
{
    public class Database : IDatabase {

        string connectionString;
        private readonly object UserID;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Registry
        /// create a new user
        /// </summary>
        public bool registerUser(User user, string password_bestaetigen)
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
                    string insert = "insert into Registry (Benutzername,Passwort) values(@username, @password)";
                    SqlCommand cmd_insert = new SqlCommand(insert, con);
                    cmd_insert.Parameters.AddWithValue("@username", user.name);
                    cmd_insert.Parameters.AddWithValue("@password", user.password);
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
                string exist = "SELECT Benutzername FROM Registry WHERE EXISTS(SELECT * FROM Registry WHERE Benutzername = @username AND Passwort = @password);";

                SqlCommand cmd = new SqlCommand(exist, con);
                cmd.Parameters.AddWithValue("@username", user.name);
                cmd.Parameters.AddWithValue("@password", user.password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
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
        public void save(KaEvent kaEvent)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string insert = "insert into Calendar " +
                "(Titel, Ort, Ganztaegig, Beginn, Ende, Prioritaet, Beschreibung, Haeufigkeit, Haeufigkeit_Anzahl, Immer_Wiederholen, Wiederholungen, Wiederholen_bis, XMontag, XDienstag, XMittwoch, XDonnerstag, XFreitag, XSamstag, XSonntag) " +
          "values(@Titel, @Ort, @Ganztaegig, @Beginn, @Ende, @Prioritaet, @Beschreibung, @Haeufigkeit, @Haeufigkeit_Anzahl, @Immer_Wiederholen, @Wiederholungen, @Wiederholen_bis, @XMontag, @XDienstag, @XMittwoch, @XDonnerstag, @XFreitag, @XSamstag, @XSonntag)";
            SqlCommand cmd_insert = new SqlCommand(insert, con);

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

            cmd_insert.ExecuteNonQuery();
            con.Close();
            return;
        }

        //public void Delete_date()
        //{
            //SqlConnection con = new SqlConnection(connectionString);
            //con.Open();
            //string delete = ("DELETE FROM Calendar WHERE UserID = @userid");
            //string delete = "DELETE FROM database.calendar WHERE UserID =' " + this.UserID.txt.Text + " '; ";
            //SqlCommand cmd_delete = new SqlCommand(delete, con);
            //cmd_delete.Parameters.AddWithValue("@UserID");
           
            //cmd_delete.ExecuteNonQuery();
            //MessageBox.Show("Termin wurde erfolgreich gelöscht");

         
            //con.Close();
        //}


    }


}

            







            
