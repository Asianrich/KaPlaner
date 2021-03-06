﻿using KaObjects;
using KaPlaner.Logic;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Wdw_login : Form
    {
        readonly IClientLogic clientLogic = ClientActivator.clientLogic;

        private Label lbl_login;
        private Label lbl_log_benutzername;
        private Button btn_log_senden;
        private Button btn_log_schließen;
        private Button wdw_registrierung;
        private Button BTN_offline;
        private TextBox tb_log_benutzername;
        private TextBox tb_log_passwort;
        private TextBox tb_log_ID;
        private Label lbl_log_ID;
        private Label lbl_log_passwort;

        public Wdw_login()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lbl_login = new System.Windows.Forms.Label();
            this.lbl_log_benutzername = new System.Windows.Forms.Label();
            this.lbl_log_passwort = new System.Windows.Forms.Label();
            this.btn_log_senden = new System.Windows.Forms.Button();
            this.btn_log_schließen = new System.Windows.Forms.Button();
            this.wdw_registrierung = new System.Windows.Forms.Button();
            this.BTN_offline = new System.Windows.Forms.Button();
            this.tb_log_benutzername = new System.Windows.Forms.TextBox();
            this.tb_log_passwort = new System.Windows.Forms.TextBox();
            this.tb_log_ID = new System.Windows.Forms.TextBox();
            this.lbl_log_ID = new System.Windows.Forms.Label();
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
            // lbl_log_benutzername
            // 
            this.lbl_log_benutzername.AutoSize = true;
            this.lbl_log_benutzername.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_benutzername.Location = new System.Drawing.Point(36, 196);
            this.lbl_log_benutzername.Name = "lbl_log_benutzername";
            this.lbl_log_benutzername.Size = new System.Drawing.Size(135, 23);
            this.lbl_log_benutzername.TabIndex = 3;
            this.lbl_log_benutzername.Text = "Benutzername";
            // 
            // lbl_log_passwort
            // 
            this.lbl_log_passwort.AutoSize = true;
            this.lbl_log_passwort.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_passwort.Location = new System.Drawing.Point(36, 228);
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
            this.btn_log_senden.TabIndex = 3;
            this.btn_log_senden.Text = "Senden";
            this.btn_log_senden.UseVisualStyleBackColor = true;
            this.btn_log_senden.Click += new System.EventHandler(this.Btn_log_send_Click);
            // 
            // btn_log_schließen
            // 
            this.btn_log_schließen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_log_schließen.Location = new System.Drawing.Point(332, 321);
            this.btn_log_schließen.Name = "btn_log_schließen";
            this.btn_log_schließen.Size = new System.Drawing.Size(90, 30);
            this.btn_log_schließen.TabIndex = 5;
            this.btn_log_schließen.Text = "Schließen";
            this.btn_log_schließen.UseVisualStyleBackColor = true;
            this.btn_log_schließen.Click += new System.EventHandler(this.Btn_log_quit_Click);
            // 
            // wdw_registrierung
            // 
            this.wdw_registrierung.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wdw_registrierung.Location = new System.Drawing.Point(183, 321);
            this.wdw_registrierung.Name = "wdw_registrierung";
            this.wdw_registrierung.Size = new System.Drawing.Size(113, 30);
            this.wdw_registrierung.TabIndex = 4;
            this.wdw_registrierung.Text = "Registrierung";
            this.wdw_registrierung.UseVisualStyleBackColor = true;
            this.wdw_registrierung.Click += new System.EventHandler(this.Wdw_registry_Click);
            // 
            // BTN_offline
            // 
            this.BTN_offline.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_offline.Location = new System.Drawing.Point(40, 372);
            this.BTN_offline.Name = "BTN_offline";
            this.BTN_offline.Size = new System.Drawing.Size(382, 30);
            this.BTN_offline.TabIndex = 6;
            this.BTN_offline.Text = "OFFLINE";
            this.BTN_offline.UseVisualStyleBackColor = true;
            this.BTN_offline.Click += new System.EventHandler(this.BTN_offline_Click);
            // 
            // tb_log_benutzername
            // 
            this.tb_log_benutzername.Location = new System.Drawing.Point(192, 193);
            this.tb_log_benutzername.Name = "tb_log_benutzername";
            this.tb_log_benutzername.Size = new System.Drawing.Size(230, 26);
            this.tb_log_benutzername.TabIndex = 0;
            // 
            // tb_log_passwort
            // 
            this.tb_log_passwort.Location = new System.Drawing.Point(192, 225);
            this.tb_log_passwort.Name = "tb_log_passwort";
            this.tb_log_passwort.Size = new System.Drawing.Size(230, 26);
            this.tb_log_passwort.TabIndex = 1;
            // 
            // tb_log_ID
            // 
            this.tb_log_ID.Location = new System.Drawing.Point(192, 257);
            this.tb_log_ID.Name = "tb_log_ID";
            this.tb_log_ID.Size = new System.Drawing.Size(230, 26);
            this.tb_log_ID.TabIndex = 2;
            this.tb_log_ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_log_ID_KeyPress);
            // 
            // lbl_log_ID
            // 
            this.lbl_log_ID.AutoSize = true;
            this.lbl_log_ID.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_ID.Location = new System.Drawing.Point(36, 260);
            this.lbl_log_ID.Name = "lbl_log_ID";
            this.lbl_log_ID.Size = new System.Drawing.Size(88, 23);
            this.lbl_log_ID.TabIndex = 12;
            this.lbl_log_ID.Text = "ServerID";
            // 
            // Wdw_login
            // 
            this.ClientSize = new System.Drawing.Size(459, 421);
            this.Controls.Add(this.lbl_log_ID);
            this.Controls.Add(this.tb_log_ID);
            this.Controls.Add(this.tb_log_passwort);
            this.Controls.Add(this.tb_log_benutzername);
            this.Controls.Add(this.BTN_offline);
            this.Controls.Add(this.wdw_registrierung);
            this.Controls.Add(this.btn_log_schließen);
            this.Controls.Add(this.btn_log_senden);
            this.Controls.Add(this.lbl_log_passwort);
            this.Controls.Add(this.lbl_log_benutzername);
            this.Controls.Add(this.lbl_login);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(475, 460);
            this.MinimumSize = new System.Drawing.Size(475, 460);
            this.Name = "Wdw_login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Btn_log_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_log_send_Click(object sender, EventArgs e)
        {
            try
            {

                bool containsSearchResult = tb_log_benutzername.ToString().Contains("#");

                if (!containsSearchResult)
                {
                    if (Int32.TryParse(tb_log_ID.Text, out int result))
                    {
                        switch (clientLogic.LoginRemote(new User(tb_log_benutzername.Text, tb_log_passwort.Text, result)))
                        {
                            case Request.Success:
                                Form open_calendar = new wdw_calendar(true);
                                open_calendar.Show();
                                tb_log_benutzername.Text = "";
                                tb_log_passwort.Text = "";
                                break;

                            default:
                                tb_log_benutzername.Text = "";
                                tb_log_passwort.Text = "";
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim ServerID, bitte überprüfen");
                    }
                }
                else
                {
                    MessageBox.Show("Der Benutzername darf keine # enthalten.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Wdw_registry_Click(object sender, EventArgs e)
        {
            try
            {
                Form open_registry = new Wdw_registrierung(clientLogic);
                open_registry.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BTN_offline_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientLogic.LoginLocal(new User(tb_log_benutzername.Text, tb_log_passwort.Text)))
                {
                    Form open_calendar = new wdw_calendar(false);
                    open_calendar.Show();
                    tb_log_benutzername.Text = "";
                    tb_log_passwort.Text = "";
                }
                else
                {
                    tb_log_benutzername.Text = "";
                    tb_log_passwort.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tb_log_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }
    }
}

