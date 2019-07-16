namespace KaPlaner.GUI
{
    partial class Vorschaufenster
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
            this.LV_dates.HideSelection = false;
            this.LV_dates.Location = new System.Drawing.Point(12, 10);
            this.LV_dates.Name = "LV_dates";
            this.LV_dates.Size = new System.Drawing.Size(657, 401);
            this.LV_dates.TabIndex = 1;
            this.LV_dates.UseCompatibleStateImageBehavior = false;
            this.LV_dates.View = System.Windows.Forms.View.Details;
            this.LV_dates.SelectedIndexChanged += new System.EventHandler(this.LV_dates_SelectedIndexChanged);
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
            // Vorschaufenster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 446);
            this.Controls.Add(this.LV_dates);
            this.Name = "Vorschaufenster";
            this.Text = "Terminvorschau";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LV_dates;
        private System.Windows.Forms.ColumnHeader Titel;
        private System.Windows.Forms.ColumnHeader Ort;
        private System.Windows.Forms.ColumnHeader Beginn;
        private System.Windows.Forms.ColumnHeader Ende;
    }
}