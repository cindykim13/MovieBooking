using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Forms.Admin;
using MovieBookingClient.Session;
using MovieBookingClient.UI.UserControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MovieBookingClient.Forms.Customer
{
    public partial class FrmMain : Form
    {
        private UCMovieList ucMovieList;
        private UC_Login ucLogin;
        private UC_Register ucRegister;
        public FrmMain()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // 1. Tải Logo (Giữ nguyên)
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Assets", "logo4.png");
                if (File.Exists(logoPath))
                {
                    logoPictureBox.Image = Image.FromFile(logoPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không thể tải logo: {ex.Message}");
            }

            // 2. Khởi tạo các UserControl (Giữ nguyên)
            ucMovieList = new UCMovieList(this);
            ucLogin = new UC_Login(this);
            ucRegister = new UC_Register(this);

            // 3. TẠO MENU PHIM (Giữ nguyên logic của bạn)
            Guna2ContextMenuStrip menuMovies = new Guna2ContextMenuStrip();
            menuMovies.RenderStyle.SelectionBackColor = Color.FromArgb(212, 33, 33);
            menuMovies.RenderStyle.SelectionForeColor = Color.White;

            ToolStripMenuItem itemNowShowing = new ToolStripMenuItem("Phim Đang Chiếu");
            itemNowShowing.Click += async (s, e) => {
                // Đảm bảo đang ở màn hình danh sách
                LoadUserControl(ucMovieList);
                // Gọi hàm lọc với chuỗi chính xác khớp với Database
                await ucMovieList.FilterByStatus("Now Showing");
            };

            ToolStripMenuItem itemComingSoon = new ToolStripMenuItem("Phim Sắp Chiếu");
            itemComingSoon.Click += async (s, e) => {
                LoadUserControl(ucMovieList);
                await ucMovieList.FilterByStatus("Coming Soon");
            };

            menuMovies.Items.Add(itemNowShowing);
            menuMovies.Items.Add(itemComingSoon);

            // Nút "PHIM" bây giờ CHỈ có nhiệm vụ hiển thị menu
            btnMovies.Click += (s, e) => {
                menuMovies.Show(btnMovies, new Point(0, btnMovies.Height));
            };

            // 4. SỰ KIỆN NÚT USER (Giữ nguyên)
            btnUserAction.Click += BtnUserAction_Click;

            // 5. [SỬA LỖI LOGIC] LOGO & NÚT MUA VÉ -> RESET VỀ TRANG CHỦ
            // Khi nhấn Logo hoặc Mua vé ngay, phải tải lại danh sách phim mặc định
            logoPictureBox.Click += async (s, e) => {
                LoadUserControl(ucMovieList); // Chuyển về view danh sách
                await ucMovieList.ResetToDefault(); // Reset bộ lọc
            };

            btnBuyTicket.Click += async (s, e) => {
                LoadUserControl(ucMovieList);
                await ucMovieList.FilterByStatus("Now Showing");
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateUserStatusUI();

            // [FIX LỖI LUỒNG ĐIỀU HƯỚNG]: Mặc định luôn tải trang danh sách phim
            NavigateToHome();
        }

        public void LoadUserControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        public void UpdateUserStatusUI()
        {
            if (SessionManager.Instance.IsLoggedIn)
            {
                btnUserAction.Text = $"XIN CHÀO, {SessionManager.Instance.Username?.ToUpper()}";
            }
            else
            {
                btnUserAction.Text = "ĐĂNG NHẬP / ĐĂNG KÝ";
            }
        }

        private void BtnUserAction_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.IsLoggedIn)
            {
                // Logic đăng xuất
                SessionManager.Instance.EndSession();
                UpdateUserStatusUI();
                NavigateToHome(); // Tải lại trang chủ sau khi đăng xuất
            }
            else
            {
                // Chuyển sang giao diện đăng nhập
                NavigateToLogin();
            }
        }

        // --- CÁC HÀM ĐIỀU HƯỚNG CÔNG KHAI ---

        public void NavigateToRegister()
        {
            LoadUserControl(ucRegister);
        }

        public void NavigateToLogin()
        {
            LoadUserControl(ucLogin);
        }

        public void NavigateToHome()
        {
            // [FIX LỖI NGHIỆP VỤ]: Bỏ kiểm tra đăng nhập khi xem phim
            LoadUserControl(ucMovieList);
        }
        public void NavigateToMovieDetail(int movieId)
        {
            // Tạo UserControl chi tiết phim, truyền ID vào
            UC_MovieDetail detailControl = new UC_MovieDetail(this, movieId);

            // Tải vào panel chính
            LoadUserControl(detailControl);
        }

        public void NavigateToSelectShowtime(int movieId)
        {
            UC_SelectShowtime showtimeControl = new UC_SelectShowtime(this, movieId);
            LoadUserControl(showtimeControl);
        }

        public void NavigateToBooking(BookingContextDTO contextInfo) // Sửa từ int sang BookingContextDTO
        {
            // Tạo UserControl Đặt vé và truyền toàn bộ đối tượng context vào
            UC_Booking bookingControl = new UC_Booking(this, contextInfo);
            LoadUserControl(bookingControl);
        }

        public void OnLoginSuccess()
        {
            UpdateUserStatusUI();
            if (string.Equals(SessionManager.Instance.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                this.Hide();
                var adminForm = new FrmAdminDashboard();
                adminForm.FormClosed += (s, args) => this.Close();
                adminForm.Show();
            }
            else
            {
                NavigateToHome();
            }
        }

public void NavigateToAdminDashboard()
        {
            this.Hide();
            MovieBookingClient.Forms.Admin.FrmAdminDashboard adminForm = new MovieBookingClient.Forms.Admin.FrmAdminDashboard();
            adminForm.FormClosed += (s, args) => this.Close();
            adminForm.Show();
        }

        public void NavigateToCustomerHome()
        {
            NavigateToHome();
            UpdateUserStatusUI();
        }
    }
}
