using System;
using System.Windows.Forms;
using MovieBookingClient.Services;
using MovieBooking.Domain.DTOs;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_MemberInfo : UserControl
    {
        private readonly UserService _userService;

        public UC_MemberInfo()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void UC_MemberInfo_Load(object sender, EventArgs e)
        {
            try
            {
                UserProfileDTO user = await _userService.GetProfileAsync();
                if (user != null)
                {
                    txtUsername.Text = user.Username;
                    txtFullName.Text = user.FullName;
                    txtEmail.Text = user.Email;
                    txtPhone.Text = user.PhoneNumber ?? "Chưa cập nhật";
                    lblRole.Text = $"Vai trò: {user.Role}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message);
            }
        }
    }
}