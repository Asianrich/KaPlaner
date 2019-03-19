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
            this.TB_add_user = new System.Windows.Forms.TextBox();
            this.BTN_add_user = new System.Windows.Forms.Button();
            this.LBL_username = new System.Windows.Forms.Label();
            this.BTN_delete_user = new System.Windows.Forms.Button();
            this.Gaste = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Einladung = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BTN_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LV_users
            // 
            this.LV_users.AutoArrange = false;
            this.LV_users.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Gaste,
            this.Einladung});
            this.LV_users.Font = new System.Drawing.Font("Arial", 12F);
            this.LV_users.GridLines = true;
            this.LV_users.Location = new System.Drawing.Point(12, 12);
            this.LV_users.Name = "LV_users";
            this.LV_users.Size = new System.Drawing.Size(543, 426);
            this.LV_users.TabIndex = 0;
            this.LV_users.UseCompatibleStateImageBehavior = false;
            this.LV_users.View = System.Windows.Forms.View.Details;
            // 
            // TB_add_user
            // 
            this.TB_add_user.Location = new System.Drawing.Point(584, 155);
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
            // 
            // LBL_username
            // 
            this.LBL_username.AutoSize = true;
            this.LBL_username.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.LBL_username.Location = new System.Drawing.Point(580, 133);
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
            // 
            // Gaste
            // 
            this.Gaste.Text = "Gaeste";
            this.Gaste.Width = 213;
            // 
            // Einladung
            // 
            this.Einladung.Text = "Einladung";
            this.Einladung.Width = 266;
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
            // Wdw_user_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
        private System.Windows.Forms.ColumnHeader Einladung;
        private System.Windows.Forms.Button BTN_Close;
    }
}