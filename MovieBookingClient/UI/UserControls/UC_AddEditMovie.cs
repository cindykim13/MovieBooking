using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_AddEditMovie : UserControl
    {
        // Sự kiện để giao tiếp với UserControl cha (UC_Admin_Movies)
        public event EventHandler OnSaved;
        public event EventHandler OnCancelled;

        // --- Dependencies & State ---
        private readonly int? _movieId;
        private readonly AdminService _adminService;
        private readonly MovieService _movieService;
        private readonly CinemaService _cinemaService;
        private List<GenreDTO> _allGenres = new List<GenreDTO>(); // Biến lưu danh sách thể loại

        public UC_AddEditMovie(int? movieId)
        {
            InitializeComponent();
            _movieId = movieId;
            _adminService = new AdminService();
            _movieService = new MovieService();
            _cinemaService = new CinemaService();

            this.Load += async (s, e) => await InitializeForm();
            btnSave.Click += async (s, e) => await SaveMovie();
            btnCancel.Click += (s, e) => OnCancelled?.Invoke(this, EventArgs.Empty);
        }

        private async Task InitializeForm()
        {
            // Tải danh sách thể loại trước tiên để đổ vào CheckedListBox
            await LoadAllGenres();

            if (_movieId.HasValue) // Chế độ Sửa
            {
                lblTitle.Text = "CẬP NHẬT THÔNG TIN PHIM";
                await LoadMovieToEdit(_movieId.Value);
            }
            else // Chế độ Thêm mới
            {
                lblTitle.Text = "THÊM MỚI PHIM";
                cboStatus.SelectedItem = "Coming Soon";
            }
        }

        private async Task LoadAllGenres()
        {
            try
            {
                _allGenres = await _movieService.GetAllGenresAsync();
                if (_allGenres != null)
                {
                    // Đổ dữ liệu vào CheckedListBox
                    clbGenres.DataSource = _allGenres;
                    clbGenres.DisplayMember = "GenreName";
                    clbGenres.ValueMember = "GenreId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách thể loại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadMovieToEdit(int id)
        {
            var movie = await _movieService.GetMovieDetailAsync(id);
            if (movie != null)
            {
                // Đổ dữ liệu vào các control
                txtTitle.Text = movie.Title;
                txtStoryLine.Text = movie.StoryLine;
                txtDirector.Text = movie.Director;
                numDuration.Value = movie.Duration;
                numReleaseYear.Value = movie.ReleaseYear;
                txtAgeRating.Text = movie.AgeRating;
                numRating.Value = (decimal)movie.Rating;
                txtPosterUrl.Text = movie.PosterUrl;
                cboStatus.SelectedItem = movie.Status;
                txtCasts.Text = string.Join(", ", movie.Casts);

                // Logic tick vào các thể loại của phim
                for (int i = 0; i < clbGenres.Items.Count; i++)
                {
                    var genreItem = (GenreDTO)clbGenres.Items[i];
                    if (movie.Genres.Contains(genreItem.GenreName))
                    {
                        clbGenres.SetItemChecked(i, true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin phim để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnCancelled?.Invoke(this, EventArgs.Empty);
            }
        }

        private async Task SaveMovie()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Tên phim không được để trống.", "Thông tin thiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy danh sách thể loại từ CheckedListBox
            var selectedGenres = clbGenres.CheckedItems.Cast<GenreDTO>().Select(g => g.GenreName).ToList();
            if (!selectedGenres.Any())
            {
                MessageBox.Show("Vui lòng chọn ít nhất một thể loại.", "Thông tin thiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Đang lưu...";

            try
            {
                if (_movieId.HasValue) // Logic Sửa
                {
                    var dto = new UpdateMovieRequestDTO
                    {
                        Title = txtTitle.Text,
                        StoryLine = txtStoryLine.Text,
                        Director = txtDirector.Text,
                        Duration = (int)numDuration.Value,
                        ReleaseYear = (int)numReleaseYear.Value,
                        AgeRating = txtAgeRating.Text,
                        Rating = (double)numRating.Value,
                        PosterUrl = txtPosterUrl.Text,
                        Status = cboStatus.SelectedItem.ToString(),
                        Genres = selectedGenres, // Gán danh sách đã chọn
                        Casts = txtCasts.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList()
                    };
                    await _adminService.UpdateMovieAsync(_movieId.Value, dto);
                }
                else // Logic Thêm mới
                {
                    var dto = new AddMovieRequestDTO
                    {
                        Title = txtTitle.Text,
                        StoryLine = txtStoryLine.Text,
                        Director = txtDirector.Text,
                        Duration = (int)numDuration.Value,
                        ReleaseYear = (int)numReleaseYear.Value,
                        AgeRating = txtAgeRating.Text,
                        Rating = (double)numRating.Value,
                        PosterUrl = txtPosterUrl.Text,
                        Status = cboStatus.SelectedItem.ToString(),
                        Genres = selectedGenres, // Gán danh sách đã chọn
                        Casts = txtCasts.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList()
                    };
                    await _adminService.AddMovieAsync(dto);
                }

                MessageBox.Show("Lưu thông tin phim thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lưu thất bại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "LƯU";
            }
        }
    }
}