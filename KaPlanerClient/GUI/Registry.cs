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

namespace WindowsFormsApp1
{
    public partial class wdw_registrierung : Form
    {
        private RichTextBox rTB_passwort_bestaetigen;
        private Label lbl_registrierung;
        private RichTextBox rTB_passwort;
        private Label lbl_reg_benutzername;
        private Label lbl_reg_passwort;
        private Label lbl_reg_passwort_bestaetigen;
        private Button btn_reg_senden;
        private Button btn_reg_schließen;
        private RichTextBox rTB_benutzername;

        public wdw_registrierung()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.rTB_passwort_bestaetigen = new System.Windows.Forms.RichTextBox();
            this.lbl_registrierung = new System.Windows.Forms.Label();
            this.rTB_passwort = new System.Windows.Forms.RichTextBox();
            this.rTB_benutzername = new System.Windows.Forms.RichTextBox();
            this.lbl_reg_benutzername = new System.Windows.Forms.Label();
            this.lbl_reg_passwort = new System.Windows.Forms.Label();
            this.lbl_reg_passwort_bestaetigen = new System.Windows.Forms.Label();
            this.btn_reg_senden = new System.Windows.Forms.Button();
            this.btn_reg_schließen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rTB_passwort_bestaetigen
            // 
            this.rTB_passwort_bestaetigen.Font = new System.Drawing.Font("Arial", 14.25F);
            this.rTB_passwort_bestaetigen.Location = new System.Drawing.Point(256, 273);
            this.rTB_passwort_bestaetigen.Name = "rTB_passwort_bestaetigen";
            this.rTB_passwort_bestaetigen.Size = new System.Drawing.Size(230, 40);
            this.rTB_passwort_bestaetigen.TabIndex = 0;
            this.rTB_passwort_bestaetigen.Text = "";
            // 
            // lbl_registrierung
            // 
            this.lbl_registrierung.AutoSize = true;
            this.lbl_registrierung.Font = new System.Drawing.Font("Arial", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_registrierung.Location = new System.Drawing.Point(134, 46);
            this.lbl_registrierung.Name = "lbl_registrierung";
            this.lbl_registrierung.Size = new System.Drawing.Size(279, 49);
            this.lbl_registrierung.TabIndex = 1;
            this.lbl_registrierung.Text = "Registrierung";
            // 
            // rTB_passwort
            // 
            this.rTB_passwort.Font = new System.Drawing.Font("Arial", 14.25F);
            this.rTB_passwort.Location = new System.Drawing.Point(256, 227);
            this.rTB_passwort.Name = "rTB_passwort";
            this.rTB_passwort.Size = new System.Drawing.Size(230, 40);
            this.rTB_passwort.TabIndex = 2;
            this.rTB_passwort.Text = "";
            // 
            // rTB_benutzername
            // 
            this.rTB_benutzername.Font = new System.Drawing.Font("Arial", 14.25F);
            this.rTB_benutzername.Location = new System.Drawing.Point(256, 181);
            this.rTB_benutzername.Name = "rTB_benutzername";
            this.rTB_benutzername.Size = new System.Drawing.Size(230, 40);
            this.rTB_benutzername.TabIndex = 3;
            this.rTB_benutzername.Text = "";
            this.rTB_benutzername.TextChanged += new System.EventHandler(this.richTextBox3_TextChanged);
            // 
            // lbl_reg_benutzername
            // 
            this.lbl_reg_benutzername.AutoSize = true;
            this.lbl_reg_benutzername.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_reg_benutzername.Location = new System.Drawing.Point(36, 198);
            this.lbl_reg_benutzername.Name = "lbl_reg_benutzername";
            this.lbl_reg_benutzername.Size = new System.Drawing.Size(135, 23);
            this.lbl_reg_benutzername.TabIndex = 4;
            this.lbl_reg_benutzername.Text = "Benutzername";
            // 
            // lbl_reg_passwort
            // 
            this.lbl_reg_passwort.AutoSize = true;
            this.lbl_reg_passwort.Font = new System.Drawing.Font("Arial", 15F);
            this.lbl_reg_passwort.Location = new System.Drawing.Point(36, 244);
            this.lbl_reg_passwort.Name = "lbl_reg_passwort";
            this.lbl_reg_passwort.Size = new System.Drawing.Size(93, 23);
            this.lbl_reg_passwort.TabIndex = 5;
            this.lbl_reg_passwort.Text = "Passwort";
            // 
            // lbl_reg_passwort_bestaetigen
            // 
            this.lbl_reg_passwort_bestaetigen.AutoSize = true;
            this.lbl_reg_passwort_bestaetigen.Font = new System.Drawing.Font("Arial", 15F);
            this.lbl_reg_passwort_bestaetigen.Location = new System.Drawing.Point(36, 290);
            this.lbl_reg_passwort_bestaetigen.Name = "lbl_reg_passwort_bestaetigen";
            this.lbl_reg_passwort_bestaetigen.Size = new System.Drawing.Size(201, 23);
            this.lbl_reg_passwort_bestaetigen.TabIndex = 6;
            this.lbl_reg_passwort_bestaetigen.Text = "Passwort bestaetigen";
            // 
            // btn_reg_senden
            // 
            this.btn_reg_senden.Font = new System.Drawing.Font("Arial", 12F);
            this.btn_reg_senden.Location = new System.Drawing.Point(40, 357);
            this.btn_reg_senden.Name = "btn_reg_senden";
            this.btn_reg_senden.Size = new System.Drawing.Size(90, 30);
            this.btn_reg_senden.TabIndex = 7;
            this.btn_reg_senden.Text = "Senden";
            this.btn_reg_senden.UseVisualStyleBackColor = true;
            this.btn_reg_senden.Click += new System.EventHandler(this.btn_reg_send_Click);
            // 
            // btn_reg_schließen
            // 
            this.btn_reg_schließen.Font = new System.Drawing.Font("Arial", 12F);
            this.btn_reg_schließen.Location = new System.Drawing.Point(396, 357);
            this.btn_reg_schließen.Name = "btn_reg_schließen";
            this.btn_reg_schließen.Size = new System.Drawing.Size(90, 30);
            this.btn_reg_schließen.TabIndex = 8;
            this.btn_reg_schließen.Text = "Schließen";
            this.btn_reg_schließen.UseVisualStyleBackColor = true;
            this.btn_reg_schließen.Click += new System.EventHandler(this.btn_reg_quit_Click);
            // 
            // wdw_registrierung
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(534, 436);
            this.Controls.Add(this.btn_reg_schließen);
            this.Controls.Add(this.btn_reg_senden);
            this.Controls.Add(this.lbl_reg_passwort_bestaetigen);
            this.Controls.Add(this.lbl_reg_passwort);
            this.Controls.Add(this.lbl_reg_benutzername);
            this.Controls.Add(this.rTB_benutzername);
            this.Controls.Add(this.rTB_passwort);
            this.Controls.Add(this.lbl_registrierung);
            this.Controls.Add(this.rTB_passwort_bestaetigen);
            this.Name = "wdw_registrierung";
            this.Text = "Registrierung";
            this.Load += new System.EventHandler(this.Registry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Registry_Load(object sender, EventArgs e)
        {

        }

        private void btn_reg_send_Click(object sender, EventArgs e)
        {
            KaPlaner.Database.Database reg = new Database();
            if (reg.registerUser(rTB_benutzername.Text, rTB_passwort.Text, rTB_passwort_bestaetigen.Text))
            {
                Form open_calendar = new wdw_calendar();
                open_calendar.Show();
                Close();
            }
            else
            {
                rTB_benutzername.Text = "";
                rTB_passwort.Text = "";
                rTB_passwort_bestaetigen.Text = "";
            }   
        }

        private void btn_reg_quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
