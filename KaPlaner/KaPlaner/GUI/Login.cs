using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class wdw_login : Form
    {
        private Label lbl_Login;
        private RichTextBox tb_log_username;
        private RichTextBox tb_log_password;
        private Label lbl_log_username;
        private Button btn_log_send;
        private Button btn_log_quit;
        private Button wdw_registry;
        private Label lbl_log_password;

        public wdw_login()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lbl_Login = new System.Windows.Forms.Label();
            this.tb_log_username = new System.Windows.Forms.RichTextBox();
            this.tb_log_password = new System.Windows.Forms.RichTextBox();
            this.lbl_log_username = new System.Windows.Forms.Label();
            this.lbl_log_password = new System.Windows.Forms.Label();
            this.btn_log_send = new System.Windows.Forms.Button();
            this.btn_log_quit = new System.Windows.Forms.Button();
            this.wdw_registry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.Font = new System.Drawing.Font("Arial", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Login.Location = new System.Drawing.Point(132, 56);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(128, 49);
            this.lbl_Login.TabIndex = 0;
            this.lbl_Login.Text = "Login";
            // 
            // tb_log_username
            // 
            this.tb_log_username.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_log_username.Location = new System.Drawing.Point(141, 183);
            this.tb_log_username.Name = "tb_log_username";
            this.tb_log_username.Size = new System.Drawing.Size(230, 40);
            this.tb_log_username.TabIndex = 1;
            this.tb_log_username.Text = "";
            // 
            // tb_log_password
            // 
            this.tb_log_password.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_log_password.Location = new System.Drawing.Point(141, 229);
            this.tb_log_password.Name = "tb_log_password";
            this.tb_log_password.Size = new System.Drawing.Size(230, 40);
            this.tb_log_password.TabIndex = 2;
            this.tb_log_password.Text = "";
            // 
            // lbl_log_username
            // 
            this.lbl_log_username.AutoSize = true;
            this.lbl_log_username.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_username.Location = new System.Drawing.Point(36, 200);
            this.lbl_log_username.Name = "lbl_log_username";
            this.lbl_log_username.Size = new System.Drawing.Size(96, 23);
            this.lbl_log_username.TabIndex = 3;
            this.lbl_log_username.Text = "username";
            // 
            // lbl_log_password
            // 
            this.lbl_log_password.AutoSize = true;
            this.lbl_log_password.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_log_password.Location = new System.Drawing.Point(37, 246);
            this.lbl_log_password.Name = "lbl_log_password";
            this.lbl_log_password.Size = new System.Drawing.Size(96, 23);
            this.lbl_log_password.TabIndex = 4;
            this.lbl_log_password.Text = "password";
            // 
            // btn_log_send
            // 
            this.btn_log_send.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_log_send.Location = new System.Drawing.Point(40, 321);
            this.btn_log_send.Name = "btn_log_send";
            this.btn_log_send.Size = new System.Drawing.Size(90, 30);
            this.btn_log_send.TabIndex = 5;
            this.btn_log_send.Text = "Send";
            this.btn_log_send.UseVisualStyleBackColor = true;
            this.btn_log_send.Click += new System.EventHandler(this.btn_log_send_Click);
            // 
            // btn_log_quit
            // 
            this.btn_log_quit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_log_quit.Location = new System.Drawing.Point(281, 321);
            this.btn_log_quit.Name = "btn_log_quit";
            this.btn_log_quit.Size = new System.Drawing.Size(90, 30);
            this.btn_log_quit.TabIndex = 6;
            this.btn_log_quit.Text = "Quit";
            this.btn_log_quit.UseVisualStyleBackColor = true;
            this.btn_log_quit.Click += new System.EventHandler(this.btn_log_quit_Click);
            // 
            // wdw_registry
            // 
            this.wdw_registry.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wdw_registry.Location = new System.Drawing.Point(161, 321);
            this.wdw_registry.Name = "wdw_registry";
            this.wdw_registry.Size = new System.Drawing.Size(90, 30);
            this.wdw_registry.TabIndex = 7;
            this.wdw_registry.Text = "Registry";
            this.wdw_registry.UseVisualStyleBackColor = true;
            this.wdw_registry.Click += new System.EventHandler(this.wdw_registry_Click);
            // 
            // wdw_login
            // 
            this.ClientSize = new System.Drawing.Size(409, 386);
            this.Controls.Add(this.wdw_registry);
            this.Controls.Add(this.btn_log_quit);
            this.Controls.Add(this.btn_log_send);
            this.Controls.Add(this.lbl_log_password);
            this.Controls.Add(this.lbl_log_username);
            this.Controls.Add(this.tb_log_password);
            this.Controls.Add(this.tb_log_username);
            this.Controls.Add(this.lbl_Login);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(425, 425);
            this.MinimumSize = new System.Drawing.Size(425, 425);
            this.Name = "wdw_login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_log_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_log_send_Click(object sender, EventArgs e)
        {
            Form open_calendar = new wdw_calendar();
            open_calendar.Show();
        }

        private void wdw_registry_Click(object sender, EventArgs e)
        {
            Form open_registry = new wdw_registry();
            open_registry.Show();
        }
    }
}
