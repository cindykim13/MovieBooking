using System;
using System.Drawing;
using System.Windows.Forms;
using MovieBooking.Domain.DTOs;

namespace MovieBookingClient.UI.Forms
{
    public partial class TicketDetail : Form
    {
        public TicketDetail(BookingHistoryDTO ticket)
        {
            InitializeComponent();
            LoadData(ticket);
        }

        private void LoadData(BookingHistoryDTO ticket)
        {
            lblMovieTitle.Text = ticket.MovieTitle;
            lblCinema.Text = $"Rạp: {ticket.CinemaName} - {ticket.RoomName}";
            lblShowTime.Text = $"Suất chiếu: {ticket.ShowTime:dd/MM/yyyy HH:mm}";
            lblSeats.Text = $"Ghế: {string.Join(", ", ticket.Seats)}";
            lblPrice.Text = $"Tổng tiền: {ticket.TotalAmount:N0} VNĐ";
            lblStatus.Text = $"Trạng thái: {ticket.Status}";
            lblPayment.Text = $"Thanh toán: {ticket.PaymentMethod}";

            if (!string.IsNullOrEmpty(ticket.PosterUrl) && Uri.IsWellFormedUriString(ticket.PosterUrl, UriKind.Absolute))
            {
                try
                {
                    picPoster.Load(ticket.PosterUrl);
                }
                catch (Exception)
                {
                    picPoster.Image = null;
                    picPoster.FillColor = Color.LightGray;
                }
            }
            else
            {
                picPoster.Image = null;
                picPoster.FillColor = Color.LightGray;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}