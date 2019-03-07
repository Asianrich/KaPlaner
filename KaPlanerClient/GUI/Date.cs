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

        private void Lbl_Titel_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
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

                TB_minute_end.Enabled = false;
                TB_hour_end.Enabled = false;

            }
            else if(cb_ganztägige_verantstaltung.Checked == false)
            {
                LBL_hour_beginn.Enabled = true;
                LBL_minute_beginn.Enabled = true;

                TB_hour_beginn.Enabled = true;
                TB_minute_beginn.Enabled = true;

                TB_minute_end.Enabled = true;
                TB_hour_end.Enabled = true;
            }
        }

        

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void tab_allgemein_Click(object sender, EventArgs e)
        {

        }

        private void btn_speichern_Click(object sender, EventArgs e)
        {
            this.returnValue = new KaEvent(TB_Title.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
