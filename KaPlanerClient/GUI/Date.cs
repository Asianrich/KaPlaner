using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KaPlaner.Database;
using KaPlaner.Objects;

namespace WindowsFormsApp1
{

    public partial class Wdw_Ereignis : Form
    {

        public KaEvent returnValue;

        public Wdw_Ereignis()
        {
            InitializeComponent();
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cb_ganztägige_verantstaltung_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_ganztägige_verantstaltung.Checked == true)
            {
                LBL_hour_beginn.Enabled = false;
                LBL_minute_beginn.Enabled = false;

                TB_hour_beginn.Enabled = false;
                TB_minute_beginn.Enabled = false;

                TB_minute_beginn.Text = "00";
                TB_hour_beginn.Text = "08";

                LBL_hour_end.Enabled = false;
                LBL_minute_end.Enabled = false;

                TB_minute_end.Enabled = false;
                TB_hour_end.Enabled = false;

                TB_minute_end.Text = "00";
                TB_hour_end.Text = "20";
            }
            else if(cb_ganztägige_verantstaltung.Checked == false)
            {
                LBL_hour_beginn.Enabled = true;
                LBL_minute_beginn.Enabled = true;

                TB_hour_beginn.Enabled = true;
                TB_minute_beginn.Enabled = true;

                TB_minute_beginn.Text = "";
                TB_hour_beginn.Text = "";

                LBL_hour_end.Enabled = true;
                LBL_minute_end.Enabled = true;

                TB_minute_end.Enabled = true;
                TB_hour_end.Enabled = true;

                TB_minute_end.Text = "";
                TB_hour_end.Text = "";
            }
        }

        private void btn_speichern_Click(object sender, EventArgs e)
        {
            this.returnValue = new KaEvent();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CB_always_repeat_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_always_repeat.Checked == true)
            {
                TB_times_repeat.Enabled = false;
                LBL_times_repeat.Enabled = false;
                CB_always_repeat_until.Enabled = false;
                TB_repeat_until.Enabled = false;

                TB_times_repeat.Text = "0";
            }
            else if (CB_always_repeat.Checked == false)
            {
                TB_times_repeat.Enabled = true;
                LBL_times_repeat.Enabled = true;
                CB_always_repeat_until.Enabled = true;
                TB_repeat_until.Enabled = true;

                TB_times_repeat.Text = "1";
            }
        }

        private void CB_always_repeat_until_CheckedChanged(object sender, EventArgs e)
        {
            if(CB_always_repeat_until.Checked == true)
            {
                TB_repeat_until.Enabled = true;
            }
            else if (CB_always_repeat_until.Checked == false)
            {
                TB_repeat_until.Enabled = false;
            }
        } 
    }
}
