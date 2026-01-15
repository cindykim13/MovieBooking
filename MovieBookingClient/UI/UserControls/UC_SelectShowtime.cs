using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs; // Sử dụng DTO chuẩn
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using MovieBookingClient.Session;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq; // Cần thiết để dùng GroupBy
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
        private MovieDetailDTO _currentMovieInfo;
        // --- State ---
        private DateTime _selectedDate;
        private List<Guna2Button> _dateButtons = new List<Guna2Button>();

        // --- Constants (Màu sắc) ---
        private readonly Color COLOR_ACTIVE_BG = Color.FromArgb(212, 33, 33);
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

            this.Load += async (s, e) => await InitDataAsync();
            btnBack.Click += (s, e) => _mainForm.NavigateToMovieDetail(_movieId);
        }

        private async Task InitDataAsync()
        {
            await LoadMovieInfoAsync();
            RenderDateBar();
            _selectedDate = DateTime.Now.Date;
            await LoadShowtimesAsync(_selectedDate);
        }

        private async Task LoadMovieInfoAsync()
        {
            try
            {
                var movie = await _movieService.GetMovieDetailAsync(_movieId);
                if (movie != null)
                {
                    _currentMovieInfo = movie;
                    lblMovieTitle.Text = movie.Title.ToUpper();
                }
            }
            catch
            {
                lblMovieTitle.Text = "THÔNG TIN PHIM";
            }
        }

        // --- PHẦN 1: LOGIC THANH CHỌN NGÀY (Giữ nguyên) ---
        private void RenderDateBar()
        {
            flpDates.Controls.Clear();
            _dateButtons.Clear();
            DateTime startDate = DateTime.Now;

            for (int i = 0; i < 14; i++)
            {
                DateTime date = startDate.AddDays(i);
                Guna2Button btnDate = new Guna2Button();
                btnDate.Size = new Size(80, 50);
                btnDate.BorderRadius = 4;
                btnDate.BorderThickness = 1;
                btnDate.BorderColor = COLOR_BORDER;
                btnDate.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnDate.Cursor = Cursors.Hand;
                btnDate.Margin = new Padding(0, 0, 10, 0);

                string dayName = date.ToString("ddd", new CultureInfo("vi-VN"));
                string dayMonth = date.ToString("dd/MM");
                btnDate.Text = $"{dayName}\n{dayMonth}";
                btnDate.Tag = date.Date;

                btnDate.Click += async (s, e) => {
                    await OnDateSelected(btnDate);
                };

                UpdateButtonState(btnDate, false);
                _dateButtons.Add(btnDate);
                flpDates.Controls.Add(btnDate);
            }

            if (_dateButtons.Count > 0) UpdateButtonState(_dateButtons[0], true);
        }

        private async Task OnDateSelected(Guna2Button clickedBtn)
        {
            foreach (var btn in _dateButtons) UpdateButtonState(btn, false);
            UpdateButtonState(clickedBtn, true);
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

        // --- PHẦN 2: LOGIC HIỂN THỊ LỊCH CHIẾU (ĐÃ SỬA LINQ GROUPBY) ---
        private async Task LoadShowtimesAsync(DateTime date)
        {
            flpCinemas.Controls.Clear();
            lblNoShowtimes.Visible = false;

            // Hiển thị loading nhẹ
            Label lblLoading = new Label { Text = "Đang tải...", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Italic) };
            flpCinemas.Controls.Add(lblLoading);

            try
            {
                // 1. Lấy danh sách phẳng (Flat List) từ API
                var flatList = await _showtimeService.GetShowtimesByMovieAsync(_movieId, date);

                flpCinemas.Controls.Clear(); // Xóa loading

                if (flatList == null || !flatList.Any())
                {
                    lblNoShowtimes.Visible = true;
                    return;
                }

                // 2. [QUAN TRỌNG] Dùng LINQ để gom nhóm danh sách phẳng lại
                // Vì DTO không có CinemaName, ta gom nhóm theo RoomName hoặc giả lập 1 Rạp duy nhất
                // Ở đây mình gom tất cả vào 1 nhóm "Hệ thống Rạp" để code UI chạy được vòng lặp
                var groupedShowtimes = flatList
                    .GroupBy(s => "Galaxy Cinema Center") // Key nhóm (Tên rạp giả lập)
                    .Select(g => new
                    {
                        CinemaName = g.Key,
                        Address = "TP. Hồ Chí Minh",
                        // Sắp xếp các suất chiếu trong nhóm theo giờ
                        Showtimes = g.OrderBy(s => s.StartTime).ToList()
                    })
                    .ToList();

                // 3. Render giao diện dựa trên danh sách đã gom nhóm
                foreach (var cinemaGroup in groupedShowtimes)
                {
                    // --- Tạo Panel Rạp ---
                    Guna2Panel pnlCinema = new Guna2Panel
                    {
                        Width = flpCinemas.Width - 30,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        FillColor = Color.White,
                        BorderColor = Color.Gainsboro,
                        BorderThickness = 1,
                        BorderRadius = 5,
                        Margin = new Padding(0, 0, 0, 20)
                    };

                    Label lblName = new Label
                    {
                        Text = cinemaGroup.CinemaName, // Lấy từ Key nhóm
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Location = new Point(15, 15),
                        AutoSize = true
                    };
                    pnlCinema.Controls.Add(lblName);

                    Label lblAddr = new Label
                    {
                        Text = cinemaGroup.Address,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        ForeColor = Color.Gray,
                        Location = new Point(15, 40),
                        AutoSize = true
                    };
                    pnlCinema.Controls.Add(lblAddr);

                    FlowLayoutPanel flpTimes = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        MaximumSize = new Size(pnlCinema.Width - 30, 0),
                        Location = new Point(15, 70),
                        Padding = new Padding(0, 0, 0, 15)
                    };

                    // --- Render Nút Suất Chiếu ---
                    // Duyệt qua danh sách con trong nhóm
                    foreach (var showtime in cinemaGroup.Showtimes)
                    {
                        Guna2Button btnTime = new Guna2Button
                        {
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
                            Tag = showtime // Lưu object ShowtimeDTO gốc vào Tag
                        };

                        btnTime.Click += (s, e) =>
                        {
                            if (!SessionManager.Instance.IsLoggedIn)
                            {
                                MessageBox.Show("Vui lòng đăng nhập để đặt vé.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _mainForm.NavigateToLogin();
                                return;
                            }

                            var selected = (ShowtimeDTO)btnTime.Tag;

                            // Đóng gói dữ liệu chuyển sang màn hình chọn ghế
                            // Lưu ý: Đảm bảo BookingContextDTO đã có trong Domain
                            var context = new BookingContextDTO
                            {
                                ShowtimeId = selected.ShowtimeId, // DTO gốc thường là Id, không phải ShowtimeId
                                CinemaName = cinemaGroup.CinemaName,
                                RoomName = selected.RoomName ?? "Phòng Chiếu",
                                MovieTitle = _currentMovieInfo?.Title ?? "Phim",
                                // Price = selected.Price,
                               //  Time = selected.StartTime
                            };

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
                lblNoShowtimes.Visible = true;
                // MessageBox.Show("Lỗi: " + ex.Message); // Debug nếu cần
            }
        }
    }
}