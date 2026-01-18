namespace MovieBookingClient.UI.UserControls.Admin
{
    partial class UC_Admin_Showtimes
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
            // --- Khai báo các đối tượng giao diện ---

            // Các biến trang trí cho Panel và Button của Guna
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();

            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnXoa = new Guna.UI2.WinForms.Guna2Button();
            btnSua = new Guna.UI2.WinForms.Guna2Button();
            btnAdd = new Guna.UI2.WinForms.Guna2Button();
            btnXem = new Guna.UI2.WinForms.Guna2Button();

            dtpNgayChieu = new System.Windows.Forms.DateTimePicker();
            lblChonNgay = new System.Windows.Forms.Label();
            dgvLichChieu = new Guna.UI2.WinForms.Guna2DataGridView();

            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichChieu).BeginInit();
            SuspendLayout();

            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(btnXoa);
            pnlHeader.Controls.Add(btnSua);
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Controls.Add(btnXem);
            pnlHeader.Controls.Add(dtpNgayChieu);
            pnlHeader.Controls.Add(lblChonNgay);
            pnlHeader.CustomizableEdges = customizableEdges23;
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(10, 10);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.ShadowDecoration.CustomizableEdges = customizableEdges24;
            pnlHeader.Size = new System.Drawing.Size(1010, 60);
            pnlHeader.TabIndex = 0;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnXoa.BorderRadius = 5;
            btnXoa.CustomizableEdges = customizableEdges13;
            btnXoa.FillColor = System.Drawing.Color.FromArgb(231, 76, 60);
            btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnXoa.ForeColor = System.Drawing.Color.White;
            btnXoa.Location = new System.Drawing.Point(897, 12);
            btnXoa.Name = "btnXoa";
            btnXoa.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnXoa.Size = new System.Drawing.Size(100, 36);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "XÓA";
            // 
            // btnSua
            // 
            btnSua.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSua.BorderRadius = 5;
            btnSua.CustomizableEdges = customizableEdges15;
            btnSua.FillColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSua.ForeColor = System.Drawing.Color.White;
            btnSua.Location = new System.Drawing.Point(791, 12);
            btnSua.Name = "btnSua";
            btnSua.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnSua.Size = new System.Drawing.Size(100, 36);
            btnSua.TabIndex = 4;
            btnSua.Text = "SỬA";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAdd.BorderRadius = 5;
            btnAdd.CustomizableEdges = customizableEdges17;
            btnAdd.FillColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(685, 12);
            btnAdd.Name = "btnAdd";
            btnAdd.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnAdd.Size = new System.Drawing.Size(100, 36);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "THÊM";
            // 
            // btnXem
            // 
            btnXem.BorderRadius = 5;
            btnXem.CustomizableEdges = customizableEdges19;
            btnXem.FillColor = System.Drawing.Color.FromArgb(100, 88, 255);
            btnXem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnXem.ForeColor = System.Drawing.Color.White;
            btnXem.Location = new System.Drawing.Point(371, 12);
            btnXem.Name = "btnXem";
            btnXem.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnXem.Size = new System.Drawing.Size(80, 36);
            btnXem.TabIndex = 2;
            btnXem.Text = "XEM";

            // 
            // dtpNgayChieu (Standard WinForms Control)
            // 
            // Cấu hình: Font to, Format Short, ShowUpDown = False (Mặc định) để hiện lịch
            dtpNgayChieu.CalendarFont = new System.Drawing.Font("Segoe UI", 10F);
            dtpNgayChieu.Font = new System.Drawing.Font("Segoe UI", 11F);
            dtpNgayChieu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpNgayChieu.Location = new System.Drawing.Point(145, 15); // Căn chỉnh lại Y=15 cho thẳng hàng với Label
            dtpNgayChieu.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpNgayChieu.Name = "dtpNgayChieu";
            dtpNgayChieu.Size = new System.Drawing.Size(220, 32);
            dtpNgayChieu.TabIndex = 1;
            dtpNgayChieu.Value = System.DateTime.Now;

            // 
            // lblChonNgay
            // 
            lblChonNgay.AutoSize = true;
            lblChonNgay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblChonNgay.Location = new System.Drawing.Point(17, 18); // Căn chỉnh Y=18
            lblChonNgay.Name = "lblChonNgay";
            lblChonNgay.Size = new System.Drawing.Size(122, 30);
            lblChonNgay.TabIndex = 0;
            lblChonNgay.Text = "Chọn ngày:";

            // 
            // dgvLichChieu
            // 
            dgvLichChieu.AllowUserToAddRows = false;
            dgvLichChieu.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dgvLichChieu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvLichChieu.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvLichChieu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvLichChieu.ColumnHeadersHeight = 40;
            dgvLichChieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvLichChieu.DefaultCellStyle = dataGridViewCellStyle6;
            dgvLichChieu.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvLichChieu.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dgvLichChieu.Location = new System.Drawing.Point(10, 70);
            dgvLichChieu.Name = "dgvLichChieu";
            dgvLichChieu.ReadOnly = true;
            dgvLichChieu.RowHeadersVisible = false;
            dgvLichChieu.RowHeadersWidth = 62;
            dgvLichChieu.RowTemplate.Height = 35;
            dgvLichChieu.Size = new System.Drawing.Size(1010, 580);
            dgvLichChieu.TabIndex = 1;
            dgvLichChieu.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            dgvLichChieu.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvLichChieu.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            dgvLichChieu.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            dgvLichChieu.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            dgvLichChieu.ThemeStyle.BackColor = System.Drawing.SystemColors.Info;
            dgvLichChieu.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dgvLichChieu.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
            dgvLichChieu.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvLichChieu.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dgvLichChieu.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvLichChieu.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvLichChieu.ThemeStyle.HeaderStyle.Height = 40;
            dgvLichChieu.ThemeStyle.ReadOnly = true;
            dgvLichChieu.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            dgvLichChieu.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLichChieu.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dgvLichChieu.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dgvLichChieu.ThemeStyle.RowsStyle.Height = 35;
            dgvLichChieu.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dgvLichChieu.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);

            // 
            // UC_Admin_Showtimes
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.Info;
            Controls.Add(dgvLichChieu);
            Controls.Add(pnlHeader);
            Name = "UC_Admin_Showtimes";
            Padding = new System.Windows.Forms.Padding(10);
            Size = new System.Drawing.Size(1030, 660);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichChieu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Button btnXem;
        private System.Windows.Forms.DateTimePicker dtpNgayChieu;
        private System.Windows.Forms.Label lblChonNgay;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLichChieu;
    }
}