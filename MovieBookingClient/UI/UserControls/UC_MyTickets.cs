using System;
using System.Drawing; // Thêm thư viện này để dùng Color
using System.Windows.Forms;
using MovieBookingClient.Services;
using MovieBookingClient.UI.UserControls.Items;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_MyTickets : UserControl
    {
        private readonly UserService _userService;

        public UC_MyTickets()
        {
            InitializeComponent();
            _userService = new UserService();

            // FIX: Đảm bảo khi rê chuột vào vùng danh sách thì lăn chuột được ngay
            flowPanelTickets.MouseEnter += (s, e) => { flowPanelTickets.Focus(); };
        }

        private async void UC_MyTickets_Load(object sender, EventArgs e)
        {
            // FIX: Tạm dừng vẽ giao diện để xử lý dữ liệu nhanh hơn và tránh lỗi hiển thị
            flowPanelTickets.SuspendLayout();

            try
            {
                // Xóa controls cũ sạch sẽ
                flowPanelTickets.Controls.Clear();

                var history = await _userService.GetBookingHistoryAsync();

                if (history != null && history.Count > 0)
                {
                    foreach (var ticket in history)
                    {
                        var item = new UC_TicketItem(ticket);

                        // FIX QUAN TRỌNG: 
                        // Set chiều rộng item bằng chiều rộng panel trừ đi 25px (để chừa chỗ cho thanh cuộn dọc)
                        // Điều này giúp tránh hiện thanh cuộn ngang xấu xí
                        item.Width = flowPanelTickets.ClientSize.Width - 25;

                        // Đảm bảo item không bị co giãn lung tung
                        item.Margin = new Padding(0, 0, 0, 10); // Cách dưới 1 chút cho đẹp

                        flowPanelTickets.Controls.Add(item);
                    }
                }
                else
                {
                    // Hiển thị thông báo nếu không có vé
                    Label lblEmpty = new Label();
                    lblEmpty.Text = "Bạn chưa mua vé nào.";
                    lblEmpty.AutoSize = true;
                    lblEmpty.Font = new System.Drawing.Font("Segoe UI", 14);
                    lblEmpty.ForeColor = Color.Gray;
                    lblEmpty.Padding = new Padding(20); // Cách lề một chút
                    flowPanelTickets.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
            finally
            {
                // FIX: Kích hoạt lại việc vẽ giao diện và ép buộc tính toán lại thanh cuộn
                flowPanelTickets.ResumeLayout(true);
                flowPanelTickets.PerformLayout();
            }
        }
    }
}