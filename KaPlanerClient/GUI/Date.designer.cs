namespace WindowsFormsApp1
{
    partial class Wdw_KaEvent
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.eingabe = new System.Windows.Forms.TabControl();
            this.tab_allgemein = new System.Windows.Forms.TabPage();
            this.LBL_minute_end = new System.Windows.Forms.Label();
            this.LBL_minute_beginn = new System.Windows.Forms.Label();
            this.TB_minute_end = new System.Windows.Forms.TextBox();
            this.TB_minute_beginn = new System.Windows.Forms.TextBox();
            this.LBL_hour_end = new System.Windows.Forms.Label();
            this.LBL_hour_beginn = new System.Windows.Forms.Label();
            this.TB_hour_end = new System.Windows.Forms.TextBox();
            this.TB_hour_beginn = new System.Windows.Forms.TextBox();
            this.LBL_year_end = new System.Windows.Forms.Label();
            this.LBL_year_beginn = new System.Windows.Forms.Label();
            this.TB_year_end = new System.Windows.Forms.TextBox();
            this.TB_year_beginn = new System.Windows.Forms.TextBox();
            this.LBL_month_end = new System.Windows.Forms.Label();
            this.LBL_month_beginn = new System.Windows.Forms.Label();
            this.TB_month_end = new System.Windows.Forms.TextBox();
            this.TB_month_beginn = new System.Windows.Forms.TextBox();
            this.LBL_day_end = new System.Windows.Forms.Label();
            this.LBL_day_beginn = new System.Windows.Forms.Label();
            this.TB_day_end = new System.Windows.Forms.TextBox();
            this.TB_day_beginn = new System.Windows.Forms.TextBox();
            this.RTB_description = new System.Windows.Forms.RichTextBox();
            this.lbl_end = new System.Windows.Forms.Label();
            this.lbl_beschreibung = new System.Windows.Forms.Label();
            this.lbl_beginn = new System.Windows.Forms.Label();
            this.lbl_ort = new System.Windows.Forms.Label();
            this.lbl_titel = new System.Windows.Forms.Label();
            this.TB_Place = new System.Windows.Forms.TextBox();
            this.TB_Title = new System.Windows.Forms.TextBox();
            this.BTN_save = new System.Windows.Forms.Button();
            this.BTN_close = new System.Windows.Forms.Button();
            this.eingabe.SuspendLayout();
            this.tab_allgemein.SuspendLayout();
            this.SuspendLayout();
            // 
            // eingabe
            // 
            this.eingabe.Controls.Add(this.tab_allgemein);
            this.eingabe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eingabe.Location = new System.Drawing.Point(12, 12);
            this.eingabe.Name = "eingabe";
            this.eingabe.SelectedIndex = 0;
            this.eingabe.Size = new System.Drawing.Size(660, 693);
            this.eingabe.TabIndex = 0;
            // 
            // tab_allgemein
            // 
            this.tab_allgemein.Controls.Add(this.LBL_minute_end);
            this.tab_allgemein.Controls.Add(this.LBL_minute_beginn);
            this.tab_allgemein.Controls.Add(this.TB_minute_end);
            this.tab_allgemein.Controls.Add(this.TB_minute_beginn);
            this.tab_allgemein.Controls.Add(this.LBL_hour_end);
            this.tab_allgemein.Controls.Add(this.LBL_hour_beginn);
            this.tab_allgemein.Controls.Add(this.TB_hour_end);
            this.tab_allgemein.Controls.Add(this.TB_hour_beginn);
            this.tab_allgemein.Controls.Add(this.LBL_year_end);
            this.tab_allgemein.Controls.Add(this.LBL_year_beginn);
            this.tab_allgemein.Controls.Add(this.TB_year_end);
            this.tab_allgemein.Controls.Add(this.TB_year_beginn);
            this.tab_allgemein.Controls.Add(this.LBL_month_end);
            this.tab_allgemein.Controls.Add(this.LBL_month_beginn);
            this.tab_allgemein.Controls.Add(this.TB_month_end);
            this.tab_allgemein.Controls.Add(this.TB_month_beginn);
            this.tab_allgemein.Controls.Add(this.LBL_day_end);
            this.tab_allgemein.Controls.Add(this.LBL_day_beginn);
            this.tab_allgemein.Controls.Add(this.TB_day_end);
            this.tab_allgemein.Controls.Add(this.TB_day_beginn);
            this.tab_allgemein.Controls.Add(this.RTB_description);
            this.tab_allgemein.Controls.Add(this.lbl_end);
            this.tab_allgemein.Controls.Add(this.lbl_beschreibung);
            this.tab_allgemein.Controls.Add(this.lbl_beginn);
            this.tab_allgemein.Controls.Add(this.lbl_ort);
            this.tab_allgemein.Controls.Add(this.lbl_titel);
            this.tab_allgemein.Controls.Add(this.TB_Place);
            this.tab_allgemein.Controls.Add(this.TB_Title);
            this.tab_allgemein.Location = new System.Drawing.Point(4, 27);
            this.tab_allgemein.Name = "tab_allgemein";
            this.tab_allgemein.Padding = new System.Windows.Forms.Padding(3);
            this.tab_allgemein.Size = new System.Drawing.Size(652, 662);
            this.tab_allgemein.TabIndex = 0;
            this.tab_allgemein.Text = "Allgemein";
            this.tab_allgemein.UseVisualStyleBackColor = true;
            // 
            // LBL_minute_end
            // 
            this.LBL_minute_end.AutoSize = true;
            this.LBL_minute_end.Enabled = false;
            this.LBL_minute_end.Location = new System.Drawing.Point(512, 256);
            this.LBL_minute_end.Name = "LBL_minute_end";
            this.LBL_minute_end.Size = new System.Drawing.Size(54, 18);
            this.LBL_minute_end.TabIndex = 56;
            this.LBL_minute_end.Text = "Minute";
            // 
            // LBL_minute_beginn
            // 
            this.LBL_minute_beginn.AutoSize = true;
            this.LBL_minute_beginn.Enabled = false;
            this.LBL_minute_beginn.Location = new System.Drawing.Point(174, 256);
            this.LBL_minute_beginn.Name = "LBL_minute_beginn";
            this.LBL_minute_beginn.Size = new System.Drawing.Size(54, 18);
            this.LBL_minute_beginn.TabIndex = 55;
            this.LBL_minute_beginn.Text = "Minute";
            // 
            // TB_minute_end
            // 
            this.TB_minute_end.Enabled = false;
            this.TB_minute_end.Location = new System.Drawing.Point(569, 253);
            this.TB_minute_end.Name = "TB_minute_end";
            this.TB_minute_end.Size = new System.Drawing.Size(50, 26);
            this.TB_minute_end.TabIndex = 54;
            this.TB_minute_end.Text = "00";
            this.TB_minute_end.TextChanged += new System.EventHandler(this.EmptyBox);
            this.TB_minute_end.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // TB_minute_beginn
            // 
            this.TB_minute_beginn.Enabled = false;
            this.TB_minute_beginn.Location = new System.Drawing.Point(246, 253);
            this.TB_minute_beginn.Name = "TB_minute_beginn";
            this.TB_minute_beginn.Size = new System.Drawing.Size(50, 26);
            this.TB_minute_beginn.TabIndex = 53;
            this.TB_minute_beginn.Text = "00";
            this.TB_minute_beginn.TextChanged += new System.EventHandler(this.EmptyBox);
            this.TB_minute_beginn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // LBL_hour_end
            // 
            this.LBL_hour_end.AutoSize = true;
            this.LBL_hour_end.Enabled = false;
            this.LBL_hour_end.Location = new System.Drawing.Point(509, 224);
            this.LBL_hour_end.Name = "LBL_hour_end";
            this.LBL_hour_end.Size = new System.Drawing.Size(57, 18);
            this.LBL_hour_end.TabIndex = 52;
            this.LBL_hour_end.Text = "Stunde";
            // 
            // LBL_hour_beginn
            // 
            this.LBL_hour_beginn.AutoSize = true;
            this.LBL_hour_beginn.Enabled = false;
            this.LBL_hour_beginn.Location = new System.Drawing.Point(174, 224);
            this.LBL_hour_beginn.Name = "LBL_hour_beginn";
            this.LBL_hour_beginn.Size = new System.Drawing.Size(57, 18);
            this.LBL_hour_beginn.TabIndex = 51;
            this.LBL_hour_beginn.Text = "Stunde";
            // 
            // TB_hour_end
            // 
            this.TB_hour_end.Enabled = false;
            this.TB_hour_end.Location = new System.Drawing.Point(569, 221);
            this.TB_hour_end.Name = "TB_hour_end";
            this.TB_hour_end.Size = new System.Drawing.Size(50, 26);
            this.TB_hour_end.TabIndex = 50;
            this.TB_hour_end.Text = "00";
            this.TB_hour_end.TextChanged += new System.EventHandler(this.EmptyBox);
            this.TB_hour_end.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // TB_hour_beginn
            // 
            this.TB_hour_beginn.Enabled = false;
            this.TB_hour_beginn.Location = new System.Drawing.Point(246, 221);
            this.TB_hour_beginn.Name = "TB_hour_beginn";
            this.TB_hour_beginn.Size = new System.Drawing.Size(50, 26);
            this.TB_hour_beginn.TabIndex = 49;
            this.TB_hour_beginn.Text = "00";
            this.TB_hour_beginn.TextChanged += new System.EventHandler(this.EmptyBox);
            this.TB_hour_beginn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // LBL_year_end
            // 
            this.LBL_year_end.AutoSize = true;
            this.LBL_year_end.Location = new System.Drawing.Point(509, 192);
            this.LBL_year_end.Name = "LBL_year_end";
            this.LBL_year_end.Size = new System.Drawing.Size(38, 18);
            this.LBL_year_end.TabIndex = 48;
            this.LBL_year_end.Text = "Jahr";
            // 
            // LBL_year_beginn
            // 
            this.LBL_year_beginn.AutoSize = true;
            this.LBL_year_beginn.Location = new System.Drawing.Point(174, 192);
            this.LBL_year_beginn.Name = "LBL_year_beginn";
            this.LBL_year_beginn.Size = new System.Drawing.Size(38, 18);
            this.LBL_year_beginn.TabIndex = 47;
            this.LBL_year_beginn.Text = "Jahr";
            // 
            // TB_year_end
            // 
            this.TB_year_end.Enabled = false;
            this.TB_year_end.Location = new System.Drawing.Point(569, 189);
            this.TB_year_end.Name = "TB_year_end";
            this.TB_year_end.Size = new System.Drawing.Size(50, 26);
            this.TB_year_end.TabIndex = 46;
            // 
            // TB_year_beginn
            // 
            this.TB_year_beginn.Enabled = false;
            this.TB_year_beginn.Location = new System.Drawing.Point(246, 189);
            this.TB_year_beginn.Name = "TB_year_beginn";
            this.TB_year_beginn.Size = new System.Drawing.Size(50, 26);
            this.TB_year_beginn.TabIndex = 45;
            // 
            // LBL_month_end
            // 
            this.LBL_month_end.AutoSize = true;
            this.LBL_month_end.Location = new System.Drawing.Point(509, 160);
            this.LBL_month_end.Name = "LBL_month_end";
            this.LBL_month_end.Size = new System.Drawing.Size(51, 18);
            this.LBL_month_end.TabIndex = 44;
            this.LBL_month_end.Text = "Monat";
            // 
            // LBL_month_beginn
            // 
            this.LBL_month_beginn.AutoSize = true;
            this.LBL_month_beginn.Location = new System.Drawing.Point(174, 160);
            this.LBL_month_beginn.Name = "LBL_month_beginn";
            this.LBL_month_beginn.Size = new System.Drawing.Size(51, 18);
            this.LBL_month_beginn.TabIndex = 43;
            this.LBL_month_beginn.Text = "Monat";
            // 
            // TB_month_end
            // 
            this.TB_month_end.Enabled = false;
            this.TB_month_end.Location = new System.Drawing.Point(569, 157);
            this.TB_month_end.Name = "TB_month_end";
            this.TB_month_end.Size = new System.Drawing.Size(50, 26);
            this.TB_month_end.TabIndex = 42;
            // 
            // TB_month_beginn
            // 
            this.TB_month_beginn.Enabled = false;
            this.TB_month_beginn.Location = new System.Drawing.Point(246, 157);
            this.TB_month_beginn.Name = "TB_month_beginn";
            this.TB_month_beginn.Size = new System.Drawing.Size(50, 26);
            this.TB_month_beginn.TabIndex = 41;
            // 
            // LBL_day_end
            // 
            this.LBL_day_end.AutoSize = true;
            this.LBL_day_end.Location = new System.Drawing.Point(509, 125);
            this.LBL_day_end.Name = "LBL_day_end";
            this.LBL_day_end.Size = new System.Drawing.Size(33, 18);
            this.LBL_day_end.TabIndex = 40;
            this.LBL_day_end.Text = "Tag";
            // 
            // LBL_day_beginn
            // 
            this.LBL_day_beginn.AutoSize = true;
            this.LBL_day_beginn.Location = new System.Drawing.Point(174, 128);
            this.LBL_day_beginn.Name = "LBL_day_beginn";
            this.LBL_day_beginn.Size = new System.Drawing.Size(33, 18);
            this.LBL_day_beginn.TabIndex = 39;
            this.LBL_day_beginn.Text = "Tag";
            // 
            // TB_day_end
            // 
            this.TB_day_end.Enabled = false;
            this.TB_day_end.Location = new System.Drawing.Point(569, 125);
            this.TB_day_end.Name = "TB_day_end";
            this.TB_day_end.Size = new System.Drawing.Size(50, 26);
            this.TB_day_end.TabIndex = 32;
            // 
            // TB_day_beginn
            // 
            this.TB_day_beginn.Enabled = false;
            this.TB_day_beginn.Location = new System.Drawing.Point(246, 125);
            this.TB_day_beginn.Name = "TB_day_beginn";
            this.TB_day_beginn.Size = new System.Drawing.Size(50, 26);
            this.TB_day_beginn.TabIndex = 31;
            // 
            // RTB_description
            // 
            this.RTB_description.Location = new System.Drawing.Point(41, 382);
            this.RTB_description.Name = "RTB_description";
            this.RTB_description.Size = new System.Drawing.Size(578, 240);
            this.RTB_description.TabIndex = 30;
            this.RTB_description.Text = "";
            this.RTB_description.TextChanged += new System.EventHandler(this.RTB_description_TextChanged);
            // 
            // lbl_end
            // 
            this.lbl_end.AutoSize = true;
            this.lbl_end.Location = new System.Drawing.Point(410, 128);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.Size = new System.Drawing.Size(45, 18);
            this.lbl_end.TabIndex = 28;
            this.lbl_end.Text = "Ende";
            // 
            // lbl_beschreibung
            // 
            this.lbl_beschreibung.AutoSize = true;
            this.lbl_beschreibung.Location = new System.Drawing.Point(38, 361);
            this.lbl_beschreibung.Name = "lbl_beschreibung";
            this.lbl_beschreibung.Size = new System.Drawing.Size(104, 18);
            this.lbl_beschreibung.TabIndex = 27;
            this.lbl_beschreibung.Text = "Beschreibung";
            // 
            // lbl_beginn
            // 
            this.lbl_beginn.AutoSize = true;
            this.lbl_beginn.Location = new System.Drawing.Point(38, 128);
            this.lbl_beginn.Name = "lbl_beginn";
            this.lbl_beginn.Size = new System.Drawing.Size(57, 18);
            this.lbl_beginn.TabIndex = 9;
            this.lbl_beginn.Text = "Beginn";
            // 
            // lbl_ort
            // 
            this.lbl_ort.AutoSize = true;
            this.lbl_ort.Location = new System.Drawing.Point(38, 63);
            this.lbl_ort.Name = "lbl_ort";
            this.lbl_ort.Size = new System.Drawing.Size(29, 18);
            this.lbl_ort.TabIndex = 7;
            this.lbl_ort.Text = "Ort";
            // 
            // lbl_titel
            // 
            this.lbl_titel.AutoSize = true;
            this.lbl_titel.Location = new System.Drawing.Point(38, 31);
            this.lbl_titel.Name = "lbl_titel";
            this.lbl_titel.Size = new System.Drawing.Size(36, 18);
            this.lbl_titel.TabIndex = 6;
            this.lbl_titel.Text = "Titel";
            // 
            // TB_Place
            // 
            this.TB_Place.Location = new System.Drawing.Point(177, 55);
            this.TB_Place.Name = "TB_Place";
            this.TB_Place.Size = new System.Drawing.Size(442, 26);
            this.TB_Place.TabIndex = 4;
            this.TB_Place.TextChanged += new System.EventHandler(this.TB_Place_TextChanged);
            // 
            // TB_Title
            // 
            this.TB_Title.Location = new System.Drawing.Point(177, 23);
            this.TB_Title.Name = "TB_Title";
            this.TB_Title.Size = new System.Drawing.Size(442, 26);
            this.TB_Title.TabIndex = 3;
            this.TB_Title.TextChanged += new System.EventHandler(this.TB_Title_TextChanged);
            // 
            // BTN_save
            // 
            this.BTN_save.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_save.Location = new System.Drawing.Point(16, 711);
            this.BTN_save.Name = "BTN_save";
            this.BTN_save.Size = new System.Drawing.Size(95, 30);
            this.BTN_save.TabIndex = 1;
            this.BTN_save.Text = "Speichern";
            this.BTN_save.UseVisualStyleBackColor = true;
            this.BTN_save.Click += new System.EventHandler(this.BTN_save_Click);
            // 
            // BTN_close
            // 
            this.BTN_close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_close.Location = new System.Drawing.Point(117, 711);
            this.BTN_close.Name = "BTN_close";
            this.BTN_close.Size = new System.Drawing.Size(95, 30);
            this.BTN_close.TabIndex = 2;
            this.BTN_close.Text = "Schließen";
            this.BTN_close.UseVisualStyleBackColor = true;
            this.BTN_close.Click += new System.EventHandler(this.BTN_close_Click);
            // 
            // Wdw_KaEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 749);
            this.Controls.Add(this.BTN_close);
            this.Controls.Add(this.BTN_save);
            this.Controls.Add(this.eingabe);
            this.MaximumSize = new System.Drawing.Size(700, 800);
            this.MinimumSize = new System.Drawing.Size(700, 726);
            this.Name = "Wdw_KaEvent";
            this.Text = "Date";
            this.eingabe.ResumeLayout(false);
            this.tab_allgemein.ResumeLayout(false);
            this.tab_allgemein.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl eingabe;
        private System.Windows.Forms.TabPage tab_allgemein;
        private System.Windows.Forms.TextBox TB_Place;
        private System.Windows.Forms.TextBox TB_Title;
        private System.Windows.Forms.Label lbl_ort;
        private System.Windows.Forms.Label lbl_titel;
        private System.Windows.Forms.Label lbl_beginn;
        private System.Windows.Forms.Label lbl_beschreibung;
        private System.Windows.Forms.Label lbl_end;
        private System.Windows.Forms.RichTextBox RTB_description;
        private System.Windows.Forms.Button BTN_save;
        private System.Windows.Forms.Button BTN_close;
        private System.Windows.Forms.Label LBL_minute_end;
        private System.Windows.Forms.Label LBL_minute_beginn;
        private System.Windows.Forms.TextBox TB_minute_end;
        private System.Windows.Forms.TextBox TB_minute_beginn;
        private System.Windows.Forms.Label LBL_hour_end;
        private System.Windows.Forms.Label LBL_hour_beginn;
        private System.Windows.Forms.TextBox TB_hour_end;
        private System.Windows.Forms.TextBox TB_hour_beginn;
        private System.Windows.Forms.Label LBL_year_end;
        private System.Windows.Forms.Label LBL_year_beginn;
        private System.Windows.Forms.TextBox TB_year_end;
        private System.Windows.Forms.TextBox TB_year_beginn;
        private System.Windows.Forms.Label LBL_month_end;
        private System.Windows.Forms.Label LBL_month_beginn;
        private System.Windows.Forms.TextBox TB_month_end;
        private System.Windows.Forms.TextBox TB_month_beginn;
        private System.Windows.Forms.Label LBL_day_end;
        private System.Windows.Forms.Label LBL_day_beginn;
        private System.Windows.Forms.TextBox TB_day_end;
        private System.Windows.Forms.TextBox TB_day_beginn;
    }
}

