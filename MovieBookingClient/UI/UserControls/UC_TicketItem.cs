using System;
using System.Drawing;
using System.Windows.Forms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.UI.Forms;

namespace MovieBookingClient.UI.UserControls.Items
{
    public partial class UC_TicketItem : UserControl
    {
        private BookingHistoryDTO _ticketData;

        public UC_TicketItem(BookingHistoryDTO ticket)
        {
            InitializeComponent();
            _ticketData = ticket;

            // Set data đơn giản
            lblMovieTitle.Text = ticket.MovieTitle;
            lblPrice.Text = $"{ticket.TotalAmount:N0} đ";

            // Sự kiện Click cho toàn bộ Control
            this.Click += Item_Click;
            lblMovieTitle.Click += Item_Click;
            lblPrice.Click += Item_Click;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            // Mở form chi tiết dưới dạng Dialog (Modal)
            using (var frmDetail = new FrmTicketDetail(_ticketData))
            {
                frmDetail.ShowDialog();
            }
        }
    }
}