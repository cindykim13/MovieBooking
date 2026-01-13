namespace MovieBookingClient.UI.Forms
{
    partial class FrmTicketDetail
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
            this.picPoster = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.lblCinema = new System.Windows.Forms.Label();
            this.lblShowTime = new System.Windows.Forms.Label();
            this.lblSeats = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblStatus = new Guna.UI2.WinForms.Guna2Chip();
            this.lblPayment = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();

            // 
            // picPoster
            // 
            this.picPoster.BorderRadius = 5;
            this.picPoster.FillColor = System.Drawing.Color.LightGray;
            this.picPoster.ImageRotate = 0F;
            this.picPoster.Location = new System.Drawing.Point(20, 20);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(120, 180);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 0;
            this.picPoster.TabStop = false;

            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMovieTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblMovieTitle.Location = new System.Drawing.Point(150, 20);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(230, 60);
            this.lblMovieTitle.TabIndex = 1;
            this.lblMovieTitle.Text = "Tên Phim";

            // 
            // lblCinema
            // 
            this.lblCinema.AutoSize = true;
            this.lblCinema.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCinema.Location = new System.Drawing.Point(20, 220);
            this.lblCinema.Name = "lblCinema";
            this.lblCinema.TabIndex = 2;
            this.lblCinema.Text = "Rạp:";

            // 
            // lblShowTime
            // 
            this.lblShowTime.AutoSize = true;
            this.lblShowTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblShowTime.Location = new System.Drawing.Point(20, 250);
            this.lblShowTime.Name = "lblShowTime";
            this.lblShowTime.TabIndex = 3;
            this.lblShowTime.Text = "Suất chiếu:";

            // 
            // lblSeats
            // 
            this.lblSeats.AutoSize = true;
            this.lblSeats.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeats.Location = new System.Drawing.Point(20, 280);
            this.lblSeats.Name = "lblSeats";
            this.lblSeats.TabIndex = 4;
            this.lblSeats.Text = "Ghế:";

            // 
            // lblPayment
            // 
            this.lblPayment.AutoSize = true;
            this.lblPayment.Location = new System.Drawing.Point(20, 310);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.TabIndex = 7;
            this.lblPayment.Text = "Thanh toán:";

            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.Green;
            this.lblPrice.Location = new System.Drawing.Point(20, 350);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "0 VNĐ";

            // 
            // lblStatus
            // 
            this.lblStatus.FillColor = System.Drawing.Color.Orange;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(250, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(130, 30);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Confirmed";

            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 5;
            this.btnClose.FillColor = System.Drawing.Color.Gray;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(100, 480);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 40);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // FrmTicketDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 550);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblSeats);
            this.Controls.Add(this.lblShowTime);
            this.Controls.Add(this.lblCinema);
            this.Controls.Add(this.lblMovieTitle);
            this.Controls.Add(this.picPoster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTicketDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmTicketDetail";
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

