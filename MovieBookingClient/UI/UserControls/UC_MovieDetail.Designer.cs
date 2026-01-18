using Guna.UI2.WinForms;

namespace MovieBookingClient.UI.UserControls
{
    partial class UC_MovieDetail
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnBack = new Guna2Button();
            guna2Elipse1 = new Guna2Elipse(components);
            pnlContainer = new Panel();
            pnlContent = new Guna2Panel();
            btnBuyTicket = new Guna2Button();
            lblStoryLine = new Label();
            lblCast = new Label();
            lblDirector = new Label();
            lblDuration = new Label();
            lblGenre = new Label();
            lblTitle = new Label();
            picPoster = new Guna2PictureBox();
            pnlContainer.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPoster).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.BorderRadius = 4;
            btnBack.Cursor = Cursors.Hand;
            btnBack.CustomizableEdges = customizableEdges1;
            btnBack.FillColor = Color.Transparent;
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            btnBack.ForeColor = Color.DimGray;
            btnBack.Location = new Point(50, 10);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new Size(126, 30);
            btnBack.TabIndex = 8;
            btnBack.Text = "< Quay lại";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.TargetControl = this;
            // 
            // pnlContainer
            // 
            pnlContainer.AutoScroll = true;
            pnlContainer.BackColor = Color.LightCoral;
            pnlContainer.Controls.Add(pnlContent);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1200, 700);
            pnlContainer.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(btnBuyTicket);
            pnlContent.Controls.Add(lblStoryLine);
            pnlContent.Controls.Add(lblCast);
            pnlContent.Controls.Add(lblDirector);
            pnlContent.Controls.Add(lblDuration);
            pnlContent.Controls.Add(lblGenre);
            pnlContent.Controls.Add(lblTitle);
            pnlContent.Controls.Add(picPoster);
            pnlContent.Controls.Add(btnBack);
            pnlContent.CustomizableEdges = customizableEdges7;
            pnlContent.Dock = DockStyle.Top;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlContent.Size = new Size(1200, 600);
            pnlContent.TabIndex = 0;
            // 
            // btnBuyTicket
            // 
            btnBuyTicket.BorderRadius = 5;
            btnBuyTicket.CustomizableEdges = customizableEdges3;
            btnBuyTicket.FillColor = Color.FromArgb(212, 33, 33);
            btnBuyTicket.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBuyTicket.ForeColor = Color.White;
            btnBuyTicket.Location = new Point(380, 440);
            btnBuyTicket.Name = "btnBuyTicket";
            btnBuyTicket.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnBuyTicket.Size = new Size(180, 35);
            btnBuyTicket.TabIndex = 7;
            btnBuyTicket.Text = "MUA VÉ NGAY";
            // 
            // lblStoryLine
            // 
            lblStoryLine.Font = new Font("Segoe UI", 12F);
            lblStoryLine.Location = new Point(380, 270);
            lblStoryLine.Name = "lblStoryLine";
            lblStoryLine.Size = new Size(750, 150);
            lblStoryLine.TabIndex = 6;
            lblStoryLine.Text = "...";
            // 
            // lblCast
            // 
            lblCast.Font = new Font("Segoe UI", 12F);
            lblCast.Location = new Point(380, 220);
            lblCast.Name = "lblCast";
            lblCast.Size = new Size(750, 50);
            lblCast.TabIndex = 5;
            lblCast.Text = "...";
            // 
            // lblDirector
            // 
            lblDirector.Font = new Font("Segoe UI", 12F);
            lblDirector.Location = new Point(380, 180);
            lblDirector.Name = "lblDirector";
            lblDirector.Size = new Size(750, 37);
            lblDirector.TabIndex = 4;
            lblDirector.Text = "...";
            // 
            // lblDuration
            // 
            lblDuration.Font = new Font("Segoe UI", 12F);
            lblDuration.Location = new Point(380, 150);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(750, 37);
            lblDuration.TabIndex = 3;
            lblDuration.Text = "...";
            // 
            // lblGenre
            // 
            lblGenre.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblGenre.ForeColor = Color.DimGray;
            lblGenre.Location = new Point(380, 110);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(750, 40);
            lblGenre.TabIndex = 2;
            lblGenre.Text = "...";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(212, 33, 33);
            lblTitle.Location = new Point(380, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 70);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Loading...";
            // 
            // picPoster
            // 
            picPoster.BorderRadius = 10;
            picPoster.CustomizableEdges = customizableEdges5;
            picPoster.ImageRotate = 0F;
            picPoster.Location = new Point(50, 40);
            picPoster.Name = "picPoster";
            picPoster.ShadowDecoration.CustomizableEdges = customizableEdges6;
            picPoster.Size = new Size(300, 450);
            picPoster.SizeMode = PictureBoxSizeMode.StretchImage;
            picPoster.TabIndex = 0;
            picPoster.TabStop = false;
            // 
            // UC_MovieDetail
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(pnlContainer);
            Name = "UC_MovieDetail";
            Size = new Size(1200, 700);
            pnlContainer.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPoster).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Panel pnlContainer;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2PictureBox picPoster;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.Label lblCast;
        private System.Windows.Forms.Label lblStoryLine;
        private Guna.UI2.WinForms.Guna2Button btnBuyTicket;
        private Guna2Button btnBack;
    }
}