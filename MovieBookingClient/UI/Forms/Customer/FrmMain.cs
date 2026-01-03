using Guna.UI2.WinForms;
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
            // Khởi tạo các UserControl
            ucMovieList = new UCMovieList(this);
            ucLogin = new UC_Login(this);
            ucRegister = new UC_Register(this);
            // --- TẠO MENU PHIM ---     
            Guna2ContextMenuStrip menuMovies = new Guna2ContextMenuStrip();
            menuMovies.RenderStyle.SelectionBackColor = Color.FromArgb(212, 33, 33);
            menuMovies.RenderStyle.SelectionForeColor = Color.White;

            ToolStripMenuItem itemNowShowing = new ToolStripMenuItem("Phim Đang Chiếu");
            itemNowShowing.Click += async (s, e) => {
                await ucMovieList.ReloadWithFilter("Now Showing");
                LoadUserControl(ucMovieList);
            };

            ToolStripMenuItem itemComingSoon = new ToolStripMenuItem("Phim Sắp Chiếu");
            itemComingSoon.Click += async (s, e) => {
                await ucMovieList.ReloadWithFilter("Coming Soon");
                LoadUserControl(ucMovieList);
            };

            menuMovies.Items.Add(itemNowShowing);
            menuMovies.Items.Add(itemComingSoon);

            // Nút "PHIM" bây giờ CHỈ có nhiệm vụ hiển thị menu
            btnMovies.Click += (s, e) => {
                menuMovies.Show(btnMovies, new Point(0, btnMovies.Height));
            };

            // Gán các sự kiện điều hướng còn lại
            btnUserAction.Click += BtnUserAction_Click;
            logoPictureBox.Click += (s, e) => NavigateToHome(); // Click logo vẫn về trang chủ
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
    }
}