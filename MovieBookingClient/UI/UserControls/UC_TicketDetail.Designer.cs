namespace MovieBookingClient.UI.Forms
{
    partial class TicketDetail
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo các controls
        private Guna.UI2.WinForms.Guna2PictureBox picPoster;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblCinema;
        private System.Windows.Forms.Label lblShowTime;
        private System.Windows.Forms.Label lblSeats;
        private System.Windows.Forms.Label lblPrice;
        private Guna.UI2.WinForms.Guna2Chip lblStatus;
        private System.Windows.Forms.Label lblPayment;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            picPoster = new Guna.UI2.WinForms.Guna2PictureBox();
            lblMovieTitle = new Label();
            lblCinema = new Label();
            lblShowTime = new Label();
            lblSeats = new Label();
            lblPrice = new Label();
            lblStatus = new Guna.UI2.WinForms.Guna2Chip();
            lblPayment = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)picPoster).BeginInit();
            SuspendLayout();
            // 
            // picPoster
            // 
            picPoster.BorderRadius = 5;
            picPoster.CustomizableEdges = customizableEdges1;
            picPoster.FillColor = Color.LightGray;
            picPoster.ImageRotate = 0F;
            picPoster.Location = new Point(33, 38);
            picPoster.Margin = new Padding(5, 6, 5, 6);
            picPoster.Name = "picPoster";
            picPoster.ShadowDecoration.CustomizableEdges = customizableEdges2;
            picPoster.Size = new Size(240, 346);
            picPoster.SizeMode = PictureBoxSizeMode.StretchImage;
            picPoster.TabIndex = 0;
            picPoster.TabStop = false;
            // 
            // lblMovieTitle
            // 
            lblMovieTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMovieTitle.ForeColor = Color.FromArgb(212, 33, 33);
            lblMovieTitle.Location = new Point(283, 50);
            lblMovieTitle.Margin = new Padding(5, 0, 5, 0);
            lblMovieTitle.Name = "lblMovieTitle";
            lblMovieTitle.Size = new Size(356, 115);
            lblMovieTitle.TabIndex = 1;
            lblMovieTitle.Text = "Tên Phim";
            // 
            // lblCinema
            // 
            lblCinema.AutoSize = true;
            lblCinema.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCinema.Location = new Point(33, 423);
            lblCinema.Margin = new Padding(5, 0, 5, 0);
            lblCinema.Name = "lblCinema";
            lblCinema.Size = new Size(65, 32);
            lblCinema.TabIndex = 2;
            lblCinema.Text = "Rạp:";
            // 
            // lblShowTime
            // 
            lblShowTime.AutoSize = true;
            lblShowTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblShowTime.Location = new Point(33, 481);
            lblShowTime.Margin = new Padding(5, 0, 5, 0);
            lblShowTime.Name = "lblShowTime";
            lblShowTime.Size = new Size(139, 32);
            lblShowTime.TabIndex = 3;
            lblShowTime.Text = "Suất chiếu:";
            // 
            // lblSeats
            // 
            lblSeats.AutoSize = true;
            lblSeats.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSeats.Location = new Point(33, 538);
            lblSeats.Margin = new Padding(5, 0, 5, 0);
            lblSeats.Name = "lblSeats";
            lblSeats.Size = new Size(65, 32);
            lblSeats.TabIndex = 4;
            lblSeats.Text = "Ghế:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPrice.ForeColor = Color.OrangeRed;
            lblPrice.Location = new Point(33, 677);
            lblPrice.Margin = new Padding(5, 0, 5, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(117, 45);
            lblPrice.TabIndex = 5;
            lblPrice.Text = "0 VNĐ";
            // 
            // lblStatus
            // 
            lblStatus.CustomizableEdges = customizableEdges3;
            lblStatus.FillColor = Color.OrangeRed;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(283, 326);
            lblStatus.Margin = new Padding(5, 6, 5, 6);
            lblStatus.Name = "lblStatus";
            lblStatus.ShadowDecoration.CustomizableEdges = customizableEdges4;
            lblStatus.Size = new Size(350, 58);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Confirmed";
            // 
            // lblPayment
            // 
            lblPayment.AutoSize = true;
            lblPayment.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPayment.Location = new Point(33, 596);
            lblPayment.Margin = new Padding(5, 0, 5, 0);
            lblPayment.Name = "lblPayment";
            lblPayment.Size = new Size(300, 32);
            lblPayment.TabIndex = 7;
            lblPayment.Text = "Phương thức thanh toán:";
            // 
            // btnClose
            // 
            btnClose.BorderRadius = 5;
            btnClose.CustomizableEdges = customizableEdges5;
            btnClose.FillColor = Color.Gray;
            btnClose.Font = new Font("Segoe UI", 9F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(613, 6);
            btnClose.Margin = new Padding(5, 6, 5, 6);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnClose.Size = new Size(35, 38);
            btnClose.TabIndex = 8;
            btnClose.Text = "X";
            btnClose.Click += btnClose_Click;
            // 
            // TicketDetail
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(653, 764);
            Controls.Add(btnClose);
            Controls.Add(lblPayment);
            Controls.Add(lblStatus);
            Controls.Add(lblPrice);
            Controls.Add(lblSeats);
            Controls.Add(lblShowTime);
            Controls.Add(lblCinema);
            Controls.Add(lblMovieTitle);
            Controls.Add(picPoster);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 6, 5, 6);
            Name = "TicketDetail";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FrmTicketDetail";
            ((System.ComponentModel.ISupportInitialize)picPoster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
