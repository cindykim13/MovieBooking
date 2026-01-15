using MovieBooking.Domain.DTOs;
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
        public string FilterStatus
        {
            get => _currentStatus;
            set => _currentStatus = value; // Setter chỉ gán giá trị, không trigger load
        }

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
            // [SỬA LỖI TẠI ĐÂY]
            // Gán đúng hành động cho từng nút
            btnNext.Click += async (s, e) => await ChangePage(1);  // +1 để sang trang tiếp
            btnPrevious.Click += async (s, e) => await ChangePage(-1); // -1 để về trang trước
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
        // HÀM CÔNG KHAI: Để FrmMain có thể yêu cầu lọc
        // 1. Hàm lọc theo Status (Gọi từ Menu Phim)
        public async Task FilterByStatus(string status)
        {
            // Cập nhật trạng thái và reset các bộ lọc tìm kiếm khác
            _currentStatus = status;
            _currentKeyword = null;
            _currentGenreId = null;

            // Reset UI tìm kiếm
            txtSearch.Clear();
            if (cboGenre.Items.Count > 0) cboGenre.SelectedIndex = 0;

            _currentPage = 1;
            await LoadMoviesAsync();
        }
        public async Task ResetToDefault()
        {
            // 1. Reset các biến trạng thái về mặc định
            this._currentPage = 1;
            this._currentStatus = "Now Showing"; // Mặc định về Phim đang chiếu
            this._currentKeyword = null;
            this._currentGenreId = null;

            // 2. Reset giao diện bộ lọc
            txtSearch.Clear();
            if (cboGenre.Items.Count > 0) cboGenre.SelectedIndex = 0; // Chọn "Tất cả"

            // 3. Reset giao diện Tab (Nếu bạn có 2 nút này trong UC_MovieList.Designer.cs)
            // Nếu trong Designer của UC_MovieList KHÔNG CÓ 2 nút này thì XÓA 2 dòng dưới đi
            if (this.Controls.ContainsKey("btnNowShowing")) ((Guna.UI2.WinForms.Guna2Button)this.Controls["btnNowShowing"]).Checked = true;
            if (this.Controls.ContainsKey("btnComingSoon")) ((Guna.UI2.WinForms.Guna2Button)this.Controls["btnComingSoon"]).Checked = false;

            // 4. Tải lại dữ liệu
            await LoadMoviesAsync();
        }

        // --- PRIVATE METHODS (Logic nội bộ) ---
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
                    pageSize: 12 // Số lượng phim trên 1 trang (4 cột x 2 hàng)
                );

                flowLayoutPanelMovies.Controls.Clear();

                if (result != null && result.Items != null && result.Items.Any())
                {
                    _totalPages = result.TotalPages;

                    foreach (var movie in result.Items)
                    {
                        var card = new UCMovieCard();
                        card.SetMovieDetails(movie);
                        // 1. Gán sự kiện cho nút "Mua vé" -> Chuyển sang trang Chọn Lịch
                        card.BuyTicketClicked += (s, movieId) => {
                            _mainForm.NavigateToSelectShowtime(movieId);
                        };
                        // 2. Gán sự kiện cho Poster/Title -> Chuyển sang trang Chi tiết
                        card.ViewDetailClicked += (s, movieId) => {
                            _mainForm.NavigateToMovieDetail(movieId);
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
        private void UpdatePaginationUI()
        {
            lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}";
            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < _totalPages;
        }

        // Hàm ChangePage (Đảm bảo logic đúng)
        private async Task ChangePage(int delta)
        {
            int newPage = _currentPage + delta; // Tính toán trang mới
            if (newPage >= 1 && newPage <= _totalPages)
            {
                _currentPage = newPage;
                await LoadMoviesAsync();
            }
        }
    }
}