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
using KaPlaner.GUI;
using WindowsFormsApp1;

namespace KaPlaner.GUI
{
    public partial class RequestList : Form
    {
        private List<int> indexes;
        public List<KaEvent> ListEvents;
        private User user;
        public RequestList(List<KaEvent> kaEvents, User user)
        {
            InitializeComponent();
            ListEvents = kaEvents;
            this.user = user;
            string[] row = new string[5];
            indexes = new List<int>();
            update();
        }

        public void update()
        {
            string[] row = new string[5];
            LV_Dates.Items.Clear();
            indexes.Clear();
            int i = 0;

            //ListEvents.ForEach(x =>
            //{
            //    if (x.members != null)
            //    {
            //        //Eingeladene Termine angezeigt bekommen.
            //        if (invite(x.members.ToArray(), user.name))
            //        {
            //            row[0] = x.Titel;
            //            row[1] = x.Ort;
            //            row[2] = x.Beginn.ToString();
            //            row[3] = x.Ende.ToString();
            //            indexes.Add(i);
            //            ListViewItem lvi = new ListViewItem(row);

            //            LV_Dates.Items.Add(lvi);
            //        }
            //    }
            //    i++;
            //});
            
            foreach (KaEvent ka in ListEvents)
            {
                List<string> users = new List<string>();
                foreach(User u in ka.members)
                {
                    users.Add(u.name);
                }
                if (ka.members != null)
                {
                    //Eingeladene Termine angezeigt bekommen.
                    if (invite(users.ToArray(), user.name))  
                    {
                        row[0] = ka.Titel;
                        row[1] = ka.Ort;
                        row[2] = ka.Beginn.ToString();
                        row[3] = ka.Ende.ToString();
                        indexes.Add(i);
                        ListViewItem lvi = new ListViewItem(row);

                        LV_Dates.Items.Add(lvi);
                    }
                }
                i++;
            }
        }

        private bool invite(string[] list, string username)
        {
            foreach(string a in list)
            {
                if(String.Equals(a,username))
                {
                    return true;
                }
            }
            return false;
        }

        private void BT_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Bt_Open_Click(object sender, EventArgs e)
        {
            try
            {
                int index = LV_Dates.FocusedItem.Index;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void load(int index)
        {
            bool isNewElement = false;
            KaEvent kaEvent;
            if (index >= ListEvents.Count)
            {
                isNewElement = true;
                kaEvent = new KaEvent();
            }
            else
            {
                kaEvent = ListEvents[index];
            }

            using (var form = new Wdw_KaEvent(kaEvent))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    kaEvent = form.returnValue;

                }
                else
                {
                    MessageBox.Show("Ne ne ne So funktionierts nicht");
                }
            }

            if (isNewElement)
            {
                ListEvents[index] = kaEvent;
            }
            else
            {
                ListEvents[index] = kaEvent;
            }

            /// Beispiel Funktion fuer das Oeffnen eines Date.cs/ oder Kaevents-Fenster
            /// Wenn man das Oeffnet sollte mit bestehenden Daten befuellt werden. 
            /// Konstruktor wird zuerst mit Wdw_KaEvent(Clientlog clientlogic, KaEvent ereignis) siehe Date.cs
        }

        private void Bt_Decline_Click(object sender, EventArgs e)
        {

        }

        private void BT_Accept_Click(object sender, EventArgs e)
        {

        }
    }
}
