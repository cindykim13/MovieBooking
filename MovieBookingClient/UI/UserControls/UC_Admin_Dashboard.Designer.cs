namespace MovieBookingClient.UI.UserControls.Admin
{
    partial class UC_Admin_Dashboard
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            lblDashboardTitle = new Label();
            pnlCardRevenue = new Guna.UI2.WinForms.Guna2Panel();
            lblRevenueVal = new Label();
            lblRevenueTitle = new Label();
            pnlCardTickets = new Guna.UI2.WinForms.Guna2Panel();
            lblTicketVal = new Label();
            lblTicketTitle = new Label();
            pnlCardMovies = new Guna.UI2.WinForms.Guna2Panel();
            lblMovieVal = new Label();
            lblMovieTitle = new Label();
            lblRecentTitle = new Label();
            dgvRecentTransactions = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlCardRevenue.SuspendLayout();
            pnlCardTickets.SuspendLayout();
            pnlCardMovies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentTransactions).BeginInit();
            SuspendLayout();
            // 
            // lblDashboardTitle
            // 
            lblDashboardTitle.AutoSize = true;
            lblDashboardTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDashboardTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblDashboardTitle.Location = new Point(27, 16);
            lblDashboardTitle.Margin = new Padding(4, 0, 4, 0);
            lblDashboardTitle.Name = "lblDashboardTitle";
            lblDashboardTitle.Size = new Size(214, 45);
            lblDashboardTitle.TabIndex = 0;
            lblDashboardTitle.Text = "TỔNG QUAN";
            // 
            // pnlCardRevenue
            // 
            pnlCardRevenue.BackColor = Color.Transparent;
            pnlCardRevenue.BorderRadius = 10;
            pnlCardRevenue.Controls.Add(lblRevenueVal);
            pnlCardRevenue.Controls.Add(lblRevenueTitle);
            pnlCardRevenue.CustomizableEdges = customizableEdges7;
            pnlCardRevenue.FillColor = Color.LightCoral;
            pnlCardRevenue.Location = new Point(36, 66);
            pnlCardRevenue.Margin = new Padding(4, 5, 4, 5);
            pnlCardRevenue.Name = "pnlCardRevenue";
            pnlCardRevenue.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlCardRevenue.Size = new Size(301, 160);
            pnlCardRevenue.TabIndex = 1;
            // 
            // lblRevenueVal
            // 
            lblRevenueVal.AutoSize = true;
            lblRevenueVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblRevenueVal.ForeColor = Color.White;
            lblRevenueVal.Location = new Point(21, 83);
            lblRevenueVal.Margin = new Padding(4, 0, 4, 0);
            lblRevenueVal.Name = "lblRevenueVal";
            lblRevenueVal.Size = new Size(242, 54);
            lblRevenueVal.TabIndex = 0;
            lblRevenueVal.Text = "5,250,000 đ";
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new Font("Segoe UI", 11F);
            lblRevenueTitle.ForeColor = Color.White;
            lblRevenueTitle.Location = new Point(29, 33);
            lblRevenueTitle.Margin = new Padding(4, 0, 4, 0);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(203, 30);
            lblRevenueTitle.TabIndex = 1;
            lblRevenueTitle.Text = "Doanh thu hôm nay";
            // 
            // pnlCardTickets
            // 
            pnlCardTickets.BackColor = Color.Transparent;
            pnlCardTickets.BorderRadius = 10;
            pnlCardTickets.Controls.Add(lblTicketVal);
            pnlCardTickets.Controls.Add(lblTicketTitle);
            pnlCardTickets.CustomizableEdges = customizableEdges9;
            pnlCardTickets.FillColor = Color.LightSkyBlue;
            pnlCardTickets.Location = new Point(368, 66);
            pnlCardTickets.Margin = new Padding(4, 5, 4, 5);
            pnlCardTickets.Name = "pnlCardTickets";
            pnlCardTickets.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlCardTickets.Size = new Size(303, 160);
            pnlCardTickets.TabIndex = 2;
            // 
            // lblTicketVal
            // 
            lblTicketVal.AutoSize = true;
            lblTicketVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTicketVal.ForeColor = Color.White;
            lblTicketVal.Location = new Point(21, 83);
            lblTicketVal.Margin = new Padding(4, 0, 4, 0);
            lblTicketVal.Name = "lblTicketVal";
            lblTicketVal.Size = new Size(126, 54);
            lblTicketVal.TabIndex = 0;
            lblTicketVal.Text = "42 Vé";
            // 
            // lblTicketTitle
            // 
            lblTicketTitle.AutoSize = true;
            lblTicketTitle.Font = new Font("Segoe UI", 11F);
            lblTicketTitle.ForeColor = Color.White;
            lblTicketTitle.Location = new Point(29, 33);
            lblTicketTitle.Margin = new Padding(4, 0, 4, 0);
            lblTicketTitle.Name = "lblTicketTitle";
            lblTicketTitle.Size = new Size(105, 30);
            lblTicketTitle.TabIndex = 1;
            lblTicketTitle.Text = "Vé bán ra";
            // 
            // pnlCardMovies
            // 
            pnlCardMovies.BackColor = Color.Transparent;
            pnlCardMovies.BorderRadius = 10;
            pnlCardMovies.Controls.Add(lblMovieVal);
            pnlCardMovies.Controls.Add(lblMovieTitle);
            pnlCardMovies.CustomizableEdges = customizableEdges11;
            pnlCardMovies.FillColor = Color.Tomato;
            pnlCardMovies.Location = new Point(709, 66);
            pnlCardMovies.Margin = new Padding(4, 5, 4, 5);
            pnlCardMovies.Name = "pnlCardMovies";
            pnlCardMovies.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlCardMovies.Size = new Size(289, 160);
            pnlCardMovies.TabIndex = 3;
            // 
            // lblMovieVal
            // 
            lblMovieVal.AutoSize = true;
            lblMovieVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMovieVal.ForeColor = Color.White;
            lblMovieVal.Location = new Point(21, 83);
            lblMovieVal.Margin = new Padding(4, 0, 4, 0);
            lblMovieVal.Name = "lblMovieVal";
            lblMovieVal.Size = new Size(154, 54);
            lblMovieVal.TabIndex = 0;
            lblMovieVal.Text = "8 Phim";
            // 
            // lblMovieTitle
            // 
            lblMovieTitle.AutoSize = true;
            lblMovieTitle.Font = new Font("Segoe UI", 11F);
            lblMovieTitle.ForeColor = Color.White;
            lblMovieTitle.Location = new Point(29, 33);
            lblMovieTitle.Margin = new Padding(4, 0, 4, 0);
            lblMovieTitle.Name = "lblMovieTitle";
            lblMovieTitle.Size = new Size(173, 30);
            lblMovieTitle.TabIndex = 1;
            lblMovieTitle.Text = "Phim đang chiếu";
            // 
            // lblRecentTitle
            // 
            lblRecentTitle.AutoSize = true;
            lblRecentTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRecentTitle.ForeColor = Color.Black;
            lblRecentTitle.Location = new Point(36, 255);
            lblRecentTitle.Margin = new Padding(4, 0, 4, 0);
            lblRecentTitle.Name = "lblRecentTitle";
            lblRecentTitle.Size = new Size(255, 32);
            lblRecentTitle.TabIndex = 5;
            lblRecentTitle.Text = "GIAO DỊCH GẦN ĐÂY";
            // 
            // dgvRecentTransactions
            // 
            dgvRecentTransactions.AllowUserToAddRows = false;
            dgvRecentTransactions.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = Color.White;
            dgvRecentTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvRecentTransactions.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvRecentTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvRecentTransactions.ColumnHeadersHeight = 35;
            dgvRecentTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvRecentTransactions.DefaultCellStyle = dataGridViewCellStyle6;
            dgvRecentTransactions.GridColor = Color.FromArgb(231, 229, 255);
            dgvRecentTransactions.Location = new Point(36, 292);
            dgvRecentTransactions.Margin = new Padding(4, 5, 4, 5);
            dgvRecentTransactions.Name = "dgvRecentTransactions";
            dgvRecentTransactions.ReadOnly = true;
            dgvRecentTransactions.RowHeadersVisible = false;
            dgvRecentTransactions.RowHeadersWidth = 62;
            dgvRecentTransactions.RowTemplate.Height = 30;
            dgvRecentTransactions.Size = new Size(962, 496);
            dgvRecentTransactions.TabIndex = 4;
            dgvRecentTransactions.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvRecentTransactions.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvRecentTransactions.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvRecentTransactions.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvRecentTransactions.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvRecentTransactions.ThemeStyle.BackColor = Color.White;
            dgvRecentTransactions.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvRecentTransactions.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(230, 230, 230);
            dgvRecentTransactions.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRecentTransactions.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvRecentTransactions.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvRecentTransactions.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRecentTransactions.ThemeStyle.HeaderStyle.Height = 35;
            dgvRecentTransactions.ThemeStyle.ReadOnly = true;
            dgvRecentTransactions.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvRecentTransactions.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentTransactions.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvRecentTransactions.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvRecentTransactions.ThemeStyle.RowsStyle.Height = 30;
            dgvRecentTransactions.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvRecentTransactions.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // UC_Admin_Dashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            Controls.Add(dgvRecentTransactions);
            Controls.Add(lblRecentTitle);
            Controls.Add(pnlCardMovies);
            Controls.Add(pnlCardTickets);
            Controls.Add(pnlCardRevenue);
            Controls.Add(lblDashboardTitle);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UC_Admin_Dashboard";
            Size = new Size(1028, 813);
            pnlCardRevenue.ResumeLayout(false);
            pnlCardRevenue.PerformLayout();
            pnlCardTickets.ResumeLayout(false);
            pnlCardTickets.PerformLayout();
            pnlCardMovies.ResumeLayout(false);
            pnlCardMovies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentTransactions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblDashboardTitle;

        private Guna.UI2.WinForms.Guna2Panel pnlCardRevenue;
        private System.Windows.Forms.Label lblRevenueVal;
        private System.Windows.Forms.Label lblRevenueTitle;

        private Guna.UI2.WinForms.Guna2Panel pnlCardTickets;
        private System.Windows.Forms.Label lblTicketVal;
        private System.Windows.Forms.Label lblTicketTitle;

        private Guna.UI2.WinForms.Guna2Panel pnlCardMovies;
        private System.Windows.Forms.Label lblMovieVal;
        private System.Windows.Forms.Label lblMovieTitle;

        private System.Windows.Forms.Label lblRecentTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecentTransactions;
    }
}