using MovieBooking.Domain.DTOs;
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

// Đổi namespace để phù hợp với vị trí mới
namespace MovieBookingClient.UI.UserControls
{
    // Đổi kế thừa từ Form sang UserControl
    public partial class UC_Register : UserControl
    {
        private readonly FrmMain _mainForm; // Tham chiếu đến Form cha
        private readonly AuthService _authService;

        // Sửa Constructor để nhận FrmMain
        public UC_Register(FrmMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _authService = new AuthService();

            // Gán sự kiện
            btnRegister.Click += BtnRegister_Click;
            lblLoginLink.Click += (s, e) => _mainForm.NavigateToLogin();
            btnTabLogin.Click += (s, e) => _mainForm.NavigateToLogin();
        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var confirmPass = txtConfirmPassword.Text;
            var email = txtEmail.Text.Trim();
            var fullName = txtFullName.Text.Trim();
            var phone = txtPhoneNumber.Text.Trim();

            // 2. Validate Client-side (Kiểm tra nhanh)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Tên đăng nhập, Mật khẩu và Email là bắt buộc.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi Mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Lỗi Mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Định dạng Email không hợp lệ.", "Lỗi Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Chuẩn bị DTO
            var registerDto = new RegisterRequestDTO
            {
                Username = username,
                Password = password,
                Email = email,
                FullName = fullName,
                PhoneNumber = phone
            };

            // 4. Gọi API
            btnRegister.Enabled = false;
            btnRegister.Text = "Đang xử lý...";

            try
            {
                // Sử dụng hàm đã có trong AuthService
                var result = await _authService.RegisterAndGetDataAsync(registerDto);

                // Nếu result != null nghĩa là API trả về 201 Created
                if (result != null)
                {
                    MessageBox.Show("Đăng ký tài khoản thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Yêu cầu Form cha chuyển về trang Login
                    _mainForm.NavigateToLogin();
                }
                // Các trường hợp lỗi (409 Conflict, 400 Bad Request) đã được BaseApiService xử lý và hiển thị MessageBox
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không mong muốn: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục lại trạng thái nút bấm
                btnRegister.Enabled = true;
                btnRegister.Text = "ĐĂNG KÝ";
            }
        }
    }
}