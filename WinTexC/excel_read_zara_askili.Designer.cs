namespace WinTexC
{
    partial class excel_read_zara_askili
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
            this.data = new System.Windows.Forms.DataGridView();
            this.topsatsayi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.t_sevkno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_read = new System.Windows.Forms.Button();
            this.lnumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_sec = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.sname = new System.Windows.Forms.ComboBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btn_devam = new System.Windows.Forms.Button();
            this.t_sipno = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.t_sevkisemri = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.t_stno = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.t_star = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.t_msn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.t_ihrno = new System.Windows.Forms.TextBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.label10 = new System.Windows.Forms.Label();
            this.t_tasimasekli = new System.Windows.Forms.ComboBox();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btn_kydt = new System.Windows.Forms.Button();
            this.t_topadet = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.yenisevk = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // data
            // 
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(350, 121);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(828, 420);
            this.data.TabIndex = 29;
            // 
            // topsatsayi
            // 
            this.topsatsayi.Location = new System.Drawing.Point(675, 41);
            this.topsatsayi.Name = "topsatsayi";
            this.topsatsayi.Size = new System.Drawing.Size(71, 21);
            this.topsatsayi.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(653, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Top. Ek. Satır  Sayı";
            // 
            // t_sevkno
            // 
            this.t_sevkno.Location = new System.Drawing.Point(571, 41);
            this.t_sevkno.Name = "t_sevkno";
            this.t_sevkno.Size = new System.Drawing.Size(87, 21);
            this.t_sevkno.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(571, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Sevk  No";
            // 
            // btn_read
            // 
            this.btn_read.Location = new System.Drawing.Point(215, 77);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(75, 23);
            this.btn_read.TabIndex = 24;
            this.btn_read.Text = "Exceli Oku";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // lnumber
            // 
            this.lnumber.Location = new System.Drawing.Point(109, 79);
            this.lnumber.Name = "lnumber";
            this.lnumber.Size = new System.Drawing.Size(100, 21);
            this.lnumber.TabIndex = 23;
            this.lnumber.Text = "25";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Max Line Number";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Seçili Exceli Aç";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_sec
            // 
            this.btn_sec.Location = new System.Drawing.Point(215, 22);
            this.btn_sec.Name = "btn_sec";
            this.btn_sec.Size = new System.Drawing.Size(75, 23);
            this.btn_sec.TabIndex = 20;
            this.btn_sec.Text = "Dosya Seç";
            this.btn_sec.UseVisualStyleBackColor = true;
            this.btn_sec.Click += new System.EventHandler(this.btn_sec_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Dosya Adı";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Worksheet name";
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(5, 23);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_log.Size = new System.Drawing.Size(329, 357);
            this.txt_log.TabIndex = 15;
            // 
            // sname
            // 
            this.sname.FormattingEnabled = true;
            this.sname.Location = new System.Drawing.Point(109, 51);
            this.sname.Name = "sname";
            this.sname.Size = new System.Drawing.Size(100, 21);
            this.sname.TabIndex = 30;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.yenisevk);
            this.groupControl1.Controls.Add(this.label13);
            this.groupControl1.Controls.Add(this.t_topadet);
            this.groupControl1.Controls.Add(this.btn_devam);
            this.groupControl1.Controls.Add(this.t_sipno);
            this.groupControl1.Controls.Add(this.label12);
            this.groupControl1.Controls.Add(this.t_sevkisemri);
            this.groupControl1.Controls.Add(this.label11);
            this.groupControl1.Controls.Add(this.t_stno);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.t_star);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.t_msn);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.t_ihrno);
            this.groupControl1.Controls.Add(this.topsatsayi);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.t_sevkno);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Location = new System.Drawing.Point(350, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(828, 103);
            this.groupControl1.TabIndex = 31;
            this.groupControl1.Text = "Anlık Veri";
            // 
            // btn_devam
            // 
            this.btn_devam.BackColor = System.Drawing.Color.Green;
            this.btn_devam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_devam.ForeColor = System.Drawing.Color.White;
            this.btn_devam.Location = new System.Drawing.Point(571, 76);
            this.btn_devam.Name = "btn_devam";
            this.btn_devam.Size = new System.Drawing.Size(142, 25);
            this.btn_devam.TabIndex = 35;
            this.btn_devam.Text = "Okumaya Devam Et";
            this.btn_devam.UseVisualStyleBackColor = false;
            this.btn_devam.Visible = false;
            this.btn_devam.Click += new System.EventHandler(this.btn_devam_Click);
            // 
            // t_sipno
            // 
            this.t_sipno.FormattingEnabled = true;
            this.t_sipno.Location = new System.Drawing.Point(337, 26);
            this.t_sipno.Name = "t_sipno";
            this.t_sipno.Size = new System.Drawing.Size(228, 21);
            this.t_sipno.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(252, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Sevk İş Emri No";
            // 
            // t_sevkisemri
            // 
            this.t_sevkisemri.Location = new System.Drawing.Point(337, 81);
            this.t_sevkisemri.Name = "t_sevkisemri";
            this.t_sevkisemri.Size = new System.Drawing.Size(228, 21);
            this.t_sevkisemri.TabIndex = 40;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(241, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Sevkiyat Takip No";
            // 
            // t_stno
            // 
            this.t_stno.DropDownWidth = 500;
            this.t_stno.FormattingEnabled = true;
            this.t_stno.Location = new System.Drawing.Point(337, 54);
            this.t_stno.Name = "t_stno";
            this.t_stno.Size = new System.Drawing.Size(228, 21);
            this.t_stno.TabIndex = 38;
            this.t_stno.DropDown += new System.EventHandler(this.t_stno_DropDown);
            this.t_stno.SelectedIndexChanged += new System.EventHandler(this.t_stno_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(279, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Sipariş No";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "SevkTarihi";
            // 
            // t_star
            // 
            this.t_star.Location = new System.Drawing.Point(122, 78);
            this.t_star.Name = "t_star";
            this.t_star.Size = new System.Drawing.Size(100, 21);
            this.t_star.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Müşteri Sip No";
            // 
            // t_msn
            // 
            this.t_msn.Location = new System.Drawing.Point(122, 51);
            this.t_msn.Name = "t_msn";
            this.t_msn.Size = new System.Drawing.Size(100, 21);
            this.t_msn.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "İhr Dosya No";
            // 
            // t_ihrno
            // 
            this.t_ihrno.Location = new System.Drawing.Point(122, 24);
            this.t_ihrno.Name = "t_ihrno";
            this.t_ihrno.Size = new System.Drawing.Size(100, 21);
            this.t_ihrno.TabIndex = 30;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Controls.Add(this.t_tasimasekli);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.textBox1);
            this.groupControl2.Controls.Add(this.sname);
            this.groupControl2.Controls.Add(this.btn_sec);
            this.groupControl2.Controls.Add(this.button1);
            this.groupControl2.Controls.Add(this.btn_read);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.lnumber);
            this.groupControl2.Location = new System.Drawing.Point(5, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(339, 187);
            this.groupControl2.TabIndex = 32;
            this.groupControl2.Text = "Zara Asklı Müşteri Excel Bilgileri";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Yükleme Şekli";
            // 
            // t_tasimasekli
            // 
            this.t_tasimasekli.FormattingEnabled = true;
            this.t_tasimasekli.Items.AddRange(new object[] {
            "TIR",
            "GEMİ",
            "UÇAK"});
            this.t_tasimasekli.Location = new System.Drawing.Point(109, 109);
            this.t_tasimasekli.Name = "t_tasimasekli";
            this.t_tasimasekli.Size = new System.Drawing.Size(100, 21);
            this.t_tasimasekli.TabIndex = 32;
            this.t_tasimasekli.Text = "TIR";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.txt_log);
            this.groupControl3.Location = new System.Drawing.Point(5, 205);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(339, 389);
            this.groupControl3.TabIndex = 33;
            this.groupControl3.Text = "Okunan Satırlar";
            // 
            // btn_kydt
            // 
            this.btn_kydt.BackColor = System.Drawing.Color.Green;
            this.btn_kydt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kydt.ForeColor = System.Drawing.Color.White;
            this.btn_kydt.Location = new System.Drawing.Point(1055, 556);
            this.btn_kydt.Name = "btn_kydt";
            this.btn_kydt.Size = new System.Drawing.Size(123, 38);
            this.btn_kydt.TabIndex = 34;
            this.btn_kydt.Text = "Verileri Kaydet";
            this.btn_kydt.UseVisualStyleBackColor = false;
            this.btn_kydt.Click += new System.EventHandler(this.btn_kydt_Click);
            // 
            // t_topadet
            // 
            this.t_topadet.Location = new System.Drawing.Point(760, 41);
            this.t_topadet.Name = "t_topadet";
            this.t_topadet.Size = new System.Drawing.Size(63, 21);
            this.t_topadet.TabIndex = 42;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(757, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 43;
            this.label13.Text = "Toplam Adet";
            // 
            // yenisevk
            // 
            this.yenisevk.Location = new System.Drawing.Point(761, 79);
            this.yenisevk.Name = "yenisevk";
            this.yenisevk.Size = new System.Drawing.Size(63, 21);
            this.yenisevk.TabIndex = 44;
            // 
            // excel_read_zara_askili
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 602);
            this.Controls.Add(this.btn_kydt);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.data);
            this.Name = "excel_read_zara_askili";
            this.Text = "excel_read_zara_askili";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.excel_read_zara_askili_Load);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView data;
        private System.Windows.Forms.TextBox topsatsayi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox t_sevkno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_read;
        private System.Windows.Forms.TextBox lnumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_sec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.ComboBox sname;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox t_star;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox t_msn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox t_ihrno;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox t_tasimasekli;
        private System.Windows.Forms.Button btn_kydt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox t_stno;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox t_sevkisemri;
        private System.Windows.Forms.ComboBox t_sipno;
        private System.Windows.Forms.Button btn_devam;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox t_topadet;
        private System.Windows.Forms.TextBox yenisevk;
    }
}