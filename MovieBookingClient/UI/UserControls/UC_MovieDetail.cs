using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_MovieDetail : UserControl
    {
        private readonly int _movieId;
        private readonly FrmMain _mainForm;
        private readonly MovieService _movieService;
        // [SỬA LỖI]: Xóa bỏ _showtimeService vì không còn dùng ở đây nữa

        public UC_MovieDetail(FrmMain mainForm, int movieId)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _movieId = movieId;
            _movieService = new MovieService();

            this.Load += async (s, e) => await LoadMovieInfoAsync();

            // Sự kiện Mua vé -> Chuyển sang màn hình Chọn Lịch (UC_SelectShowtime)
            btnBuyTicket.Click += (s, e) => _mainForm.NavigateToSelectShowtime(_movieId);
            // Sự kiện quay lại
            btnBack.Click += (s, e) => _mainForm.NavigateToHome();
        }

        // [SỬA LỖI]: Xóa bỏ hàm InitDataAsync và LoadShowtimesAsync cũ

        private async Task LoadMovieInfoAsync()
        {
            try
            {
                var movie = await _movieService.GetMovieDetailAsync(_movieId);
                if (movie != null)
                {
                    lblTitle.Text = movie.Title.ToUpper();
                    lblGenre.Text = "Thể loại: " + (movie.Genres != null ? string.Join(", ", movie.Genres) : "Đang cập nhật");
                    lblDuration.Text = $"Thời lượng: {movie.Duration} phút | Khởi chiếu: {movie.ReleaseYear}";
                    lblDirector.Text = "Đạo diễn: " + (movie.Director ?? "Đang cập nhật");
                    lblCast.Text = "Diễn viên: " + (movie.Casts != null ? string.Join(", ", movie.Casts) : "Đang cập nhật");
                    lblStoryLine.Text = movie.StoryLine ?? "Chưa có mô tả.";

                    // Tải ảnh Poster
                    LoadImageFromUrl(movie.PosterUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin phim: " + ex.Message);
            }
        }

        private async void LoadImageFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var imageBytes = await httpClient.GetByteArrayAsync(url);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        picPoster.Image = Image.FromStream(ms);
                    }
                }
            }
            catch { /* Bỏ qua lỗi ảnh để không crash app */ }
        }
    }
}