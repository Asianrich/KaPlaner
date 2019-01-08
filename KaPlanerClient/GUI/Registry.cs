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
    public partial class wdw_registry : Form
    {
        private RichTextBox richTextBox1;
        private Label lbl_registry;
        private RichTextBox richTextBox2;
        private Label lbl_reg_username;
        private Label lbl_reg_password;
        private Label lbl_reg_password_cofirm;
        private Button btn_reg_send;
        private Button btn_reg_quit;
        private RichTextBox richTextBox3;

        public wdw_registry()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lbl_registry = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.lbl_reg_username = new System.Windows.Forms.Label();
            this.lbl_reg_password = new System.Windows.Forms.Label();
            this.lbl_reg_password_cofirm = new System.Windows.Forms.Label();
            this.btn_reg_send = new System.Windows.Forms.Button();
            this.btn_reg_quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(246, 273);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(230, 40);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // lbl_registry
            // 
            this.lbl_registry.AutoSize = true;
            this.lbl_registry.Font = new System.Drawing.Font("Arial", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_registry.Location = new System.Drawing.Point(187, 50);
            this.lbl_registry.Name = "lbl_registry";
            this.lbl_registry.Size = new System.Drawing.Size(180, 49);
            this.lbl_registry.TabIndex = 1;
            this.lbl_registry.Text = "Registry";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(246, 227);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(230, 40);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(246, 181);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(230, 40);
            this.richTextBox3.TabIndex = 3;
            this.richTextBox3.Text = "";
            this.richTextBox3.TextChanged += new System.EventHandler(this.richTextBox3_TextChanged);
            // 
            // lbl_reg_username
            // 
            this.lbl_reg_username.AutoSize = true;
            this.lbl_reg_username.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_reg_username.Location = new System.Drawing.Point(49, 198);
            this.lbl_reg_username.Name = "lbl_reg_username";
            this.lbl_reg_username.Size = new System.Drawing.Size(96, 23);
            this.lbl_reg_username.TabIndex = 4;
            this.lbl_reg_username.Text = "username";
            // 
            // lbl_reg_password
            // 
            this.lbl_reg_password.AutoSize = true;
            this.lbl_reg_password.Font = new System.Drawing.Font("Arial", 15F);
            this.lbl_reg_password.Location = new System.Drawing.Point(49, 244);
            this.lbl_reg_password.Name = "lbl_reg_password";
            this.lbl_reg_password.Size = new System.Drawing.Size(96, 23);
            this.lbl_reg_password.TabIndex = 5;
            this.lbl_reg_password.Text = "password";
            // 
            // lbl_reg_password_cofirm
            // 
            this.lbl_reg_password_cofirm.AutoSize = true;
            this.lbl_reg_password_cofirm.Font = new System.Drawing.Font("Arial", 15F);
            this.lbl_reg_password_cofirm.Location = new System.Drawing.Point(49, 290);
            this.lbl_reg_password_cofirm.Name = "lbl_reg_password_cofirm";
            this.lbl_reg_password_cofirm.Size = new System.Drawing.Size(166, 23);
            this.lbl_reg_password_cofirm.TabIndex = 6;
            this.lbl_reg_password_cofirm.Text = "password confirm";
            // 
            // btn_reg_send
            // 
            this.btn_reg_send.Font = new System.Drawing.Font("Arial", 12F);
            this.btn_reg_send.Location = new System.Drawing.Point(53, 357);
            this.btn_reg_send.Name = "btn_reg_send";
            this.btn_reg_send.Size = new System.Drawing.Size(90, 30);
            this.btn_reg_send.TabIndex = 7;
            this.btn_reg_send.Text = "Send";
            this.btn_reg_send.UseVisualStyleBackColor = true;
            this.btn_reg_send.Click += new System.EventHandler(this.btn_reg_send_Click);
            // 
            // btn_reg_quit
            // 
            this.btn_reg_quit.Font = new System.Drawing.Font("Arial", 12F);
            this.btn_reg_quit.Location = new System.Drawing.Point(386, 357);
            this.btn_reg_quit.Name = "btn_reg_quit";
            this.btn_reg_quit.Size = new System.Drawing.Size(90, 30);
            this.btn_reg_quit.TabIndex = 8;
            this.btn_reg_quit.Text = "Quit";
            this.btn_reg_quit.UseVisualStyleBackColor = true;
            this.btn_reg_quit.Click += new System.EventHandler(this.btn_reg_quit_Click);
            // 
            // wdw_registry
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(534, 436);
            this.Controls.Add(this.btn_reg_quit);
            this.Controls.Add(this.btn_reg_send);
            this.Controls.Add(this.lbl_reg_password_cofirm);
            this.Controls.Add(this.lbl_reg_password);
            this.Controls.Add(this.lbl_reg_username);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.lbl_registry);
            this.Controls.Add(this.richTextBox1);
            this.Name = "wdw_registry";
            this.Text = "Registry";
            this.Load += new System.EventHandler(this.Registry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Registry_Load(object sender, EventArgs e)
        {

        }

        private void btn_reg_send_Click(object sender, EventArgs e)
        {
            Form open_calendar = new wdw_calendar();
            open_calendar.Show();
            Close();
        }

        private void btn_reg_quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            //Test Push2
        }
    }
}
