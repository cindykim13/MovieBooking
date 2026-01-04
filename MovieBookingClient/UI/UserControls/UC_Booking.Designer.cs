namespace MovieBookingClient.UI.UserControls
{
    partial class UC_Booking
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Panel();
            pnlSeatMapContainer = new Panel();
            pnlSeatMap = new Panel();
            lblScreen = new Label();
            panelScreenLine = new Guna.UI2.WinForms.Guna2Panel();
            pnlNote = new Panel();
            lblNoteSold = new Label();
            boxSold = new Panel();
            lblNoteSelecting = new Label();
            boxSelecting = new Panel();
            lblNoteSweetbox = new Label();
            boxSweetbox = new Panel();
            lblNoteVIP = new Label();
            boxVIP = new Panel();
            lblNoteStandard = new Label();
            boxStandard = new Panel();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            pnlSummary = new Guna.UI2.WinForms.Guna2ShadowPanel();
            picPoster = new Guna.UI2.WinForms.Guna2PictureBox();
            lblMovieTitle = new Label();
            lblShowtime = new Label();
            lblCinemaRoom = new Label();
            lblSeatsLabel = new Label();
            lblSeats = new Label();
            lblTotalLabel = new Label();
            lblTotalPrice = new Label();
            btnPayment = new Guna.UI2.WinForms.Guna2Button();
            panelMain.SuspendLayout();
            pnlSeatMapContainer.SuspendLayout();
            pnlSeatMap.SuspendLayout();
            pnlNote.SuspendLayout();
            pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPoster).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(pnlSeatMapContainer);
            panelMain.Controls.Add(pnlSummary);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1200, 700);
            panelMain.TabIndex = 0;
            // 
            // pnlSeatMapContainer
            // 
            pnlSeatMapContainer.BackColor = Color.WhiteSmoke;
            pnlSeatMapContainer.Controls.Add(pnlSeatMap);
            pnlSeatMapContainer.Controls.Add(pnlNote);
            pnlSeatMapContainer.Controls.Add(btnBack);
            pnlSeatMapContainer.Dock = DockStyle.Fill;
            pnlSeatMapContainer.Location = new Point(0, 0);
            pnlSeatMapContainer.Name = "pnlSeatMapContainer";
            pnlSeatMapContainer.Size = new Size(850, 700);
            pnlSeatMapContainer.TabIndex = 1;
            // 
            // pnlSeatMap
            // 
            pnlSeatMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlSeatMap.AutoScroll = true;
            pnlSeatMap.BackColor = Color.White;
            pnlSeatMap.BorderStyle = BorderStyle.FixedSingle;
            pnlSeatMap.Controls.Add(lblScreen);
            pnlSeatMap.Controls.Add(panelScreenLine);
            pnlSeatMap.Location = new Point(20, 60);
            pnlSeatMap.Name = "pnlSeatMap";
            pnlSeatMap.Size = new Size(810, 580);
            pnlSeatMap.TabIndex = 0;
            // 
            // lblScreen
            // 
            lblScreen.Anchor = AnchorStyles.Top;
            lblScreen.AutoSize = true;
            lblScreen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblScreen.ForeColor = Color.DimGray;
            lblScreen.Location = new Point(326, 33);
            lblScreen.Name = "lblScreen";
            lblScreen.Size = new Size(119, 28);
            lblScreen.TabIndex = 1;
            lblScreen.Text = "MÀN HÌNH";
            // 
            // panelScreenLine
            // 
            panelScreenLine.Anchor = AnchorStyles.Top;
            panelScreenLine.BorderColor = Color.DarkGray;
            panelScreenLine.BorderThickness = 4;
            panelScreenLine.CustomBorderThickness = new Padding(0, 4, 0, 0);
            panelScreenLine.CustomizableEdges = customizableEdges1;
            panelScreenLine.Location = new Point(100, 20);
            panelScreenLine.Name = "panelScreenLine";
            panelScreenLine.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelScreenLine.Size = new Size(600, 10);
            panelScreenLine.TabIndex = 0;
            // 
            // pnlNote
            // 
            pnlNote.Controls.Add(lblNoteSold);
            pnlNote.Controls.Add(boxSold);
            pnlNote.Controls.Add(lblNoteSelecting);
            pnlNote.Controls.Add(boxSelecting);
            pnlNote.Controls.Add(lblNoteSweetbox);
            pnlNote.Controls.Add(boxSweetbox);
            pnlNote.Controls.Add(lblNoteVIP);
            pnlNote.Controls.Add(boxVIP);
            pnlNote.Controls.Add(lblNoteStandard);
            pnlNote.Controls.Add(boxStandard);
            pnlNote.Dock = DockStyle.Bottom;
            pnlNote.Location = new Point(0, 650);
            pnlNote.Name = "pnlNote";
            pnlNote.Size = new Size(850, 50);
            pnlNote.TabIndex = 2;
            // 
            // lblNoteSold
            // 
            lblNoteSold.AutoSize = true;
            lblNoteSold.Location = new Point(472, 10);
            lblNoteSold.Name = "lblNoteSold";
            lblNoteSold.Size = new Size(69, 25);
            lblNoteSold.TabIndex = 9;
            lblNoteSold.Text = "Đã bán";
            // 
            // boxSold
            // 
            boxSold.BackColor = Color.DimGray;
            boxSold.Location = new Point(446, 15);
            boxSold.Name = "boxSold";
            boxSold.Size = new Size(20, 20);
            boxSold.TabIndex = 8;
            // 
            // lblNoteSelecting
            // 
            lblNoteSelecting.AutoSize = true;
            lblNoteSelecting.Location = new Point(341, 10);
            lblNoteSelecting.Name = "lblNoteSelecting";
            lblNoteSelecting.Size = new Size(99, 25);
            lblNoteSelecting.TabIndex = 7;
            lblNoteSelecting.Text = "Đang chọn";
            // 
            // boxSelecting
            // 
            boxSelecting.BackColor = Color.FromArgb(212, 33, 33);
            boxSelecting.Location = new Point(315, 15);
            boxSelecting.Name = "boxSelecting";
            boxSelecting.Size = new Size(20, 20);
            boxSelecting.TabIndex = 6;
            // 
            // lblNoteSweetbox
            // 
            lblNoteSweetbox.AutoSize = true;
            lblNoteSweetbox.Location = new Point(220, 10);
            lblNoteSweetbox.Name = "lblNoteSweetbox";
            lblNoteSweetbox.Size = new Size(89, 25);
            lblNoteSweetbox.TabIndex = 5;
            lblNoteSweetbox.Text = "Sweetbox";
            // 
            // boxSweetbox
            // 
            boxSweetbox.BackColor = Color.Pink;
            boxSweetbox.BorderStyle = BorderStyle.FixedSingle;
            boxSweetbox.Location = new Point(191, 15);
            boxSweetbox.Name = "boxSweetbox";
            boxSweetbox.Size = new Size(20, 20);
            boxSweetbox.TabIndex = 4;
            // 
            // lblNoteVIP
            // 
            lblNoteVIP.AutoSize = true;
            lblNoteVIP.Location = new Point(147, 10);
            lblNoteVIP.Name = "lblNoteVIP";
            lblNoteVIP.Size = new Size(38, 25);
            lblNoteVIP.TabIndex = 3;
            lblNoteVIP.Text = "VIP";
            // 
            // boxVIP
            // 
            boxVIP.BackColor = Color.MistyRose;
            boxVIP.BorderStyle = BorderStyle.FixedSingle;
            boxVIP.Location = new Point(121, 15);
            boxVIP.Name = "boxVIP";
            boxVIP.Size = new Size(20, 20);
            boxVIP.TabIndex = 2;
            // 
            // lblNoteStandard
            // 
            lblNoteStandard.AutoSize = true;
            lblNoteStandard.Location = new Point(46, 10);
            lblNoteStandard.Name = "lblNoteStandard";
            lblNoteStandard.Size = new Size(74, 25);
            lblNoteStandard.TabIndex = 1;
            lblNoteStandard.Text = "Thường";
            // 
            // boxStandard
            // 
            boxStandard.BackColor = Color.White;
            boxStandard.BorderStyle = BorderStyle.FixedSingle;
            boxStandard.Location = new Point(20, 15);
            boxStandard.Name = "boxStandard";
            boxStandard.Size = new Size(20, 20);
            boxStandard.TabIndex = 0;
            // 
            // btnBack
            // 
            btnBack.BorderRadius = 4;
            btnBack.CustomizableEdges = customizableEdges3;
            btnBack.FillColor = Color.Transparent;
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            btnBack.ForeColor = Color.DimGray;
            btnBack.Location = new Point(20, 10);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnBack.Size = new Size(126, 30);
            btnBack.TabIndex = 3;
            btnBack.Text = "< Quay lại";
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.Transparent;
            pnlSummary.Controls.Add(picPoster);
            pnlSummary.Controls.Add(lblMovieTitle);
            pnlSummary.Controls.Add(lblShowtime);
            pnlSummary.Controls.Add(lblCinemaRoom);
            pnlSummary.Controls.Add(lblSeatsLabel);
            pnlSummary.Controls.Add(lblSeats);
            pnlSummary.Controls.Add(lblTotalLabel);
            pnlSummary.Controls.Add(lblTotalPrice);
            pnlSummary.Controls.Add(btnPayment);
            pnlSummary.Dock = DockStyle.Right;
            pnlSummary.FillColor = Color.White;
            pnlSummary.Location = new Point(850, 0);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.ShadowColor = Color.Black;
            pnlSummary.ShadowDepth = 50;
            pnlSummary.ShadowShift = 10;
            pnlSummary.Size = new Size(350, 700);
            pnlSummary.TabIndex = 0;
            // 
            // picPoster
            // 
            picPoster.BorderRadius = 8;
            picPoster.CustomizableEdges = customizableEdges5;
            picPoster.ImageRotate = 0F;
            picPoster.Location = new Point(58, 60);
            picPoster.Name = "picPoster";
            picPoster.ShadowDecoration.CustomizableEdges = customizableEdges6;
            picPoster.Size = new Size(250, 323);
            picPoster.SizeMode = PictureBoxSizeMode.StretchImage;
            picPoster.TabIndex = 0;
            picPoster.TabStop = false;
            // 
            // lblMovieTitle
            // 
            lblMovieTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMovieTitle.Location = new Point(30, 21);
            lblMovieTitle.Name = "lblMovieTitle";
            lblMovieTitle.Size = new Size(310, 50);
            lblMovieTitle.TabIndex = 1;
            lblMovieTitle.Text = "Tên Phim";
            lblMovieTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblShowtime
            // 
            lblShowtime.Font = new Font("Segoe UI", 10F);
            lblShowtime.Location = new Point(30, 461);
            lblShowtime.Name = "lblShowtime";
            lblShowtime.Size = new Size(310, 35);
            lblShowtime.TabIndex = 2;
            lblShowtime.Text = "Suất chiếu: 20/12/2025 19:30";
            lblShowtime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCinemaRoom
            // 
            lblCinemaRoom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCinemaRoom.Location = new Point(30, 423);
            lblCinemaRoom.Name = "lblCinemaRoom";
            lblCinemaRoom.Size = new Size(310, 38);
            lblCinemaRoom.TabIndex = 3;
            lblCinemaRoom.Text = "CGV Vincom - Rạp 1";
            lblCinemaRoom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSeatsLabel
            // 
            lblSeatsLabel.AutoSize = true;
            lblSeatsLabel.Font = new Font("Segoe UI", 10F);
            lblSeatsLabel.Location = new Point(30, 509);
            lblSeatsLabel.Name = "lblSeatsLabel";
            lblSeatsLabel.Size = new Size(99, 28);
            lblSeatsLabel.TabIndex = 4;
            lblSeatsLabel.Text = "Ghế chọn:";
            // 
            // lblSeats
            // 
            lblSeats.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSeats.ForeColor = Color.FromArgb(212, 33, 33);
            lblSeats.Location = new Point(130, 505);
            lblSeats.Name = "lblSeats";
            lblSeats.Size = new Size(210, 50);
            lblSeats.TabIndex = 5;
            lblSeats.Text = "---";
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Font = new Font("Segoe UI", 10F);
            lblTotalLabel.Location = new Point(30, 568);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(99, 28);
            lblTotalLabel.TabIndex = 6;
            lblTotalLabel.Text = "Tổng tiền:";
            // 
            // lblTotalPrice
            // 
            lblTotalPrice.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotalPrice.ForeColor = Color.FromArgb(212, 33, 33);
            lblTotalPrice.Location = new Point(130, 555);
            lblTotalPrice.Name = "lblTotalPrice";
            lblTotalPrice.Size = new Size(210, 41);
            lblTotalPrice.TabIndex = 7;
            lblTotalPrice.Text = "0 đ";
            // 
            // btnPayment
            // 
            btnPayment.BorderRadius = 5;
            btnPayment.Cursor = Cursors.Hand;
            btnPayment.CustomizableEdges = customizableEdges7;
            btnPayment.DisabledState.BorderColor = Color.DarkGray;
            btnPayment.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPayment.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPayment.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPayment.FillColor = Color.FromArgb(212, 33, 33);
            btnPayment.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnPayment.ForeColor = Color.White;
            btnPayment.Location = new Point(30, 626);
            btnPayment.Name = "btnPayment";
            btnPayment.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnPayment.Size = new Size(290, 50);
            btnPayment.TabIndex = 8;
            btnPayment.Text = "THANH TOÁN";
            // 
            // UC_Booking
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(panelMain);
            Name = "UC_Booking";
            Size = new Size(1200, 700);
            panelMain.ResumeLayout(false);
            pnlSeatMapContainer.ResumeLayout(false);
            pnlSeatMap.ResumeLayout(false);
            pnlSeatMap.PerformLayout();
            pnlNote.ResumeLayout(false);
            pnlNote.PerformLayout();
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPoster).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel pnlSeatMapContainer;
        private System.Windows.Forms.Panel pnlSeatMap;
        private Guna.UI2.WinForms.Guna2ShadowPanel pnlSummary;
        private Guna.UI2.WinForms.Guna2Panel panelScreenLine;
        private System.Windows.Forms.Label lblScreen;
        private Guna.UI2.WinForms.Guna2PictureBox picPoster;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblShowtime;
        private System.Windows.Forms.Label lblCinemaRoom;
        private System.Windows.Forms.Label lblSeatsLabel;
        private System.Windows.Forms.Label lblSeats;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Label lblTotalLabel;
        private Guna.UI2.WinForms.Guna2Button btnPayment;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.Panel boxStandard;
        private System.Windows.Forms.Label lblNoteStandard;
        private System.Windows.Forms.Label lblNoteVIP;
        private System.Windows.Forms.Panel boxVIP;
        private System.Windows.Forms.Label lblNoteSelecting;
        private System.Windows.Forms.Panel boxSelecting;
        private System.Windows.Forms.Label lblNoteSold;
        private System.Windows.Forms.Panel boxSold;
        // Bổ sung Sweetbox
        private System.Windows.Forms.Label lblNoteSweetbox;
        private System.Windows.Forms.Panel boxSweetbox;
    }
}