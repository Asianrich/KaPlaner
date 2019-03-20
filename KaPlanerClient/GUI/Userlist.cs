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

namespace KaPlaner.GUI
{
    public partial class Wdw_user_list : Form
    {
        public Wdw_user_list()
        {
            InitializeComponent();
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            try
            {
                //Save_usergroup(???);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Save_usergroup(KaEvent list, int index)
        {
            list.members = new string[index];
            for (int zeile = 0; zeile < LV_users.Items.Count; zeile++)
            {
                for (int spalte = 0; spalte < LV_users.Columns.Count; spalte++)
                {
                    list.members[zeile] += LV_users.Items[zeile].SubItems[spalte].ToString();
                }
            }
        }
    }
}
