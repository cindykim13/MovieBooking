using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_Admin_Showtimes : UserControl
    {
        private readonly AdminShowtimeService _adminShowtimeService;

        public UC_Admin_Showtimes()
        {
            InitializeComponent();
            _adminShowtimeService = new AdminShowtimeService();

            // Chỉ gán sự kiện trong Constructor
            this.Load += async (s, e) => await LoadData();
            btnXem.Click += async (s, e) => await LoadData();
            btnAdd.Click += (s, e) => ShowAddEditForm(null);
            btnSua.Click += (s, e) => HandleUpdateClick();
            btnXoa.Click += async (s, e) => await HandleDeleteClick();
        }

        private async Task LoadData()
        {
            try
            {
                var list = await _adminShowtimeService.GetShowtimesByDateAsync(dtpNgayChieu.Value);
                dgvLichChieu.DataSource = list;
                FormatColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu lịch chiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvLichChieu.DataSource = null;
            }
        }

        private void FormatColumns()
        {
            if (dgvLichChieu.Columns.Count == 0) return;

            // Ẩn các cột kỹ thuật
            string[] hideColumns = { "ShowtimeId", "MovieId", "RoomId", "CinemaId", "Status", "EndTime" };
            foreach (var colName in hideColumns)
            {
                if (dgvLichChieu.Columns.Contains(colName))
                {
                    dgvLichChieu.Columns[colName].Visible = false;
                }
            }

            // Đặt lại tên và định dạng cho các cột hiển thị
            if (dgvLichChieu.Columns.Contains("MovieTitle")) dgvLichChieu.Columns["MovieTitle"].HeaderText = "Tên Phim";
            if (dgvLichChieu.Columns.Contains("CinemaName")) dgvLichChieu.Columns["CinemaName"].HeaderText = "Tên Rạp";
            if (dgvLichChieu.Columns.Contains("RoomName")) dgvLichChieu.Columns["RoomName"].HeaderText = "Phòng Chiếu";
            if (dgvLichChieu.Columns.Contains("StartTime"))
            {
                dgvLichChieu.Columns["StartTime"].HeaderText = "Giờ Bắt Đầu";
                dgvLichChieu.Columns["StartTime"].DefaultCellStyle.Format = "HH:mm";
            }
            if (dgvLichChieu.Columns.Contains("BasePrice"))
            {
                dgvLichChieu.Columns["BasePrice"].HeaderText = "Giá Vé Gốc (VND)";
                dgvLichChieu.Columns["BasePrice"].DefaultCellStyle.Format = "N0";
            }
        }
        // --- HÀM XỬ LÝ SỰ KIỆN NÚT BẤM ---

        private void HandleUpdateClick()
        {
            if (dgvLichChieu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một suất chiếu để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dgvLichChieu.SelectedRows[0].Cells["ShowtimeId"].Value);
            ShowAddEditForm(id);
        }

        private async Task HandleDeleteClick()
        {
            if (dgvLichChieu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một suất chiếu để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvLichChieu.SelectedRows[0].Cells["ShowtimeId"].Value);
            string movieTitle = dgvLichChieu.SelectedRows[0].Cells["MovieTitle"].Value?.ToString() ?? "Không rõ";
            string startTime = Convert.ToDateTime(dgvLichChieu.SelectedRows[0].Cells["StartTime"].Value).ToString("HH:mm");

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa/hủy suất chiếu của phim '{movieTitle}' lúc {startTime} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool success = await _adminShowtimeService.DeleteShowtimeAsync(id);
                    if (success)
                    {
                        MessageBox.Show("Thao tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Thao tác thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thực hiện: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // --- HÀM QUẢN LÝ HIỂN THỊ ---

        private void ShowAddEditForm(int? id)
        {
            // Tạo UserControl mới để Thêm/Sửa
            var ucAddEdit = new UC_AddEditShowtime(id);
            ucAddEdit.Dock = DockStyle.Fill;

            // Đăng ký sự kiện để biết khi nào nó hoàn thành
            ucAddEdit.OnSaved += async (s, e) => {
                this.Controls.Remove(ucAddEdit);
                ShowGridAndToolbar(true);
                await LoadData(); // Tải lại dữ liệu sau khi lưu
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
            pnlHeader.Visible = show;
            dgvLichChieu.Visible = show;
        }
    }
}