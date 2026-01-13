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
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.AutoSize = true;
            this.lblMovieTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMovieTitle.ForeColor = System.Drawing.Color.Black;
            this.lblMovieTitle.Location = new System.Drawing.Point(20, 18);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(97, 28);
            this.lblMovieTitle.TabIndex = 0;
            this.lblMovieTitle.Text = "Tên Phim";

            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblPrice.Location = new System.Drawing.Point(750, 18);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(88, 28);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "Giá tiền";

            // 
            // UC_TicketItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblMovieTitle);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "UC_TicketItem";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.Size = new System.Drawing.Size(900, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}