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
using WindowsFormsApp1;
using KaPlaner.Logic;
using System.Data.SqlClient;

namespace KaPlaner.GUI
{
    public partial class Wdw_date_list : Form
    {
        ClientLogic clientLogic = ClientActivator.clientLogic;
        DateTime date;

        private List<int> indexes;
        public List<KaEvent> ListEvents;

        public Wdw_date_list(List<KaEvent>kaEvents, DateTime date)
        {
            InitializeComponent();
            string[] row = new string[5];
            this.date = date;

            /*
             * Joshua ListenUpdates wir brauchen die KaEvents des jeweiligen Tages
             * Aendere den Konstrukter so, das es fuer dich und dem foreach passt.
             * Am Besten den Foreach
             * Fuelle die ListEvents mit Werten. Damit man spaeter beim oeffnen des Date.cs mit Daten befuellen.
             */
            ListEvents = kaEvents;




            foreach (KaEvent ka in ListEvents)
            {

                if (ka.Beginn.Day == date.Day && ka.Beginn.Year == date.Year && ka.Beginn.Month == date.Month)
                {
                    row[0] = ka.Titel;
                    row[1] = ka.Ort;
                    row[2] = ka.Beginn.ToString();
                    row[3] = ka.Ende.ToString();
                    indexes.Add(ka.TerminID);
                    ListViewItem lvi = new ListViewItem(row);

                    LV_dates.Items.Add(lvi);
                }
            }
        }

        public void load(int index)
        {
            bool isNewElement = false;
            KaEvent kaEvent;
            if (index >= ListEvents.Count)
            {
                isNewElement = true;
                kaEvent = null;
            }
            else
            {
                kaEvent = ListEvents[index];
            }


            using (var form = new Wdw_KaEvent(kaEvent, date))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    kaEvent = form.returnValue;

                }
                else
                {
                    //MessageBox.Show("Ne ne ne So funktionierts nicht");
                }
            }

            if (isNewElement)
            {
                
                ListEvents.Add(kaEvent);
            }
            else
            {
                ListEvents[index] = kaEvent;
            }


            //Beispiel Funktion fuer das Oeffnen eines Date.cs/ oder Kaevents-Fenster
            // Wenn man das Oeffnet sollte mit bestehender Daten befuellt werden. Konstruktor wird zuerst mit Wdw_KaEvent(Clientlog clientlogic, KaEvent ereignis) siehe Date.cs
            //Form date = new Wdw_KaEvent();
        }

        private void BTN_oeffnen_Click(object sender, EventArgs e)
        {
            try
            {
                int index = LV_dates.FocusedItem.Index;
                load(indexes[index]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BTN_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BTN_new_Click(object sender, EventArgs e)
        {
            try
            {
                load(ListEvents.Count + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BTN_invite_Click(object sender, EventArgs e)
        {
            try
            {
                

                using (var form = new Wdw_user_list())
                {
                    var results = form.ShowDialog();

                    //Betroffenes Termin
                    KaEvent FocusEvent = ListEvents[LV_dates.FocusedItem.Index];
                    FocusEvent.members = form.listStringreturn;
                    //Joshua hier bei updatest du den Event. FocusEvent ist das neue Event, welches abgeändert wurde.


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BTN_delete_Click(object sender, EventArgs e)
        {
            // Logik Lösch-Button hier einfügen
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
