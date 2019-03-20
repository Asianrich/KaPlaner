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

        KaEvent[] ListEvents;

        public RequestList(KaEvent[] kaEvents)
        {
            InitializeComponent();
            ListEvents = kaEvents;

            string[] row = new string[5];

            foreach (KaEvent ka in ListEvents)
            {
                row[0] = ka.Titel;
                row[1] = ka.Ort;
                row[2] = ka.Beginn.ToString();
                row[3] = ka.Ende.ToString();
                row[4] = ka.owner.name;
                ListViewItem lvi = new ListViewItem(row);
                LV_Dates.Items.Add(lvi);
            }


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
                load(index);
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
            if (index >= ListEvents.Length)
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
                    //MessageBox.Show("Ne ne ne So funktionierts nicht");
                }
            }

            if (isNewElement)
            {
                List<KaEvent> GenList = ListEvents.ToList<KaEvent>();
                GenList.Add(kaEvent);
                ListEvents = GenList.ToArray();
            }
            else
            {
                ListEvents[index] = kaEvent;
            }


            //Beispiel Funktion fuer das Oeffnen eines Date.cs/ oder Kaevents-Fenster
            // Wenn man das Oeffnet sollte mit bestehender Daten befuellt werden. Konstruktor wird zuerst mit Wdw_KaEvent(Clientlog clientlogic, KaEvent ereignis) siehe Date.cs
            //Form date = new Wdw_KaEvent();
        }
    }
}
