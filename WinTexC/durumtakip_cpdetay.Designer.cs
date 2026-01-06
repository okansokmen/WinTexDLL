namespace WinTexC
{
    partial class durumtakip_cpdetay
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
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            this.grid_cp = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.secili_siparis = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid_cp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid_cp
            // 
            this.grid_cp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_cp.Location = new System.Drawing.Point(2, 42);
            this.grid_cp.MainView = this.gridView1;
            this.grid_cp.Name = "grid_cp";
            this.grid_cp.Size = new System.Drawing.Size(886, 342);
            this.grid_cp.TabIndex = 0;
            this.grid_cp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grid_cp;
            this.gridView1.Name = "gridView1";
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
            this.secili_siparis.Location = new System.Drawing.Point(601, 4);
            this.secili_siparis.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.secili_siparis.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.secili_siparis.LookAndFeel.SkinName = "Office 2010 Blue";
            this.secili_siparis.Name = "secili_siparis";
            this.secili_siparis.Size = new System.Drawing.Size(278, 32);
            toolTipItem3.Text = "Sipariş listesinden seçtiğiniz siparişin sipariş numarası yazmaktadır";
            superToolTip3.Items.Add(toolTipItem3);
            this.secili_siparis.SuperTip = superToolTip3;
            this.secili_siparis.TabIndex = 65;
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
            this.simpleButton1.Location = new System.Drawing.Point(2, 4);
            this.simpleButton1.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.simpleButton1.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Fuchsia;
            this.simpleButton1.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(485, 32);
            toolTipItem4.Text = "Sipariş listesinden seçtiğiniz siparişin sipariş numarası yazmaktadır";
            superToolTip4.Items.Add(toolTipItem4);
            this.simpleButton1.SuperTip = superToolTip4;
            this.simpleButton1.TabIndex = 66;
            this.simpleButton1.Text = "Sipariş Zaman Çizelgesi (Critical Path)";
            // 
            // durumtakip_cpdetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 465);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.secili_siparis);
            this.Controls.Add(this.grid_cp);
            this.Name = "durumtakip_cpdetay";
            this.Text = "Sipariş Zaman Çizelgesi";
            this.Load += new System.EventHandler(this.durumtakip_cpdetay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_cp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid_cp;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraEditors.SimpleButton secili_siparis;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}