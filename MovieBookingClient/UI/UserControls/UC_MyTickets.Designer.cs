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
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowPanelTickets = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();

            // 
            // UC_MyTickets Setup
            // 
            this.Size = new System.Drawing.Size(1200, 700);
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // 
            // lblTitle
            // 
            this.lblTitle.Text = "VÉ CỦA TÔI";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 33, 33);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.AutoSize = true;

            // 
            // flowPanelTickets
            // 
            this.flowPanelTickets.AutoScroll = true; // Bắt buộc để hiện thanh cuộn

            // Quan trọng: TopDown + NoWrap = List dọc chuẩn
            this.flowPanelTickets.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelTickets.WrapContents = false;

            this.flowPanelTickets.Location = new System.Drawing.Point(30, 80);
            this.flowPanelTickets.Size = new System.Drawing.Size(1140, 600);

            // Anchor 4 chiều: Giúp panel tự co giãn khi form chính thay đổi kích thước
            this.flowPanelTickets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            // Padding bên phải 10 để thanh cuộn không đè lên nội dung
            this.flowPanelTickets.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.flowPanelTickets.BackColor = System.Drawing.Color.Transparent;

            // 
            // Adding Controls
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.flowPanelTickets);

            this.Load += new System.EventHandler(this.UC_MyTickets_Load);
        }
    }
}