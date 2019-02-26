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
    public partial class wdw_login : Form
    {
        private Label lbl_login;
        private RichTextBox tb_log_benutzername;
        private RichTextBox tb_log_passwort;
        private Label lbl_log_benutzername;
        private Button btn_log_senden;
        private Button btn_log_schließen;
        private Button wdw_registrierung;
        private Label lbl_log_passwort;

        public wdw_login()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lbl_login = new System.Windows.Forms.Label();
            this.tb_log_benutzername = new System.Windows.Forms.RichTextBox();
            this.tb_log_passwort = new System.Windows.Forms.RichTextBox();
            this.lbl_log_benutzername = new System.Windows.Forms.Label();
            this.lbl_log_passwort = new System.Windows.Forms.Label();
            this.btn_log_senden = new System.Windows.Forms.Button();
            this.btn_log_schließen = new System.Windows.Forms.Button();
            this.wdw_registrierung = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_login
            // 
            this.lbl_login.AutoSize = true;
            this.lbl_login.Font = new System.Drawing.Font("Arial", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login.Location = new System.Drawing.Point(183, 64);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(128, 49);
            this.lbl_login.TabIndex = 0;
            this.lbl_login.Text = "Login";
            // 
            // tb_log_benutzername
            // 
            this.tb_log_benutzername.Font = new System.Drawing.Font("Arial", 14.25F);
            this.tb_log_benutzername.Location = new System.Drawing.Point(192, 183);
            this.tb_log_benutzername.Name = "tb_log_benutzername";
            this.tb_log_benutzername.Size = new System.Drawing.Size(230, 40);
            this.tb_log_benutzername.TabIndex = 1;
            this.tb_log_benutzername.Text = "";
            this.tb_log_benutzername.TextChanged += new System.EventHandler(this.tb_log_benutzername_TextChanged);
            // 
            // tb_log_passwort
            // 
            this.tb_log_passwort.Font = new System.Drawing.Font("Arial", 14.25F);
            this.tb_log_passwort.Location = new System.Drawing.Point(192, 229);
            this.tb_log_passwort.Name = "tb_log_passwort";
            this.tb_log_passwort.Size = new System.Drawing.Size(230, 40);
            this.tb_log_passwort.TabIndex = 2;
            this.tb_log_passwort.Text = "";
            // 
            // lbl_log_benutzername
            // 
            this.lbl_log_benutzername.AutoSize = true;
            this.lbl_log_benutzername.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_benutzername.Location = new System.Drawing.Point(36, 200);
            this.lbl_log_benutzername.Name = "lbl_log_benutzername";
            this.lbl_log_benutzername.Size = new System.Drawing.Size(135, 23);
            this.lbl_log_benutzername.TabIndex = 3;
            this.lbl_log_benutzername.Text = "Benutzername";
            // 
            // lbl_log_passwort
            // 
            this.lbl_log_passwort.AutoSize = true;
            this.lbl_log_passwort.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_passwort.Location = new System.Drawing.Point(37, 246);
            this.lbl_log_passwort.Name = "lbl_log_passwort";
            this.lbl_log_passwort.Size = new System.Drawing.Size(93, 23);
            this.lbl_log_passwort.TabIndex = 4;
            this.lbl_log_passwort.Text = "Passwort";
            // 
            // btn_log_senden
            // 
            this.btn_log_senden.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_log_senden.Location = new System.Drawing.Point(40, 321);
            this.btn_log_senden.Name = "btn_log_senden";
            this.btn_log_senden.Size = new System.Drawing.Size(90, 30);
            this.btn_log_senden.TabIndex = 5;
            this.btn_log_senden.Text = "Senden";
            this.btn_log_senden.UseVisualStyleBackColor = true;
            this.btn_log_senden.Click += new System.EventHandler(this.btn_log_send_Click);
            // 
            // btn_log_schließen
            // 
            this.btn_log_schließen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_log_schließen.Location = new System.Drawing.Point(332, 321);
            this.btn_log_schließen.Name = "btn_log_schließen";
            this.btn_log_schließen.Size = new System.Drawing.Size(90, 30);
            this.btn_log_schließen.TabIndex = 6;
            this.btn_log_schließen.Text = "Schließen";
            this.btn_log_schließen.UseVisualStyleBackColor = true;
            this.btn_log_schließen.Click += new System.EventHandler(this.btn_log_quit_Click);
            // 
            // wdw_registrierung
            // 
            this.wdw_registrierung.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wdw_registrierung.Location = new System.Drawing.Point(183, 321);
            this.wdw_registrierung.Name = "wdw_registrierung";
            this.wdw_registrierung.Size = new System.Drawing.Size(113, 30);
            this.wdw_registrierung.TabIndex = 7;
            this.wdw_registrierung.Text = "Registrierung";
            this.wdw_registrierung.UseVisualStyleBackColor = true;
            this.wdw_registrierung.Click += new System.EventHandler(this.wdw_registry_Click);
            // 
            // wdw_login
            // 
            this.ClientSize = new System.Drawing.Size(459, 386);
            this.Controls.Add(this.wdw_registrierung);
            this.Controls.Add(this.btn_log_schließen);
            this.Controls.Add(this.btn_log_senden);
            this.Controls.Add(this.lbl_log_passwort);
            this.Controls.Add(this.lbl_log_benutzername);
            this.Controls.Add(this.tb_log_passwort);
            this.Controls.Add(this.tb_log_benutzername);
            this.Controls.Add(this.lbl_login);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(475, 425);
            this.MinimumSize = new System.Drawing.Size(475, 425);
            this.Name = "wdw_login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btn_log_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_log_send_Click(object sender, EventArgs e)
        {
            KaPlaner.Database.Database reg = new Database();
            if(reg.login(tb_log_benutzername.Text, tb_log_passwort.Text))
            {
                Form open_calendar = new wdw_calendar();
                open_calendar.Show();
            }
            else
            {
                tb_log_benutzername.Text = "";
                tb_log_passwort.Text = "";
            }
        }

        private void wdw_registry_Click(object sender, EventArgs e)
        {
            Form open_registry = new wdw_registrierung();
            open_registry.Show();
        }

        private void tb_log_benutzername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
