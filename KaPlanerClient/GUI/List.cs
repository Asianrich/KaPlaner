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

namespace KaPlaner.GUI
{
    public partial class Wdw_List : Form
    {

        private KaEvent[] ListEvents;
        public Wdw_List(KaEvent[] kaEvents)
        {
            InitializeComponent();
            string[] row = new string[5];


            /*
             * Joshua ListenUpdates wir brauchen die KaEvents des jeweiligen Tages
             * Aendere den Konstrukter so, das es fuer dich und dem foreach passt.
             * Am Besten den Foreach
             * Fuelle die ListEvents mit Werten. Damit man spaeter beim oeffnen des Date.cs mit Daten befuellen.
             */
            ListEvents = kaEvents;





            foreach (KaEvent ka in ListEvents)
            {
                row[0] = ka.Titel;
                row[1] = ka.Ort;
                row[2] = ka.Beginn.ToString();
                row[3] = ka.Ende.ToString();

                ListViewItem lvi = new ListViewItem(row);

                LV_Dates.Items.Add(lvi);               
            }
        }

        private void LV_Dates_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Read(ka);
        }

        public void Read(KaEvent ka)
        {
            
        }

        public void load(ClientLogic clientLogic, KaEvent kaEvent)
        {

            using (var form = new Wdw_KaEvent(clientLogic, kaEvent))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    kaEvent = form.returnValue;
                    MessageBox.Show(kaEvent.Titel);
                }
                else
                {
                    //MessageBox.Show("Ne ne ne So funktionierts nicht");
                }
            }
            //Beispiel Funktion fuer das Oeffnen eines Date.cs/ oder Kaevents-Fenster
            // Wenn man das Oeffnet sollte mit bestehender Daten befuellt werden. Konstruktor wird zuerst mit Wdw_KaEvent(Clientlog clientlogic, KaEvent ereignis) siehe Date.cs
            //Form date = new Wdw_KaEvent();
        }

        private void BTN_oeffnen_Click(object sender, EventArgs e)
        {
            load(null, ListEvents[LV_Dates.FocusedItem.Index]);
        }

        private void BTN_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
