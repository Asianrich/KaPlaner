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
        readonly IClientLogic clientLogic = ClientActivator.clientLogic;
        readonly DateTime date;

        private readonly List<int> indexes;
        public List<KaEvent> ListEvents;
        readonly bool isOnline;

        public Wdw_date_list(List<KaEvent> kaEvents, DateTime date, bool isOnline)
        {
            InitializeComponent();
            this.isOnline = isOnline;
            this.date = date;
            indexes = new List<int>();
            ListEvents = kaEvents;
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            string[] row = new string[5];
            LV_dates.Items.Clear();
            indexes.Clear();
            int i = 0;
            foreach (KaEvent ka in ListEvents)
            {

                if (ka.Beginn.Day == date.Day && ka.Beginn.Year == date.Year && ka.Beginn.Month == date.Month)
                {
                    row[0] = ka.Titel;
                    row[1] = ka.Ort;
                    row[2] = ka.Beginn.ToString();
                    row[3] = ka.Ende.ToString();
                    indexes.Add(i);
                    ListViewItem lvi = new ListViewItem(row);

                    LV_dates.Items.Add(lvi);
                }
                i++;
            }
        }


        public void LoadEvent(int index)
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


            using (var form = new Wdw_KaEvent(kaEvent, date, isOnline))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    kaEvent = form.returnValue;
                }
                else
                {
                    isNewElement = false;
                }
            }

            if (isNewElement)
            {
                ListEvents.Add(kaEvent);
                UpdateEvents();
            }
            else
            {
                ListEvents[index - 1] = kaEvent;
            }
        }


        private void BTN_oeffnen_Click(object sender, EventArgs e)
        {
            try
            {
                int index = LV_dates.FocusedItem.Index;
                LoadEvent(indexes[index]);
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
                LoadEvent(ListEvents.Count + 1);
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

                    KaEvent FocusEvent = ListEvents[LV_dates.FocusedItem.Index];
                    List<User> users = new List<User>();
                    char[] delim = { '#' };

                    foreach (string s in form.listStringreturn)
                    {
                        string[] parts = s.Split(delim);
                        User user = new User(parts[0]);
                        if (parts[1] != null)
                        {
                            if (Int32.TryParse(parts[1], out int result))
                            {
                                user.serverID = result;
                            }
                        }
                        users.Add(user);
                    }


                    FocusEvent.members = users;

                    clientLogic.SendInvites(FocusEvent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BTN_delete_Click(object sender, EventArgs e)
        {
            try
            {
                ListEvents.RemoveAt(indexes[LV_dates.FocusedItem.Index]);
                UpdateEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
