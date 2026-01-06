namespace WinTexC
{
    partial class durumtakip_dosyapaneli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(durumtakip_dosyapaneli));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.data = new System.Windows.Forms.DataGridView();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dsil = new DevExpress.XtraEditors.SimpleButton();
            this.dac = new DevExpress.XtraEditors.SimpleButton();
            this.did = new System.Windows.Forms.Label();
            this.dadi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dacik = new System.Windows.Forms.RichTextBox();
            this.dosya_sec = new DevExpress.XtraEditors.SimpleButton();
            this.file = new System.Windows.Forms.TextBox();
            this.btn_msjgonder = new DevExpress.XtraEditors.SimpleButton();
            this.txt_bildirimadet = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.secili_siparis = new DevExpress.XtraEditors.SimpleButton();
            this.txt_gonmails = new System.Windows.Forms.TextBox();
            this.secili_modeladi = new System.Windows.Forms.TextBox();
            this.mailcheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dadi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // data
            // 
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(12, 12);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(745, 337);
            this.data.TabIndex = 0;
            this.data.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.data_RowHeaderMouseDoubleClick);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.mailcheck);
            this.groupControl1.Controls.Add(this.dsil);
            this.groupControl1.Controls.Add(this.dac);
            this.groupControl1.Controls.Add(this.did);
            this.groupControl1.Controls.Add(this.dadi);
            this.groupControl1.Controls.Add(this.simpleButton4);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.dacik);
            this.groupControl1.Controls.Add(this.dosya_sec);
            this.groupControl1.Controls.Add(this.file);
            this.groupControl1.Controls.Add(this.btn_msjgonder);
            this.groupControl1.Location = new System.Drawing.Point(12, 355);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(745, 160);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Dosya İşlemleri";
            // 
            // dsil
            // 
            this.dsil.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.dsil.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.dsil.Appearance.Options.UseFont = true;
            this.dsil.Appearance.Options.UseForeColor = true;
            this.dsil.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.dsil.AppearanceDisabled.Options.UseBackColor = true;
            this.dsil.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dsil.AppearanceHovered.Options.UseBackColor = true;
            this.dsil.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dsil.ImageOptions.Image")));
            this.dsil.Location = new System.Drawing.Point(575, 123);
            this.dsil.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dsil.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.dsil.LookAndFeel.SkinName = "Office 2010 Blue";
            this.dsil.Name = "dsil";
            this.dsil.Size = new System.Drawing.Size(165, 32);
            this.dsil.TabIndex = 79;
            this.dsil.Text = "Seçili Dosyayı SİL";
            this.dsil.Click += new System.EventHandler(this.dsil_Click);
            // 
            // dac
            // 
            this.dac.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.dac.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.dac.Appearance.Options.UseFont = true;
            this.dac.Appearance.Options.UseForeColor = true;
            this.dac.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.dac.AppearanceDisabled.Options.UseBackColor = true;
            this.dac.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dac.AppearanceHovered.Options.UseBackColor = true;
            this.dac.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dac.ImageOptions.Image")));
            this.dac.Location = new System.Drawing.Point(575, 26);
            this.dac.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dac.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.dac.LookAndFeel.SkinName = "Office 2010 Blue";
            this.dac.Name = "dac";
            this.dac.Size = new System.Drawing.Size(165, 32);
            this.dac.TabIndex = 77;
            this.dac.Text = "Seçili dosyayı AÇ";
            this.dac.Click += new System.EventHandler(this.dac_Click);
            // 
            // did
            // 
            this.did.AutoSize = true;
            this.did.Location = new System.Drawing.Point(693, 26);
            this.did.Name = "did";
            this.did.Size = new System.Drawing.Size(0, 13);
            this.did.TabIndex = 76;
            // 
            // dadi
            // 
            this.dadi.Location = new System.Drawing.Point(152, 63);
            this.dadi.Name = "dadi";
            this.dadi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dadi.Size = new System.Drawing.Size(251, 20);
            this.dadi.TabIndex = 75;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.simpleButton4.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Appearance.Options.UseForeColor = true;
            this.simpleButton4.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.simpleButton4.AppearanceDisabled.Options.UseBackColor = true;
            this.simpleButton4.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.simpleButton4.AppearanceHovered.Options.UseBackColor = true;
            this.simpleButton4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
            this.simpleButton4.Location = new System.Drawing.Point(5, 61);
            this.simpleButton4.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.simpleButton4.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.simpleButton4.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(142, 32);
            this.simpleButton4.TabIndex = 74;
            this.simpleButton4.Text = "Dosya Adı";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.simpleButton1.AppearanceDisabled.Options.UseBackColor = true;
            this.simpleButton1.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.simpleButton1.AppearanceHovered.Options.UseBackColor = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(5, 101);
            this.simpleButton1.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.simpleButton1.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.simpleButton1.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(142, 32);
            this.simpleButton1.TabIndex = 73;
            this.simpleButton1.Text = "Açıklama(Varsa)";
            // 
            // dacik
            // 
            this.dacik.Location = new System.Drawing.Point(152, 101);
            this.dacik.Name = "dacik";
            this.dacik.Size = new System.Drawing.Size(251, 59);
            this.dacik.TabIndex = 72;
            this.dacik.Text = "";
            // 
            // dosya_sec
            // 
            this.dosya_sec.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.dosya_sec.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.dosya_sec.Appearance.Options.UseFont = true;
            this.dosya_sec.Appearance.Options.UseForeColor = true;
            this.dosya_sec.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.dosya_sec.AppearanceDisabled.Options.UseBackColor = true;
            this.dosya_sec.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dosya_sec.AppearanceHovered.Options.UseBackColor = true;
            this.dosya_sec.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dosya_sec.ImageOptions.Image")));
            this.dosya_sec.Location = new System.Drawing.Point(4, 23);
            this.dosya_sec.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dosya_sec.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.dosya_sec.LookAndFeel.SkinName = "Office 2010 Blue";
            this.dosya_sec.Name = "dosya_sec";
            this.dosya_sec.Size = new System.Drawing.Size(142, 32);
            this.dosya_sec.TabIndex = 71;
            this.dosya_sec.Text = "Eklenecek Dosya";
            this.dosya_sec.Click += new System.EventHandler(this.dosya_sec_Click);
            // 
            // file
            // 
            this.file.Location = new System.Drawing.Point(152, 23);
            this.file.Multiline = true;
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(251, 32);
            this.file.TabIndex = 70;
            // 
            // btn_msjgonder
            // 
            this.btn_msjgonder.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btn_msjgonder.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.btn_msjgonder.Appearance.Options.UseFont = true;
            this.btn_msjgonder.Appearance.Options.UseForeColor = true;
            this.btn_msjgonder.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.btn_msjgonder.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_msjgonder.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_msjgonder.AppearanceHovered.Options.UseBackColor = true;
            this.btn_msjgonder.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_msjgonder.ImageOptions.Image")));
            this.btn_msjgonder.Location = new System.Drawing.Point(409, 57);
            this.btn_msjgonder.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_msjgonder.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_msjgonder.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_msjgonder.Name = "btn_msjgonder";
            this.btn_msjgonder.Size = new System.Drawing.Size(142, 32);
            this.btn_msjgonder.TabIndex = 9;
            this.btn_msjgonder.Text = "Kaydet";
            this.btn_msjgonder.Click += new System.EventHandler(this.btn_msjgonder_Click);
            // 
            // txt_bildirimadet
            // 
            this.txt_bildirimadet.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.txt_bildirimadet.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.txt_bildirimadet.Appearance.Options.UseFont = true;
            this.txt_bildirimadet.Appearance.Options.UseForeColor = true;
            this.txt_bildirimadet.Appearance.Options.UseTextOptions = true;
            this.txt_bildirimadet.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.txt_bildirimadet.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.txt_bildirimadet.AppearanceDisabled.Options.UseBackColor = true;
            this.txt_bildirimadet.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txt_bildirimadet.AppearanceHovered.Options.UseBackColor = true;
            this.txt_bildirimadet.Location = new System.Drawing.Point(668, 521);
            this.txt_bildirimadet.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.txt_bildirimadet.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.txt_bildirimadet.LookAndFeel.SkinName = "Office 2010 Blue";
            this.txt_bildirimadet.Name = "txt_bildirimadet";
            this.txt_bildirimadet.Size = new System.Drawing.Size(89, 32);
            this.txt_bildirimadet.TabIndex = 68;
            this.txt_bildirimadet.Text = "0";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.simpleButton3.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Appearance.Options.UseForeColor = true;
            this.simpleButton3.Appearance.Options.UseTextOptions = true;
            this.simpleButton3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.simpleButton3.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.simpleButton3.AppearanceDisabled.Options.UseBackColor = true;
            this.simpleButton3.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.simpleButton3.AppearanceHovered.Options.UseBackColor = true;
            this.simpleButton3.Location = new System.Drawing.Point(478, 521);
            this.simpleButton3.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.simpleButton3.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.simpleButton3.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(184, 32);
            this.simpleButton3.TabIndex = 67;
            this.simpleButton3.Text = "Bildirim Yapılacak Kişi Sayısı: ";
            // 
            // secili_siparis
            // 
            this.secili_siparis.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.secili_siparis.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.secili_siparis.Appearance.Options.UseFont = true;
            this.secili_siparis.Appearance.Options.UseForeColor = true;
            this.secili_siparis.Appearance.Options.UseTextOptions = true;
            this.secili_siparis.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.secili_siparis.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.secili_siparis.AppearanceDisabled.Options.UseBackColor = true;
            this.secili_siparis.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.secili_siparis.AppearanceHovered.Options.UseBackColor = true;
            this.secili_siparis.Location = new System.Drawing.Point(16, 521);
            this.secili_siparis.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.secili_siparis.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.secili_siparis.LookAndFeel.SkinName = "Office 2010 Blue";
            this.secili_siparis.Name = "secili_siparis";
            this.secili_siparis.Size = new System.Drawing.Size(261, 32);
            toolTipItem1.Text = "Sipariş listesinden seçtiğiniz siparişin sipariş numarası yazmaktadır";
            superToolTip1.Items.Add(toolTipItem1);
            this.secili_siparis.SuperTip = superToolTip1;
            this.secili_siparis.TabIndex = 69;
            // 
            // txt_gonmails
            // 
            this.txt_gonmails.Enabled = false;
            this.txt_gonmails.Location = new System.Drawing.Point(283, 521);
            this.txt_gonmails.Multiline = true;
            this.txt_gonmails.Name = "txt_gonmails";
            this.txt_gonmails.Size = new System.Drawing.Size(189, 44);
            this.txt_gonmails.TabIndex = 70;
            // 
            // secili_modeladi
            // 
            this.secili_modeladi.Location = new System.Drawing.Point(228, 527);
            this.secili_modeladi.Name = "secili_modeladi";
            this.secili_modeladi.Size = new System.Drawing.Size(35, 20);
            this.secili_modeladi.TabIndex = 86;
            this.secili_modeladi.Visible = false;
            // 
            // mailcheck
            // 
            this.mailcheck.AutoSize = true;
            this.mailcheck.Checked = true;
            this.mailcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mailcheck.Location = new System.Drawing.Point(409, 31);
            this.mailcheck.Name = "mailcheck";
            this.mailcheck.Size = new System.Drawing.Size(155, 17);
            this.mailcheck.TabIndex = 80;
            this.mailcheck.Text = "Gönderilecek Maile Eklensin";
            this.mailcheck.UseVisualStyleBackColor = true;
            // 
            // durumtakip_dosyapaneli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 565);
            this.Controls.Add(this.secili_modeladi);
            this.Controls.Add(this.txt_gonmails);
            this.Controls.Add(this.secili_siparis);
            this.Controls.Add(this.txt_bildirimadet);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.data);
            this.Name = "durumtakip_dosyapaneli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dosya İşlemleri";
            this.Load += new System.EventHandler(this.durumtakip_dosyapaneli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dadi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView data;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btn_msjgonder;
        private DevExpress.XtraEditors.SimpleButton dosya_sec;
        private System.Windows.Forms.TextBox file;
        public DevExpress.XtraEditors.SimpleButton txt_bildirimadet;
        public DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.ComboBoxEdit dadi;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RichTextBox dacik;
        public DevExpress.XtraEditors.SimpleButton secili_siparis;
        private System.Windows.Forms.Label did;
        private DevExpress.XtraEditors.SimpleButton dsil;
        private DevExpress.XtraEditors.SimpleButton dac;
        public System.Windows.Forms.TextBox txt_gonmails;
        public System.Windows.Forms.TextBox secili_modeladi;
        private System.Windows.Forms.CheckBox mailcheck;
    }
}