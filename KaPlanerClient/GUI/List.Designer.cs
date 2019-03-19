namespace KaPlaner.GUI
{
    partial class Wdw_date_list
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
            this.LV_dates = new System.Windows.Forms.ListView();
            this.Titel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Beginn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ende = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BTN_delete = new System.Windows.Forms.Button();
            this.BTN_oeffnen = new System.Windows.Forms.Button();
            this.BTN_list_close = new System.Windows.Forms.Button();
            this.BTN_new = new System.Windows.Forms.Button();
            this.BTN_invite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LV_dates
            // 
            this.LV_dates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Titel,
            this.Ort,
            this.Beginn,
            this.Ende});
            this.LV_dates.Font = new System.Drawing.Font("Arial", 12F);
            this.LV_dates.FullRowSelect = true;
            this.LV_dates.GridLines = true;
            this.LV_dates.Location = new System.Drawing.Point(12, 12);
            this.LV_dates.Name = "LV_dates";
            this.LV_dates.Size = new System.Drawing.Size(860, 401);
            this.LV_dates.TabIndex = 0;
            this.LV_dates.UseCompatibleStateImageBehavior = false;
            this.LV_dates.View = System.Windows.Forms.View.Details;
            // 
            // Titel
            // 
            this.Titel.Text = "Titel";
            this.Titel.Width = 250;
            // 
            // Ort
            // 
            this.Ort.Text = "Ort";
            this.Ort.Width = 200;
            // 
            // Beginn
            // 
            this.Beginn.Text = "Beginn";
            this.Beginn.Width = 100;
            // 
            // Ende
            // 
            this.Ende.Text = "Ende";
            this.Ende.Width = 100;
            // 
            // BTN_delete
            // 
            this.BTN_delete.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_delete.Location = new System.Drawing.Point(214, 419);
            this.BTN_delete.Name = "BTN_delete";
            this.BTN_delete.Size = new System.Drawing.Size(95, 30);
            this.BTN_delete.TabIndex = 73;
            this.BTN_delete.Text = "Löschen";
            this.BTN_delete.UseVisualStyleBackColor = true;
            // 
            // BTN_oeffnen
            // 
            this.BTN_oeffnen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_oeffnen.Location = new System.Drawing.Point(12, 419);
            this.BTN_oeffnen.Name = "BTN_oeffnen";
            this.BTN_oeffnen.Size = new System.Drawing.Size(95, 30);
            this.BTN_oeffnen.TabIndex = 74;
            this.BTN_oeffnen.Text = "Öffnen";
            this.BTN_oeffnen.UseVisualStyleBackColor = true;
            this.BTN_oeffnen.Click += new System.EventHandler(this.BTN_oeffnen_Click);
            // 
            // BTN_list_close
            // 
            this.BTN_list_close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_list_close.Location = new System.Drawing.Point(315, 419);
            this.BTN_list_close.Name = "BTN_list_close";
            this.BTN_list_close.Size = new System.Drawing.Size(95, 30);
            this.BTN_list_close.TabIndex = 75;
            this.BTN_list_close.Text = "Schließen";
            this.BTN_list_close.UseVisualStyleBackColor = true;
            this.BTN_list_close.Click += new System.EventHandler(this.BTN_close_Click);
            // 
            // BTN_new
            // 
            this.BTN_new.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_new.Location = new System.Drawing.Point(113, 419);
            this.BTN_new.Name = "BTN_new";
            this.BTN_new.Size = new System.Drawing.Size(95, 30);
            this.BTN_new.TabIndex = 76;
            this.BTN_new.Text = "Neu";
            this.BTN_new.UseVisualStyleBackColor = true;
            this.BTN_new.Click += new System.EventHandler(this.BTN_new_Click);
            // 
            // BTN_invite
            // 
            this.BTN_invite.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_invite.Location = new System.Drawing.Point(416, 419);
            this.BTN_invite.Name = "BTN_invite";
            this.BTN_invite.Size = new System.Drawing.Size(95, 30);
            this.BTN_invite.TabIndex = 77;
            this.BTN_invite.Text = "Einladen";
            this.BTN_invite.UseVisualStyleBackColor = true;
            this.BTN_invite.Click += new System.EventHandler(this.BTN_invite_Click);
            // 
            // Wdw_date_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.BTN_invite);
            this.Controls.Add(this.BTN_new);
            this.Controls.Add(this.BTN_list_close);
            this.Controls.Add(this.BTN_oeffnen);
            this.Controls.Add(this.BTN_delete);
            this.Controls.Add(this.LV_dates);
            this.Name = "Wdw_date_list";
            this.Text = "Terminliste";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LV_dates;
        private System.Windows.Forms.ColumnHeader Titel;
        private System.Windows.Forms.ColumnHeader Ort;
        private System.Windows.Forms.ColumnHeader Beginn;
        private System.Windows.Forms.ColumnHeader Ende;
        private System.Windows.Forms.Button BTN_delete;
        private System.Windows.Forms.Button BTN_oeffnen;
        private System.Windows.Forms.Button BTN_list_close;
        private System.Windows.Forms.Button BTN_new;
        private System.Windows.Forms.Button BTN_invite;
    }
}