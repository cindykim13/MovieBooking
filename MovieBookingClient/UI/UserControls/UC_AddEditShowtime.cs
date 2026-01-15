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
                dtpStartTime.Value = DateTime.Now.AddHours(1); // Mặc định 1h sau
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
                // Tạm tắt sự kiện để tránh gọi LoadRoomsByCinema 2 lần
                cboCinema.SelectedIndexChanged -= async (s, e) => { /*...*/ };

                cboMovie.SelectedValue = showtime.MovieId;
                cboCinema.SelectedValue = showtime.CinemaId;

                await LoadRoomsByCinema(showtime.CinemaId);

                cboRoom.SelectedValue = showtime.RoomId;
                dtpStartTime.Value = showtime.StartTime;
                txtBasePrice.Text = showtime.BasePrice.ToString("F0");

                // Bật lại sự kiện
                cboCinema.SelectedIndexChanged += async (s, e) => { /*...*/ };
            }
        }

        private async Task SaveShowtime()
        {
            // Validate
            if (cboMovie.SelectedValue == null || cboRoom.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Phim và Phòng chiếu.", "Thiếu thông tin");
                return;
            }

            bool success = false;

            if (_showtimeId.HasValue) // Chế độ Sửa
            {
                var dto = new UpdateShowtimeRequestDTO
                {
                    MovieId = (int)cboMovie.SelectedValue,
                    RoomId = (int)cboRoom.SelectedValue,
                    StartTime = dtpStartTime.Value,
                    BasePrice = decimal.Parse(txtBasePrice.Text),
                    Status = 1 // Giả sử mặc định là Open
                };
                success = await _adminShowtimeService.UpdateShowtimeAsync(_showtimeId.Value, dto);
            }
            else // Chế độ Thêm
            {
                var dto = new CreateShowtimeRequestDTO
                {
                    MovieId = (int)cboMovie.SelectedValue,
                    RoomId = (int)cboRoom.SelectedValue,
                    StartTime = dtpStartTime.Value,
                    BasePrice = decimal.Parse(txtBasePrice.Text)
                };
                success = await _adminShowtimeService.CreateShowtimeAsync(dto);
            }

            if (success)
            {
                MessageBox.Show("Lưu lịch chiếu thành công!");
                OnSaved?.Invoke(this, EventArgs.Empty); // Kích hoạt sự kiện để quay về
            }
            else
            {
                MessageBox.Show("Lưu thất bại. Vui lòng kiểm tra lại thông tin (có thể bị trùng lịch).");
            }
        }
    }
}