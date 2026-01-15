using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using MovieBookingClient.Session; // Đảm bảo import để kiểm tra Session
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.Forms.Admin
{
    public partial class FrmQuanLyPhim : UserControl
    {
        private readonly MovieService _movieService;

        public FrmQuanLyPhim()
        {
            InitializeComponent();
            _movieService = new MovieService();

            // --- 1. ĐĂNG KÝ SỰ KIỆN ---

            if (this.btnThem != null)
            {
                this.btnThem.Click -= btnThem_Click;
                this.btnThem.Click += btnThem_Click;
            }

            if (this.btnSua != null)
            {
                this.btnSua.Click -= btnSua_Click;
                this.btnSua.Click += btnSua_Click;
            }

            if (this.btnXoa != null)
            {
                this.btnXoa.Click -= btnXoa_Click;
                this.btnXoa.Click += btnXoa_Click;
            }

            // Đăng ký sự kiện Load
            this.Load += async (s, e) => await LoadData();
        }

        // --- 2. HÀM TẢI DỮ LIỆU ---
        private async Task LoadData(string keyword = "")
        {
            try
            {
                var result = await _movieService.SearchMoviesAsync(keyword, null, null, null, 1, 100);

                if (result != null && result.Items != null)
                {
                    BindGrid(result.Items);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi Load Phim: " + ex.Message);
            }
        }

        private void BindGrid(List<MovieDTO> list)
        {
            if (dgvPhim.InvokeRequired)
            {
                dgvPhim.Invoke(new Action(() => BindGrid(list)));
                return;
            }

            dgvPhim.DataSource = null;
            dgvPhim.AutoGenerateColumns = false;
            dgvPhim.Columns.Clear();

            dgvPhim.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 50 });
            dgvPhim.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Tên Phim", Width = 200 });
            dgvPhim.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Duration", HeaderText = "Thời lượng", Width = 80 });
            dgvPhim.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReleaseYear", HeaderText = "Năm", Width = 60 });
            dgvPhim.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Điểm", Width = 60 });
            dgvPhim.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Genres", HeaderText = "Thể Loại", Width = 150 });

            dgvPhim.DataSource = list;
            dgvPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhim.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Đảm bảo chọn cả dòng
        }

        // --- 3. TÌM KIẾM ---
        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            await LoadData(textBox1.Text.Trim());
        }

        // --- 4. CHỨC NĂNG THÊM ---
        private async void btnThem_Click(object sender, EventArgs e)
        {
            using (FrmThemPhim frm = new FrmThemPhim())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        // --- 5. CHỨC NĂNG SỬA ---
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvPhim.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phim cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var movieItem = (MovieDTO)dgvPhim.SelectedRows[0].DataBoundItem;

            using (FrmThemPhim frm = new FrmThemPhim(movieItem.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        // --- 6. CHỨC NĂNG XÓA (ĐÃ CẬP NHẬT) ---
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra lựa chọn
            if (dgvPhim.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phim cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra trạng thái đăng nhập trước khi gọi API
            if (!SessionManager.Instance.IsLoggedIn)
            {
                MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var movieItem = (MovieDTO)dgvPhim.SelectedRows[0].DataBoundItem;

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa phim: {movieItem.Title}?\nHành động này không thể hoàn tác.",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Gọi Service (Lúc này MovieService sẽ dùng Token từ SessionManager)
                    bool isDeleted = await _movieService.DeleteMovieAsync(movieItem.Id);

                    if (isDeleted)
                    {
                        MessageBox.Show("Đã xóa phim thành công.", "Thông báo");
                        await LoadData(); // Cập nhật lại danh sách ngay lập tức
                    }
                    // Lưu ý: Nếu thất bại (401/403), BaseApiService đã tự hiện MessageBox rồi.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}