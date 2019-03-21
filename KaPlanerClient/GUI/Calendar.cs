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
using KaPlaner.GUI;

namespace WindowsFormsApp1
{
    public enum MonthName { JANUAR, FEBRUAR, MAERZ, APRIL, MAI, JUNI, JULI, AUGUST, SEPTEMBER, OKTOBER, NOVEMBER, DEZEMBER }

    public partial class wdw_calendar : Form
    {
        IClientLogic clientLogic = ClientActivator.clientLogic;

        ClientLogic client;
#pragma warning disable CS0169 // The field 'wdw_calendar.kaEvents' is never used
        List<KaEvent> kaEvents;
#pragma warning restore CS0169 // The field 'wdw_calendar.kaEvents' is never used

        

        DateTime localDate = DateTime.Now;      //current datetime
        int monthcounter = 0;                   //month-counter
        int year = 0;                           //current year
        bool isOnline;


        private string[] month = new string[]
        { "Januar", "Februar", "Maerz", "April",
            "Mai", "Juni", "Juli", "August",
            "September", "Oktober", "November",
            "Dezember"
        };

        public wdw_calendar(bool isOnline, ClientLogic clientLogic)
        {
            
            InitializeComponent();
            this.isOnline = isOnline;
            KaEvent[] kaEvents2 = new KaEvent[3];

            client = clientLogic;

            for (int i = 0; i < 3; i++)
            {
                kaEvents2[i] = new KaEvent();
                kaEvents2[i].Titel = "Titetl " + i;
                kaEvents2[i].Ort = "Ort " + i;
                kaEvents2[i].Beginn = new DateTime(2019, 03, 03);
            }

            kaEvents = kaEvents2.ToList();

            if (!isOnline)
            {
                BTN_manual_update.Visible = false;
                BTN_manual_update.Enabled = false;
                BT_Request.Visible = false;
                BT_Request.Enabled = false;
            }
            else
            {
                BTN_manual_update.Visible = true;
                BTN_manual_update.Enabled = true;
                BT_Request.Visible = true;
                BT_Request.Enabled = true;
            }

            monthcounter = (localDate.Month - 1);
            year = localDate.Year;

            lbl_year.Text = Convert.ToString(year);
            LBL_month.Text = month[monthcounter];

            check();


        }

        private void btn_quit_calendar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void TB_open_list(object sender, EventArgs e)
        {
            try
            {
                RichTextBox trigger = (RichTextBox)sender;

                Int32.TryParse(trigger.Text, out int day);

                DateTime date = new DateTime(year, monthcounter + 1,day);



                //KaEvent[] kaEvents = new KaEvent[3];

                //for (int i = 0; i < 3; i++)
                //{
                //    kaEvents[i] = new KaEvent();
                //    kaEvents[i].Titel = "Titetl " + i;
                //    kaEvents[i].Ort = "Ort " + i;
                //}

                using (var form = new Wdw_date_list(kaEvents, date, isOnline))
                {
                    var result = form.ShowDialog();

                    kaEvents = form.ListEvents;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary> check end of old year </summary>
        private void btn_prev_Click(object sender, EventArgs e)
        {
            try
            {
                if (monthcounter == 0)
                {
                    monthcounter = 11;
                    year--;
                    lbl_year.Text = Convert.ToString(year);
                }
                else
                {
                    monthcounter--;
                }
                LBL_month.Text = month[monthcounter];
                check();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                /// <summary> check beginning of new year </summary>
                if (monthcounter == 11)
                {
                    monthcounter = 0;
                    year++;
                    lbl_year.Text = Convert.ToString(year);
                }
                else
                {
                    monthcounter++;
                }
                LBL_month.Text = month[monthcounter];
                check();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ///<summary> 
        /// Check 30 or 31 days and Check leap year
        /// </sumary>
        private void check()
        {
            switch (monthcounter)
            {
                case (int)MonthName.JANUAR:
                    tb_Der_Neunundzwanzigste.Enabled = true;
                    tb_Der_Neunundzwanzigste.Visible = true;
                    tb_Der_Dreißigste.Enabled = true;
                    tb_Der_Dreißigste.Visible = true;
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
                case (int)MonthName.FEBRUAR:

                    //Check leap year
                    if (DateTime.IsLeapYear(year))
                    {
                        tb_Der_Neunundzwanzigste.Enabled = true;
                        tb_Der_Neunundzwanzigste.Visible = true;
                        tb_Der_Dreißigste.Enabled = false;
                        tb_Der_Dreißigste.Visible = false;
                        tb_Der_Einunddreißigste.Enabled = false;
                        tb_Der_Einunddreißigste.Visible = false;
                    }
                    else
                    {
                        tb_Der_Neunundzwanzigste.Enabled = false;
                        tb_Der_Neunundzwanzigste.Visible = false;
                        tb_Der_Dreißigste.Enabled = false;
                        tb_Der_Dreißigste.Visible = false;
                        tb_Der_Einunddreißigste.Enabled = false;
                        tb_Der_Einunddreißigste.Visible = false;
                    }
                    break;
                case (int)MonthName.MAERZ:
                    tb_Der_Neunundzwanzigste.Enabled = true;
                    tb_Der_Neunundzwanzigste.Visible = true;
                    tb_Der_Dreißigste.Enabled = true;
                    tb_Der_Dreißigste.Visible = true;
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
                case (int)MonthName.APRIL:
                    tb_Der_Einunddreißigste.Enabled = false;
                    tb_Der_Einunddreißigste.Visible = false;
                    break;
                case (int)MonthName.MAI:
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
                case (int)MonthName.JUNI:
                    tb_Der_Einunddreißigste.Enabled = false;
                    tb_Der_Einunddreißigste.Visible = false;
                    break;
                case (int)MonthName.JULI:
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
                case (int)MonthName.AUGUST:
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
                case (int)MonthName.SEPTEMBER:
                    tb_Der_Einunddreißigste.Enabled = false;
                    tb_Der_Einunddreißigste.Visible = false;
                    break;
                case (int)MonthName.OKTOBER:
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
                case (int)MonthName.NOVEMBER:
                    tb_Der_Einunddreißigste.Enabled = false;
                    tb_Der_Einunddreißigste.Visible = false;
                    break;
                case (int)MonthName.DEZEMBER:
                    tb_Der_Einunddreißigste.Enabled = true;
                    tb_Der_Einunddreißigste.Visible = true;
                    break;
            }
        }

        private void BTN_manual_update_Click(object sender, EventArgs e)
        {
            // Logik für manuelles Update
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BT_Request_Click(object sender, EventArgs e)
        {

            KaEvent[] kaEvents = new KaEvent[3];

            for (int i = 0; i < 3; i++)
            {
                kaEvents[i] = new KaEvent();
                kaEvents[i].Titel = "Titel" + i;
                kaEvents[i].Beginn = DateTime.Now;
                kaEvents[i].Ende = DateTime.Now;
                kaEvents[i].Ort = "Ort" + i;
                kaEvents[i].owner = new User("Name " + i, "Password");
            }


            using (var form = new RequestList(kaEvents.ToList<KaEvent>(), client.currentUser))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {


                }
                else
                {
                    //MessageBox.Show("Ne ne ne So funktionierts nicht");
                }
            }
        }
    }
}
