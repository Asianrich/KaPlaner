﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KaObjects;
using System.Data.SqlClient;
using KaPlaner.Storage;

namespace WindowsFormsApp1
{
    public partial class Wdw_KaEvent : Form
    {
        public KaEvent returnValue;

        public Wdw_KaEvent()
        {
            InitializeComponent();

            returnValue = new KaEvent();

            DateTime localDate = DateTime.Now;
            TB_repeat_until_day.Text = DateTime.Now.ToString("dd");
            TB_repeat_until_month.Text = DateTime.Now.ToString("MM");
            TB_repeat_until_year.Text = DateTime.Now.ToString("yyyy");
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// btn_speichern_click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_speichern_Click(object sender, EventArgs e)
        {
            KaPlaner.Storage.Database reg = new Database();
            this.Write();
            reg.Save(returnValue.Title, returnValue.Ort, returnValue.ganztaegig, returnValue.Beginn, returnValue.Ende, returnValue.Prioritaet, returnValue.Beschreibung, returnValue.Haeufigkeit,
                returnValue.Haeufigkeit_Anzahl, returnValue.Immer_Wiederholen, returnValue.Wiederholungen, returnValue.Wiederholen_bis, returnValue.Wochentag, returnValue.XMontag,
                returnValue.XDienstag, returnValue.XMittwoch, returnValue.XDonnerstag, returnValue.XFreitag, returnValue.XSamstag, returnValue.XSonntag);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        /// #######################################
        /// <summary>
        /// values from tab1
        /// </summary>

        //TB_Title
        private void TB_Title_TextChanged(object sender, EventArgs e)
        {
            returnValue.Title = TB_Title.Text;
        }

        //TB_Place
        private void TB_Place_TextChanged(object sender, EventArgs e)
        {
            returnValue.Ort = TB_Place.Text;
        }

        //CB_ganztaegige_veranstaltung
        private void CB_ganztaegige_verantstaltung_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_ganztägige_verantstaltung.Checked == true)
            {
                LBL_hour_beginn.Enabled = false;
                LBL_minute_beginn.Enabled = false;

                TB_hour_beginn.Enabled = false;
                TB_minute_beginn.Enabled = false;

                TB_minute_beginn.Text = "00";
                TB_hour_beginn.Text = "08";

                LBL_hour_end.Enabled = false;
                LBL_minute_end.Enabled = false;

                TB_minute_end.Enabled = false;
                TB_hour_end.Enabled = false;

                TB_minute_end.Text = "00";
                TB_hour_end.Text = "20";
            }
            else if (CB_ganztägige_verantstaltung.Checked == false)
            {
                LBL_hour_beginn.Enabled = true;
                LBL_minute_beginn.Enabled = true;

                TB_hour_beginn.Enabled = true;
                TB_minute_beginn.Enabled = true;

                TB_minute_beginn.Text = "";
                TB_hour_beginn.Text = "";

                LBL_hour_end.Enabled = true;
                LBL_minute_end.Enabled = true;

                TB_minute_end.Enabled = true;
                TB_hour_end.Enabled = true;

                TB_minute_end.Text = "";
                TB_hour_end.Text = "";
            }
        }

        // write the date from the GUI in the object
        public void Write()
        {
            int year = Convert.ToInt32(TB_year_beginn.Text);
            int month = Convert.ToInt32(TB_month_beginn.Text);
            int day = Convert.ToInt32(TB_day_beginn.Text);
            int hour = Convert.ToInt32(TB_hour_beginn.Text);
            int minute = Convert.ToInt32(TB_minute_beginn.Text);
            int sec = 0;
            returnValue.Beginn = new DateTime(year, month, day, hour, minute, sec);

            year = Convert.ToInt32(TB_year_end.Text);
            month = Convert.ToInt32(TB_month_end.Text);
            day = Convert.ToInt32(TB_day_end.Text);
            hour = Convert.ToInt32(TB_hour_end.Text);
            minute = Convert.ToInt32(TB_minute_end.Text);
            returnValue.Ende = new DateTime(year, month, day, hour, minute, sec);

            year = Convert.ToInt32(TB_repeat_until_year.Text);
            month = Convert.ToInt32(TB_repeat_until_month.Text);
            day = Convert.ToInt32(TB_repeat_until_day.Text);
            minute = 0;
            returnValue.Wiederholen_bis = new DateTime(year, month, day, hour, minute, sec);
        }

