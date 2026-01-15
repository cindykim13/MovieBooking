namespace MovieBookingClient.UI.UserControls
{
    partial class UCMovieCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            picPoster = new Guna.UI2.WinForms.Guna2PictureBox();
            btnBuyTicket = new Guna.UI2.WinForms.Guna2Button();
            lblGenre = new Label();
            lblTitle = new Label();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPoster).BeginInit();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 12;
            guna2Elipse1.TargetControl = this;
            // 
            // guna2ShadowPanel1
            // 
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(picPoster);
            guna2ShadowPanel1.Controls.Add(btnBuyTicket);
            guna2ShadowPanel1.Controls.Add(lblGenre);
            guna2ShadowPanel1.Controls.Add(lblTitle);
            guna2ShadowPanel1.Dock = DockStyle.Fill;
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(0, 0);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Padding = new Padding(10);
            guna2ShadowPanel1.ShadowColor = Color.Black;
            guna2ShadowPanel1.ShadowDepth = 30;
            guna2ShadowPanel1.ShadowShift = 4;
            guna2ShadowPanel1.Size = new Size(260, 460);
            guna2ShadowPanel1.TabIndex = 0;
            // 
            // picPoster
            // 
            picPoster.BorderRadius = 8;
            picPoster.CustomizableEdges = customizableEdges1;
            picPoster.Dock = DockStyle.Top;
            picPoster.ImageRotate = 0F;
            picPoster.Location = new Point(10, 10);
            picPoster.Name = "picPoster";
            picPoster.ShadowDecoration.CustomizableEdges = customizableEdges2;
            picPoster.Size = new Size(240, 340);
            picPoster.SizeMode = PictureBoxSizeMode.StretchImage;
            picPoster.TabIndex = 0;
            picPoster.TabStop = false;
            // 
            // btnBuyTicket
            // 
            btnBuyTicket.BorderRadius = 6;
            btnBuyTicket.Cursor = Cursors.Hand;
            btnBuyTicket.CustomizableEdges = customizableEdges3;
            btnBuyTicket.DisabledState.BorderColor = Color.DarkGray;
            btnBuyTicket.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBuyTicket.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBuyTicket.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBuyTicket.FillColor = Color.FromArgb(212, 33, 33);
            btnBuyTicket.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBuyTicket.ForeColor = Color.White;
            btnBuyTicket.Location = new Point(10, 405);
            btnBuyTicket.Name = "btnBuyTicket";
            btnBuyTicket.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnBuyTicket.Size = new Size(240, 40);
            btnBuyTicket.TabIndex = 3;
            btnBuyTicket.Text = "MUA VÉ";
            // 
            // lblGenre
            // 
            lblGenre.Font = new Font("Segoe UI", 9F);
            lblGenre.ForeColor = Color.Gray;
            lblGenre.Location = new Point(10, 379);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(240, 23);
            lblGenre.TabIndex = 2;
            lblGenre.Text = "Thể loại";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 353);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Tên Phim";
            // 
            // UCMovieCard
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Transparent;
            Controls.Add(guna2ShadowPanel1);
            Margin = new Padding(10);
            Name = "UCMovieCard";
            Size = new Size(260, 460);
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPoster).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2PictureBox picPoster;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGenre;
        private Guna.UI2.WinForms.Guna2Button btnBuyTicket;
    }
}