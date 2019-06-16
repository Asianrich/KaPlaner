using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KaObjects;
using KaPlaner.Logic;

namespace WindowsFormsApp1
{
    public partial class Wdw_KaEvent : Form
    {
        IClientLogic clientLogic = ClientActivator.clientLogic;

        public KaEvent returnValue;
        DateTime date;
        bool isOnline;

        /// <summary>
        /// wrote the informations from an object of the class KaEvent to the input mask
        /// </summary>
        public Wdw_KaEvent(KaEvent ereignis, DateTime date = new DateTime(), bool isOnline = false)
        {
            InitializeComponent();
            this.date = date;
            this.isOnline = isOnline;
            TB_day_beginn.Text = date.Day.ToString();
            TB_day_end.Text = date.Day.ToString();
            TB_month_beginn.Text = date.Month.ToString();
            TB_month_end.Text = date.Month.ToString();
            TB_year_beginn.Text = date.Year.ToString();
            TB_year_end.Text = date.Year.ToString();

            returnValue = new KaEvent();

            /*
            if (ereignis != null)
            {
                TB_Title.Text = ereignis.Titel;
                TB_Place.Text = ereignis.Ort;

                TB_day_beginn.Text = ereignis.Beginn.ToString("dd");
                TB_month_beginn.Text = ereignis.Beginn.ToString("MM");
                TB_year_beginn.Text = ereignis.Beginn.ToString("yyyy");
                TB_hour_beginn.Text = ereignis.Beginn.ToString("HH");
                TB_minute_beginn.Text = ereignis.Beginn.ToString("mm");

                TB_day_end.Text = ereignis.Beginn.ToString("dd");
                TB_month_end.Text = ereignis.Beginn.ToString("MM");
                TB_year_end.Text = ereignis.Beginn.ToString("yyyy");
                TB_hour_end.Text = ereignis.Beginn.ToString("HH");
                TB_minute_end.Text = ereignis.Beginn.ToString("mm");

                //NUD_Priority.Value = ereignis.Prioritaet;

                RTB_description.Text = ereignis.Beschreibung;

                switch (ereignis.Haeufigkeit)
                {
                    case "keine":
                        CB_none.Checked = true;
                        pan_frequency.Enabled = false;
                        break;
                    case "taeglich":
                        CB_dayli.Checked = true;
                        break;
                    case "woechentlich":
                        CB_weekly.Checked = true;
                        break;
                    case "monatlich":
                        CB_monthly.Checked = true;
                        break;
                    case "jaehrlich":
                        CB_yearly.Checked = true;
                        break;
                }*/

                /*
                TB_number_repetitions.Text = ereignis.Haeufigkeit_Anzahl.ToString();

                if (ereignis.Immer_Wiederholen == 1) { CB_always_repeat.Checked = true; }
                else { CB_always_repeat.Checked = false; }

                TB_times_repeat.Text = ereignis.Wiederholungen.ToString();

                TB_repeat_until_day.Text = ereignis.Wiederholen_bis.ToString("dd");
                TB_repeat_until_month.Text = ereignis.Wiederholen_bis.ToString("MM");
                TB_repeat_until_year.Text = ereignis.Wiederholen_bis.ToString("yyyy");

                if (ereignis.XMontag <= -1) { CB_mon.Checked = false; }
                else { CB_mon.Checked = true; }
                if (ereignis.XDienstag <= -1) { CB_die.Checked = false; }
                else { CB_die.Checked = true; }
                if (ereignis.XMittwoch <= -1) { CB_mit.Checked = false; }
                else { CB_mit.Checked = true; }
                if (ereignis.XDonnerstag <= -1) { CB_don.Checked = false; }
                else { CB_don.Checked = true; }
                if (ereignis.XFreitag <= -1) { CB_fre.Checked = false; }
                else { CB_fre.Checked = true; }
                if (ereignis.XSamstag <= -1) { CB_sam.Checked = false; }
                else { CB_sam.Checked = true; }
                if (ereignis.XSonntag <= -1) { CB_son.Checked = false; }
                else { CB_son.Checked = true; }

                NUD_mon.Value = ereignis.XMontag;
                NUD_tue.Value = ereignis.XDienstag;
                NUD_wen.Value = ereignis.XMittwoch;
                NUD_thu.Value = ereignis.XDonnerstag;
                NUD_fri.Value = ereignis.XFreitag;
                NUD_sat.Value = ereignis.XSamstag;
                NUD_sun.Value = ereignis.XSonntag;
                
            }
            */
        }


        public void onlyNumber(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != ',')
                //Copy & paste zulassen
                if (Char.IsControl((e.KeyChar)))
                {
                }
                //Nur Nummern zulassen
                else if (!Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
        }

        private void EmptyBox(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;

            if(box.Text == String.Empty)
            {
                box.Text = "0";
            }
            if(Int32.TryParse(box.Text, out int result))
            {
                if(result > 60)
                {
                    box.Text = "60";
                }
                else if(result < 0)
                {
                    box.Text = "0";
                }
            }


        }

