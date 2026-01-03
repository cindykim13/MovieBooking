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
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.picPoster = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnBuyTicket = new Guna.UI2.WinForms.Guna2Button();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 12;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.picPoster);
            this.guna2ShadowPanel1.Controls.Add(this.btnBuyTicket);
            this.guna2ShadowPanel1.Controls.Add(this.lblGenre);
            this.guna2ShadowPanel1.Controls.Add(this.lblTitle);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowDepth = 30;
            this.guna2ShadowPanel1.ShadowShift = 4;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(260, 460); // [SỬA] Tăng kích thước panel chứa
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // picPoster
            // 
            this.picPoster.BorderRadius = 8;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Top;
            this.picPoster.ImageRotate = 0F;
            this.picPoster.Location = new System.Drawing.Point(10, 10);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(240, 330); // [SỬA] Tăng chiều rộng ảnh
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 0;
            this.picPoster.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); // Tăng font
            this.lblTitle.Location = new System.Drawing.Point(10, 350); // [SỬA] Đẩy xuống dưới ảnh
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Tên Phim";
            // 
            // lblGenre
            // 
            this.lblGenre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGenre.ForeColor = System.Drawing.Color.Gray;
            this.lblGenre.Location = new System.Drawing.Point(10, 375); // [SỬA] Đẩy xuống dưới tên
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(240, 20);
            this.lblGenre.TabIndex = 2;
            this.lblGenre.Text = "Thể loại";
            // 
            // btnBuyTicket
            // 
            this.btnBuyTicket.BorderRadius = 6;
            this.btnBuyTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuyTicket.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBuyTicket.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBuyTicket.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBuyTicket.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBuyTicket.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnBuyTicket.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuyTicket.ForeColor = System.Drawing.Color.White;
            this.btnBuyTicket.Location = new System.Drawing.Point(10, 405); // [SỬA] Đẩy xuống đáy thẻ
            this.btnBuyTicket.Name = "btnBuyTicket";
            this.btnBuyTicket.Size = new System.Drawing.Size(240, 40); // Nút rộng bằng thẻ
            this.btnBuyTicket.TabIndex = 3;
            this.btnBuyTicket.Text = "MUA VÉ";
            // 
            // UCMovieCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Margin = new System.Windows.Forms.Padding(15); // [SỬA] Margin 15px mỗi bên (Tổng khoảng cách ngang = 30px)
            this.Name = "UCMovieCard";
            this.Size = new System.Drawing.Size(260, 460); // [SỬA] Kích thước UserControl khớp với Panel
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
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