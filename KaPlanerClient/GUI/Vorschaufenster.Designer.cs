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
            this.rTB_Terminvorschau = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rTB_Terminvorschau
            // 
            this.rTB_Terminvorschau.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_Terminvorschau.Location = new System.Drawing.Point(12, 12);
            this.rTB_Terminvorschau.Name = "rTB_Terminvorschau";
            this.rTB_Terminvorschau.ReadOnly = true;
            this.rTB_Terminvorschau.Size = new System.Drawing.Size(547, 346);
            this.rTB_Terminvorschau.TabIndex = 0;
            this.rTB_Terminvorschau.Text = "";
            // 
            // Vorschaufenster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 423);
            this.Controls.Add(this.rTB_Terminvorschau);
            this.Name = "Vorschaufenster";
            this.Text = "Terminvorschau";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rTB_Terminvorschau;
    }
}