        private void BTN_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// BTN_save_Click
        /// </summary>
        private void BTN_save_Click(object sender, EventArgs e)
        {
            try
            {
                this.Write();
                if (!isOnline)
                {
                    clientLogic.SaveLocal(returnValue);
                }
                else
                {
                    clientLogic.SaveRemote(returnValue);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// #######################################
        /// <summary>
        /// values from tab1
        /// </summary>

        //TB_Title
        private void TB_Title_TextChanged(object sender, EventArgs e)
        {
            returnValue.Titel = TB_Title.Text;
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

        /// <summary>
        /// write the informations from the input mask in an object of the class KaEvent
        /// </summary>
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

 /*
            if (!CB_none.Checked)
            {
                year = Convert.ToInt32(TB_repeat_until_year.Text);
                month = Convert.ToInt32(TB_repeat_until_month.Text);
                day = Convert.ToInt32(TB_repeat_until_day.Text);
                minute = 0;
                returnValue.Wiederholen_bis = new DateTime(year, month, day, hour, minute, sec);
            }
            else
            {
                returnValue.Wiederholen_bis = new DateTime(2019,1,1,1,1,1); //Another cheezy trick just to avoid sql errors
            }
*/
        }

        //NUD_Priority
/*        private void NUD_Priority_ValueChanged(object sender, EventArgs e)
        {
            returnValue.Prioritaet = Decimal.ToInt32(NUD_Priority.Value);
        }
*/

        //RTB_description
        private void RTB_description_TextChanged(object sender, EventArgs e)
        {
            returnValue.Beschreibung = RTB_description.Text;
        }


        /// #######################################
        /// <summary>
        /// values from tab2
        /// none, dayli, weekly, monthly, yearly activate respectively deactivate the respectively other Checkboxes
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

//                returnValue.Haeufigkeit = "keine";
            }
            else if (CB_none.Checked == false)
            {
                pan_frequency.Enabled = true;
                pan_constraint.Enabled = true;
                pan_which_day.Enabled = true;
                pan_weekday.Enabled = true;
                lbl_dates_with_actions.Enabled = true;
                MC_date_summery.Enabled = true;

//                returnValue.Haeufigkeit = "";
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

//                returnValue.Haeufigkeit = "taeglich";
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

//                returnValue.Haeufigkeit = "";
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
//               returnValue.Haeufigkeit = "woechentlich";

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

//                returnValue.Haeufigkeit = "";
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

//                returnValue.Haeufigkeit = "monatlich";
            }
            else if (CB_monthly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_weekly.Enabled = true;
                CB_yearly.Enabled = true;

//                returnValue.Haeufigkeit = "";
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

//                returnValue.Haeufigkeit = "jaehrlich";
            }
            else if (CB_yearly.Checked == false)
            {
                CB_none.Enabled = true;
                CB_dayli.Enabled = true;
                CB_weekly.Enabled = true;
                CB_monthly.Enabled = true;
                TB_number_repetitions.Enabled = true;
                lbl_times_per.Enabled = true;

                //returnValue.Haeufigkeit = "";
            }
        }

        /// <summary>
        /// TB_number_repetitions
        /// The new value will written in the object every time if the value will changed or 0 if the textbox is empty.
        /// </summary>
        private void TB_number_repetitions_TextChanged(object sender, EventArgs e)
        {
            if(TB_number_repetitions.Text == "")
            {
//                returnValue.Haeufigkeit_Anzahl = 0;
            }
            else
            {
//                returnValue.Haeufigkeit_Anzahl = Convert.ToInt32(TB_number_repetitions.Text);
            }
        }

        /// <summary>
        /// always repeat
        /// </summary>
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
//                returnValue.Immer_Wiederholen = 1;
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
  //              returnValue.Immer_Wiederholen = 0;
            }
        }

        //TB_times_repeat
        private void TB_times_repeat_TextChanged(object sender, EventArgs e)
        {
            if(TB_times_repeat.Text == "")
            {
//                returnValue.Wiederholungen = 0;
            }
            else
            {
//                returnValue.Wiederholungen = Convert.ToInt32(TB_times_repeat.Text);
            }
        }

        /// CB_always_repeat_CheckedChanged, 
        /// CB_mon, CB_die, CB_mit, CB_don, CB_fre, CB_sam, CB_son activate or
        /// deactivate the suitable NUD-box. As symbol for the deactivation
        /// the functions write the value -1 in the object.

        //CB_mon 
        private void CB_mon_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_mon.Checked == false)
            {
//                returnValue.XMontag = -1;
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
//                returnValue.XDienstag = -1;
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
//                returnValue.XMittwoch = -1;
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
//                returnValue.XDonnerstag = -1;
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
//                returnValue.XFreitag = -1;
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
//                returnValue.XSamstag = -1;
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
//                returnValue.XSonntag = -1;
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
//          returnValue.XMontag = Convert.ToInt32(NUD_mon.Value);
        }

        //NUD_tue
        private void NUD_tue_ValueChanged(object sender, EventArgs e)
        {
//            returnValue.XDienstag = Convert.ToInt32(NUD_tue.Value);
        }

        //NUD_wen
        private void NUD_wen_ValueChanged(object sender, EventArgs e)
        {
 //           returnValue.XMittwoch = Convert.ToInt32(NUD_wen.Value);
        }

        //NUD_thu
        private void NUD_thu_ValueChanged(object sender, EventArgs e)
        {
//            returnValue.XDonnerstag = Convert.ToInt32(NUD_thu.Value);
        }

        //NUD_fri
        private void NUD_fri_ValueChanged(object sender, EventArgs e)
        {
//            returnValue.XFreitag = Convert.ToInt32(NUD_fri.Value);
        }

        //NUD_sat
        private void NUD_sat_ValueChanged(object sender, EventArgs e)
        {
//            returnValue.XSamstag = Convert.ToInt32(NUD_sat.Value);
        }

        //NUD_sun
        private void NUD_sun_ValueChanged(object sender, EventArgs e)
        {
//            returnValue.XSonntag = Convert.ToInt32(NUD_sun.Value);
        }

        //MC_date_summery
        private void MC_date_summery_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }  /// ####################################### 
}

