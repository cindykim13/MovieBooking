namespace MovieBookingClient.UI.UserControls.Items
{
    partial class UC_TicketItem
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblPrice;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblMovieTitle = new Label();
            lblPrice = new Label();
            SuspendLayout();
            // 
            // lblMovieTitle
            // 
            lblMovieTitle.AutoSize = true;
            lblMovieTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMovieTitle.ForeColor = Color.Black;
            lblMovieTitle.Location = new Point(25, 28);
            lblMovieTitle.Margin = new Padding(4, 0, 4, 0);
            lblMovieTitle.Name = "lblMovieTitle";
            lblMovieTitle.Size = new Size(138, 38);
            lblMovieTitle.TabIndex = 0;
            lblMovieTitle.Text = "Tên Phim";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPrice.ForeColor = Color.FromArgb(212, 33, 33);
            lblPrice.Location = new Point(938, 28);
            lblPrice.Margin = new Padding(4, 0, 4, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(119, 38);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "Giá tiền";
            // 
            // UC_TicketItem
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            Controls.Add(lblPrice);
            Controls.Add(lblMovieTitle);
            Cursor = Cursors.Hand;
            Margin = new Padding(4, 5, 4, 5);
            Name = "UC_TicketItem";
            Padding = new Padding(0, 0, 0, 2);
            Size = new Size(1125, 94);
            ResumeLayout(false);
            PerformLayout();

        }
    }
}