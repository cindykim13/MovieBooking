using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MovieBookingClient.UI.Forms.Admin
{
    // 👇 Trang chủ hiển thị Thống kê (Dashboard)
    public class FrmDashboardHome : UserControl
    {
        // Khai báo giao diện
        private Panel pnCards; // Chứa 3 thẻ thống kê
        private Panel pnCard1, pnCard2, pnCard3; // 3 thẻ
        private Label lblTitle;
        private DataGridView dgvTopMovies;
        private Label lblSubTitle;

        public FrmDashboardHome()
        {
            InitializeComponent();
            LoadThongKe();
        }

        // --- PHẦN 1: LOGIC (Fake số liệu) ---
        private void LoadThongKe()
        {
            // 1. Fake số liệu trên 3 thẻ
            UpdateCard(pnCard1, "DOANH THU HÔM NAY", "15,450,000 đ", Color.FromArgb(0, 192, 192)); // Xanh ngọc
            UpdateCard(pnCard2, "VÉ ĐÃ BÁN", "185 Vé", Color.FromArgb(255, 128, 0));    // Cam
            UpdateCard(pnCard3, "PHIM ĐANG CHIẾU", "8 Phim", Color.FromArgb(128, 128, 255)); // Tím

            // 2. Fake danh sách phim HOT
            var listTop = new List<dynamic>
            {
                new { Hang = 1, TenPhim = "Mai (Trấn Thành)", DoanhThu = "5,200,000 đ", LuotXem = 65 },
                new { Hang = 2, TenPhim = "Đào, Phở và Piano", DoanhThu = "3,800,000 đ", LuotXem = 48 },
                new { Hang = 3, TenPhim = "Kung Fu Panda 4", DoanhThu = "2,500,000 đ", LuotXem = 30 },
                new { Hang = 4, TenPhim = "Dune: Hành Tinh Cát", DoanhThu = "2,100,000 đ", LuotXem = 25 },
                new { Hang = 5, TenPhim = "Exhuma: Quật Mộ", DoanhThu = "1,850,000 đ", LuotXem = 17 },
            };
            dgvTopMovies.DataSource = listTop;
        }

        // Hàm vẽ chữ lên thẻ (Helper)
        private void UpdateCard(Panel pn, string title, string value, Color bgColor)
        {
            pn.BackColor = bgColor;
            pn.Controls.Clear();

            Label lblT = new Label { Text = title, ForeColor = Color.White, Font = new Font("Segoe UI", 10), Location = new Point(20, 20), AutoSize = true };
            Label lblV = new Label { Text = value, ForeColor = Color.White, Font = new Font("Segoe UI", 20, FontStyle.Bold), Location = new Point(20, 50), AutoSize = true };

            pn.Controls.Add(lblT);
            pn.Controls.Add(lblV);
        }

        // --- PHẦN 2: GIAO DIỆN (Code thay Designer) ---
        private void InitializeComponent()
        {
            this.Size = new Size(1100, 700);
            this.BackColor = Color.FromArgb(242, 245, 250);

            // 1. Tiêu đề
            lblTitle = new Label();
            lblTitle.Text = "Tổng Quan Hệ Thống 📊";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkSlateGray;
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            // 2. Khu vực chứa 3 thẻ (Cards)
            pnCards = new Panel { Location = new Point(20, 80), Size = new Size(1060, 150) };

            // Tạo 3 Panel con
            pnCard1 = TaoPanelCard(0);
            pnCard2 = TaoPanelCard(350);
            pnCard3 = TaoPanelCard(700);

            pnCards.Controls.Add(pnCard1);
            pnCards.Controls.Add(pnCard2);
            pnCards.Controls.Add(pnCard3);
            this.Controls.Add(pnCards);

            // 3. Bảng Top Phim
            lblSubTitle = new Label { Text = "🔥 Top Phim Bán Chạy Trong Ngày", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(20, 260), AutoSize = true };
            this.Controls.Add(lblSubTitle);

            dgvTopMovies = new DataGridView();
            dgvTopMovies.Location = new Point(20, 300);
            dgvTopMovies.Size = new Size(1040, 350);
            dgvTopMovies.BackgroundColor = Color.White;
            dgvTopMovies.BorderStyle = BorderStyle.None;
            dgvTopMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopMovies.RowTemplate.Height = 40;
            dgvTopMovies.ColumnHeadersHeight = 45;
            dgvTopMovies.AllowUserToAddRows = false;
            dgvTopMovies.ReadOnly = true;
            dgvTopMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Màu sắc bảng đẹp
            dgvTopMovies.EnableHeadersVisualStyles = false;
            dgvTopMovies.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvTopMovies.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTopMovies.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvTopMovies.DefaultCellStyle.Font = new Font("Segoe UI", 11);

            this.Controls.Add(dgvTopMovies);
        }

        // Hàm tạo khung thẻ nhanh
        private Panel TaoPanelCard(int x)
        {
            return new Panel
            {
                Location = new Point(x, 0),
                Size = new Size(320, 140),
                BackColor = Color.Gray, // Sẽ được tô màu sau
                Padding = new Padding(10)
            };
        }
    }
}