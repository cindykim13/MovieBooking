using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using MovieBookingClient.Session; // <--- ĐÃ MỞ LẠI: Để dùng SessionManager
using MovieBookingClient.Global;
using System;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_Login : UserControl
    {
        private readonly FrmMain _mainForm;
        private readonly AuthService _authService;

        public UC_Login(FrmMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _authService = new AuthService();

            btnLogin.Click += BtnLogin_Click;
            btnTabRegister.Click += BtnTabRegister_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Đang xử lý...";

            try
            {
                var result = await _authService.LoginAsync(username, password);

                if (result != null)
                {
                    // 1. LƯU VÀO SESSIONMANAGER (BẮT BUỘC): Để BaseApiService có Token đi xóa phim
                    SessionManager.Instance.StartSession(result.Token, result.Username, result.Role);

                    // 2. Lưu vào UserSession (Dùng cho các mục đích hiển thị khác của bạn)
                    UserSession.AccessToken = result.Token;
                    UserSession.CurrentUsername = result.Username;
                    UserSession.CurrentRole = result.Role;

                    // --- PHÂN QUYỀN ---
                    if (string.Equals(result.Role, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Xin chào Admin: {result.Username}", "Thành công");
                        _mainForm.NavigateToAdminDashboard();
                    }
                    else
                    {
                        MessageBox.Show($"Đăng nhập thành công!", "Thành công");
                        _mainForm.NavigateToCustomerHome();
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "ĐĂNG NHẬP";
            }
        }

        private void BtnTabRegister_Click(object sender, EventArgs e)
        {
            _mainForm.NavigateToRegister();
        }
    }
}