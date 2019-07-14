namespace KaPlaner.GUI
{
    partial class RequestList
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
            this.LV_Dates = new System.Windows.Forms.ListView();
            this.Titel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Beginn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ende = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Einlader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Zustand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BT_Close = new System.Windows.Forms.Button();
            this.Bt_Open = new System.Windows.Forms.Button();
            this.Bt_Decline = new System.Windows.Forms.Button();
            this.BT_Accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LV_Dates
            // 
            this.LV_Dates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Titel,
            this.Ort,
            this.Beginn,
            this.Ende,
            this.Einlader,
            this.Zustand});
            this.LV_Dates.Location = new System.Drawing.Point(12, 12);
            this.LV_Dates.Name = "LV_Dates";
            this.LV_Dates.Size = new System.Drawing.Size(776, 360);
            this.LV_Dates.TabIndex = 0;
            this.LV_Dates.UseCompatibleStateImageBehavior = false;
            this.LV_Dates.View = System.Windows.Forms.View.Details;
            // 
            // Titel
            // 
            this.Titel.Text = "Titel";
            this.Titel.Width = 200;
            // 
            // Ort
            // 
            this.Ort.Text = "Ort";
            // 
            // Beginn
            // 
            this.Beginn.Text = "Beginn";
            // 
            // Ende
            // 
            this.Ende.Text = "Ende";
            // 
            // Einlader
            // 
            this.Einlader.Text = "Einlader";
            // 
            // Zustand
            // 
            this.Zustand.Text = "Zustand";
            // 
            // BT_Close
            // 
            this.BT_Close.Location = new System.Drawing.Point(12, 378);
            this.BT_Close.Name = "BT_Close";
            this.BT_Close.Size = new System.Drawing.Size(107, 34);
            this.BT_Close.TabIndex = 1;
            this.BT_Close.Text = "Abbrechen";
            this.BT_Close.UseVisualStyleBackColor = true;
            this.BT_Close.Click += new System.EventHandler(this.BT_Close_Click);
            // 
            // Bt_Open
            // 
            this.Bt_Open.Location = new System.Drawing.Point(125, 378);
            this.Bt_Open.Name = "Bt_Open";
            this.Bt_Open.Size = new System.Drawing.Size(107, 34);
            this.Bt_Open.TabIndex = 2;
            this.Bt_Open.Text = "Öffnen";
            this.Bt_Open.UseVisualStyleBackColor = true;
            this.Bt_Open.Click += new System.EventHandler(this.Bt_Open_Click);
            // 
            // Bt_Decline
            // 
            this.Bt_Decline.Location = new System.Drawing.Point(238, 378);
            this.Bt_Decline.Name = "Bt_Decline";
            this.Bt_Decline.Size = new System.Drawing.Size(107, 34);
            this.Bt_Decline.TabIndex = 3;
            this.Bt_Decline.Text = "Ablehnen";
            this.Bt_Decline.UseVisualStyleBackColor = true;
            this.Bt_Decline.Click += new System.EventHandler(this.Bt_Decline_Click);
            // 
            // BT_Accept
            // 
            this.BT_Accept.Location = new System.Drawing.Point(351, 377);
            this.BT_Accept.Name = "BT_Accept";
            this.BT_Accept.Size = new System.Drawing.Size(107, 34);
            this.BT_Accept.TabIndex = 4;
            this.BT_Accept.Text = "Annehmen";
            this.BT_Accept.UseVisualStyleBackColor = true;
            this.BT_Accept.Click += new System.EventHandler(this.BT_Accept_Click);
            // 
            // RequestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 423);
            this.Controls.Add(this.BT_Accept);
            this.Controls.Add(this.Bt_Decline);
            this.Controls.Add(this.Bt_Open);
            this.Controls.Add(this.BT_Close);
            this.Controls.Add(this.LV_Dates);
            this.Name = "RequestList";
            this.Text = "RequestList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LV_Dates;
        private System.Windows.Forms.ColumnHeader Titel;
        private System.Windows.Forms.ColumnHeader Ort;
        private System.Windows.Forms.ColumnHeader Beginn;
        private System.Windows.Forms.ColumnHeader Ende;
        private System.Windows.Forms.ColumnHeader Einlader;
        private System.Windows.Forms.Button BT_Close;
        private System.Windows.Forms.Button Bt_Open;
        private System.Windows.Forms.ColumnHeader Zustand;
        private System.Windows.Forms.Button Bt_Decline;
        private System.Windows.Forms.Button BT_Accept;
    }
}