using KaObjects;
using KaPlaner.Logic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace KaPlaner.GUI
{
    public partial class Wdw_date_list : Form
    {
        readonly IClientLogic clientLogic = ClientActivator.clientLogic;
        readonly DateTime date;

        //private readonly List<int> indexes;
        public List<KaEvent> ListEvents = new List<KaEvent>();
        public List<KaEvent> dates;
        readonly bool isOnline;

        public Wdw_date_list(List<KaEvent> kaEvents, DateTime date, bool isOnline)
        {
            InitializeComponent();
            this.isOnline = isOnline;
            this.date = date;
            dates = kaEvents;
            foreach (KaEvent ka in dates)
            {
                if (ka.Beginn.ToString("dd/MM/yy") == date.ToString("dd/MM/yy"))
                {
                    ListEvents.Add(ka);
                }
            }
            //indexes = new List<int>();
            //ListEvents = kaEvents;
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            string[] row = new string[5];
            LV_dates.Items.Clear();

            foreach (KaEvent ka in ListEvents)
            {

                //if (ka.Beginn.Day == date.Day && ka.Beginn.Year == date.Year && ka.Beginn.Month == date.Month)
                //{
                row[0] = ka.Titel;
                row[1] = ka.Ort;
                row[2] = ka.Beginn.ToString();
                row[3] = ka.Ende.ToString();
                //indexes.Add(i);
                ListViewItem lvi = new ListViewItem(row);

                LV_dates.Items.Add(lvi);
                //}
                //i++;
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
                dates.Add(kaEvent);
                UpdateEvents();
            }
            else
            {
                ListEvents[index] = kaEvent;
            }
        }


        private void BTN_oeffnen_Click(object sender, EventArgs e)
        {
            try
            {
                int index = LV_dates.FocusedItem.Index;
                LoadEvent(index);
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
                clientLogic.DeleteRemote(ListEvents[LV_dates.FocusedItem.Index]);
                ListEvents.RemoveAt(LV_dates.FocusedItem.Index);
                dates.RemoveAt(LV_dates.FocusedItem.Index);
                UpdateEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
