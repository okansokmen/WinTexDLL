namespace WinTexC
{
    partial class durumtakip_cp_musteri
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
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(durumtakip_cp_musteri));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.secili_musteri = new DevExpress.XtraEditors.SimpleButton();
            this.btn_msjgonder = new DevExpress.XtraEditors.SimpleButton();
            this.btn_yenisatir = new DevExpress.XtraEditors.SimpleButton();
            this.btn_satirsil = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 41);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1116, 453);
            this.gridControl1.TabIndex = 75;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView3_RowUpdated);
            // 
            // simpleButton1
            // 
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
            this.simpleButton1.Location = new System.Drawing.Point(6, 3);
            this.simpleButton1.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.simpleButton1.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.simpleButton1.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(485, 32);
            toolTipItem5.Text = "Sipariş listesinden seçtiğiniz siparişin sipariş numarası yazmaktadır";
            superToolTip5.Items.Add(toolTipItem5);
            this.simpleButton1.SuperTip = superToolTip5;
            this.simpleButton1.TabIndex = 77;
            this.simpleButton1.Text = "Müşteri bazlı CP sabitleri";
            // 
            // secili_musteri
            // 
            this.secili_musteri.Appearance.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.secili_musteri.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.secili_musteri.Appearance.Options.UseFont = true;
            this.secili_musteri.Appearance.Options.UseForeColor = true;
            this.secili_musteri.Appearance.Options.UseTextOptions = true;
            this.secili_musteri.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.secili_musteri.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.secili_musteri.AppearanceDisabled.Options.UseBackColor = true;
            this.secili_musteri.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.secili_musteri.AppearanceHovered.Options.UseBackColor = true;
            this.secili_musteri.Location = new System.Drawing.Point(501, 3);
            this.secili_musteri.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.secili_musteri.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.secili_musteri.LookAndFeel.SkinName = "Office 2010 Blue";
            this.secili_musteri.Name = "secili_musteri";
            this.secili_musteri.Size = new System.Drawing.Size(136, 32);
            toolTipItem6.Text = "Sipariş listesinden seçtiğiniz siparişin sipariş numarası yazmaktadır";
            superToolTip6.Items.Add(toolTipItem6);
            this.secili_musteri.SuperTip = superToolTip6;
            this.secili_musteri.TabIndex = 76;
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
            this.btn_msjgonder.Location = new System.Drawing.Point(974, 3);
            this.btn_msjgonder.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_msjgonder.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_msjgonder.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_msjgonder.Name = "btn_msjgonder";
            this.btn_msjgonder.Size = new System.Drawing.Size(142, 32);
            this.btn_msjgonder.TabIndex = 83;
            this.btn_msjgonder.Text = "Hepsini Kaydet";
            this.btn_msjgonder.Click += new System.EventHandler(this.btn_msjgonder_Click);
            // 
            // btn_yenisatir
            // 
            this.btn_yenisatir.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btn_yenisatir.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.btn_yenisatir.Appearance.Options.UseFont = true;
            this.btn_yenisatir.Appearance.Options.UseForeColor = true;
            this.btn_yenisatir.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.btn_yenisatir.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_yenisatir.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_yenisatir.AppearanceHovered.Options.UseBackColor = true;
            this.btn_yenisatir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.btn_yenisatir.Location = new System.Drawing.Point(811, 3);
            this.btn_yenisatir.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_yenisatir.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_yenisatir.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_yenisatir.Name = "btn_yenisatir";
            this.btn_yenisatir.Size = new System.Drawing.Size(131, 32);
            this.btn_yenisatir.TabIndex = 84;
            this.btn_yenisatir.Text = "Yeni Satır Ekle";
            this.btn_yenisatir.Click += new System.EventHandler(this.btn_yenisatir_Click);
            // 
            // btn_satirsil
            // 
            this.btn_satirsil.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btn_satirsil.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.btn_satirsil.Appearance.Options.UseFont = true;
            this.btn_satirsil.Appearance.Options.UseForeColor = true;
            this.btn_satirsil.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.btn_satirsil.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_satirsil.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_satirsil.AppearanceHovered.Options.UseBackColor = true;
            this.btn_satirsil.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.btn_satirsil.Location = new System.Drawing.Point(652, 3);
            this.btn_satirsil.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.btn_satirsil.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.btn_satirsil.LookAndFeel.SkinName = "Office 2010 Blue";
            this.btn_satirsil.Name = "btn_satirsil";
            this.btn_satirsil.Size = new System.Drawing.Size(138, 32);
            this.btn_satirsil.TabIndex = 85;
            this.btn_satirsil.Text = "Seçili Satırı Sil";
            this.btn_satirsil.Click += new System.EventHandler(this.btn_satirsil_Click);
            // 
            // durumtakip_cp_musteri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 496);
            this.Controls.Add(this.btn_satirsil);
            this.Controls.Add(this.btn_yenisatir);
            this.Controls.Add(this.btn_msjgonder);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.secili_musteri);
            this.Controls.Add(this.gridControl1);
            this.Name = "durumtakip_cp_musteri";
            this.Text = "durumtakip_cp_musteri";
            this.Load += new System.EventHandler(this.durumtakip_cp_musteri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.SimpleButton secili_musteri;
        private DevExpress.XtraEditors.SimpleButton btn_msjgonder;
        private DevExpress.XtraEditors.SimpleButton btn_yenisatir;
        private DevExpress.XtraEditors.SimpleButton btn_satirsil;
    }
}