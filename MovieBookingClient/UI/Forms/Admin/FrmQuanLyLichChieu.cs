using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MovieBookingClient.UI.Forms.Admin
{
   public class FrmQuanLyLichChieu : UserControl
    {
        // Khai báo Control
        private Panel panelTop;
        private Label lblTitle;
        private Label lblChonNgay;
        private DateTimePicker dtpNgayChieu; // Cái lịch chọn ngày
        private Button btnXem;
        private Button btnThem;
        private DataGridView dgvLichChieu;

        // Các cột của bảng
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colPhim;
        private DataGridViewTextBoxColumn colPhong;
        private DataGridViewTextBoxColumn colGio;
        private DataGridViewTextBoxColumn colGia;

        public FrmQuanLyLichChieu()
        {
            // 1. Vẽ giao diện
            InitializeComponent();

            // 2. Load dữ liệu giả
            LoadDuLieuGia();
        }

        // --- PHẦN 1: LOGIC XỬ LÝ ---

        private void LoadDuLieuGia()
        {
            // Giả lập dữ liệu lịch chiếu
            var listLich = new List<dynamic>
            {
                new { Id = "LC01", TenPhim = "Mai", Phong = "Cinema 01 (IMAX)", Gio = "18:00 - 20:15", Gia = "120,000" },
                new { Id = "LC02", TenPhim = "Đào, Phở và Piano", Phong = "Cinema 02", Gio = "19:30 - 21:00", Gia = "45,000" },
                new { Id = "LC03", TenPhim = "Kung Fu Panda 4", Phong = "Cinema 03", Gio = "20:00 - 21:45", Gia = "110,000" },
                new { Id = "LC04", TenPhim = "Dune: Hành Tinh Cát", Phong = "Cinema 01 (IMAX)", Gio = "21:00 - 23:45", Gia = "150,000" },
            };

            dgvLichChieu.DataSource = listLich;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xếp lịch chiếu đang phát triển!", "Thông báo");
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            string ngay = dtpNgayChieu.Value.ToString("dd/MM/yyyy");
            MessageBox.Show($"Đang lọc lịch chiếu ngày: {ngay}", "Thông báo");
        }

        // --- PHẦN 2: GIAO DIỆN (Code thay thế Designer) ---
        private void InitializeComponent()
        {
            // Khởi tạo
            this.panelTop = new Panel();
            this.lblTitle = new Label();
            this.lblChonNgay = new Label();
            this.dtpNgayChieu = new DateTimePicker();
            this.btnXem = new Button();
            this.btnThem = new Button();
            this.dgvLichChieu = new DataGridView();

            // Khởi tạo cột
            this.colId = new DataGridViewTextBoxColumn();
            this.colPhim = new DataGridViewTextBoxColumn();
            this.colPhong = new DataGridViewTextBoxColumn();
            this.colGio = new DataGridViewTextBoxColumn();
            this.colGia = new DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLichChieu)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // Cấu hình Form chung
            this.Size = new Size(1100, 700);
            this.BackColor = Color.FromArgb(242, 245, 250);

            // 
            // 1. Panel Top (Chứa tiêu đề + Lịch)
            // 
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 110;
            this.panelTop.BackColor = Color.White;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblChonNgay);
            this.panelTop.Controls.Add(this.dtpNgayChieu);
            this.panelTop.Controls.Add(this.btnXem);
            this.panelTop.Controls.Add(this.btnThem);

            // Tiêu đề
            this.lblTitle.Text = "Quản Lý Lịch Chiếu 📅";
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkSlateGray;
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.AutoSize = true;

            // Label "Chọn ngày"
            this.lblChonNgay.Text = "Chọn ngày chiếu:";
            this.lblChonNgay.Font = new Font("Segoe UI", 10F);
            this.lblChonNgay.Location = new Point(25, 68);
            this.lblChonNgay.AutoSize = true;

            // DateTimePicker (Lịch)
            this.dtpNgayChieu.Format = DateTimePickerFormat.Short;
            this.dtpNgayChieu.Font = new Font("Segoe UI", 11F);
            this.dtpNgayChieu.Location = new Point(150, 65);
            this.dtpNgayChieu.Size = new Size(140, 30);

            // Nút Xem
            this.btnXem.Text = "Xem Lịch";
            this.btnXem.BackColor = Color.SeaGreen;
            this.btnXem.ForeColor = Color.White;
            this.btnXem.FlatStyle = FlatStyle.Flat;
            this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnXem.Location = new Point(310, 64);
            this.btnXem.Size = new Size(100, 32);
            this.btnXem.Click += new EventHandler(this.btnXem_Click);

            // Nút Thêm Mới
            this.btnThem.Text = "+ Thêm Suất Chiếu";
            this.btnThem.BackColor = Color.FromArgb(0, 118, 212);
            this.btnThem.ForeColor = Color.White;
            this.btnThem.FlatStyle = FlatStyle.Flat;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnThem.Size = new Size(180, 40);
            this.btnThem.Location = new Point(900, 50);
            this.btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnThem.Click += new EventHandler(this.btnThem_Click);

            // 
            // 2. DataGridView (Bảng Lịch Chiếu)
            // 
            this.dgvLichChieu.Dock = DockStyle.Fill;
            this.dgvLichChieu.BackgroundColor = Color.White;
            this.dgvLichChieu.BorderStyle = BorderStyle.None;
            this.dgvLichChieu.ColumnHeadersHeight = 45;
            this.dgvLichChieu.RowTemplate.Height = 35;
            this.dgvLichChieu.AllowUserToAddRows = false;
            this.dgvLichChieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichChieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Cấu hình các cột
            this.colId.DataPropertyName = "Id"; this.colId.HeaderText = "Mã LC"; this.colId.Width = 80;
            this.colPhim.DataPropertyName = "TenPhim"; this.colPhim.HeaderText = "Phim Chiếu";
            this.colPhong.DataPropertyName = "Phong"; this.colPhong.HeaderText = "Phòng Chiếu";
            this.colGio.DataPropertyName = "Gio"; this.colGio.HeaderText = "Giờ Chiếu";
            this.colGia.DataPropertyName = "Gia"; this.colGia.HeaderText = "Giá Vé";

            this.dgvLichChieu.Columns.AddRange(new DataGridViewColumn[] { colId, colPhim, colPhong, colGio, colGia });

            // Thêm vào Form
            this.Controls.Add(this.dgvLichChieu);
            this.Controls.Add(this.panelTop);

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichChieu)).EndInit();
            this.ResumeLayout(false);
        }
    }
}