        //NUD_Priority
        private void NUD_Priority_ValueChanged(object sender, EventArgs e)
        {
            returnValue.Prioritaet = Decimal.ToInt32(NUD_Priority.Value);
        }

        //RTB_description
        private void RTB_description_TextChanged(object sender, EventArgs e)
        {
            returnValue.Beschreibung = RTB_description.Text;
        }


        /// #######################################
        /// <summary>
        /// values from tab2
        /// </summary>

        //none
        private void CB_none_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_none.Checked == true)
            {
                pan_frequency.Enabled = false;
                pan_constraint.Enabled = false;
                pan_weekday.Enabled = false;
                pan_which_day.Enabled = false;
                lbl_dates_with_actions.Enabled = false;
                MC_date_summery.Enabled = false;

                returnValue.Haeufigkeit = "keine";
            }
            else if (CB_none.Checked == false)
            {
                pan_frequency.Enabled = true;
                pan_constraint.Enabled = true;
                pan_which_day.Enabled = true;
                pan_weekday.Enabled = true;
                lbl_dates_with_actions.Enabled = true;
                MC_date_summery.Enabled = true;

                returnValue.Haeufigkeit = "";
            }
        }

        //dayli
        private void CB_dayli_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_dayli.Checked)
            {
                CB_none.Enabled = false;
                CB_weekly.Enabled = false;
                CB_monthly.Enabled = false;
                CB_yearly.Enabled = false;
                TB_number_repetitions.Enabled = true;
                pan_constraint.Enabled = true;
                pan_weekday.Enabled = true;
                pan_which_day.Enabled = false;

                returnValue.Haeufigkeit = "taeglich";
                TB_number_repetitions.Text = "1";
            }
            else if (CB_dayli.Checked == false)
            {
                CB_none.Enabled = true;
                CB_weekly.Enabled = true;
                CB_monthly.Enabled = true;
                CB_yearly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                pan_frequency.Enabled = true;
                pan_which_day.Enabled = true;

                returnValue.Haeufigkeit = "";
                TB_number_repetitions.Text = "0";
            }
        }

        //weekly
        private void CB_weekly_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_weekly.Checked)
            {
                CB_none.Enabled = false;
                CB_dayli.Enabled = false;
                CB_monthly.Enabled = false;
                CB_yearly.Enabled = false;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = false;
                pan_which_day.Enabled = false;
                returnValue.Haeufigkeit = "woechentlich";

            }
            else if (CB_weekly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_monthly.Enabled = true;
                CB_yearly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;
                TB_times_repeat.Enabled = true;
                pan_which_day.Enabled = true;

                returnValue.Haeufigkeit = "";
            }
        }

        //monthly
        private void CB_monthly_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_monthly.Checked == true)
            {
                CB_none.Enabled = false;
                CB_dayli.Enabled = false;
                CB_weekly.Enabled = false;
                CB_yearly.Enabled = false;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;

                returnValue.Haeufigkeit = "monatlich";
            }
            else if (CB_monthly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_weekly.Enabled = true;
                CB_yearly.Enabled = true;

                returnValue.Haeufigkeit = "";
            }
        }

        //yearly
        private void CB_yearly_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_yearly.Checked == true)
            {
                CB_none.Enabled = false;
                CB_dayli.Enabled = false;
                CB_weekly.Enabled = false;
                CB_monthly.Enabled = false;

                returnValue.Haeufigkeit = "jaehrlich";
            }
            else if (CB_yearly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_weekly.Enabled = true;
                CB_monthly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;

                returnValue.Haeufigkeit = "";
            }
        }

        //TB_number_repetitions
        private void TB_number_repetitions_TextChanged(object sender, EventArgs e)
        {
            returnValue.Haeufigkeit_Anzahl = Convert.ToInt32(TB_number_repetitions.Text);
        }

        /// <summary>
        /// always repeat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_always_repeat_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_always_repeat.Checked)
            {
                TB_times_repeat.Enabled = false;
                LBL_times_repeat.Enabled = false;
                LBL_always_repeat_until.Enabled = false;
                TB_repeat_until_day.Enabled = false;
                TB_repeat_until_month.Enabled = false;
                TB_repeat_until_year.Enabled = false;

                TB_times_repeat.Text = "0";
                returnValue.Immer_Wiederholen = 1;
            }
            else if (CB_always_repeat.Checked == false)
            {
                TB_times_repeat.Enabled = true;
                LBL_times_repeat.Enabled = true;
                LBL_always_repeat_until.Enabled = true;
                TB_repeat_until_day.Enabled = true;
                TB_repeat_until_month.Enabled = true;
                TB_repeat_until_year.Enabled = true;

                TB_times_repeat.Text = "1";
                returnValue.Immer_Wiederholen = 0;
            }
        }

        //TB_times_repeat
        private void TB_times_repeat_TextChanged(object sender, EventArgs e)
        {
            returnValue.Wiederholungen = Convert.ToInt32(TB_times_repeat.Text);
        }

        //CB_mon 
        private void CB_mon_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_mon.Checked == false)
            {
                returnValue.XMontag = -1;
                NUD_mon.Enabled = false;
            }
            else
            {
                NUD_mon.Enabled = true;
            }
        }

        //CB_die
        private void CB_die_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_die.Checked == false)
            {
                returnValue.XDienstag = -1;
                NUD_tue.Enabled = false;
            }
            else
            {
                NUD_tue.Enabled = true;
            }
        }

        //CB_mit
        private void CB_mit_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_mit.Checked == false)
            {
                returnValue.XMittwoch = -1;
                NUD_wen.Enabled = false;
            }
            else
            {
                NUD_wen.Enabled = true;
            }
        }

        //CB_don
        private void CB_don_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_don.Checked == false)
            {
                returnValue.XDonnerstag = -1;
                NUD_thu.Enabled = false;
            }
            else
            {
                NUD_thu.Enabled = true;
            }
        }

        //CB_fre
        private void CB_fre_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_fre.Checked == false)
            {
                returnValue.XFreitag = -1;
                NUD_fri.Enabled = false;
            }
            else
            {
                NUD_fri.Enabled = true;
            }
        }

        //CB_sam
        private void CB_sam_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_sam.Checked == false)
            {
                returnValue.XSamstag = -1;
                NUD_sat.Enabled = false;
            }
            else
            {
                NUD_sat.Enabled = true;
            }
        }

        //CB_son
        private void CB_son_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_son.Checked == false)
            {
                returnValue.XSonntag = -1;
                NUD_sun.Enabled = false;
            }
            else
            {
                NUD_sun.Enabled = true;
            }
        }

        //NUD_mon
        private void NUD_mon_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XMontag = Convert.ToInt32(NUD_mon.Value);
        }

        //NUD_tue
        private void NUD_tue_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XDienstag = Convert.ToInt32(NUD_tue.Value);
        }

        //NUD_wen
        private void NUD_wen_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XMittwoch = Convert.ToInt32(NUD_wen.Value);
        }

        //NUD_thu
        private void NUD_thu_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XDonnerstag = Convert.ToInt32(NUD_thu.Value);
        }

        //NUD_fri
        private void NUD_fri_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XFreitag = Convert.ToInt32(NUD_fri.Value);
        }

        //NUD_sat
        private void NUD_sat_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XSamstag = Convert.ToInt32(NUD_sat.Value);
        }

        //NUD_sun
        private void NUD_sun_ValueChanged(object sender, EventArgs e)
        {
            returnValue.XSonntag = Convert.ToInt32(NUD_sun.Value);
        }

        //MC_date_summery
        private void MC_date_summery_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void BTN_delete_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True");
            con.Open();

            string delete = "delete from Calendar where Titel,Ort,Tag,Monat,Jahr,Stunde,Minute,Prioritaet,Beschreibung,Haeufigkeit,Beschraenkung,Wochentag,Welcher_Tag) values(@titel, @ort, @monat, @jahr, @stunde, @minute, @priorität, @beschreibung,@haeufigkeit,@beschraenkung,@wochentag,@welcher_tag)";
            SqlCommand cmd_delete = new SqlCommand(delete, con);

            cmd_delete.ExecuteNonQuery();
            MessageBox.Show("Termin wurde erfolgreich gelöscht");

            con.Close();
        }

    }  /// #######################################
   
}
