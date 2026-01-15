using MovieBookingClient.UI.UserControls;
using MovieBookingClient.UI.UserControls.Admin;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MovieBookingClient.Forms.Admin
{
    public partial class FrmAdminDashboard : Form
    {
        // Khai báo các UserControl để tái sử dụng
        private UC_Admin_Dashboard ucDashboard;
        private UC_Admin_Movies ucMovies;
        private UC_Admin_Showtimes ucShowtimes;
        private UC_Admin_Rooms ucRooms;

        public FrmAdminDashboard()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Tải logo (nếu có)
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Assets", "admin_logo.png"); // Tên file logo cho Admin
                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Image = Image.FromFile(logoPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không thể tải logo Admin: {ex.Message}");
            }

            // Khởi tạo các UserControl
            ucDashboard = new UC_Admin_Dashboard();
            ucMovies = new UC_Admin_Movies();
            ucShowtimes = new UC_Admin_Showtimes();
            ucRooms = new UC_Admin_Rooms();

            // Gán sự kiện Click cho các nút trên Menu
            btnTrangChu.Click += (s, e) => LoadUserControl(ucDashboard, "TỔNG QUAN");
            btnPhim.Click += (s, e) => LoadUserControl(ucMovies, "QUẢN LÝ PHIM");
            btnLichChieu.Click += (s, e) => LoadUserControl(ucShowtimes, "QUẢN LÝ LỊCH CHIẾU");
            btnPhong.Click += (s, e) => LoadUserControl(ucRooms, "QUẢN LÝ PHÒNG VÀ GHẾ");

            // Tải UserControl mặc định khi khởi động
            LoadUserControl(ucDashboard, "TỔNG QUAN");
        }

        // Hàm trung tâm để tải UserControl vào Panel chính
        private void LoadUserControl(UserControl uc, string title)
        {
            pnMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnMain.Controls.Add(uc);
            lblTitle.Text = title.ToUpper(); // Cập nhật tiêu đề trên Header
        }
    }
}