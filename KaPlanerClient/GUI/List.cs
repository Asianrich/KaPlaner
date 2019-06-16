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
        IClientLogic clientLogic = ClientActivator.clientLogic;
        DateTime date;

        private List<int> indexes;
        public List<KaEvent> ListEvents;
        bool isOnline;

        public Wdw_date_list(List<KaEvent>kaEvents, DateTime date, bool isOnline)
        {
            InitializeComponent();
            this.isOnline = isOnline;
            this.date = date;
            indexes = new List<int>();
            ListEvents = kaEvents;
            update();
        }

        public void update()
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


            using (var form = new Wdw_KaEvent(kaEvent, date, isOnline))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                { 
                        kaEvent = form.returnValue;
                }
                else
                {
                        MessageBox.Show("Ne ne ne So funktionierts nicht");
                        isNewElement = false;
                    }
                }

                if (isNewElement)
                {
                    ListEvents.Add(kaEvent);
                    update();
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

                    KaEvent FocusEvent = ListEvents[LV_dates.FocusedItem.Index];
                    FocusEvent.members = form.listStringreturn;
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
                update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
