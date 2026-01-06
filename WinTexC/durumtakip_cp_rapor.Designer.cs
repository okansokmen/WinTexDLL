namespace WinTexC
{
    partial class durumtakip_cp_rapor
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
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(durumtakip_cp_rapor));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.tar1 = new System.Windows.Forms.DateTimePicker();
            this.tar2 = new System.Windows.Forms.DateTimePicker();
            this.btn_msjgonder = new DevExpress.XtraEditors.SimpleButton();
            this.btn_oktarihinegore = new DevExpress.XtraEditors.SimpleButton();
            this.tar4 = new System.Windows.Forms.DateTimePicker();
            this.tar3 = new System.Windows.Forms.DateTimePicker();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.kayit_no = new System.Windows.Forms.Label();
            this.not_gonderi = new System.Windows.Forms.TextBox();
            this.txt_gonmails = new System.Windows.Forms.TextBox();
            this.btn_ok = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(-1, 120);
            this.gridControl1.MainView = this.gridView3;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1202, 489);
            this.gridControl1.TabIndex = 75;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsFind.AlwaysVisible = true;
            this.gridView3.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView3_RowCellStyle);
            this.gridView3.DoubleClick += new System.EventHandler(this.gridView3_DoubleClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Appearance.Options.UseTextOptions = true;
            this.simpleButton1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.simpleButton1.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.simpleButton1.AppearanceDisabled.Options.UseBackColor = true;
            this.simpleButton1.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.simpleButton1.AppearanceHovered.Options.UseBackColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(-1, 1);
            this.simpleButton1.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.simpleButton1.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.simpleButton1.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(1202, 32);
            toolTipItem4.Text = "Sipariş listesinden seçtiğiniz siparişin sipariş numarası yazmaktadır";
            superToolTip4.Items.Add(toolTipItem4);
            this.simpleButton1.SuperTip = superToolTip4;
            this.simpleButton1.TabIndex = 76;
            this.simpleButton1.Text = "Sipariş Zaman Çizelgesi (Critical Path)";
            // 
            // tar1
            // 
            this.tar1.Location = new System.Drawing.Point(12, 54);
            this.tar1.Name = "tar1";
            this.tar1.Size = new System.Drawing.Size(200, 20);
            this.tar1.TabIndex = 77;
            // 
            // tar2
            // 
            this.tar2.Location = new System.Drawing.Point(12, 79);
            this.tar2.Name = "tar2";
            this.tar2.Size = new System.Drawing.Size(200, 20);
            this.tar2.TabIndex = 78;
            // 
            // btn_msjgonder
            // 
            this.btn_msjgonder.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btn_msjgonder.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.btn_msjgonder.Appearance.Options.UseFont = true;
            this.btn_msjgonder.Appearance.Options.UseForeColor = true;
            this.btn_msjgonder.Appearance.Options.UseTextOptions = true;
            this.btn_msjgonder.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn_msjgonder.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.btn_msjgonder.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_msjgonder.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_msjgonder.AppearanceHovered.Options.UseBackColor = true;
            this.btn_msjgonder.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_msjgonder.ImageOptions.Image")));
            this.btn_msjgonder.Location = new System.Drawing.Point(218, 54);
            this.btn_msjgonder.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_msjgonder.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_msjgonder.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_msjgonder.Name = "btn_msjgonder";
            this.btn_msjgonder.Size = new System.Drawing.Size(153, 45);
            this.btn_msjgonder.TabIndex = 83;
            this.btn_msjgonder.Text = "Gönderi Tarihine Göre Filtrele";
            this.btn_msjgonder.Click += new System.EventHandler(this.btn_msjgonder_Click);
            // 
            // btn_oktarihinegore
            // 
            this.btn_oktarihinegore.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btn_oktarihinegore.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.btn_oktarihinegore.Appearance.Options.UseFont = true;
            this.btn_oktarihinegore.Appearance.Options.UseForeColor = true;
            this.btn_oktarihinegore.Appearance.Options.UseTextOptions = true;
            this.btn_oktarihinegore.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn_oktarihinegore.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.btn_oktarihinegore.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_oktarihinegore.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_oktarihinegore.AppearanceHovered.Options.UseBackColor = true;
            this.btn_oktarihinegore.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.btn_oktarihinegore.Location = new System.Drawing.Point(1066, 54);
            this.btn_oktarihinegore.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_oktarihinegore.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_oktarihinegore.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_oktarihinegore.Name = "btn_oktarihinegore";
            this.btn_oktarihinegore.Size = new System.Drawing.Size(125, 46);
            this.btn_oktarihinegore.TabIndex = 86;
            this.btn_oktarihinegore.Text = "OKEY Tarihine Göre Filtrele";
            this.btn_oktarihinegore.Click += new System.EventHandler(this.btn_oktarihinegore_Click);
            // 
            // tar4
            // 
            this.tar4.Location = new System.Drawing.Point(860, 80);
            this.tar4.Name = "tar4";
            this.tar4.Size = new System.Drawing.Size(200, 20);
            this.tar4.TabIndex = 85;
            // 
            // tar3
            // 
            this.tar3.Location = new System.Drawing.Point(860, 54);
            this.tar3.Name = "tar3";
            this.tar3.Size = new System.Drawing.Size(200, 20);
            this.tar3.TabIndex = 84;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btn_ok);
            this.groupControl1.Controls.Add(this.txt_gonmails);
            this.groupControl1.Controls.Add(this.not_gonderi);
            this.groupControl1.Controls.Add(this.kayit_no);
            this.groupControl1.Location = new System.Drawing.Point(377, 39);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(477, 75);
            this.groupControl1.TabIndex = 87;
            this.groupControl1.Text = "Uyarı Gönderim";
            // 
            // kayit_no
            // 
            this.kayit_no.Location = new System.Drawing.Point(5, 22);
            this.kayit_no.Name = "kayit_no";
            this.kayit_no.Size = new System.Drawing.Size(92, 26);
            this.kayit_no.TabIndex = 86;
            // 
            // not_gonderi
            // 
            this.not_gonderi.Location = new System.Drawing.Point(5, 45);
            this.not_gonderi.Multiline = true;
            this.not_gonderi.Name = "not_gonderi";
            this.not_gonderi.Size = new System.Drawing.Size(311, 25);
            this.not_gonderi.TabIndex = 87;
            // 
            // txt_gonmails
            // 
            this.txt_gonmails.Location = new System.Drawing.Point(103, 20);
            this.txt_gonmails.Multiline = true;
            this.txt_gonmails.Name = "txt_gonmails";
            this.txt_gonmails.Size = new System.Drawing.Size(213, 22);
            this.txt_gonmails.TabIndex = 88;
            // 
            // btn_ok
            // 
            this.btn_ok.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btn_ok.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.btn_ok.Appearance.Options.UseFont = true;
            this.btn_ok.Appearance.Options.UseForeColor = true;
            this.btn_ok.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.btn_ok.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_ok.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_ok.AppearanceHovered.Options.UseBackColor = true;
            this.btn_ok.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_ok.ImageOptions.Image")));
            this.btn_ok.Location = new System.Drawing.Point(322, 22);
            this.btn_ok.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_ok.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_ok.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(150, 48);
            this.btn_ok.TabIndex = 89;
            this.btn_ok.Text = "Mesaj Gönder";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // durumtakip_cp_rapor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 608);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btn_oktarihinegore);
            this.Controls.Add(this.tar4);
            this.Controls.Add(this.tar3);
            this.Controls.Add(this.btn_msjgonder);
            this.Controls.Add(this.tar2);
            this.Controls.Add(this.tar1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gridControl1);
            this.Name = "durumtakip_cp_rapor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cp Rapor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.durumtakip_cp_rapor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.DateTimePicker tar1;
        private System.Windows.Forms.DateTimePicker tar2;
        private DevExpress.XtraEditors.SimpleButton btn_msjgonder;
        private DevExpress.XtraEditors.SimpleButton btn_oktarihinegore;
        private System.Windows.Forms.DateTimePicker tar4;
        private System.Windows.Forms.DateTimePicker tar3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label kayit_no;
        private System.Windows.Forms.TextBox not_gonderi;
        public System.Windows.Forms.TextBox txt_gonmails;
        private DevExpress.XtraEditors.SimpleButton btn_ok;
    }
}