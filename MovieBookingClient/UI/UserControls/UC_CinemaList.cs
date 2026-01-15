using MovieBooking.Domain.DTOs; // Đảm bảo using đúng namespace của DTO
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_CinemaList : UserControl
    {
        private readonly FrmMain _mainForm;
        private readonly CinemaService _cinemaService;

        public UC_CinemaList(FrmMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _cinemaService = new CinemaService();

            // Gán sự kiện Load để tự động tải dữ liệu khi UserControl được hiển thị
            this.Load += async (sender, e) => await LoadCinemasAsync();
        }

        private async Task LoadCinemasAsync()
        {
            // 1. Hiển thị trạng thái đang tải
            flpMainContent.Controls.Clear();
            var loadingLabel = new Label
            {
                Text = "Đang tải danh sách rạp...",
                Font = new Font("Segoe UI", 14, FontStyle.Italic),
                AutoSize = false,
                Width = this.Width,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 50, 0, 0)
            };
            flpMainContent.Controls.Add(loadingLabel);

            try
            {
                // 2. Gọi API để lấy danh sách rạp
                var cinemas = await _cinemaService.GetAllCinemasAsync();

                // Xóa thông báo đang tải
                flpMainContent.Controls.Clear();

                if (cinemas != null && cinemas.Any())
                {
                    // 3. Gom nhóm dữ liệu theo Thành phố (City)
                    var cinemasByCity = cinemas.GroupBy(c => c.City);

                    // 4. Vẽ giao diện động dựa trên dữ liệu đã gom nhóm
                    foreach (var group in cinemasByCity)
                    {
                        // Tạo tiêu đề cho mỗi thành phố
                        var cityLabel = new Label
                        {
                            Text = group.Key.ToUpper(),
                            Font = new Font("Segoe UI", 16, FontStyle.Bold),
                            ForeColor = Color.FromArgb(212, 33, 33), // Màu đỏ CGV
                            AutoSize = true,
                            Margin = new Padding(0, 20, 0, 10) // Khoảng cách trên và dưới
                        };
                        flpMainContent.Controls.Add(cityLabel);

                        // Lặp qua từng rạp trong thành phố
                        foreach (var cinema in group)
                        {
                            // Tạo một Panel để chứa thông tin một rạp
                            var cinemaPanel = new Panel
                            {
                                Width = flpMainContent.Width - 50, // Chiều rộng panel
                                AutoSize = true,
                                Padding = new Padding(0, 0, 0, 15) // Khoảng cách dưới
                            };

                            // Tên rạp
                            var cinemaNameLabel = new Label
                            {
                                Text = cinema.Name,
                                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                                ForeColor = Color.Black,
                                AutoSize = true,
                                Location = new Point(0, 0)
                            };

                            // Địa chỉ rạp
                            var cinemaAddressLabel = new Label
                            {
                                Text = cinema.Address,
                                Font = new Font("Segoe UI", 10),
                                ForeColor = Color.Gray,
                                AutoSize = true,
                                Location = new Point(0, cinemaNameLabel.Height) // Nằm ngay dưới tên rạp
                            };

                            cinemaPanel.Controls.Add(cinemaNameLabel);
                            cinemaPanel.Controls.Add(cinemaAddressLabel);
                            flpMainContent.Controls.Add(cinemaPanel);
                        }
                    }
                }
                else
                {
                    // Hiển thị thông báo nếu không có dữ liệu
                    flpMainContent.Controls.Add(new Label { Text = "Không có rạp nào để hiển thị.", Font = new Font("Segoe UI", 12) });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu API không hoạt động
                flpMainContent.Controls.Clear();
                MessageBox.Show("Lỗi tải danh sách rạp: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}