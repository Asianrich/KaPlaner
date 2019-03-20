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
        public List<string> listStringreturn = new List<string>();

        ListViewItem lvi = new ListViewItem();
        int index = 0;


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

        private void BTN_add_user_Click(object sender, EventArgs e)
        {
            if (TB_add_user.Text != String.Empty)
            {
                LV_users.Items.Add(TB_add_user.Text);
                listStringreturn.Add(TB_add_user.Text);
                TB_add_user.Text = String.Empty;

            }
            
        }

        private void BTN_delete_user_Click(object sender, EventArgs e)
        {
            LV_users.Items.Remove(LV_users.FocusedItem);
            listStringreturn.RemoveAt(LV_users.FocusedItem.Index);
        }
    }
}
