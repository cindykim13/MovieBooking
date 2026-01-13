using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MovieBookingClient.UI.Forms.Admin
{
    public class FrmQuanLyPhong : UserControl
    {
        private Panel panelTop;
        private Label lblTitle;
        private Label labelSearch;
        private Button btnThem;
        private TextBox txtTimKiem;
        private DataGridView dgvPhong;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTen;
        private DataGridViewTextBoxColumn colGhe;
        private DataGridViewTextBoxColumn colTrangThai;

        public FrmQuanLyPhong()
        {
            // 1. Vẽ giao diện bằng code
            InitializeComponent();

            // 2. Load dữ liệu giả để test
            LoadDuLieuGia();
        }

        // --- PHẦN 1: LOGIC XỬ LÝ ---

        private void LoadDuLieuGia()
        {
            // Tạo dữ liệu giả
            var listPhong = new List<dynamic>
            {
                new { Id = "P01", TenPhong = "Cinema 01 (IMAX)", SoGhe = 120, TrangThai = "Hoạt động" },
                new { Id = "P02", TenPhong = "Cinema 02 (Standard)", SoGhe = 80, TrangThai = "Đang dọn" },
                new { Id = "P03", TenPhong = "Cinema 03 (Couple)", SoGhe = 40, TrangThai = "Bảo trì" },
                new { Id = "P04", TenPhong = "Cinema 04 (VIP)", SoGhe = 30, TrangThai = "Hoạt động" },
            };

            // Đổ vào bảng
            dgvPhong.DataSource = listPhong;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang phát triển!", "Thông báo");
        }

        // --- PHẦN 2: GIAO DIỆN (Code thay thế Designer) ---
        private void InitializeComponent()
        {
            // Khởi tạo
            this.panelTop = new Panel();
            this.labelSearch = new Label();
            this.btnThem = new Button();
            this.txtTimKiem = new TextBox();
            this.lblTitle = new Label();
            this.dgvPhong = new DataGridView();
            this.colId = new DataGridViewTextBoxColumn();
            this.colTen = new DataGridViewTextBoxColumn();
            this.colGhe = new DataGridViewTextBoxColumn();
            this.colTrangThai = new DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // 
            // Cấu hình Form chính
            // 
            this.BackColor = Color.FromArgb(242, 245, 250); // Màu nền xám nhạt hiện đại
            this.Size = new Size(1100, 700);

            // 
            // 1. Panel Top (Thanh trên cùng)
            // 
            this.panelTop.BackColor = Color.White;
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 100;
            this.panelTop.Controls.Add(this.labelSearch);
            this.panelTop.Controls.Add(this.btnThem);
            this.panelTop.Controls.Add(this.txtTimKiem);
            this.panelTop.Controls.Add(this.lblTitle);

            // Tiêu đề
            this.lblTitle.Text = "Quản Lý Phòng Chiếu 🎬";
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkSlateGray;
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.AutoSize = true;

            // Nút Thêm
            this.btnThem.Text = "+ Thêm Phòng";
            this.btnThem.BackColor = Color.FromArgb(0, 118, 212); // Màu xanh dương
            this.btnThem.ForeColor = Color.White;
            this.btnThem.FlatStyle = FlatStyle.Flat;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnThem.Size = new Size(150, 40);
            this.btnThem.Location = new Point(900, 50);
            this.btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnThem.Click += new EventHandler(this.btnThem_Click); // Gắn sự kiện Click

            // Label Tìm kiếm
            this.labelSearch.Text = "Tìm kiếm:";
            this.labelSearch.Font = new Font("Segoe UI", 10F);
            this.labelSearch.Location = new Point(25, 65);
            this.labelSearch.AutoSize = true;

            // Ô Tìm kiếm
            this.txtTimKiem.Font = new Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new Point(100, 60);
            this.txtTimKiem.Size = new Size(600, 30);
            this.txtTimKiem.BackColor = Color.WhiteSmoke;
            this.txtTimKiem.BorderStyle = BorderStyle.None;

            // 
            // 2. DataGridView (Bảng dữ liệu)
            // 
            this.dgvPhong.Dock = DockStyle.Fill;
            this.dgvPhong.BackgroundColor = Color.White;
            this.dgvPhong.BorderStyle = BorderStyle.None;
            this.dgvPhong.ColumnHeadersHeight = 40;
            this.dgvPhong.RowTemplate.Height = 35;
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Định nghĩa cột
            this.colId.DataPropertyName = "Id"; this.colId.HeaderText = "Mã Phòng";
            this.colTen.DataPropertyName = "TenPhong"; this.colTen.HeaderText = "Tên Phòng";
            this.colGhe.DataPropertyName = "SoGhe"; this.colGhe.HeaderText = "Số Ghế";
            this.colTrangThai.DataPropertyName = "TrangThai"; this.colTrangThai.HeaderText = "Trạng Thái";

            this.dgvPhong.Columns.AddRange(new DataGridViewColumn[] { colId, colTen, colGhe, colTrangThai });

            // Thêm bảng vào Form
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.panelTop);

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.ResumeLayout(false);
        }
    }
}