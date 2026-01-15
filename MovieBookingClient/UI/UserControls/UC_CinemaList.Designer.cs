namespace MovieBookingClient.UI.UserControls
{
    partial class UC_CinemaList
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
            pnlMain = new Panel();
            flpMainContent = new FlowLayoutPanel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = SystemColors.Info;
            pnlMain.Controls.Add(flpMainContent);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1200, 700);
            pnlMain.TabIndex = 0;
            // 
            // flpMainContent
            // 
            flpMainContent.AutoSize = true;
            flpMainContent.FlowDirection = FlowDirection.TopDown;
            flpMainContent.Location = new Point(0, 100);
            flpMainContent.MaximumSize = new Size(1180, 0);
            flpMainContent.MinimumSize = new Size(1180, 0);
            flpMainContent.Name = "flpMainContent";
            flpMainContent.Padding = new Padding(100, 20, 100, 20);
            flpMainContent.Size = new Size(1180, 50);
            flpMainContent.TabIndex = 1;
            flpMainContent.WrapContents = false;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1200, 100);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Red;
            lblTitle.Location = new Point(3, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1194, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HỆ THỐNG RẠP PHIM";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UC_CinemaList
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(pnlMain);
            Name = "UC_CinemaList";
            Size = new Size(1200, 700);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpMainContent;
    }
}