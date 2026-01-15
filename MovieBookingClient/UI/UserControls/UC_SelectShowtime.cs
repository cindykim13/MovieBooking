using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using MovieBookingClient.Session;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_SelectShowtime : UserControl
    {
        // --- Dependencies ---
        private readonly FrmMain _mainForm;
        private readonly int _movieId;
        private readonly ShowtimeService _showtimeService;
        private readonly MovieService _movieService;
        // --- State ---
        private DateTime _selectedDate;
        private List<Guna2Button> _dateButtons = new List<Guna2Button>();
        private DateTime _currentWeekStartDate;
        private MovieDetailDTO _currentMovieInfo; // Thêm biến toàn cục để lưu info phim

        // --- Constants (Màu sắc) ---
        private readonly Color COLOR_ACTIVE_BG = Color.FromArgb(212, 33, 33); // Đỏ CGV
        private readonly Color COLOR_ACTIVE_TEXT = Color.White;
        private readonly Color COLOR_INACTIVE_BG = Color.White;
        private readonly Color COLOR_INACTIVE_TEXT = Color.Black;
        private readonly Color COLOR_BORDER = Color.Silver;

        public UC_SelectShowtime(FrmMain mainForm, int movieId)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _movieId = movieId;
            _showtimeService = new ShowtimeService();
            _movieService = new MovieService();

            // Gán sự kiện
            this.Load += async (s, e) => await InitDataAsync();
            // Logic nút Back: Quay lại trang chi tiết phim để người dùng có thể xem lại nội dung
            btnBack.Click += (s, e) => _mainForm.NavigateToMovieDetail(_movieId);
            // [MỚI] Gán sự kiện cho nút điều hướng tuần
            btnNextWeek.Click += (s, e) => ChangeWeek(7);
            btnPreviousWeek.Click += (s, e) => ChangeWeek(-7);

        }

        private async Task InitDataAsync()
        {
            // 1. Lấy thông tin tên phim để hiển thị lên Header
            await LoadMovieInfoAsync();

            // 2. Thiết lập ngày bắt đầu của tuần hiện tại và vẽ thanh ngày
            _currentWeekStartDate = DateTime.Now.Date;
            RenderDateBar();

            // 3. Tải lịch chiếu cho ngày mặc định (Hôm nay)
            _selectedDate = DateTime.Now.Date;
            await LoadShowtimesAsync(_selectedDate);

            // Cập nhật trạng thái nút Back
            UpdateWeekNavigationButtons();
        }

        private async Task LoadMovieInfoAsync()
        {
            try
            {
                var movie = await _movieService.GetMovieDetailAsync(_movieId);
                if (movie != null)
                {
                    _currentMovieInfo = movie; // [QUAN TRỌNG] Lưu lại để dùng sau
                    lblMovieTitle.Text = movie.Title.ToUpper();
                }
            }
            catch
            {
                lblMovieTitle.Text = "THÔNG TIN PHIM";
            }
        }

        // --- PHẦN 1: LOGIC THANH CHỌN NGÀY ---
        private void RenderDateBar()
        {
            flpDates.Controls.Clear();
            _dateButtons.Clear();

            // Tạo nút cho 7 ngày, bắt đầu từ _currentWeekStartDate
            for (int i = 0; i < 7; i++)
            {
                DateTime date = _currentWeekStartDate.AddDays(i);

                // Không tạo nút cho các ngày trong quá khứ
                if (date < DateTime.Now.Date) continue;

                Guna2Button btnDate = new Guna2Button();
                btnDate.Size = new Size(80, 60); // Tăng chiều cao cho đẹp
                btnDate.BorderRadius = 5;
                btnDate.BorderThickness = 1;
                btnDate.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnDate.Cursor = Cursors.Hand;
                btnDate.Margin = new Padding(5); // Thêm margin

                string dayName = date.ToString("ddd", new CultureInfo("vi-VN"));
                string dayMonth = date.ToString("dd/MM");
                btnDate.Text = $"{dayName}\n{dayMonth}";
                btnDate.Tag = date.Date;

                btnDate.Click += async (s, e) => await OnDateSelected(btnDate);

                // Highlight ngày đang được chọn (_selectedDate)
                UpdateButtonState(btnDate, date.Date == _selectedDate.Date);

                _dateButtons.Add(btnDate);
                flpDates.Controls.Add(btnDate);
            }
        }
        private void ChangeWeek(int daysToAdd)
        {
            _currentWeekStartDate = _currentWeekStartDate.AddDays(daysToAdd);
            RenderDateBar();
            UpdateWeekNavigationButtons();
        }

        private void UpdateWeekNavigationButtons()
        {
            // Chỉ vô hiệu hóa nút "Back" khi tuần hiện tại là tuần của ngày hôm nay
            btnPreviousWeek.Enabled = (_currentWeekStartDate.Date > DateTime.Now.Date);
        }

        private async Task OnDateSelected(Guna2Button clickedBtn)
        {
            // Reset style tất cả nút
            foreach (var btn in _dateButtons)
            {
                UpdateButtonState(btn, false);
            }

            // Highlight nút được chọn
            UpdateButtonState(clickedBtn, true);

            // Cập nhật ngày và tải lại lịch
            _selectedDate = (DateTime)clickedBtn.Tag;
            await LoadShowtimesAsync(_selectedDate);
        }

        private void UpdateButtonState(Guna2Button btn, bool isActive)
        {
            if (isActive)
            {
                btn.FillColor = COLOR_ACTIVE_BG;
                btn.ForeColor = COLOR_ACTIVE_TEXT;
                btn.BorderColor = COLOR_ACTIVE_BG;
            }
            else
            {
                btn.FillColor = COLOR_INACTIVE_BG;
                btn.ForeColor = COLOR_INACTIVE_TEXT;
                btn.BorderColor = COLOR_BORDER;
            }
        }

        // --- PHẦN 2: LOGIC HIỂN THỊ LỊCH CHIẾU ---
        private async Task LoadShowtimesAsync(DateTime date)
        {
            flpCinemas.Controls.Clear();
            lblNoShowtimes.Visible = false;

            // Hiển thị Loading (tạm thời add label vào flpCinemas)
            Label lblLoading = new Label { Text = "Đang tải lịch chiếu...", AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Italic) };
            flpCinemas.Controls.Add(lblLoading);

            try
            {
                var cinemaShowtimes = await _showtimeService.GetShowtimesByMovieAsync(_movieId, date);

                flpCinemas.Controls.Clear(); // Xóa loading

                if (cinemaShowtimes == null || !cinemaShowtimes.Any())
                {
                    lblNoShowtimes.Visible = true;
                    return;
                }

                // Render danh sách Rạp
                foreach (var cinema in cinemaShowtimes)
                {
                    // Panel chứa 1 Rạp
                    Guna2Panel pnlCinema = new Guna2Panel
                    {
                        Width = flpCinemas.Width - 30, // Trừ scrollbar
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        FillColor = Color.White,
                        BorderColor = Color.Gainsboro,
                        BorderThickness = 1,
                        BorderRadius = 5,
                        Margin = new Padding(0, 0, 0, 20)
                    };

                    // Tên Rạp
                    Label lblName = new Label
                    {
                        Text = cinema.CinemaName,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = Color.Black,
                        AutoSize = true,
                        Location = new Point(15, 15)
                    };
                    pnlCinema.Controls.Add(lblName);

                    // Địa chỉ Rạp
                    Label lblAddr = new Label
                    {
                        Text = cinema.Address,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Location = new Point(15, 40)
                    };
                    pnlCinema.Controls.Add(lblAddr);

                    // Container chứa các nút giờ (FlowLayout bên trong Panel Rạp)
                    FlowLayoutPanel flpTimes = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        MaximumSize = new Size(pnlCinema.Width - 30, 0), // Giới hạn chiều rộng để xuống dòng
                        Location = new Point(15, 70),
                        Padding = new Padding(0, 0, 0, 15)
                    };

                    // Render các nút Suất chiếu
                    foreach (var showtime in cinema.Showtimes)
                    {
                        Guna2Button btnTime = new Guna2Button
                        {
                            // Hiển thị giờ (VD: 19:30)
                            Text = showtime.StartTime.ToString("HH:mm"),
                            Width = 90,
                            Height = 35,
                            BorderRadius = 4,
                            BorderThickness = 1,
                            BorderColor = Color.Silver,
                            FillColor = Color.WhiteSmoke,
                            ForeColor = Color.Black,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Margin = new Padding(0, 0, 10, 10),
                            Cursor = Cursors.Hand,
                            Tag = showtime  // Lưu ID để dùng khi click
                        };

                        // Sự kiện khi chọn giờ -> Chuyển sang Đặt vé
                        btnTime.Click += (s, e) =>
                        {
                            if (!SessionManager.Instance.IsLoggedIn)
                            {
                                MessageBox.Show("Vui lòng đăng nhập để chọn ghế.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _mainForm.NavigateToLogin();
                                return;
                            }

                            // [FIX LỖI QUAN TRỌNG]: Ép kiểu Tag về đúng ShowtimeDto
                            var selectedShowtime = (ShowtimeDTO)btnTime.Tag;

                            // Đóng gói dữ liệu context để gửi đi
                            var context = new BookingContextDTO
                            {
                                ShowtimeId = selectedShowtime.ShowtimeId,
                                ShowTime = selectedShowtime.StartTime,
                                CinemaName = cinema.CinemaName,
                                RoomName = selectedShowtime.RoomName,
                                MovieTitle = _currentMovieInfo?.Title ?? "Phim",
                                PosterUrl = _currentMovieInfo?.PosterUrl
                            };

                            // Kiểm tra đăng nhập
                            if (!SessionManager.Instance.IsLoggedIn)
                            {
                                MessageBox.Show("Vui lòng đăng nhập để chọn ghế và đặt vé.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Yêu cầu FrmMain mở trang Login, và "dặn" rằng sau khi login thành công
                                // thì thực hiện hành động NavigateToBooking với context đã chuẩn bị
                                _mainForm.NavigateToLogin(() => _mainForm.NavigateToBooking(context));

                                return;
                            }

                            // Nếu đã đăng nhập, chuyển thẳng đến trang đặt vé
                            _mainForm.NavigateToBooking(context);
                        };

                        flpTimes.Controls.Add(btnTime);
                    }

                    pnlCinema.Controls.Add(flpTimes);
                    flpCinemas.Controls.Add(pnlCinema);
                }
            }
            catch (Exception ex)
            {
                flpCinemas.Controls.Clear();
                MessageBox.Show($"Lỗi tải lịch chiếu: {ex.Message}");
            }
        }
    }
}