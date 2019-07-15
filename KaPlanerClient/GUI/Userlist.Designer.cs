namespace KaPlaner.GUI
{
    partial class Wdw_user_list
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LV_users = new System.Windows.Forms.ListView();
            this.Gaste = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TB_add_user = new System.Windows.Forms.TextBox();
            this.BTN_add_user = new System.Windows.Forms.Button();
            this.LBL_username = new System.Windows.Forms.Label();
            this.BTN_delete_user = new System.Windows.Forms.Button();
            this.BTN_Close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LV_users
            // 
            this.LV_users.AutoArrange = false;
            this.LV_users.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Gaste});
            this.LV_users.Font = new System.Drawing.Font("Arial", 12F);
            this.LV_users.GridLines = true;
            this.LV_users.Location = new System.Drawing.Point(12, 12);
            this.LV_users.Name = "LV_users";
            this.LV_users.Size = new System.Drawing.Size(543, 426);
            this.LV_users.TabIndex = 0;
            this.LV_users.UseCompatibleStateImageBehavior = false;
            this.LV_users.View = System.Windows.Forms.View.Details;
            // 
            // Gaste
            // 
            this.Gaste.Text = "Gaeste";
            this.Gaste.Width = 213;
            // 
            // TB_add_user
            // 
            this.TB_add_user.Location = new System.Drawing.Point(584, 152);
            this.TB_add_user.Name = "TB_add_user";
            this.TB_add_user.Size = new System.Drawing.Size(204, 20);
            this.TB_add_user.TabIndex = 1;
            // 
            // BTN_add_user
            // 
            this.BTN_add_user.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.BTN_add_user.Location = new System.Drawing.Point(584, 181);
            this.BTN_add_user.Name = "BTN_add_user";
            this.BTN_add_user.Size = new System.Drawing.Size(109, 29);
            this.BTN_add_user.TabIndex = 2;
            this.BTN_add_user.Text = "Hinzufügen";
            this.BTN_add_user.UseVisualStyleBackColor = true;
            this.BTN_add_user.Click += new System.EventHandler(this.BTN_add_user_Click);
            // 
            // LBL_username
            // 
            this.LBL_username.AutoSize = true;
            this.LBL_username.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.LBL_username.Location = new System.Drawing.Point(580, 130);
            this.LBL_username.Name = "LBL_username";
            this.LBL_username.Size = new System.Drawing.Size(127, 19);
            this.LBL_username.TabIndex = 3;
            this.LBL_username.Text = "Benutzername:";
            // 
            // BTN_delete_user
            // 
            this.BTN_delete_user.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.BTN_delete_user.Location = new System.Drawing.Point(699, 181);
            this.BTN_delete_user.Name = "BTN_delete_user";
            this.BTN_delete_user.Size = new System.Drawing.Size(89, 29);
            this.BTN_delete_user.TabIndex = 4;
            this.BTN_delete_user.Text = "Löschen";
            this.BTN_delete_user.UseVisualStyleBackColor = true;
            this.BTN_delete_user.Click += new System.EventHandler(this.BTN_delete_user_Click);
            // 
            // BTN_Close
            // 
            this.BTN_Close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.BTN_Close.Location = new System.Drawing.Point(584, 216);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(204, 29);
            this.BTN_Close.TabIndex = 5;
            this.BTN_Close.Text = "Schließen";
            this.BTN_Close.UseVisualStyleBackColor = true;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(580, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Eingabe:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(580, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Benutzername#ServerID";
            // 
            // Wdw_user_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_Close);
            this.Controls.Add(this.BTN_delete_user);
            this.Controls.Add(this.LBL_username);
            this.Controls.Add(this.BTN_add_user);
            this.Controls.Add(this.TB_add_user);
            this.Controls.Add(this.LV_users);
            this.Name = "Wdw_user_list";
            this.Text = "Gruppenmitglieder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LV_users;
        private System.Windows.Forms.TextBox TB_add_user;
        private System.Windows.Forms.Button BTN_add_user;
        private System.Windows.Forms.Label LBL_username;
        private System.Windows.Forms.Button BTN_delete_user;
        private System.Windows.Forms.ColumnHeader Gaste;
        private System.Windows.Forms.Button BTN_Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}