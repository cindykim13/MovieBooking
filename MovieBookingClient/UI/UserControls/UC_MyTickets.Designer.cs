namespace MovieBookingClient.UI.UserControls
{
    partial class UC_MyTickets
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowPanelTickets;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowPanelTickets = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();

            this.Size = new System.Drawing.Size(1200, 700);
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // Title
            this.lblTitle.Text = "VÉ CỦA TÔI";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 33, 33);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.AutoSize = true;

            // FlowLayoutPanel (Danh sách cuộn)
            this.flowPanelTickets.Location = new System.Drawing.Point(30, 80);
            this.flowPanelTickets.Size = new System.Drawing.Size(1000, 600);
            this.flowPanelTickets.AutoScroll = true;
            this.flowPanelTickets.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelTickets.WrapContents = false;

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.flowPanelTickets);

            this.Load += new System.EventHandler(this.UC_MyTickets_Load);
        }
    }
}