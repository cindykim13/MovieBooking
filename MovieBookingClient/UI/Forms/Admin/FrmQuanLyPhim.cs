using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services; // Namespace chứa MovieService
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace MovieBookingClient.UI.Forms.Admin
{
    public partial class FrmQuanLyPhim : UserControl
    {
        // Service giả định (Bạn cần đảm bảo bên Client có class này)
        private readonly MovieService _movieService;

        public FrmQuanLyPhim()
        {
            InitializeComponent();
            _movieService = new MovieService(); // Khởi tạo service
        }

        private async void frmAdmin_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        // --- HÀM TẢI DỮ LIỆU ---
        private async Task LoadData(string keyword = "")
        {
            try
            {
                // Gọi hàm Search thay vì GetPaged vì nó linh hoạt hơn
                // Tham số: keyword, status, genreId, year, pageIndex, pageSize
                // Truyền null/0 cho các tham số không dùng để lấy tất cả
                var result = await _movieService.SearchMoviesAsync(keyword, null, null, null, 1, 100);

                if (result != null && result.Items != null)
                {
                    BindGrid(result.Items);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- ĐỔ DỮ LIỆU VÀO BẢNG ---
        private void BindGrid(List<MovieDTO> list)
        {
            dgvPhim.DataSource = null;
            dgvPhim.DataSource = list;

            // Ẩn các cột không cần thiết
            string[] cotAn = { "PosterUrl", "Description" }; // Thêm tên cột muốn ẩn
            foreach (var col in cotAn)
            {
                if (dgvPhim.Columns[col] != null) dgvPhim.Columns[col].Visible = false;
            }

            // Đặt tên tiếng Việt cho cột
            if (dgvPhim.Columns["MovieId"] != null) dgvPhim.Columns["MovieId"].HeaderText = "Mã Phim";
            if (dgvPhim.Columns["Title"] != null) dgvPhim.Columns["Title"].HeaderText = "Tên Phim";
            if (dgvPhim.Columns["Duration"] != null) dgvPhim.Columns["Duration"].HeaderText = "Thời lượng";
            if (dgvPhim.Columns["ReleaseYear"] != null) dgvPhim.Columns["ReleaseYear"].HeaderText = "Năm SX";
            if (dgvPhim.Columns["Genres"] != null) dgvPhim.Columns["Genres"].HeaderText = "Thể Loại";
            if (dgvPhim.Columns["Status"] != null) dgvPhim.Columns["Status"].HeaderText = "Trạng Thái";

            dgvPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- TÌM KIẾM ---
        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Vì Server có hàm Search xịn rồi, ta gọi thẳng Server luôn (hoặc lọc RAM nếu muốn)
            // Ở đây mình gọi Server để test tính năng SearchMoviesAsync
            await LoadData(textBox1.Text.Trim());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thêm đang phát triển");
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}