using MovieBooking.Domain.DTOs; // Sử dụng DTO từ project Shared
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using MovieBookingClient.Session;
using System;
using System.Windows.Forms;

// Đổi namespace để phù hợp với vị trí mới
namespace MovieBookingClient.UI.UserControls
{
    // Đổi kế thừa từ Form sang UserControl
    public partial class UC_Login : UserControl
    {
        private readonly FrmMain _mainForm; // Tham chiếu đến Form cha
        private readonly AuthService _authService;

        // Sửa Constructor để nhận FrmMain
        public UC_Login(FrmMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _authService = new AuthService();

            // Gán sự kiện
            btnLogin.Click += BtnLogin_Click;
            btnTabRegister.Click += BtnTabRegister_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông tin thiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Đang xử lý...";

            try
            {
                var result = await _authService.LoginAsync(username, password);

                if (result != null)
                {
                    // Lưu Session
                    SessionManager.Instance.StartSession(result.Token, result.Username, result.Role);

                    // YÊU CẦU FORM CHA XỬ LÝ SAU KHI ĐĂNG NHẬP THÀNH CÔNG
                    _mainForm.OnLoginSuccess();
                }
                else
                {
                    // Lỗi đã được BaseApiService xử lý và hiển thị, chỉ cần reset UI
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không mong muốn: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "ĐĂNG NHẬP";
            }
        }

        // Sự kiện khi nhấn vào tab Đăng ký
        private void BtnTabRegister_Click(object sender, EventArgs e)
        {
            // YÊU CẦU FORM CHA CHUYỂN SANG GIAO DIỆN ĐĂNG KÝ
            _mainForm.NavigateToRegister();
        }
    }
}