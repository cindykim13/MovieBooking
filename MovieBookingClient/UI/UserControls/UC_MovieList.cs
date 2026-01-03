using MovieBooking.Domain.DTOs; // Sử dụng DTO từ project Shared
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UCMovieList : UserControl
    {
        private readonly MovieService _movieService;
        private readonly FrmMain _mainForm;
        private int _currentPage = 1;
        private int _totalPages = 1;
        public string FilterStatus { get; set; } = "Now Showing";

        // Lưu trữ các tham số lọc hiện tại
        private string _currentStatus = "Now Showing"; // Mặc định
        private string _currentKeyword = null;
        private int? _currentGenreId = null;

        public UCMovieList(FrmMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _movieService = new MovieService();

            this.Load += UCMovieList_Load;
            btnNext.Click += async (s, e) => await ChangePage(_currentPage + 1);
            btnPrevious.Click += async (s, e) => await ChangePage(_currentPage - 1);
            // Sự kiện Tìm kiếm (Click nút hoặc nhấn Enter)
            btnSearch.Click += async (s, e) => await ExecuteSearch();
            txtSearch.KeyDown += async (s, e) => {
                if (e.KeyCode == Keys.Enter) await ExecuteSearch();
            };

            // Sự kiện Lọc Thể loại (Thay đổi là lọc ngay)
            cboGenre.SelectedIndexChanged += async (s, e) => await ExecuteSearch();
        }
        private async void UCMovieList_Load(object sender, EventArgs e)
        {
            await LoadGenresAsync();
            await LoadMoviesAsync();
        }
        private async Task LoadGenresAsync()
        {
            var genres = await _movieService.GetAllGenresAsync();
            if (genres != null)
            {
                // Thêm mục "Tất cả" vào đầu danh sách
                genres.Insert(0, new GenreDTO { GenreId = 0, GenreName = "Tất cả thể loại" });

                // Gán dữ liệu (Tắt event handler tạm thời để tránh trigger khi bind)
                cboGenre.SelectedIndexChanged -= async (s, e) => await ExecuteSearch();

                cboGenre.DataSource = genres;
                cboGenre.DisplayMember = "GenreName";
                cboGenre.ValueMember = "GenreId";
                cboGenre.SelectedIndex = 0;

                // Bật lại event handler
                cboGenre.SelectedIndexChanged += async (s, e) => await ExecuteSearch();
            }
        }
        // Hàm public để FrmMain gọi
        public async Task ReloadWithFilter(string status)
        {
            this.FilterStatus = status;
            _currentPage = 1; // Reset về trang 1
            await LoadMoviesAsync();
        }
        // HÀM CÔNG KHAI: Để FrmMain có thể yêu cầu lọc
        public async Task FilterByStatus(string status)
        {
            // Cập nhật trạng thái lọc và reset các bộ lọc khác
            _currentStatus = status;
            _currentKeyword = null;
            _currentGenreId = null;
            txtSearch.Clear();
            _currentPage = 1; // Reset về trang đầu
            await LoadMoviesAsync();
        }

        // HÀM CÔNG KHAI: Để thực hiện tìm kiếm
        private async Task SearchMovies()
        {
            _currentKeyword = txtSearch.Text;
            // Ở đây có thể lấy _currentGenreId từ cboGenre.SelectedValue
            _currentPage = 1;
            await LoadMoviesAsync();
        }
        // Hàm xử lý logic thu thập dữ liệu lọc
        private async Task ExecuteSearch()
        {
            // 1. Lấy Keyword
            _currentKeyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(_currentKeyword)) _currentKeyword = null;

            // 2. Lấy Genre ID
            if (cboGenre.SelectedValue is int id && id > 0)
            {
                _currentGenreId = id;
            }
            else
            {
                _currentGenreId = null; // Chọn "Tất cả" hoặc lỗi -> null
            }

            // 3. Reset về trang 1 và tải lại
            _currentPage = 1;
            await LoadMoviesAsync();
        }
        // HÀM TRUNG TÂM: Gọi API và render giao diện
        private async Task LoadMoviesAsync()
        {
            // Hiển thị Loading
            flowLayoutPanelMovies.Controls.Clear();
            Label lblLoading = new Label
            {
                Text = "Đang tải dữ liệu...",
                AutoSize = false,
                Width = flowLayoutPanelMovies.Width,
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 12)
            };
            flowLayoutPanelMovies.Controls.Add(lblLoading);

            try
            {
                // Gọi API Search với đầy đủ tham số
                var result = await _movieService.SearchMoviesAsync(
                    keyword: _currentKeyword,
                    status: _currentStatus,
                    genreId: _currentGenreId,
                    year: null,
                    pageIndex: _currentPage,
                    pageSize: 8 // Số lượng phim trên 1 trang (4 cột x 2 hàng)
                );

                flowLayoutPanelMovies.Controls.Clear();

                if (result != null && result.Items != null && result.Items.Any())
                {
                    _totalPages = result.TotalPages;

                    foreach (var movie in result.Items)
                    {
                        var card = new UCMovieCard();
                        card.SetMovieDetails(movie);
                        card.BuyTicketClicked += (s, id) => {
                            MessageBox.Show($"Xem chi tiết phim: {id}");
                            // TODO: Gọi FrmMain để chuyển trang chi tiết
                        };
                        flowLayoutPanelMovies.Controls.Add(card);
                    }
                }
                else
                {
                    flowLayoutPanelMovies.Controls.Add(new Label
                    {
                        Text = "Không tìm thấy phim phù hợp.",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.DimGray
                    });
                }

                UpdatePaginationUI();
            }
            catch (Exception ex)
            {
                flowLayoutPanelMovies.Controls.Clear();
                MessageBox.Show("Lỗi tải phim: " + ex.Message);
            }
        }

        private void Card_BuyTicketClicked(object sender, int movieId)
        {
            // Yêu cầu Form cha chuyển sang giao diện chi tiết phim
            // _mainForm.NavigateToMovieDetail(movieId); // Sẽ làm ở bước sau
            MessageBox.Show($"Chuyển sang trang chi tiết của phim có ID: {movieId}.", "Thông báo");
        }

        private void UpdatePaginationUI()
        {
            lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}";
            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < _totalPages;
        }

        private async Task ChangePage(int delta)
        {
            int newPage = _currentPage + delta;
            if (newPage >= 1 && newPage <= _totalPages)
            {
                _currentPage = newPage;
                await LoadMoviesAsync();
            }
        }
    }
}