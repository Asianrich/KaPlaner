namespace WindowsFormsApp1
{
    partial class Wdw_Ereignis
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
            this.LBL_hour_ende = new System.Windows.Forms.Label();
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
            this.rTB_beschreibung = new System.Windows.Forms.RichTextBox();
            this.lbl_prioritaet = new System.Windows.Forms.Label();
            this.lbl_end = new System.Windows.Forms.Label();
            this.lbl_beschreibung = new System.Windows.Forms.Label();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.lbl_beginn = new System.Windows.Forms.Label();
            this.cb_ganztägige_verantstaltung = new System.Windows.Forms.CheckBox();
            this.lbl_ort = new System.Windows.Forms.Label();
            this.lbl_titel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.TB_Title = new System.Windows.Forms.TextBox();
            this.tab_wiederholung = new System.Windows.Forms.TabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.nUD_son = new System.Windows.Forms.NumericUpDown();
            this.nUD_sam = new System.Windows.Forms.NumericUpDown();
            this.nUD_fre = new System.Windows.Forms.NumericUpDown();
            this.nUD_don = new System.Windows.Forms.NumericUpDown();
            this.nUD_mit = new System.Windows.Forms.NumericUpDown();
            this.nUD_die = new System.Windows.Forms.NumericUpDown();
            this.nUD_mon = new System.Windows.Forms.NumericUpDown();
            this.lbl_mal_wiederholen = new System.Windows.Forms.Label();
            this.cb_son = new System.Windows.Forms.CheckBox();
            this.cb_sam = new System.Windows.Forms.CheckBox();
            this.cb_fre = new System.Windows.Forms.CheckBox();
            this.cb_don = new System.Windows.Forms.CheckBox();
            this.cb_mit = new System.Windows.Forms.CheckBox();
            this.cb_die = new System.Windows.Forms.CheckBox();
            this.cb_Mon = new System.Windows.Forms.CheckBox();
            this.rb_wiederholen_bis = new System.Windows.Forms.RadioButton();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.btn_wiederholen_bis = new System.Windows.Forms.Button();
            this.rb_immer_wiederholen = new System.Windows.Forms.RadioButton();
            this.lbl_mal_pro = new System.Windows.Forms.Label();
            this.nUD_anzahl_wiederholungen = new System.Windows.Forms.NumericUpDown();
            this.cb_haeufigkeit = new System.Windows.Forms.ComboBox();
            this.mC_terminübersicht = new System.Windows.Forms.MonthCalendar();
            this.lbl_termine_mit_aktionen = new System.Windows.Forms.Label();
            this.lbl_welcher_tag = new System.Windows.Forms.Label();
            this.lbl_wochentag = new System.Windows.Forms.Label();
            this.lbl_beschraenkung = new System.Windows.Forms.Label();
            this.lbl_haeufigkeit = new System.Windows.Forms.Label();
            this.btn_speichern = new System.Windows.Forms.Button();
            this.btn_schließen = new System.Windows.Forms.Button();
            this.eingabe.SuspendLayout();
            this.tab_allgemein.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.tab_wiederholung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_son)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_sam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_fre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_don)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_mit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_die)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_mon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_anzahl_wiederholungen)).BeginInit();
            this.SuspendLayout();
            // 
            // eingabe
            // 
            this.eingabe.Controls.Add(this.tab_allgemein);
            this.eingabe.Controls.Add(this.tab_wiederholung);
            this.eingabe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eingabe.Location = new System.Drawing.Point(12, 12);
            this.eingabe.Name = "eingabe";
            this.eingabe.SelectedIndex = 0;
            this.eingabe.Size = new System.Drawing.Size(889, 693);
            this.eingabe.TabIndex = 0;
            // 
            // tab_allgemein
            // 
            this.tab_allgemein.Controls.Add(this.LBL_minute_end);
            this.tab_allgemein.Controls.Add(this.LBL_minute_beginn);
            this.tab_allgemein.Controls.Add(this.TB_minute_end);
            this.tab_allgemein.Controls.Add(this.TB_minute_beginn);
            this.tab_allgemein.Controls.Add(this.LBL_hour_ende);
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
            this.tab_allgemein.Controls.Add(this.rTB_beschreibung);
            this.tab_allgemein.Controls.Add(this.lbl_prioritaet);
            this.tab_allgemein.Controls.Add(this.lbl_end);
            this.tab_allgemein.Controls.Add(this.lbl_beschreibung);
            this.tab_allgemein.Controls.Add(this.numericUpDown8);
            this.tab_allgemein.Controls.Add(this.lbl_beginn);
            this.tab_allgemein.Controls.Add(this.cb_ganztägige_verantstaltung);
            this.tab_allgemein.Controls.Add(this.lbl_ort);
            this.tab_allgemein.Controls.Add(this.lbl_titel);
            this.tab_allgemein.Controls.Add(this.textBox2);
            this.tab_allgemein.Controls.Add(this.TB_Title);
            this.tab_allgemein.Location = new System.Drawing.Point(4, 27);
            this.tab_allgemein.Name = "tab_allgemein";
            this.tab_allgemein.Padding = new System.Windows.Forms.Padding(3);
            this.tab_allgemein.Size = new System.Drawing.Size(881, 662);
            this.tab_allgemein.TabIndex = 0;
            this.tab_allgemein.Text = "Allgemein";
            this.tab_allgemein.UseVisualStyleBackColor = true;
            this.tab_allgemein.Click += new System.EventHandler(this.tab_allgemein_Click);
            // 
            // LBL_minute_end
            // 
            this.LBL_minute_end.AutoSize = true;
            this.LBL_minute_end.Location = new System.Drawing.Point(512, 256);
            this.LBL_minute_end.Name = "LBL_minute_end";
            this.LBL_minute_end.Size = new System.Drawing.Size(54, 18);
            this.LBL_minute_end.TabIndex = 56;
            this.LBL_minute_end.Text = "Minute";
            this.LBL_minute_end.Click += new System.EventHandler(this.label9_Click);
            // 
            // LBL_minute_beginn
            // 
            this.LBL_minute_beginn.AutoSize = true;
            this.LBL_minute_beginn.Location = new System.Drawing.Point(174, 256);
            this.LBL_minute_beginn.Name = "LBL_minute_beginn";
            this.LBL_minute_beginn.Size = new System.Drawing.Size(54, 18);
            this.LBL_minute_beginn.TabIndex = 55;
            this.LBL_minute_beginn.Text = "Minute";
            this.LBL_minute_beginn.Click += new System.EventHandler(this.label10_Click);
            // 
            // TB_minute_end
            // 
            this.TB_minute_end.Location = new System.Drawing.Point(569, 253);
            this.TB_minute_end.Name = "TB_minute_end";
            this.TB_minute_end.Size = new System.Drawing.Size(30, 26);
            this.TB_minute_end.TabIndex = 54;
            this.TB_minute_end.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
            // 
            // TB_minute_beginn
            // 
            this.TB_minute_beginn.Location = new System.Drawing.Point(246, 253);
            this.TB_minute_beginn.Name = "TB_minute_beginn";
            this.TB_minute_beginn.Size = new System.Drawing.Size(30, 26);
            this.TB_minute_beginn.TabIndex = 53;
            this.TB_minute_beginn.TextChanged += new System.EventHandler(this.textBox12_TextChanged);
            // 
            // LBL_hour_ende
            // 
            this.LBL_hour_ende.AutoSize = true;
            this.LBL_hour_ende.Location = new System.Drawing.Point(509, 224);
            this.LBL_hour_ende.Name = "LBL_hour_ende";
            this.LBL_hour_ende.Size = new System.Drawing.Size(57, 18);
            this.LBL_hour_ende.TabIndex = 52;
            this.LBL_hour_ende.Text = "Stunde";
            // 
            // LBL_hour_beginn
            // 
            this.LBL_hour_beginn.AutoSize = true;
            this.LBL_hour_beginn.Location = new System.Drawing.Point(174, 224);
            this.LBL_hour_beginn.Name = "LBL_hour_beginn";
            this.LBL_hour_beginn.Size = new System.Drawing.Size(57, 18);
            this.LBL_hour_beginn.TabIndex = 51;
            this.LBL_hour_beginn.Text = "Stunde";
            // 
            // TB_hour_end
            // 
            this.TB_hour_end.Location = new System.Drawing.Point(569, 221);
            this.TB_hour_end.Name = "TB_hour_end";
            this.TB_hour_end.Size = new System.Drawing.Size(30, 26);
            this.TB_hour_end.TabIndex = 50;
            // 
            // TB_hour_beginn
            // 
            this.TB_hour_beginn.Location = new System.Drawing.Point(246, 221);
            this.TB_hour_beginn.Name = "TB_hour_beginn";
            this.TB_hour_beginn.Size = new System.Drawing.Size(30, 26);
            this.TB_hour_beginn.TabIndex = 49;
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
            this.TB_year_end.Location = new System.Drawing.Point(569, 189);
            this.TB_year_end.Name = "TB_year_end";
            this.TB_year_end.Size = new System.Drawing.Size(30, 26);
            this.TB_year_end.TabIndex = 46;
            // 
            // TB_year_beginn
            // 
            this.TB_year_beginn.Location = new System.Drawing.Point(246, 189);
            this.TB_year_beginn.Name = "TB_year_beginn";
            this.TB_year_beginn.Size = new System.Drawing.Size(30, 26);
            this.TB_year_beginn.TabIndex = 45;
            this.TB_year_beginn.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
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
            this.TB_month_end.Location = new System.Drawing.Point(569, 157);
            this.TB_month_end.Name = "TB_month_end";
            this.TB_month_end.Size = new System.Drawing.Size(30, 26);
            this.TB_month_end.TabIndex = 42;
            // 
            // TB_month_beginn
            // 
            this.TB_month_beginn.Location = new System.Drawing.Point(246, 157);
            this.TB_month_beginn.Name = "TB_month_beginn";
            this.TB_month_beginn.Size = new System.Drawing.Size(30, 26);
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
            this.TB_day_end.Location = new System.Drawing.Point(569, 125);
            this.TB_day_end.Name = "TB_day_end";
            this.TB_day_end.Size = new System.Drawing.Size(30, 26);
            this.TB_day_end.TabIndex = 32;
            // 
            // TB_day_beginn
            // 
            this.TB_day_beginn.Location = new System.Drawing.Point(246, 125);
            this.TB_day_beginn.Name = "TB_day_beginn";
            this.TB_day_beginn.Size = new System.Drawing.Size(30, 26);
            this.TB_day_beginn.TabIndex = 31;
            // 
            // rTB_beschreibung
            // 
            this.rTB_beschreibung.Location = new System.Drawing.Point(41, 382);
            this.rTB_beschreibung.Name = "rTB_beschreibung";
            this.rTB_beschreibung.Size = new System.Drawing.Size(558, 240);
            this.rTB_beschreibung.TabIndex = 30;
            this.rTB_beschreibung.Text = "";
            // 
            // lbl_prioritaet
            // 
            this.lbl_prioritaet.AutoSize = true;
            this.lbl_prioritaet.Location = new System.Drawing.Point(38, 306);
            this.lbl_prioritaet.Name = "lbl_prioritaet";
            this.lbl_prioritaet.Size = new System.Drawing.Size(72, 18);
            this.lbl_prioritaet.TabIndex = 29;
            this.lbl_prioritaet.Text = "Prioritaet";
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
            // numericUpDown8
            // 
            this.numericUpDown8.Location = new System.Drawing.Point(177, 304);
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown8.TabIndex = 26;
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
            // cb_ganztägige_verantstaltung
            // 
            this.cb_ganztägige_verantstaltung.AutoSize = true;
            this.cb_ganztägige_verantstaltung.Location = new System.Drawing.Point(177, 87);
            this.cb_ganztägige_verantstaltung.Name = "cb_ganztägige_verantstaltung";
            this.cb_ganztägige_verantstaltung.Size = new System.Drawing.Size(205, 22);
            this.cb_ganztägige_verantstaltung.TabIndex = 8;
            this.cb_ganztägige_verantstaltung.Text = "Ganztägige Veranstaltung";
            this.cb_ganztägige_verantstaltung.UseVisualStyleBackColor = true;
            this.cb_ganztägige_verantstaltung.CheckedChanged += new System.EventHandler(this.Cb_ganztägige_verantstaltung_CheckedChanged);
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
            this.lbl_titel.Click += new System.EventHandler(this.Lbl_Titel_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(177, 55);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(422, 26);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // TB_Title
            // 
            this.TB_Title.Location = new System.Drawing.Point(177, 23);
            this.TB_Title.Name = "TB_Title";
            this.TB_Title.Size = new System.Drawing.Size(422, 26);
            this.TB_Title.TabIndex = 3;
            // 
            // tab_wiederholung
            // 
            this.tab_wiederholung.Controls.Add(this.dateTimePicker1);
            this.tab_wiederholung.Controls.Add(this.nUD_son);
            this.tab_wiederholung.Controls.Add(this.nUD_sam);
            this.tab_wiederholung.Controls.Add(this.nUD_fre);
            this.tab_wiederholung.Controls.Add(this.nUD_don);
            this.tab_wiederholung.Controls.Add(this.nUD_mit);
            this.tab_wiederholung.Controls.Add(this.nUD_die);
            this.tab_wiederholung.Controls.Add(this.nUD_mon);
            this.tab_wiederholung.Controls.Add(this.lbl_mal_wiederholen);
            this.tab_wiederholung.Controls.Add(this.cb_son);
            this.tab_wiederholung.Controls.Add(this.cb_sam);
            this.tab_wiederholung.Controls.Add(this.cb_fre);
            this.tab_wiederholung.Controls.Add(this.cb_don);
            this.tab_wiederholung.Controls.Add(this.cb_mit);
            this.tab_wiederholung.Controls.Add(this.cb_die);
            this.tab_wiederholung.Controls.Add(this.cb_Mon);
            this.tab_wiederholung.Controls.Add(this.rb_wiederholen_bis);
            this.tab_wiederholung.Controls.Add(this.numericUpDown2);
            this.tab_wiederholung.Controls.Add(this.btn_wiederholen_bis);
            this.tab_wiederholung.Controls.Add(this.rb_immer_wiederholen);
            this.tab_wiederholung.Controls.Add(this.lbl_mal_pro);
            this.tab_wiederholung.Controls.Add(this.nUD_anzahl_wiederholungen);
            this.tab_wiederholung.Controls.Add(this.cb_haeufigkeit);
            this.tab_wiederholung.Controls.Add(this.mC_terminübersicht);
            this.tab_wiederholung.Controls.Add(this.lbl_termine_mit_aktionen);
            this.tab_wiederholung.Controls.Add(this.lbl_welcher_tag);
            this.tab_wiederholung.Controls.Add(this.lbl_wochentag);
            this.tab_wiederholung.Controls.Add(this.lbl_beschraenkung);
            this.tab_wiederholung.Controls.Add(this.lbl_haeufigkeit);
            this.tab_wiederholung.Location = new System.Drawing.Point(4, 27);
            this.tab_wiederholung.Name = "tab_wiederholung";
            this.tab_wiederholung.Padding = new System.Windows.Forms.Padding(3);
            this.tab_wiederholung.Size = new System.Drawing.Size(881, 662);
            this.tab_wiederholung.TabIndex = 1;
            this.tab_wiederholung.Text = "Wiederholung";
            this.tab_wiederholung.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(360, 354);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker1.TabIndex = 34;
            // 
            // nUD_son
            // 
            this.nUD_son.Location = new System.Drawing.Point(556, 167);
            this.nUD_son.Name = "nUD_son";
            this.nUD_son.Size = new System.Drawing.Size(50, 26);
            this.nUD_son.TabIndex = 33;
            // 
            // nUD_sam
            // 
            this.nUD_sam.Location = new System.Drawing.Point(500, 167);
            this.nUD_sam.Name = "nUD_sam";
            this.nUD_sam.Size = new System.Drawing.Size(50, 26);
            this.nUD_sam.TabIndex = 32;
            // 
            // nUD_fre
            // 
            this.nUD_fre.Location = new System.Drawing.Point(444, 167);
            this.nUD_fre.Name = "nUD_fre";
            this.nUD_fre.Size = new System.Drawing.Size(50, 26);
            this.nUD_fre.TabIndex = 31;
            // 
            // nUD_don
            // 
            this.nUD_don.Location = new System.Drawing.Point(387, 167);
            this.nUD_don.Name = "nUD_don";
            this.nUD_don.Size = new System.Drawing.Size(50, 26);
            this.nUD_don.TabIndex = 30;
            // 
            // nUD_mit
            // 
            this.nUD_mit.Location = new System.Drawing.Point(331, 167);
            this.nUD_mit.Name = "nUD_mit";
            this.nUD_mit.Size = new System.Drawing.Size(50, 26);
            this.nUD_mit.TabIndex = 29;
            // 
            // nUD_die
            // 
            this.nUD_die.Location = new System.Drawing.Point(273, 167);
            this.nUD_die.Name = "nUD_die";
            this.nUD_die.Size = new System.Drawing.Size(50, 26);
            this.nUD_die.TabIndex = 28;
            // 
            // nUD_mon
            // 
            this.nUD_mon.Location = new System.Drawing.Point(210, 167);
            this.nUD_mon.Name = "nUD_mon";
            this.nUD_mon.Size = new System.Drawing.Size(50, 26);
            this.nUD_mon.TabIndex = 27;
            // 
            // lbl_mal_wiederholen
            // 
            this.lbl_mal_wiederholen.AutoSize = true;
            this.lbl_mal_wiederholen.Location = new System.Drawing.Point(431, 87);
            this.lbl_mal_wiederholen.Name = "lbl_mal_wiederholen";
            this.lbl_mal_wiederholen.Size = new System.Drawing.Size(121, 18);
            this.lbl_mal_wiederholen.TabIndex = 26;
            this.lbl_mal_wiederholen.Text = "mal wiederholen";
            // 
            // cb_son
            // 
            this.cb_son.AutoSize = true;
            this.cb_son.Location = new System.Drawing.Point(556, 131);
            this.cb_son.Name = "cb_son";
            this.cb_son.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_son.Size = new System.Drawing.Size(55, 22);
            this.cb_son.TabIndex = 25;
            this.cb_son.Text = "Son";
            this.cb_son.UseVisualStyleBackColor = true;
            // 
            // cb_sam
            // 
            this.cb_sam.AutoSize = true;
            this.cb_sam.Location = new System.Drawing.Point(500, 131);
            this.cb_sam.Name = "cb_sam";
            this.cb_sam.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_sam.Size = new System.Drawing.Size(60, 22);
            this.cb_sam.TabIndex = 24;
            this.cb_sam.Text = "Sam";
            this.cb_sam.UseVisualStyleBackColor = true;
            // 
            // cb_fre
            // 
            this.cb_fre.AutoSize = true;
            this.cb_fre.Location = new System.Drawing.Point(445, 131);
            this.cb_fre.Name = "cb_fre";
            this.cb_fre.Size = new System.Drawing.Size(51, 22);
            this.cb_fre.TabIndex = 23;
            this.cb_fre.Text = "Fre";
            this.cb_fre.UseVisualStyleBackColor = true;
            // 
            // cb_don
            // 
            this.cb_don.AutoSize = true;
            this.cb_don.Location = new System.Drawing.Point(387, 131);
            this.cb_don.Name = "cb_don";
            this.cb_don.Size = new System.Drawing.Size(56, 22);
            this.cb_don.TabIndex = 22;
            this.cb_don.Text = "Don";
            this.cb_don.UseVisualStyleBackColor = true;
            // 
            // cb_mit
            // 
            this.cb_mit.AutoSize = true;
            this.cb_mit.Location = new System.Drawing.Point(331, 131);
            this.cb_mit.Name = "cb_mit";
            this.cb_mit.Size = new System.Drawing.Size(48, 22);
            this.cb_mit.TabIndex = 21;
            this.cb_mit.Text = "Mit";
            this.cb_mit.UseVisualStyleBackColor = true;
            // 
            // cb_die
            // 
            this.cb_die.AutoSize = true;
            this.cb_die.Location = new System.Drawing.Point(273, 131);
            this.cb_die.Name = "cb_die";
            this.cb_die.Size = new System.Drawing.Size(52, 22);
            this.cb_die.TabIndex = 20;
            this.cb_die.Text = "Die";
            this.cb_die.UseVisualStyleBackColor = true;
            // 
            // cb_Mon
            // 
            this.cb_Mon.AutoSize = true;
            this.cb_Mon.Location = new System.Drawing.Point(210, 131);
            this.cb_Mon.Name = "cb_Mon";
            this.cb_Mon.Size = new System.Drawing.Size(57, 22);
            this.cb_Mon.TabIndex = 19;
            this.cb_Mon.Text = "Mon";
            this.cb_Mon.UseVisualStyleBackColor = true;
            // 
            // rb_wiederholen_bis
            // 
            this.rb_wiederholen_bis.AutoSize = true;
            this.rb_wiederholen_bis.Location = new System.Drawing.Point(558, 85);
            this.rb_wiederholen_bis.Name = "rb_wiederholen_bis";
            this.rb_wiederholen_bis.Size = new System.Drawing.Size(171, 22);
            this.rb_wiederholen_bis.TabIndex = 18;
            this.rb_wiederholen_bis.TabStop = true;
            this.rb_wiederholen_bis.Text = "Wiederholen bis zum";
            this.rb_wiederholen_bis.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(375, 85);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown2.TabIndex = 16;
            // 
            // btn_wiederholen_bis
            // 
            this.btn_wiederholen_bis.Location = new System.Drawing.Point(735, 83);
            this.btn_wiederholen_bis.Name = "btn_wiederholen_bis";
            this.btn_wiederholen_bis.Size = new System.Drawing.Size(120, 26);
            this.btn_wiederholen_bis.TabIndex = 14;
            this.btn_wiederholen_bis.Text = "Wiederholen bis";
            this.btn_wiederholen_bis.UseVisualStyleBackColor = true;
            // 
            // rb_immer_wiederholen
            // 
            this.rb_immer_wiederholen.AutoSize = true;
            this.rb_immer_wiederholen.Location = new System.Drawing.Point(210, 85);
            this.rb_immer_wiederholen.Name = "rb_immer_wiederholen";
            this.rb_immer_wiederholen.Size = new System.Drawing.Size(157, 22);
            this.rb_immer_wiederholen.TabIndex = 12;
            this.rb_immer_wiederholen.TabStop = true;
            this.rb_immer_wiederholen.Text = "Immer wiederholen";
            this.rb_immer_wiederholen.UseVisualStyleBackColor = true;
            // 
            // lbl_mal_pro
            // 
            this.lbl_mal_pro.AutoSize = true;
            this.lbl_mal_pro.Location = new System.Drawing.Point(336, 43);
            this.lbl_mal_pro.Name = "lbl_mal_pro";
            this.lbl_mal_pro.Size = new System.Drawing.Size(33, 18);
            this.lbl_mal_pro.TabIndex = 11;
            this.lbl_mal_pro.Text = "mal";
            // 
            // nUD_anzahl_wiederholungen
            // 
            this.nUD_anzahl_wiederholungen.Location = new System.Drawing.Point(210, 41);
            this.nUD_anzahl_wiederholungen.Name = "nUD_anzahl_wiederholungen";
            this.nUD_anzahl_wiederholungen.Size = new System.Drawing.Size(120, 26);
            this.nUD_anzahl_wiederholungen.TabIndex = 10;
            // 
            // cb_haeufigkeit
            // 
            this.cb_haeufigkeit.FormattingEnabled = true;
            this.cb_haeufigkeit.Location = new System.Drawing.Point(375, 40);
            this.cb_haeufigkeit.Name = "cb_haeufigkeit";
            this.cb_haeufigkeit.Size = new System.Drawing.Size(121, 26);
            this.cb_haeufigkeit.TabIndex = 8;
            // 
            // mC_terminübersicht
            // 
            this.mC_terminübersicht.CalendarDimensions = new System.Drawing.Size(4, 1);
            this.mC_terminübersicht.Location = new System.Drawing.Point(53, 280);
            this.mC_terminübersicht.Name = "mC_terminübersicht";
            this.mC_terminübersicht.TabIndex = 7;
            // 
            // lbl_termine_mit_aktionen
            // 
            this.lbl_termine_mit_aktionen.AutoSize = true;
            this.lbl_termine_mit_aktionen.Location = new System.Drawing.Point(52, 253);
            this.lbl_termine_mit_aktionen.Name = "lbl_termine_mit_aktionen";
            this.lbl_termine_mit_aktionen.Size = new System.Drawing.Size(152, 18);
            this.lbl_termine_mit_aktionen.TabIndex = 6;
            this.lbl_termine_mit_aktionen.Text = "Termine mit Aktionen";
            // 
            // lbl_welcher_tag
            // 
            this.lbl_welcher_tag.AutoSize = true;
            this.lbl_welcher_tag.Location = new System.Drawing.Point(52, 169);
            this.lbl_welcher_tag.Name = "lbl_welcher_tag";
            this.lbl_welcher_tag.Size = new System.Drawing.Size(94, 18);
            this.lbl_welcher_tag.TabIndex = 4;
            this.lbl_welcher_tag.Text = "Welcher Tag";
            // 
            // lbl_wochentag
            // 
            this.lbl_wochentag.AutoSize = true;
            this.lbl_wochentag.Location = new System.Drawing.Point(50, 132);
            this.lbl_wochentag.Name = "lbl_wochentag";
            this.lbl_wochentag.Size = new System.Drawing.Size(87, 18);
            this.lbl_wochentag.TabIndex = 3;
            this.lbl_wochentag.Text = "Wochentag";
            // 
            // lbl_beschraenkung
            // 
            this.lbl_beschraenkung.AutoSize = true;
            this.lbl_beschraenkung.Location = new System.Drawing.Point(50, 87);
            this.lbl_beschraenkung.Name = "lbl_beschraenkung";
            this.lbl_beschraenkung.Size = new System.Drawing.Size(116, 18);
            this.lbl_beschraenkung.TabIndex = 2;
            this.lbl_beschraenkung.Text = "Beschraenkung";
            // 
            // lbl_haeufigkeit
            // 
            this.lbl_haeufigkeit.AutoSize = true;
            this.lbl_haeufigkeit.Location = new System.Drawing.Point(50, 49);
            this.lbl_haeufigkeit.Name = "lbl_haeufigkeit";
            this.lbl_haeufigkeit.Size = new System.Drawing.Size(87, 18);
            this.lbl_haeufigkeit.TabIndex = 1;
            this.lbl_haeufigkeit.Text = "Haeufigkeit";
            // 
            // btn_speichern
            // 
            this.btn_speichern.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_speichern.Location = new System.Drawing.Point(12, 711);
            this.btn_speichern.Name = "btn_speichern";
            this.btn_speichern.Size = new System.Drawing.Size(95, 30);
            this.btn_speichern.TabIndex = 1;
            this.btn_speichern.Text = "Speichern";
            this.btn_speichern.UseVisualStyleBackColor = true;
            this.btn_speichern.Click += new System.EventHandler(this.btn_speichern_Click);
            // 
            // btn_schließen
            // 
            this.btn_schließen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_schließen.Location = new System.Drawing.Point(113, 711);
            this.btn_schließen.Name = "btn_schließen";
            this.btn_schließen.Size = new System.Drawing.Size(95, 30);
            this.btn_schließen.TabIndex = 2;
            this.btn_schließen.Text = "Schließen";
            this.btn_schließen.UseVisualStyleBackColor = true;
            this.btn_schließen.Click += new System.EventHandler(this.Btn_close_Click);
            // 
            // Wdw_Ereignis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 761);
            this.Controls.Add(this.btn_schließen);
            this.Controls.Add(this.btn_speichern);
            this.Controls.Add(this.eingabe);
            this.MaximumSize = new System.Drawing.Size(950, 800);
            this.MinimumSize = new System.Drawing.Size(950, 800);
            this.Name = "Wdw_Ereignis";
            this.Text = "Date";
            this.eingabe.ResumeLayout(false);
            this.tab_allgemein.ResumeLayout(false);
            this.tab_allgemein.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.tab_wiederholung.ResumeLayout(false);
            this.tab_wiederholung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_son)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_sam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_fre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_don)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_mit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_die)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_mon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_anzahl_wiederholungen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl eingabe;
        private System.Windows.Forms.TabPage tab_allgemein;
        private System.Windows.Forms.TabPage tab_wiederholung;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox TB_Title;
        private System.Windows.Forms.Label lbl_ort;
        private System.Windows.Forms.Label lbl_titel;
        private System.Windows.Forms.Label lbl_beginn;
        private System.Windows.Forms.CheckBox cb_ganztägige_verantstaltung;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.Label lbl_beschreibung;
        private System.Windows.Forms.Label lbl_end;
        private System.Windows.Forms.RichTextBox rTB_beschreibung;
        private System.Windows.Forms.Label lbl_prioritaet;
        private System.Windows.Forms.ComboBox cb_haeufigkeit;
        private System.Windows.Forms.MonthCalendar mC_terminübersicht;
        private System.Windows.Forms.Label lbl_termine_mit_aktionen;
        private System.Windows.Forms.Label lbl_welcher_tag;
        private System.Windows.Forms.Label lbl_wochentag;
        private System.Windows.Forms.Label lbl_beschraenkung;
        private System.Windows.Forms.Label lbl_haeufigkeit;
        private System.Windows.Forms.CheckBox cb_son;
        private System.Windows.Forms.CheckBox cb_sam;
        private System.Windows.Forms.CheckBox cb_fre;
        private System.Windows.Forms.CheckBox cb_don;
        private System.Windows.Forms.CheckBox cb_mit;
        private System.Windows.Forms.CheckBox cb_die;
        private System.Windows.Forms.CheckBox cb_Mon;
        private System.Windows.Forms.RadioButton rb_wiederholen_bis;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button btn_wiederholen_bis;
        private System.Windows.Forms.RadioButton rb_immer_wiederholen;
        private System.Windows.Forms.Label lbl_mal_pro;
        private System.Windows.Forms.NumericUpDown nUD_anzahl_wiederholungen;
        private System.Windows.Forms.Button btn_speichern;
        private System.Windows.Forms.Button btn_schließen;
        private System.Windows.Forms.Label lbl_mal_wiederholen;
        private System.Windows.Forms.NumericUpDown nUD_don;
        private System.Windows.Forms.NumericUpDown nUD_mit;
        private System.Windows.Forms.NumericUpDown nUD_die;
        private System.Windows.Forms.NumericUpDown nUD_mon;
        private System.Windows.Forms.NumericUpDown nUD_son;
        private System.Windows.Forms.NumericUpDown nUD_sam;
        private System.Windows.Forms.NumericUpDown nUD_fre;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label LBL_minute_end;
        private System.Windows.Forms.Label LBL_minute_beginn;
        private System.Windows.Forms.TextBox TB_minute_end;
        private System.Windows.Forms.TextBox TB_minute_beginn;
        private System.Windows.Forms.Label LBL_hour_ende;
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

