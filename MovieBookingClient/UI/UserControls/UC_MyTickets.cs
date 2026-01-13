using System;
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
        }

        private async void UC_MyTickets_Load(object sender, EventArgs e)
        {
            try
            {
                var history = await _userService.GetBookingHistoryAsync();
                flowPanelTickets.Controls.Clear();

                if (history != null && history.Count > 0)
                {
                    foreach (var ticket in history)
                    {
                        var item = new UC_TicketItem(ticket);
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
                    flowPanelTickets.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }
    }
}