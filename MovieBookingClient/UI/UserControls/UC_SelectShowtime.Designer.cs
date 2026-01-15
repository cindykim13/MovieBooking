namespace MovieBookingClient.UI.UserControls
{
    partial class UC_SelectShowtime
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            panelHeader = new Panel();
            lblMovieTitle = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            panelDateSelection = new Panel();
            btnNextWeek = new Guna.UI2.WinForms.Guna2Button();
            btnPreviousWeek = new Guna.UI2.WinForms.Guna2Button();
            flpDates = new FlowLayoutPanel();
            lblDateLabel = new Label();
            pnlMainContent = new Panel();
            lblNoShowtimes = new Label();
            flpCinemas = new FlowLayoutPanel();
            panelHeader.SuspendLayout();
            panelDateSelection.SuspendLayout();
            pnlMainContent.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.TargetControl = this;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblMovieTitle);
            panelHeader.Controls.Add(btnBack);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 60);
            panelHeader.TabIndex = 0;
            // 
            // lblMovieTitle
            // 
            lblMovieTitle.AutoSize = true;
            lblMovieTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMovieTitle.ForeColor = Color.FromArgb(212, 33, 33);
            lblMovieTitle.Location = new Point(180, 12);
            lblMovieTitle.Name = "lblMovieTitle";
            lblMovieTitle.Size = new Size(383, 45);
            lblMovieTitle.TabIndex = 1;
            lblMovieTitle.Text = "TÊN PHIM ĐANG CHỌN";
            lblMovieTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            btnBack.BorderRadius = 4;
            btnBack.Cursor = Cursors.Hand;
            btnBack.CustomizableEdges = customizableEdges5;
            btnBack.FillColor = Color.Transparent;
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
            btnBack.ForeColor = Color.DimGray;
            btnBack.Location = new Point(10, 10);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnBack.Size = new Size(128, 40);
            btnBack.TabIndex = 0;
            btnBack.Text = "< Quay lại";
            // 
            // panelDateSelection
            // 
            panelDateSelection.BackColor = Color.FromArgb(248, 248, 248);
            panelDateSelection.BorderStyle = BorderStyle.FixedSingle;
            panelDateSelection.Controls.Add(btnNextWeek);
            panelDateSelection.Controls.Add(btnPreviousWeek);
            panelDateSelection.Controls.Add(flpDates);
            panelDateSelection.Controls.Add(lblDateLabel);
            panelDateSelection.Dock = DockStyle.Top;
            panelDateSelection.Location = new Point(0, 60);
            panelDateSelection.Name = "panelDateSelection";
            panelDateSelection.Size = new Size(1200, 110);
            panelDateSelection.TabIndex = 1;
            // 
            // btnNextWeek
            // 
            btnNextWeek.BorderRadius = 5;
            btnNextWeek.CustomizableEdges = customizableEdges1;
            btnNextWeek.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnNextWeek.ForeColor = Color.White;
            btnNextWeek.Location = new Point(1075, 31);
            btnNextWeek.Name = "btnNextWeek";
            btnNextWeek.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNextWeek.Size = new Size(50, 50);
            btnNextWeek.TabIndex = 0;
            btnNextWeek.Text = ">";
            // 
            // btnPreviousWeek
            // 
            btnPreviousWeek.BorderRadius = 5;
            btnPreviousWeek.CustomizableEdges = customizableEdges3;
            btnPreviousWeek.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPreviousWeek.ForeColor = Color.White;
            btnPreviousWeek.Location = new Point(114, 31);
            btnPreviousWeek.Name = "btnPreviousWeek";
            btnPreviousWeek.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPreviousWeek.Size = new Size(50, 50);
            btnPreviousWeek.TabIndex = 1;
            btnPreviousWeek.Text = "<";
            // 
            // flpDates
            // 
            flpDates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flpDates.AutoScroll = true;
            flpDates.BackColor = Color.Transparent;
            flpDates.Location = new Point(184, 20);
            flpDates.Name = "flpDates";
            flpDates.Size = new Size(860, 70);
            flpDates.TabIndex = 2;
            flpDates.WrapContents = false;
            // 
            // lblDateLabel
            // 
            lblDateLabel.AutoSize = true;
            lblDateLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDateLabel.Location = new Point(9, 0);
            lblDateLabel.Name = "lblDateLabel";
            lblDateLabel.Size = new Size(117, 28);
            lblDateLabel.TabIndex = 0;
            lblDateLabel.Text = "Chọn ngày:";
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = Color.White;
            pnlMainContent.Controls.Add(lblNoShowtimes);
            pnlMainContent.Controls.Add(flpCinemas);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(0, 170);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Padding = new Padding(20);
            pnlMainContent.Size = new Size(1200, 530);
            pnlMainContent.TabIndex = 2;
            // 
            // lblNoShowtimes
            // 
            lblNoShowtimes.AutoSize = true;
            lblNoShowtimes.Font = new Font("Segoe UI", 14F);
            lblNoShowtimes.ForeColor = Color.Gray;
            lblNoShowtimes.Location = new Point(400, 50);
            lblNoShowtimes.Name = "lblNoShowtimes";
            lblNoShowtimes.Size = new Size(556, 38);
            lblNoShowtimes.TabIndex = 1;
            lblNoShowtimes.Text = "Không có suất chiếu nào cho ngày đã chọn.";
            lblNoShowtimes.Visible = false;
            // 
            // flpCinemas
            // 
            flpCinemas.AutoScroll = true;
            flpCinemas.Dock = DockStyle.Fill;
            flpCinemas.FlowDirection = FlowDirection.TopDown;
            flpCinemas.Location = new Point(20, 20);
            flpCinemas.Name = "flpCinemas";
            flpCinemas.Size = new Size(1160, 490);
            flpCinemas.TabIndex = 0;
            flpCinemas.WrapContents = false;
            // 
            // UC_SelectShowtime
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(pnlMainContent);
            Controls.Add(panelDateSelection);
            Controls.Add(panelHeader);
            Name = "UC_SelectShowtime";
            Size = new Size(1200, 700);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelDateSelection.ResumeLayout(false);
            panelDateSelection.PerformLayout();
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Panel panelDateSelection;
        private System.Windows.Forms.Label lblDateLabel;
        private System.Windows.Forms.FlowLayoutPanel flpDates;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.FlowLayoutPanel flpCinemas;
        private System.Windows.Forms.Label lblNoShowtimes;
        private Guna.UI2.WinForms.Guna2Button btnNextWeek;
        private Guna.UI2.WinForms.Guna2Button btnPreviousWeek;
    }
}