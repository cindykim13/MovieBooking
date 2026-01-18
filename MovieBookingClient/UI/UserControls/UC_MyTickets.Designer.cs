namespace MovieBookingClient.UI.UserControls
{
    partial class UC_MyTickets
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.FlowLayoutPanel flowPanelTickets; // Public để dễ debug nếu cần

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            flowPanelTickets = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(212, 33, 33);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "VÉ CỦA TÔI";
            // 
            // flowPanelTickets
            // 
            flowPanelTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPanelTickets.AutoScroll = true;
            flowPanelTickets.BackColor = Color.White;
            flowPanelTickets.FlowDirection = FlowDirection.TopDown;
            flowPanelTickets.Location = new Point(30, 80);
            flowPanelTickets.Name = "flowPanelTickets";
            flowPanelTickets.Padding = new Padding(0, 0, 10, 0);
            flowPanelTickets.Size = new Size(1128, 476);
            flowPanelTickets.TabIndex = 1;
            flowPanelTickets.WrapContents = false;
            // 
            // UC_MyTickets
            // 
            BackColor = SystemColors.Info;
            Controls.Add(lblTitle);
            Controls.Add(flowPanelTickets);
            Name = "UC_MyTickets";
            Size = new Size(1183, 590);
            Load += UC_MyTickets_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}