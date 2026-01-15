using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using System;
using System.IO; // Cần cho OpenFileDialog
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_Admin_Movies : UserControl
    {
        private readonly MovieService _movieService;
        private readonly AdminService _adminService;

        // [SỬA LỖI CS0104]: Chỉ định rõ Timer của Windows Forms
        private readonly System.Windows.Forms.Timer _searchTimer;

        public UC_Admin_Movies()
        {
            InitializeComponent();
            _movieService = new MovieService();
            _adminService = new AdminService();

            // Khởi tạo Timer
            _searchTimer = new System.Windows.Forms.Timer { Interval = 500 };

            InitializeEvents();
            ConfigureDataGridView();
        }

        private void InitializeEvents()
        {
            this.Load += async (s, e) => await LoadData();

            _searchTimer.Tick += async (s, e) => {
                _searchTimer.Stop();
                await LoadData(txtSearch.Text.Trim());
            };

            txtSearch.TextChanged += (s, e) => {
                _searchTimer.Stop();
                _searchTimer.Start();
            };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnImport.Click += BtnImport_Click;
        }

        private void ConfigureDataGridView()
        {
            dgvMovies.AutoGenerateColumns = false;
            dgvMovies.Columns.Clear();

            dgvMovies.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MovieId", // Đặt tên để truy cập
                DataPropertyName = "MovieId",
                HeaderText = "ID",
                Width = 50
            });
            dgvMovies.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title", // Đặt tên
                DataPropertyName = "Title",
                HeaderText = "Tên Phim",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvMovies.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReleaseYear",
                DataPropertyName = "ReleaseYear",
                HeaderText = "Năm",
                Width = 80
            });
            dgvMovies.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Duration",
                DataPropertyName = "Duration",
                HeaderText = "Thời lượng (phút)",
                Width = 150
            });
            dgvMovies.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Rating",
                DataPropertyName = "Rating",
                HeaderText = "Đánh giá",
                Width = 100
            });
            dgvMovies.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Trạng thái",
                Width = 120
            });
        }

        private async Task LoadData(string keyword = "")
        {
            try
            {
                var result = await _movieService.SearchMoviesAsync(keyword, null, null, null, 1, 100);
                if (result != null)
                {
                    dgvMovies.DataSource = result.Items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phim: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- SỰ KIỆN CÁC NÚT BẤM ---
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ShowAddEditForm(null); // Gọi form Thêm (không có ID)
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bộ phim để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int selectedMovieId = Convert.ToInt32(dgvMovies.SelectedRows[0].Cells["MovieId"].Value);
            ShowAddEditForm(selectedMovieId); // Gọi form Sửa với ID đã chọn
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bộ phim để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedMovieId = Convert.ToInt32(dgvMovies.SelectedRows[0].Cells["MovieId"].Value);
            string movieTitle = dgvMovies.SelectedRows[0].Cells["Title"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phim '{movieTitle}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _adminService.DeleteMovieAsync(selectedMovieId);
                    MessageBox.Show("Xóa phim thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadData(txtSearch.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa phim: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "Chọn file dữ liệu phim"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Stream fileStream = openFileDialog.OpenFile())
                        {
                            int count = await _adminService.ImportMoviesFromFileAsync(fileStream, openFileDialog.FileName);
                            MessageBox.Show($"Import thành công! Đã thêm {count} phim mới.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi import file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- HÀM QUẢN LÝ HIỂN THỊ ---

        private void ShowAddEditForm(int? movieId)
        {
            var ucAddEdit = new UC_AddEditMovie(movieId);
            ucAddEdit.Dock = DockStyle.Fill;

            // Đăng ký sự kiện
            ucAddEdit.OnSaved += async (s, e) => {
                this.Controls.Remove(ucAddEdit);
                ShowGridAndToolbar(true);
                await LoadData();
            };

            ucAddEdit.OnCancelled += (s, e) => {
                this.Controls.Remove(ucAddEdit);
                ShowGridAndToolbar(true);
            };

            // Ẩn lưới và hiện form
            ShowGridAndToolbar(false);
            this.Controls.Add(ucAddEdit);
            ucAddEdit.BringToFront();
        }

        private void ShowGridAndToolbar(bool show)
        {
            panelHeader.Visible = show;
            dgvMovies.Visible = show;
        }
    }
}