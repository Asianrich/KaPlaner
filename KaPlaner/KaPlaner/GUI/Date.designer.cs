namespace WindowsFormsApp1
{
    partial class wdw_date
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
            this.tab = new System.Windows.Forms.TabControl();
            this.tab_general = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lbl_priority = new System.Windows.Forms.Label();
            this.lbl_end = new System.Windows.Forms.Label();
            this.lbl_bemerkung = new System.Windows.Forms.Label();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.cb_free = new System.Windows.Forms.CheckBox();
            this.cb_fulltime = new System.Windows.Forms.CheckBox();
            this.lbl_disposability = new System.Windows.Forms.Label();
            this.lbl_minute = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.lbl_hours = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.cb_duration = new System.Windows.Forms.CheckBox();
            this.nUD_ende_minuten = new System.Windows.Forms.NumericUpDown();
            this.nUD_beginn_minuten = new System.Windows.Forms.NumericUpDown();
            this.nUD_ende_stunden = new System.Windows.Forms.NumericUpDown();
            this.nUD_beginn_stunden = new System.Windows.Forms.NumericUpDown();
            this.lbl_days = new System.Windows.Forms.Label();
            this.btn_end = new System.Windows.Forms.Button();
            this.btn_beginning = new System.Windows.Forms.Button();
            this.lbl_beginning = new System.Windows.Forms.Label();
            this.cb_ganztägig = new System.Windows.Forms.CheckBox();
            this.lbl_place = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_typ = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rb_tast = new System.Windows.Forms.RadioButton();
            this.rb_event = new System.Windows.Forms.RadioButton();
            this.tab_Repetition = new System.Windows.Forms.TabPage();
            this.cb_Sun = new System.Windows.Forms.CheckBox();
            this.cb_Sat = new System.Windows.Forms.CheckBox();
            this.cb_Fri = new System.Windows.Forms.CheckBox();
            this.cb_Thu = new System.Windows.Forms.CheckBox();
            this.cb_Wed = new System.Windows.Forms.CheckBox();
            this.cb_Tue = new System.Windows.Forms.CheckBox();
            this.cb_Mon = new System.Windows.Forms.CheckBox();
            this.rb_repeat_until = new System.Windows.Forms.RadioButton();
            this.lbl_time = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.btn_wiederholen_bis = new System.Windows.Forms.Button();
            this.rb_repeat = new System.Windows.Forms.RadioButton();
            this.rb_always_repeat = new System.Windows.Forms.RadioButton();
            this.lbl_times = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lbl_alle = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.mC_terminübersicht = new System.Windows.Forms.MonthCalendar();
            this.lbl_date_with_actions = new System.Windows.Forms.Label();
            this.lbl_which_day = new System.Windows.Forms.Label();
            this.lbl_weekday = new System.Windows.Forms.Label();
            this.lbl_limitation = new System.Windows.Forms.Label();
            this.lbl_frequency = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tab_general.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ende_minuten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_beginn_minuten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ende_stunden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_beginn_stunden)).BeginInit();
            this.tab_Repetition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tab_general);
            this.tab.Controls.Add(this.tab_Repetition);
            this.tab.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(12, 12);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(900, 550);
            this.tab.TabIndex = 0;
            // 
            // tab_general
            // 
            this.tab_general.Controls.Add(this.richTextBox1);
            this.tab_general.Controls.Add(this.lbl_priority);
            this.tab_general.Controls.Add(this.lbl_end);
            this.tab_general.Controls.Add(this.lbl_bemerkung);
            this.tab_general.Controls.Add(this.numericUpDown8);
            this.tab_general.Controls.Add(this.cb_free);
            this.tab_general.Controls.Add(this.cb_fulltime);
            this.tab_general.Controls.Add(this.lbl_disposability);
            this.tab_general.Controls.Add(this.lbl_minute);
            this.tab_general.Controls.Add(this.numericUpDown7);
            this.tab_general.Controls.Add(this.lbl_hours);
            this.tab_general.Controls.Add(this.numericUpDown6);
            this.tab_general.Controls.Add(this.numericUpDown5);
            this.tab_general.Controls.Add(this.cb_duration);
            this.tab_general.Controls.Add(this.nUD_ende_minuten);
            this.tab_general.Controls.Add(this.nUD_beginn_minuten);
            this.tab_general.Controls.Add(this.nUD_ende_stunden);
            this.tab_general.Controls.Add(this.nUD_beginn_stunden);
            this.tab_general.Controls.Add(this.lbl_days);
            this.tab_general.Controls.Add(this.btn_end);
            this.tab_general.Controls.Add(this.btn_beginning);
            this.tab_general.Controls.Add(this.lbl_beginning);
            this.tab_general.Controls.Add(this.cb_ganztägig);
            this.tab_general.Controls.Add(this.lbl_place);
            this.tab_general.Controls.Add(this.lbl_title);
            this.tab_general.Controls.Add(this.lbl_typ);
            this.tab_general.Controls.Add(this.textBox2);
            this.tab_general.Controls.Add(this.textBox1);
            this.tab_general.Controls.Add(this.rb_tast);
            this.tab_general.Controls.Add(this.rb_event);
            this.tab_general.Location = new System.Drawing.Point(4, 27);
            this.tab_general.Name = "tab_general";
            this.tab_general.Padding = new System.Windows.Forms.Padding(3);
            this.tab_general.Size = new System.Drawing.Size(892, 519);
            this.tab_general.TabIndex = 0;
            this.tab_general.Text = "General";
            this.tab_general.UseVisualStyleBackColor = true;
            this.tab_general.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(357, 517);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(518, 120);
            this.richTextBox1.TabIndex = 30;
            this.richTextBox1.Text = "";
            // 
            // lbl_priority
            // 
            this.lbl_priority.AutoSize = true;
            this.lbl_priority.Location = new System.Drawing.Point(50, 450);
            this.lbl_priority.Name = "lbl_priority";
            this.lbl_priority.Size = new System.Drawing.Size(55, 18);
            this.lbl_priority.TabIndex = 29;
            this.lbl_priority.Text = "priority";
            // 
            // lbl_end
            // 
            this.lbl_end.AutoSize = true;
            this.lbl_end.Location = new System.Drawing.Point(50, 300);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.Size = new System.Drawing.Size(34, 18);
            this.lbl_end.TabIndex = 28;
            this.lbl_end.Text = "end";
            // 
            // lbl_bemerkung
            // 
            this.lbl_bemerkung.AutoSize = true;
            this.lbl_bemerkung.Location = new System.Drawing.Point(90, 517);
            this.lbl_bemerkung.Name = "lbl_bemerkung";
            this.lbl_bemerkung.Size = new System.Drawing.Size(88, 18);
            this.lbl_bemerkung.TabIndex = 27;
            this.lbl_bemerkung.Text = "Bemerkung";
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Location = new System.Drawing.Point(189, 442);
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown8.TabIndex = 26;
            // 
            // cb_free
            // 
            this.cb_free.AutoSize = true;
            this.cb_free.Location = new System.Drawing.Point(326, 400);
            this.cb_free.Name = "cb_free";
            this.cb_free.Size = new System.Drawing.Size(54, 22);
            this.cb_free.TabIndex = 25;
            this.cb_free.Text = "free";
            this.cb_free.UseVisualStyleBackColor = true;
            // 
            // cb_fulltime
            // 
            this.cb_fulltime.AutoSize = true;
            this.cb_fulltime.Location = new System.Drawing.Point(189, 400);
            this.cb_fulltime.Name = "cb_fulltime";
            this.cb_fulltime.Size = new System.Drawing.Size(80, 22);
            this.cb_fulltime.TabIndex = 24;
            this.cb_fulltime.Text = "full-time";
            this.cb_fulltime.UseVisualStyleBackColor = true;
            // 
            // lbl_disposability
            // 
            this.lbl_disposability.AutoSize = true;
            this.lbl_disposability.Location = new System.Drawing.Point(50, 400);
            this.lbl_disposability.Name = "lbl_disposability";
            this.lbl_disposability.Size = new System.Drawing.Size(95, 18);
            this.lbl_disposability.TabIndex = 23;
            this.lbl_disposability.Text = "disposability";
            // 
            // lbl_minute
            // 
            this.lbl_minute.AutoSize = true;
            this.lbl_minute.Location = new System.Drawing.Point(568, 342);
            this.lbl_minute.Name = "lbl_minute";
            this.lbl_minute.Size = new System.Drawing.Size(54, 18);
            this.lbl_minute.TabIndex = 22;
            this.lbl_minute.Text = "minute";
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(512, 342);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown7.TabIndex = 21;
            // 
            // lbl_hours
            // 
            this.lbl_hours.AutoSize = true;
            this.lbl_hours.Location = new System.Drawing.Point(441, 342);
            this.lbl_hours.Name = "lbl_hours";
            this.lbl_hours.Size = new System.Drawing.Size(46, 18);
            this.lbl_hours.TabIndex = 20;
            this.lbl_hours.Text = "hours";
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(385, 342);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown6.TabIndex = 19;
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(270, 342);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown5.TabIndex = 18;
            // 
            // cb_duration
            // 
            this.cb_duration.AutoSize = true;
            this.cb_duration.Location = new System.Drawing.Point(189, 342);
            this.cb_duration.Name = "cb_duration";
            this.cb_duration.Size = new System.Drawing.Size(83, 22);
            this.cb_duration.TabIndex = 17;
            this.cb_duration.Text = "duration";
            this.cb_duration.UseVisualStyleBackColor = true;
            // 
            // nUD_ende_minuten
            // 
            this.nUD_ende_minuten.Location = new System.Drawing.Point(326, 292);
            this.nUD_ende_minuten.Name = "nUD_ende_minuten";
            this.nUD_ende_minuten.Size = new System.Drawing.Size(50, 26);
            this.nUD_ende_minuten.TabIndex = 16;
            // 
            // nUD_beginn_minuten
            // 
            this.nUD_beginn_minuten.Location = new System.Drawing.Point(326, 242);
            this.nUD_beginn_minuten.Name = "nUD_beginn_minuten";
            this.nUD_beginn_minuten.Size = new System.Drawing.Size(50, 26);
            this.nUD_beginn_minuten.TabIndex = 15;
            // 
            // nUD_ende_stunden
            // 
            this.nUD_ende_stunden.Location = new System.Drawing.Point(270, 292);
            this.nUD_ende_stunden.Name = "nUD_ende_stunden";
            this.nUD_ende_stunden.Size = new System.Drawing.Size(50, 26);
            this.nUD_ende_stunden.TabIndex = 14;
            // 
            // nUD_beginn_stunden
            // 
            this.nUD_beginn_stunden.Location = new System.Drawing.Point(270, 242);
            this.nUD_beginn_stunden.Name = "nUD_beginn_stunden";
            this.nUD_beginn_stunden.Size = new System.Drawing.Size(50, 26);
            this.nUD_beginn_stunden.TabIndex = 13;
            // 
            // lbl_days
            // 
            this.lbl_days.AutoSize = true;
            this.lbl_days.Location = new System.Drawing.Point(326, 342);
            this.lbl_days.Name = "lbl_days";
            this.lbl_days.Size = new System.Drawing.Size(41, 18);
            this.lbl_days.TabIndex = 12;
            this.lbl_days.Text = "days";
            // 
            // btn_end
            // 
            this.btn_end.Location = new System.Drawing.Point(189, 292);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(75, 26);
            this.btn_end.TabIndex = 11;
            this.btn_end.Text = "end";
            this.btn_end.UseVisualStyleBackColor = true;
            // 
            // btn_beginning
            // 
            this.btn_beginning.Location = new System.Drawing.Point(189, 242);
            this.btn_beginning.Name = "btn_beginning";
            this.btn_beginning.Size = new System.Drawing.Size(75, 26);
            this.btn_beginning.TabIndex = 10;
            this.btn_beginning.Text = "beginning";
            this.btn_beginning.UseVisualStyleBackColor = true;
            this.btn_beginning.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_beginning
            // 
            this.lbl_beginning.AutoSize = true;
            this.lbl_beginning.Location = new System.Drawing.Point(50, 250);
            this.lbl_beginning.Name = "lbl_beginning";
            this.lbl_beginning.Size = new System.Drawing.Size(76, 18);
            this.lbl_beginning.TabIndex = 9;
            this.lbl_beginning.Text = "beginning";
            // 
            // cb_ganztägig
            // 
            this.cb_ganztägig.AutoSize = true;
            this.cb_ganztägig.Location = new System.Drawing.Point(189, 182);
            this.cb_ganztägig.Name = "cb_ganztägig";
            this.cb_ganztägig.Size = new System.Drawing.Size(121, 22);
            this.cb_ganztägig.TabIndex = 8;
            this.cb_ganztägig.Text = "full-time event";
            this.cb_ganztägig.UseVisualStyleBackColor = true;
            // 
            // lbl_place
            // 
            this.lbl_place.AutoSize = true;
            this.lbl_place.Location = new System.Drawing.Point(50, 150);
            this.lbl_place.Name = "lbl_place";
            this.lbl_place.Size = new System.Drawing.Size(46, 18);
            this.lbl_place.TabIndex = 7;
            this.lbl_place.Text = "place";
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(50, 100);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(32, 18);
            this.lbl_title.TabIndex = 6;
            this.lbl_title.Text = "title";
            this.lbl_title.Click += new System.EventHandler(this.lbl_Titel_Click);
            // 
            // lbl_typ
            // 
            this.lbl_typ.AutoSize = true;
            this.lbl_typ.Location = new System.Drawing.Point(50, 50);
            this.lbl_typ.Name = "lbl_typ";
            this.lbl_typ.Size = new System.Drawing.Size(28, 18);
            this.lbl_typ.TabIndex = 5;
            this.lbl_typ.Text = "typ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(189, 142);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(315, 26);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 92);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(315, 26);
            this.textBox1.TabIndex = 3;
            // 
            // rb_tast
            // 
            this.rb_tast.AutoSize = true;
            this.rb_tast.Location = new System.Drawing.Point(326, 48);
            this.rb_tast.Name = "rb_tast";
            this.rb_tast.Size = new System.Drawing.Size(55, 22);
            this.rb_tast.TabIndex = 1;
            this.rb_tast.TabStop = true;
            this.rb_tast.Text = "task";
            this.rb_tast.UseVisualStyleBackColor = true;
            this.rb_tast.CheckedChanged += new System.EventHandler(this.rb_Aufgabe_CheckedChanged);
            // 
            // rb_event
            // 
            this.rb_event.AutoSize = true;
            this.rb_event.Location = new System.Drawing.Point(189, 48);
            this.rb_event.Name = "rb_event";
            this.rb_event.Size = new System.Drawing.Size(63, 22);
            this.rb_event.TabIndex = 0;
            this.rb_event.TabStop = true;
            this.rb_event.Text = "event";
            this.rb_event.UseVisualStyleBackColor = true;
            // 
            // tab_Repetition
            // 
            this.tab_Repetition.Controls.Add(this.cb_Sun);
            this.tab_Repetition.Controls.Add(this.cb_Sat);
            this.tab_Repetition.Controls.Add(this.cb_Fri);
            this.tab_Repetition.Controls.Add(this.cb_Thu);
            this.tab_Repetition.Controls.Add(this.cb_Wed);
            this.tab_Repetition.Controls.Add(this.cb_Tue);
            this.tab_Repetition.Controls.Add(this.cb_Mon);
            this.tab_Repetition.Controls.Add(this.rb_repeat_until);
            this.tab_Repetition.Controls.Add(this.lbl_time);
            this.tab_Repetition.Controls.Add(this.numericUpDown2);
            this.tab_Repetition.Controls.Add(this.btn_wiederholen_bis);
            this.tab_Repetition.Controls.Add(this.rb_repeat);
            this.tab_Repetition.Controls.Add(this.rb_always_repeat);
            this.tab_Repetition.Controls.Add(this.lbl_times);
            this.tab_Repetition.Controls.Add(this.numericUpDown1);
            this.tab_Repetition.Controls.Add(this.lbl_alle);
            this.tab_Repetition.Controls.Add(this.comboBox1);
            this.tab_Repetition.Controls.Add(this.mC_terminübersicht);
            this.tab_Repetition.Controls.Add(this.lbl_date_with_actions);
            this.tab_Repetition.Controls.Add(this.lbl_which_day);
            this.tab_Repetition.Controls.Add(this.lbl_weekday);
            this.tab_Repetition.Controls.Add(this.lbl_limitation);
            this.tab_Repetition.Controls.Add(this.lbl_frequency);
            this.tab_Repetition.Location = new System.Drawing.Point(4, 27);
            this.tab_Repetition.Name = "tab_Repetition";
            this.tab_Repetition.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Repetition.Size = new System.Drawing.Size(892, 519);
            this.tab_Repetition.TabIndex = 1;
            this.tab_Repetition.Text = "Repetition";
            this.tab_Repetition.UseVisualStyleBackColor = true;
            // 
            // cb_Sun
            // 
            this.cb_Sun.AutoSize = true;
            this.cb_Sun.Location = new System.Drawing.Point(658, 196);
            this.cb_Sun.Name = "cb_Sun";
            this.cb_Sun.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_Sun.Size = new System.Drawing.Size(54, 22);
            this.cb_Sun.TabIndex = 25;
            this.cb_Sun.Text = "Sun";
            this.cb_Sun.UseVisualStyleBackColor = true;
            // 
            // cb_Sat
            // 
            this.cb_Sat.AutoSize = true;
            this.cb_Sat.Location = new System.Drawing.Point(601, 196);
            this.cb_Sat.Name = "cb_Sat";
            this.cb_Sat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_Sat.Size = new System.Drawing.Size(51, 22);
            this.cb_Sat.TabIndex = 24;
            this.cb_Sat.Text = "Sat";
            this.cb_Sat.UseVisualStyleBackColor = true;
            // 
            // cb_Fri
            // 
            this.cb_Fri.AutoSize = true;
            this.cb_Fri.Location = new System.Drawing.Point(549, 196);
            this.cb_Fri.Name = "cb_Fri";
            this.cb_Fri.Size = new System.Drawing.Size(46, 22);
            this.cb_Fri.TabIndex = 23;
            this.cb_Fri.Text = "Fri";
            this.cb_Fri.UseVisualStyleBackColor = true;
            // 
            // cb_Thu
            // 
            this.cb_Thu.AutoSize = true;
            this.cb_Thu.Location = new System.Drawing.Point(491, 196);
            this.cb_Thu.Name = "cb_Thu";
            this.cb_Thu.Size = new System.Drawing.Size(52, 22);
            this.cb_Thu.TabIndex = 22;
            this.cb_Thu.Text = "Thu";
            this.cb_Thu.UseVisualStyleBackColor = true;
            // 
            // cb_Wed
            // 
            this.cb_Wed.AutoSize = true;
            this.cb_Wed.Location = new System.Drawing.Point(425, 196);
            this.cb_Wed.Name = "cb_Wed";
            this.cb_Wed.Size = new System.Drawing.Size(60, 22);
            this.cb_Wed.TabIndex = 21;
            this.cb_Wed.Text = "Wed";
            this.cb_Wed.UseVisualStyleBackColor = true;
            // 
            // cb_Tue
            // 
            this.cb_Tue.AutoSize = true;
            this.cb_Tue.Location = new System.Drawing.Point(367, 196);
            this.cb_Tue.Name = "cb_Tue";
            this.cb_Tue.Size = new System.Drawing.Size(52, 22);
            this.cb_Tue.TabIndex = 20;
            this.cb_Tue.Text = "Tue";
            this.cb_Tue.UseVisualStyleBackColor = true;
            // 
            // cb_Mon
            // 
            this.cb_Mon.AutoSize = true;
            this.cb_Mon.Location = new System.Drawing.Point(304, 196);
            this.cb_Mon.Name = "cb_Mon";
            this.cb_Mon.Size = new System.Drawing.Size(57, 22);
            this.cb_Mon.TabIndex = 19;
            this.cb_Mon.Text = "Mon";
            this.cb_Mon.UseVisualStyleBackColor = true;
            // 
            // rb_repeat_until
            // 
            this.rb_repeat_until.AutoSize = true;
            this.rb_repeat_until.Location = new System.Drawing.Point(300, 150);
            this.rb_repeat_until.Name = "rb_repeat_until";
            this.rb_repeat_until.Size = new System.Drawing.Size(102, 22);
            this.rb_repeat_until.TabIndex = 18;
            this.rb_repeat_until.TabStop = true;
            this.rb_repeat_until.Text = "repeat until";
            this.rb_repeat_until.UseVisualStyleBackColor = true;
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(676, 104);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(38, 18);
            this.lbl_time.TabIndex = 17;
            this.lbl_time.Text = "time";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(620, 100);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 26);
            this.numericUpDown2.TabIndex = 16;
            // 
            // btn_wiederholen_bis
            // 
            this.btn_wiederholen_bis.Location = new System.Drawing.Point(454, 148);
            this.btn_wiederholen_bis.Name = "btn_wiederholen_bis";
            this.btn_wiederholen_bis.Size = new System.Drawing.Size(75, 23);
            this.btn_wiederholen_bis.TabIndex = 14;
            this.btn_wiederholen_bis.Text = "Wiederholen bis";
            this.btn_wiederholen_bis.UseVisualStyleBackColor = true;
            // 
            // rb_repeat
            // 
            this.rb_repeat.AutoSize = true;
            this.rb_repeat.Location = new System.Drawing.Point(491, 100);
            this.rb_repeat.Name = "rb_repeat";
            this.rb_repeat.Size = new System.Drawing.Size(71, 22);
            this.rb_repeat.TabIndex = 13;
            this.rb_repeat.TabStop = true;
            this.rb_repeat.Text = "repeat";
            this.rb_repeat.UseVisualStyleBackColor = true;
            // 
            // rb_always_repeat
            // 
            this.rb_always_repeat.AutoSize = true;
            this.rb_always_repeat.Location = new System.Drawing.Point(300, 100);
            this.rb_always_repeat.Name = "rb_always_repeat";
            this.rb_always_repeat.Size = new System.Drawing.Size(122, 22);
            this.rb_always_repeat.TabIndex = 12;
            this.rb_always_repeat.TabStop = true;
            this.rb_always_repeat.Text = "always repeat";
            this.rb_always_repeat.UseVisualStyleBackColor = true;
            // 
            // lbl_times
            // 
            this.lbl_times.AutoSize = true;
            this.lbl_times.Location = new System.Drawing.Point(617, 50);
            this.lbl_times.Name = "lbl_times";
            this.lbl_times.Size = new System.Drawing.Size(38, 18);
            this.lbl_times.TabIndex = 11;
            this.lbl_times.Text = "time";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(491, 47);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown1.TabIndex = 10;
            // 
            // lbl_alle
            // 
            this.lbl_alle.AutoSize = true;
            this.lbl_alle.Location = new System.Drawing.Point(451, 50);
            this.lbl_alle.Name = "lbl_alle";
            this.lbl_alle.Size = new System.Drawing.Size(34, 18);
            this.lbl_alle.TabIndex = 9;
            this.lbl_alle.Text = "Alle";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(300, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 26);
            this.comboBox1.TabIndex = 8;
            // 
            // mC_terminübersicht
            // 
            this.mC_terminübersicht.CalendarDimensions = new System.Drawing.Size(3, 1);
            this.mC_terminübersicht.Location = new System.Drawing.Point(300, 300);
            this.mC_terminübersicht.Name = "mC_terminübersicht";
            this.mC_terminübersicht.TabIndex = 7;
            // 
            // lbl_date_with_actions
            // 
            this.lbl_date_with_actions.AutoSize = true;
            this.lbl_date_with_actions.Location = new System.Drawing.Point(50, 350);
            this.lbl_date_with_actions.Name = "lbl_date_with_actions";
            this.lbl_date_with_actions.Size = new System.Drawing.Size(124, 18);
            this.lbl_date_with_actions.TabIndex = 6;
            this.lbl_date_with_actions.Text = "date with actions";
            this.lbl_date_with_actions.Click += new System.EventHandler(this.lbl_termine_mit_aktionen_Click);
            // 
            // lbl_which_day
            // 
            this.lbl_which_day.AutoSize = true;
            this.lbl_which_day.Location = new System.Drawing.Point(52, 196);
            this.lbl_which_day.Name = "lbl_which_day";
            this.lbl_which_day.Size = new System.Drawing.Size(47, 18);
            this.lbl_which_day.TabIndex = 4;
            this.lbl_which_day.Text = "which";
            // 
            // lbl_weekday
            // 
            this.lbl_weekday.AutoSize = true;
            this.lbl_weekday.Location = new System.Drawing.Point(50, 150);
            this.lbl_weekday.Name = "lbl_weekday";
            this.lbl_weekday.Size = new System.Drawing.Size(70, 18);
            this.lbl_weekday.TabIndex = 3;
            this.lbl_weekday.Text = "weekday";
            // 
            // lbl_limitation
            // 
            this.lbl_limitation.AutoSize = true;
            this.lbl_limitation.Location = new System.Drawing.Point(50, 100);
            this.lbl_limitation.Name = "lbl_limitation";
            this.lbl_limitation.Size = new System.Drawing.Size(70, 18);
            this.lbl_limitation.TabIndex = 2;
            this.lbl_limitation.Text = "limitation";
            // 
            // lbl_frequency
            // 
            this.lbl_frequency.AutoSize = true;
            this.lbl_frequency.Location = new System.Drawing.Point(50, 50);
            this.lbl_frequency.Name = "lbl_frequency";
            this.lbl_frequency.Size = new System.Drawing.Size(75, 18);
            this.lbl_frequency.TabIndex = 1;
            this.lbl_frequency.Text = "frequency";
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(16, 582);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(90, 30);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(149, 582);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(90, 30);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // wdw_date
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 631);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.tab);
            this.MaximumSize = new System.Drawing.Size(950, 670);
            this.MinimumSize = new System.Drawing.Size(950, 670);
            this.Name = "wdw_date";
            this.Text = "Date";
            this.Load += new System.EventHandler(this.wdw_Eintrag_Load);
            this.tab.ResumeLayout(false);
            this.tab_general.ResumeLayout(false);
            this.tab_general.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ende_minuten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_beginn_minuten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ende_stunden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_beginn_stunden)).EndInit();
            this.tab_Repetition.ResumeLayout(false);
            this.tab_Repetition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tab_general;
        private System.Windows.Forms.TabPage tab_Repetition;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rb_tast;
        private System.Windows.Forms.RadioButton rb_event;
        private System.Windows.Forms.Label lbl_typ;
        private System.Windows.Forms.Label lbl_place;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label lbl_beginning;
        private System.Windows.Forms.CheckBox cb_ganztägig;
        private System.Windows.Forms.Button btn_beginning;
        private System.Windows.Forms.NumericUpDown nUD_ende_stunden;
        private System.Windows.Forms.NumericUpDown nUD_beginn_stunden;
        private System.Windows.Forms.Label lbl_days;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.CheckBox cb_duration;
        private System.Windows.Forms.NumericUpDown nUD_ende_minuten;
        private System.Windows.Forms.NumericUpDown nUD_beginn_minuten;
        private System.Windows.Forms.Label lbl_disposability;
        private System.Windows.Forms.Label lbl_minute;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label lbl_hours;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.CheckBox cb_free;
        private System.Windows.Forms.CheckBox cb_fulltime;
        private System.Windows.Forms.Label lbl_bemerkung;
        private System.Windows.Forms.Label lbl_end;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lbl_priority;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MonthCalendar mC_terminübersicht;
        private System.Windows.Forms.Label lbl_date_with_actions;
        private System.Windows.Forms.Label lbl_which_day;
        private System.Windows.Forms.Label lbl_weekday;
        private System.Windows.Forms.Label lbl_limitation;
        private System.Windows.Forms.Label lbl_frequency;
        private System.Windows.Forms.CheckBox cb_Sun;
        private System.Windows.Forms.CheckBox cb_Sat;
        private System.Windows.Forms.CheckBox cb_Fri;
        private System.Windows.Forms.CheckBox cb_Thu;
        private System.Windows.Forms.CheckBox cb_Wed;
        private System.Windows.Forms.CheckBox cb_Tue;
        private System.Windows.Forms.CheckBox cb_Mon;
        private System.Windows.Forms.RadioButton rb_repeat_until;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button btn_wiederholen_bis;
        private System.Windows.Forms.RadioButton rb_repeat;
        private System.Windows.Forms.RadioButton rb_always_repeat;
        private System.Windows.Forms.Label lbl_times;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lbl_alle;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_close;
    }
}

