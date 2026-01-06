namespace WinTexC
{
    partial class excel_read_lpp
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_sec = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lnumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_read = new System.Windows.Forms.Button();
            this.data = new System.Windows.Forms.DataGridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.topsatsayi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dosyano = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.sname = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Worksheet name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(152, 21);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dosya Adı";
            // 
            // btn_sec
            // 
            this.btn_sec.Location = new System.Drawing.Point(271, 27);
            this.btn_sec.Name = "btn_sec";
            this.btn_sec.Size = new System.Drawing.Size(75, 23);
            this.btn_sec.TabIndex = 5;
            this.btn_sec.Text = "Dosya Seç";
            this.btn_sec.UseVisualStyleBackColor = true;
            this.btn_sec.Click += new System.EventHandler(this.btn_sec_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Seçili Exceli Aç";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lnumber
            // 
            this.lnumber.Location = new System.Drawing.Point(112, 84);
            this.lnumber.Name = "lnumber";
            this.lnumber.Size = new System.Drawing.Size(152, 21);
            this.lnumber.TabIndex = 8;
            this.lnumber.Text = "25";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Max Line Number";
            // 
            // btn_read
            // 
            this.btn_read.Location = new System.Drawing.Point(271, 82);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(75, 23);
            this.btn_read.TabIndex = 9;
            this.btn_read.Text = "Exceli Oku";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // data
            // 
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(369, 161);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(860, 446);
            this.data.TabIndex = 14;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.topsatsayi);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.dosyano);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Location = new System.Drawing.Point(369, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(860, 145);
            this.groupControl1.TabIndex = 15;
            this.groupControl1.Text = "Anlık Bilgi";
            // 
            // topsatsayi
            // 
            this.topsatsayi.Location = new System.Drawing.Point(710, 54);
            this.topsatsayi.Name = "topsatsayi";
            this.topsatsayi.Size = new System.Drawing.Size(100, 21);
            this.topsatsayi.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(558, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Toplam Eklenecek Satır  Sayı";
            // 
            // dosyano
            // 
            this.dosyano.Location = new System.Drawing.Point(710, 28);
            this.dosyano.Name = "dosyano";
            this.dosyano.Size = new System.Drawing.Size(100, 21);
            this.dosyano.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(650, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Dosya No";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.sname);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.btn_read);
            this.groupControl2.Controls.Add(this.textBox1);
            this.groupControl2.Controls.Add(this.lnumber);
            this.groupControl2.Controls.Add(this.btn_sec);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.button1);
            this.groupControl2.Location = new System.Drawing.Point(12, 6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(351, 149);
            this.groupControl2.TabIndex = 16;
            this.groupControl2.Text = "LPP Müşteri Excel Bilgileri";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.txt_log);
            this.groupControl3.Location = new System.Drawing.Point(12, 161);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(351, 446);
            this.groupControl3.TabIndex = 17;
            this.groupControl3.Text = "Okunan Satırlar";
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(13, 33);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_log.Size = new System.Drawing.Size(321, 407);
            this.txt_log.TabIndex = 1;
            // 
            // sname
            // 
            this.sname.FormattingEnabled = true;
            this.sname.Location = new System.Drawing.Point(112, 57);
            this.sname.Name = "sname";
            this.sname.Size = new System.Drawing.Size(152, 21);
            this.sname.TabIndex = 18;
            // 
            // excel_read_lpp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 613);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.data);
            this.Name = "excel_read_lpp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "excel_read_lpp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.excel_read_lpp_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_sec;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox lnumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_read;
        private System.Windows.Forms.DataGridView data;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox topsatsayi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dosyano;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.ComboBox sname;
    }
}