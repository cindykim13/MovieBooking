using MovieBooking.Domain.DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UCMovieCard : UserControl
    {
        private MovieDTO _movie;

        // Định nghĩa một event để thông báo cho Form cha khi nút "Mua vé" được nhấn
        public event EventHandler<int> BuyTicketClicked;

        public UCMovieCard()
        {
            InitializeComponent();
            btnBuyTicket.Click += (s, e) => BuyTicketClicked?.Invoke(this, _movie.MovieId);
        }

        // Phương thức công khai để thiết lập dữ liệu cho Card
        public void SetMovieDetails(MovieDTO movie)
        {
            _movie = movie;

            // Gán dữ liệu vào các control
            lblTitle.Text = movie.Title;
            lblGenre.Text = movie.Genres;

            // Tải ảnh từ URL một cách bất đồng bộ
            LoadImageFromUrl(movie.PosterUrl);
        }

        // Hàm tải ảnh bất đồng bộ để không làm "đơ" giao diện
        private async void LoadImageFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                // Nếu không có URL, có thể hiển thị ảnh mặc định
                // picPoster.Image = Properties.Resources.DefaultImage;
                return;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Tải dữ liệu ảnh dưới dạng byte array
                    var imageBytes = await httpClient.GetByteArrayAsync(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        // Chuyển đổi byte array thành Image
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            picPoster.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ví dụ: link ảnh hỏng, không có mạng)
                // Ghi log lỗi và có thể hiển thị ảnh mặc định
                Console.WriteLine($"Lỗi tải ảnh: {ex.Message}");
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}