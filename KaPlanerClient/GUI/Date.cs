using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KaPlaner.Database;
using KaPlaner.Objects;

namespace WindowsFormsApp1
{
    public partial class Wdw_KaEvent : Form
    {
        public KaEvent returnValue;

        public Wdw_KaEvent()
        {
            InitializeComponent();






        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void Cb_ganztägige_verantstaltung_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_ganztägige_verantstaltung.Checked == true)
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
            else if(cb_ganztägige_verantstaltung.Checked == false)
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

        /// <summary>
        /// Save All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_speichern_Click(object sender, EventArgs e)
        {
            this.returnValue = new KaEvent();

            this.Save();



            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// write from the GUI in the object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name=""></param>
        public void Save()
        {

            //Values Tab1
            returnValue.Title = TB_Title.Text;
            returnValue.Ort = TB_Place.Text;

            if (CB_always_repeat.Checked == true)
            {
                returnValue.ganztaegig = 1;
            }
            else if (CB_always_repeat.Checked == false)
            {
                returnValue.ganztaegig = 0;
            }

            returnValue.Beginn = new DateTime(
                
                Int32.Parse(TB_year_beginn.Text),
                Int32.Parse(TB_month_beginn.Text),
                Convert.ToInt32(TB_day_beginn.Text),
                Int32.Parse(TB_hour_beginn.Text),
                Int32.Parse(TB_minute_beginn.Text),
                0);

            returnValue.Ende = new DateTime(
                Int32.Parse(TB_year_end.Text),
                Int32.Parse(TB_month_end.Text),
                Int32.Parse(TB_day_end.Text),
                Int32.Parse(TB_hour_end.Text),
                Int32.Parse(TB_minute_end.Text),
                0);

            returnValue.Prioritaet = Decimal.ToInt32(NUD_Priority.Value);
            returnValue.Beschreibung = RTB_description.Text;

            //Values Tab2
            if (CB_none.Checked == true) { returnValue.Haeufigkeit = "keine"; }
            else if(CB_dayli.Checked == true) { returnValue.Haeufigkeit = "taeglich"; }
            else if(CB_weekly.Checked == true) { returnValue.Haeufigkeit = "woechentlich"; }
            else if(CB_monthly.Checked == true) { returnValue.Haeufigkeit = "monatlich";  }
            else if(CB_yearly.Checked == true) { returnValue.Haeufigkeit = "jaehrlich";  }

            returnValue.Haeufigkeit_Anzahl = Int32.Parse(TB_number_repetitions.Text);

            if(CB_always_repeat.Checked == true) { returnValue.Immer_Wiederholen = 1; }
            else if(CB_always_repeat.Checked == false) { returnValue.Immer_Wiederholen = 0; }

            returnValue.Wiederholungen = Int32.Parse(TB_times_repeat.Text);

            returnValue.Wiederholen_bis = new DateTime(
                Int32.Parse(TB_repeat_until_day.Text),
                Int32.Parse(TB_repeat_until_month.Text),
                Int32.Parse(TB_repeat_until_year.Text)
            );

            if (CB_mon.Checked == true) { returnValue.Wochentag = "Montag"; }
            else if (CB_die.Checked == true) { returnValue.Wochentag = "Dienstag"; }
            else if (CB_mit.Checked == true) { returnValue.Wochentag = "Mittwoch"; }
            else if (CB_don.Checked == true) { returnValue.Wochentag = "Donnerstag"; }
            else if (CB_fre.Checked == true) { returnValue.Wochentag = "Freitag"; }
            else if (CB_sam.Checked == true) { returnValue.Wochentag = "Samstag"; }
            else if (CB_son.Checked == true) { returnValue.Wochentag = "Sonntag"; }

        }

        //always repeat
        private void CB_always_repeat_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_always_repeat.Checked == true)
            {
                TB_times_repeat.Enabled = false;
                LBL_times_repeat.Enabled = false;
                LBL_always_repeat_until.Enabled = false;
                TB_repeat_until_day.Enabled = false;
                TB_repeat_until_month.Enabled = false;
                TB_repeat_until_year.Enabled = false;

                TB_times_repeat.Text = "0";
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
            }
        }

        //none
        private void CB_none_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_none.Checked == true)
            {
                pan_frequency.Enabled = false;
                pan_constraint.Enabled = false;
                pan_which_day.Enabled = false;
                pan_weekday.Enabled = false;
                lbl_dates_with_actions.Enabled = false;
                MC_date_summery.Enabled = false;
            }
            else if (CB_none.Checked == false)
            {
                pan_frequency.Enabled = true;
                pan_constraint.Enabled = true;
                pan_which_day.Enabled = true;
                pan_weekday.Enabled = true;
                lbl_dates_with_actions.Enabled = true;
                MC_date_summery.Enabled = true;
            }
        }

        //dayli
        private void CB_dayli_CheckedChanged(object sender, EventArgs e)
        {
            
            if (CB_dayli.Checked == true)
            {
                CB_none.Enabled = false;
                CB_weekly.Enabled = false;
                CB_monthly.Enabled = false;
                CB_yearly.Enabled = false;
                TB_number_repetitions.Enabled = false;
                pan_constraint.Enabled = false;
            }
            else if (CB_dayli.Checked == false)
            {
                CB_none.Enabled = true;
                CB_weekly.Enabled = true;
                CB_monthly.Enabled = true;
                CB_yearly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                pan_constraint.Enabled = true;
            }
        }

        //weekly
        private void CB_weekly_CheckedChanged(object sender, EventArgs e)
        {    
            if (CB_weekly.Checked == true)
            {
                CB_none.Enabled = false;
                CB_dayli.Enabled = false;
                CB_monthly.Enabled = false;
                CB_yearly.Enabled = false;
                TB_number_repetitions.Enabled = false;
                lbl_times_per.Enabled = false;
                pan_constraint.Enabled = false;
            }
            else if (CB_weekly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_monthly.Enabled = true;
                CB_yearly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;
                pan_constraint.Enabled = true;
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
                TB_number_repetitions.Enabled = false;
                lbl_times_per.Enabled = false;
                pan_constraint.Enabled = false;
            }
            else if (CB_monthly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_weekly.Enabled = true;
                CB_yearly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;
                pan_constraint.Enabled = true;
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
                TB_number_repetitions.Enabled = false;
                lbl_times_per.Enabled = false;
                pan_constraint.Enabled = false;
            }
            else if (CB_yearly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_weekly.Enabled = true;
                CB_monthly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;
                pan_constraint.Enabled = true;
            }
        }

        private void MC_date_summery_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}
