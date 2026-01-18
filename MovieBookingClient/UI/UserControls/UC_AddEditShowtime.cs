using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_AddEditShowtime : UserControl
    {
        // Định nghĩa các sự kiện để giao tiếp với UserControl cha
        public event EventHandler OnSaved;
        public event EventHandler OnCancelled;

        private readonly int? _showtimeId;
        private readonly AdminShowtimeService _adminShowtimeService;
        private readonly MovieService _movieService;
        private readonly CinemaService _cinemaService;

        public UC_AddEditShowtime(int? showtimeId)
        {
            InitializeComponent();
            _showtimeId = showtimeId;
            _adminShowtimeService = new AdminShowtimeService();
            _movieService = new MovieService();
            _cinemaService = new CinemaService();

            SetupEvents();
            this.Load += async (s, e) => await InitializeForm();
        }

        private void SetupEvents()
        {
            btnSave.Click += async (s, e) => await SaveShowtime();
            btnCancel.Click += (s, e) => OnCancelled?.Invoke(this, EventArgs.Empty);
            cboCinema.SelectedIndexChanged += async (s, e) => {
                if (cboCinema.SelectedValue is int cinemaId && cinemaId > 0)
                {
                    await LoadRoomsByCinema(cinemaId);
                }
                else
                {
                    cboRoom.DataSource = null; // Xóa danh sách phòng nếu không chọn rạp nào
                }
            };
        }
        private async void CboCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCinema.SelectedValue is int cinemaId && cinemaId > 0)
            {
                await LoadRoomsByCinema(cinemaId);
            }
            else
            {
                cboRoom.DataSource = null;
            }
        }

        private async Task InitializeForm()
        {
            // Tải dữ liệu cho các ComboBox
            await LoadMovies();
            await LoadCinemas();

            if (_showtimeId.HasValue)
            {
                lblTitle.Text = "CẬP NHẬT LỊCH CHIẾU";
                await LoadShowtimeDetails(_showtimeId.Value);
            }
            else
            {
                lblTitle.Text = "THÊM MỚI LỊCH CHIẾU";
                DateTime defaultTime = DateTime.Now.AddHours(1);
                dtpDate.Value = defaultTime.Date;
                dtpTime.Value = defaultTime;
            }
        }

        private async Task LoadMovies()
        {
            try
            {
                var movies = await _movieService.SearchMoviesAsync(null, "Now Showing", null, null, 1, 1000);
                cboMovie.DataSource = movies.Items;
                cboMovie.DisplayMember = "Title";
                cboMovie.ValueMember = "MovieId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phim: " + ex.Message);
            }
        }

        private async Task LoadCinemas()
        {
            try
            {
                var cinemas = await _cinemaService.GetAllCinemasAsync();
                cboCinema.DataSource = cinemas;
                cboCinema.DisplayMember = "Name";
                cboCinema.ValueMember = "CinemaId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách rạp: " + ex.Message);
            }
        }

        private async Task LoadRoomsByCinema(int cinemaId)
        {
            try
            {
                var rooms = await _cinemaService.GetRoomsByCinemaAsync(cinemaId);
                cboRoom.DataSource = rooms;
                cboRoom.DisplayMember = "Name";
                cboRoom.ValueMember = "RoomId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message);
                cboRoom.DataSource = null;
            }
        }

        private async Task LoadShowtimeDetails(int id)
        {
            var showtime = await _adminShowtimeService.GetShowtimeByIdAsync(id);
            if (showtime != null)
            {
                // [SỬA ĐỔI] Tắt/Bật sự kiện đúng cách
                cboCinema.SelectedIndexChanged -= CboCinema_SelectedIndexChanged;

                cboMovie.SelectedValue = showtime.MovieId;
                cboCinema.SelectedValue = showtime.CinemaId;

                // Tải danh sách phòng rồi mới gán giá trị
                await LoadRoomsByCinema(showtime.CinemaId);
                cboRoom.SelectedValue = showtime.RoomId;

                dtpDate.Value = showtime.StartTime.Date;
                dtpTime.Value = showtime.StartTime;
                txtBasePrice.Text = showtime.BasePrice.ToString("F0");

                cboCinema.SelectedIndexChanged += CboCinema_SelectedIndexChanged;
            }
        }

        private async Task SaveShowtime()
        {
            // 1. Validate dữ liệu đầu vào từ Form
            // [SỬA ĐỔI] Thêm cboCinema.SelectedValue vào validation
            if (cboMovie.SelectedValue == null || cboCinema.SelectedValue == null || cboRoom.SelectedValue == null || string.IsNullOrWhiteSpace(txtBasePrice.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin (Phim, Rạp, Phòng, Giá vé).", "Thiếu thông tin");
                return;
            }

            if (!decimal.TryParse(txtBasePrice.Text, out decimal basePrice) || basePrice < 0)
            {
                MessageBox.Show("Giá vé không hợp lệ.", "Lỗi Dữ liệu");
                return;
            }
            // Gộp Ngày và Giờ lại thành 1 biến DateTime ---
            // Lấy phần Ngày từ dtpDate
            DateTime datePart = dtpDate.Value.Date;
            // Lấy phần Giờ/Phút/Giây từ dtpTime
            TimeSpan timePart = dtpTime.Value.TimeOfDay;
            // Gộp lại
            DateTime finalStartTime = datePart.Add(timePart);
            // 2. Cập nhật giao diện để báo hiệu đang xử lý
            btnSave.Enabled = false;
            btnSave.Text = "Đang lưu...";

            try
            {
                bool success = false;
                int movieId = (int)cboMovie.SelectedValue;
                int cinemaId = (int)cboCinema.SelectedValue;
                int roomId = (int)cboRoom.SelectedValue;
                // 3. Phân biệt logic Thêm mới và Cập nhật
                if (_showtimeId.HasValue)
                {
                    // --- LOGIC CẬP NHẬT ---
                    var dto = new UpdateShowtimeRequestDTO
                    {
                        MovieId = (int)cboMovie.SelectedValue,
                        RoomId = (int)cboRoom.SelectedValue,
                        CinemaId = cinemaId,
                        StartTime = finalStartTime,
                        BasePrice = basePrice,
                        Status = 1 // Mặc định giữ trạng thái Open
                    };
                    success = await _adminShowtimeService.UpdateShowtimeAsync(_showtimeId.Value, dto);
                }
                else
                {
                    // --- LOGIC THÊM MỚI ---
                    var dto = new CreateShowtimeRequestDTO
                    {
                        MovieId = movieId,
                        CinemaId = cinemaId,
                        RoomId = roomId,
                        StartTime = finalStartTime,
                        BasePrice = basePrice
                    };
                    success = await _adminShowtimeService.CreateShowtimeAsync(dto);
                }

                // 4. Xử lý kết quả trả về
                if (success)
                {
                    MessageBox.Show("Lưu lịch chiếu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnSaved?.Invoke(this, EventArgs.Empty); // Bắn sự kiện để quay về trang danh sách
                }
                else
                {
                    // Trường hợp này xảy ra khi API trả về false mà không ném lỗi
                    MessageBox.Show("Lưu thất bại do lỗi không xác định.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // [SỬA ĐỔI QUAN TRỌNG] Bắt các loại Exception cụ thể
            catch (InvalidOperationException ex) // Bắt lỗi nghiệp vụ như Trùng lịch, Đã có vé bán
            {
                MessageBox.Show(ex.Message, "Lỗi Nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex) // Bắt lỗi validation dữ liệu như Phim không tồn tại
            {
                MessageBox.Show(ex.Message, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Bắt các lỗi hệ thống khác (mất mạng, API sập...)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 5. Luôn trả lại trạng thái cho nút bấm dù thành công hay thất bại
                btnSave.Enabled = true;
                btnSave.Text = "LƯU";
            }
        }
    }
}